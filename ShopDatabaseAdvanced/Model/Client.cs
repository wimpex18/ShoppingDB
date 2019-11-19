using ShopDatabaseAdvanced.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDatabaseAdvanced.Model
{
    class Client
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }

        public Client()
        {

        }

        public Client(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            Code = Code;
            ShoppingCarts = new List<ShoppingCart>();
        }

        internal void AddToClient(ShoppingCart shopcart)
        {
            ShoppingCarts.Add(shopcart);
        }
    }
}
