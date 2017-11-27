// <copyright company="Recorded Books Inc" file="OrderHistoryExtractor.cs">
// Copyright © 2016 All Rights Reserved
// </copyright>


using WebMarket.ETL.configuration;
using WebMarket.Model;
using WebMarket.Server;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Xml;
 
    public sealed class OrderHistoryExtractor
    {

 
        private static BulkElasticOrderHistory BulkElasticOrderHistory { get; set; }
        
        public static void BulkInsert()
        {
            LoadAllItems();
            if (BulkElasticOrderHistory.OrderHistories.Count > 0)
            {
                var elasticorderhistoryService = new ElasticOrderHistoryMDG();
                var result = elasticorderhistoryService.Post(BulkElasticOrderHistory);
            }
        }



        private static void LoadAllItems()
        {
            BulkElasticOrderHistory = new BulkElasticOrderHistory();
            var items = new List<ElasticOrderHistory>();
            try
            {
                using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
                //using (SqlConnection cn = SqlConnectionProvider.GetConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = ResourceProvider.Get().GetString("Source_OrderHistory");
                        cmd.CommandTimeout = 3600;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var orderHistory = new ElasticOrderHistory();
                                if (!reader.IsDBNull(reader.GetOrdinal("AccountNo")))
                                {
                                    if(reader.GetDataTypeName(reader.GetOrdinal("AccountNo")).Equals("int", StringComparison.CurrentCultureIgnoreCase))
                                    {
                                        orderHistory.AccountNo = reader.GetInt32(reader.GetOrdinal("AccountNo")).ToString();
                                    }
                                    else
                                    {
                                        orderHistory.AccountNo = reader.GetString(reader.GetOrdinal("AccountNo"));
                                    }
                                };
                                if (!reader.IsDBNull(reader.GetOrdinal("InvoiceNo")))
                                {
                                    orderHistory.InvoiceNo = reader.GetInt32(reader.GetOrdinal("InvoiceNo"));
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("OrderNo")))
                                {
                                    orderHistory.OrderNo = reader.GetInt32(reader.GetOrdinal("OrderNo"));
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("OrderDate")))
                                {
                                    orderHistory.OrderDate = reader.GetDateTime(reader.GetOrdinal("OrderDate"));
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("InvoiceDate")))
                                {
                                    orderHistory.InvoiceDate = reader.GetDateTime(reader.GetOrdinal("InvoiceDate"));
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("EntityId")))
                                {
                                    orderHistory.EntityId = reader.GetInt32(reader.GetOrdinal("EntityId"));
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("LibraryId")))
                                {
                                    orderHistory.LibraryId = reader.GetInt32(reader.GetOrdinal("LibraryId"));
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("ISBN")))
                                {
                                    orderHistory.ISBN = reader.GetString(reader.GetOrdinal("ISBN"));
                                }
                                orderHistory.ItemNo = reader.GetString(reader.GetOrdinal("ItemNo"));
                                orderHistory.NetAmount = reader.GetDecimal(reader.GetOrdinal("NetAmount"));
                                orderHistory.Quantity = reader.GetInt32(reader.GetOrdinal("Quantity"));
                                if (!reader.IsDBNull(reader.GetOrdinal("Format")))
                                {
                                    orderHistory.Format = reader.GetString(reader.GetOrdinal("Format"));
                                }
                                if (!reader.IsDBNull(reader.GetOrdinal("OrderMethod")))
                                {
                                    orderHistory.OrderMethod = reader.GetString(reader.GetOrdinal("OrderMethod"));
                                }

                                items.Add(orderHistory);
                            }
                        }
                    }
                }
                BulkElasticOrderHistory.OrderHistories = items;
            }
            catch (NullReferenceException nullEx)
            {
                //var props = eXtensibleConfig.GetProperties();
                //var message = nullEx.Message;
                //EventWriter.WriteError(message, SeverityType.Critical, "DataAccess", props);
            }
            catch (InvalidCastException castEx)
            {
                //var props = eXtensibleConfig.GetProperties();
                //var message = castEx.Message;
                //EventWriter.WriteError(message, SeverityType.Critical, "OrderHistoryExtractor", props);
            }
            catch (SqlException sqlEx)
            {
                //var props = eXtensibleConfig.GetProperties();
                //var message = sqlEx.Message;
                //EventWriter.WriteError(message, SeverityType.Critical, "OrderHistoryExtractor", props);
            }
            catch (Exception ex)
            {
                //var props = eXtensibleConfig.GetProperties();
                //var message = ex.Message;
                //EventWriter.WriteError(message, SeverityType.Critical, "OrderHistoryExtractor", props);
            }


        }
    }
}
