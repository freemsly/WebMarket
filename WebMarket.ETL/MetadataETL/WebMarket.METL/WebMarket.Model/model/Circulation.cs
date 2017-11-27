// <copyright company="Recorded Books, Inc" file="Circulation.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


namespace WebMarket.Model
{
    using System;

    [Serializable]
    public  class Circulation 
    {
        //here it is oneclick libraryid, this will be changed to entityid in future
        public int ScopeId { get; set; }
        public int Count { get; set; }
    }
}
