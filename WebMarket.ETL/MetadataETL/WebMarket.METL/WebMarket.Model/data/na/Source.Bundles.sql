SELECT Isbn, BundledItemNumber AS Id, TotalBundlePriceRnd AS Price, ItemNumber, TitleName AS Name 
FROM  tm_bundledproducts BP INNER JOIN stocklines S
ON BP.ItemNumber = S.Job_no and Isbn IS NOT NULL AND BP.status_code ='Available' AND BP.statusid=0 