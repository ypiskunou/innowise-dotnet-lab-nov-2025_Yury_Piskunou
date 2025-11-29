
using ProductService.Extensions;
using ProductService.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureDependencies();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference).Assembly);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()  
            .AllowAnyMethod()   
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination");
    });
});

var app = builder.Build();

app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();