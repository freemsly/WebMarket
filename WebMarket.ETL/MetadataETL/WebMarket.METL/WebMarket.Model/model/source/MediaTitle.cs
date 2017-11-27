// <copyright company="Recorded Books, Inc" file="MediaTitle.cs">
// Copyright © 2015 All Right Reserved
// </copyright>


namespace WebMarket.Model
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class MediaTitle
    {
        public string TitleId { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }

        private List<Contributor> _contributors;
        public List<Contributor> Contributors { get { return _contributors; } set { _contributors = value; } }

        public List<ContentAdvisory> ContentAdvisory { get; set; }

        private List<Bisac> _bisacs;
        public List<Bisac> Bisacs { get { return _bisacs; } set { _bisacs = value; } }

        public List<string> SalesRights { get; set; }

        public DateTime TitlePublicationDate { get; set; }
        public int OnixLanguageId { get; set; }
        public string Language { get; set; }
        public string Imprints { get; set; }
        public int SeriesId { get; set; }
        public string Series { get; set; }
        public int NumberInSeries { get; set; }
        public string ISBN { get; set; }
        public string Publisher { get; set; }
        public DateTime EditionPublicationDate { get; set; }
        public int FormatId { get; set; }
        public int BindingId { get; set; }
        public int NumberOfMedia { get; set; }
        public int BindingMethodId { get; set; }
        public int OriginalCopyright { get; set; }
        public string StatusCode { get; set; }
        public string SourceItemId { get; set; }
        public string Format { get; set; }
        public string Audience { get; set; }
        public bool IsFiction { get; set; }
        public string FnF { get; set; }
        public string Description { get; set; }
        public bool HasDrm { get; set; }
        
        

        public int Index { get; set; }
        public int Outdex { get; set; }

        public decimal ListPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public decimal DiscountPrice { get; set; }

        public List<Group> Groups { get; set; }

        
        public int StockLevel { get; set; }

        public string Awards { get; set; }

        public List<string> Review { get; set; }
        public int Rating { get; set; }
        public bool IsMarcAllowed { get; set; }
        public string ProductLine { get; set; }

        //below 2 fields are for CD & DVD only
        public string RecordingType { get; set; } //abridged or unbridged
        public string MediaTypeDescription { get; set; } // kit, video, audio
        public string MediaTypeBinding { get; set; } // hanging set, class set  - is KIT
        public bool IsExclusive { get; set; }



        public List<SubscriptionOwnership> Subscription { get; set; }
        public List<Holds> Holds { get; set; }
        public List<Circulation> Circulations { get; set; }
        public List<UsageTerm> UsageTerm { get; set; }
        public List<SOPMetadata> SOP { get; set; }
        public List<Expiration> Expirations { get; set; }

        public List<Ownership> Ownership { get; set; }
    }
    
}
