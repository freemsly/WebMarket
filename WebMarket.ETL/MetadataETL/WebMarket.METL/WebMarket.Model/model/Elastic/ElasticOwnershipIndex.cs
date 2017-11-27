// <copyright company="Recorded Books, Inc" file="ElasticOwnershipIndex.cs">
// Copyright © 2014 All Right Reserved
// </copyright>




namespace WebMarket.Model
{
    using Nest;
    using System;
    using System.Collections.Generic;

    [Serializable]
    [ElasticsearchType(Name = "elasticownershipindex")]
    public class ElasticOwnershipIndex
    {

        
        public string Id { get; set; }
        
        public int ScopeId { get; set; }
        
        public int TotalCopies { get; set; }
        
        public int CirculationCopies { get; set; }
        
        public int HoldsCopies { get; set; }
        
        public decimal HoldsRatio { get; set; }
        
        public List<SubscriptionOwnership> Subscriptions { get; set; }
        
        public List<SOP> Sop { get; set; }
        
        public List<Expiration> Expirations { get; set; }
    }

    [Serializable]
    public class ElasticOwnershipIndexCollection
    {
        public string Isbn { get; set; }

        private List<ElasticOwnershipIndex> _elasticOwnership = new List<ElasticOwnershipIndex>();
        public List<ElasticOwnershipIndex> ElasticOwnership
        {
            get { return _elasticOwnership; }
            set { _elasticOwnership = value; }
        }

    }


    [Serializable]
    public class BulkElasticOwnershipIndexCollection
    {
        private List<ElasticOwnershipIndexCollection> _elasticOwnershipCollections = new List<ElasticOwnershipIndexCollection>();
        public List<ElasticOwnershipIndexCollection> ElasticOwnershipCollections
        {
            get { return _elasticOwnershipCollections; }
            set { _elasticOwnershipCollections = value; }
        }
    }
}
