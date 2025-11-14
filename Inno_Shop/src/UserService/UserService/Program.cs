
using UserService.Extensions;
using AssemblyReference = UserService.Presentation.AssemblyReference;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddDbContext<RepositoryContext>(
//     opts => opts.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
// );
// builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();
// builder.Services.AddScoped<IServiceManager, ServiceManager>();
// builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureDependencies();
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference).Assembly);

var app = builder.Build();
app.ConfigureExceptionHandler();

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