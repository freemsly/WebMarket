// <copyright company="Recorded Books Inc" file="BulkProfileOwnershipMDG.cs">
// Copyright © 2017 All Rights Reserved
// </copyright>

using MongoDB.Bson;
using System;
using System.Linq;
using WebMarket.Common;
using WebMarket.Model;

namespace WebMarket.Server
{
    using MongoDB.Driver;
    public class BulkProfileOwnershipMDG 
    {
        public BulkProfileOwnership Post(BulkProfileOwnership t)
        {
            var collection = new MongoClient(new MongoUrl("")).GetDatabase("").GetCollection<BsonDocument>("etlownershipdata");
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("Year", DateTime.Now.Year) & builder.Eq("Month", DateTime.Now.Month) &
                         builder.Eq("Day", DateTime.Now.Day - 1);
            if (collection.CountAsync(filter).Result == 0)
            {
                var groupedOwnership = t.ProfileOwnerships.GroupBy(g => g.ScopeId);
                var dateTime = t.ProfileOwnerships.First().CreatedAt;
                var ownershipList = groupedOwnership.Select(item => new BsonDocument()
                {
                    {"ScopeId", item.Key},
                    {
                        "Data", new BsonArray(item.Select(i => new BsonDocument()
                        {
                            {"Isbn", i.Isbn},
                            {"HoldsCopies", i.HoldsCopies},
                            {"TotalCopies", i.TotalCopies},
                            {"CirculationCopies", i.CirculationCopies},

                        }))
                    },
                    {"Day", dateTime.Day},
                    {"Month", dateTime.Month},
                    {"Year", dateTime.Year}
                }).ToList();
                ConsoleProcess.Start(t.GetType());
                foreach (var ownership in ownershipList)
                {
                    collection.InsertOneAsync(ownership);
                }
                ConsoleProcess.End(t.GetType());
            }
            return t;
        }
    }

}
