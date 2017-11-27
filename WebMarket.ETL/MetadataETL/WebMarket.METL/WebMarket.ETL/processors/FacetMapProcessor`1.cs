// <copyright company="Recorded Books Inc" file="FacetMapProcessor_1.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;

    public abstract class FacetMapProcessor<T> : Processor<T> where T : class, new()
    {
        private List<FacetMap> facetMaps = new List<FacetMap>();
        public List<FacetMap> FacetMaps { get { return facetMaps; } set { facetMaps = value; } }
    }
}
