namespace Application.DTOs;

public record ProjectAssetLinkDto(int projectId, int assetId);
public record ProjectDepartmentLinkDto(int projectId, int departmentId);
public record ProjectRiskLinkDto(int projectId, int riskId);