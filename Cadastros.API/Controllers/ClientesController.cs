using AutoMapper;
using Cadastros.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Vendas.Domain;
using Vendas.Infrastructure.Cross;
using Vendas.Infrastructure.Repositories;

namespace Cadastros.API.Controllers
{
    /// <summary>
    /// Endpoint referente ao cadastro de clientes
    /// </summary>
    [Route("api/v1/clientes")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ClientesController(IClienteRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<Cliente>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _repository.GetAsync(pageSize, pageIndex);
            var count = await _repository.CountAsync();
            var model = new PagedResponse<Cliente>(pageIndex, pageSize, count, result);
            return Ok(model);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Cliente), StatusCodes.Status200OK)]
        public async Task<ActionResult<Cliente>> GetByIdAsync(int id)
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
        public async Task<ActionResult<Cliente>> CreateAsync([FromBody] CreateClienteModel request)
        {
            var cliente = _mapper.Map<Cliente>(request);
            var result = await _repository.AddAsync(cliente);
            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateAsync([FromBody] Cliente cliente)
        {
            var result = await _repository.UpdateAsync(cliente);

            if (result == null)
                return NotFound(new { Message = $"Cliente com o ID {cliente.Id} não encontrado." });

            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);

            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}