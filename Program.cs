using System;
using SocialSite.Models;
using SocialSite.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace SocialSite
{
    internal class Program
    {
        static void Main(string[] args)
        {

            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            IConfigurationRoot configuration = configBuilder.AddJsonFile("appsettings.json").Build();

            DataContextEF dataContextEF = new DataContextEF(configuration);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors((options) =>
            {
                options.AddPolicy("AllowAll", (policy) =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHttpsRedirection();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}

