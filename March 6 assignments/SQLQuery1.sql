create database CG
use CG;
create table student(stuid int primary key, studname varchar(30), location varchar(50))

--normal insert
insert into student values(101,'rav','chennai');

select *from student

--inserting as per my order
insert into student(location,stuid,studname)values('banglore',102,'mahesh')

--partial insert
insert into student(stuid,studname)values(103,'suresh')

--multiple input
insert into student values(104,'kiran','delhi'),(105,'sita','noaida'),(106,'sashti','hyderabad')

--updating single column in a single row(don't update primaery key)
update student set studname='joseph' where stuid=102;

--updating multiple columns in a single row
update student set studname='david',location='Vizag'  where stuid=104;

--updating multiple rows ina single column
--update student set location='kolkata' where stuid in (102,104,105);


--deleting single row 
delete from student where stuid=105;
--deleting multiple rows
delete from student where stuid in (102,104)
--deleting all rows 
delete from student;
--delete student ; thi is also okay 


--difference in truncate and delete command

create table empinfo(empid int identity(1,1) primary key,empname varchar(40))

insert into empinfo values('Ravi');
insert into empinfo values('sita');
insert into empinfo values('chandan');

select *from empinfo;

--1st difference is you can't use where clause with truncate and it's a ddl command whereas
--delete can be used with where and it is DML commmand

--truncate empinfo where empname='sita'; --not possible

delete empinfo

insert into empinfo values('jagdish');

truncate table empinfo

insert into empinfo values('Mohan');
-- logs are maintained when delete command is used
--where as for teuncate it is not maintained it starts from start

--constraints
--synyax--  constraint  <constraintname> typeofconstraint(collist)
--1)null or not null (column level constraint)applied beside column

create table demo1(id int not null, fname varchar(30), mname varchar(30), lname varchar(20))

--not null means don't forgot the value but duplicates are allowed

--insert into demo1 values(null,null,null,null);--error

insert into demo1 values(101,null,null,null)
insert into demo1 values(101,null,null,null)--duplicate

insert into demo1 values(101,null,null,'janaki')

--you can't use  this null as table level constrait
--it is used as column level only

--2)Unique constraint
--can't enter duplicate values but can enter null values but
--number of null depend on system or type of database being used
--can be applied both as column and table level

create table demo2(id int not null, fname varchar(30), mname varchar(30), lname varchar(20),constraint uk1 unique(mname,lname))



insert into demo2 values(101,'kiran',null,'kumar');--fine
insert into demo2 values(102,'kiran',null,'das')--fine
insert into demo2 values(102,'kiran',null,'das')--error
insert into demo2 values (103,'kiran',null,'kishore')--fine
insert into demo2 values (null,'kiran',null,'kishore1')--fine
insert into demo2 values (null,'kiran',null,'kishore2') --error

select *from demo2;



--)Primary key constraint

--------------
-- A combination of not null and unique is nothing but primary key 
-- this can be applied both as column level and table level 
-- remeber only one primary key will be there for a table 
-- means in one primary there can be multiple columns

-- column level primary key
create table demo3(id int primary key,fname varchar(30) not null,
mname varchar(30) null,lname varchar(40));
insert into demo3 values(101,'kiran',null,null);
insert into demo3 values(101,'kiran',null,null);--error
insert into demo3 values(null,'kiran',null,null);--error

-- table level
create table demo4(id int ,fname varchar(40) not null,
mname varchar(30),lname varchar(40),
constraint pk1 primary key(id))

insert into demo4 values(101,'kiran',null,null);
insert into demo4 values(101,'kiran',null,null);--error
insert into demo4 values(null,'kiran',null,null);--error


--creating a composite primary key

create table demo5(id int, fname varchar(40) not null, mname varchar(40) null,lname varchar(40)
constraint pk44 primary key (id,fname));

insert into demo5 values(101,'kiran',null,null);--fine

insert into demo5 values(101,'mahesh',null,null);--fine

insert into demo5 values(102,'mahesh',null,null);--fine

--error

create table demo6(id int, fname varchar(40) not null, mname varchar(40) null,lname varchar(40)
constraint pk44 primary key (id,fname));

--4) check constraint -values are checked based on conditions

create table bankdemo(bankid int primary key,bankname varchar(40),balance int ,balance int check(balance>2000));

create table bankdemo2(bankid int primary key,bankname varchar(40),balance int ,constraint  check (balance>2000));

insert into bankdemo values(101,'BOI',300); --error due to amount<1000

insert into bankdemo2 values (101,'BOI',100) -- here also eror as more than 2000 only u need to enter 

insert into bankdemo values(102,'BOB',2001);-- okay will run


--5) Default constraint 

--Default constraint : here if u forget 
--any column system will put null to that column 
--but i want my default value there so 
-- default constraint is used 

create table employee(empid int primary key ,
empname varchar(30) default 'Mr.X',salary int);

insert into employee(empid,salary) values(101,23000);
insert into employee(empid,salary) values(102,22000);
select * from employee;   



