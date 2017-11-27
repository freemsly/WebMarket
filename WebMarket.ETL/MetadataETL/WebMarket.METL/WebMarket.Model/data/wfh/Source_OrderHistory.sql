SELECT DISTINCT AccountNo,OrderNo,InvoiceNo,EntityID,OrderDate,InvoiceDate,ItemNo,OHD.ISBN,0 AS LibraryId,Quantity,NetAmount,PM.MediaFormat AS [Format], REPLACE(OrderMethod,'Web','WebMarket') AS OrderMethod
FROM OrderHistoryDetail OHD INNER JOIN ProductMetadata PM 
ON OHD.ISBN = PM.ISBN
WHERE OrderLineStatus IN ('Completed') AND OrderMethod='Web'