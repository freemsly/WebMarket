declare @series table(Name nvarchar(150), Label nvarchar(150), AccountNo nvarchar(12), MaxInSeries int)

insert into @series (Name,Label,AccountNo,MaxInSeries)

SELECT Distinct(
		REPLACE(
		REPLACE(
		REPLACE(	
		REPLACE(
		REPLACE(
		REPLACE(
		REPLACE(
		REPLACE(
		REPLACE(
		REPLACE(
		REPLACE(ast.[Series_Title],'Carlisles''','Carlisles')
			,'Do Not Disturb','Do Not Disturb') 
			,'Expecting!','Expecting')
			,'Mail-Order Bride','Mail Order Bride')
			,'Red-Hot Revenge','Red Hot Revenge')
			,'Royal &  Ruthless','Royal & Ruthless')
			,'Vieux Carr?','Vieux Carr,')
			,'McAllister''''s Gifts','MacAllisters Gifts')
			,'Macallister''''s Gifts','MacAllisters Gifts')
			,'The Billionaire''''s Club','The Billionaires Club')
			,'Undone!','Undone')),
		 ast.[Series_Title],
		 ast.[Entry_ID],
		 coalesce(ast.[Max_In_Series],0) 
from arsySeriesTitle ast where (LEN(ast.[Series_Title]) > 1) -- AND (ast.[Max_In_Series] > 0)

select G.Name,
	STUFF( (Select ',' + convert(nvarchar(12),DN.AccountNo)
	from @series as DN where DN.Name = G.Name
	order by G.Name for xml path('') ),1,1, '') as SourceId
	from @series as G group by Name