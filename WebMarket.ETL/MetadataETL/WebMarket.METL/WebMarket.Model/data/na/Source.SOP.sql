----WITH QuarterlyTitles([COPClassID],[Title],[SeoTitle],[ReleaseYear],[Release],[ReleaseType], [Job_no])
----AS (
----SELECT 
 
----ce.[COPClassID],
----((cast(ce.[ReleaseQuarter] AS NVARCHAR(10))
----+CASE
----WHEN ce.[ReleaseQuarter]%10=1 AND ce.[ReleaseQuarter]%100<>11 THEN 'st'
----WHEN ce.[ReleaseQuarter]%10=2 AND ce.[ReleaseQuarter]%100<>12 THEN 'nd'
----WHEN ce.[ReleaseQuarter]%10=3 AND ce.[ReleaseQuarter]%100<>13 THEN 'rd'
----ELSE 'th'
----END) + ' Quarter ' + cast(ce.[ReleaseYear] AS NVARCHAR(4)) + ' ' + ac.[COPClass] + ' COP') AS [Title],
----cast(replace(((cast(ce.[ReleaseQuarter] AS NVARCHAR(10))
----+CASE
----WHEN ce.[ReleaseQuarter]%10=1 AND ce.[ReleaseQuarter]%100<>11 THEN 'st'
----WHEN ce.[ReleaseQuarter]%10=2 AND ce.[ReleaseQuarter]%100<>12 THEN 'nd'
----WHEN ce.[ReleaseQuarter]%10=3 AND ce.[ReleaseQuarter]%100<>13 THEN 'rd'
----else 'th'
----end) + ' Quarter ' + cast(ce.[ReleaseYear] AS NVARCHAR(4)) + ' ' + ac.[COPClass]),' ','-') AS NVARCHAR(100)) AS [SeoTitle],
----ce.[ReleaseYear],
----ce.[ReleaseQuarter] AS [Release],
----cast('Quarterly' AS NVARCHAR(10)) AS [ReleaseType],
---- [Job_no]
----FROM [dbo].[tm_COPClassEdition] AS ce
----inner join [dbo].[ARSYCOPClass] AS ac ON ce.[COPClassID]=ac.[COPClassID]
----WHERE 
---- ce.[COPClassID] NOT in (5,29) AND 
---- (ce.[ReleaseYear] IS NOT NULL AND ce.[ReleaseYear] BETWEEN 2000 AND 2100) and
----(ce.[ReleaseQuarter] IS NOT NULL AND ce.[ReleaseQuarter] BETWEEN 1 AND 4)
----),
----MonthlyTitles([COPClassID],[Title],[SeoTitle],[ReleaseYear],[Release],[ReleaseType], [Job_no])
----AS (
----SELECT 
 
----ce.[COPClassID],
----(datename(month,dateadd(month,ce.[ReleaseMonth],0)-1) + ' ' + cast(ce.[ReleaseYear] AS NVARCHAR(4)) + ' ' + ac.[COPClass] + ' COP') AS [Title],
----cast(replace((datename(month,dateadd(month,ce.[ReleaseMonth],0)-1) + ' ' + cast(ce.[ReleaseYear] AS NVARCHAR(4)) + ' ' + ac.[COPClass]),' ','-') AS NVARCHAR(100)) AS [SeoTitle],
----ce.[ReleaseYear],
----ce.[ReleaseMonth] AS [Release],
----cast('Monthly' AS NVARCHAR(10)) AS [ReleaseType],
---- [Job_no]
----FROM [dbo].[tm_COPClassEdition] AS ce
----inner join [dbo].[ARSYCOPClass] AS ac ON ce.[COPClassID]=ac.[COPClassID]
----WHERE 
----ce.[COPClassID] NOT in (5,29) AND 
---- (ce.[ReleaseYear] IS NOT NULL AND ce.[ReleaseYear] BETWEEN 2000 AND 2100) AND
----(ce.[ReleaseMonth] IS NOT NULL AND ce.[ReleaseMonth] BETWEEN 1 AND 12)
----),
----CopTitles([COPClassID],[Title],[SeoTitle],[ReleaseYear],[Release],[ReleaseType], [Job_no])
----AS (
----SELECT q.[COPClassID],q.[Title],[SeoTitle],q.[ReleaseYear],q.[Release],q.[ReleaseType], [Job_no]
----FROM [QuarterlyTitles] AS q
----union
----SELECT m.[COPClassID],m.[Title],[SeoTitle],m.[ReleaseYear],m.[Release],m.[ReleaseType], [Job_no]
----FROM [MonthlyTitles] AS m
----)
----SELECT CAST(c.[COPClassID] AS VARCHAR(10)),c.[Title], Isbn,c.[SeoTitle],c.[ReleaseYear],c.[Release],cast(c.[ReleaseType] AS NVARCHAR(10)) AS [ReleaseType], ISNULL(Physical_stock,0) AS [StockLevel],Geographical_Location
----FROM [CopTitles] AS c
----INNER JOIN stocklines(nolock) S
----LEFT OUTER JOIN dbo.GoodStock_location  AS GSL WITH (nolock) ON s.Stock_No= GSL.stock_no AND GSL.Geographical_Location = 'COP Warehouse'
----ON C.Job_no = S.Job_No
----WHERE C.Job_No IS NOT NULL AND S.Job_No IS NOT NULL 
----ORDER BY c.[Title]

select A.SopID As Id ,a.SopName AS Name,A.SOPISBN as Isbn, A.SOPTemplateName, A.ReleaseYear AS [year], A.SOPFrequency AS Frequency, A.SOPGroup, A.SOPFormat as [Format], a.EntityID  from SOPData A  where Selection=1
AND A.GroupEndDate>=(GETDATE()) AND A.SOPISBN IS NOT NULL
UNION ALL
select A.SopID As Id ,a.SopName AS Name,A.SOPISBN as Isbn, A.SOPTemplateName, A.ReleaseYear AS [year], A.SOPFrequency AS Frequency, A.SOPGroup, A.SOPFormat as [Format], B.EntityID  from SOPData A CROSS JOIN 
SOPData B
WHERE A.EntityID=0 AND (A.SOPID = B.SOPID and A.SOPGroupID = B.SOPGroupID ) AND (A.Selection=1 AND B.Selection=2) AND B.EntityID <> 0  AND A.GroupEndDate >= GETDATE() AND B.GroupEndDate >= GETDATE()
 AND A.SOPISBN IS NOT NULL AND A.SOPISBN IS NOT NULL