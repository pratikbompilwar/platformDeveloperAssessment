using assessment_platform_developer.Repositories;
using assessment_platform_developer.Services.Interfaces;

public class DeleteCustomerService : IDeleteCustomerService
{
    private readonly ICustomerCommandRepository customerCommandRepository;

    public DeleteCustomerService(ICustomerCommandRepository customerCommandRepository)
    {
        this.customerCommandRepository = customerCommandRepository;
    }


    /// <summary>
    /// method to delete customer from the list
    /// </summary>
    /// <param name="id"></param>
    public void DeleteCustomer(int id)
    {
        customerCommandRepository.Delete(id);
    }
}