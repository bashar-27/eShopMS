var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddLogging();
builder.Services.AddCarter();
builder.Services.AddMediatR(config => { config.RegisterServicesFromAssemblies(typeof(Program).Assembly); } );
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
   // options.AutoCreateSchemaObjects = AutoCreate.All;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
