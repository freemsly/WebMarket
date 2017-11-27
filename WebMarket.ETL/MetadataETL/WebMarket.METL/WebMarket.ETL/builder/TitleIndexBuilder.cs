// <copyright company="Recorded Books Inc" file="TitleIndexBuilder.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>


using System.Globalization;
using WebMarket.Common;
using WebMarket.Contracts;
using WebMarket.Model;
using ImageUrl = WebMarket.Model.ImageUrl;

namespace WebMarket.ETL
{
    
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public sealed class TitleIndexBuilder : Builder<MediaTitle,TitleIndex>
    {
        #region constructors

        public TitleIndexBuilder(ProcessItem<MediaTitle> item)
        {
            ProcessItem = item;
        }

        #endregion

        public override bool Validate()
        {
            return true;
        }

        public override TitleIndex Build()
        {
            return LocalBuild();
        }

        public TitleIndex BuildCompact()
        {
            var item = new TitleIndex
            {
                Ownership = new List<Ownership>(),
                Isbn = ProcessItem.Model.ISBN ?? string.Empty,
            };

            if (ProcessItem.Model.Ownership.Count > 0)
            {
                if (ProcessItem.Model.Holds.Count > 0)
                {
                    foreach (var ownershipitem in ProcessItem.Model.Ownership)
                    {
                        foreach (var holditem in ProcessItem.Model.Holds.Where(holditem => holditem.ScopeId == ownershipitem.ScopeId))
                        {
                            ownershipitem.HoldsCopies = holditem.Count;
                        }
                    }
                }
                if (ProcessItem.Model.Circulations.Count > 0)
                {
                    foreach (var ownershipitem in ProcessItem.Model.Ownership)
                    {
                        foreach (var circulationitem in ProcessItem.Model.Circulations.Where(circulationitem => circulationitem.ScopeId == ownershipitem.ScopeId))
                        {
                            ownershipitem.CirculationCopies = circulationitem.Count;
                        }
                    }
                }
                item.Ownership.AddRange(ProcessItem.Model.Ownership);
            }
            return item;

        }

        #region helper methods

        private TitleIndex LocalBuild()
        {
            var item = new TitleIndex()
            {
                Ownership = new List<Ownership>(),
                SalesRights = new List<string>(),
                UsageTerms = new List<string>(),
                SOP = new List<SOP>(),
                Genres = new List<string>()
            };

            item.Description = ProcessItem.Model.Description;

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.PublishedDate)) 
            {
                item.PublishedOn = ProcessItem.SimpleProperties.GetValue<DateTime>(Constants.Facets.PublishedDate);
            }

