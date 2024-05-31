using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;
using System.Collections.Generic;

public class GetAllCustomerService : IGetAllCustomerService
{
    private readonly ICustomerRepository customerRepository;

    public GetAllCustomerService(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        return customerRepository.GetAll();
    }

}