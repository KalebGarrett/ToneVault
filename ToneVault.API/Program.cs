using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using ToneVault.API.Repositories;
using ToneVault.API.Repositories.Interfaces;
using ToneVault.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMongoRepository<Tone>, ToneRepository>();
var conventionPack = new ConventionPack {new CamelCaseElementNameConvention()};
ConventionRegistry.Register("camelCase", conventionPack, t => true);

var mongoDbConnectionString = builder.Configuration["ConnectionString:MongoDb"];
builder.Services.AddScoped<IMongoClient, MongoClient>(_ => new MongoClient(MongoClientSettings.FromConnectionString(mongoDbConnectionString)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();