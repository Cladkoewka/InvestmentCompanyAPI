namespace Application.DTOs;

public record AssetGetDto(int Id, string Name);
public record AssetCreateDto(string Name);
public record AssetUpdateDto(string Name);