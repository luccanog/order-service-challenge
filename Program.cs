using Ambev.Services;
using Ambev.Storage;
using Microsoft.EntityFrameworkCore;
using Testcontainers.PostgreSql;

namespace Ambev
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 
            
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Default");
                opt.UseNpgsql(connectionString);
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}