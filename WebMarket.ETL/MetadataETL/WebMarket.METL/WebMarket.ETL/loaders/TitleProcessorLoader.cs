// <copyright company="Recorded Books Inc" file="TitleProcessorLoader.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{


    public sealed class TitleProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public override string FacetToken
        {
            get { return Constants.Facets.Title; }
        }

        public override bool Initialize()
        {
            IsInitialized = true;
            return IsInitialized;
        }

        public override IProcessor<MediaTitle> Load()
        {
            return new TitleProcessor();
        }
    }
}
