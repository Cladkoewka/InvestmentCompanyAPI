namespace Application.DTOs;

public record DepartmentGetDto(int Id, string Name);
public record DepartmentCreateDto(string Name);
public record DepartmentUpdateDto(string Name);