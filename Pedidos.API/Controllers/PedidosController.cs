using AutoMapper;
using Pedidos.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vendas.Domain;
using Vendas.Infrastructure.Cross;
using Vendas.Infrastructure.Repositories;

namespace Pedidos.API.Controllers
{
    [Route("api/v1/pedidos")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoRepository _repository;
        private readonly IMapper _mapper;

        public PedidosController(IPedidoRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<Pedido>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _repository.GetAsync(pageSize, pageIndex);
            var count = await _repository.CountAsync();
            var model = new PagedResponse<Pedido>(pageIndex, pageSize, count, result);
            return Ok(model);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Pedido), StatusCodes.Status200OK)]
        public async Task<ActionResult<Pedido>> GetByIdAsync(int id)
        {
            if (id <= 0)
                return BadRequest();

            var result = await _repository.GetByIdAsync(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Pedido>> CreateAsync([FromBody] CreatePedidoModel request)
        {
            var pedido = _mapper.Map<Pedido>(request);
            var result = await _repository.AddAsync(pedido);
            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }

        [HttpGet("GetUltimoPrecoByClienteProduto")]
        [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<decimal>> GetUltimoPrecoByClienteProduto([FromQuery] int clienteId, [FromQuery] int produtoId)
        {
            var result = await _repository.GetUltimoPrecoByClienteProduto(clienteId, produtoId);
            if (result.HasValue)
                return result.Value;
            else
                return NotFound();
        }
    }
}