SELECT [ItemCode] AS [TitleId]
	,REPLACE(ProductName,CHAR(11),'') AS [Title]
	,REPLACE(SubTitle,CHAR(11),'') AS [Subtitle]
	,(Select [ContentAdvisory-Sex] AS [@sex], [ContentAdvisory-Language] AS [@language], [ContentAdvisory-Violence] AS [@violence] FOR XML PATH('ContentAdvisory'),Type) AS 'ContentAdvisory'
	,( SELECT CASE [TYPE] WHEN 1 THEN 'author' WHEN 3 THEN 'narrator' END AS [@type],
				c.[TitleGroupRank] AS [@order],
				c.FirstName AS [@fn],
				c.LastName AS [@ln],
				'' AS [@organisation],
				'' AS [@mail]				
				FROM [dbo].[Contributors] c(nolock) 
				WHERE ItemCode = p.[ItemCode]
				ORDER BY [TitleGroupRank] FOR XML PATH('Contributor'),Type ) AS 'Contributors'
	
	,Marc21 AS [IsMarcAllowed]
	,[Language]  AS [Language]
	,[Imprints] AS [Imprints]
	,[Awards] AS [Awards]
	,ScheduledReleaseDate AS [TitlePublicationDate]
	,SeriesPositionNum AS [NumberInSeries]
	,SeriesName AS Series
	,TargetAudience AS [Audience] 
	,isbn as [ISBN]
	,publisher as [Publisher]	
	,[MediaQuantity] AS [NumberOfMedia]
	--,ISNULL(Physical_stock,0) AS [StockLevel]
	,[ItemCode]  AS [SourceItemId] -- z number
 	,MediaFormat AS [Format]
	,DRM AS [HasDrm]
	,IsFiction AS FnF
	,IsExclusive as [IsExclusive]
	,(SELECT TOP 1 ListPrice FROM dbo.Price WHERE Isbn = p.Isbn) As ListPrice
	,(CASE IsAbridged WHEN 1 THEN 'abridged' WHEN 0 THEN 'unabridged' ELSE '' END) AS RecordingType
	--,(CASE WHEN s.publication_code= 'Kit' AND (Job_No like '3D%' OR Job_No like '3B%' OR Job_No LIKE '3L%' OR Job_No like '3G%') THEN (CASE e.BindingId WHEN 10 THEN 'Read Along Pack' WHEN 27 THEN 'Hanging Kit' WHEN 13 THEN 'Class Set' ELSE '' END) ELSE '' END ) AS MediaTypeDescription
	--,(SELECT ProductType FROM ARSY_ProductType(NOLOCK) WHERE ProductTypeId = SPD.ProductTypeID) AS ProductLine
	,CAST(FLOOR(RAND(CHECKSUM(NEWID()))*(4-1)+1) AS INT) AS Rating
	,(SELECT TOP 1 PM.TitleGroupId AS [@id], '' AS [@name], CAST(ISNUll(PM.TitleGroupRank,0) AS INT) AS [@rank]
		FROM ProductMetadata PM WHERE ISBN = p.ISBN FOR XML PATH('Group'), TYPE) AS 'Groups'
	,(SELECT BusinessTermName AS [@name]FROM [WebProductData] WHERE  Isbn = p.isbn FOR XML PATH('UsageTerm'),Type ) AS UsageTerm
FROM  ProductMetadata p
WHERE p.[isbn] IS NOT NULL  AND p.[publisher] IS NOT NULL  AND p.status = 'Available'		
FOR XML PATH('MediaTitle')