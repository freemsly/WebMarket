--SELECT e.ISBN, MAX(b.BlurbContent)
--FROM dbo.stocklines AS s INNER JOIN dbo.tm_Edition AS e 
--ON s.stock_no = e.Stock_No INNER JOIN dbo.tm_Blurb AS b 
--ON e.TitleID = b.TitleID 
--WHERE (b.BlurbTypeID = 12) 
--AND (b.BlurbContent IS NOT NULL) AND (e.ISBN IS NOT NULL) AND e.ISBN <> ''
--GROUP BY e.ISBN

SELECT CAST(TItleId AS VARCHAR(10)) , BlurbContent FROM tm_Blurb WITH (NOLOCK) WHERE (BlurbTypeID = 12) 
AND (BlurbContent IS NOT NULL) 


