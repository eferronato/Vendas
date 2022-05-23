using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notificacoes.Worker.Consumers;

namespace Notificacoes.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //Registra a comunicação com o RabbitMQ
                    services.AddMassTransit(x =>
                    {
                        x.AddConsumer<PrecoChangedConsumer>();

                        x.UsingRabbitMq((ctx, cfg) =>
                        {
                            cfg.Host(hostContext.Configuration.GetConnectionString("RabbitMq"));
                            cfg.ConfigureEndpoints(ctx);
                        });
                    });

                    services.AddHostedService<Worker>();
                });
    }
}
