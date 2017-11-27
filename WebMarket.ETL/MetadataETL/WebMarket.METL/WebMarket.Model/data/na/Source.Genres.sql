/****** Script for SelectTopNRows command from SSMS  ******/
CREATE TABLE #Bisacs
(
	TitleId VARCHAR(10),
	Genre varchar(8000),
	[RANK] INT

)
INSERT INTO #Bisacs (TitleId, Genre,[RANK])
SELECT CAST([TitleId] AS VARCHAR(10)),RBCODE,[RANK]
FROM [Trilogy].[dbo].[tm_BisacCode]  BC
INNER JOIN [dbo].[ARSYBisacCode] ABC
ON BC.BisacCodeid = ABC.codeID

SELECT [TitleId]
	  ,Genre = STUFF((SELECT ', ' + Genre
           FROM #Bisacs b 
           WHERE b.TitleId = b1.TitleId 
		   order by [Rank]
          FOR XML PATH('')), 1, 2, '')
FROM #Bisacs b1
GROUP BY [TitleId]

DROP TABLE #Bisacs
  