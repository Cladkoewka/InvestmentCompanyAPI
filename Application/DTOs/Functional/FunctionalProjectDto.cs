namespace Application.DTOs.Functional;

public record FunctionalProjectDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Status { get; init; }
    public decimal Profit { get; init; }
    public decimal Cost { get; init; }
    public DateTime Deadline { get; init; }
    public string CustomerName { get; init; }
}