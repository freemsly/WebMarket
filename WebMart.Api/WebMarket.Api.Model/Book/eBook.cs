using System.Runtime.Serialization;
using WebMarket.Common;

namespace WebMarket.Api.Model
{
    [DataContract]
    public class eBook : Book
    {
        [DataMember(Name = SearchConstants.HasAttachment)]
        public bool HasAttachment { get; set; }
    }
}
