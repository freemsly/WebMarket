// <copyright company="Recorded Books Inc" file="MarcProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using System.Linq;
using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Collections.Generic;

    public class MarcProcessor : Processor<MediaTitle>
    {
        #region SourceData (AdditionalsMetadata)

        private Dictionary<string, IEnumerable<MarcProcessorLoader.Marcs>> _sourceData = new Dictionary<string, IEnumerable<MarcProcessorLoader.Marcs>>();

        public Dictionary<string, IEnumerable<MarcProcessorLoader.Marcs>> SourceData
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
            if (item.Model.SourceItemId == null) return;
            if (!SourceData.ContainsKey(item.Model.SourceItemId)) return;
            foreach (var i in SourceData[item.Model.SourceItemId])
            {
                if (item.SimpleProperties.Contains((String.Intern(Constants.Facets.HasMarc))))
                {
                    item.SimpleProperties[Constants.Facets.HasMarc].Value = i.HasMarc;
                }
                else
                {
                    item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.HasMarc), i.HasMarc));
                }

                if (item.SimpleProperties.Contains((String.Intern(Constants.Facets.MarcFile))))
                {
                    item.SimpleProperties[Constants.Facets.MarcFile].Value = i.FileName;
                }
                else
                {
                    item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.MarcFile), i.FileName));
                }
            }
        }
    }
}
