// <copyright company="Recorded Books Inc" file="ContentAdvisoryProcessorLoader.cs">
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
using WebMarket.Model.model;

namespace WebMarket.ETL
{

    public class ContentAdvisoryProcessorLoader : ProcessorLoader<MediaTitle>
    {
        private readonly ConnectionString connectionString;
        public IDictionary<string, IEnumerable<ContentAdvisory>> Data { get; set; }
        public override string FacetToken
        {
            get { return Constants.Facets.ContentAdvisory; }
        }

        public override bool Initialize()
        {

            using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
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
            var processor = new ContentAdvisoryProcessor();

            processor.SourceData = Data;
            return processor;
        }

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_ContentAdvisory");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<ContentAdvisory>>();
            var contentAdvisoryList = new Collection<IsbnContentAdvisory>();
            while (reader.Read())
            {
                var item = new IsbnContentAdvisory();
                item.Isbn = reader.GetString(0);
                item.Sex = reader.GetString(1);
                item.Language = reader.GetString(2);
                item.Violence = reader.GetString(3);
                contentAdvisoryList.Add(item);   
            }

            var groupedItems = contentAdvisoryList.GroupBy(t => t.Isbn);
            foreach (var i in groupedItems)
            {
                Data.Add(i.Key, i);   
            }
        }

        public class IsbnContentAdvisory : ContentAdvisory
        {
            public string Isbn { get; set; }
        }
    }

    
}
