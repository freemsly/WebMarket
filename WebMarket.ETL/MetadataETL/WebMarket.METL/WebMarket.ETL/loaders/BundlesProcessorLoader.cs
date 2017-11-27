// <copyright company="Recorded Books Inc" file="BundlesProcessorLoader.cs">
// Copyright © 2017 All Rights Reserved
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

    public class BundlesProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public IDictionary<string, IsbnBundles> Data { get; set; }
        public override string FacetToken => "bundles";
        
        public override bool Initialize()
        {
            
           using (SqlConnection cn = new SqlConnection( EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
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

        public override IProcessor<MediaTitle> Load()
        {
            var processor = new BundlesProcessor();

            processor.SourceData = Data;
            return processor;
        }

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_Bundles");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IsbnBundles>();
            var bundlesList = new Collection<IsbnBundles>();
            while (reader.Read())
            {
                IsbnBundles item = new IsbnBundles();
                item.Isbn = reader.GetString(0);
                item.Id = reader.GetString(1);
                item.Price = reader.GetDecimal(2);
                item.ItemNumber = reader.GetString(3);
                item.Name = reader.GetString(4);
                bundlesList.Add(item);   
            }
            
            foreach (var bundle in bundlesList.ToLookup(t => t.Isbn))
            {
                Data.Add(bundle.Key, bundle.FirstOrDefault()); 
            }

        }

        public class IsbnBundles : Bundle
        {
            public string Isbn { get; set; }
        }
    }

    
}
