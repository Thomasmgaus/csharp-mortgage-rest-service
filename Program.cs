using mortgage_application.Model;
using mortgage_application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MortgageDatabaseSettings>(
    builder.Configuration.GetSection("MortgageDatabase"));

builder.Services.AddSingleton<ApplicantService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
