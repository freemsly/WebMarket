
SELECT  OneclicklibraryId, EntityId FROM [Trilogy].Org.Account(NOLOCK) WHERE OneclicklibraryId IS NOT NULL AND EntityId IS NOT NULL
	   
SELECT DISTINCT TPD.EntityId,
				ISBN AS Isbn,
				SUM(ISNULL(Quantity,0)) AS TotalCopies,
				'' AS SubscriptionName,
				ISNULL(TPD.OneclickLibraryId,0) AS OneclickLibraryId
FROM [Trilogy].[dbo].[TrilogyPurchaseDetail](NOLOCK) TPD
WHERE TPD.EntityId IS NOT NULL AND ISBN IS NOT NULL AND OrderLineStatus <> 'Cancelled' AND LOWER([Format]) NOT IN ('ebook','eaudio','emagazine')
GROUP BY ISBN,TPD.EntityId,AccountNo,TPD.OneclickLibraryId

