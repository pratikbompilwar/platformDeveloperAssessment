using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using System.Collections.Generic;
using System.Linq;

public class CustomerQueryRepository : ICustomerQueryRepository
{
    // Assuming you have a DbContext named 'context'
    private readonly List<Customer> customers = new List<Customer>();

    public IEnumerable<Customer> GetAll()
    {
        return customers;
    }

    public Customer Get(int id)
    {
        return customers.FirstOrDefault(c => c.ID == id);
    }
}