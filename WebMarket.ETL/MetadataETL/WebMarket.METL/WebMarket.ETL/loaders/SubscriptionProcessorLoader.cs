// <copyright company="Recorded Books Inc" file="SubscriptionProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System;
using System.Collections.Concurrent;
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

    public class SubscriptionProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public Dictionary<string, IEnumerable<SubscriptionOwnership>> Data { get; set; }
        public override string FacetToken => "subscription";

        public override bool Initialize()
        {
            if (Constants.Environment == "na")
            {
                ProcessNorthAmericaSubscription();
            }
            else
            {
                ProcessOtherRegionSubscription();
            }
            return IsInitialized;
        }

        private void ProcessNorthAmericaSubscription()
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
        }

        private void ProcessOtherRegionSubscription()
        {
            Dictionary<int, int> mappingLibraryId = new Dictionary<int, int>();
            using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
            //using (SqlConnection cn = SqlConnectionProvider.GetConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
            {
               
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT DISTINCT OneclickdigitallibraryId, EntityId FROM LibraryAccount(NOLOCK) WHERE OneClickdigitalLibraryID IS NOT NULL";
                    cmd.CommandTimeout = 3600;
                    using (SqlDataReader sqlreader = cmd.ExecuteReader())
                    {
                        while (sqlreader.Read())
                        {
                            var tenantId = sqlreader.GetInt32(0);
                            var entityId = sqlreader.GetInt32(1);
                            if (!mappingLibraryId.ContainsKey(tenantId))
                                mappingLibraryId.Add(tenantId, entityId);
                        }
                    }
                }

            }
            using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.dpcore))
            //using (SqlConnection cn = SqlConnectionProvider.GetConnection("metl.destination.dpcore"))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = ResourceProvider.Get().GetString("Platform_Subscription");
                    cmd.CommandTimeout = 3600;
                    var mapCounter = 0;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Data = new Dictionary<string, IEnumerable<SubscriptionOwnership>>();
                        var list = new Collection<SubscriptionOwnership>();
                        while (reader.Read())
                        {
                            SubscriptionOwnership item = new SubscriptionOwnership();
                            item.Id = String.Intern(reader.GetString(0));
                            item.Name = String.Intern(reader.GetString(1));
                            item.Isbn = String.Intern(reader.GetString(2));
                            var libraryId = reader.GetInt32(3);
                            
                            item.ScopeId = mappingLibraryId.ContainsKey(libraryId) ? mappingLibraryId[libraryId] : 0;
                            item.StartDate = reader.GetDateTime(4);
                            item.EndDate = reader.GetDateTime(5);
                            list.Add(item);
                            if (mappingLibraryId.ContainsKey(libraryId)) mapCounter++;
                        }
                        Console.WriteLine("mapCounter " + mapCounter);
                        Console.WriteLine("Total subscription list count " + list.Count);

                        foreach (var subscription in list.ToLookup(t => t.Isbn))
                        {
                            Data.Add(subscription.Key, subscription);
                        }
                        Console.WriteLine("Data " + Data.Count);
                    }
                }
                IsInitialized = true;
            }
        }

        public override IProcessor<MediaTitle> Load()
        {
            var processor = new SubscriptionProcessor();
            processor.SourceData = Data;
            return processor;
        }

        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("Source_Subscription");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<SubscriptionOwnership>>();
            var list = new Collection<SubscriptionOwnership>();
            while (reader.Read())
            {
                SubscriptionOwnership item = new SubscriptionOwnership();
                item.Id = String.Intern(reader.GetString(0));
                item.Name = String.Intern(reader.GetString(1));
                item.Isbn = String.Intern(reader.GetString(2));
                item.ScopeId = reader.GetInt32(3);
                item.StartDate = reader.GetDateTime(4);
                item.EndDate = reader.GetDateTime(5);
                list.Add(item);
            }


            foreach (var subscription in list.ToLookup(t => t.Isbn))
            {
                Data.Add(subscription.Key, subscription);
            }
        }

        //public class IsbnSubscription : Subscription
        //{
        //    public string Isbn { get; set; }
        //}

    }

    
}
