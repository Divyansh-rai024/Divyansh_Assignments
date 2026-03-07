
--SELF JOIN 
----------
--self join means joining the table with itself now here only one table is there okay //--/ in this table only some body is manaager for some body so as per question i had done self join 
--for the same table two objects i created .


create table Employee(id int primary key ,
name varchar(50) not null,
managerid int );


insert into employee values(1,'mike',3),(2,'David',3),(3,'Roger',null),(4,'Marry',2),(5,'Joseph',2),
(7,'Ben',2);

select * from employee;

--give me all the emp with the names and their corresponding managers
select e1.name as "empname", e2.name as "Manager" from Employee e1 inner join Employee e2 on 
e1.managerid = e2.id;

--give me name of the employee also who is not having any manager and if he is top mananger 
--write beside him as CEO
--version 1
select e1.name as "empname", e2.name as "Manager" from Employee e1 left join Employee e2 on 
e1.managerid = e2.id;

--version 2 
select e1.name as "empname" ,e2.name as "Manager" from Employee e1 left join Employee e2 on 
e1.managerid=e2.id where e2.name is null  


-- want to keep him as CEO 

select e1.name as "empname" ,isnull(e2.name,'CEO') as "Manager"  from Employee e1 left join Employee e2 on 
e1.managerid=e2.id 


--subquery

create table dept23 (deptid int primary key ,deptname varchar(30))

insert into dept23 values(10,'HR'),(20,'Software'),(30,'Sales');

select * from dept23;

create table emp23 (empid int primary key ,empname varchar(40),deptid
int foreign key references dept23(deptid));

insert into emp23 values(101,'ravi',30),(102,'kiran',10),(103,'sita',10)
,(104,'suresh',20)

select * from dept23;
select * from emp23;
-- subquery means query insdie another query 

--give me the department name of employee whose empid is 103

select deptname from dept23 where deptid =(select deptid from emp23 where 
empid =103)

--give me names of employee working in HR dept
select empname from emp23 where deptid in (select deptid from dept23 where deptname='HR');


create table empdemo3(empid integer  ,empname varchar(30) ,
salary integer )
insert into empdemo3 values ( 101,'ravi',1200);
insert into empdemo3 values ( 102,'mohan',1100);
insert into empdemo3 values ( 103,'kumar',1400);
insert into empdemo3 values ( 104,'senthil',1000);
insert into empdemo3 values ( 105,'manju',200);
insert into empdemo3 values ( 106,'manoj',500);

select * from empdemo3;


-- give me second largest salary from the table 

select max(salary) from empdemo3 where salary <(select max(salary) from empdemo3);

--another way 
select max(salary) from empdemo3 where salary not in (select max(salary) from empdemo3);


-- third largest 

select max(salary) from empdemo3 where salary < (select max(salary) from 
empdemo3 where salary <(select max(salary) from empdemo3));

--


select empname,salary from empdemo3
order by salary desc
offset 3 rows 
fetch next 1 row only;


--some assingment 
-----------------
--First create three tables like this one is students and another is classes and another is studentclass 
---so there is many to many  relation ship between two tables so i have here one junction table okay so 
--now to answer the question never join student with classes table okay see
--in the answer i had joined the tables using juntion table which is studentclass 

--assingment 
  create table students(studentid int ,studentname varchar(30));
insert into students values(1,'john'),(2,'Matt'),(3,'James');
create table classes(classid int ,classname varchar(30));
insert into classes values(1,'art'),(2,'history'),(3,'Maths');
create table studentclass(studentid int,classid int );
insert into studentclass values(1,1),(1,2),(3,1),(3,2),(3,3);

select * from students;
select * from classes;
select * from studentclass;

--what will be the best possible join if i want to know all the students who have taken  which classes
--version 1
select s1.studentname, c1.classname from students s1 join studentclass sc on sc.studentid=s1.studentid
join classes c1 on c1.classid=sc.classid

--version 2
select s1.studentname from students s1 join studentclass sc on sc.studentid=s1.studentid

--version 3
select  distinct (s1.studentname) from students s1 join studentclass sc on sc.studentid=s1.studentid
join classes c1 on c1.classid=sc.classid

--mine
SELECT DISTINCT s.studentid, s.studentname FROM students s INNER JOIN studentclass sc
ON s.studentid = sc.studentid;

--give me all the students who have not attended the classes
--version 1
select s1.studentname, c1.classname from students s1 left  join studentclass sc on sc.studentid=s1.studentid
left join classes c1 on c1.classid=sc.classid

--version 2
select s1.studentname ,c1.classname from students s1 left join studentclass sc on sc.studentid=s1.studentid
left join classes c1 on c1.classid=sc.classid where c1.classname is null

--version 3 
select s1.studentname  from students s1 left join studentclass sc on sc.studentid=s1.studentid
left join classes c1 on c1.classid=sc.classid where c1.classname is null

--Alter command 
-------------------

--it is used to modify the structure of exsisting table using which u can perform any of the following tasks 

--1)change the datatype of the column 
--2)Increase or Decrese the width of the columnm
--3)change  null to not null and not null to null 
--4)add a new column 
--5)drop an exsisting column 
--6)add a new constraint
--7)drop an exisiting constraint .

--for 1,2,3 
-- alter table <Tname> alter col <colname><dtype>[width][notnull/null]
drop table students

create table students (sno int,sname varchar(50),class int)

insert into students values (101,'ravi',12)
insert into students values(102,'kumar',4)
insert into students values (103,'senthil',8)

select * from students

alter table students alter column sname char(40) not null;

sp_help students;

--adding a new 
--alter table <tablename> add <colname> <dtype><width>[not null]
alter table students add  City varchar(40) not null 

-- i cananot add values while desining new colum after add new column 
-- i can add so i will go with 
alter table students add  City varchar(40)

select * from students;

--now i want to make not null that last city column 
-- frist add some values in city and then apply frist formulas 
update students set city='UK' where sno in (101,102,103)

alter table students alter column City varchar(30) not null;

sp_help students;

sp_help students;
-- drop a columnn 
--alter table <tname> drop column <colname>
alter table students drop column City

select * from students 

--adding a constraint
--alter table <tname> add constraint<constraintname> typeofconstraint(colllist)
alter table students alter column sno int  not null;

alter table students add constraint kk45 primary key(sno)

sp_help students;

--dropping constraint
--alter table <tname> drop constraint <constraintname>
alter table students drop constraint kk45 

--trying to create  composite  primary key and dropping it
alter table students alter column sno int  not null;

alter table students add constraint kk33 primary key(sno, sname)
sp_help students;

--now dropping it 
alter table students drop constraint kk33
sp_help students;



