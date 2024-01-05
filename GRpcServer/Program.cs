using GRpcServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
//grpc listen port 50051

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<MainService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
