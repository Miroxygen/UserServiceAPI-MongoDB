namespace App.Models;

public class UserDto
{
    public string? Name {get; set;}
    public string? Username {get; set;}
    public int UserId { get; set; } 
    public int Followers { get; set; } 
    public List<int>? Following { get; set; } 
}
