using Application.DTOs;

namespace Application.Interfaces;

public interface ICustomerService : IService<CustomerGetDto, CustomerCreateDto, CustomerUpdateDto>
{
    Task<CustomerGetDto?> GetByNameAsync(string name);
}