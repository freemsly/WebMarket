// <copyright company="Recorded Books, Inc" file="ElasticOrderHistory.cs">
// Copyright © 2016 All Right Reserved
// </copyright>


using Nest;

namespace WebMarket.Model
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    [ElasticsearchType(Name = "elasticorderhistory")]
    public class ElasticOrderHistory
    {
        public string AccountNo { get; set; }
     
        public int OrderNo { get; set; }
     
        public int InvoiceNo { get; set; }
     
        public int EntityId { get; set; }
     
        public DateTime OrderDate { get; set; }
     
        public DateTime InvoiceDate { get; set; }
     
        public string ItemNo { get; set; }
     
        public string ISBN { get; set; }
     
        public int LibraryId { get; set; }
     
        public int Quantity { get; set; }
     
        public Decimal NetAmount { get; set; }
     
        public string Format { get; set; }
     
        public string OrderMethod { get; set; }


    }

    [Serializable]
    public class BulkElasticOrderHistory
    {
        private List<ElasticOrderHistory> _orderHistories = new List<ElasticOrderHistory>();
        public List<ElasticOrderHistory> OrderHistories
        {
            get { return _orderHistories; }
            set { _orderHistories = value; }
        }
    }
}
