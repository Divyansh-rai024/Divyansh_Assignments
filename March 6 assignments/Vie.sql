select * from Customers;
select * from Orders;

create view vw_CustomerOrderView as 
select c.CustomerID,c.CompanyName,c.City ,
o.OrderID,o.OrderDate,o.Freight from Customers c 
inner join Orders o on c.CustomerID=o.CustomerID;

select * from vw_CustomerOrderView where City='London'


select *from [dbo].[view_2]


select *from Orders;
Select  *from [Order Details];

Select  *from Products;


CREATE VIEW vw_OrderLineItems AS
SELECT
    o.OrderID,
    o.OrderDate,
    c.CompanyName AS CustomerName,
    p.ProductName,
    od.Quantity,
    od.UnitPrice,
    od.Quantity * od.UnitPrice AS LineTotal
FROM Orders o
INNER JOIN Customers c ON o.CustomerID = c.CustomerID
INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
INNER JOIN Products p ON od.ProductID = p.ProductID;


SELECT OrderID, ProductName, LineTotal
FROM vw_OrderLineItems
WHERE CustomerName = 'Alfreds Futterkiste';


select *from[dbo].[View4]

-- This UPDATE goes to the Orders table
-- here i am updating a first view which i created it is possible 
-- the effect is seen vice versa 
select * from vw_CustomerOrderView

UPDATE vw_CustomerOrderView
SET Freight = 50.00
WHERE OrderID = 10248;



CREATE VIEW vw_OrderSummary
WITH SCHEMABINDING
AS
SELECT
    o.OrderID,
    o.OrderDate,
    c.CompanyName,
    SUM(od.Quantity * od.UnitPrice) AS TotalAmount
FROM dbo.Orders o
INNER JOIN dbo.Customers c ON o.CustomerID = c.CustomerID
INNER JOIN dbo.[Order Details] od ON o.OrderID = od.OrderID
GROUP BY o.OrderID, o.OrderDate, c.CompanyName;

select * from vw_OrderSummary

UPDATE vw_OrderSummary
SET TotalAmount = 50.00
WHERE OrderID = 10692;

drop table Orders;

drop table [Order Details];

drop view vw_OrderSummary;




