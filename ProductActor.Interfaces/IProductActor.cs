using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using ProductActor.Interfaces.Models;

namespace ProductActor.Interfaces
{
    /// <summary>
    /// Represent products in shop
    /// </summary>
    public interface IProductActor : IActor
    {
        /// <summary>
        /// Get product properties
        /// </summary>
        /// <returns></returns>
        Task<ProductProperties> GetProperties();

        /// <summary>
        /// Set product properties
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        Task SetProperties(ProductProperties properties);
    }
}
