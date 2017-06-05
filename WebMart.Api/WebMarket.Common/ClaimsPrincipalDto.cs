using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading;

namespace WebMarket.Common
{
    public class ClaimsPrincipalDto : ClaimsPrincipal
    {
        private static readonly IList<string> Keys = new List<string>{
            "kpi.context",
            "kpi.instance",
            "kpi.zone",
            "kpi.authkey",
            "kpi.ip",
            "kpi.useragent",
            "kpi.requestmethod",
            "kpi.requestpath",
            "kpi.session",
            "kpi.hitindex",
            "kpi.scope",
            "kpi.key",
            "kpi.source",
            "kpi.tag",
            "kpi.group",
            "kpi.activity",
            "kpi.elapsed",
            "kpi.data",
            "kpi.index",
            "kpi.source",
            "kpi.query",
        };

        private readonly Stopwatch _stopwatch;

        /// <summary>
        /// 
        /// </summary>
        public Stopwatch StopWatch
        {
            get { return _stopwatch; }
        }

        #region Items (List<TypedItem>)

        private List<TypedItem> _items = new List<TypedItem>();

        /// <summary>
        /// Gets or sets the List<TypedItem/> value for Items
        /// </summary>
        /// <value> The List<TypedItem/> value.</value>

        public List<TypedItem> Items
        {
            get { return _items; }
            set
            {
                if (_items != null && _items != value)
                {
                    _items = value;
                }
            }
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="identity"></param>
        public ClaimsPrincipalDto(ClaimsIdentity identity)
            : base(identity)
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
            MarkBegin();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddItem(string key, object value)
        {
            //Items.Merge(key, value);
            Items.Add(new TypedItem(key, value));
        }

        /// <summary>
        /// 
        /// </summary>
        public void MarkBegin()
        {
            Items.Add(new TypedItem("api.request.begin", DateTimeOffset.Now));
        }

        /// <summary>
        /// 
        /// </summary>
        public void MarkEnd()
        {
            _stopwatch.Stop();
            Items.Add(new TypedItem("api.request.end", DateTimeOffset.Now));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public static void AddMarker(string key)
        {
            var dto = ClaimsPrincipal.Current as ClaimsPrincipalDto;
            dto?.AddItem(key, dto.StopWatch.Elapsed.TotalMilliseconds);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="item"></param>
        public static void Add(string key, object item)
        {
            var dto = Current as ClaimsPrincipalDto;
            if (dto != null)
            {
                dto.AddItem(key, item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double GetElapsed()
        {
            if (_stopwatch.IsRunning)
            {
                _stopwatch.Stop();
            }
            return _stopwatch.Elapsed.TotalMilliseconds;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ProfileData GenerateProfileItem()
        {

            ProfileData item = new ProfileData();
            ClaimsPrincipalDto dto = Current as ClaimsPrincipalDto;
            if (dto != null)
            {
                //if (dto.Items.ContainsKey<string>("kpi.scope"))
                //{
                //    item.Scope = dto.Items.Get<string>("kpi.scope");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.session"))
                //{
                //    item.SessionId = dto.Items.Get<string>("kpi.session");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.key"))
                //{
                //    item.Key = dto.Items.Get<string>("kpi.key");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.authkey"))
                //{
                //    item.AuthKey = dto.Items.Get<string>("kpi.authkey");
                //}
                //if (dto.Items.ContainsKey<int>("kpi.index"))
                //{
                //    item.Index = dto.Items.Get<int>("kpi.index");
                //    item.Index++;
                //}
                //if (dto.Items.ContainsKey<string>("kpi.ip"))
                //{
                //    item.IP = dto.Items.Get<string>("kpi.ip");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.zone"))
                //{
                //    item.Zone = dto.Items.Get<string>("kpi.zone");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.instance"))
                //{
                //    item.AppInstance = dto.Items.Get<string>("kpi.instance");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.context"))
                //{
                //    item.AppContext = dto.Items.Get<string>("kpi.context");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.useragent"))
                //{
                //    item.UserAgent = dto.Items.Get<string>("kpi.useragent");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.requestmethod"))
                //{
                //    item.RequestMethod = dto.Items.Get<string>("kpi.requestmethod");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.requestpath"))
                //{
                //    item.RequestPath = dto.Items.Get<string>("kpi.requestpath");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.tag"))
                //{
                //    item.Tag = dto.Items.Get<string>("kpi.tag");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.source"))
                //{
                //    item.Source = dto.Items.Get<string>("kpi.source");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.group"))
                //{
                //    item.Group = dto.Items.Get<string>("kpi.group");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.activity"))
                //{
                //    item.Activity = dto.Items.Get<string>("kpi.activity");
                //}
                //if (dto.Items.ContainsKey<string>("kpi.query"))
                //{
                //    item.SearchQuery = dto.Items.Get<string>("kpi.query");
                //}
                item.Elapsed = dto.GetElapsed();


                var list = new List<string>();
                foreach (var typedItem in dto.Items)
                {
                    if (!Keys.Contains(typedItem.Key, StringComparer.OrdinalIgnoreCase))
                    {
                        list.Add(typedItem.ToString());
                    }
                }
                item.KpiLog = list.ToArray();
            }

            return item;
        }
    }
}
