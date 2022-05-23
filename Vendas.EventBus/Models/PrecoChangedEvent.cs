using System;
using Vendas.Domain;

namespace Vendas.EventBus.Models
{
    public class PrecoChangedEvent : EventBase
    {        
        public int ClienteId { get; set; }
        public int ProdutoId { get; set; }
        public decimal Preco { get; set; }

        /// <summary>
        /// Factory para criação do evento a partir da entidade 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static PrecoChangedEvent Create(PoliticaPreco entity)
        {
            return new PrecoChangedEvent
            {
                ClienteId = entity.ClienteId,
                ProdutoId = entity.ProdutoId,
                Preco = entity.Preco
            };
        }
    }
}
