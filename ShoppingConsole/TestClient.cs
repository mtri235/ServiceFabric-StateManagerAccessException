using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using ProductActor.Interfaces;
using ProductActor.Interfaces.Models;
using ShoppingCartActor.Interfaces;
using ShoppingCartActor.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingConsole
{
    public class TestClient
    {
        private readonly ShoppingItemDetail[] items;

        public TestClient() {
            items = new ShoppingItemDetail[] {
                new ShoppingItemDetail("item-1", "tomato", "Red"),
                new ShoppingItemDetail("item-2", "potato", "Yellow"),
                new ShoppingItemDetail("item-3", "chocolate", "Brown"),
            };
        }

        public Task CreateProduct() =>
            Task.WhenAll(
                from item in items
                select GetProductActor(item.Id)
                    .SetProperties(new ProductProperties(
                        item.Name,
                        item.Description)));

        public Task CreateShoppingItems() {
            var actor = GetActor();
            return Task.WhenAll(from item in items select actor.AddItem(new ShoppingItem(item.Id, item.Name)));
        }

        public Task<ShoppingItemDetail[]> GetShoppingItems() =>
           GetActor().ListItems(
               from item in items
               select item.Id);
        
        private IProductActor GetProductActor(string id) =>
            ActorProxy.Create<IProductActor>(
                new ActorId(id),
                new Uri("fabric:/StateManagerAccess/ProductActorService"));

        private IShoppingCartActor GetActor() =>
            ActorProxy.Create<IShoppingCartActor>(
                new ActorId("MrTriShoppingCart"),
                new Uri("fabric:/StateManagerAccess/ShoppingCartActorService"));
    }
}
