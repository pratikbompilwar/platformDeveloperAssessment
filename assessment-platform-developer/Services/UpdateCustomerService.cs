using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;

public class UpdateCustomerService : IUpdateCustomerService
{
    private readonly ICustomerRepository customerRepository;

    public UpdateCustomerService(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }



    public void UpdateCustomer(Customer customer)
    {
        customerRepository.Update(customer);
    }

}