
using NLog;
using UserService.Contracts;
using UserService.Extensions;
using AssemblyReference = UserService.Presentation.AssemblyReference;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureDependencies();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference).Assembly);
builder.Services.AddHttpClient();

var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>(); 
app.ConfigureExceptionHandler(logger); 
 
if (app.Environment.IsProduction()) 
    app.UseHsts(); 

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();