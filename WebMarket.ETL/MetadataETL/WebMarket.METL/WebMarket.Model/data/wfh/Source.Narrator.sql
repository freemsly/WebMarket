--SELECT DISTINCT TOP (100) PERCENT CASE cn.ContributorTypeID WHEN 842 THEN 'author' WHEN 906 THEN 'narrator' END AS contributorType, c.forenames AS FirstName, 
--                      c.surname AS LastName, c.mail_name AS MailName, cn.ContributorTypeID, convert(nvarchar(15),c.account_no) AS SourceId
--FROM         dbo.tm_Contributor(nolock) AS cn INNER JOIN
--                      dbo.customers(nolock) AS c ON cn.account_no = c.account_no
--WHERE     (cn.ContributorTypeID IN (906)) AND (c.mail_name IS NOT NULL)
--ORDER BY cn.ContributorTypeID, LastName, FirstName

SELECT 'narrator' AS contributorType, RTRIM(LEFT(Narrator, CHARINDEX(' ', Narrator))) AS FirstName,
  SUBSTRING(Narrator, CHARINDEX(' ', Narrator) + 1, 8000) AS LastName,'' AS MailName, 0 as ContributorTypeID, ISBN as SourceId 
  FROM ProductMetadata
UNION
SELECT 'author' AS contributorType, RTRIM(LEFT(Authors, CHARINDEX(' ', Authors))) AS FirstName,
  SUBSTRING(Authors, CHARINDEX(' ', Narrator) + 1, 8000) AS LastName,'' AS MailName, 0 as ContributorTypeID, ISBN as SourceId 
  FROM ProductMetadata