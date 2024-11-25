
using BlogApi.Data.Contexts;
using BlogApi.Helper;
using BlogApi.Repository.Interaces;
using BlogApi.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlogApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var connectionString = builder.Configuration.GetConnectionString("Defult");
            builder.Services.AddDbContext<BlogDbContext>(op =>
            {
                op.UseSqlServer(connectionString);
            });

            builder.Services.AddScoped<IBlogRepository, BlogRepository>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                //var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
                var context = serviceProvider.GetRequiredService<BlogDbContext>();
                SeedingData.SeedData(context);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
