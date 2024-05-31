using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;

public class GetCustomerService : IGetCustomerService
{
    private readonly ICustomerRepository customerRepository;

    public GetCustomerService(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public Customer GetCustomer(int id)
    {
        return customerRepository.Get(id);
    }
}