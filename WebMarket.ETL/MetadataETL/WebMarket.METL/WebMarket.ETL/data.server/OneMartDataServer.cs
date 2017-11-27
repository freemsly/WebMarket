// <copyright company="Recorded Books Inc" file="OneMartDataServer.cs">
// Copyright © 2015 All Rights Reserved
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
    using System.Xml;

    public sealed class OneMartDataServer : DataServer<MediaTitle>
    {
        public List<MediaTitle> Items { get; set; }

        public override IEnumerator<ProcessItem<MediaTitle>> GetEnumerator()
        {
            LoadAllItems();
            int j = 0;

            foreach (var item in Items)
            {
                j++;
                ProcessItem<MediaTitle> processItem = new ProcessItem<MediaTitle>()
                {
                    Model = item,
                    SimpleProperties = new SimplePropertyCollection(),
                    TokenProperties = new TokenPropertyCollection()
                };

                yield return processItem;

            }    
        }

        private void LoadAllItems()
        {
            Items = new List<MediaTitle>();
            try
            {
                int i = 0;
                using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
                //using (SqlConnection cn = SqlConnectionProvider.GetConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
                {
                    cn.Open();
                    using (SqlCommand cmd = cn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        //cmd.CommandText = Resources.Trilogy_MediaTitle_Merge;
                        cmd.CommandText = ResourceProvider.Get().GetString("Source_MediaTitle_Merge");
                        cmd.CommandTimeout = 3600;
                        using (XmlReader reader = cmd.ExecuteXmlReader())
                        {
                            reader.ReadToNextSibling("MediaTitle");
                            bool isRead = true;
                            while (isRead)
                            {
                                string s = reader.ReadOuterXml();
                                if (String.IsNullOrWhiteSpace(s))
                                {
                                    isRead = false;
                                }
                                else
                                {
                                    i++;
                                    //MediaTitle item = GenericSerializer.StringToGenericItem<MediaTitle>(s);
                                    MediaTitle item = s.ParseXML<MediaTitle>();
                                    item.Index = i;
                                    Items.Add(item);
                                }
                            }
                        }

                    }

                }
            }
            catch (SqlException sqlEx)
            {
                //var props = eXtensibleConfig.GetProperties();
                //var message = sqlEx.Message;
                //EventWriter.WriteError(message, SeverityType.Critical, "DataAccess", props);
            }
            catch (Exception ex)
            {
                //var props = eXtensibleConfig.GetProperties();
                //var message = ex.Message;
                //EventWriter.WriteError(message, SeverityType.Critical, "DataAccess", props);
            }


        }

       
    }
}
