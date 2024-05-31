using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;

public class AddCustomerService : IAddCustomerService
{
    private readonly ICustomerRepository customerRepository;

    public AddCustomerService(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public void AddCustomer(Customer customer)
    {
        customerRepository.Add(customer);
    }


}