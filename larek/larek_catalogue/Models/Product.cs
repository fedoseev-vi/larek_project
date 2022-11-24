using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace larek_catalogue.Models
{
    public class Product
    {
        public int product_id { get; set; }
        public int category_id { get; set; }
        public int brand_id { get; set; }
        public double price { get; set; }
        public string product_name { get; set; }

        public Category Category{ get; set; }
        public Brand Brand { get; set; }
    }
}
