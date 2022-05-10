using System.Globalization;
using Discounts.Api.Services;
using Discounts.Application;
using Discounts.Domain;
using Discounts.Infrastructure;

// Enable W3C Trace Context support for distributed tracing
System.Diagnostics.Activity.DefaultIdFormat = System.Diagnostics.ActivityIdFormat.W3C;

// Allow grpc to operate in a non-TLS environment
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

// Format dates and currencies in AU format
CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-AU");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services
    .AddDiscountsApplication()
    .AddDiscountsDomain()
    .AddDiscountsInfrastructure(builder.Configuration);
var app = builder.Build();
app.MapGrpcService<GrpcDiscountsService>();
app.MapGet("/", () => "Discount Service");

app.Run();
