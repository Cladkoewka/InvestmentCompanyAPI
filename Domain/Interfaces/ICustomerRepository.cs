using Domain.Models;

namespace Domain.Interfaces;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByNameAsync(string name);
}