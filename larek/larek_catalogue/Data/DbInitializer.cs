using larek_catalogue.Models;
using System;
using System.Linq;


namespace larek_catalogue.Data
{
    public static class DbInitializer
    {
        public static void Initialize(CatalogueContext context)
        {
            {
                context.Database.EnsureCreated();

                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                Product p = new Product{category_id=1,brand_id=1,price = 69,product_name="Шоколад Бабаевский тёмный 70% какао 100 грамм"};
                Product pp = new Product { category_id = 1, brand_id = 1, price = 69, product_name = "Шоколад Бабаевский с миндалём 100 грамм" };
                Brand b = new Brand{brand_name="Бабаевский"};
                Category c =  new Category{category_name="Шоколад"};

                context.Products.Add(p);
                context.Products.Add(pp);
                context.Brands.Add(b);
                context.Categories.Add(c);
                context.SaveChanges();
            }
        }
    }
}
