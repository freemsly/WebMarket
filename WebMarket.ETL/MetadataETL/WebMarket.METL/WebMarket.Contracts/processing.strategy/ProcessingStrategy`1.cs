// <copyright company="Recorded Books Inc" file="ProcessingStrategy`1.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public abstract class ProcessingStrategy<T> : IProcessingStrategy<ProcessItem<T>> where T : class, new()
    {
        #region Loaders (List<ProcessorLoader>)

        private List<ProcessorLoader<T>> _Loaders = new List<ProcessorLoader<T>>();

        /// <summary>
        /// Gets or sets the List<ProcessorLoader> value for Loaders
        /// </summary>
        /// <value> The List<ProcessorLoader> value.</value>

        public List<ProcessorLoader<T>> Loaders
        {
            get { return _Loaders; }
            set
            {
                if (_Loaders != value)
                {
                    _Loaders = value;
                }
            }
        }

        #endregion

        protected IProcessor<T> Processors { get; set; }

        //private IModelRequestService service = new PassThroughModelRequestService(
        //                new DataRequestService(new XF.DataServices.ModelDataGatewayDataService())
        //                );

        //protected IModelRequestService Service
        //{
        //    get { return service; }
        //}

        bool IProcessingStrategy<ProcessItem<T>>.Initialize()
        {
            return Initialize();
        }

        void IProcessingStrategy<ProcessItem<T>>.ExecuteStrategy()
        {
            ExecuteStrategy();
        }

        void IProcessingStrategy<ProcessItem<T>>.Cleanup()
        {
            Cleanup();
        }

        public virtual IDataServer<ProcessItem<T>> DataSource { get; set; }

        protected virtual bool Initialize()
        {
            
           bool b = DataSource != null ;
            
           b = b ? InitializeLoaders() : false;

           b = b ? GenerateProcessorChain() : false;
            
           return b;
        }

        protected virtual void ExecuteStrategy()
        {
            Processors.Initialize();
            if (DataSource.Initialize())
            {
                foreach (ProcessItem<T> item in DataSource)
                {
                    Processors.Execute(item);
                }                
            }
        }

        protected virtual void Cleanup()
        {
            DataSource.Cleanup();
            Processors.Cleanup();
        }

        protected virtual bool InitializeLoaders()
        {
            bool b = true;
            foreach (var item in Loaders)
            {
                b =  b ? item.Initialize() : false;
            }
            return b;
        }

        protected virtual bool GenerateProcessorChain()
        {
            bool b = true;
            List<ProcessorLoader<T>> removals = new List<ProcessorLoader<T>>();
            foreach (ProcessorLoader<T> item in Loaders)
            {
                if (!item.IsInitialized)
                {
                    b = false;
                    removals.Add(item);
                }
            }
            foreach (var item in removals)
            {
                Loaders.Remove(item);
            }

            List<IProcessor<T>> list = new List<IProcessor<T>>();
            for (int i = 0; i < Loaders.Count; i++)
            {
                IProcessor<T> p = Loaders[i].Load();
                if (i > 0)
                {
                    list[i - 1].SetSuccessor(p);
                }
                list.Add(p);
            }
            Processors = new HeadProcessor<T>();
            Processors.SetSuccessor(list[0]);

            return b;
        }

    }
}
