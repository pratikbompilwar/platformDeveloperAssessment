using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;

public class DeleteCustomerService : IDeleteCustomerService
{
    private readonly ICustomerRepository customerRepository;

    public DeleteCustomerService(ICustomerRepository customerRepository)
    {
        this.customerRepository = customerRepository;
    }


    public void DeleteCustomer(int id)
    {
        customerRepository.Delete(id);
    }
}