            item.Isbn = ProcessItem.Model.ISBN ?? string.Empty;
            item.Id = item.Isbn;
            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.Publisher))
            {
                var data = ProcessItem.SimpleProperties.GetValue<string>(Constants.Facets.Publisher);
                item.Publisher = data;
                item.Publishers = new List<string>() { data };
            }

            if (ProcessItem.TokenProperties.ContainsAny<MediaType>())
            {
                var data = ProcessItem.TokenProperties.Get<MediaType>();
                item.MediaType = data.Text;
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.Language))
            {
                var data = ProcessItem.SimpleProperties.GetValue<string>(Constants.Facets.Language);
                item.Language = data;
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.Series))
            {
                var data = ProcessItem.SimpleProperties.GetValue<string>(Constants.Facets.Series);
                item.Series = data;
            }


            if (ProcessItem.TokenProperties.ContainsAny<Author>())
            {
                var q = (ProcessItem.TokenProperties.Where(x => x.GetType() == typeof(Author)).OrderBy(x => x.Order)).ToList();
                for (var i = 0; i < q.Count; i++)
                {
                    if (i == 0)
                    {
                        item.Author = q[i].Text;
                    }
                    else
                    {
                        item.Author += "|" + q[i].Text;
                    }
                    //item.Authors.Add(q[i].Text); 
                }

                var data = ProcessItem.SimpleProperties.GetValue<string>(Constants.Facets.SortAuthor);
                item.AuthorSort = data; 
            }

            if (ProcessItem.TokenProperties.ContainsAny<Narrator>())
            {
                var q = (ProcessItem.TokenProperties.Where(x => x.GetType() == typeof(Narrator)).OrderBy(x => x.Order)).ToList();
                var sb = new StringBuilder();
                for (int i = 0; i < q.Count; i++)
                {
                    sb.Append(i == 0 ? q[i].Text : String.Format("| {0}", q[i].Text));
                    //item.Narrators.Add(q[i].Text);
                }
                item.Narrator = sb.ToString();
            }

            if (ProcessItem.TokenProperties.ContainsAny<Genre>())
            {
                int i = 0;
                var list = new List<Genre>();
                var sb = new StringBuilder();
                foreach (var y in ProcessItem.TokenProperties.Where(x => x.GetType() == typeof(Genre)).OrderBy(x => x.Order))
                {
                    if (i++ > 0)
                    {
                        sb.Append("|");
                    }
                    sb.Append(y.Text.Trim());
                    list.Add(new Genre()
                    {
                        Token = y.Token,
                        Text = y.Text.Trim()
                    });
                }
                item.Genre = sb.ToString();
                item.Genres = list.Select(t => t.Text).Distinct().ToList();

            }


            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.Audience))
            {
                var data = ProcessItem.SimpleProperties.GetValue<string>(Constants.Facets.Audience);
                if(!string.IsNullOrEmpty(data))
                    item.Audience = data;
            }
            if (ProcessItem.SimpleProperties.Contains(Constants.PreviewFile))
            {
                item.PreviewFile = ProcessItem.SimpleProperties.GetValue<string>(Constants.PreviewFile);
            }
            
            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.HasDrm))
            {
               item.HasDrm = ProcessItem.SimpleProperties.GetValue<bool>(Constants.Facets.HasDrm);
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.HasImages))
            {
                item.HasImages = ProcessItem.SimpleProperties.GetValue<bool>(Constants.Facets.HasImages);
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.ImageUrls))
            {
                //item.Images = new List<ImageUrl>(ProcessItem.ImageUrls);
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Activation))
            {
                item.ActivatedOn = ProcessItem.SimpleProperties.GetValue<DateTime>(Constants.Activation);
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.ReleaseDate))
            {
                item.ActivatedOn = ProcessItem.SimpleProperties.GetValue<DateTime>(Constants.Facets.ReleaseDate);
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.HasDrm))
            {
                item.HasDrm = ProcessItem.SimpleProperties.GetValue<bool>(Constants.Facets.HasDrm);
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Duration))
            {
                var found = ProcessItem.SimpleProperties.GetValue<decimal>(Constants.Duration);
                item.Duration = found;
            }

            if (ProcessItem.TokenProperties.ContainsAny<Title>())
            {
                var data = ProcessItem.TokenProperties.Get<Title>();
                item.Title = data.Text;
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.Subtitle))
            {
                item.ItemSubtitle = ProcessItem.SimpleProperties.GetValue<string>(Constants.Facets.Subtitle);
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.Fulltitle))
            {
                item.FullTitle = ProcessItem.SimpleProperties.GetValue<string>(Constants.Facets.Fulltitle);
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.IsFiction))
            {
                item.IsFiction = (bool)ProcessItem.SimpleProperties.GetValue<bool>(Constants.Facets.IsFiction);
            }

            item.Imprint = ProcessItem.Model.Imprints;
            item.ListPrice = ProcessItem.Model.ListPrice;
            item.DiscountPrice = ProcessItem.Model.DiscountPrice;
            item.RetailPrice = ProcessItem.Model.RetailPrice;


            if (ProcessItem.Model.Subscription.Count > 0)
            {
                item.Subscriptions = new List<string>();
                foreach (var subscription in ProcessItem.Model.Subscription)
                {
                    if (!item.Subscriptions.Contains(subscription.Name))
                    {
                        item.Subscriptions.Add(subscription.Name);
                    }
                }
            }
            
            if (ProcessItem.Model.SalesRights.Count > 0)
            {
                item.SalesRights = new List<string>();
                foreach (var salesRights in ProcessItem.Model.SalesRights)
                {
                    item.SalesRights.Add(salesRights);
                }
            }

            if (ProcessItem.Model.UsageTerm.Count > 0)
            {
                item.UsageTerms.AddRange(ProcessItem.Model.UsageTerm.Select(t => t.Name));
            }

            

            
            MergeOwnershipData(item);

            if (ProcessItem.Model.SOP.Count > 0)
            {
                item.SOP.AddRange(ProcessItem.Model.SOP.Where(t => t.ScopeId == 0));
            }

            if (ProcessItem.Model.Groups != null && ProcessItem.Model.Groups.Any())
            {
                item.Group = ProcessItem.Model.Groups.FirstOrDefault();
            }
            else
            {
                var randomNumer = Util.GeneratorRandomInteger(1000000, 2000000);
                //Console.WriteLine(@"Isbn{ " + item.Isbn +@"} GroupId = " +randomNumer);
                //EventWriter.Write(EventTypeOption.Inform, new List<TypedItem>() {new TypedItem() {Key = "Isbn{" + item.Isbn +"} GroupId=" +randomNumer}  });
                item.Group = new Group {Id = randomNumer, Rank = 0};
            }
            item.MediaCount = ProcessItem.Model.NumberOfMedia;
            FilterAndMapStockLevel(item);

            item.SourceItemId = ProcessItem.Model.SourceItemId;
            item.Awards = ProcessItem.Model.Awards;
            item.Review = ProcessItem.Model.Review;
            item.Rating = ProcessItem.Model.Rating;
            //item.IsMarcAllowed = ProcessItem.Model.IsMarcAllowed;
            item.ProductLine = ProcessItem.Model.ProductLine;

            //if (ProcessItem.ImageUrls != null)
            //{
            //    item.Images = new List<ImageUrl>(ProcessItem.ImageUrls);
            //}
            //else
            //{
            //    //set it to default image
            //    var imageUrl = Constants.IsbnImageRootUrl;
            //    var environment = Constants.Environment.ToLower();
                
            //    item.Images = new List<ImageUrl>
            //    {
            //         new ImageUrl() { Name = "medium", Url = String.Format(CultureInfo.InvariantCulture, "{0}default/{1}_image_95x140.jpg", imageUrl,environment) },
            //         new ImageUrl() { Name = "large", Url = String.Format(CultureInfo.InvariantCulture, "{0}default/{1}_image_128x192.jpg", imageUrl,environment) },
            //         new ImageUrl() { Name = "x-large", Url = String.Format(CultureInfo.InvariantCulture, "{0}default/{1}_image_148x230.jpg", imageUrl,environment) },
            //         new ImageUrl() { Name = "xx-large", Url = String.Format(CultureInfo.InvariantCulture, "{0}default/{1}_image_512x512.jpg", imageUrl,environment) }
            //    };
            //}

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.HasMarc))
            {
                item.IsMarcAllowed = (bool)ProcessItem.SimpleProperties.GetValue<bool>(Constants.Facets.HasMarc);
            }

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.IsComingSoon))
            {
                item.IsComingSoon = (bool)ProcessItem.SimpleProperties.GetValue<bool>(Constants.Facets.IsComingSoon);
            }
            item.RecordingType = ProcessItem.Model.RecordingType;
            //item.MediaTypeDescription = ProcessItem.Model.MediaTypeDescription;
            FilterAndMapMediatypedescription(item);

            //isExclusive field            
            item.IsExclusive = ProcessItem.Model.IsExclusive;

            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.Keywords))
            {
                var data = ProcessItem.SimpleProperties.GetValue<List<string>>(Constants.Facets.Keywords);
                item.PopularKeywords = data;
            }

            if (ProcessItem.Model.ContentAdvisory.Any())
            {
                item.ContentAdvisory = ProcessItem.Model.ContentAdvisory.FirstOrDefault();
            }

            item.SystemTotalCopies = ProcessItem.Model.Ownership.Sum(t => t.TotalCopies);
            item.SystemHoldsCopies = ProcessItem.Model.Ownership.Sum(t => t.HoldsCopies);
            item.SystemCirculationCopies = ProcessItem.Model.Ownership.Sum(t => t.CirculationCopies);

            item.SeriesNo = ProcessItem.Model.NumberInSeries;
            item.MediaTypeBinding = ProcessItem.Model.MediaTypeBinding;
            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.Price))
            {
                var data = ProcessItem.SimpleProperties.GetValue<List<Pricing>>(Constants.Facets.Price);
                item.Pricing = data;
            }
            if (ProcessItem.SimpleProperties.Contains(Constants.Facets.Bundles))
            {
                var data = ProcessItem.SimpleProperties.GetValue<Bundle>(Constants.Facets.Bundles);
                item.Bundle = data;
            }
            return item;
        }

        private void FilterAndMapStockLevel(TitleIndex item)
        {
            if (!IsDigital(item))
            {
                item.StockLevel = StockLevel.Calculate(ProcessItem.Model.StockLevel).ToString();
            }
        }

        private void FilterAndMapMediatypedescription(TitleIndex item)
        {
            item.MediaTypeDescription = ProcessItem.Model.MediaTypeDescription;
            //ignore binding information for ebook
            if (IfEbook(item))
            {
                item.MediaTypeDescription = string.Empty;
            }
            
        }

        private static bool IfEbook(TitleIndex item)
        {
            return item.MediaType.ToLower() == "ebook";
        }

        private static bool IsDigital(TitleIndex item)
        {
            return item.MediaType.ToLower() == "ebook" || item.MediaType.ToLower() == "eaudio";
        }

        private void MergeOwnershipData(TitleIndex item)
        {
            try
            {
                if (ProcessItem.Model.Ownership.Count > 0)
                {
                    if (ProcessItem.Model.Expirations.Count > 0)
                    {
                        foreach (var ownershipitem in ProcessItem.Model.Ownership)
                        {
                            foreach (
                                var expiry in
                                ProcessItem.Model.Expirations.Where(expiryitem => expiryitem.TenantId == ownershipitem.PlatformTenantId))
                            {
                                if (ownershipitem.Expirations == null)
                                {
                                    ownershipitem.Expirations = new List<Expiration>();
                                }
                                ownershipitem.Expirations.Add(expiry);
                            }
                        }
                    }
                    if (ProcessItem.Model.SOP.Count > 0)
                    {
                        foreach (var ownershipitem in ProcessItem.Model.Ownership)
                        {
                            foreach (
                                var sop in
                                ProcessItem.Model.SOP.Where(
                                    sopitem => sopitem.ScopeId == ownershipitem.ScopeId))
                            {
                                if (ownershipitem.Sop == null)
                                {
                                    ownershipitem.Sop = new List<SOP>();
                                }
                                ownershipitem.Sop.Add(sop);
                            }
                        }
                    }
                    if (ProcessItem.Model.Holds.Count > 0)
                    {
                        foreach (var ownershipitem in ProcessItem.Model.Ownership)
                        {
                            foreach (
                                var holditem in
                                    ProcessItem.Model.Holds.Where(
                                        holditem => holditem.ScopeId == ownershipitem.PlatformTenantId))
                            {
                                ownershipitem.HoldsCopies = holditem.Count;
                            }
                        }
                    }
                    if (ProcessItem.Model.Circulations.Count > 0)
                    {
                        foreach (var ownershipitem in ProcessItem.Model.Ownership)
                        {
                            foreach (
                                var circulationitem in
                                    ProcessItem.Model.Circulations.Where(
                                        circulationitem => circulationitem.ScopeId == ownershipitem.PlatformTenantId))
                            {
                                ownershipitem.CirculationCopies = circulationitem.Count;
                            }
                        }
                    }
                    if (ProcessItem.Model.Subscription.Count > 0)
                    {
                        foreach (var ownershipitem in ProcessItem.Model.Ownership)
                        {
                            foreach (
                                var subscriptionOwnership in
                                    ProcessItem.Model.Subscription.Where(
                                        subscriptionitem => subscriptionitem.ScopeId == ownershipitem.ScopeId))
                            {
                                //ownershipitem.TotalCopies = 1;
                                if (ownershipitem.Subscriptions == null)
                                {
                                    ownershipitem.Subscriptions = new List<SubscriptionOwnership>();
                                }
                                ownershipitem.Subscriptions.Add(new SubscriptionOwnership
                                {
                                    Name = subscriptionOwnership.Name,
                                    Id = subscriptionOwnership.Id,
                                    StartDate = subscriptionOwnership.StartDate,
                                    EndDate = subscriptionOwnership.EndDate,
                                    Isbn = subscriptionOwnership.Isbn,
                                    ScopeId = subscriptionOwnership.ScopeId
                                });
                            }
                        }
                    }
                    item.Ownership.AddRange(ProcessItem.Model.Ownership);
                }

                //add susbscription to ownership
                if (ProcessItem.Model.Subscription.Count > 0)
                {
                    HashSet<int> hashset = new HashSet<int>();
                    foreach (
                        var subscriptionOwnership in
                            ProcessItem.Model.Subscription.Where(x => !item.Ownership.Any(y => x.ScopeId == y.ScopeId)))
                    {
                        if (hashset.Add(subscriptionOwnership.ScopeId))
                        {
                            item.Ownership.Add(new Ownership
                            {
                                ScopeId = subscriptionOwnership.ScopeId,
                                //TotalCopies = 1,
                                Subscriptions = new List<SubscriptionOwnership> { new SubscriptionOwnership
                                {
                                    Name = subscriptionOwnership.Name,
                                    Id = subscriptionOwnership.Id,
                                    StartDate = subscriptionOwnership.StartDate,
                                    EndDate = subscriptionOwnership.EndDate,
                                    Isbn = subscriptionOwnership.Isbn,
                                    ScopeId = subscriptionOwnership.ScopeId
                                }}
                            });
                        }
                        //else
                        //{
                        //    foreach (var ownership in item.Ownership.Where(t => t.ScopeId == subscriptionOwnership.ScopeId))
                        //    {
                        //        ownership.Subscriptions.Add(new Subscription { Name = subscriptionOwnership.Name });
                        //    }
                        //}

                    }
                }
            }

            catch (Exception exception)
            {
                Console.Write(exception);
            }
        }

        #endregion


    }

}
