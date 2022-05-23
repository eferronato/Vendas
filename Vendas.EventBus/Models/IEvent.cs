using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.EventBus.Models
{
    public class EventBase
    {
        public EventBase()
        {
            this.CreatedAt = DateTime.Now;
        }

        public DateTime CreatedAt { get; set; }
    }
}
