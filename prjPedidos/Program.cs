using Microsoft.EntityFrameworkCore;
using prjPedidos.Data;
using prjPedidos.Repositories;
using prjPedidos.Repositories.Interfaces;

namespace prjPedidos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Banco de dados
            builder.Services.AddEntityFrameworkSqlServer().AddDbContext<SistemaPedidosDB>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase"))
                );


            // Repositórios
            builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
            builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
            builder.Services.AddScoped<IItensPedidoRepository, ItemRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                    c.RoutePrefix = string.Empty; 
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
