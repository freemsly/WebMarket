// <copyright company="Recorded Books Inc" file="FictionNonFictionProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using System.Collections.Generic;
using System.Linq;
using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    
    using System;

    public class FictionNonFictionProcessor : Processor<MediaTitle>
    {
        private readonly IEnumerable<string> _fictionConstants = new List<string>() {"fiction","1","true"};
        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            if (!String.IsNullOrEmpty(item.Model.FnF) && _fictionConstants.Contains(item.Model.FnF, StringComparer.OrdinalIgnoreCase))
            {
                item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.IsFiction), true));
            }
            else
            {
                item.SimpleProperties.Add(new TypedItem(String.Intern(Constants.Facets.IsFiction), false));
            }
        }
    }
}
