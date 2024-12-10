namespace Application.DTOs.Functional;

public class ProjectDetailsDto
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string ProjectStatus { get; set; }
    public decimal ProjectProfit { get; set; }
    public decimal ProjectCost { get; set; }
    public DateTime ProjectDeadline { get; set; }
    public string CustomerName { get; set; }
    public string EditorName { get; set; }
    public string? AssetNames { get; set; }
    public string? RiskTypes { get; set; }
    public string? DepartmentNames { get; set; }
}
