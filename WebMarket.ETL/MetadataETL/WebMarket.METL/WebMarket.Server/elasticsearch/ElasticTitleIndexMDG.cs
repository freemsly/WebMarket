// <copyright company="Recorded Books Inc" file="TitleIndexMDG.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using WebMarket.Model;
using WebMarket.Server.elasticsearch;

namespace WebMarket.Server
{
    public class ElasticTitleIndexMDG 
    {
        public ElasticTitleIndex Post(ElasticTitleIndex t)
        {
            ElasticSearch.Index(t);
            return t;
        }
    }
}
