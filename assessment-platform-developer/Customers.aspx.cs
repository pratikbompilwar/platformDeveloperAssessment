using assessment_platform_developer.Models;
using assessment_platform_developer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Container = SimpleInjector.Container;

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
                PopulateCustomerListBox();
                PopulateCustomerDropDownLists();
            }
			else
			{
				customers = (List<Customer>)ViewState["Customers"];
			}

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

        private void PopulateStateDropDownListsBasedOnCountry(string country)
        {
            StateDropDownList.Items.Clear();
            if (country == "0")
            {
                var canadianProvinceList = Enum.GetValues(typeof(CanadianProvinces))
                    .Cast<CanadianProvinces>()
                    .Select(p => new ListItem
                    {
                        Text = p.ToString(),
                        Value = ((int)p).ToString()
                    })
                    .ToArray();

                StateDropDownList.Items.Add(new ListItem(""));
                StateDropDownList.Items.AddRange(canadianProvinceList);
            }
            else
            {
                var usProvinceList = Enum.GetValues(typeof(USStates))
                    .Cast<USStates>()
                    .Select(p => new ListItem
                    {
                        Text = p.ToString(),
                        Value = ((int)p).ToString()
                    })
                    .ToArray();

                StateDropDownList.Items.Add(new ListItem(""));
                StateDropDownList.Items.AddRange(usProvinceList);
            }
        }

        protected void PopulateCustomerListBox()
		{
			CustomersDDL.Items.Clear();
			var storedCustomers = customers.Select(c => new ListItem(c.Name)).ToArray();
			if (storedCustomers.Length != 0)
			{
                CustomersDDL.Items.Add(new ListItem("Add new customer"));
                CustomersDDL.Items.AddRange(storedCustomers);				
				return;
			}

			CustomersDDL.Items.Add(new ListItem("Add new customer"));
		}

        /// <summary>
        /// Click event for ADD button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void AddButton_Click(object sender, EventArgs e)
		{
            //customer data from form
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
                ContactPhone = ContactPhone.Text,
                ContactEmail = ContactEmail.Text,
                ID = customers.Count == 0 ? 1 : (customers.Last().ID + 1)
            };

			//validate form
			List<string> errors = ValidateCustomerForm(customer);

            if (errors.Count > 0)
            {
                ErrorLabel.ForeColor = Color.Red;
                ErrorLabel.Text = "- " + errors.Aggregate((x, y) => x + ".<br />- " + y);

            }
            else
            {
                if(!customers.Any(x=> x.ContactName == CustomerName.Text) && CustomerName.Text != "Add new customer")
                {

                    var testContainer = (Container)HttpContext.Current.Application["DIContainer"];
                    var customerService = testContainer.GetInstance<IAddCustomerService>();
                    customerService.AddCustomer(customer);
                    customers.Add(customer);

                    PopulateCustomerListBox();
                    CustomersDDL.SelectedIndex = 0;

                    ErrorLabel.ForeColor = Color.Green;
                    ErrorLabel.Text = "Customer with name " + CustomerName.Text + " added succefully.";

                    //clearing form
                    ClearForm();
                }
                else
                {
                    ErrorLabel.ForeColor = Color.Red;
                    ErrorLabel.Text = "- Customer name already exist. Please try changing customer name.";
                }

            }
        }

        /// <summary>
        /// click event for update button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            bool areErrors = false;

            var updatedCustomers = customers.Select(customer =>
            {                
                if (customer.ID == Convert.ToInt32(IDLabel.Text))
                {
                    //updated customer
                    Customer updatedCustomer = new Customer
                    {
                        Name = CustomerName.Text,
                        Address = customer.Address == CustomerAddress.Text ? customer.Address : CustomerAddress.Text,
                        City = customer.City == CustomerCity.Text ? customer.City : CustomerCity.Text,
                        State = customer.State == StateDropDownList.Text ? customer.State : StateDropDownList.Text,
                        Zip = customer.Zip == CustomerZip.Text ? customer.Zip : CustomerZip.Text,
                        Country = customer.Country == CountryDropDownList.Text ? customer.Country : CountryDropDownList.Text,
                        Email = customer.Email == CustomerEmail.Text ? customer.Email : CustomerEmail.Text,
                        Phone = customer.Phone == CustomerPhone.Text ? customer.Phone : CustomerPhone.Text,
                        Notes = customer.Notes == CustomerNotes.Text ? customer.Notes : CustomerNotes.Text,
                        ContactName = customer.ContactName == ContactName.Text ? customer.ContactName : ContactName.Text,
                        ContactPhone = customer.ContactPhone == ContactPhone.Text ? customer.ContactPhone : ContactPhone.Text,
                        ContactEmail = customer.ContactEmail == ContactEmail.Text ? customer.ContactEmail : ContactEmail.Text,
                        ID = customer.ID
                    };

                    //validate form                
                    List<string> errors = ValidateCustomerForm(updatedCustomer);

                    if (errors.Count > 0)
                    {
                        ErrorLabel.ForeColor = Color.Red;
                        ErrorLabel.Text = "- " + errors.Aggregate((x, y) => x + ".<br />- " + y);
                        areErrors = true;
                    }
                    else
                    {
                        //customer = updatedCustomer;
                        customer.Address = customer.Address == CustomerAddress.Text ? customer.Address : CustomerAddress.Text;
                        customer.City = customer.City == CustomerCity.Text ? customer.City : CustomerCity.Text;
                        customer.State = customer.State == StateDropDownList.Text ? customer.State : StateDropDownList.Text;
                        customer.Zip = customer.Zip == CustomerZip.Text ? customer.Zip : CustomerZip.Text;
                        customer.Country = customer.Country == CountryDropDownList.Text ? customer.Country : CountryDropDownList.Text;
                        customer.Email = customer.Email == CustomerEmail.Text ? customer.Email : CustomerEmail.Text;
                        customer.Phone = customer.Phone == CustomerPhone.Text ? customer.Phone : CustomerPhone.Text;
                        customer.Notes = customer.Notes == CustomerNotes.Text ? customer.Notes : CustomerNotes.Text;
                        customer.ContactName = customer.ContactName == ContactName.Text ? customer.ContactName : ContactName.Text;
                        customer.ContactPhone = customer.ContactPhone == ContactPhone.Text ? customer.ContactPhone : ContactPhone.Text;
                        customer.ContactEmail = customer.ContactEmail == ContactEmail.Text ? customer.ContactEmail : ContactEmail.Text;
                    }
                }  
                
                return customer;                

            }).ToList();            

            customers = areErrors ? customers: updatedCustomers;

            if (!areErrors)
            {
                //messages
                ErrorLabel.ForeColor = Color.Green;
                ErrorLabel.Text = "Customer with name " + CustomerName.Text + " updated succefully.";

                //show add button and hide update/ delete button
                AddButton.Visible = true;
                UpdateButton.Visible = false;
                DeleteButton.Visible = false;
                CustomerName.Enabled = true;

                HeaderCustomer.InnerText = "Add customer";

                //updating customerList dropdown
                PopulateCustomerListBox();
                CustomersDDL.SelectedIndex = 0;

                //clearing form
                ClearForm();
            }

        }

        /// <summary>
        /// Click event for delete button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteButton_Click(object sender, EventArgs e)
        {
            CustomersDDL.Items.Add(new ListItem("Add new customer"));

            if (!string.IsNullOrEmpty(CustomerName.Text) 
                && customers.Any(x => x.Name == CustomerName.Text))                
            {
                customers.RemoveAll(y => y.ID == Convert.ToInt32(IDLabel.Text));

                ErrorLabel.ForeColor = Color.Green;
                ErrorLabel.Text = "Customer with name "+ CustomerName.Text + " deleted succefully.";

                //show add button and hide update/ delete button
                AddButton.Visible = true;
                UpdateButton.Visible = false;
                DeleteButton.Visible = false;

                CustomerName.Enabled = true;
                HeaderCustomer.InnerText = "Add customer";

                PopulateCustomerListBox();
                CustomersDDL.SelectedIndex = 0;

                //clearing form
                ClearForm();
            }
            else
            {
                ErrorLabel.ForeColor = Color.Red;
                ErrorLabel.Text = "Invalid delete action";
            }
        }

        protected void CustomersDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = CustomersDDL.SelectedValue;
            if (!string.IsNullOrEmpty(selectedValue))
            {
                DisplaySelectedCustomer(selectedValue);
            }

            //hide add button and display update/ delete button
            AddButton.Visible = false;
            UpdateButton.Visible = true;
            DeleteButton.Visible = true;
            
            HeaderCustomer.InnerText = "Update customer";
            ErrorLabel.Text = "";

            CustomerName.Enabled = false;

        }

        protected void Country_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = CountryDropDownList.SelectedValue;

            PopulateStateDropDownListsBasedOnCountry(selectedValue);

        }

        /// <summary>
        /// method to validate customer object properties
        /// </summary>
        /// <param name="inputCustomerObj"></param>
        /// <returns></returns>
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

            if (!string.IsNullOrEmpty(inputCustomerObj.Country) && inputCustomerObj.Country == "1" && !string.IsNullOrEmpty(inputCustomerObj.Zip) && !Regex.IsMatch(inputCustomerObj.Zip, usZipCodePattern))
			{
                errors.Add("Invalid US Zip Code");
            }

            if (!string.IsNullOrEmpty(inputCustomerObj.Country) && inputCustomerObj.Country == "0" && !string.IsNullOrEmpty(inputCustomerObj.Zip) && !Regex.IsMatch(inputCustomerObj.Zip, canadaZipCodePattern))
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

        /// <summary>
        /// method to display selected customer from dropdown list
        /// </summary>
        /// <param name="selectedValue"></param>
        private void DisplaySelectedCustomer(string selectedValue)
        {
            //get selected customer by name

			Customer selectedCustomer = customers.FirstOrDefault(x=> x.Name == selectedValue);

			//bind selected customer values to customer form

			if (selectedCustomer != null)
			{
                CustomerName.Text = selectedCustomer.Name;
                CustomerAddress.Text = selectedCustomer.Address;
                CustomerEmail.Text = selectedCustomer.Email;
                CustomerPhone.Text = selectedCustomer.Phone;
                CustomerCity.Text = selectedCustomer.City;
                StateDropDownList.Text = selectedCustomer.State;
                CustomerZip.Text = selectedCustomer.Zip;
                CountryDropDownList.Text = selectedCustomer.Country;
                CustomerNotes.Text = selectedCustomer.Notes;
                ContactName.Text = selectedCustomer.ContactName;
                ContactPhone.Text = selectedCustomer.ContactPhone;
                ContactEmail.Text = selectedCustomer.ContactEmail;
                IDLabel.Text = selectedCustomer.ID.ToString();
            }
        }

        /// <summary>
        /// method to clear of form data
        /// </summary>
        private void ClearForm()
        {
            // clreared all values from form

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
    }
}