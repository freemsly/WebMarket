// <copyright company="Recorded Books, Inc" file="Subscription.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

using System;
using System.Collections.Generic;
using Nest;

namespace WebMarket.Model
{

    public class Subscription
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class SubscriptionOwnership : Subscription
    {
        public string Isbn { get; set; }
        public int ScopeId  { get; set; }

        public bool IsCurrent => EndDate <= DateTime.Now;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
