// <copyright company="Recorded Books Inc" file="ProcessorLoader.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System;
    using System.Globalization;


    public abstract class ProcessorLoader<T> where T : class, new()
    {
        private TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        public bool IsInitialized { get; set; }

        public abstract string FacetToken { get; }

        public abstract bool Initialize();


        public abstract IProcessor<T> Load();

        public static string Tokenize(string rawInput)
        {
            return Tokenizer.Tokenize(rawInput);
        }

        public virtual void Write(string initializationStatus)
        {
            string s = this.GetType().Name;
            Console.WriteLine(String.Format("{0} initialized = {1}",s,initializationStatus));
        }

        public virtual void Cleanup(IModelRequestService service)
        {

        }

        protected string ToTitleCase(string input)
        {
            return (!String.IsNullOrWhiteSpace(input)) ? textInfo.ToTitleCase(input) : input;
        }
    }

}
