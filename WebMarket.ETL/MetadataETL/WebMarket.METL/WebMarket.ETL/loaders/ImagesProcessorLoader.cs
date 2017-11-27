// <copyright company="Recorded Books Inc" file="ImagesProcessorLoader.cs">
// Copyright © 2017 All Rights Reserved
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
    

    public class ImagesProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public Dictionary<string, IEnumerable<IsbnImages>> Data { get; set; }

        public override string FacetToken
        {
            get { return Constants.ImageUrls; }
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
            var processor = new ImagesProcessor {SourceData = Data};
            return processor;
        }


        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Ingestion_Images");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<IsbnImages>>();
            var list = new Collection<IsbnImages>();
            while (reader.Read())
            {
                IsbnImages item = new IsbnImages();
                item.Isbn = reader.GetString(0);
                item.S3FolderName = reader.GetString(1);
                item.HasImages = reader.GetBoolean(2);
                list.Add(item);
            }
            foreach (var item in list.ToLookup(t => t.Isbn))
            {
                Data.Add(item.Key, item);
            }
        }

        public class IsbnImages
        {
            public string Isbn { get; set; }
            public string S3FolderName { get; set; }
            public bool HasImages { get; set; }
        }
    }
}

