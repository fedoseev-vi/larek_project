using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace larek_catalogue.Models
{
    public class Category
    {
        public int category_id { get; set; }
        public string category_name { get; set; }

        public ICollection<Product> Product { get; set; }
    }
}
