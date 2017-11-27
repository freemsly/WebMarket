// <copyright company="Recorded Books Inc" file="ImagesProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public class ImagesProcessor : Processor<MediaTitle>
    {
        #region SourceData (AdditionalsMetadata)

        private Dictionary<string, IEnumerable<ImagesProcessorLoader.IsbnImages>> _sourceData = new Dictionary<string, IEnumerable<ImagesProcessorLoader.IsbnImages>>();

        public Dictionary<string, IEnumerable<ImagesProcessorLoader.IsbnImages>> SourceData
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
            
            //if (!SourceData.ContainsKey(item.Model.ISBN)) return;
            //var serverPath = Constants.IsbnImageRootUrl;
            //foreach (var i in SourceData[item.Model.ISBN])
            //{
            //    if (i.HasImages)
            //    {
            //        if (item.SimpleProperties.Contains((String.Intern(Constants.Facets.HasImages))))
            //        {
            //            item.SimpleProperties[Constants.Facets.HasImages].Value = i.HasImages;
            //        }
            //        else
            //        {
            //            item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.HasImages),i.HasImages));
            //        }
            //        item.SimpleProperties[Constants.Facets.HasImages].Value = i.HasImages;
            //        string s3Folder = i.S3FolderName;
            //        ImageUrl medium = new ImageUrl() { Name = "medium", Url = String.Format("{0}{1}/{1}_image_95x140.jpg", serverPath, s3Folder) };
            //        ImageUrl large = new ImageUrl() { Name = "large", Url = String.Format("{0}{1}/{1}_image_128x192.jpg", serverPath, s3Folder) };
            //        ImageUrl xLarge = new ImageUrl() { Name = "x-large", Url = String.Format("{0}{1}/{1}_image_148x230.jpg", serverPath, s3Folder) };
            //        ImageUrl xxLarge = new ImageUrl() { Name = "xx-large", Url = String.Format("{0}{1}/{1}_image_512x512_iTunes.png", serverPath, s3Folder) };
            //        //item.ImageUrls = new[] { medium, large, xLarge, xxLarge };
            //    }
            //    else
            //    {
            //        var environment = Constants.Environment.ToLower();
            //        ImageUrl medium = new ImageUrl() { Name = "medium", Url = String.Format(CultureInfo.InvariantCulture, "{0}default/{1}_image_95x140.jpg", serverPath, environment) }; // 93x135
            //        ImageUrl large = new ImageUrl() { Name = "large", Url = String.Format(CultureInfo.InvariantCulture, "{0}default/{1}_image_128x192.jpg", serverPath, environment) }; // 129x208
            //        ImageUrl xLarge = new ImageUrl() { Name = "x-large", Url = String.Format(CultureInfo.InvariantCulture, "{0}default/{1}_image_148x230.jpg", serverPath, environment) };
            //        ImageUrl xxLarge = new ImageUrl() { Name = "xx-large", Url = String.Format(CultureInfo.InvariantCulture, "{0}default/{1}_image_512x512.jpg", serverPath, environment) };
            //       // item.ImageUrls = new[] { medium, large, xLarge, xxLarge };
            //    }
            //}
        }
    }
}
