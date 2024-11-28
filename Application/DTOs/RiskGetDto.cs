namespace Application.DTOs;

public record RiskGetDto(int Id, string Type, int Grade);
public record RiskCreateDto(string Type, int Grade);
public record RiskUpdateDto(string Type, int Grade);