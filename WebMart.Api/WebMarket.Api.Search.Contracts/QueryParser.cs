// <copyright company="Recorded Books, Inc" file="QueryParser.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using System.Collections.Generic;

namespace WebMarket.Api.Search.Contracts
{
    using System;
    using System.Collections.Specialized;

    public class QueryParser<T> : IQueryParser<T> where T : class, new()
    {
        Query<T> IQueryParser<T>.Parse(string tokens, NameValueCollection filters)
        {
            return Parse(tokens, filters);
        }

        Query<T> IQueryParser<T>.Parse(NameValueCollection formFields)
        {
            return Parse(formFields);
        }

        public virtual Query<T> Parse(NameValueCollection formFields)
        {

            Query<T> item = new Query<T> { Source = "webmarket.api" };
            item.PageSize = 10;
            item.PageIndex = 0;

            if (formFields != null)
            {
                foreach (KeyValuePair<string, string> field in formFields)
                {
                    item.Criterion.Add(field);
                }
            }
            return item;
        }

        public virtual Query<T> Parse(string tokens, NameValueCollection nvc)
        {
            Query<T> item = new Query<T>();
            string[] t = tokens.Split(new char[]{'/'}, StringSplitOptions.RemoveEmptyEntries);
            if (t.Length > 0)
            {
                item.Criterion.Add(new KeyValuePair<string, string>(t[0], t[1]));
            }
         
            return Parse(nvc);
        }

    }
}
