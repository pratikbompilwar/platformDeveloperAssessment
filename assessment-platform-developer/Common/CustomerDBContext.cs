using assessment_platform_developer.Models;
using System.Data.Entity;

public class CustomerDBContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
}