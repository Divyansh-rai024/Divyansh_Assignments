create table dept(deptid int primary key,deptname varchar(40))
insert into dept values(10,'HR');
insert into dept values(20,'Sales')
insert into dept values(30,'Finanace')

create table emp(empid int primary key,empname varchar(40), 
worksin int foreign key references dept(deptid)); --column level constraint

insert into emp values(101,'Ravi',10);
insert into emp values(102,'sita',10)
insert into emp values(103,'Suresh',20)
insert into emp values(104,'Vidya',30)
insert into emp values(105,'mohan',null);
insert into emp values(106,'sohan',40);--error

select *from dept
select *from emp

--another way of refering master table

create table emp1(empid int  primary key, empname varchar(40),
deptid int,constraint forkey1 foreign key(deptid) references dept(deptid));--table level constraint


insert into emp1 values(101,'Ravi',10);
insert into emp1 values(102,'sita',10)
insert into emp1 values(103,'Suresh',20)
insert into emp1 values(104,'Vidya',30)
insert into emp1 values(105,'mohan',null);
insert into emp1 values(106,'sohan',40);--error

select *from emp1


--create a teble where doctor and patient data is there
--and treatment is going on (doctor and patient is invoolved)
--create 3 tables of doctor,patient and treatment
--treatment will be child of both doctor and patient 

/*create table doctor( doctorid int primary key, doctorname varchar(40), specialization varchar(40) ); 

create table patient( patientid int primary key, patientname varchar(40), age int );

create table treatment( treatid int primary key, doctorid int, patientid int, disease varchar(50),
treatdate date, constraint fk_doctor foreign key(doctorid) references doctor(doctorid), 
constraint fk_patient foreign key(patientid) references patient(patientid) );


insert into doctor values(1,'Sharma','Cardiology');
insert into doctor values(2,'Rao','Neurology');
insert into doctor values(3,'Khan','Orthopedic');

insert into patient values(101,'Ravi',35);
insert into patient values(102,'Sita',28);
insert into patient values(103,'Mohan',45);

insert into treatment values(1001,1,101,'Heart Pain','2025-01-10');
insert into treatment values(1002,2,102,'Migraine','2025-01-12');
insert into treatment values(1003,3,103,'Fracture','2025-01-15');


insert into treatment values(1004,null,101,'Checkup','2025-01-20');  ---error 
*/



create table doctor(doctorid int primary key,doctorname varchar(40), specialization varchar(40));

create table patient(patientid int primary key,patientname varchar(40),age int);

create table treatment(treatid int primary key,doctorid int, patientid int,disease varchar(50),treatdate date,
 constraint fk_doctor foreign key(doctorid) references doctor(doctorid),
 constraint fk_patient foreign key(patientid) references patient(patientid));

insert into doctor values(1,'Sharma','Cardiology');
insert into doctor values(2,'Rao','Neurology');
insert into doctor values(3,'Khan','Orthopedic');

insert into patient values(101,'Ravi',35);
insert into patient values(102,'Sita',28);
insert into patient values(103,'Mohan',45);

insert into treatment values(1001,1,101,'Heart Pain','2025-01-10');
insert into treatment values(1002,2,102,'Migraine','2025-01-12');
insert into treatment values(1003,3,103,'Fracture','2025-01-15');

alter table patient
add doctorid int;

alter table patient
add constraint fk_patient_doctor
foreign key (doctorid) references doctor(doctorid);

select *from treatment

select *from doctor

select *from patient


create table empdetails(empid int primary key ,empname varchar(30),empsal int);

insert into empdetails values(101,'ravi',34000)
insert into empdetails values(102,'sohan',30000);
insert into empdetails values(103,'sita',38000);

select sum(empsal)as "totalsal" ,max(empsal)as "maxsal" ,
min(empsal)as "minsal",avg(empsal)as "avgsal",
count(*) as "totoalemps" from empdetails;

select *from empdetails

create table ids(id int)
insert into ids values(1),(2),(1),(1),(1),(2),(3)

select *from ids

select id from ids group by id;--it tells how many groups are there

select id,count(id) from ids group by id;   

create table dept1(
  deptno int ,
  dname  varchar(14),
  loc    varchar(13),
  constraint pkk1 primary key(deptno)
);
 
 create table empl(
  empno    int primary key,
  ename    varchar(10),
  job      varchar(9),
  mgr      int,
  hiredate date,
  sal      int,
  comm     int,
  deptno   int  foreign key  references dept1 (deptno)
);

insert into dept1 values(10, 'ACCOUNTING', 'NEW YORK');
insert into dept1 values(20, 'RESEARCH', 'DALLAS');
insert into dept1
values(30, 'SALES', 'CHICAGO');
insert into dept1
values(40, 'OPERATIONS', 'BOSTON'); 

select * from dept1;

insert into empl values( 7839, 'KING', 'PRESIDENT', null,'1981-11-17' , 5000, null, 10);
insert into empl values( 7698, 'BLAKE', 'MANAGER', 7839,'1981-05-01',2850, null, 30);
insert into empl values( 7782, 'CLARK', 'MANAGER', 7839,'1981-06-09', 2450, null, 10);
insert into empl values( 7566, 'JONES', 'MANAGER', 7839,'1981-04-02', 2975, null, 20);
insert into empl values( 7788, 'SCOTT', 'ANALYST', 7566,'1987-04-19', 3000, null, 20);
insert into empl values( 7902, 'FORD', 'ANALYST', 7566, '1981-12-03', 3000, null, 20);
insert into empl values( 7369, 'SMITH', 'CLERK', 7902,'1980-12-17', 800, null, 20);
insert into empl values( 7499, 'ALLEN', 'SALESMAN', 7698, '1981-02-20', 1600, 300, 30);
insert into empl values(  7521, 'WARD', 'SALESMAN', 7698, '1981-02-22', 1250, 500, 30);
insert into empl values( 7654, 'MARTIN', 'SALESMAN', 7698, '1981-09-28', 1250, 1400, 30);
insert into empl values(  7844, 'TURNER', 'SALESMAN', 7698, '1981-09-08', 1500, 0, 30);
insert into empl values( 7876, 'ADAMS', 'CLERK', 7788, '1987-05-23', 1100, null, 20);
insert into empl values( 7900, 'JAMES', 'CLERK', 7698, '1981-12-03', 950, null, 30);
insert into empl values( 7934, 'MILLER', 'CLERK', 7782, '1982-01-23', 1300, null, 10);

