// <copyright company="Recorded Books Inc" file="AudienceProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Model;

namespace WebMarket.ETL
{

    public class AudienceProcessor : SimplePropertyProcessor
    {
        protected override string PropertyToken
        {
            get { return Constants.Facets.Audience; }
        }

        protected override object GetTokenValue(MediaTitle item)
        {
            return item.Audience;
        }
    }
}
