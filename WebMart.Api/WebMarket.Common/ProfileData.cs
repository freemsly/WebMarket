using System;
using System.Collections.Generic;
using System.Text;

namespace WebMarket.Common
{
    public class ProfileData
    {
        public string Activity { get; set; }
        public string AppContext { get; set; }
        public string AppInstance { get; set; }
        public string AuthKey { get; set; }
        public double Elapsed { get; set; }
        public string Group { get; set; }
        public string SearchQuery { get; set; }
        public int Index { get; set; }
        public string IP { get; set; }
        public string Key { get; set; }
        public string[] KpiLog { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public int ResultCount { get; set; }
        public string Scope { get; set; }
        public string SessionId { get; set; }
        public string Source { get; set; }
        public string Tag { get; set; }
        public string UserAgent { get; set; }
        public string Zone { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minutes { get; set; }
    }
}
