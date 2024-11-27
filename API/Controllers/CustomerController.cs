using Microsoft.AspNetCore.Mvc;
using Domain.Interfaces;
using Domain.Models;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> TestCustomerRepositoryMethods()
        {
            // Тестирование GetByIdAsync
            var customerById = await _customerRepository.GetByIdAsync(1);
            var result = customerById is not null 
                ? $"Customer by ID: {customerById.Name}" 
                : "Customer not found";
            result += "<br>------------<br>";
            
            // Тестирование GetAllAsync
            var allCustomers = await _customerRepository.GetAllAsync();
            result += "<br>All customers:<br>";
            foreach (var customer in allCustomers)
            {
                result += $"Customer: {customer.Name}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование AddAsync
            var newCustomer = new Customer { Name = "New Customer" };
            await _customerRepository.AddAsync(newCustomer);
            result += "Added new customer<br>";
            
            allCustomers = await _customerRepository.GetAllAsync();
            result += "<br>All customers:<br>";
            foreach (var customer in allCustomers)
            {
                result += $"Customer: {customer.Name}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование UpdateAsync
            var customerToUpdate = new Customer { Id = 1, Name = "Updated Customer" };
            await _customerRepository.UpdateAsync(customerToUpdate);
            result += "Updated customer<br>";
            
            allCustomers = await _customerRepository.GetAllAsync();
            result += "<br>All customers:<br>";
            foreach (var customer in allCustomers)
            {
                result += $"Customer: {customer.Name}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование DeleteAsync
            await _customerRepository.DeleteAsync(customerToUpdate);
            result += "Deleted customer<br>";
            
            allCustomers = await _customerRepository.GetAllAsync();
            result += "<br>All customers:<br>";
            foreach (var customer in allCustomers)
            {
                result += $"Customer: {customer.Name}<br>";
            }
            result += "<br>------------<br>";

            // Тестирование GetByNameAsync
            var customerByName = await _customerRepository.GetByNameAsync("New Customer");
            result += "Found customer by name:<br>";
            if (customerByName != null)
            {
                result += $"Found customer by name: {customerByName.Name}<br>";
            }
            else
            {
                result += "No customer found by name.<br>";
            }

            return Content(result, "text/html");
        }
    }
}
