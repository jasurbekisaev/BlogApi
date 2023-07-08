using AutoMapper;
using BlogApi.Context;
using BlogApi.Extensions;
using BlogApi.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = "JWT Bearer. : \"Authorization: Bearer { token } \"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<BlogdbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("IdentityDb"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddScoped<BlogManager>();
builder.Services.AddScoped<PostManager>();
builder.Services.AddScoped<CommentManager>();
builder.Services.AddScoped<SavedPostManager>();
builder.Services.AddScoped<IMapper, Mapper>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseMigrateBlogDb();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
