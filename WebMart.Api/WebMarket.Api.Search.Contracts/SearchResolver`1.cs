// <copyright company="Recorded Books, Inc" file="SearchResolver_1.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Api.Search.Contracts
{
    public abstract class SearchResolver<T> : IQueryResolver<T> where T : class, new()
    {
        void IQueryResolver<T>.Resolve(Query<T> query)
        {
            Resolve(query);
        }

        public abstract void Resolve(Query<T> query);

    }
}