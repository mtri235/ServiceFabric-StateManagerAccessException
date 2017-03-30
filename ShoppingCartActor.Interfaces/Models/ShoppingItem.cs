using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartActor.Interfaces.Models
{
    [DataContract]
    public class ShoppingItem
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Id { get; private set; }

        public ShoppingItem(string id, string name)
        {
            Name = name;
            Id = id;
        }
    }
}
