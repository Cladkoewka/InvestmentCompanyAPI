namespace Application.DTOs;

public record CustomerGetDto(int Id, string Name);
public record CustomerCreateDto(string Name);
public record CustomerUpdateDto(string Name);