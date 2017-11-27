// <copyright company="Recorded Books Inc" file="CountryCode.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class CountryCode
    {
        [XmlText]
        public string countryCode { get; set; }
    }
}
