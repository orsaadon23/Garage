var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
builder.Services.AddSingleton(new DatabaseService(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

var app = builder.Build();

app.RegisterGarageEndpoints();


app.Run();
