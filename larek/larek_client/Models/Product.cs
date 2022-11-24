using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace larek_client.Models
{
    class Product
    {
            public int product_id { get; set; }
            public int category_id { get; set; }
            public int brand_id { get; set; }
            public int price { get; set; }
            public string product_name { get; set; }
            public Category Category { get; set; }
            public Brand Brand { get; set; }
    }
}
