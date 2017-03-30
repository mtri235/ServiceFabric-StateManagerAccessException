using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using ProductActor.Interfaces;
using ProductActor.Interfaces.Models;

namespace ProductActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class ProductActor : Actor, IProductActor
    {
        public const string PropertiesStateKey = "propertiesstate";
        /// <summary>
        /// Initializes a new instance of ProductActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public ProductActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        private Task SetPropertiesStateAsync(ProductProperties properties) =>
            this.StateManager.SetStateAsync(PropertiesStateKey, properties);

        private async Task<ProductProperties> GetStateAsync() {
            var conditionalValue = await this.StateManager.TryGetStateAsync<ProductProperties>(PropertiesStateKey);
            return conditionalValue.HasValue ? conditionalValue.Value : new ProductProperties(null, null);
        }


        public Task<ProductProperties> GetProperties() => GetStateAsync();

        public Task SetProperties(ProductProperties properties) => SetPropertiesStateAsync(properties);
    }
}
