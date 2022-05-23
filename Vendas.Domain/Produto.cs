using System;

namespace Vendas.Domain
{
    public class Produto : EntityBase
    {        
        public string SKU { get; set; }
        public string Descricao { get; set; }
    }
}
