using Microsoft.AspNetCore.Identity;

namespace UrlShort.Models;

public class ShortUrl
{
    public int Id { get; set; }
    public string Url { get; set; }
    public string ShortenedUrl { get; set; }
    public string AuthorName { get; set; }
    public DateTime CreationDate = DateTime.Now;

    public ShortUrl(int id, string url, string shortenedUrl, string authorName)
    {
        Id = id;
        Url = url;
        ShortenedUrl = shortenedUrl;
        AuthorName = authorName;
    }

    public ShortUrl()
    {
        
    }
}