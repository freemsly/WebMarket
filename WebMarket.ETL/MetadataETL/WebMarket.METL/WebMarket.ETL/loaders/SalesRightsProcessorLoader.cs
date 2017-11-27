// <copyright company="Recorded Books Inc" file="SalesRightsProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using WebMarket.Contracts;
using WebMarket.ETL.configuration;
using WebMarket.Model;

namespace WebMarket.ETL
{

    public class SalesRightsProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public IDictionary<string, IEnumerable<string>> Data { get; set; }
        public override string FacetToken
        {
            get { return Constants.Facets.SalesRights; }
        }

        public override bool Initialize()
        {
            using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
            //using (SqlConnection cn = SqlConnectionProvider.GetConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    InitializeCommand(cmd);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        BorrowReader(reader);
                    }
                    IsInitialized = Data.Keys.Count > Constants.TotalProductsCount;
                }
            }
            return IsInitialized;
        }

        public override IProcessor<MediaTitle> Load()
        {
            var processor = new SalesRightsProcessor();

            processor.SourceData = Data;
            return processor;
        }

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_SalesRights");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<string>>();
            var salesRightsList = new Collection<IsbnSalesRights>();
            while (reader.Read())
            {
                IsbnSalesRights item = new IsbnSalesRights();
                item.Isbn = reader.GetString(0);
                item.CountryCode = reader.GetString(1);
                salesRightsList.Add(item);   
            }
            
            foreach (var salesRights in salesRightsList.ToLookup(t => t.Isbn))
            {
                var countryCodes = salesRights.Select(item => item.CountryCode);
                Data.Add(salesRights.Key, countryCodes);   
            }

        }

        public class IsbnSalesRights
        {
            public string Isbn { get; set; }
            public string  CountryCode { get; set; }
        }
    }

    
}
