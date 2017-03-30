using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ProductActor.Interfaces.Models
{
    [DataContract]
    public class ProductProperties
    {
        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        public ProductProperties(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
