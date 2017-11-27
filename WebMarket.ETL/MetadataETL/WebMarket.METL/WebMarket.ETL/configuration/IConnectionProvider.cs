using System;
using System.Collections.Generic;
using System.Text;
using WebMarket.Model.model;

namespace WebMarket.ETL.configuration
{
    public interface IConnectionProvider
    {
        bool IsNoSql { get; set; }
        ConnectionString Get();
    }
}
