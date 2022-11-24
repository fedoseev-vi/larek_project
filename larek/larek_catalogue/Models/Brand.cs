using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace larek_catalogue.Models
{
    public class Brand
    {
        public int brand_id { get; set; }
        public string brand_name { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
