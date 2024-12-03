namespace Domain.Models.Auth;

public class User
{
    public int Id { get; set; }                 
    public required string FullName { get; set; }         
    public required string Email { get; set; }             
    public required string PasswordHash { get; set; }      
    public int RoleId { get; set; }               
    public Role? Role { get; set; }                
}