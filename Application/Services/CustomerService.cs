using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerGetDto?> GetByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
                return null;

            return _mapper.Map<CustomerGetDto>(customer);
        }

        public async Task<IEnumerable<CustomerGetDto>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            return customers.Select(c => _mapper.Map<CustomerGetDto>(c));
        }

        public async Task<CustomerGetDto> AddAsync(CustomerCreateDto dto)
        {
            var existingCustomer = await _customerRepository.GetByNameAsync(dto.Name);
            if (existingCustomer != null)
            {
                return _mapper.Map<CustomerGetDto>(existingCustomer);
            }

            var customer = _mapper.Map<Customer>(dto);
            await _customerRepository.AddAsync(customer);

            return _mapper.Map<CustomerGetDto>(customer);
        }

        public async Task<bool> UpdateAsync(int id, CustomerUpdateDto dto)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
                return false;

            _mapper.Map(dto, existingCustomer);
            await _customerRepository.UpdateAsync(existingCustomer);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id);
            if (existingCustomer == null)
                return false;

            await _customerRepository.DeleteAsync(existingCustomer);
            return true;
        }

        public async Task<CustomerGetDto?> GetByNameAsync(string name)
        {
            var customer = await _customerRepository.GetByNameAsync(name);
            return _mapper.Map<CustomerGetDto>(customer);
        }
    }
}
