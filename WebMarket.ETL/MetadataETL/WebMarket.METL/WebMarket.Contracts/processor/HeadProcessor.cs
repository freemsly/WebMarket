// <copyright company="Recorded Books Inc" file="HeadProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class HeadProcessor<T> : Processor<T> where T : class, new()
    {
        protected override void Execute(ProcessItem<T> item)
        {
            
        }
    }
}
