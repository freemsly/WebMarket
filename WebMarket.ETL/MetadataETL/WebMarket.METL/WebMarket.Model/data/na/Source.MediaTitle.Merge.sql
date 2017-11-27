SELECT DISTINCT t.[titleid] AS [TitleId]
	,REPLACE(t.[title],CHAR(11),'') AS [Title]
	,REPLACE(t.[subtitle],CHAR(11),'') AS [Subtitle]
	,( SELECT CASE cn.ContributorTypeID WHEN 842 THEN 'author' WHEN 906 THEN 'narrator' END AS [@type],
				cn.[Rank] AS [@order],
				c.[forenames] AS [@fn],
				c.[surname] AS [@ln],
				c.[mail_name] AS [@mail],
				c.[organisation] AS [@organisation]
				FROM [dbo].[tm_Contributor](nolock) cn INNER JOIN [dbo].[Customers] c ON cn.account_no = c.account_no
				WHERE cn.TitleID = t.[TitleID]
				ORDER BY cn.ContributorTypeID,cn.[Rank] FOR XML PATH('Contributor'),Type ) AS 'Contributors'
	
	--,(select (ltrim(rtrim(bc.[BisacCodeId]))) AS [@code],bc.[Rank] AS [@order], abc.RBCode as [@genres] from [dbo].[tm_BisacCode](nolock) bc
	--	inner join ARSYBisacCode(nolock) abc on bc.BisacCodeId = abc.CodeId where bc.[TitleId] = t.[titleid]
	--			FOR XML PATH('Bisac'),Type ) AS Bisacs		
	,s.marc_allowed AS [IsMarcAllowed]
	,t.[PublicDomain]
	--,t.[date_published] AS [TitlePublicationDate]
	--,ISNULl(PublishDate, ee.date_published) AS [TitlePublicationDate]
	,s.date_published AS	[TitlePublicationDate]
	,t.[onixLanguageCode] AS [OnixLanguageId]
	,(SELECT TOP 1 [UserDescription] FROM [dbo].[onix_CodeListContent] WHERE ListItemID = t.[onixLanguageCode] ) AS [Language]
	,(SELECT TOP 1 ImprintName FROM tm_Imprint WHERE ImprintID =  te.[ImprintID] AND ImprintName != 'No Imprint') AS [Imprints]
	,(SELECT DISTINCT STUFF((SELECT ','+Prize_Description FROM ARSYPrizes P INNER JOIN tm_Prizes TP ON P.Prize_Id = Tp.Prize_ID WHERE Titleid = T.TitleID FOR XML PATH('')),1,1,'') ) AS [Awards]
	,COALESCE(te.[SeriesID],0) AS [SeriesId]
	,te.[No_In_Series] AS [NumberInSeries]
	,(select TOP 1 [Series_Title] FROM [arsySeriesTitle](NOLOCK) WHERE Entry_ID =te.SeriesID AND Series_Title <> 'Default Blank') AS Series
	--,(select TOP 1 [TargetAudience] AS [Audience] from [dbo].[ProductData](NOLOCK) where [Status] = 'Available' and  [TargetAudience] is not null AND [ISBN] = s.Isbn) AS [Audience] 
	,case t.[GenreID] when 1 then 'Young Adult' when 2 then 'Beginning Reader' when 3 then 'Children''s' when 4 then 'Adult' else 'Adult' end as [Audience]
	,s.isbn as [ISBN]
	,s.publisher as [Publisher]	
	--,COALESCE(s.date_published,e.PublishDate) as [EditionPublicationDate]
	,ISNULl(PublishDate, ee.date_published) AS [EditionPublicationDate]
	,ee.Exclusivity AS [IsExclusive]
	,e.FormatID as [FormatId]
	,e.BindingID AS [BindingId]
	,e.NoOfMedia AS [NumberOfMedia]
	,ISNULL(Physical_stock,0) AS [StockLevel]
	,e.BindingMethodID AS [BindingMethodId]
	,e.OrigCopyright AS [OriginalCopyright]
	,s.status_code AS [StatusCode]
	,s.[Job_No] AS [SourceItemId] -- z number
 	,(case when bf.[entry_id] in (17,29,31,34,36,29,37,38,44)
 		then (select formats from ARSYbinding_formats where Entry_ID = 44)
 		else bf.[formats]
 		end) AS [Format]
	,e.DRMType AS [HasDrm]
	,g.GenreType AS FnF
	,s.selling_Price As ListPrice
	,s.CoPub_Retail_Price AS RetailPrice	
	,(SELECT TOP 1 USPromoPrice FROM [dbo].[PromoPricingDetail](nolock) PPD
	INNER JOIN [dbo].[PromoPricingsummary](nolock) PPS
	ON PPD.SummaryEntryId = PPS.Entry_Id
	WHERE PPS.IsActive=1 AND PPD.Isactive=1 AND iscurrent=1 AND Isbn=s.isbn) AS DiscountPrice
	,(CASE EditionVersionTypeID WHEN 916 THEN 'abridged' WHEN 936 THEN 'unabridged' ELSE '' END) AS RecordingType
	--,(CASE WHEN s.publication_code= 'Kit' AND (Job_No like '3D%' OR Job_No like '3B%' OR Job_No LIKE '3L%' OR Job_No like '3G%' OR Job_No like '3M%' OR Job_No like 'DU%' OR Job_No like 'DV%' OR Job_No like 'DW%' ) 
	--		THEN (CASE e.BindingId WHEN 10 THEN 'Read Along Pack' WHEN 27 THEN 'Hanging Kit' WHEN 13 THEN 'Class Set'  ELSE '' END)
	--		WHEN s.publication_code= 'Video' AND (Job_No like 'DU%' OR Job_No like 'DV%' OR Job_No like 'DW%')
	--		THEN  (CASE e.BindingId WHEN 37 THEN 'Canadian Library'  WHEN 36 THEN 'US University' WHEN 5 THEN 'Library' ELSE  '' END)
	--		  ELSE '' END ) 
	--		AS MediaTypeDescription
	,s.publication_code AS MediaTypeDescription
	--,(Select TOP 1 SalesAudience FROM PublisherModel(NOLOCK) PM 
	--							 INNER JOIN HydratedModel(NOLOCK) HM 
	--							ON HM.stock_no = s.stock_no AND Hm.PublisherModelId = PM.Id)
	,(SELECT binding FROM tm_binding(NOLOCK) WHERE bindingid = e.BindingID AND  BindingID in (10,12,13,15,24,27,33,34,35,36,37,38,39,40,41,42,43)) AS MediaTypeBinding
	,(SELECT ProductType FROM ARSY_ProductType(NOLOCK) WHERE ProductTypeId = SPD.ProductTypeID) AS ProductLine
	,CAST(FLOOR(RAND(CHECKSUM(NEWID()))*(4-1)+1) AS INT) AS Rating
	--,(SELECT TOP 1 IG.GroupId 
	--	FROM [Trilogy].[dbo].[ISBNGroup] IG
	--	INNER JOIN Trilogy.dbo.ProductGroup PG
	--	ON IG.GroupId = PG.GroupId 
	--	WHERE ISBN = s.ISBN AND IG.GroupTypeId = 1 ) AS [GroupId]
	,(SELECT TOP 1 IG.GroupId AS [@id], '' AS [@name], DisplayRank AS [@rank]
		FROM [Trilogy].[dbo].[ISBNGroup] IG
		--INNER JOIN Trilogy.dbo.ProductGroup PG
		--ON IG.GroupId = PG.GroupId 
		WHERE ISBN = s.ISBN AND IG.GroupTypeId = 1 FOR XML PATH('Group'), TYPE) AS 'Groups'
	,(SELECT termname AS [@name]FROM Publisher Pub 
					  INNER JOIN PublisherModel  PM on PM.PublisherId = Pub.Id and  PM.MediaTypeId = CASE WHEN e.FormatId in (29,34,36,38) THEN 44 ELSE e.FormatID END
					  INNER JOIN HydratedModel   HM on HM.stock_no = s.stock_no and Hm.PublisherModelId = PM.Id and HM.IsActive =1 and HM.IsKit =0
					  INNER JOIN BusinessTerm BT on PM.BusinessTermId = BT.Id  WHERE  Pub.accountno = s.Publisher_Account_No FOR XML PATH('UsageTerm'),Type ) AS UsageTerm
