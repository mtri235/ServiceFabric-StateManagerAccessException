using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using ShoppingCartActor.Interfaces.Models;

namespace ShoppingCartActor.Interfaces
{
    /// <summary>
    /// This interface defines the methods exposed by an actor.
    /// Clients use this interface to interact with the actor that implements it.
    /// </summary>
    public interface IShoppingCartActor : IActor
    {
        /// <summary>
        /// Query shopping cart items by ids
        /// </summary>
        /// <returns></returns>
        Task<ShoppingItemDetail[]> ListItems(IEnumerable<string> ids);

        /// <summary>
        /// Add item into shopping cart
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        Task AddItem(ShoppingItem item);
    }
}
