using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using ShoppingCartActor.Interfaces;
using ShoppingCartActor.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new TestClient();
            Console.WriteLine("Press any key to start setup");
            Console.ReadLine();
            client.CreateProduct();
            Console.WriteLine("Products created");
            client.CreateShoppingItems();
            Console.WriteLine("Shopping cart filled");

            Console.WriteLine("l:list product in cart ; any other key: quit");
            var willContinue = Console.ReadLine();
            while (willContinue == "l")
            {
                var items = client.GetShoppingItems().Result;
                var names = from item in items select $"{item.Name}: {item.Description}";
                Console.WriteLine(String.Join("\n", names));
                Console.WriteLine("----------------------");
                willContinue = Console.ReadLine();
            }
            
        }
    }
}
