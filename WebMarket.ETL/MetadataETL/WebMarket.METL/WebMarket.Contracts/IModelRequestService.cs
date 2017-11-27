using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace WebMarket.Contracts
{
    public interface IModelRequestService
    {
        IResponse<T> ExecuteAction<T, U>(T model) where T : class, new();
        IResponse<T> Get<T>() where T : class, new();
        IResponse<T> GetAll<T>() where T : class, new();
        IResponse<T> Post<T>(T model) where T : class, new();
    }

    public interface IResponse<T> : IEnumerable<T>, IEnumerable
    {
        bool IsOkay { get; }
        T Model { get; }
        string Status { get; }
    }
}
