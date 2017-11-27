// <copyright company="Recorded Books Inc" file="PublicationDateProcessorLoader.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    public class PublicationDateProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public override string FacetToken
        {
            get { return Constants.Facets.PublishedDate; }
        }

        public override bool Initialize()
        {
            IsInitialized = true;
            return IsInitialized;
        }

        public override IProcessor<MediaTitle> Load()
        {
            return new PublicationDateProcessor();
        }
    }
}
