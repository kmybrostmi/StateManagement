using Microsoft.Extensions.Caching.Memory;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMemoryCache();
var app = builder.Build();

app.MapGet("/cache", async (HttpContext httpContext , IMemoryCache cache  ) =>
{
    int num01 = 0;
    string num01Key = nameof(num01);

    num01 = cache.Get<int>(num01Key);
    num01++;
    cache.Set(num01Key, num01, new MemoryCacheEntryOptions
    {
        //AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(30),
        SlidingExpiration = TimeSpan.FromMinutes(1)
    });
    httpContext.Response.StatusCode = 200;
    httpContext.Response.ContentType = "text/html";
    await httpContext.Response.WriteAsync($" <h1> {num01} </h1> ");
});


app.MapGet("/", () => "Hello World!");

app.Run();
