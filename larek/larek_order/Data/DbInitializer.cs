using larek_order.Models;
using System;
using System.Linq;


namespace larek_order.Data
{
    public static class DbInitializer
    {
        public static void Initialize(OrderContext context)
        {
            {
                context.Database.EnsureCreated();

                if (context.Orders.Any())
                {
                    return;   // DB has been seeded
                }

               Order p = new Order { status_id = 1, product = 1,  adress = "Москва, улица Пушкина, дом Колотушкина", customer_name = "Владимир" };
                Status[] b = { 
                    new Status{ status_name = "Создан" },
                    new Status{ status_name = "Принят" },
                    new Status{ status_name = "Собран" },
                    new Status{ status_name = "Отправлен" },
                    new Status{ status_name = "Получен" }
                };

                context.Orders.Add(p);
                foreach (Status a in b)
                { 
                    context.Statuses.Add(a); 
                }
                context.SaveChanges();
            }
        }
    }
}