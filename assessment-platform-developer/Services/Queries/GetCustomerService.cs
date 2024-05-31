using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;

public class GetCustomerService : IGetCustomerService
{
    private readonly ICustomerQueryRepository customerQueryRepository;

    public GetCustomerService(ICustomerQueryRepository customerQueryRepository)
    {
        this.customerQueryRepository = customerQueryRepository;
    }

    public Customer GetCustomer(int id)
    {
        return customerQueryRepository.Get(id);
    }
}