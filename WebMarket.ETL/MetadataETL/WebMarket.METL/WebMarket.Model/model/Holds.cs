// <copyright company="Recorded Books, Inc" file="Holds.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


namespace WebMarket.Model
{
    using System;

    [Serializable]
    public  class Holds 
    {
        //here it is oneclick libraryid, this will be changed to entityid in future
        public int ScopeId { get; set; }
        public int Count { get; set; }
    }
}
