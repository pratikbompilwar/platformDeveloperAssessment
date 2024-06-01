using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using assessment_platform_developer.Services;
using Container = SimpleInjector.Container;
using assessment_platform_developer.Services.Interfaces;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Reflection.Emit;
using System.Net.NetworkInformation;

namespace assessment_platform_developer
{
	public partial class Customers : Page
	{
		private static List<Customer> customers = new List<Customer>();

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
				var customerService = testContainer.GetInstance<IGetAllCustomerService>();

				var allCustomers = customerService.GetAllCustomers();
				ViewState["Customers"] = allCustomers;
			}
			else
			{
				customers = (List<Customer>)ViewState["Customers"];
			}

			PopulateCustomerListBox();
			PopulateCustomerDropDownLists();
		}

		private void PopulateCustomerDropDownLists()
		{

			var countryList = Enum.GetValues(typeof(Countries))
				.Cast<Countries>()
				.Select(c => new ListItem
				{
					Text = c.ToString(),
					Value = ((int)c).ToString()
				})
				.ToArray();

			CountryDropDownList.Items.AddRange(countryList);
			CountryDropDownList.SelectedValue = ((int)Countries.Canada).ToString();


			var provinceList = Enum.GetValues(typeof(CanadianProvinces))
				.Cast<CanadianProvinces>()
				.Select(p => new ListItem
				{
					Text = p.ToString(),
					Value = ((int)p).ToString()
				})
				.ToArray();

			StateDropDownList.Items.Add(new ListItem(""));
			StateDropDownList.Items.AddRange(provinceList);
		}

		protected void PopulateCustomerListBox()
		{
			CustomersDDL.Items.Clear();
			var storedCustomers = customers.Select(c => new ListItem(c.Name)).ToArray();
			if (storedCustomers.Length != 0)
			{
				CustomersDDL.Items.AddRange(storedCustomers);
				CustomersDDL.SelectedIndex = 0;
				return;
			}

			CustomersDDL.Items.Add(new ListItem("Add new customer"));
		}

		protected void AddButton_Click(object sender, EventArgs e)
		{
			var customer = new Customer
			{
				Name = CustomerName.Text,
				Address = CustomerAddress.Text,
				City = CustomerCity.Text,
				State = StateDropDownList.SelectedValue,
				Zip = CustomerZip.Text,
				Country = CountryDropDownList.SelectedValue,
				Email = CustomerEmail.Text,
				Phone = CustomerPhone.Text,
				Notes = CustomerNotes.Text,
				ContactName = ContactName.Text,
				ContactPhone = CustomerPhone.Text,
				ContactEmail = CustomerEmail.Text
			};

			//validate form
			List<string> errors = ValidateCustomerForm(customer);

            if (errors.Count > 0)
			{
				ErrorLabel.Text = "- " + errors.Aggregate((x, y) => x + ".<br />- " + y);

            }

			var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
			var customerService = testContainer.GetInstance<IAddCustomerService>();
			customerService.AddCustomer(customer);
			customers.Add(customer);

			CustomersDDL.Items.Add(new ListItem(customer.Name));

			CustomerName.Text = string.Empty;
			CustomerAddress.Text = string.Empty;
			CustomerEmail.Text = string.Empty;
			CustomerPhone.Text = string.Empty;
			CustomerCity.Text = string.Empty;
			StateDropDownList.SelectedIndex = 0;
			CustomerZip.Text = string.Empty;
			CountryDropDownList.SelectedIndex = 0;
			CustomerNotes.Text = string.Empty;
			ContactName.Text = string.Empty;
			ContactPhone.Text = string.Empty;
			ContactEmail.Text = string.Empty;
		}

        private List<string> ValidateCustomerForm(Customer inputCustomerObj)
        {
			List<string> errors = new List<string>();
            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$";
            string usZipCodePattern = @"^\d{5}(-\d{4})?$";
			string canadaZipCodePattern = @"^[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$";

            //validate input Name
            if (string.IsNullOrEmpty(inputCustomerObj.Name) || inputCustomerObj.Name.Length > 50)
            {
				errors.Add("Name is required or Name cannot be longer than 50 characters");
            }

            //validate input Address
            if (string.IsNullOrEmpty(inputCustomerObj.Address) || inputCustomerObj.Address.Length > 200)
            {
                errors.Add("Address is required or Address cannot be longer than 200 characters");
            }

            //validate input City
            if (string.IsNullOrEmpty(inputCustomerObj.City) || inputCustomerObj.City.Length > 50)
            {
                errors.Add("City is required or City cannot be longer than 50 characters");
            }

            //validate input State
            if (string.IsNullOrEmpty(inputCustomerObj.State) || inputCustomerObj.State.Length > 50)
            {
                errors.Add("State is required or State cannot be longer than 50 characters");
            }

            //validate input Zip
            if (string.IsNullOrEmpty(inputCustomerObj.Zip) || inputCustomerObj.Zip.Length > 7)
            {
                errors.Add("Zip is required or Zip cannot be longer than 7 characters");
            }

            if (!string.IsNullOrEmpty(inputCustomerObj.Country) && inputCustomerObj.Country == "United States" && !string.IsNullOrEmpty(inputCustomerObj.Zip) && !Regex.IsMatch(inputCustomerObj.Zip, usZipCodePattern))
			{
                errors.Add("Invalid US Zip Code");
            }

            if (!string.IsNullOrEmpty(inputCustomerObj.Country) && inputCustomerObj.Country == "Canada" && !string.IsNullOrEmpty(inputCustomerObj.Zip) && !Regex.IsMatch(inputCustomerObj.Zip, canadaZipCodePattern))
            {
                errors.Add("Invalid Canada Zip Code");
            }


            //validate input Country
            if (string.IsNullOrEmpty(inputCustomerObj.Country) || inputCustomerObj.Country.Length > 50)
            {
                errors.Add("Country is required or Country cannot be longer than 50 characters");
            }

            //validate input Email
            if (!Regex.IsMatch(inputCustomerObj.Email, emailPattern))
            {
                errors.Add("Email is Invalid");
            }

            //validate input Phone
            if (string.IsNullOrEmpty(inputCustomerObj.Phone) || inputCustomerObj.Phone.Length > 10)
            {
                errors.Add("Phone is required or Phone cannot be longer than 10 characters");
            }

            //validate input Notes
            if (!string.IsNullOrEmpty(inputCustomerObj.Notes) && inputCustomerObj.City.Length > 200)
            {
                errors.Add("Notes cannot be longer than 200 characters");
            }

            //validate input Contact Name
            if (string.IsNullOrEmpty(inputCustomerObj.ContactName) || inputCustomerObj.ContactName.Length > 50)
            {
                errors.Add("ContactName is required or ContactName cannot be longer than 50 characters");
            }

            //validate input Contact Phone
            if (string.IsNullOrEmpty(inputCustomerObj.ContactPhone) || inputCustomerObj.ContactPhone.Length > 10)
            {
                errors.Add("Contact phone is required or Contact phone cannot be longer than 10 characters");
            }

            //validate input Contact Email
            if (!Regex.IsMatch(inputCustomerObj.ContactEmail, emailPattern))
            {
                errors.Add("Contact email is Invalid");
            }

            return errors;
        }
    }
}