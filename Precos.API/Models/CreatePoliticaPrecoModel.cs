namespace Precos.API.Models
{
    public class CreatePoliticaPrecoModel
    {
        public string Descricao { get; set; }
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }        
        public decimal Preco { get; set; }
    }
}
