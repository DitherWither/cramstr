using AspNetCore.Proxy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProxies();
// builder.Services.AddAuthorization();
builder.Services.AddControllers();


var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.RunProxy(proxy => proxy
    .UseHttp((context, args) =>
    {
        if(context.Request.Path.StartsWithSegments("/api"))
            return "http://localhost:5132";
        return "http://localhost:5107";
    }));

app.Run();