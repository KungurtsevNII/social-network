namespace Web.Api.Models;

public sealed class Profile
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string? Interest { get; set; }
    public string City { get; set; }
}