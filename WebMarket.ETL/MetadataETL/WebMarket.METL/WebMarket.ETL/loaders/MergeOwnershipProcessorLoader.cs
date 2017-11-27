// <copyright company="Recorded Books Inc" file="MergeOwnershipProcessorLoader.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;

    [Serializable]
    public sealed class MergeOwnershipProcessorLoader : ProcessorLoader<MediaTitle>
    {
        private IModelRequestService service = null;

        public override string FacetToken
        {
            get { return "merge"; }
        }

        public override bool Initialize()
        {
            return LocalInitialize(service);
        }

        public override IProcessor<MediaTitle> Load()
        {
            return LocalLoad();
        }

        #region local implementations

        private bool LocalInitialize(IModelRequestService modelRequestService)
        {
            service = modelRequestService;
            IsInitialized = true;
            return IsInitialized;
        }

        private IProcessor<MediaTitle> LocalLoad()
        {
            IProcessor<MediaTitle> processor = new MergeOwnershipProcessor() {Service = service};
            return processor;
        }

        #endregion
    }
}
