// <copyright company="Recorded Books, Inc" file="Author.cs">
// Copyright © 2014 All Right Reserved
// </copyright>


using System;

namespace WebMarket.Model
{
    using WebMarket.Common;
    [Serializable]
    public class Author : ListItem
    {
        #region properties

        #region Facet (string)

        private string facet = Constants.Facets.Author;

        public string Facet
        {
            get { return facet; }
            set { facet = value; }
        }

        #endregion

        #endregion
    }
}
