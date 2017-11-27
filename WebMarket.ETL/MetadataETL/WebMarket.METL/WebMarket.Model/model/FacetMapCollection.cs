// <copyright company="Recorded Books Inc" file="FacetMapCollection.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class FacetMapCollection : KeyedCollection<string, FacetMap>
    {
        protected override string GetKeyForItem(FacetMap item)
        {
            return String.Format("{0}.{1}", item.FacetToken, item.FacetValue);
        }
        public void AddRange(IEnumerable<FacetMap> list)
        {
            foreach (var item in list)
            {
                string key = String.Format("{0}.{1}", item.FacetToken, item.FacetValue);
                if (!Contains(key))
                {
                    Add(item);
                }

            }
        }
    }
}
