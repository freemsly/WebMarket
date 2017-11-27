// <copyright company="Recorded Books Inc" file="IProcessingStrategy.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public interface IProcessingStrategy<T> where T : class, new()
    {
        bool Initialize();
        void ExecuteStrategy();
        void Cleanup();
    }
}
