using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WebMarket.Common;

namespace WebMarket.Api.Model
{
    [KnownType(typeof(Cd))]
    [KnownType(typeof(Dvd))]
    [KnownType(typeof(Mp3))]
    [KnownType(typeof(eAudio))]
    [KnownType(typeof(eBook))]
    [KnownType(typeof(Text))]
    [KnownType(typeof(Playaway))]
    [KnownType(typeof(LargePrint))]
    [DataContract]
    public class Book : Item
    {
        [IgnoreDataMember]
        public ItemTypeOption ItemType { get; set; }

        //[DataMember(Name = "iteminterest", EmitDefaultValue = false)]
        //public ItemInterest ItemInterest { get; set; }

        //[DataMember(Name = "customerinterest", EmitDefaultValue = false)]
        //public CustomerInterest CustomerInterest { get; set; }

        [DataMember(Name = SearchConstants.Isbn)]
        public string Isbn { get; set; }
        [DataMember(Name = SearchConstants.SubTitle)]
        public string Subtitle { get; set; }
        [DataMember(Name = SearchConstants.Author)]
        public string Authors { get; set; }
        [DataMember(Name = SearchConstants.Publisher)]
        public string Publisher { get; set; }
        [DataMember(Name = SearchConstants.Genre)]
        public string Genre { get; set; }
        [DataMember(Name = SearchConstants.Language)]
        public string Language { get; set; }
        [DataMember(Name = SearchConstants.Audience)]
        public string Audience { get; set; }
        [DataMember(Name = SearchConstants.MediaType)]
        public string MediaType { get; set; }
        [DataMember(Name = SearchConstants.ReleaseDate)]
        public DateTime ReleasedDate { get; set; }
        [DataMember(Name = SearchConstants.PublishedDate)]
        public DateTime PublishedDate { get; set; }
        [DataMember(Name = SearchConstants.Description)]
        public string Description { get; set; }
        [DataMember(Name = SearchConstants.Series)]
        public string Series { get; set; }
        [DataMember(Name = SearchConstants.SeriesNo)]
        public string SeriesNo { get; set; }
        [DataMember(Name = SearchConstants.Title)]
        public string Title { get; set; }
        [DataMember(Name = SearchConstants.FictionNonfiction)]
        public bool IsFiction { get; set; }
        [DataMember(Name = SearchConstants.ListPrice)]
        public decimal ListPrice { get; set; }
        [DataMember(Name = SearchConstants.RetailPrice)]
        public decimal RetailPrice { get; set; }
        [DataMember(Name = SearchConstants.DiscountPrice)]
        public decimal DiscountPrice { get; set; }
        private List<ImageUrl> _images;
        [DataMember(Name = "images")]
        public List<ImageUrl> Images
        {
            get { return _images ?? (_images = new List<ImageUrl>()); }
            set { _images = value; }
        }

        [DataMember(Name = SearchConstants.Award)]
        public string Awards { get; set; }

        [DataMember(Name = SearchConstants.Imprint)]
        public string Imprint { get; set; }

        [DataMember(Name = "sourceitemid")]
        public string SourceItemId { get; set; }

        [DataMember(Name = SearchConstants.Review)]
        public List<string> Review { get; set; }

        [DataMember(Name = SearchConstants.Rating)]
        public int Rating { get; set; }

        [DataMember(Name = SearchConstants.IsMarcAllowed)]
        public bool IsMarcAllowed { get; set; }

        [DataMember(Name = SearchConstants.ContentAdvisory)]
        public ContentAdvisory ContentAdvisory { get; set; }

        [DataMember(Name = SearchConstants.MediaTypeDescription)]
        public string MediaTypeDescription { get; set; }

        [DataMember(Name = SearchConstants.MediaTypeBinding)]
        public string MediaTypeBinding { get; set; }

        //[DataMember(Name = SearchConstants.Pricing)]
        //public List<Pricing> Pricing { get; set; }
    }
}
