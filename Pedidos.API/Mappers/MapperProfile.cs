using AutoMapper;
using Pedidos.API.Models;
using System;
using Vendas.Domain;

namespace Pedidos.API.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CreatePedidoModel, Pedido>().AfterMap((s, d) => { d.DataEmissao = DateTime.Now; });
            CreateMap<CreateItemPedidoModel, ItemPedido>();
        }
    }
}
