--SELECT 
--      [Id]
--      ,[SourceItemId] 
--      ,[ISBN]           
--      ,[HasImages]
--      ,[S3FolderName]
--      ,[ActivationTimeStamp]      
--      ,[ReleaseDate]
--      ,[PublicationDate]      
--      ,[ScheduledReleaseDate]
--      ,[PreReleaseDate]
--      ,[IsPreRelease]
--      ,COALESCE([Imprint],'No Imprint') AS [Imprint]
--      ,[PreviewFileName]
--      ,[TargetAudience]
--      ,[TitleTypeId]
--      ,[DrmEnabled] as [HasDrm]
--      ,[ActualDurationMinutes]
--      ,[DeclaredDurationMinutes]
--      ,[MediaQty]
--      ,[MediaFormat]
--	  ,COALESCE([HasAttachment],0) AS [HasAttachments]
--	  ,case charindex(' ',[Description],200) when 0 then [Description] else substring([Description],0,charindex(' ',[Description],200)) + '...' END AS [ShortDescription]
--	  ,(select SUM(cast(filesize as bigint)) From ProductCatalog.EAudioChapter(nolock) where EAudioId=p.Id) as FileSize
--  FROM [ProductCatalog].[FullTitleDetails] p
--  WHERE     ((IsSaleable = 1) OR (IsLendable = 1)) AND [ISBN]  IS NOT NULL


SELECT SourceItemId,	
		Isbn, 
		S3Folder AS S3FolderName,
		CAST(NULLIF(XmlDoc.value('(/TitleIndex/ActivatedOn)[1]', 'varchar(30)'), '') AS DATETIME)  AS ActivationTimeStamp,
		XmlDoc.value('(/TitleIndex/IsPreRelease)[1]', 'BIT') AS IsPreRelease,
		Imprint,
		XmlDoc.value('(/TitleIndex/PreviewFile)[1]', 'VARCHAR(250)') AS PreviewFileName, 
		XmlDoc.value('(/TitleIndex/HasDrm)[1]', 'BIT') AS HasDrm,
		XmlDoc.value('(/TitleIndex/HasAttachments)[1]', 'BIT') AS HasAttachments, 
		ActualDurationMinutes AS ActualDurationMinutes
FROM metadata.Book(NOLOCK)
WHERE SourceItemId IS NOT NULL 
AND Isbn IS NOT NULL 
AND SourceItemId <> '' 
AND ISBN <> ''