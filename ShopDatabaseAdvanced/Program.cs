using ShopDatabaseAdvanced.Model;
using ShopDatabaseAdvanced.Models;
using ShopDatabaseAdvanced.ShopAdvancedDbContext;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDatabaseAdvanced
{
	class Program
	{
		static void Main(string[] args)
		{

			using (var db = new AdvancedShopDatabaseContext())
			{
                //db.Foods.AddRange(groceries);
                //db.SaveChanges();


                ShoppingCart newCart = new ShoppingCart();
				db.ShoppingCarts.Add(newCart);


                Console.WriteLine("Hello, please enter your first and last name");
                string clientName = Console.ReadLine();
                Client Client = db.Clients.Include("ShoppingCarts").FirstOrDefault(x => x.Name == clientName);

                if (Client == null)
                {
                    Console.WriteLine($"Hello {clientName}!");
                    Console.WriteLine($"You're a new client");
                    Client newClient = new Client (clientName);
                    db.Clients.Add(newClient);
                    newClient.AddToClient(newCart);
                }
                else
                {
                    Console.WriteLine($"Welcome back {clientName}!");
                    Console.WriteLine($"You've visited our store {Client.ShoppingCarts.Count} times");
                    Console.WriteLine("Do you want to see your purchase history? Y/N");
                    if (Console.ReadLine() == "Y")
                    {
                        foreach (var cart in Client.ShoppingCarts)
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Shopping cart created on {cart.DateCreated}");
                            foreach (var food in cart.Items)
                            {
                                Console.WriteLine($"{food.Name} Price: {food.Price}");
                            }
                            Console.WriteLine($"Total: {cart.Sum}");
                        }
                    }
                    Client.AddToClient(newCart);
                    db.Clients.AddOrUpdate(Client);
                }


                ChooseFood(db, newCart);
				while (Console.ReadLine() == "Y")
				{
					ChooseFood(db, newCart);
                }
                
                
                Console.WriteLine("Your shopping cart: ");
                foreach (var food in newCart.Items)
                {
                    Console.WriteLine();
                    Console.WriteLine($"{food.Name} Price: {food.Price}");
                }
                Console.WriteLine();
                Console.WriteLine("Do you want to pay? Y/N");
                if (Console.ReadLine() == "Y")
                {
                    Console.WriteLine();
                    Console.WriteLine("Thank you for visit! See you next time.");
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("Your cart has been deleted!");
                }


                /*
                var shoppingCarts = db.ShoppingCarts.Include("Items").ToList();
                foreach (var cart in shoppingCarts)
				{
                    Console.WriteLine();
                    Console.WriteLine($"Shopping cart created on {cart.DateCreated}");
					foreach(var food in cart.Items)
					{
						Console.WriteLine($"{food.Name} Price: {food.Price}");
					}
					Console.WriteLine($"Total: {cart.Sum}");
				}
                */
			}

		
		}

		private static void ChooseFood(AdvancedShopDatabaseContext db, ShoppingCart newCart)
		{



                Console.WriteLine();
            Console.WriteLine("What do you want to buy?");
			string foodName = Console.ReadLine();
			Food chosenFood = db.Foods.FirstOrDefault(x => x.Name == foodName);

                newCart.AddToCart(chosenFood);
                Console.WriteLine();
                    Console.WriteLine("Your Shopping cart: ");
                    foreach (var food in newCart.Items)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"{food.Name} Price: {food.Price}");
                    }

                    Console.WriteLine($"Total: {newCart.Sum}");
                    Console.WriteLine();
                    Console.WriteLine("Anything else? Y/N");
                    
                
            
           
		}
	}
}
