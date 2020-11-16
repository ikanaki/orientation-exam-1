SELECT ShipsTable.Name 
FROM dbo.ShipsTable INNER JOIN dbo.PlanetsTable ON PlanetId=PlanetsTable.Id 
WHERE PlanetsTable.Name='Earth';

SELECT dbo.PlanetsTable.Name, COUNT(dbo.ShipsTable.Name)  
FROM dbo.ShipsTable INNER JOIN dbo.PlanetsTable ON PlanetId=PlanetsTable.Id
GROUP BY dbo.PlanetsTable.Name;