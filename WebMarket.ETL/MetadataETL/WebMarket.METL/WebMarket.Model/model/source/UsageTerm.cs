// <copyright company="Recorded Books Inc" file="Bisac.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class UsageTerm
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

    }
}