select * from dept1;
select * from empl;

--give me count of emp in each dept
--version 1
select deptno from empl group by deptno;
--version 2
select deptno,count(ename) from empl group by deptno;

--give me max and min salary in each category of jobs

select job from empl group by job
select job, max(sal),min(sal) from empl group by job;


--sum of sal in each job and filter whose sum >5000

select job from empl group by job;

select job , sum(sal) from empl group by job;

select job , sum(sal) from empl group by job having sum(sal)>5000;

create table dept3(deptid int primary key ,deptname varchar(30));
insert into dept3 values(10,'sales'),(20,'Marketing'),
(30,'Software'),(40,'HR');
create table emp3(empid int primary key ,empname varchar(30),
worksin int foreign key 
references dept3(deptid));
insert into emp3 values(101,'ravi',10),
(102,'kiran',20),(103,'mahesh',30),(104,'suresh',20),
(105,'satish',null);

select * from dept3;
select * from emp3;

--emoployees who have dept

--version 1
select e1.empname, d1.deptname from emp3 e1 inner join dept3 d1 on e1.worksin=d1.deptid;

--version 2
select e1.empname + ' is working in '+ d1.deptname from emp3 e1 inner join 
dept3 d1 on e1.worksin=d1.deptid;

--version 3
select e1.empname from emp3 e1 inner join dept3 d1 on e1.worksin=d1.deptid;

--all emp who haven't got dept
--version 1
select e1.empname, d1.deptname from emp3 e1 left join dept3 d1 on e1.worksin=d1.deptid;

--version 2
select e1.empname ,d1.deptname from emp3 e1 left join dept3 d1 on e1.worksin=d1.deptid
where d1.deptname is null;

-- version 3 
select e1.empname  from emp3 e1 left join dept3 d1 on e1.worksin=d1.deptid
where d1.deptname is null;

--same using right join
select e1.empname ,d1.deptname from dept3 d1 right join emp3 e1 on e1.worksin=d1.deptid
where d1.deptname is null;

--give me dept where no emp is working
--version 1
select d1.deptname, e1.empname from dept3 d1 left join  emp3 e1 on d1.deptid=e1.worksin;

--version 2
select d1.deptname,e1.empname from dept3 d1 left join emp3 e1 on d1.deptid=e1.worksin
where e1.empname is null;

--version 3 
select d1.deptname from dept3 d1 left join emp3 e1 on d1.deptid=e1.worksin
where e1.empname is null;

--count of emp working in each dept (use both the tables)
--use joins and group by

--version 1
select d1.deptname, e1.empname from dept3 d1 inner join emp3 e1 
on d1.deptid=e1.worksin

--version 2
select d1.deptname, count( e1.empname) from dept3 d1 inner join
emp3 e1 on d1.deptid=e1.worksin group by d1.deptname;

-- joining with the third table 
create table Location(locid int primary key ,
 locname varchar(30),empid int references 
  emp3(empid));

insert into location values (1001,'delhi',102);
insert into location values (1002,'bangalore',103);
insert into location values (1003,'pune',104);
insert into location values(1004,'chennai',105);

select * from dept3;
select * from emp3;
select * from location

--give me all the employees who got dept andn also location 

--version 1
select e1.empname,d1.deptname,l1.locname from emp3 e1 inner join dept3 d1 on
e1.worksin=d1.deptid inner join location l1 on e1.empid=l1.empid;
--version 2 
select e1.empname from emp3 e1 inner join dept3 d1 on
e1.worksin=d1.deptid inner join location l1 on e1.empid=l1.empid;

-- give me all the employees who got dept but not location 
--version 1
select e1.empname,d1.deptname,l1.locname from emp3 e1 inner join dept3 d1 on
e1.worksin=d1.deptid left join location l1 on e1.empid=l1.empid;

--version 2 
select e1.empname,d1.deptname,l1.locname from emp3 e1 inner join dept3 d1 on
e1.worksin=d1.deptid left join location l1 on e1.empid=l1.empid where l1.locname is null;

--version 3 

select e1.empname from emp3 e1 inner join dept3 d1 on
e1.worksin=d1.deptid left join location l1 on e1.empid=l1.empid where l1.locname is null;

--emp who didn't get dept but location 
--version 1
select e1.empname,d1.deptname,l1.locname from emp3 e1 left join dept3 d1 on
e1.worksin=d1.deptid left join location l1 on e1.empid=l1.empid;

--version 2
select e1.empname,d1.deptname,l1.locname from emp3 e1 left join dept3 d1 on
e1.worksin=d1.deptid left join location l1 on e1.empid=l1.empid where d1.deptname is null;

--version 3
select e1.empname from emp3 e1 left join dept3 d1 on
e1.worksin=d1.deptid inner join location l1 on e1.empid=l1.empid where d1.deptname is null;

