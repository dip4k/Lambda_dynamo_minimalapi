using Amazon.DynamoDBv2;
using Amazon;
using CompanyDetailMinimalApi.Repositories;
using CompanyDetailMinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

// dynamo db configs
//var awsOptions = config.GetAWSOptions();
//builder.Services.AddDefaultAWSOptions(awsOptions);
//builder.Services.AddAWSService<IAmazonDynamoDB>();
//builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
var newRegion = RegionEndpoint.GetBySystemName(config.GetValue<string>("AWS:Region"));

builder.Services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(newRegion));
builder.Services.AddSingleton<ICompanyDetailRepository>(provider =>
    new CompanyDetailRepository(provider.GetRequiredService<IAmazonDynamoDB>(),
        config.GetValue<string>("Database:CompanyTableName")));
builder.Services.AddSingleton<IContactRepository>(provider =>
    new ContactRepository(provider.GetRequiredService<IAmazonDynamoDB>(),
        config.GetValue<string>("Database:ContactTableName")));
builder.Services.AddSingleton<IAppService, AppService>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

//app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

app.Run();
