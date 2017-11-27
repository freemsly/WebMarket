// <copyright company="Recorded Books, Inc" file="Narrator.cs">
// Copyright © 2014 All Right Reserved
// </copyright>

using System;

namespace WebMarket.Model
{
    using WebMarket.Common;
    [Serializable]
    public class Narrator : ListItem
    {

        private string facet = Constants.Facets.Narrator;

        public string Facet
        {
            get { return facet; }
            set { facet = value; }
        }
    }
}
