--SELECT TitleOfCourtesy + ' ' + LastName + ' ' + FirstName as EmployeeFullName, 
--	Shippers.CompanyName, Orders.EmployeeID, Customers.ContactName, [Order Details].UnitPrice, Orders.Freight
IF OBJECT_ID('pr_GetOrderSummary', 'P') IS NOT NULL
    DROP PROCEDURE pr_GetOrderSummary
GO

CREATE PROCEDURE pr_GetOrderSummary 
@EmployeeID NVARCHAR(100) = NULL,
@CustomerID NVARCHAR(100) = NULL,
@StartDate DATE='1 Jan 1996',
@EndDate DATE ='31 Aug 1996'

AS
BEGIN
SELECT Employees.TitleOfCourtesy + ' ' +Employees. LastName + ' ' + Employees.FirstName as EmployeeFullName,
	Shippers.CompanyName as "Shipper CompanyName", Customers.CompanyName as "Customer CompanyName", FORMAT (o.OrderDate, 'dd-MM-yyyy') as Date, O.Freight, COUNT(o.OrderID) AS NumberOfOders,
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
GO

exec pr_GetOrderSummary
