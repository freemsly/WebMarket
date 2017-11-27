// <copyright company="Recorded Books Inc" file="CirculationProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System;
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

    public class ExpirationProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public IDictionary<string, IEnumerable<Expiration>> Data { get; set; }
        public override string FacetToken => "expiration";

        public override bool Initialize()
        {
            using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.dpcore))
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
            var processor = new ExpirationProcessor();
            processor.SourceData = Data;
            return processor;
        }

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Platform_Expiration");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<Expiration>>();
            ICollection<Expiration> list = new Collection<Expiration>();
            while (reader.Read())
            {
                Expiration item = new Expiration();
                item.TenantId = reader.GetInt32(0);
                item.Isbn = String.Intern(reader.GetString(1));
                item.ExpiryOn = reader.GetDateTime(2);
                list.Add(item);
            }
            foreach (var holds in list.ToLookup(t => t.Isbn))
            {
                Data.Add(holds.Key, holds.ToList());
            }
        }

    }

    
}
