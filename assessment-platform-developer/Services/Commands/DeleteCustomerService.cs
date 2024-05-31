using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;

public class DeleteCustomerService : IDeleteCustomerService
{
    private readonly ICustomerCommandRepository customerCommandRepository;

    public DeleteCustomerService(ICustomerCommandRepository customerCommandRepository)
    {
        this.customerCommandRepository = customerCommandRepository;
    }


    public void DeleteCustomer(int id)
    {
        customerCommandRepository.Delete(id);
    }
}