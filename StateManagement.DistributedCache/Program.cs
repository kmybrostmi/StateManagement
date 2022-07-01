
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedSqlServerCache(c =>
{
    c.SchemaName = "dbo";
    c.TableName = "DataCache";
    c.ConnectionString = @"Persist Security Info=False;User ID=sa;Initial Catalog=Cache ;Data Source=ROSTAMI-LP\ROSTAMISQL2017; Password=123; MultipleActiveResultSets=true; Encrypt=False";
});
var app = builder.Build();

app.MapGet("/cache", async (HttpContext httpContext, IDistributedCache cache) =>
{
    int num01 = 0;
    string num01Key = nameof(num01);

    num01 = int.Parse(cache.GetString(num01Key) ?? "0");
    num01++;

    cache.SetString(num01Key, num01.ToString(), new DistributedCacheEntryOptions
    {
        SlidingExpiration = TimeSpan.FromMinutes(1),
    });
    httpContext.Response.StatusCode = 200;
    httpContext.Response.ContentType = "text/html";
    await httpContext.Response.WriteAsync($" <h1> {num01} </h1> ");
});


app.MapGet("/", () => "Hello World!");

app.Run();
