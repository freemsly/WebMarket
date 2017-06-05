
using System.Runtime.Serialization;
using WebMarket.Common;

namespace WebMarket.Api.Model
{
    [DataContract]
    public class Playaway : Book
    {
        [DataMember(Name = SearchConstants.Narrator)]
        public string Narrators { get; set; }
    }
}
