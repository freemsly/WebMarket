// <copyright company="Recorded Books Inc" file="Language.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>



namespace WebMarket.Model
{
    using WebMarket.Common;
    using System;

    [Serializable]
    public class Language : ListItem
    {
        #region properties

        #region Facet (string)

        private string facet = Constants.Facets.Language;

        public string Facet
        {
            get { return facet; }
            set { facet = value; }
        }

        #endregion

        #endregion
    }

}
