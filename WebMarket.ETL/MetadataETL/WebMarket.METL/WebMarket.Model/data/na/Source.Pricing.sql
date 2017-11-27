SELECT s.isbn as [ISBN]
	,'USD' As Currency
	,s.selling_Price As ListPrice
	,s.CoPub_Retail_Price AS RetailPrice	
	,(SELECT TOP 1 USPromoPrice FROM [dbo].[PromoPricingDetail](nolock) PPD
		INNER JOIN [dbo].[PromoPricingsummary](nolock) PPS
		ON PPD.SummaryEntryId = PPS.Entry_Id
		WHERE PPS.IsActive=1 AND PPD.Isactive=1 AND iscurrent=1 AND Isbn=s.isbn) AS DiscountPrice
	FROM dbo.tm_Title AS t WITH (nolock) INNER JOIN
         dbo.tm_Edition AS e WITH (nolock) ON t.TitleID = e.TitleID 
		INNER JOIN dbo.stocklines AS s WITH (nolock) ON e.Stock_No = s.stock_no 
		INNER JOIN dbo.ARSYbinding_formats AS bf WITH (nolock) ON e.FormatID = bf.Entry_ID 
		INNER JOIN dbo.ARSY_GenreType AS g ON t.GenreTypeID = g.GenreTypeID 
		LEFT OUTER JOIN dbo.tm_EditionExtras AS ee WITH (nolock) ON ee.EditionID = e.EditionID 
		LEFT OUTER JOIN dbo.tm_TitleExtras AS te WITH (nolock) ON t.TitleID = te.TitleID
		LEFT OUTER JOIN dbo.GoodStock_location  AS GSL WITH (nolock) ON e.Stock_No= GSL.stock_no
		LEFT OUTER JOIN dbo.stocProductDetails AS SPD (NOLOCK) ON e.Stock_No = SPD.Stock_No
		WHERE  s.[isbn] IS NOT NULL  AND s.[publisher] IS NOT NULL  AND s.status_code IN ('Available','Remaining Stock Only')
		AND bf.[entry_id] in (3,16,36,44,45,29,42,34,38,7) AND e.BindingID <> 7
