// <copyright company="Recorded Books Inc" file="MagazineExtractor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using MySql.Data.MySqlClient;
using WebMarket.ETL.configuration;
using WebMarket.Model;
using WebMarket.Server;
using WebMarket.Server.elasticsearch;

namespace WebMarket.ETL.extractor
{
    public sealed class MagazineExtractor
    {
    
        private static BulkElasticMagazineIndex BulkElasticMagazineIndex { get; set; }
        private static BulkElasticSuggestiveMagazineIndex BulkElasticSuggestiveMagazineIndex { get; set; }

        public static void BulkInsert()
        {
            if (Constants.Environment.ToLower() == "na")
            {
                LoadAllItems();
                if (BulkElasticMagazineIndex.ElasticMagazine.Count > 0)
                {
                    var elasticmagazineServer = new BulkElasticMagazineIndexMDG();
                    elasticmagazineServer.Post(BulkElasticMagazineIndex);
                    //var result = Service.Post<BulkElasticMagazineIndex>(BulkElasticMagazineIndex);
                }

                ProcessSuggestiveMagazineIndex();
            }
        }

        private static void ProcessSuggestiveMagazineIndex()
        {
            BulkElasticSuggestiveMagazineIndex =
                new BulkElasticSuggestiveMagazineIndex {ElasticSuggestive = new List<ElasticSuggestiveMagazineIndex>()};

            foreach (var item in BulkElasticMagazineIndex.ElasticMagazine)
            {
                var elasticMagazine = new ElasticSuggestiveMagazineIndex
                {
                    Title = new Suggest()
                    {
                        Input = new[] {item.Title},
                        Output = item.Title
                    },
                    Publisher = item.Publisher,
                    Genre = item.Genre
                };
                BulkElasticSuggestiveMagazineIndex.ElasticSuggestive.Add(elasticMagazine);
            }

            if (BulkElasticSuggestiveMagazineIndex.ElasticSuggestive.Count > 0)
            {
                var elasticmagazineSuggestiveServer = new BulkElasticSuggestiveMagazineIndexMDG();
                elasticmagazineSuggestiveServer.Post(BulkElasticSuggestiveMagazineIndex);
                //Service.Post<BulkElasticSuggestiveMagazineIndex>(BulkElasticSuggestiveMagazineIndex);
            }
        }

        

        private static void LoadAllItems()
        {
            BulkElasticMagazineIndex = new BulkElasticMagazineIndex();
            var items = new List<ElasticMagazineIndex>();
            try
            {
                var zinioMagazines =  LoadMagazineFromZinio();
                var trilogyMagazines = LoadMagazineFromTrilogy();

                items = MergeMagazineFromTrilogyToZinio(zinioMagazines, trilogyMagazines);

                BulkElasticMagazineIndex.ElasticMagazine= items;
            }
            catch (NullReferenceException nullEx)
            {
                //var props = eXtensibleConfig.GetProperties();
                //EventWriter.WriteError(nullEx, SeverityType.Critical, "MagazineExtractor", props);
            }
            catch (MySqlException sqlEx)
            {
                //var props = eXtensibleConfig.GetProperties();
                //EventWriter.WriteError(sqlEx, SeverityType.Critical, "MagazineExtractor", props);
            }
            catch (Exception ex)
            {
                //var props = eXtensibleConfig.GetProperties();
                //EventWriter.WriteError(ex, SeverityType.Critical, "MagazineExtractor", props);
            }


        }

        private static List<ElasticMagazineIndex> MergeMagazineFromTrilogyToZinio(List<ElasticMagazineIndex> zinioMagazines, List<ElasticMagazineIndex> trilogyMagazines)
        {

            var mergedList = (from item1 in trilogyMagazines
                join item2 in zinioMagazines
                on item1.Id equals item2.Id
                select item2).ToList();

            foreach (var trilogyItem in trilogyMagazines)
            {
                foreach (var mergeItem in mergedList)
                {
                    if (trilogyItem.Id != mergeItem.Id) continue;
                    mergeItem.Genre = trilogyItem.Genre;
                    mergeItem.Price = trilogyItem.Price;
                    mergeItem.TermsAndConditionIdentifier = trilogyItem.TermsAndConditionIdentifier;
                }
            }
            return mergedList.ToList();
        }

