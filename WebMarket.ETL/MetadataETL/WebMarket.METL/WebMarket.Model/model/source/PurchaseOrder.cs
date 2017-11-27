// <copyright company="Recorded Books, Inc" file="PurchaseOrder.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

using System.Collections.Generic;

namespace WebMarket.Model
{
    using System;

    [Serializable]
    public class PurchaseOrder
    {
        public int AccountNo { get; set; }
        public int OrderNo { get; set; }
        public int InvoiceNo { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string ItemNo { get; set; }
        public string ISBN { get; set; }
        public int LibraryId { get; set; }
        public int Quantity { get; set; }
        public Decimal NetAmount { get; set; }
        public DateTime CreatedAt { get; set; }

        
    }
    [Serializable]
    public class BulkPurchaseOrder
    {
        private List<PurchaseOrder> _purchaseOrders = new List<PurchaseOrder>();
        public List<PurchaseOrder> PurchaseOrders
        {
            get { return _purchaseOrders; }
            set { _purchaseOrders = value; }
        }

    }
    
}
