// <copyright company="Recorded Books Inc" file="MergeProcessorLoader.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;

    [Serializable]
    public sealed class MergeProcessorLoader : ProcessorLoader<MediaTitle>
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
            IProcessor<MediaTitle> processor = new MergeProcessor() {Service = service};
            return processor;
        }

        #endregion
    }
}
