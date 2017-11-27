// <copyright company="Recorded Books Inc" file="NarratorsProcessorLoader.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using WebMarket.Contracts;
using WebMarket.ETL.configuration;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;

    public class NarratorsProcessorLoader : ProcessorLoader<MediaTitle>
    {
        protected TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        public List<Contributor> SourceData { get; set; }
        public List<Contributor> Contributors { get; set; }

        public override string FacetToken
        {
            get { return Constants.Facets.Narrator; }
        }

        public NarratorsProcessorLoader()
        {
            Contributors = new List<Contributor>();
        }


        public override bool Initialize()
        {
            List<Contributor> list = new List<Contributor>();
            using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
            //using (SqlConnection cn = SqlConnectionProvider.GetConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandTimeout = 3200;
                    InitializeCommand(cmd);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        BorrowReader(reader, list);
                    }
                    SourceData = list;
                    IsInitialized = true;
                }
            }
            return IsInitialized;
        }

        public override IProcessor<MediaTitle> Load()
        {
            return new NarratorsProcessor() { };
        }

        public void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_Narrator");
        }

        private void BorrowReader(SqlDataReader reader, List<Contributor> list)
        {
            while (reader.Read())
            {
                string contributorType = reader.GetString(reader.GetOrdinal("contributorType"));
                if (contributorType.Equals(FacetToken))
                {
                    Contributor item = new Contributor()
                    {
                        MailName = reader.GetString(reader.GetOrdinal("MailName")),
                        SourceId = reader.GetString(reader.GetOrdinal("SourceId"))
                    };
                    if (!reader.IsDBNull(reader.GetOrdinal("FirstName")))
                    {
                        item.FirstName = String.Intern(reader.GetString(reader.GetOrdinal("FirstName")));
                    }
                    if (!reader.IsDBNull(reader.GetOrdinal("LastName")))
                    {
                        item.LastName = String.Intern(reader.GetString(reader.GetOrdinal("LastName")));
                    }
                    list.Add(item);
                }
            }
        }
    }
}
