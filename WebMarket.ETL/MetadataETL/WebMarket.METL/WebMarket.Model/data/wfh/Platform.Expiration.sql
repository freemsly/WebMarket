DECLARE @now Date = GETDATE()
SELECT libraryId, Isbn, EndOn,CircCap,CircCount
FROM holding.LibraryIsbn(NOLOCK)    
WHERE  EndOn IS NOT NULL AND EndOn >= @now AND (CircCap < 0 OR CircCount < CircCap) AND LibraryId <> 4021  