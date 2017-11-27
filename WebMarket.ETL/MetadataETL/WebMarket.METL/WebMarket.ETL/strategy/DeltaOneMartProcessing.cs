// <copyright company="Recorded Books Inc" file="DeltaOneMartProcessing.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public  class DeltaOneMartProcessing : IOneMartProcessing
    {
        public List<ProcessorLoader<MediaTitle>> Get()
        {
            return new List<ProcessorLoader<MediaTitle>>()
            {
                new TitleProcessorLoader(),
                new HoldsProcessorLoader(),
                new CirculationProcessorLoader(),
                new OwnershipProcessorLoader(),
                new MergeOwnershipProcessorLoader()
            };
        }
    }

}
