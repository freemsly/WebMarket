using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WebMarket.Contracts
{
    [DataContract(Namespace = "http://eXtensibleSolutions/schemas/2014/04")]
    public class TypedItem
    {
        public TypedItem()
        {
        }

        public TypedItem(string key, object value)
        {
        }

        [DataMember]
        [XmlAttribute("key")]
        public string Key { get; set; }
        [DataMember]
        [XmlAttribute("domain")]
        public string Domain { get; set; }
        [DataMember]
        [XmlAttribute("scope")]
        public string Scope { get; set; }
        [IgnoreDataMember]
        [XmlIgnore]
        public DateTimeOffset Tds { get; set; }
        [DataMember]
        [XmlAttribute("txt")]
        public string Text { get; set; }
        
        [DataMember]
        public object Value { get; set; }
        [IgnoreDataMember]
        [XmlIgnore]
        public string Display { get; }
        
    }
}
