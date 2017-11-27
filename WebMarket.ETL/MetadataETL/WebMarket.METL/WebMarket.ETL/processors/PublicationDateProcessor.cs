// <copyright company="Recorded Books Inc" file="PublicationDateProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Model;

namespace WebMarket.ETL
{

    public class PublicationDateProcessor : SimplePropertyProcessor
    {
        protected override string PropertyToken
        {
            get { return Constants.Facets.PublishedDate; }
        }

        protected override object GetTokenValue(MediaTitle item)
        {
            return item.TitlePublicationDate; // this is the original publication date;
        }
    }
}
