--Exception Handling
--temporary == batch
--example 1(:- divide by 0
BEGIN TRY

    SELECT 1/0 AS Error

END TRY
BEGIN CATCH

    SELECT
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_STATE() AS ErrorState,
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_LINE() AS ErrorLine,
        ERROR_PROCEDURE() AS ErrorProcedure,
        ERROR_MESSAGE() AS ErrorMessage

END CATCH


--example 2(:-primary key voilation
create table dbo.TestUSers(ID int primary key,name varchar(50))
insert into dbo.TestUSers values(1,'Alice')

begin try
insert into dbo.TestUSers values(1,'Bon')
end try

begin catch
select 
Error_Number() as ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_MESSAGE() AS ErrorMessage,
ERROR_LINE() AS ErrorLine,
ERROR_PROCEDURE() AS ErrorProcedure
       
end catch

--example 3(:-conversion error

begin try
declare @val int;
set @val=convert(int,'abc')
end try

begin catch
select
Error_Number() as ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_MESSAGE() AS ErrorMessage,
ERROR_LINE() AS ErrorLine,
ERROR_PROCEDURE() AS ErrorProcedure
end catch

--example 4(:- 

CREATE TABLE dbo.TableA (Id INT PRIMARY KEY, Val INT);
CREATE TABLE dbo.TableB (Id INT PRIMARY KEY, Val INT);

INSERT INTO dbo.TableA VALUES (1, 100);
INSERT INTO dbo.TableB VALUES (1, 200);

BEGIN TRY
    BEGIN TRANSACTION;
    UPDATE dbo.TableA SET Val = Val + 10 WHERE Id = 1;
    WAITFOR DELAY '00:00:10';  -- hold lock on TableA
    UPDATE dbo.TableB SET Val = Val + 10 WHERE Id = 1;
    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    SELECT
        ERROR_NUMBER() AS ErrorNumber,      -- 1205 if deadlocked
        ERROR_SEVERITY() AS ErrorSeverity,
        ERROR_STATE() AS ErrorState,
        ERROR_MESSAGE() AS ErrorMessage;
END CATCH;



-- Create Customers table
CREATE TABLE Customers
(
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(100)
);

-- Create Orders table with a foreign key reference to Customers
CREATE TABLE Orders1
(
    OrderID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT,
    OrderAmount DECIMAL(10, 2),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);


CREATE PROCEDURE InsertCustomerOrder
    @CustomerName NVARCHAR(100),
    @OrderAmount DECIMAL(10, 2)

AS
BEGIN
    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        -- Insert into Customers table
        INSERT INTO Customers (CustomerName)
        VALUES (@CustomerName);

        -- Simulate error by inserting an invalid CustomerID into Orders (e.g., 0, which does not exist)
        INSERT INTO Orders1 (CustomerID, OrderAmount)
        VALUES (1, @OrderAmount);

        -- If no error, commit transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback the transaction if an error occurs
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Error handling: Output error details using functions
        PRINT 'An error occurred during the transaction';

        PRINT 'Error Number: ' + CAST(ERROR_NUMBER() AS NVARCHAR(10));
        PRINT 'Error Message: ' + ERROR_MESSAGE();
        PRINT 'Error Severity: ' + CAST(ERROR_SEVERITY() AS NVARCHAR(10));
        PRINT 'Error State: ' + CAST(ERROR_STATE() AS NVARCHAR(10));
        PRINT 'Error Procedure: ' + ISNULL(ERROR_PROCEDURE(), 'N/A');
        PRINT 'Error Line: ' + CAST(ERROR_LINE() AS NVARCHAR(10));

        -- Optionally, re-throw the error
        -- THROW;
    END CATCH
END

exec InsertCustomerOrder 'ravi',123.34


select * from customers ;
select * from Orders1;

--now alter the sp using putting 0 like  this below 

alter PROCEDURE InsertCustomerOrder
    @CustomerName NVARCHAR(100),
    @OrderAmount DECIMAL(10, 2)
AS
BEGIN
    BEGIN TRY
        -- Start transaction
        BEGIN TRANSACTION;

        -- Insert into Customers table
        INSERT INTO Customers (CustomerName)
        VALUES (@CustomerName);

        -- Simulate error by inserting an invalid CustomerID into Orders (e.g., 0, which does not exist)
        INSERT INTO Orders1 (CustomerID, OrderAmount)
        VALUES (0, @OrderAmount);

        -- If no error, commit transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback the transaction if an error occurs
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        -- Error handling: Output error details using functions
        PRINT 'An error occurred during the transaction';

        PRINT 'Error Number: ' + CAST(ERROR_NUMBER() AS NVARCHAR(10));
        PRINT 'Error Message: ' + ERROR_MESSAGE();
        PRINT 'Error Severity: ' + CAST(ERROR_SEVERITY() AS NVARCHAR(10));
        PRINT 'Error State: ' + CAST(ERROR_STATE() AS NVARCHAR(10));
        PRINT 'Error Procedure: ' + ISNULL(ERROR_PROCEDURE(), 'N/A');
        PRINT 'Error Line: ' + CAST(ERROR_LINE() AS NVARCHAR(10));

        -- Optionally, re-throw the error
        -- THROW;
    END CATCH
END
exec InsertCustomerOrder 'mahesh',128.34


select * from customers ;
select * from Orders1;


--suppose i want to raise my own errpr with my own 
--severity error in sql server then i will use RaisErrror methor

Begin try
declare @age int =15;
if @age<18
RAISERROR('User must be atleast 18',16,5);

if @age<120
RAISERROR('User age is unreaisticly high',16,6);

if @age<null
RAISERROR('Age Cannot be null',16,7);
end try
begin catch
select 
        ERROR_NUMBER() AS ErrorNumber,
        ERROR_STATE() AS ErrorState,
    
        ERROR_MESSAGE() AS ErrorMessage

end catch




CREATE TABLE Employees23 (
    EmpID INT PRIMARY KEY,
    Name VARCHAR(50),
    Department VARCHAR(50),
    Salary INT
);

-- Insert sample data
INSERT INTO Employees23 (EmpID, Name, Department, Salary)
VALUES 
(1, 'Alice', 'HR', 60000),
(2, 'Bob', 'IT', 55000),
(3, 'Charlie', 'HR', 70000),
(4, 'David', 'IT', 60000),
(5, 'Eva', 'Finance', 75000),
(6, 'Frank', 'Finance', 62000),
(7, 'Grace', 'IT', 55000),
(8, 'Hannah', 'HR', 50000);


Task:
--We want to find all employees whose salary is greater than the average salary of their 
--respective department.


--joins 
select e.empid, e.name,e.department, e.salary
from employees23 e
join
(
    select department, avg(salary) as avgsalary
    from employees23
    group by department
) d on e.department = d.department where e.salary > d.avgsalary;



--Co-related sub query
select e.empid, e.name, e.department, e.salary
from employees23 e
where e.salary >
(
    select avg(e2.salary)
    from employees23 e2
    where e2.department = e.department
);


UPDATE Employees23
SET Salary = 55000
WHERE Name IN ('Bob', 'Grace');

-- Add one more IT employee with 55000 to make it three
INSERT INTO Employees23 (EmpID, Name, Department, Salary)
VALUES (9, 'Ivy', 'IT', 55000);

-- Add one HR employee with the same salary as Alice (60000)
INSERT INTO Employees23 (EmpID, Name, Department, Salary)
VALUES (10, 'Jack', 'HR', 60000);

--and after adding these changes fire the below commands
select EmpID, Name, Department,Salary , row_number() over(partition by department order by salary desc)as Rownum
from Employees23;

select EmpID, Name, Department,Salary , rank() over(partition by department order by salary desc)as Rownum
from Employees23;

select EmpID, Name, Department,Salary , dense_rank() over(partition by department order by salary desc)as Rownum
from Employees23;


--Indexes

create table depmgrdet(did char(3),dname varchar(10),mid char(3))
insert into depmgrdet values('d03','production','e02')
insert into depmgrdet values('d02','marketing',null)
select * from depmgrdet
create nonclustered index ix_depmgrdet on depmgrdet(did)
select * from depmgrdet
--can create another clustred index on another column
create table empmgrdet (empid varchar(30), empname varchar(30), mgr varchar(30))
create clustered index ix_empmgrdet on empmgrdet(empid) 
insert into empmgrdet values('e05','parag','e02')
insert into empmgrdet values('e04','smith','e05')
select * from empmgrdet

insert into depmgrdet values('d04','advt','e03')
insert into depmgrdet values('d05','sale','e03')
insert into depmgrdet values('d08','advt','e03')
insert into depmgrdet values('d07','sale','e03')
select * from depmgrdet


--can create non clustred index on same column
create nonclustered index ix_empmgrdet on empmgrdet(empid)
--can create more than one non clustred index
create nonclustered index ix_empmgrdetname on empmgrdet(empname)
--can create clusterd index if not exist on table if non clustred index exist
create clustered index ix_depmgrdetid on depmgrdet(did)



select * from depdet
create nonclustered index ix_deptncid on depdet(depid)
create clustered index ix_deptcid on depdet(depid)
create clustered index ix_deptcname on depdet(dname)












