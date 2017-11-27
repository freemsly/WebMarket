DECLARE @now Date = GETDATE()
SELECT libraryId, Isbn, COUNT(1) AS TotalCopies
FROM holding.LibraryIsbn    
WHERE  BeginOn <= @now AND (EndOn IS NULL OR EndOn > @now) 
AND (CircCap < 0 OR CircCount < CircCap) AND LibraryId <> 1062  
GROUP BY libraryId,Isbn
ORDER BY COUNT(1) DESC




