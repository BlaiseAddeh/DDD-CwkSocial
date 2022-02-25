

using CwkSocial.Api.Options;
using CwkSocial.Application.UserProfiles.Queries;
using CwkSocial.DAL;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//------------------ Configuration pour le DbContext -------------
var cs = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(cs));

//--------------- Configuration de AutoMapper et de Mediator--------------------

builder.Services.AddAutoMapper(typeof(Program), typeof(GetAllUserProfiles));
builder.Services.AddMediatR(typeof(GetAllUserProfiles));


//--------------- Gestion de la version de l'API -----------------

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddVersionedApiExplorer(config =>
{
    config.GroupNameFormat = "'v'VVV";
    config.SubstituteApiVersionInUrl = true;
});

//--------------- Fin de Gestion de la version -----------------


builder.Services.AddSwaggerGen();    //NB: Position a remarquer

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.ApiVersion.ToString());
        }
    });
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

