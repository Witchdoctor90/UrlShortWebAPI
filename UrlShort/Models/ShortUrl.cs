namespace UrlShort.Models;

public class ShortUrl
{
    public int Id { get; set; }
    public string Url { get; set; } = "";
    public string ShortenedUrl { get; set; } = "";
    public DateTime CreationDate = DateTime.Now;
}