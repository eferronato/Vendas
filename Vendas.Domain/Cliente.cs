using System;

namespace Vendas.Domain
{
    public class Cliente : EntityBase
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
    }
}
