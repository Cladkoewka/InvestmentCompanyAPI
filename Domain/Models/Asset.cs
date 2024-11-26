namespace Domain.Models;

// Инвестиционный инструмент (валюта, драгметаллы, акции и т.д.)
public class Asset
{
    public int Id { get; set; }
    public required string Name { get; set; }
}