var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/cookies", async (HttpContext httpContext) =>
{
    int num01 = 0;
    int num02 = 0;
    string num01Key = nameof(num01);
    string num02Key = nameof(num02);
    num01 = int.Parse(httpContext.Request.Cookies["Num01"] ?? "0");
    num02 = int.Parse(httpContext.Request.Cookies["Num02"] ?? "0");
    num01++;
    num02++;

    httpContext.Response.Cookies.Append(num01Key, num01.ToString());
    httpContext.Response.Cookies.Append(num02Key, num02.ToString(), new CookieOptions
    {
        Expires = DateTimeOffset.Now.AddSeconds(10),
        Secure = true,
        IsEssential = true,
        Path = "/cookies",
    });
    httpContext.Response.ContentType="text/html";   
    await httpContext.Response.WriteAsync($" <h1> {num01} -- {num02} </h1> ");
});

app.MapGet("/clear", async (HttpContext httpContext) =>
{
    httpContext.Response.Cookies.Delete("num01");
    httpContext.Response.Cookies.Delete("num02");
});


app.MapGet("/", () => "Hello World!");

app.Run();
