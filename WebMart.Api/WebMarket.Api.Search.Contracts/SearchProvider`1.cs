// <copyright company="Recorded Books, Inc" file="SearchProvider_1.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


namespace WebMarket.Api.Search.Contracts
{
    using System.Collections.Specialized;

    public abstract class SearchProvider<T> : ISearchProvider<T> where T : class, new()
    {

        private IQueryResolver<T> _Resolver = null;
        public IQueryResolver<T> Resolver
        {
            get
            {
                return _Resolver;
            }
            set
            {
                _Resolver = value;
            }
        }

        #region Profiler (IProfileQuery)

        private IProfiler _Profiler;

        /// <summary>
        /// Gets or sets the IProfileQuery value for Profiler
        /// </summary>
        /// <value> The IProfileQuery value.</value>

        public IProfiler Profiler
        {
            get { return _Profiler; }
            set
            {
                if (_Profiler != value)
                {
                    _Profiler = value;
                }
            }
        }
        

        /// <summary>
        /// Gets or sets the IProfileQuery value for Profiler
        /// </summary>
        /// <value> The IProfileQuery value.</value>

        #endregion



        void ISearchProvider<T>.ExecuteQuery(Query<T> query)
        {
            throw new System.NotImplementedException();
        }

        

        public virtual void ResolveQuery(Query<T> query)
        {
            _Resolver.Resolve(query);
        }


        protected void ExecuteQuery(Query<T> query)
        {
            ResolveQuery(query);
        }

        public Query<T> ExecuteQuery(NameValueCollection nvc)
        {
            return null;
        }


    }
}

