// <copyright company="Recorded Books, Inc" file="Holds.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


namespace WebMarket.Model
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
