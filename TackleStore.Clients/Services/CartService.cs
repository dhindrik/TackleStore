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
        public ProductItem Product { get; set; }
        public Variant Variant { get; set; }
        public int Quantity { get; set; }
        public string QuantityText
        {
            get => Quantity.ToString();
            set {
                if(int.TryParse(value, out int result))
                {
                    Quantity = result;
                }
                else if(string.IsNullOrWhiteSpace(value))
                {
                    Quantity = 0;
                }
            } 
        }
        public double TotalPrice
        {
            get
            {
                return Quantity * Variant.Price;
            }
        }
    }

    public class CartService
    {
        public static List<CartItem> items = new ();

        public static Task Add(ProductItem product, Variant variant, int quantity = 1)
        {
            var item = items.SingleOrDefault(x => x.Product.Id == product.Id);

            if (item != null)
            {
                item.Quantity += quantity;
                return Task.CompletedTask;
            }

            item = new CartItem()
            {
                Product = product,
                Variant = variant,
                Quantity = quantity
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
