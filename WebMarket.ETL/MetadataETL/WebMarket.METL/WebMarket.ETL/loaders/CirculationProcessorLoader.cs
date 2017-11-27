// <copyright company="Recorded Books Inc" file="CirculationProcessorLoader.cs">
// Copyright © 2017 All Rights Reserved
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

    public class CirculationProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public IDictionary<string, IEnumerable<IsbnCirculation>> Data { get; set; }
        public override string FacetToken => "circulation";

        public override bool Initialize()
        {
           // using (SqlConnection cn = SqlConnectionProvider.GetConnection("metl.destination.oneclick"))
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
            var processor = new CirculationProcessor();
            processor.SourceData = Data;
            return processor;
        }

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("OneClick_Circulation");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<IsbnCirculation>>();
            ICollection<IsbnCirculation> list = new Collection<IsbnCirculation>();
            while (reader.Read())
            {
                IsbnCirculation item = new IsbnCirculation();
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

        public class IsbnCirculation : Circulation
        {
            public string Isbn { get; set; }
        }

    }

    
}
