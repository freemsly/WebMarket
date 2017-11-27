--DECLARE @subscription table
--(
--    Isbn varchar(25),
--    SubscriptionName varchar(500),
--	SubscriptionId varchar(25)
--)
--INSERT INTO @subscription
--SELECT SL.JOB_NO As Id, SL.description AS Name, SL2.isbn As Isbn   
--    FROM stocklines(NOLOCK) SL 	
--	   INNER JOIN  Stoccomponent_stock(NOLOCK) SS on     SL.stock_no = SS.stock_no
--	   INNER JOIN  stocklines(NOLOCK) SL2 on     SL2.stock_no = SS.associated_stock
--	   WHERE  SL.Job_No LIKE 'SUBID%'
	   
--SELECT * FROM @subscription	     


--SELECT DISTINCT SL.JOB_NO As Id, SL.description AS Name, SL2.isbn As Isbn, CS.EntityID 
--FROM stocklines(NOLOCK) SL 	
--INNER JOIN  Stoccomponent_stock(NOLOCK) SS on     SL.stock_no = SS.stock_no
--INNER JOIN  stocklines(NOLOCK) SL2 on     SL2.stock_no = SS.associated_stock
--INNER JOIN CustomerSubscription(NOLOCK) CS ON SubscriptionId = SL.Job_No 
--WHERE  SL.Job_No LIKE 'SUBID%' AND EntityID IS NOT NULL AND OneClickLibraryID IS NOT NULL
--AND Enddate >=GETDATE() 

SELECT DISTINCT SubscriptionId As Id, SubscriptionName AS Name,  Isbn, EntityID, StartDate, EndDate 
FROM CustomerSubscription(NOLOCK) 
WHERE  SubscriptionId LIKE 'SUBID%' AND EntityID IS NOT NULL AND OneClickLibraryID IS NOT NULL AND Isbn IS NOT NULL


