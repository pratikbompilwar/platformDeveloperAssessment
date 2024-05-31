using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using System.Collections.Generic;
using System.Linq;

public class CustomerCommandRepository : ICustomerCommandRepository
{
    // Assuming you have a DbContext named 'context'
    private readonly List<Customer> customers = new List<Customer>();   

    public void Add(Customer customer)
    {
        customers.Add(customer);
    }

    public void Update(Customer customer)
    {
        var existingCustomer = customers.FirstOrDefault(c => c.ID == customer.ID);
        if (existingCustomer != null)
        {
            // Update properties of existingCustomer based on the properties of customer
            // For example:
            existingCustomer.Name = customer.Name;
            // Repeat for other properties
        }
    }

    public void Delete(int id)
    {
        var customer = customers.FirstOrDefault(c => c.ID == id);
        if (customer != null)
        {
            customers.Remove(customer);
        }
    }
}