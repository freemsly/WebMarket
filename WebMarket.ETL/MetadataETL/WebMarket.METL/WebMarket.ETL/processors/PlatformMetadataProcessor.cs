// <copyright company="Recorded Books Inc" file="OneclickMetadataProcessor.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;

    public class PlatformMetadataProcessor : Processor<MediaTitle>
    {
        #region SourceData (AdditionalsMetadata)

        private Dictionary<string, IEnumerable<OneclickMetadata>> _sourceData = new Dictionary<string, IEnumerable<OneclickMetadata>>();

        public Dictionary<string, IEnumerable<OneclickMetadata>> SourceData
        {
            get { return _sourceData; }
            set
            {
                if (_sourceData != null && _sourceData != value)
                {
                    _sourceData = value;
                }
            }
        }

        #endregion

        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            if (!SourceData.ContainsKey(item.Model.ISBN)) return;
            foreach (var metadata in SourceData[item.Model.ISBN])
            {
                AddOneclickMetadata(item, metadata);   
            }
        }

        private void AddOneclickMetadata(ProcessItem<MediaTitle> item, OneclickMetadata found)
        {
            if (!String.IsNullOrWhiteSpace(found.S3Foldername))
            {
                if (!String.IsNullOrWhiteSpace(found.PreviewFilename))
                {
                    //string path = String.Format("{0}/{1}", found.S3Foldername, found.PreviewFilename).Replace('\\', '/');
                    if (item.SimpleProperties.Contains((String.Intern(Constants.PreviewFile))))
                    {
                        item.SimpleProperties[Constants.PreviewFile].Value = found.PreviewFilename;
                    }
                    else
                    {
                        item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.PreviewFile), found.PreviewFilename));
                    }
                }
            }

            if (item.SimpleProperties.Contains((Constants.Facets.IsComingSoon)))
            {
                item.SimpleProperties[Constants.Facets.IsComingSoon].Value = found.IsPreRelease;
            }
            else
            {
                item.SimpleProperties.Add(new TypedItem(Constants.Facets.IsComingSoon, found.IsPreRelease));   
            }

            if (found.ActivationTimeStamp > DateTime.MinValue)
            {
                if (item.SimpleProperties.Contains((String.Intern(Constants.Activation))))
                {
                    item.SimpleProperties[Constants.Activation].Value = found.ActivationTimeStamp;
                }
                else
                {
                    item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Activation),
                        found.ActivationTimeStamp));
                }
            }

            if (found.ActualDuration > 0.0M)
            {
                if (item.SimpleProperties.Contains((String.Intern(Constants.Duration))))
                {
                    item.SimpleProperties[Constants.Duration].Value = found.ActualDuration;
                }
                else
                {
                    item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Duration),
                        found.ActualDuration));
                }
            }
            if (found.AudioFileSize > 0)
            {
                if (item.SimpleProperties.Contains((String.Intern(Constants.AudioFileSize))))
                {
                    item.SimpleProperties[Constants.AudioFileSize].Value = found.AudioFileSize;
                }
                else
                {
                    item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.AudioFileSize),
                        found.AudioFileSize));
                }
            }

            if (found.ActivationTimeStamp > DateTime.MinValue)
            {
                if (item.SimpleProperties.Contains((String.Intern(Constants.Facets.ReleaseDate))))
                {
                    item.SimpleProperties[Constants.Facets.ReleaseDate].Value = found.ActivationTimeStamp;
                }
                else
                {
                    item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.ReleaseDate),
                        found.ActivationTimeStamp.Date));
                }
            }

            if (found.HasDrm)
            {
                if (item.SimpleProperties.Contains((String.Intern(Constants.Facets.HasDrm))))
                {
                    item.SimpleProperties[Constants.Facets.HasDrm].Value = found.HasDrm;
                }
                else
                {
                    item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.HasDrm),
                        found.HasDrm));
                }
            }
            if (found.HasAttachments)
            {
                if (item.SimpleProperties.Contains((String.Intern(Constants.Facets.HasAttachments))))
                {
                    item.SimpleProperties[Constants.Facets.HasDrm].Value = found.HasAttachments;
                }
                else
                {
                    item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.HasAttachments),
                        found.HasAttachments));
                }
            }
        }
    }
}
