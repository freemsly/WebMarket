using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace WebMarket.Api.Model
{
    public class Displayable
    {
        public Item Item { get; set; }

        [DataMember(Name = "recommendations", EmitDefaultValue = false)]
        public List<Item> Recommendations { get; set; }

        [DataMember(Name = "otherformats", EmitDefaultValue = false)]
        public List<Item> OtherFormatsItems { get; set; }

        //[DataMember(Name = "bundle", EmitDefaultValue = false)]
        //public Bundle Bundle { get; set; }
    }
}
