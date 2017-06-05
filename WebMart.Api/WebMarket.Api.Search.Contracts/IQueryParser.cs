// <copyright company="Recorded Books, Inc" file="IQueryParser.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Api.Search.Contracts
{
    using System.Collections.Specialized;

    public interface IQueryParser<T> where T : class, new()
    {
        // this is intended for parsing a url
        Query<T> Parse(string tokens, NameValueCollection filters);

        Query<T> Parse(NameValueCollection formFields);
    }
}
