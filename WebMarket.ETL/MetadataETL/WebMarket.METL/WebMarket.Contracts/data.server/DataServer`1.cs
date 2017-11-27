// <copyright company="Recorded Books Inc" file="DataServer1.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System;
    using System.Collections;
    using System.Collections.Generic;


    public abstract class DataServer<T> : IDataServer<ProcessItem<T>> where T : class, new()
    {

        bool IDataServer<ProcessItem<T>>.Initialize()
        {
            return Initialize();
        }

        void IDataServer<ProcessItem<T>>.Cleanup()
        {
            Cleanup();
        }

        IEnumerator<ProcessItem<T>> IEnumerable<ProcessItem<T>>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        protected virtual bool Initialize()
        {
            return true;
        }

        protected void Cleanup()
        {

        }

        public abstract IEnumerator<ProcessItem<T>> GetEnumerator();

    }
}
