SELECT isbn, splitdata AS countrycodes   
FROM   ProductMetadata  CROSS APPLY
 dbo.fnSplitString(  [CountryCode],';')  cc 
INNER JOIN (SELECT DISTINCT country FROM LibraryAddress) LA
ON   cc.splitdata = LA.country
