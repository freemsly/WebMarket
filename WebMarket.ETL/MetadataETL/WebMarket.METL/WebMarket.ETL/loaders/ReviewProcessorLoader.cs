// <copyright company="Recorded Books Inc" file="ReviewProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using System.Collections.Generic;
using System.Linq;
using WebMarket.Contracts;
using WebMarket.ETL.configuration;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Data.SqlClient;

    public class ReviewProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public Dictionary<string, IEnumerable<Review>> Data { get; set; }
        public override string FacetToken => "Review";

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
                    IsInitialized = true;
                }
            }
            return IsInitialized;
        }

        public override IProcessor<MediaTitle> Load()
        {
            var processor = new ReviewProcessor();
            processor.SourceData = Data;
            return processor;
        }

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_Review");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            var list = new Collection<Review>();
            Data = new Dictionary<string, IEnumerable<Review>>();
            while (reader.Read())
            {
                var item = new Review();
                item.Isbn = !reader.IsDBNull(0) ? reader.GetString(0) : string.Empty;
                item.Data = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                list.Add(item);
            }
            var groupedItems = list.GroupBy(t => t.Isbn);
            foreach (var review in groupedItems)
            {
                Data.Add(review.Key, review);
            }
        }

        public class Review
        {
            public string Isbn { get; set; }
            public string Data { get; set; }
        }

    }

    
}
