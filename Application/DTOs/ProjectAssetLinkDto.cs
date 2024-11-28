namespace Application.DTOs;

public record ProjectAssetLinkDto(int ProjectId, int AssetId);
public record ProjectDepartmentLinkDto(int ProjectId, int DepartmentId);
public record ProjectRiskLinkDto(int ProjectId, int RiskId);