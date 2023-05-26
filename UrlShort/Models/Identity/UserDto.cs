using System.Text.Json.Serialization;

namespace UrlShort.Models.Identity;

public class UserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    [JsonConstructor]
    public UserDto(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }
}