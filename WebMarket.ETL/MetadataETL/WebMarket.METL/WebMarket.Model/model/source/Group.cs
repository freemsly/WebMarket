// <copyright company="Recorded Books Inc" file="Bisac.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Group
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("rank")]
        public int Rank { get; set; }
    }
}
