namespace Application.DTOs;

public record EmployeeGetDto(int Id, string FirstName, string LastName, int DepartmentId);
public record EmployeeCreateDto(string FirstName, string LastName, int DepartmentId);
public record EmployeeUpdateDto(string FirstName, string LastName, int DepartmentId);