using System.Collections.Generic;

namespace Pedidos.API.Models
{
    public class CreatePedidoModel
    {
        public int ClienteId { get; set; }
        public int CondPagtoId { get; set; }
        public List<CreateItemPedidoModel> Itens { get; set; }
    }
}
