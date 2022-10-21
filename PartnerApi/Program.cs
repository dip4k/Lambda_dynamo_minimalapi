using Amazon;
using Amazon.DynamoDBv2;

using PartnerApi.Repositories;
using PartnerApi.Sevices;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

var newRegion = RegionEndpoint.GetBySystemName(config.GetValue<string>("AWS:Region"));
builder.Services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(newRegion));

builder.Services.AddSingleton<IPartnerRepository>(provider =>
    new PartnerRepository(provider.GetRequiredService<IAmazonDynamoDB>(),
        config.GetValue<string>("Database:PartnerTableName")));
builder.Services.AddSingleton<IAppservice, AppService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
//app.UseAuthorization();
app.UseCors("corsapp");

app.MapControllers();

//app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

app.Run();
