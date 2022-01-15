using GaragePi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Configuration.AddJsonFile("doorconfig.json");
var doorConfigList = new DoorConfigList();
builder.Configuration.GetSection("DoorConfiguration").Bind(doorConfigList);
var parms = new ServiceParameters();
builder.Configuration.GetSection("ServiceParameters").Bind(parms);

var doorService = new GarageDoorService(parms, doorConfigList);
builder.Services.AddSingleton(doorService);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

