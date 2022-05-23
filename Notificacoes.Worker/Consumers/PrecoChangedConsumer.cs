using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.EventBus.Models;

namespace Notificacoes.Worker.Consumers
{
    /// <summary>
    ///  Quando houver alteração de preços de um determinado produto já adquirido pelo cliente, devemos notificá-lo caso este esteja sendo vendido a
    ///  um preço inferior ao que ele comprou, de modo que o direcione a compra-lo novamente, aproveitando a promoção do mesmo. 
    /// </summary>
    public class PrecoChangedConsumer : IConsumer<PrecoChangedEvent>
    {
        private readonly ILogger<PrecoChangedConsumer> _logger;

        public PrecoChangedConsumer(ILogger<PrecoChangedConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<PrecoChangedEvent> context)
        {
            var message = context.Message;
            var novoPreco = message.Preco;

            _logger.LogInformation("Preço alterado. Cliente: {Cliente} | Produto: {Produto} | Preço: {Preco}. Verificando último preço... ",
                message.ClienteId, message.ProdutoId, novoPreco);

            var ultimoPreco = await GetUltimoPrecoByClienteProduto(message.ClienteId, message.ProdutoId);

            //Verifica se o produto já foi adquirido pelo cliente e se o novo preço é inferior ao que o cliente comprou
            if (ultimoPreco.HasValue && novoPreco < ultimoPreco)
            {
                //Aqui faria o envio do aviso ao cliente
                _logger.LogInformation("======> Preço alterado, notificar o Cliente: {Cliente}. PROMOÇÃO! O produto: {Produto} está mais barato! Preço: {Preco}.",
                    message.ClienteId, message.ProdutoId, novoPreco);
            }            
        }

        private static async Task<decimal?> GetUltimoPrecoByClienteProduto(int clienteId, int produtoId)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                var request = new System.Net.Http.HttpRequestMessage();
                //URL = Nome usado no docker-compose
                request.RequestUri = new Uri($"http://pedidos.api/api/v1/pedidos/GetUltimoPrecoByClienteProduto?clienteId={clienteId}&produtoId={produtoId}");
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return Convert.ToDecimal(await response.Content.ReadAsStringAsync());
                else
                    return null;
            }
        }
    }    
}
