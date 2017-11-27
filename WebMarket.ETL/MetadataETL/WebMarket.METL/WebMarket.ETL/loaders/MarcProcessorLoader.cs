// <copyright company="Recorded Books Inc" file="ImagesProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System.Collections.ObjectModel;
using System.Linq;
using WebMarket.Contracts;
using WebMarket.ETL.configuration;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class MarcProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public Dictionary<string, IEnumerable<Marcs>> Data { get; set; }

        public override string FacetToken
        {
            get { return "marc"; }
        }

        public override bool Initialize()
        {
            using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.ingestion))
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
            var processor = new MarcProcessor {SourceData = Data};
            return processor;
        }


        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Ingestion_Marc");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<Marcs>>();
            var list = new Collection<Marcs>();
            while (reader.Read())
            {
                Marcs item = new Marcs();
                item.ProductNumber = reader.GetString(0);
                item.HasMarc = reader.GetBoolean(1);
                item.FileName = reader.GetString(2);
                list.Add(item);
            }
            foreach (var item in list.ToLookup(t => t.ProductNumber))
            {
                Data.Add(item.Key, item);
            }
        }

        public class Marcs
        {
            public string ProductNumber { get; set; }
            public bool HasMarc { get; set; }
            public string FileName { get; set; }
        }
    }
}