FROM         dbo.tm_Title AS t WITH (nolock) INNER JOIN
                      dbo.tm_Edition AS e WITH (nolock) ON t.TitleID = e.TitleID 
					  INNER JOIN dbo.stocklines AS s WITH (nolock) ON e.Stock_No = s.stock_no 
					  INNER JOIN dbo.ARSYbinding_formats AS bf WITH (nolock) ON e.FormatID = bf.Entry_ID 
					  INNER JOIN dbo.ARSY_GenreType AS g ON t.GenreTypeID = g.GenreTypeID 
					  LEFT OUTER JOIN dbo.tm_EditionExtras AS ee WITH (nolock) ON ee.EditionID = e.EditionID 
					  LEFT OUTER JOIN dbo.tm_TitleExtras AS te WITH (nolock) ON t.TitleID = te.TitleID
					  LEFT OUTER JOIN dbo.GoodStock_location  AS GSL WITH (nolock) ON e.Stock_No= GSL.stock_no and GSL.Geographical_Location = 'Purchase Warehouse'
					  LEFT OUTER JOIN dbo.stocProductDetails AS SPD (NOLOCK) ON e.Stock_No = SPD.Stock_No
				       

         
WHERE  s.[isbn] IS NOT NULL  AND s.[publisher] IS NOT NULL  AND s.status_code IN ('Available','Remaining Stock Only')
AND bf.[entry_id] in (3,16,36,44,45,29,42,34,38,7) AND e.BindingID <> 7
--and (case when bf.[entry_id] in (17,29,31,34,36,29,37,38,44)
-- 		then (select formats from ARSYbinding_formats where Entry_ID = 44)
-- 		else bf.[formats]
-- 		end) IN ('eBook','eAudio')
--ORDER BY t.TitleID ASC
		
FOR XML PATH('MediaTitle')