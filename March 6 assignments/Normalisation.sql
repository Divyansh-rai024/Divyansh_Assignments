create table sailors
(
 sid int constraint pk_sid primary key,
 sname varchar(20),
 rating int,
 age int
)
 
create table boats
(
 bid int constraint pk_bid primary key,
 bname varchar(20),
 color varchar(20)
)
 
create table reserves
(
sid int constraint fk_sid foreign key(sid) references sailors(sid),
bid int constraint fk_bid foreign key(bid) references boats(bid),
day date
)
 
insert into sailors values(22,'dustin',7,45)
insert into sailors values(29,'brutus',1,39)
insert into sailors values(31,'lubber',9,55)
insert into sailors values(32,'andy',8,25)
insert into sailors values(58,'Rusty',10,35)
 
insert into boats values(101,'interlake','blue')
insert into boats values(102,'interlake','red')
insert into boats values(103,'clipper','green')
insert into boats values(104,'marine','red')
 
insert into reserves values(22,101,'1/1/2004')
insert into reserves values(22,102,'1/1/2004')
insert into reserves values(22,103,'1/2/2004')
insert into reserves values(31,103,'5/5/2005')
insert into reserves values(32,104,'7/4/2005')
 
--QUESTIONS
--1)Write a query to find the name of the sailors who reserved red boat
--2) Write a query to find the names of sailors who have reserved a red and a
--green boat.
--3) Write a query to find the name of sailor who reserved 1 boat
--4) Write a query to find the sids of all sailors who have reserved red boats
--but not green boats.
--5) Write a query to find the sailors with the highest rating
--6) Write a query to find  the number of sailors elligible to vote
--7) Write a query to count different sailors name
--8) Write a query to find  the oldest sailor
--9)Write a query to find name as SAILERNAME,Rating,Age,bname as BOATNAME,Color,Day.
--10)Write a query to display the sailor name  starts with letter a and b. 
--11)Find the names of the Sailors who have reserved at least one boat.
--12)Compute increments for the ratings of persons who have sailed two different boats on the same day.
--13)Find the ages of sailors whose name begins and ends with B and has at least 3 characters.

--1)
select distinct s.sname from sailors s join 
reserves r on s.sid=r.sid join boats b on 
r.bid=b.bid where b.color='red';

--2)
select s.sname from sailors s join reserves r
on s.sid=r.sid join boats b on r.bid=b.bid where
b.color in ('red','green') group by s.sname having 
count(distinct b.color)=2;

--3) 
select s.sname from sailors s join reserves r on 
s.sid=r.sid group by s.sname having count(*)=1;

--4) 
select distinct r.sid from reserves r join boats b
on r.bid=b.bid where b.color='red' and r.sid not in
(select r2.sid from reserves r2 join boats b2 on 
r2.bid=b2.bid where b2.color='green');

--5) 
select sname from sailors where rating=(select max(rating)
from sailors);

--6) 
select count(*) from sailors where age>=18;

--7) 
select count(distinct sname) from sailors;

--8) 
select sname from sailors where age=(select max(age) from sailors);

--9) 
select s.sname as sailername,s.rating,s.age,b.bname
as boatname,b.color,r.day from sailors s join reserves
r on s.sid=r.sid join boats b on r.bid=b.bid;

--10)
select sname from sailors where sname like 'a%' or 
sname like 'b%';

--11) 
select distinct s.sname from sailors s join
reserves r on s.sid=r.sid;

--12)
update sailors set rating=rating+1 where sid 
in (select sid from reserves group by sid,day 
having count(distinct bid)>=2);

--13) 
select age from sailors where sname like 'b%b'
and len(sname)>=3;
