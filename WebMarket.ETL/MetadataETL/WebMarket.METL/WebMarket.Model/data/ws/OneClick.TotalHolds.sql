
SELECT Isbn, PatronLibraryId AS LibraryId, Count(1) AS TotalHolds 
FROM [DPCore].[holding].[PatronInterest](NOLOCK)  PT
WHERE InterestTypeId=2 AND EndOn > GETDATE()
GROUP BY Isbn,PatronLibraryId
ORDER BY COUNT(1) DESC