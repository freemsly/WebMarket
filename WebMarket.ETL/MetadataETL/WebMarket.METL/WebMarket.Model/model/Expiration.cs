// <copyright company="Recorded Books, Inc" file="Expiration.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


namespace WebMarket.Model
{
    using System;

    [Serializable]
    public  class Expiration
    {
        //here it is platform tenant id, this will be changed to entity id in future
        public int TenantId { get; set; }

        public string Isbn { get; set; }
        public DateTime ExpiryOn { get; set; }
    }
}
