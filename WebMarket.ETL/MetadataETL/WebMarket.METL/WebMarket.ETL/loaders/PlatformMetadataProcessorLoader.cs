// <copyright company="Recorded Books Inc" file="OneclickMetadataProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>

using System.Collections.ObjectModel;
using System.Linq;
using WebMarket.Contracts;
using WebMarket.ETL.configuration;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class PlatformMetadataProcessorLoader : ProcessorLoader<MediaTitle>
    {
        public Dictionary<string, IEnumerable<OneclickMetadata>> Data { get; set; }

        public override string FacetToken => Constants.ImageUrls;

        public override bool Initialize()
        {
            
            using (SqlConnection cn = new SqlConnection(EtlServiceProvider.ConnectionStrings.SqlServer.dpmetadata))
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
            PlatformMetadataProcessor processor = new PlatformMetadataProcessor();
            processor.SourceData = Data;
            return processor;
        }


        private void InitializeCommand(SqlCommand cmd)
        {
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = ResourceProvider.Get().GetString("OneClick_Metadata");
            cmd.CommandTimeout = 3600;
        }

        private void BorrowReader(SqlDataReader reader)
        {
            Data = new Dictionary<string, IEnumerable<OneclickMetadata>>();
            var list = new Collection<OneclickMetadata>();
            while (reader.Read())
            {
                OneclickMetadata item = new OneclickMetadata();
                //item.Id = reader.GetInt32(0);
                item.SourceItemId = reader.GetString(0);
                item.Isbn = String.Intern(reader.GetString(1));
                //item.HasImages = reader.GetBoolean(3);
                item.S3Foldername = reader.GetString(reader.GetOrdinal("S3Foldername"));
                if (!reader.IsDBNull(reader.GetOrdinal("ActivationTimeStamp")))
                {
                    item.ActivationTimeStamp = reader.GetDateTime(reader.GetOrdinal("ActivationTimeStamp"));
                }
                //if (!reader.IsDBNull(reader.GetOrdinal("ReleaseDate")))
                //{
                //    item.ReleaseDate = reader.GetDateTime(reader.GetOrdinal("ReleaseDate"));
                //}
                //if (!reader.IsDBNull(reader.GetOrdinal("PublicationDate")))
                //{
                //    item.PublicationDate = reader.GetDateTime(reader.GetOrdinal("PublicationDate"));
                //}
                //if (!reader.IsDBNull(reader.GetOrdinal("ScheduledReleaseDate")))
                //{
                //    item.ScheduledReleaseDate = reader.GetDateTime(reader.GetOrdinal("ScheduledReleaseDate"));
                //}
                //if (!reader.IsDBNull(reader.GetOrdinal("PreReleaseDate")))
                //{
                //    item.PreReleaseDate = reader.GetDateTime(reader.GetOrdinal("PreReleaseDate"));
                //}
                item.IsPreRelease = reader.GetBoolean(reader.GetOrdinal("IsPreRelease"));

                item.Imprint = reader.GetString(reader.GetOrdinal("Imprint"));
                if (!reader.IsDBNull(reader.GetOrdinal("PreviewFileName")))
                {
                    item.PreviewFilename = reader.GetString(reader.GetOrdinal("PreviewFileName"));
                }
                item.HasDrm = reader.GetBoolean(reader.GetOrdinal("HasDrm"));
                if (!reader.IsDBNull(reader.GetOrdinal("ActualDurationMinutes")))
                {
                    item.ActualDuration = reader.GetDecimal(reader.GetOrdinal("ActualDurationMinutes"));
                }
                //if (!reader.IsDBNull(reader.GetOrdinal("DeclaredDurationMinutes")))
                //{
                //    item.DeclaredDuration = reader.GetDecimal(reader.GetOrdinal("DeclaredDurationMinutes"));
                //}
                //if (!reader.IsDBNull(reader.GetOrdinal("MediaQty")))
                //{
                //    item.MediaQuantity = reader.GetInt32(reader.GetOrdinal("MediaQty"));
                //}
                //if (!reader.IsDBNull(reader.GetOrdinal("FileSize")))
                //{
                //    item.AudioFileSize = reader.GetInt64(reader.GetOrdinal("FileSize"));
                //}
                item.HasAttachments = reader.GetBoolean(reader.GetOrdinal("HasAttachments"));

                list.Add(item);
            }
            var groupedItems = list.GroupBy(t => t.Isbn);
            foreach (var metadata in groupedItems)
            {
                Data.Add(metadata.Key, metadata);
            }
        }
    }
}

