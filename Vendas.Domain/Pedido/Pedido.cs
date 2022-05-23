using System;
using System.Collections.Generic;

namespace Vendas.Domain
{
    public class Pedido : EntityBase
    {        
        public DateTime DataEmissao { get; set; }        
        public int ClienteId { get; set; }        
        public Cliente Cliente { get; set; }
        public int CondPagtoId { get; set; }
        public CondPagto CondPagto { get; set; }        
        public List<ItemPedido> Itens { get; set; }        
    }
}
