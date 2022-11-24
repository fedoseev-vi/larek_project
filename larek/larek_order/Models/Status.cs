using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace larek_order.Models
{
    public class Status
    {
        public int status_id { get; set; }
        public string status_name { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
