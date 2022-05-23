using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vendas.Domain;
using Vendas.Infrastructure.Cross;
using Vendas.Infrastructure.Repositories;

namespace Relatorios.API.Controllers
{
    [Route("api/v1/relatorios")]
    [ApiController]
    public class RelatoriosController : ControllerBase
    {
        private readonly IPedidoRepository _repository;

        public RelatoriosController(IPedidoRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("VendasPorCliente")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PagedResponse<Pedido>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetVendasPorClienteAsync(
            [FromQuery] string cnpj,
            [FromQuery] string razaoSocial,
            [FromQuery] int pageSize = 50,
            [FromQuery] int pageIndex = 0)
        {
            if (!string.IsNullOrWhiteSpace(cnpj))
                return Ok(await _repository.GetPedidosByCnpjCliente(cnpj, pageSize, pageIndex));
            else if (!string.IsNullOrWhiteSpace(razaoSocial))
                return Ok(await _repository.GetPedidosByRazaoSocialCliente(razaoSocial, pageSize, pageIndex));
            else
                return BadRequest("É necessário informar o CNPJ ou Razão Social do cliente.");
        }
    }
}