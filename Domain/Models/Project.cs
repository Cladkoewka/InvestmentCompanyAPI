namespace Domain.Models;

// Инвестиционный проект
public class Project
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Status { get; set; }
    public decimal Profit { get; set; }
    public decimal Cost { get; set; }
    public DateTime Deadline { get; set; }
    
    // Foreign Key
    public int CustomerId { get; set; }
    public int EditorId { get; set; }

}