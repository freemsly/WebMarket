// <copyright company="Recorded Books Inc" file="SimplePropertyCollection.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System.Collections.ObjectModel;

    public class SimplePropertyCollection : KeyedCollection<string, TypedItem>
    {
        protected override string GetKeyForItem(TypedItem item)
        {
            return item.Key;
        }

        public T GetValue<T>(string key)
        {
            T t = default(T);
            if (Contains(key) && this[key].Value != null && this[key].Value.GetType().Equals(typeof(T)))
            {
                t = (T)this[key].Value;
            }
            return t;
        }
    }
}
