// <copyright company="Recorded Books Inc" file="GenresProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System.Data.SqlClient;
using WebMarket.Contracts;
using WebMarket.ETL.configuration;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class GenresProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public Dictionary<string, string> Data { get; set; }
        public override string FacetToken
        {
            get { return Constants.Facets.Genre; }
        }

        public override IProcessor<MediaTitle> Load()
        {
            var processor = new GenresProcessor();
            processor.SourceData = Data;
            return processor;
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

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_Genres");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string,string>();
            while (reader.Read())
            {
                if (!reader.IsDBNull(0) && !reader.IsDBNull(1))
                {
                    Data.Add(reader.GetString(0), String.Intern(reader.GetString(1) ?? string.Empty));
                }
            }
        }

    }
}
