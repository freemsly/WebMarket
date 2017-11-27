// <copyright company="Recorded Books, Inc" file="Contributor.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public  class Contributor
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("order")]
        public int Order { get; set; }

        [XmlAttribute("fn")]
        public string FirstName { get; set; }

        [XmlAttribute("ln")]
        public string LastName { get; set; }

        [XmlAttribute("mail")]
        public string MailName { get; set; }

        [XmlAttribute("organisation")]
        public string Organisation { get; set; }

        public string SourceId { get; set; }
    }
}
