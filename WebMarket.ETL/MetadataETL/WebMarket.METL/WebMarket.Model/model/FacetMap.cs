// <copyright company="Recorded Books Inc" file="FacetMap.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    [Serializable]
    public partial class FacetMap
    {


        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        private List<string> keyTokens = new List<string>();
        public List<string> KeyTokens
        {
            get { return keyTokens; }
            set { keyTokens = value; }
        }

        private int count;
        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        private string facetToken;
        public string FacetToken
        {
            get { return facetToken; }
            set { facetToken = value; }
        }

        private string facetDisplay;
        public string FacetDisplay
        {
            get { return facetDisplay; }
            set { facetDisplay = value; }
        }

        private string sourceSystem;
        public string SourceSystem
        {
            get { return sourceSystem; }
            set { sourceSystem = value; }
        }

        private string sourceSystemId;
        public string SourceSystemId
        {
            get { return sourceSystemId; }
            set { sourceSystemId = value; }
        }

        private string facetValue;
        public string FacetValue
        {
            get { return facetValue; }
            set { facetValue = value; }
        }

        private string facetValueDisplay;
        public string FacetValueDisplay
        {
            get { return facetValueDisplay; }
            set { facetValueDisplay = value; }
        }

    }
}
