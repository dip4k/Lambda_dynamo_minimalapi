using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers();

// dynamo db configs
//var awsOptions = config.GetAWSOptions();
//builder.Services.AddDefaultAWSOptions(awsOptions);
//builder.Services.AddAWSService<IAmazonDynamoDB>();
//builder.Services.AddScoped<IDynamoDBContext, DynamoDBContext>();
var newRegion = RegionEndpoint.GetBySystemName(config.GetValue<string>("AWS:Region"));

builder.Services.AddSingleton<IAmazonDynamoDB>(_ => new AmazonDynamoDBClient(newRegion));
//builder.Services.AddSingleton<ICustomerRepository>(provider =>
//    new CustomerRepository(provider.GetRequiredService<IAmazonDynamoDB>(),
//        config.GetValue<string>("Database:TableName")));



// Add AWS Lambda support. When application is run in Lambda Kestrel is swapped out as the web server with Amazon.Lambda.AspNetCoreServer. This
// package will act as the webserver translating request and responses between the Lambda event source and ASP.NET Core.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

var app = builder.Build();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Welcome to running ASP.NET Core Minimal API on AWS Lambda");

app.Run();
