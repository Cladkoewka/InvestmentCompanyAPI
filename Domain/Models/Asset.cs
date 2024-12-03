namespace Domain.Models;

// Investment instrument (dollars, gold etc)
public class Asset
{
    public int Id { get; set; }
    public required string Name { get; set; }
}