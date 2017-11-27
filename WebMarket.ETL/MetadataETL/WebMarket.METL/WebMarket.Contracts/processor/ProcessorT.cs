// <copyright company="Recorded Books Inc" file="Processor`1.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


namespace WebMarket.Contracts
{
    using System;
    using System.Globalization;

    public abstract class Processor<T> : IProcessor<T> where T : class, new()
    {
        private TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

        protected IProcessor<T> Successor { get; set; }

        bool IProcessor<T>.Initialize()
        {
            bool b = Initialize();
            return (b && Successor != null) ? Successor.Initialize() : b;
        }

        void IProcessor<T>.Execute(ProcessItem<T> item)
        {

            Execute(item);
            if (Successor != null && !item.IsCancelled)
            {
                Successor.Execute(item);
            }
        }

        void IProcessor<T>.Cleanup()
        {
            Cleanup();
            if (Successor != null)
            {
                Successor.Cleanup();
            }
        }

        void IProcessor<T>.SetSuccessor(IProcessor<T> successor)
        {
            Successor = successor;
        }

        protected virtual bool Initialize()
        {
            return true;
        }

        protected virtual void Cleanup()
        {

        }

        protected abstract void Execute(ProcessItem<T> item);


        protected static string Tokenize(string rawInput)
        {
            return Tokenizer.Tokenize(rawInput);
        }

        protected string ToTitleCase(string input)
        {
            return (!String.IsNullOrWhiteSpace(input))?textInfo.ToTitleCase(input) : input;
        }
    }
}
