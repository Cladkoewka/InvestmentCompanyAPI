using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // Получить всех клиентов
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerGetDto>>> GetAllAsync()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        // Получить клиента по ID
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerGetDto>> GetByIdAsync(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
                return NotFound(); // Возвращаем 404, если клиент не найден

            return Ok(customer);
        }

        // Добавить нового клиента
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<CustomerGetDto>> AddAsync([FromBody] CustomerCreateDto dto)
        {
            // Проверка валидации
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var customer = await _customerService.AddAsync(dto);
            return StatusCode(201, customer);
        }

        // Обновить клиента
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAsync(int id, [FromBody] CustomerUpdateDto dto)
        {
            // Проверка валидации
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var isUpdated = await _customerService.UpdateAsync(id, dto);
            if (!isUpdated)
                return NotFound(); // Возвращаем 404, если клиент не найден

            return NoContent(); // Возвращаем статус 204 (без контента) при успешном обновлении
        }

        // Удалить клиента
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var isDeleted = await _customerService.DeleteAsync(id);
            if (!isDeleted)
                return NotFound(); // Возвращаем 404, если клиент не найден

            return NoContent(); // Возвращаем статус 204 при успешном удалении
        }

        // Получить клиента по имени
        [Authorize(Roles = "Admin,Viewer")]
        [HttpGet("by-name/{name}")]
        public async Task<ActionResult<CustomerGetDto>> GetByNameAsync(string name)
        {
            var customer = await _customerService.GetByNameAsync(name);
            if (customer == null)
                return NotFound(); // Возвращаем 404, если клиент с таким именем не найден

            return Ok(customer);
        }
    }
}
