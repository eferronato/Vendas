using System;

namespace Vendas.Domain
{
    public class PoliticaPreco : EntityBase
    {        
        public string Descricao { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public decimal Preco { get; set; }
    }
}
