
using System.Runtime.Serialization;

namespace WebMarket.Api.Model
{
    [DataContract]
    public class ContentAdvisory
    {
        [DataMember(Name = "sex")]
        public string Sex { get; set; }
        [DataMember(Name = "language")]
        public string Language { get; set; }
        [DataMember(Name = "violence")]
        public string Violence { get; set; }
    }

    public enum ContentAdvisoryEnum
    {
        None,
        UnRated,
        Mild,
        Moderate,
        Strong
    }
}
