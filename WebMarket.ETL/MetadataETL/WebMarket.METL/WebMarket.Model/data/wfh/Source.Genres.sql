 SELECT [ItemCode],REPLACE(REPLACE(Genre,',',''),';',',')
FROM ProductMetadata PM WHERE [ItemCode] IS NOT NULL AND [Genre] IS NOT  NULL
    