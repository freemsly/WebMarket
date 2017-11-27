--SELECT FTD.ISBN AS Isbn, PL.LibraryID, COUNT(1) AS TotalCirculations 
--FROM Oneclick.Holdings.PatronTitleTransactionTable(NOLOCK) PTT
--INNER JOIN Oneclick.ProductCatalog.FullTitleDetails(NOLOCK) FTD
--ON PTT.TitleId = FTD.Id
--INNER JOIN Oneclick.dbo.PatronLibraries PL
--ON PL.PatronID = PTT.PatronId
--WHERE TransactionTypeId in(2,6) and TerminationTimestamp is NULL
--GROUP BY FTD.Isbn, Pl.LibraryID
--ORDER BY COUNT(1) DESC

SELECT Isbn, PatronLibraryId AS LibraryId, Count(1) AS TotalCirculations 
FROM [DPCore].[holding].[PatronInterest](NOLOCK)  PT
WHERE InterestTypeId=3 AND EndOn > GETDATE()
GROUP BY Isbn,PatronLibraryId
ORDER BY COUNT(1) DESC

