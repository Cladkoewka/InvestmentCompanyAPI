namespace Domain.Models;

// Risk (default, valute etc)
public class Risk
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public int Grade { get; set; }
}