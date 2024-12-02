using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        // Получить отдел по ID
        public async Task<DepartmentGetDto?> GetByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
                return null;

            return _mapper.Map<DepartmentGetDto>(department);
        }

        // Получить все отделы
        public async Task<IEnumerable<DepartmentGetDto>> GetAllAsync()
        {
            var departments = await _departmentRepository.GetAllAsync();
            return departments.Select(department => _mapper.Map<DepartmentGetDto>(department));
        }

        // Добавить новый отдел
        public async Task<DepartmentGetDto> AddAsync(DepartmentCreateDto dto)
        {
            var existingDepartment = await _departmentRepository.GetByNameAsync(dto.Name);
            if (existingDepartment != null)
            {
                // Если отдел с таким именем уже существует, возвращаем его
                return _mapper.Map<DepartmentGetDto>(existingDepartment);
            }

            var department = _mapper.Map<Department>(dto);
            await _departmentRepository.AddAsync(department);

            return _mapper.Map<DepartmentGetDto>(department);
        }

        // Обновить отдел
        public async Task<bool> UpdateAsync(int id, DepartmentUpdateDto dto)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);
            if (existingDepartment == null)
                return false;

            // Маппим DTO в сущность, сохраняя существующий объект
            _mapper.Map(dto, existingDepartment);
            await _departmentRepository.UpdateAsync(existingDepartment);
            return true;
        }

        // Удалить отдел
        public async Task<bool> DeleteAsync(int id)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);
            if (existingDepartment == null)
                return false;

            await _departmentRepository.DeleteAsync(existingDepartment);
            return true;
        }

        // Получить отдел по имени
        public async Task<DepartmentGetDto?> GetByNameAsync(string name)
        {
            var department = await _departmentRepository.GetByNameAsync(name);
            if (department == null)
                return null;

            return _mapper.Map<DepartmentGetDto>(department);
        }
    }
}
