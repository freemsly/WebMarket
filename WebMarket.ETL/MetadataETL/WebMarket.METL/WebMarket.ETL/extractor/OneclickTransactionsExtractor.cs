// <copyright company="Recorded Books Inc" file="OneclickTransactionsExtractor.cs">
// Copyright © 2015 All Rights Reserved
// </copyright>


using System.Configuration;
using WebMarket.Model;

namespace WebMarket.ETL
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    [Serializable]
    public static class OneclickTransactionsExtractor
    {

       private static BulkProfileOwnership BulkProfileOwnership { get; set; }

        public static void ProfileOwnership()
        {
            if (GetOneclickTransaction(1))
            {
                //Service.Post(BulkProfileOwnership);
            }
        }
        
        private static bool GetOneclickTransaction(int counter)
        {
            BulkProfileOwnership = new BulkProfileOwnership();
            using (var cn = new SqlConnection("metl.destination.oneclick"))
            //using (var cn = new SqlConnection(ConfigurationManager.ConnectionStrings["metl.destination.oneclick"].ConnectionString))
            {
                cn.Open();

                using (SqlCommand cmd = cn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = @"WITH Holds(libraryid,isbn,totalcopies)
                        AS
                        (	
	                        select PatronLibraryId AS LibraryId,Isbn,COUNT(1) AS totalcopies from DPCore.holding.PatronInterest(NOLOCK) where InterestTypeId=2
	                        AND CAST(BeginOn AS DATE)  = DATEADD(day,-" + counter + @" , Cast(GETDATE() as date)) AND EndOn > getdate() 
	                        GROUP BY PatronLibraryId,Isbn
                        )
                        , Circs(libraryid,isbn,totalcopies)
                        AS
                        (	
	                        select PatronLibraryId AS LibraryId,Isbn,COUNT(1) AS totalcopies from DPCore.holding.PatronInterest(NOLOCK)  where InterestTypeId=3
	                        AND CAST(BeginOn AS DATE)  = DATEADD(day,-" + counter + @" , Cast(GETDATE() as date)) AND EndOn > getdate() 
	                        GROUP BY PatronLibraryId,Isbn
                        )

                        SELECT L.SourceItemId AS ScopeId, Li.Isbn, 
                        SUM(ISNULL(Li.CircCount,0)) AS TotalCopies,
                        ISNULL(C.totalcopies,0) AS CirculationCopies,
                        ISNULL(H.totalcopies,0) AS HoldsCopies
                        FROM DPCore.holding.LibraryIsbn LI(NOLOCK)
                        INNER JOIN OneClick.LibraryProfile.Library L(NOLOCK)
                        ON Li.LibraryId =  l.Id
                        LEFT OUTER JOIN Holds H
                        ON Li.Isbn = H.isbn ANd li.LibraryId = H.libraryid
                        LEFT OUTER JOIN Circs C
                        ON Li.Isbn = C.isbn ANd li.LibraryId = C.libraryid
                        WHERE l.Id <> 1062 
                        GROUP BY SourceItemId, Li.isbn,C.totalcopies,H.totalcopies 
                        ";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new ProfileOwnership();
                            item.ScopeId = reader.GetInt32(0);
                            item.Isbn = reader.GetString(1);
                            item.TotalCopies = reader.GetInt32(2);
                            item.CirculationCopies = reader.GetInt32(3);
                            item.HoldsCopies = reader.GetInt32(4);
                            item.CreatedAt = DateTime.Now.Subtract(TimeSpan.FromDays(counter));
                            BulkProfileOwnership.ProfileOwnerships.Add(item);
                        }
                    }
                }
            }
            return BulkProfileOwnership.ProfileOwnerships.Count > 0;
        }

    }
}
