using AutoMapper;
using Precos.API.Models;
using Vendas.Domain;

namespace Cadastros.API.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreatePoliticaPrecoModel, PoliticaPreco>();            
            CreateMap<UpdatePoliticaPrecoModel, PoliticaPreco>();            
            CreateMap<PoliticaPreco, PoliticaPrecoResponse>();
        }
    }
}
