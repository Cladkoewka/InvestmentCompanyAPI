namespace Domain.Models;

public class Employee
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    
    // Foreign Key
    public int DepartmentId { get; set; }
}