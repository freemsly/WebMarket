// <copyright company="Recorded Books Inc" file="FullOneMartProcessing.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.ETL
{
    using  WebMarket.Contracts;
    using WebMarket.Model;
    using System.Collections.Generic; 

    public class FullOneMartProcessing : IOneMartProcessing
    {
        public List<ProcessorLoader<MediaTitle>> Get()
        {
            return new List<ProcessorLoader<MediaTitle>>()
            {
                new PopularKeywordsProcessorLoader(),
                new AudienceProcessorLoader(),
                new AuthorsProcessorLoader(),
                new DescriptionProcessorLoader(),
                new FictionNonFictionProcessorLoader(),
                new GenresProcessorLoader(),
                new LanguageProcessorLoader(),
                new MediaTypeProcessorLoader(),
                new ImagesProcessorLoader(),
                new NarratorsProcessorLoader(),
                new PublicationDateProcessorLoader(),
                new PublisherProcessorLoader(),
                new ReleaseDateProcessorLoader(),
                new SOPProcessorLoader(),
                new TitleProcessorLoader(),
                new SeriesProcessorLoader(),
                new ReviewProcessorLoader(),
                new SalesRightsProcessorLoader(),
                new PriceProcessorLoader(),
                new SubscriptionProcessorLoader(),
                new MarcProcessorLoader(),
                new BundlesProcessorLoader(),
                new PlatformMetadataProcessorLoader(),
                new HoldsProcessorLoader(),
                new CirculationProcessorLoader(),
                new ExpirationProcessorLoader(),
                new OwnershipProcessorLoader(),
                new MergeProcessorLoader()
            };
        }
    }

}
