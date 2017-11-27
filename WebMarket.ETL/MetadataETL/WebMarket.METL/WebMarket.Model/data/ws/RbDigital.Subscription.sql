SELECT CAST(S.[SubscriptionId] AS varchar(10)) AS Id
      ,S.Name
	  ,SI.Isbn
	  ,LS.LibraryId As EntityId
      ,LS.[BeginOn] AS StartDate
      ,LS.[EndOn] As EndDate
  FROM [DPCore].[holding].[LibrarySubscription] LS
  INNER JOIN holding.Subscription S
  ON LS.SubscriptionId = S.SubscriptionId
  INNER JOIN holding.SubscriptionIsbn SI
  ON SI.SubscriptionId = LS.SubscriptionId
  WHERE LS.EndOn >= Getdate()
