// <copyright company="Recorded Books Inc" file="SOPProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.ETL.configuration;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
 
    public class SOPProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public Dictionary<string, IEnumerable<SOPMetadata>> Data { get; set; }
        public override string FacetToken => "SOP";

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
            var processor = new SOPProcessor();
            processor.SourceData = Data;
            return processor;
        }

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_SOP");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<SOPMetadata>>();
            var list = new Collection<SOPMetadata>();
            while (reader.Read())
            {
                SOPMetadata item = new SOPMetadata();
                item.Id = reader.GetInt32(0).ToString();
                item.Name = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                item.Isbn = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty;
                item.Year = !reader.IsDBNull(4)? reader.GetInt32(4):0;
                item.Group = !reader.IsDBNull(6) ? reader.GetString(6) : string.Empty;
                item.Format = !reader.IsDBNull(7) ? reader.GetString(7) : string.Empty;
                item.ScopeId = !reader.IsDBNull(8)?reader.GetInt32(8):0;
                //item.StockLevel = StockLevel.Calculate(reader.GetInt32(7)).ToString();
                list.Add(item);
            }
            
            foreach (var subscription in list.ToLookup(t => t.Isbn))
            {
                Data.Add(subscription.Key, subscription);
            }
        }

    }

    
}
