// <copyright company="Recorded Books Inc" file="IProcessor`1.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{

    public interface IProcessor<T> where T : class, new()
    {
        bool Initialize();
        void Execute(ProcessItem<T> item);
        void Cleanup();
        void SetSuccessor(IProcessor<T> successor);
    }
}
