using Azure.Identity;
using Azure.ResourceManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.Devices;
using Microsoft.IdentityModel.Tokens;
using SimGreenHearts.IoTDevicesAPI.Interfaces;
using SimGreenHearts.IoTDevicesAPI.Models;
using SimGreenHearts.IoTDevicesAPI.Services;
using System.Resources;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration["VaultUri"]!), new DefaultAzureCredential());
}

// Add services to the container.

var hubString = $"HostName={builder.Configuration["IOTHUB_HOSTNAME"]}.azure-devices.net;SharedAccessKeyName={builder.Configuration["IOTHUB_SAK_NAME"]};SharedAccessKey={builder.Configuration["IOTHUB_SAK"]}";
builder.Services.AddScoped(typeof(RegistryManager), rm => RegistryManager.CreateFromConnectionString(hubString));
builder.Services.AddScoped<IAzureIotHubClient, DevicesClient>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x => x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwtKey"]!)),
        ClockSkew = TimeSpan.Zero
    });
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
