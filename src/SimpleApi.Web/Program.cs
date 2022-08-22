using MediatR;
using SimpleApi.Model;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(typeof(TaskEntity).Assembly);
var app = builder.Build();

app.MapControllers();
app.Run();