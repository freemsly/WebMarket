SELECT DISTINCT OneClickdigitalLibraryID AS OneclicklibraryId,EntityId FROM LibraryAccount(NOLOCK) WHERE EntityID IS NOT NULL AND OneClickdigitalLibraryID IS NOT NULL

SELECT DISTINCT ISNULL(CAST(LA.EntityId AS INT),0),
				ISBN AS Isbn,
				SUM(CAST(Quantity AS INT)) AS TotalCopies,
				--((SELECT DISTINCT STUFF((SELECT ','+SubscriptionName FROM #temp_subscription WHERE AccountId = AccountNo AND ISBN =TPD.ISBN   FOR XML PATH('')),1,1,''))) AS SubscriptionName,
				'' AS SubscriptionName,
				ISNULL(CAST(OneclickLibraryId AS INT),0) AS OneclickLibraryId
FROM [dbo].OrderHistoryDetail(NOLOCK) ORH
INNER JOIN dbo.LibraryAccount LA ON ORH.AccountNo =  LA.[SAPLibraryAccountNumber]
WHERE ISBN IS NOT NULL
GROUP BY ISBN,La.EntityId,OneclickLibraryId