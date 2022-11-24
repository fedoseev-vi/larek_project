using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace larek_client.Models
{
    class Order
    {
            public int order_id { get; set; }
            public int status_id { get; set; }
            public int product { get; set; }
            public string adress { get; set; }
            public string customer_name { get; set; }
            public Status Status { get; set; }
    }
}
