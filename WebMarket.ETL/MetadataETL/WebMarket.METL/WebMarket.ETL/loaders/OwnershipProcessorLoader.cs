// <copyright company="Recorded Books Inc" file="OwnershipProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System.Collections.Concurrent;
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
 

    public class OwnershipProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public IDictionary<string, IEnumerable<IsbnOwnership>> Data { get; set; }
        private ConcurrentDictionary<int, int> EntityIdMapper { get; set; }

        public override string FacetToken
        {
            get { return Constants.Facets.Ownership; }
        }

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
            using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.dpcore))
            //using (SqlConnection cn = SqlConnectionProvider.GetConnection("metl.destination.dpcore"))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    InitializeSecondaryCommand(cmd);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        BorrowSecondaryReader(reader);
                    }
                    IsInitialized = true;
                }
            }
            return IsInitialized;
        }

        public override IProcessor<MediaTitle> Load()
        {
            OwnershipProcessor processor = new OwnershipProcessor();
            processor.SourceData = Data;
            return processor;
        }


        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_Ownership");
            cmd.CommandTimeout = 3600;
        }

        private void InitializeSecondaryCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("OneClick_Ownership");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<IsbnOwnership>>();
            ICollection<IsbnOwnership> list = new Collection<IsbnOwnership>();
            EntityIdMapper = new ConcurrentDictionary<int, int>();

            while (reader.Read())
            {
                EntityIdMapper.TryAdd(reader.GetInt32(0), reader.GetInt32(1));
            }

            if (reader.NextResult())
            {
                while (reader.Read())
                {
                    IsbnOwnership item = new IsbnOwnership();
                    item.ScopeId = reader.GetInt32(0);
                    item.Isbn = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                    item.TotalCopies = reader.GetInt32(2);
                    item.PlatformTenantId = reader.GetInt32(4);
                    list.Add(item);
                }
            }

           

            var groupedItems = list.GroupBy(t => t.Isbn);
            foreach (var ownership in groupedItems)
            {
                Data.Add(ownership.Key, ownership);
            }
        }

        private void BorrowSecondaryReader(SqlDataReader reader)
        {
            ICollection<IsbnOwnership> list = new Collection<IsbnOwnership>();
            while (reader.Read())
            {
                IsbnOwnership item = new IsbnOwnership();
                item.ScopeId = reader.GetInt32(0);
                item.Isbn = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
                item.TotalCopies = reader.GetInt32(2);
                item.PlatformTenantId = reader.GetInt32(0);
                list.Add(item);
            }

            foreach (var item in list.Where(item => EntityIdMapper.ContainsKey(item.PlatformTenantId)))
            {
                item.ScopeId = EntityIdMapper[item.PlatformTenantId];
            }

            var groupedItems = list.GroupBy(t => t.Isbn);
            foreach (var ownership in groupedItems)
            {
                if (!Data.ContainsKey(ownership.Key))
                {
                    Data.Add(ownership.Key, ownership);
                }
                else
                    Data[ownership.Key] = ownership;
            }
        }

        public class IsbnOwnership : Ownership
        {
            public string Isbn { get; set; }
        }
    }
}

