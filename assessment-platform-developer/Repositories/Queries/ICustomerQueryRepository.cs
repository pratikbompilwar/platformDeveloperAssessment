﻿using assessment_platform_developer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assessment_platform_developer.Repositories
{
    /// <summary>
    /// customer query repository for repository
    /// </summary>
    public interface ICustomerQueryRepository
    {
        IEnumerable<Customer> GetAll();
        Customer Get(int id);
    }
}
