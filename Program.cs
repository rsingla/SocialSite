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


            Computer computer = new Computer()
            {
                Motherboard = "Yohaan",
                CPUCores = 4,
                HasWifi = false,
                HasLTE = false,
                ReleaseDate = new DateTime(2014, 01, 22),
                Price = 2000,
                VideoCard = ""
            };

            dataContextEF.Add(computer);
            dataContextEF.SaveChanges();

            string sql = @"INSERT INTO TutorialAppSchema.Computer (
                Motherboard, 
                CPUCores, 
                HasWifi, 
                HasLTE, 
                ReleaseDate, 
                Price, 
                VideoCard) 
                            VALUES ('" +
                            computer.Motherboard + "', " +
                            computer.CPUCores + ", '" +
                            computer.HasWifi + "', '" +
                            computer.HasLTE + "', '" +
                            computer.ReleaseDate + "', " +
                            computer.Price + ", '" +
                            computer.VideoCard + "')";

            Console.WriteLine(sql);


            File.WriteAllText("log.txt", sql);

        }
    }
}

