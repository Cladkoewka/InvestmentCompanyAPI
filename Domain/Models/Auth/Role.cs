namespace Domain.Models.Auth;

public class Role
{
    public int Id { get; set; }          // Идентификатор роли
    public string Name { get; set; }     // Название роли (например, "Admin", "User")
}