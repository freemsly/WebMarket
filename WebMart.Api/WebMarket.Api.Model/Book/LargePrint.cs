
using System.Runtime.Serialization;
using WebMarket.Common;

namespace WebMarket.Api.Model
{
    [DataContract]
    public class LargePrint : Book
    {
        [DataMember(Name = SearchConstants.MediaCount)]
        public int MediaCount { get; set; }
    }
}
