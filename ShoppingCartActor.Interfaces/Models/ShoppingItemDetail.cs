using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartActor.Interfaces.Models
{
    [DataContract]
    public class ShoppingItemDetail
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Id { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        public ShoppingItemDetail(string id, string name, string description)
        {
            Name = name;
            Id = id;
            Description = description;
        }
    }
}
