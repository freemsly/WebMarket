// <copyright company="Recorded Books, Inc" file="AdditionalMetadata.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;

    public sealed class OneclickMetadata
    {
        //public int Id { get; set; }
        public string SourceItemId { get; set; }
        //public bool HasImages { get; set; }
        public string S3Foldername { get; set; }
        public DateTime ActivationTimeStamp { get; set; }
        //public DateTime ReleaseDate { get; set; }
        //public DateTime PublicationDate { get; set; }
        //public DateTime ScheduledReleaseDate { get; set; }
        //public DateTime PreReleaseDate { get; set; }
        public bool IsPreRelease { get; set; }
        public string Imprint { get; set; }
        public string PreviewFilename { get; set; }
        public string Isbn { get; set; }
        public bool HasDrm { get; set; }
        public decimal ActualDuration { get; set; }
        //public decimal DeclaredDuration { get; set; }
        //public int MediaQuantity { get; set; }
        public long AudioFileSize { get; set; }
        public bool HasAttachments { get; set; }
    }
}
