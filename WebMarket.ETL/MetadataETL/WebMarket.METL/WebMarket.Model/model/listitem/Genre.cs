// <copyright company="Recorded Books, Inc" file="Genre.cs">
// Copyright © 2014 All Right Reserved
// </copyright>


using System;

namespace WebMarket.Model
{
    using WebMarket.Common;
    [Serializable]
    public class Genre : ListItem
    {

        private string facet = Constants.Facets.Genre;

        public string Facet
        {
            get { return facet; }
            set { facet = value; }
        }
    }
}
