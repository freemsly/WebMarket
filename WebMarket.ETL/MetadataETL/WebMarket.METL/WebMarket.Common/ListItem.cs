// <copyright company="Recorded Books Inc" file="ListItem.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Common
{
    using System;

    /// <summary>
    /// This class...
    /// </summary>
    [Serializable]
    public abstract class ListItem
    {
        public string Id { get; set; }

        private string text;
        public string Text { get => text; set => text = value;}

        private string _token;
        public string Token { get => _token;set => _token = value;}

        public int Order { get; set; }

    }
}
