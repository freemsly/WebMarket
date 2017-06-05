// <copyright company="Recorded Books, Inc" file="ApiProfiler.cs">
// Copyright © 2017 All Right Reserved
// </copyright>

using System;
using WebMarket.Api.Search.Contracts;

namespace WebMarket.Api.Infrastructure.Common
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ApiProfiler : IProfiler
    {
        private void Publish<T>(Query<T> query) where T : class, new()
        {
            KpiPublisher.Publish<T>(query);
        }

        void IProfiler.Publish<T>(Query<T> query)
        {
            Publish(query);
        }
    }
}
