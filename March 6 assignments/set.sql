create function multiply(@x int,@y int)
returns int
as 
begin
return @x*@y;
end

select  dbo.multiply (23,45)

create table orders(orderid int primary key ,orderdate datetime,
whichcustomer varchar(10))

insert into orders values(101,'1996-08-01','c01')
insert into orders values(102,'1997-04-02','c01')
insert into orders values(103,'2012-08-01','c01')
insert into orders values(104,'2013-08-05','c02')
insert into orders values(105,'2014-08-01','c02')

select * from orders;
--write a function to find last or latest 
--order ordered by the given customer ..

create function fn_lastorder(@cust varchar(10))
returns datetime
as begin
    declare @lastdate datetime
    select @lastdate = max(orderdate)
    from orders
    where whichcustomer = @cust

    return @lastdate
end

select dbo.fn_lastorder('c01') as lastorderdate
select dbo.fn_lastorder('c02') as lastorderdate  



create table Books(
title_id varchar(10),
pages int,
qty_sold int)
insert into Books values('b0101',200,89)
insert into Books values('b0102',300,79)
insert into Books values('b0103',700,85)
select * from Books


---write a function on this table which will give me no of books sold
--based on id value u provide to the function ?

create function copies_sold(@title_id varchar(10))
returns int
as begin 
declare @quantity int;
select @quantity=0;
select @quantity=qty_sold from Books where title_id=@title_id;
return @quantity;
end 

select dbo.copies_sold('b0101')



--Table valued function

--syntax:
----------
--create function <function_name>(parameters_list)
--returns table as
--return (<any select command which will give me resultset>)

--procedure of execution(to call inline table function) :
---------------------------------------------------------
--select * from <function_name>(parameters_list)
    
-- Example on inline table valued function 
_______________________________________________

create table employee_info(
     ID          int,
     name        varchar (10),
     salary      int,
     start_date  datetime,
     city       varchar (10),
     region      char (1))

insert into employee_info
               values (1,  'Jason', 40420,  '02/01/94', 'New York', 'W')
 insert into employee_info 
               values (2,  'Robert',14420,  '01/02/95', 'Vancouver','N')
 insert into employee_info 
               values (3,  'Celia', 24020,  '12/03/96', 'Toronto',  'W')

select * from employee_info


--write an inline table valued function to find employees in particular
--region 

create function listemp(@region char)
returns table
as
return select *from employee_info where region=@region

select * from listemp('N');

--Example on multiline table valued function 
create function listemp221(@region char)
returns @table Table
(
    ID int not null,
    name varchar(20),
    city varchar(20),
    region char,
    message varchar(100)
    )
    as begin
    if exists (select ID,name,city,region  from employee_info where region=@region)
    begin
    insert into @table(ID,name,city,region,message)
    select ID,name,city,region, 'kkk'  from employee_info
    where region = @region
end
else
begin
--if no rows are found
 insert into @table(ID,name,city,region,message) values(0,'no data','null','n','no values in this region')
 end
 return;
    end
    select *from  listemp221('0')

    --simpler 
alter function listemp221(@region char)
returns @table Table
(
ID int not null,
name varchar(50),
city varchar(50),
reigon char
)
as begin
insert into @table(ID,name,city,reigon)
select ID,name,City,region from employee_info 
where region=@region
return;
end


--set

CREATE TABLE Employees_A (
    employee_id INT,
    first_name NVARCHAR(50),
    last_name NVARCHAR(50)
);

INSERT INTO Employees_A (employee_id, first_name, last_name)
VALUES (1, 'John', 'Doe'),
       (2, 'Jane', 'Smith'),
       (3, 'Alice', 'Johnson');

-- Table: Employees_B
CREATE TABLE Employees_B (
    employee_id INT,
    first_name NVARCHAR(50),
    last_name NVARCHAR(50)
);

INSERT INTO Employees_B (employee_id, first_name, last_name)
VALUES (2, 'Jane', 'Smith'),
       (3, 'Alice', 'Johnson'),
       (4, 'Bob', 'Brown');


-- Get all distinct employees from both tables
SELECT employee_id, first_name, last_name FROM Employees_A
UNION
SELECT employee_id, first_name, last_name FROM Employees_B;


-- Get the employees that are present in both tables
SELECT employee_id, first_name, last_name FROM Employees_A
INTERSECT
SELECT employee_id, first_name, last_name FROM Employees_B;



-- Get employees that are present in Employees_A but not in Employees_B
SELECT employee_id, first_name, last_name FROM Employees_A
EXCEPT
SELECT employee_id, first_name, last_name FROM Employees_B;


-- Combine UNION, INTERSECT, and EXCEPT in one query
-- Step 1: Find all employees from both tables using UNION
-- Step 2: Find common employees using INTERSECT
-- Step 3: Find employees that are only in Employees_A but not in Employees_B using EXCEPT
SELECT employee_id, first_name, last_name FROM Employees_A
UNION
SELECT employee_id, first_name, last_name FROM Employees_B
INTERSECT
SELECT employee_id, first_name, last_name FROM Employees_A
EXCEPT
SELECT employee_id, first_name, last_name FROM Employees_B;

select *from empl
select *from dept1
--give me all the employees who are working as clerk job or working in dept stales

select e1.ename from empl e1 join dept1 d1 on d1.deptno=e1.deptno where 
e1.job='CLERK'
union
select e1.ename from empl e1 join dept1 d1 on d1.deptno=e1.deptno where 
d1.dname='SALES'





