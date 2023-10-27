using server_Jwt2.Repository_s;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<clientDatabaseSettings>(
    builder.Configuration.GetSection("clientDatabase"));
builder.Services.AddSingleton<clientService>();
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
