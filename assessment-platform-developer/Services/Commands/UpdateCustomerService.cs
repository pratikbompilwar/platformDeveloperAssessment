using assessment_platform_developer.Models;
using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;

public class UpdateCustomerService : IUpdateCustomerService
{
    private readonly ICustomerCommandRepository customerCommandRepository;

    public UpdateCustomerService(ICustomerCommandRepository customerCommandRepository)
    {
        this.customerCommandRepository = customerCommandRepository;
    }



    public void UpdateCustomer(Customer customer)
    {
        customerCommandRepository.Update(customer);
    }

}