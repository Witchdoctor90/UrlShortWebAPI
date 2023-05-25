using Microsoft.EntityFrameworkCore;
using UrlShort.Models;
using UrlShort.Models.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration
        .GetConnectionString("DefaultConnection")
    ));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/shorturl", async (string url, AppDbContext db, HttpContext ctx) =>
{
    //validating URL
    if (!Uri.TryCreate(url, UriKind.Absolute, out var createdUri))
        return Results.BadRequest("Invalid URL!");
    
    //creating ShortURL
    var random = new Random();
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@abcdefghijklmnopqrstuvwxyz";

    var randomString = new string(Enumerable.Repeat(chars, 8)
        .Select(s => s[random.Next(s.Length)])
        .ToArray());

    //mapping
    var sUrl = new ShortUrl()
    {
        Url = url,
        ShortenedUrl = randomString
    };

    //saving to db
    db.Urls.Add(sUrl);
    await db.SaveChangesAsync();

    //construct url
    var result = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{sUrl.ShortenedUrl}";
    return Results.Ok(result);
});

app.MapFallback(async (AppDbContext db, HttpContext ctx) =>
{
    var path = ctx.Request.Path.ToUriComponent().Trim('/');
    var urlMatch = await db.Urls
        .FirstOrDefaultAsync(u => u.ShortenedUrl.Trim() == path.Trim());

    return urlMatch == null ? Results.NotFound() : Results.Redirect(urlMatch.Url);
});

app.Run();