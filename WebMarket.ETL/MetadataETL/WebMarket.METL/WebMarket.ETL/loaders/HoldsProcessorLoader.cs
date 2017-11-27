// <copyright company="Recorded Books Inc" file="HoldsProcessorLoader.cs">
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

    public class HoldsProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public IDictionary<string,IEnumerable<IsbnHolds>> Data { get; set; }
        public override string FacetToken
        {
            get { return "holds"; }
        }

        public override bool Initialize()
        {
            //using (SqlConnection cn = SqlConnectionProvider.GetConnection("metl.destination.oneclick"))
            using (SqlConnection cn =  new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.dpcore))
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
            var processor = new HoldsProcessor();
            processor.SourceData = Data;
            return processor;
        }

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("OneClick_TotalHolds");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<IsbnHolds>>();
            ICollection<IsbnHolds> list = new Collection<IsbnHolds>();
            while (reader.Read())
            {
                IsbnHolds item = new IsbnHolds();
                item.Isbn = String.Intern(reader.GetString(0));
                item.ScopeId = reader.GetInt32(1);
                item.Count = reader.GetInt32(2);
                list.Add(item);
            }
            foreach (var holds in list.ToLookup(t => t.Isbn))
            {
                Data.Add(holds.Key, holds.ToList());
            }
        }

        public class IsbnHolds : Holds
        {
            public string Isbn { get; set; }
        }

    }

    
}
