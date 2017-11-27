// <copyright company="Recorded Books Inc" file="TokenPropertyCollection.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Common;

namespace WebMarket.Contracts
{
    using System.Collections.Generic;
    using System.Linq;

    public class TokenPropertyCollection : List<ListItem>
    {
        public bool ContainsAny<T>() where T : ListItem
        {
            var found = this.Find(x => x.GetType().Equals(typeof(T)));
            return found != null;
        }

        public T Get<T>() where T : ListItem
        {
            var found = Find(x => x.GetType().Equals(typeof(T)));
            return found as T;
        }

        public IEnumerable<T> GetAll<T>()
        {
            var q = FindAll(x => x.GetType().Equals(typeof(T))).OrderBy(x => x.Order).ToList();
            return q as List<T>;
        }
    }
}
