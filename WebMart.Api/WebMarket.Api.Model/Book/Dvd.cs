
using System.Runtime.Serialization;
using WebMarket.Common;

namespace WebMarket.Api.Model
{
    [DataContract]
    public class Dvd : Book
    {
        [DataMember(Name = SearchConstants.MediaCount)]
        public int MediaCount { get; set; }
        [DataMember(Name = SearchConstants.StockLevel)]
        public string StockLevel { get; set; }
        [DataMember(Name = SearchConstants.Narrator)]
        public string Narrators { get; set; }
        [DataMember(Name = SearchConstants.RecordingType)]
        public string RecordingType { get; set; }
    }
}
