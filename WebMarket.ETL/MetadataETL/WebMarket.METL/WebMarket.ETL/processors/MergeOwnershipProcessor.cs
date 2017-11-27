// <copyright company="Recorded Books Inc" file="MergeProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{


    public class MergeOwnershipProcessor : Processor<MediaTitle>
    {
        public IModelRequestService Service { get; set; }
        private BulkElasticOwnershipIndexCollection ElasticOwnershipIndexList { get; set; }

        public MergeOwnershipProcessor()
        {
            ElasticOwnershipIndexList = new BulkElasticOwnershipIndexCollection();
        }

        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            var builder = new TitleIndexBuilder(item);
            var index = builder.BuildCompact();
            ElasticOwnershipIndexList.ElasticOwnershipCollections.Add(index.MapOwnership());
        }

        private void BulkExecute<T>(T ttype) where T : class,new()
        {
            var response = Service.Post(ttype);
            if (!response.IsOkay)
            {
                //EventWriter.WriteError(response.Status, SeverityType.Error);
            }
        }

        protected override void Cleanup()
        {
            BulkExecute(ElasticOwnershipIndexList);
        }
    }
}
