using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;

public class AddCustomerService : IAddCustomerService
{
    private readonly ICustomerCommandRepository customerCommandRepository;

    public AddCustomerService(ICustomerCommandRepository customerCommandRepository)
    {
        this.customerCommandRepository = customerCommandRepository;
    }

    /// <summary>
    /// method to add customer into customer list
    /// </summary>
    /// <param name="customer"></param>
    public void AddCustomer(Customer customer)
    {
        customerCommandRepository.Add(customer);
    }


}