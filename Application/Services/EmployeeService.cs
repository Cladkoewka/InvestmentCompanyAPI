using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeGetDto>> GetAllAsync()
        {
            var employees = await _employeeRepository.GetAllAsync();
            return employees.Select(employee => _mapper.Map<EmployeeGetDto>(employee));
        }

        public async Task<EmployeeGetDto?> GetByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
                return null;

            return _mapper.Map<EmployeeGetDto>(employee);
        }

        public async Task<IEnumerable<EmployeeGetDto>> GetByDepartmentIdAsync(int departmentId)
        {
            var employees = await _employeeRepository.GetByDepartmentIdAsync(departmentId);
            return employees.Select(employee => _mapper.Map<EmployeeGetDto>(employee));
        }

        public async Task<EmployeeGetDto> AddAsync(EmployeeCreateDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _employeeRepository.AddAsync(employee);

            return _mapper.Map<EmployeeGetDto>(employee);
        }

        public async Task<bool> UpdateAsync(int id, EmployeeUpdateDto dto)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
                return false;

            _mapper.Map(dto, existingEmployee);
            await _employeeRepository.UpdateAsync(existingEmployee);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existingEmployee = await _employeeRepository.GetByIdAsync(id);
            if (existingEmployee == null)
                return false;

            await _employeeRepository.DeleteAsync(existingEmployee);
            return true;
        }
    }
}
