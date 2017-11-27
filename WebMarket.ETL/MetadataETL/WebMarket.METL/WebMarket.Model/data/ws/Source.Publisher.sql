--declare @publisher table (Name nvarchar(150), Label nvarchar(150), AccountNo nvarchar(12))

--insert into @publisher (Name,Label,AccountNo)	
--select distinct REPLACE(REPLACE([publisher],' & ',' and '),' Co.',' Company'), publisher, [item code] FROM [dbo].productmetadata(nolock) where publisher is not null order by [publisher]


--  select G.Name, 
--	stuff( (Select ',' + convert(nvarchar(12),DN.AccountNo) 
--	from @publisher as DN where DN.Name = G.Name 
--	order by G.Name for xml path('') ),1,1, '') as SourceId 
--	from @publisher as G group by Name
	
SELECT DISTINCT publisher,0 as SourceId FROM ProductMetadata WHERE publisher IS NOT NULL