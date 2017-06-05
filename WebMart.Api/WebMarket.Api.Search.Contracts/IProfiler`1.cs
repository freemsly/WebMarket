// <copyright company="Recorded Books, Inc" file="IProfiler.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Api.Search.Contracts
{
    public interface IProfiler
    {
        void Publish<T>(Query<T> query) where T : class, new();
    }
}
