// <copyright company="Recorded Books, Inc" file="IQueryResolver.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Api.Search.Contracts
{
    public interface IQueryResolver<T> where T : class, new()
    {
        void Resolve(Query<T> query);
    }
}
