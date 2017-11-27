// <copyright company="Recorded Books Inc" file="Builder`1.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    public abstract class Builder<T,U> where T : class, new()
    {
        protected ProcessItem<T> ProcessItem { get; set; }

        public abstract bool Validate();

        public abstract U Build();
    }
}
