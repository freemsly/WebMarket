// <copyright company="Recorded Books Inc" file="MediaType.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>



namespace WebMarket.Model
{
    using System;
    using WebMarket.Common;

    [Serializable]
    public class MediaType : ListItem
    {
        #region properties

        #region Facet (string)

        private string facet = Constants.Facets.MediaType;

        public string Facet
        {
            get { return facet; }
            set { facet = value; }
        }

        #endregion

        #endregion
    }
}
