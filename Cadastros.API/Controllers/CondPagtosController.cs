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
    [Route("api/v1/condpagtos")]
    [ApiController]
    public class CondPagtosController : ControllerBase
    {
        private readonly ICondPagtoRepository _repository;
        private readonly IMapper _mapper;

        public CondPagtosController(ICondPagtoRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<CondPagto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _repository.GetAsync(pageSize, pageIndex);
            var count = await _repository.CountAsync();
            var model = new PagedResponse<CondPagto>(pageIndex, pageSize, count, result);
            return Ok(model);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(CondPagto), StatusCodes.Status200OK)]
        public async Task<ActionResult<CondPagto>> GetByIdAsync(int id)
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
        public async Task<ActionResult<CondPagto>> CreateAsync([FromBody] CreateCondPagtoModel request)
        {
            var condPagto = _mapper.Map<CondPagto>(request);
            var result = await _repository.AddAsync(condPagto);
            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateAsync([FromBody] CondPagto condPagto)
        {
            var result = await _repository.UpdateAsync(condPagto);

            if (result == null)
                return NotFound(new { Message = $"Condição de pagamento com o ID {condPagto.Id} não encontrada." });

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