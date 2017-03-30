using ShoppingCartActor.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartActor.Models
{
    [DataContract]
    public class ShoppingItemsState
    {
        [DataMember]
        public Dictionary<string , ShoppingItem> Items { get; private set; }
        public ShoppingItemsState() {
            Items = new Dictionary<string, ShoppingItem>();
        }

        public ShoppingItemsState(Dictionary<string, ShoppingItem> items)
        {
            Items = items;
        }

    }
}
