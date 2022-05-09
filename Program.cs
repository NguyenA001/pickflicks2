using Microsoft.EntityFrameworkCore;
using pickflicks2.Services;
using pickflicks2.Services.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<MWGService>();
builder.Services.AddScoped<GenreRankingService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<MWGMatchService>();
builder.Services.AddScoped<MWGStatusService>();
builder.Services.AddScoped<InvitationService>();

var connectionString = builder.Configuration.GetConnectionString("MyConnectionString");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddCors(options => {
    options.AddPolicy("MyPolicy",
    builder => {
        builder.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:3002", "http://[2601:205:4181:230::a937]:5238")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

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

// app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
