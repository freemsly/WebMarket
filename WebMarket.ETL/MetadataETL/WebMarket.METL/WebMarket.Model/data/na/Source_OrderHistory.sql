﻿SELECT DISTINCT AccountNo,OrderNo,InvoiceNo,EntityID,OrderDate,InvoiceDate,ItemNo,ISBN,OneclickLibraryID AS LibraryId,Quantity,NetAmount,[Format],OrderMethod 
FROM [Trilogy].[dbo].[TrilogyPurchaseDetail](NOLOCK) WHERE OrderLineStatus IN ('Completed')