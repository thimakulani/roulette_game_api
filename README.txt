REQUIREMENTS


.NET 5,
Dapper.
SQLLITE,
Microsoft.Data.Sqlite 7.0.0,
Swagger UI,
====================================================================================================================================================================
//GetOrderSummary



USE [Northwind]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetOrderSummary]    Script Date: 2022/11/22 16:34:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[pr_GetOrderSummary] 
@EmployeeID NVARCHAR(100) = NULL,
@CustomerID NVARCHAR(100) = NULL,
@StartDate DATE='1 Jan 1996',
@EndDate DATE ='31 Aug 1996'

AS
BEGIN
SELECT Employees.TitleOfCourtesy + ' ' +Employees. LastName + ' ' + Employees.FirstName as EmployeeFullName,
	Shippers.CompanyName, Customers.CompanyName as CustomerCompanyName, FORMAT (o.OrderDate, 'dd-MM-yyyy') as Date, O.Freight, COUNT(o.OrderID) AS NumberOfOders,
	Round(Sum(p.UnitPrice*quantity*(1-Discount)), 2) as TotalOrderValue, COUNT(p.ProductID) AS NumberOfDifferentProducts
	
FROM Orders AS o
INNER JOIN Employees ON o.EmployeeID = Employees.EmployeeID
INNER JOIN Shippers ON o.ShipVia = Shippers.ShipperID
INNER JOIN Customers ON o.CustomerID = Customers.CustomerID
INNER JOIN [Order Details] ON o.OrderID = [Order Details].OrderID
--INNER JOIN Products ON [Order Details].ProductID = Products.ProductID
JOIN ( SELECT DISTINCT *
			FROM Products
		) p ON [Order Details].ProductID = p.ProductID
WHERE (Employees.EmployeeID = @EmployeeID OR @EmployeeID IS NULL) AND
	(Customers.CustomerID = @CustomerID OR @CustomerID IS NULL)
	--AND Products.ProductName IN (SELECT DISTINCT ProductName
	--							FROM Products
	--							WHERE [Order Details].ProductID=Products.ProductID
	--							)

GROUP BY Employees.TitleOfCourtesy, Employees.FirstName, Employees.LastName, Shippers.CompanyName, 
Customers.CompanyName, o.OrderDate, O.Freight, o.OrderID
END
