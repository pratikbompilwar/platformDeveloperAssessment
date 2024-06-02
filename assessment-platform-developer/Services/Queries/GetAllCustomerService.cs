using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;
using System.Collections.Generic;

public class GetAllCustomerService : IGetAllCustomerService
{
    private readonly ICustomerQueryRepository customerQueryRepository;

    public GetAllCustomerService(ICustomerQueryRepository customerQueryRepository)
    {
        this.customerQueryRepository = customerQueryRepository;
    }

    /// <summary>
    /// method to get all customer of list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Customer> GetAllCustomers()
    {
        return customerQueryRepository.GetAll();
    }

}