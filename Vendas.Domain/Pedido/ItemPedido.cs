using System;
using System.Text.Json.Serialization;

namespace Vendas.Domain
{
    public class ItemPedido : EntityBase
    {
        [JsonIgnore]
        public Pedido Pedido { get; set; }
        public int PedidoId { get; set; }        
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }        
    }
}
