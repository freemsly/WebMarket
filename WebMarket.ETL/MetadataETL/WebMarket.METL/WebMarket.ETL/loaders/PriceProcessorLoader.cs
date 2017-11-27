// <copyright company="Recorded Books Inc" file="PriceProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System;
using System.CodeDom;
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

    public class PriceProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public Dictionary<string, IEnumerable<Pricing>> SourceData { get; set; }
        public override string FacetToken
        {
            get { return Constants.Facets.Price; }
        }

        public override IProcessor<MediaTitle> Load()
        {
            var processor = new PriceProcessor {SourceData = SourceData};
            return processor;
        }

        public override bool Initialize()
        {
            using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
            //using (SqlConnection cn = SqlConnectionProvider.GetConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandTimeout = 3200;
                    InitializeCommand(cmd);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        BorrowReader(reader);
                    }
                    IsInitialized = true;
                }
            }
            return IsInitialized;
        }
        public void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_Pricing");
        }

        private void BorrowReader(SqlDataReader reader)
        {
            SourceData = new Dictionary<string, IEnumerable<Pricing>>();
            ICollection<IsbnPricing> list = new Collection<IsbnPricing>();
            while (reader.Read())
            {
                var item = new IsbnPricing();
                item.Isbn = String.Intern(reader.GetString(reader.GetOrdinal("ISBN")));
                item.Currency = String.Intern(reader.GetString(reader.GetOrdinal("Currency")));
                item.Price = new Price();
                if (!reader.IsDBNull(reader.GetOrdinal("ListPrice")))
                {
                    item.Price.List = reader.GetDecimal(reader.GetOrdinal("ListPrice"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("RetailPrice")))
                {
                    item.Price.Retail = reader.GetDecimal(reader.GetOrdinal("RetailPrice"));
                }
                if (!reader.IsDBNull(reader.GetOrdinal("DiscountPrice")))
                {
                    item.Price.Discount = reader.GetDecimal(reader.GetOrdinal("DiscountPrice"));
                }
                list.Add(item);
            }

            foreach (var item in list.ToLookup(t => t.Isbn))
            {
                var pricingList = item.Select(t => new Pricing
                {
                    Price = t.Price,
                    Currency = t.Currency
                }).ToList();
                SourceData.Add(item.Key, pricingList);
            }
        }
        private class IsbnPricing : Pricing
        {
            public string Isbn { get; set; }
        }
    }
}
