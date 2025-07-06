// *********************************************************************************
//	<copyright file="Program.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Program Class.</summary>
// *********************************************************************************


using Azure.Identity;
using Microsoft.OpenApi.Models;
using NotesTracker.API.Middleware;
using NotesTracker.Business.Helpers;
using static NotesTracker.Shared.Constants.ConfigurationConstants;

#region Configure Services

var builder = WebApplication.CreateBuilder();
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(path: LocalAppsettingsFileConstant, optional: true).AddEnvironmentVariables();

var credentials = builder.Environment.IsDevelopment()
	? new DefaultAzureCredential()
	: new DefaultAzureCredential(new DefaultAzureCredentialOptions
	{
		ManagedIdentityClientId = builder.Configuration[ManagedIdentityClientIdConstant],
	});

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		p =>
		{
			p.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader();
		});
});

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc(ApiVersion, new OpenApiInfo
	{
		Title = NotesTrackerApiName,
		Version = ApiVersion,
		Description = "API documentation for Notes Tracker",
		Contact = new OpenApiContact
		{
			Name = "Debanjan Paul",
			Email = "debanjanpaul10@gmail.com"
		}

	});
});

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddHttpContextAccessor();

builder.ConfigureAzureAppConfiguration(credentials);
builder.ConfigureApiServices();

#endregion

#region Configure Application

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint(SwaggerEndpoint, $"{NotesTrackerApiName}.{ApiVersion}");
	c.RoutePrefix = SwaggerUiPrefix;
});

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();

#endregion


