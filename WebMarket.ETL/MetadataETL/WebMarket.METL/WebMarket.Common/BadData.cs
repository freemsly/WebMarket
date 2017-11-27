// <copyright company="Recorded Books Inc" file="BadData.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

namespace WebMarket.Common
{
    using System;

    [Serializable]
    public class BadData
    {
        public int Index { get; set; }
        public string ErrorMessage { get; set; }
        public string Data { get; set; }
        public DateTimeOffset TDS { get; set; }
    }
}
