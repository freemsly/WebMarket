// <copyright company="Recorded Books, Inc" file="ImageUrl.cs">
// Copyright © 2015 All Right Reserved
// </copyright>

using System.Runtime.Serialization;

namespace WebMarket.Api.Model
{
    using System;
    
    [DataContract]
    public class ImageUrl
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }
    }
}
