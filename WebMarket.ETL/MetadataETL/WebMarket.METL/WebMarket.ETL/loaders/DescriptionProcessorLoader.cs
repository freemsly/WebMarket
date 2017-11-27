// <copyright company="Recorded Books Inc" file="DescriptionProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using WebMarket.Contracts;
using WebMarket.ETL.configuration;
using WebMarket.Model;
using WebMarket.Model.model;

namespace WebMarket.ETL
{

    public class DescriptionProcessorLoader : ProcessorLoader<MediaTitle>
    {
        private readonly ConnectionString connectionString;
        public Dictionary<string, string> Data { get; set; }
        public override string FacetToken
        {
            get { return Constants.Description; }
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
            var processor = new DescriptionProcessor();
            processor.SourceData = Data;
            return processor;
        }

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_Description");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, string>();
            HashSet<string> hs = new HashSet<string>();
            while (reader.Read())
            {
                var titleId = reader.GetString(0);
                var description = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                if (hs.Add(titleId))
                {
                    Data.Add(titleId, description);
                }
            }
        }
    }

    
}
