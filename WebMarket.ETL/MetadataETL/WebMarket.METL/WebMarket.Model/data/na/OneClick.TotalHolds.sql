--SELECT FTD.ISBN AS Isbn, PL.LibraryId, COUNT(1) AS TotalHolds 
--FROM Oneclick.Holdings.PatronTitleTransactionTable(NOLOCK) PTT
--INNER JOIN ProductCatalog.FullTitleDetails(NOLOCK) FTD
--ON PTT.TitleId = FTD.Id
--INNER JOIN dbo.PatronLibraries PL
--ON PL.PatronID = PTT.PatronId
--WHERE TransactionTypeId=1 and TerminationTimestamp is NULL
--GROUP BY FTD.Isbn, Pl.LibraryID
--ORDER BY COUNT(1) DESC

SELECT Isbn, PatronLibraryId AS LibraryId, Count(1) AS TotalHolds 
FROM [DPCore].[holding].[PatronInterest](NOLOCK)  PT
WHERE InterestTypeId=2 AND EndOn > GETDATE()
GROUP BY Isbn,PatronLibraryId
ORDER BY COUNT(1) DESC