        private static List<ElasticMagazineIndex> LoadMagazineFromTrilogy()
        {
            var items = new List<ElasticMagazineIndex>();
            using (SqlConnection cn = new SqlConnection((EtlServiceProvider.ConnectionStrings.SqlServer.trilogy)))
            //using (SqlConnection cn = SqlConnectionProvider.GetConnection(EtlServiceProvider.ConnectionStrings.SqlServer.trilogy))
            {
                cn.Open();
                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT Job_no AS Id, GrossSellingPrice as Price,Genre, " +
                                      "(SELECT TOP 1 Id FROM HydratedModel(NOLOCK) HM WHERE HM.Job_No = s.Job_No AND Hm.Stock_no = s.stock_no AND PublisherModelId = 794 AND IsActive = 1) AS HydratedModelId " +
                                      "FROM eMagazineDetail m INNER JOIN stocklines s on m.Stock_no = s.stock_no and s.status_code = 'Available'";
                    cmd.CommandTimeout = 3600;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var magazine = new ElasticMagazineIndex()
                            {
                                Id = reader.GetString(reader.GetOrdinal("Id")),
                                Genre = reader.GetString(reader.GetOrdinal("Genre")),
                                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                                TermsAndConditionIdentifier = reader.GetInt32(reader.GetOrdinal("HydratedModelId")),
                            };
                            items.Add(magazine);
                        }
                    }
                }
            }
            return items;
        }

     

        private static List<ElasticMagazineIndex> LoadMagazineFromZinio()
        {
           
            var magazineMetadata = LoadMagazineMetadata();
            var popularMagazines = LoadPopularMagazines();

            foreach (var item in magazineMetadata)
            {
                foreach (var popularMagazine in popularMagazines)
                {
                    if (item.MagazineId == popularMagazine.MagazineId)
                    {
                        item.CheckoutCount = popularMagazine.CheckoutCount;
                    }
                }   
            }
            return magazineMetadata;
        }

        private static List<ElasticMagazineIndex> LoadPopularMagazines()
        {
            var items = new List<ElasticMagazineIndex>();
            using (var cn = new MySqlConnection("metl.zinio.mysql"))
            //using (var cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["metl.zinio.mysql"].ConnectionString))
            {
                cn.Open();
                using (MySqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT Magazine_Id AS MagazineId, COUNT(PATRON_ID) As PatronCheckoutCount FROM RBDG.ZINIO_MAGAZINE_STATISTIC " +
                        "LEFT JOIN RBDG.PATRON ON RBDG.PATRON.ID = RBDG.ZINIO_MAGAZINE_STATISTIC.PATRON_ID " +
                        "LEFT JOIN RBDG.LIBRARY ON RBDG.PATRON.LIBRARY_ID = RBDG.LIBRARY.ID " +
                        "WHERE THE_DATE >= now() - interval 3 month " +
                        "AND RBDG.LIBRARY.OCD_ROOT_DOMAIN = 'rbdigital.com' " +
                        "GROUP BY Magazine_ID;";
                    cmd.CommandTimeout = 3600;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var magazine = new ElasticMagazineIndex()
                            {
                                MagazineId = reader.GetString(reader.GetOrdinal("MagazineId")),
                                CheckoutCount = reader.GetInt32(reader.GetOrdinal("PatronCheckoutCount"))
                            };
                            items.Add(magazine);
                        }
                    }
                }
            }
            return items;
        }

        private static List<ElasticMagazineIndex> LoadMagazineMetadata()
        {
            var items = new List<ElasticMagazineIndex>();
            using (var cn = new MySqlConnection("metl.zinio.mysql"))
            //using (var cn = new MySqlConnection(ConfigurationManager.ConnectionStrings["metl.zinio.mysql"].ConnectionString))
            {
                cn.Open();
                using (MySqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT RBID,M.NAME, COUNTRY,GENRE,DESCRIPTION,LANGUAGE,COVER_DATE AS CoverDate," +
                                      "M.ID AS MagazineID,MI.Id AS IssueId,P.Name AS Publisher,REPLACE(MI.COVER_IMAGE_URL,'http','https') As ImageUrl,Frequency" +
                                      ", Rating, CURRENT_ISSUE_LIMIT AS CapLimit, ISSN,Price_USD AS Price " +
                                      "FROM RBDG.ZINIO_MAGAZINE M " +
                                      "INNER JOIN RBDG.ZINIO_MAGAZINE_PUBLISHER P ON M.PUBLISHER_ID = P.Id " +
                                      "INNER JOIN RBDG.ZINIO_MAGAZINE_ISSUE MI ON MI.ID = " +
                                      "(SELECT ID FROM RBDG.ZINIO_MAGAZINE_ISSUE WHERE Magazine_ID = M.Id ORDER BY COVER_DATE DESC LIMIT 1)  " +
                                      "WHERE P.TIME_PERIOD = 4";
                    cmd.CommandTimeout = 3600;
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var magazine = new ElasticMagazineIndex()
                            {
                                Id = reader.GetString(reader.GetOrdinal("RBID")),
                                Title = reader.GetString(reader.GetOrdinal("Name")),
                                Country = reader.GetString(reader.GetOrdinal("Country")),
                                Genre = reader.GetString(reader.GetOrdinal("Genre")),
                                Description = reader.GetString(reader.GetOrdinal("Description")),
                                IssueId = reader.GetString(reader.GetOrdinal("IssueId")),
                                Language = reader.GetString(reader.GetOrdinal("Language")),
                                Publisher = reader.GetString(reader.GetOrdinal("publisher")),
                                MagazineId = reader.GetString(reader.GetOrdinal("MagazineId")),
                                CoverDate = reader.GetDateTime(reader.GetOrdinal("CoverDate")),
                                ImageUrl = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                Issn = reader.GetString(reader.GetOrdinal("ISSN")),
                                //Audience = reader.GetString(reader.GetOrdinal("ImageUrl")),
                                PublishedOn = reader.GetDateTime(reader.GetOrdinal("CoverDate")),
                                Rating = reader.GetString(reader.GetOrdinal("Rating")),
                                Frequency = reader.GetString(reader.GetOrdinal("Frequency")),
                                CapLimit = reader.GetInt32(reader.GetOrdinal("CapLimit")),
                                Price = reader.GetInt32(reader.GetOrdinal("Price")),
                            };
                            items.Add(magazine);
                        }
                    }
                }
            }
            return items;
        }
    }
}
