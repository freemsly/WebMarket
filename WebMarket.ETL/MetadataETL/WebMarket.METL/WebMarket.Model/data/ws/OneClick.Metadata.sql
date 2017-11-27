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