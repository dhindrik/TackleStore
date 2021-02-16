using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using TackleStore.Models;
using TinyPubSubLib;

namespace TackleStore.Clients.Services
{
    public record CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class CartService
    {
        public static List<CartItem> items = new ();

        public static Task Add(Product product)
        {
            var item = items.SingleOrDefault(x => x.Product.Sku == product.Sku);

            if (item != null)
            {
                item.Quantity++;
                return Task.CompletedTask;
            }

            item = new CartItem()
            {
                Product = product,
                Quantity = 1
            };

            items.Add(item);

            TinyPubSub.Publish("cart-updated");

            return Task.CompletedTask;
        }

        public static Task<List<CartItem>> Get()
        {
            return Task.FromResult(items);
        }
    }
}
