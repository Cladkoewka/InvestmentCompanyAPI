namespace Domain.Models;

// Риски (банкротство, валютный риск и т.д.)
public class Risk
{
    public int Id { get; set; }
    public required string Type { get; set; }
    public int Grade { get; set; }
}