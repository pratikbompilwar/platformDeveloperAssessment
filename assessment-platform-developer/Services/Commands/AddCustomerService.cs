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

    public void AddCustomer(Customer customer)
    {
        customerCommandRepository.Add(customer);
    }


}