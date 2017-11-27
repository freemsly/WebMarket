// <copyright company="Recorded Books, Inc" file="Series.cs">
// Copyright © 2014 All Right Reserved
// </copyright>


using System;

namespace WebMarket.Model
{
    using WebMarket.Common;
    [Serializable]
    public class Series : ListItem
    {
        #region Count (int)

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        #endregion

        #region Facet (string)

        private string facet = Constants.Facets.Series;

        public string Facet
        {
            get { return facet; }
            set { facet = value; }
        }

        #endregion
    }
}
