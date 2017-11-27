// <copyright company="Recorded Books Inc" file="CountryCode.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class ContentAdvisory
    {
        [XmlAttribute("sex")]
        public string Sex { get; set; }

        [XmlAttribute("language")]
        public string Language { get; set; }

        [XmlAttribute("violence")]
        public string Violence { get; set; }
    }
}
