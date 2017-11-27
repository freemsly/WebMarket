// <copyright company="Recorded Books Inc" file="SimplePropertyProcessor.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Contracts;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;

    public abstract class SimplePropertyProcessor : Processor<MediaTitle>
    {
        protected abstract string PropertyToken { get; }
        protected override void Execute(ProcessItem<MediaTitle> item)
        {
            object propertyValue = GetTokenValue(item.Model);
            item.SimpleProperties.Add(new TypedItem(String.Intern(PropertyToken), propertyValue));
        }

        protected abstract object GetTokenValue(MediaTitle item);

    }
}
