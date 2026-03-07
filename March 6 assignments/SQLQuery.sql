 create procedure printdata
 as
 begin

 declare @x varchar(30)
set @x ='India is doing well'
print @x;
end

exec printdata;

--adding another/modifyingprocedure(use alter commmand)
 alter procedure printdata
 as
 begin

 declare @x varchar(30)
set @x ='India is doing well'
print 'Hello World'
print @x;
end

exec printdata;

--default name space is dbo i.e database owner
--if no space is given in db it gets stored in dbo

--another way to check script/code
sp_helptext printdata

create procedure addnums(@num1 int,@num2 int)
as begin
declare @result int;
set @result=@num1 +@num2;
print'The sum is'+Convert (varchar(20),@result);

end
exec addnums 20 ,30

--returning a value in stored procedure
alter procedure addnums(@num1 int,@num2 int)
as begin
declare @result int;
set @result=@num1 +@num2;
--print'The sum is'+Convert (varchar(20),@result);
return @result;
end

declare @result1 int;
exec @result1 =addnums 10,25
print'The sum is'+Convert(varchar(20), @result1);

---to use variable i want to use variable given me from outside
--then i stored procedure will use out variable like we were using in C#
--here i can out or output also
alter procedure addnums(@num1 int,@num2 int,@result int output)
as begin
set @result=@num1 +@num2;
--print'The sum is'+Convert (varchar(20),@result);
return @result;
end

declare @result2 int;
declare @sum1 int
exec @result2=addnums 12,45,@sum1 output;
print'The sum is'+Convert (varchar(20),@result2);


create procedure calculator (@num1 int ,@num2 int,@addresult int output ,@substractresult
int output)
as
begin 
set @addresult=@num1 + @num2;
set @substractresult=@num1-@num2;
select @addresult;
select @substractresult;
end 

declare @sum int ;
declare @diff int;
exec calculator 100,34,@sum output,@diff output 
print 'The sum is '+Convert(varchar(20),@sum)
print 'The diff is '+Convert(varchar(20),@diff)



 alter procedure printdata with encryption
as 
begin 
declare @x varchar(30)
set @x='india is doing well'
print @x;
end 

sp_helptext printdata

alter procedure printdata 
as 
begin 
declare @x varchar(30)
set @x='india is doing well'
print @x;
end

--write a sp for findind x to the power of y
create procedure calculate (@num1 int ,@num2 int,@powerresult float output)
as
begin
 set @powerresult = POWER(@num1, @num2);
 select @powerresult;
end

declare @result float;

exec calculate 2, 3, @result output;

select @result as PowerResult;


 create table studentdata(studid int primary key ,studname varchar(50))
-- sp for insert 
create proc insertstud (@studid1 int ,@studname1 varchar(50))
as
begin 
insert into studentdata values(@studid1,@studname1);
end 

exec insertstud 102,'suresh'

select * from studentdata 

-- sp for udpate 
create proc updatestud (@studid1 int ,@studname1 varchar(50))
as
begin 
update  studentdata set studname=@studname1 where studid=@studid1;
end 

exec updatestud 102,'kiran'

select * from studentdata 


-- sp for delete
create proc deletestud (@studid1 int)
as
begin 
delete  studentdata  where studid=@studid1;
end 

exec deletestud 102

select * from studentdata 


-- sp for select
create proc selectstud(@studid1 int)
as begin
select *from studentdata where studid=@studid1
end

exec selectstud 101

--proc to display only name of student only
alter  proc selectstud(@studid1 int)
as
begin
declare @name1 varchar(40);
select @name1=studname from studentdata where studid=@studid1;
print @name1;
end

exec selectstud 101

