var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(c =>
{
    c.IdleTimeout = TimeSpan.FromSeconds(60);    
   
});

var app = builder.Build();

app.UseSession();

app.MapGet("/SessionId", async (HttpContext httpContext) =>
{
    httpContext.Response.StatusCode = 200;
    httpContext.Response.ContentType = "text/html";
    await httpContext.Response.WriteAsync($" <h1> {httpContext.Session.Id} </h1> ");
});


app.MapGet("/Session", async (HttpContext httpContext) =>
{
    int num01 = 0;
    string num01Key = nameof(num01);

    num01 = httpContext.Session.GetInt32(num01Key) ?? 0;
    num01++;
    httpContext.Session.SetInt32(num01Key, num01);

    httpContext.Response.StatusCode = 200;
    httpContext.Response.ContentType = "text/html";
    await httpContext.Response.WriteAsync($" <h1> {num01} </h1> ");
});

app.MapGet("/", () => "Hello World!");

app.Run();
