using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShort.Models;
using UrlShort.Models.DB;

namespace UrlShort.Controllers;

[ApiController]
public class ShorturlController : Controller
{
    private readonly IUrlShortener _urlShortener;
    private readonly AppDbContext _db;

    public ShorturlController(IUrlShortener urlShortener, AppDbContext db)
    {
        _urlShortener = urlShortener;
        _db = db;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]/GetForUser")]
    public IActionResult GetForUser()
    {
        var result = _db.Urls.Where(u => u.AuthorName == HttpContext.User.Identity.Name);
        return Ok(result.ToList());
    }

    
    [HttpGet]
    [Route("[controller]/GetAll")]
    public IActionResult GetAll()
    {
        var result = _db.Urls.ToList();
        return Ok(result);
    }
    
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("[controller]/shorten")]
    public async Task<string> Shorten([FromBody]string url)
    {
        if (!_urlShortener.IsValid(url)) return "Invalid URL!";
        var path = GenerateUniqueUrlPath(url);

        _db.Urls.Add(new ShortUrl()
        {
            ShortenedUrl = path,
            Url = url,
            AuthorName = HttpContext.User.Identity.Name
        });
        
        await _db.SaveChangesAsync();
        
        var result = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{path}";
        return result;
    }
    
    [NonAction]
    public string GenerateUniqueUrlPath(string url)
    {
        var path = _urlShortener.GenerateUrl(url);
        if (_db.Urls.Any(u => u.ShortenedUrl == url)) 
            path = GenerateUniqueUrlPath(url);
        return path;
    }
}