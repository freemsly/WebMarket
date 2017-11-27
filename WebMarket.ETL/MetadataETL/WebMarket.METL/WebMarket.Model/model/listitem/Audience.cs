// <copyright company="Recorded Books Inc" file="Audience.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>



namespace WebMarket.Model
{
    using System;
    using WebMarket.Common;

    [Serializable]
    public class Audience : ListItem
    {
        private string facet = Constants.Facets.Audience;
        public string Facet { get { return facet; } set { facet = value; } }

    }
}
