using System.Runtime.Serialization;
using WebMarket.Common;

namespace WebMarket.Api.Model
{
    [DataContract]
    public class eAudio : Book
    {
        [DataMember(Name = SearchConstants.FileSize)]
        public string FileSize { get; set; }

        [DataMember(Name = SearchConstants.Duration)]
        public decimal Duration { get; set; }

        [DataMember(Name = SearchConstants.Narrator)]
        public string Narrators { get; set; }

        [IgnoreDataMember]
        public string PreviewFile { get; set; }

        [DataMember(Name = SearchConstants.PreviewUrl, EmitDefaultValue = false)]
        public string PreviewUrl { get; set; }

        public string GetPreviewUrl(string previewFile)
        {
            return string.Empty;
            //return !string.IsNullOrEmpty(previewFile) ? S3Manager.FetchPreviewUrl(previewFile) : null;
        }
        [DataMember(Name = SearchConstants.RecordingType)]
        public string RecordingType { get; set; }
    }
}
