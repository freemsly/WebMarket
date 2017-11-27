// <copyright company="Recorded Books Inc" file="ImageUrl.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Common
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]
    public class ImageUrl
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Url { get; set; }
    }
}
