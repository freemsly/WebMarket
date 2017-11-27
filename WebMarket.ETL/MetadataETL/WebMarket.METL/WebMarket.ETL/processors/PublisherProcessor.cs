// <copyright company="Recorded Books Inc" file="PublisherProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Model;

namespace WebMarket.ETL
{
    public class PublisherProcessor : SimplePropertyProcessor
    {
        protected override string PropertyToken
        {
            get { return Constants.Facets.Publisher; }
        }

        protected override object GetTokenValue(MediaTitle item)
        {
            return item.Publisher;
        }
    }
}
