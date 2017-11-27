// <copyright company="Recorded Books Inc" file="IOneMartProcessing.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


namespace WebMarket.ETL
{
    using WebMarket.Contracts;
    using WebMarket.Model;
    using System.Collections.Generic;

    public interface IOneMartProcessing
    {
        List<ProcessorLoader<MediaTitle>> Get();
    }
}
