using Vendas.Domain;

namespace Precos.API.Models
{
    public class PoliticaPrecoResponse
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Produto Produto { get; set; }
        public Cliente Cliente { get; set; }
        public decimal Preco { get; set; }
    }
}
