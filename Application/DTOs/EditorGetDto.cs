namespace Application.DTOs;

public record EditorGetDto(int Id, string FullName, string Email, string PhoneNumber);
public record EditorCreateDto(string FullName, string Email, string PhoneNumber);
public record EditorUpdateDto(string FullName, string Email, string PhoneNumber);