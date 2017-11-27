// <copyright company="Recorded Books, Inc" file="ProfileOwnership.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


namespace WebMarket.Model
{
    using System;
    using System.Collections.Generic;

    [Serializable]
    public  class ProfileOwnership : Ownership
    {
        public string Isbn { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    [Serializable]
    public class BulkProfileOwnership
    {
        private List<ProfileOwnership> _profileOwnerships = new List<ProfileOwnership>();
        public List<ProfileOwnership> ProfileOwnerships
        {
            get { return _profileOwnerships; }
            set { _profileOwnerships = value; }
        }
    }
}
