// <copyright company="Recorded Books, Inc" file="TitleIndex.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public  class Facets
    {
        public string publisher { get; set; }
        public string mediatype { get; set; }
        public string id { get; set; }
        public string language { get; set; }
        public string audience { get; set; }
        public List<string> author { get; set; }
        public List<string> genre { get; set; }
        public List<string> narrator { get; set; }
        public string series { get; set; }
        public bool hasdrm { get; set; }
        public bool isfiction { get; set; }
        public string title { get; set; }
        public bool hasattachments { get; set; }
    }
}