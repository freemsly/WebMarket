// <copyright company="Recorded Books Inc" file="Bisac.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Bisac
    {
        [XmlAttribute("code")]
        public string Code { get; set; }

        [XmlAttribute("order")]
        public int Order { get; set; }

        [XmlAttribute("genres")]
        public string Genres { get; set; }
    }
}
