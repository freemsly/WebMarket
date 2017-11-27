// <copyright company="Recorded Books Inc" file="ProcessItem.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public class ProcessItem<T> : IDisposable where T : class, new()
    {
        public bool IsCancelled { get; set; }

        public T Model { get; set; }

        public TokenPropertyCollection TokenProperties { get; set; }

        public SimplePropertyCollection SimpleProperties { get; set; }
        
        public List<ErrorData> Errors { get; private set; }


        public void AddError(string token, string errorMessage)
        {
            AddError(token, errorMessage, String.Empty);
        }

        public void AddError(string token, string errorMessage, string errorData)
        {
            if (Errors == null)
            {
                Errors = new List<ErrorData>();
            }
            Errors.Add(new ErrorData(token, errorMessage, errorData));
        }

        void IDisposable.Dispose()
        {
            if (TokenProperties != null)
            {
                TokenProperties.Clear();
            }
            if (SimpleProperties != null)
            {
                SimpleProperties.Clear();
            }
            if (Model != null)
            {
                Model = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
