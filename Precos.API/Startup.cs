using Cadastros.API.Mappers;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Vendas.Infrastructure;
using Vendas.Infrastructure.Repositories;

namespace Precos.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            AddServices(services);
            ConfigureDatabase(services);
            ConfigureEventBus(services);

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Precos.API", Version = "v1" });
            });
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IPoliticaPrecoRepository, PoliticaPrecoRepository>();

            services.AddAutoMapper(expression => { expression.AllowNullCollections = true; }, typeof(MapperProfile));
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDbContext<VendasDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SqlConnection")));
        }

        private void ConfigureEventBus(IServiceCollection services)
        {
            services.AddMassTransit(bus =>
            {
                bus.UsingRabbitMq((ctx, busConfigurator) =>
                {
                    busConfigurator.Host(Configuration.GetConnectionString("RabbitMq"));
                });
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Precos.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
