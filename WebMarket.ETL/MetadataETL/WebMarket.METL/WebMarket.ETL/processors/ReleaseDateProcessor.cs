// <copyright company="Recorded Books Inc" file="ReleaseDateProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;

    public class ReleaseDateProcessor : SimplePropertyProcessor
    {
        protected override string PropertyToken
        {
            get { return Constants.Facets.ReleaseDate; }
        }

        protected override object GetTokenValue(MediaTitle item)
        {
            return item.EditionPublicationDate > DateTime.MinValue ? item.EditionPublicationDate : item.TitlePublicationDate;
        }
    }
}
