// <copyright company="Recorded Books Inc" file="IDataServer`1.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System.Collections.Generic;

    public interface IDataServer<T> : IEnumerable<T> where T : class, new()
    {
        bool Initialize();

        void Cleanup();

    }
}
