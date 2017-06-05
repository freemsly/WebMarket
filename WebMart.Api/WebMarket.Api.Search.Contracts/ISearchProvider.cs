// <copyright company="Recorded Books, Inc" file="ISearchProvider.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Api.Search.Contracts
{

    public interface ISearchProvider<T> where T : class, new()
    {
        IQueryResolver<T> Resolver { get; set; }

        void ExecuteQuery(Query<T> query);
    }
}
