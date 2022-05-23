namespace Pedidos.API.Models
{
    public class CreateItemPedidoModel
    {
        public int ProdutoId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
    }
}
