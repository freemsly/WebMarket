WITH CustomersCountryCode(CountryCode)
AS
(
select distinct Nationality from customers WHERE Nationality IS NOT NULL AND type_code='Library'
)

select DISTINCT
    e.Isbn,
    ER.CountryCode
from tm_editionrights(NOLOCK) ER
INNER JOIN dbo.tm_Edition AS e WITH (nolock) ON ER.EditionId = e.EditionId 
INNER JOIN CustomersCountryCode CCC ON ER.CountryCode =  CCC.CountryCode
WHERE IsActive=1 AND e.Isbn IS NOT NULL

