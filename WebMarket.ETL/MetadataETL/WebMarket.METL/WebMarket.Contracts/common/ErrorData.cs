// <copyright company="Recorded Books Inc" file="ErrorData.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System;
    public class ErrorData
    {
        private string facetToken;
        public string FacetToken
        {
            get { return facetToken; }
            set { facetToken = value; }
        }

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        private string data;
        public string Data
        {
            get { return data; }
            set { data = value; }
        }

        public ErrorData(string token, string errorMessage)
            : this(token, errorMessage, String.Empty) { }

        public ErrorData(string token, string errorMessage, string errorData)
        {
            facetToken = token;
            message = errorMessage;
            data = errorData;
        }
    }

}
