// <copyright company="Recorded Books, Inc" file="KpiPublisher.cs">
// Copyright © 2017 All Right Reserved
// </copyright>


using System.Globalization;
using WebMarket.Common;
using WebMarket.Api.Search.Contracts;

namespace WebMarket.Api.Infrastructure.Common
{
    using System;
    using System.Text;
    /// <summary>
    /// 
    /// </summary>
    public sealed class KpiPublisher
    {
        /// <summary>
        /// 
        /// </summary>
        public static bool IsOn;
        private static readonly WebMarket.Common.Api _api;

        static KpiPublisher()
        {
            Initialize();
        }

        private static void Initialize()
        {
                IsOn = _api.IsProfilling;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        public static void Publish<T>(Query<T> query) where T : class, new()
        {
            if (!IsOn) return;
            ProfileData item;
            BuildQueryData(query, out item);
            Publish(item);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public static void Publish(ProfileData item)
        {
            //Service.Post<ProfileData>(item);
        }

        private static void BuildQueryData<T>(Query<T> query, out ProfileData item) where T : class, new()
        {
            ClaimsPrincipalDto.AddMarker("kpi.profiler.execute");
            item = ClaimsPrincipalDto.GenerateProfileItem();
            var todaysDate = DateTime.Now;
            item.Day = todaysDate.Day;
            item.Month = todaysDate.Month;
            item.Year = todaysDate.Year;
            item.CreatedAt = todaysDate;
            item.Hour = todaysDate.Hour;
            item.Minutes = todaysDate.Minute;

            item.Activity = "search.api";
            item.Tag = query.Source;
            item.Scope = query.ScopeId.ToString(CultureInfo.InvariantCulture);
            
            var i = 0;
            var sb = new StringBuilder(500);
            foreach (var facet in query.Facets)
            {
                if (i++ > 0)
                {
                    sb.Append(";");
                }
                var sbValue = new StringBuilder();
                foreach (var value in facet.Value)
                {
                    sbValue.Append(value);
                    sbValue.Append(",");
                }
                sb.AppendFormat("{0}:{1}", facet.Key, sbValue);
            }
            foreach (var term in query.Criterion)
            {
                if (i++ > 0)
                {
                    sb.Append(";");
                }
                sb.AppendFormat("{0}:{1}", term.Key, term.Value);
                item.Key = string.Format("{0}:{1}", term.Key, term.Value.Trim());
            }
            sb.AppendFormat(";page-count:{0}", query.PageCount);
            sb.AppendFormat(";page-index:{0}", query.PageIndex);
            sb.AppendFormat(";page-size:{0}", query.PageSize);
            sb.AppendFormat(";resultset-count:{0}", query.ResultSetCount);
            sb.AppendFormat(";sort-by:{0}", query.SortBy);
            sb.AppendFormat(";sort-order:{0}", query.SortOrder);
            if (!String.IsNullOrEmpty(query.SessionId))
            {
                sb.AppendFormat(";sessionid-{0}", query.SessionId);
            }
            item.Group = sb.ToString();
            item.ResultCount = query.ResultSetCount;
        }

    }
}

