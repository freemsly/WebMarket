using System.Collections.Generic;
using System.Runtime.Serialization;
using WebMarket.Common;

namespace WebMarket.Api.Model
{
    [DataContract]
    public class eMagazine : Item
    {
        [DataMember(Name=SearchConstants.MediaType)]
        public string MediaType => "eMagazine";

        [DataMember(Name =SearchConstants.Publisher)]
        public string Publisher { get; set; } 

        [DataMember(Name= "country")]
        public string Country { get; set; } 

        [DataMember(Name=SearchConstants.Audience)]
        public string Audience { get; set; } 

        [IgnoreDataMember]
        public string SortTitle { get; set; } 

        [DataMember(Name = SearchConstants.Description)]
        public string Description { get; set; } 

        [DataMember(Name = "rbid")]
        public string RBZID { get; set; } 

        [DataMember(Name = SearchConstants.Title)]
        public string Title { get; set; } 

        [DataMember(Name = "token")]
        public string Token { get; set; } 

        [DataMember(Name = SearchConstants.Genre)]
        public string Genre { get; set; } 

        [DataMember(Name = SearchConstants.Language)]
        public string Language { get; set; } 

        [DataMember(Name = "magazineid")]
        public string MagazineId { get; set; } 

        [DataMember(Name = "issueid")]
        public string IssueId { get; set; } 

        [DataMember(Name = SearchConstants.PublishedDate)]
        public string PublishedOn { get; set; } 

        [DataMember(Name = "coverdate")]
        public string CoverDate { get; set; } 

        [DataMember(Name = "imageurl")]
        public string ImageUrl { get; set; }


        [DataMember(Name = "checkedouton")]
        public string CheckoutOn { get; set; } // PATRON CHECKOUT ON

        [DataMember(Name = "issuetitle")]
        public string IssueTitle { get; set; } // ISSUE TITLE


        [DataMember(Name = "frequency")]
        public string Frequency { get; set; }

        [DataMember(Name = "rating")]
        public string Rating { get; set; }

        [DataMember(Name = "caplimit")]
        public int CapLimit { get; set; }

        [DataMember(Name = "price")]
        public decimal Price { get; set; }
        
        [DataMember(Name = "issn")]
        public string Issn { get; set; }
    }
}
