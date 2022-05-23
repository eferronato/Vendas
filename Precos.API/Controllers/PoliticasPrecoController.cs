using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Precos.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vendas.Domain;
using Vendas.EventBus.Models;
using Vendas.Infrastructure.Cross;
using Vendas.Infrastructure.Repositories;

namespace Precos.API.Controllers
{
    [Route("api/v1/precos")]
    [ApiController]
    public class PoliticasPrecoController : ControllerBase
    {
        private readonly IPoliticaPrecoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publisher; 

        public PoliticasPrecoController(IPoliticaPrecoRepository repository, IMapper mapper, IPublishEndpoint publisher)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _publisher = publisher;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PagedResponse<PoliticaPrecoResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var result = await _repository.GetAsync(pageSize, pageIndex);
            var count = await _repository.CountAsync();
            var model = new PagedResponse<PoliticaPrecoResponse>(pageIndex, pageSize, count, _mapper.Map<IEnumerable<PoliticaPrecoResponse>>(result));
            return Ok(model);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PoliticaPreco), StatusCodes.Status200OK)]
        public async Task<ActionResult<PoliticaPreco>> GetByIdAsync(int id)
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
        public async Task<ActionResult<PoliticaPrecoResponse>> CreateAsync([FromBody] CreatePoliticaPrecoModel request)
        {
            var entity = _mapper.Map<PoliticaPreco>(request);
            var result = await _repository.AddAsync(entity);

            return CreatedAtAction("GetById", new { id = result.Id }, _mapper.Map<PoliticaPrecoResponse>(result));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdatePoliticaPrecoModel model)
        {
            var politicaAtual = await _repository.GetByIdAsync(model.Id);
            if (politicaAtual == null)
                return NotFound(new { Message = $"Política de preço com o ID {model.Id} não encontrada." });

            var entity = _mapper.Map<PoliticaPreco>(model);
            var result = await _repository.UpdateAsync(entity);

            //Verifica se o preço foi alterado e dispara um evento para notificar a alteração do preço
            if (model.Preco != politicaAtual.Preco)
            {
                var message = PrecoChangedEvent.Create(entity);
                await _publisher.Publish(message);
            }

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