using AutoMapper;
using Cadastros.API.Models;
using Vendas.Domain;

namespace Cadastros.API.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateClienteModel, Cliente>();
            CreateMap<CreateProdutoModel, Produto>();
            CreateMap<CreateCondPagtoModel, CondPagto>();
        }
    }
}
