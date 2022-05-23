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
    [Route("api/v1/produtos")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<Produto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _repository.GetAsync(pageSize, pageIndex);
            var count = await _repository.CountAsync();
            var model = new PagedResponse<Produto>(pageIndex, pageSize, count, result);
            return Ok(model);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Produto), StatusCodes.Status200OK)]
        public async Task<ActionResult<Produto>> GetByIdAsync(int id)
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
        public async Task<ActionResult<Produto>> CreateAsync([FromBody] CreateProdutoModel request)
        {
            var produto = _mapper.Map<Produto>(request);
            var result = await _repository.AddAsync(produto);
            return CreatedAtAction("GetById", new { id = result.Id }, result);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateAsync([FromBody] Produto produto)
        {
            var result = await _repository.UpdateAsync(produto);

            if (result == null)
                return NotFound(new { Message = $"Produto com o ID {produto.Id} não encontrado." });

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