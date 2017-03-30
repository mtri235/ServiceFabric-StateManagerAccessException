using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using ShoppingCartActor.Interfaces;
using ShoppingCartActor.Models;
using ShoppingCartActor.Interfaces.Models;
using ProductActor.Interfaces;

namespace ShoppingCartActor
{
    /// <remarks>
    /// Shopping cart contains shopping items
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class ShoppingCartActor : Actor, IShoppingCartActor
    {
        public const string ShoppingItemsStateKey = "shoppingitems";
        /// <summary>
        /// Initializes a new instance of ShoppingCartActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public ShoppingCartActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        private Task SetShoppingStateAsync(ShoppingItemsState state) =>
            this.StateManager.SetStateAsync(ShoppingItemsStateKey, state);

        private async Task<ShoppingItemsState> GetShoppingStateAsync() {
            var state = await this.StateManager.TryGetStateAsync<ShoppingItemsState>(ShoppingItemsStateKey);
            return state.HasValue ? state.Value: new ShoppingItemsState();
        }

        /// <summary>
        /// Get a shopping item in cart by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Task<ShoppingItem> GetShoppingItemById(string id) =>
            GetShoppingStateAsync()
                .ContinueWith(task =>
                    task.Result.Items
                    .Where(kv => kv.Key == id)
                    .Select(kv => kv.Value)
                    .FirstOrDefault());

        /// <summary>
        /// Get detail of a product in cart by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Task<ShoppingItemDetail> GetShoppingItemDetailById(string id) =>
             ActorProxy.Create<IProductActor>(
                    new ActorId(id),
                    new Uri("fabric:/StateManagerAccess/ProductActorService"))
                    .GetProperties()
                    .ContinueWith(task =>
                        GetShoppingItemById(id).ContinueWith(item =>
                        new ShoppingItemDetail(
                            item.Result.Id,
                            item.Result.Name,
                            task.Result.Description)))
                    .Unwrap();

        /// <inheritdoc />
        public Task<ShoppingItemDetail[]> ListItems(IEnumerable<string> ids) =>
            Task.WhenAll(
                from id in ids
                select GetShoppingItemDetailById(id));
        
        /// <inheritdoc />
        public async Task AddItem(ShoppingItem item)
        {
            var existingState = await GetShoppingStateAsync();
            existingState.Items.Add(item.Id, item);
            var newState = new ShoppingItemsState(existingState.Items);
            await SetShoppingStateAsync(newState);
        }
    }
}
