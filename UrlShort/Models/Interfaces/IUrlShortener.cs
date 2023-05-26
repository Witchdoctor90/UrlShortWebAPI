namespace UrlShort.Models;

public interface IUrlShortener
{
    private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@abcdefghijklmnopqrstuvwxyz";
    
    public bool IsValid(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var createdUri);
    }

    public string GenerateUrl(string url)
    {
        var random = new Random();
        
        var result = new string(Enumerable.Repeat(Chars, 8)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());
        return result;
    }
    
    

}