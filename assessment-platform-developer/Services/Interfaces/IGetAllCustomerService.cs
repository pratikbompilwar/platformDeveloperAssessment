using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_platform_developer.Services.Interfaces
{
    public interface IGetAllCustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
    }
}
