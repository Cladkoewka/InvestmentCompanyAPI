namespace Domain.Models.Auth;

public class User
{
    public int Id { get; set; }                  // Идентификатор пользователя
    public string FullName { get; set; }          // Полное имя пользователя
    public string Email { get; set; }             // Электронная почта пользователя
    public string PasswordHash { get; set; }      // Хэш пароля (не сам пароль)
    public int RoleId { get; set; }               // Идентификатор роли пользователя
    public Role Role { get; set; }                // Роль пользователя (связь с моделью Role)
}