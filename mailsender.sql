
create table Employeelogin
(
Username nvarchar(50),
Password nvarchar(50)
)

select * from Employeelogin

create procedure Sp_Employeelogin
(    
@Username Nvarchar(50),    
@Password varchar(50)    
)    
as begin    
if exists(select distinct  top 1 Username from Employeelogin where Username=@Username and Password=@Password)    
begin    
select distinct  top 1 Username from Employeelogin where Username=@Username and Password=@Password;    
end    
 else     
 select null;    
 end
GO

insert into Employeelogin values ('ram@gmail.com','22mcr112')

select * from People

create table People
(
Id int identity(1,1) primary key,
Name varchar(50),
DOB date,
PhoneNumber bigint,
AadhaarNumber bigint,
RationCardNumber bigint,
Email varchar(1000),
Things varchar(1000)null
)

alter procedure Sp_People
(
@Id int=null,
@Name varchar(50)=null,
@DOB date=null,
@PhoneNumber bigint=null,
@AadhaarNumber bigint=null,
@RationCardNumber bigint=null,
@Email varchar(1000)=null,
@Things varchar(1000)=null,
@Action int
)
AS BEGIN
IF(@Action=1)
BEGIN
INSERT INTO People VALUES(@Name,@DOB,@PhoneNumber,@AadhaarNumber,@RationCardNumber,@Email,@Things)
END
IF(@Action=2)
BEGIN
UPDATE People SET Name=@Name,DOB=@DOB,PhoneNumber=@PhoneNumber,AadhaarNumber=@AadhaarNumber,RationCardNumber=@RationCardNumber,Email=@Email,Things=@Things where Id=@Id
END
IF(@Action=3)
BEGIN
DELETE FROM People where Id=@Id
END
IF(@Action=4)
BEGIN
SELECT * FROM People
END
END

select * from People

--create table DownloadExcel
--(
--Name varchar(50),
--Subject varchar(50),
--Body  varchar(1000),
--Email varchar(1000)
--)

--select * from DownloadExcel


Alter procedure sp_SelectAll
as begin
(select top 1 SUBSTRING((select ',' + Email as 'data()' from People for XML path('')), 2, 999) as Email)
end

exec sp_SelectAll

create table MailSentRecord
(
Id int Identity(1,1),
Email	 varchar(1000),
message varchar(1000),
SentDate Varchar(20)
)

select * from MailSentRecord


alter proc Sp_InsertMailSentRecord
(
@Id int=Null,
@Email	 varchar(1000)=Null,
@message varchar(1000)=Null,
@SentDate Varchar(20)=Null,
@Action int
)
AS BEGIN
IF(@Action=1)
BEGIN
INSERT INTO MailSentRecord values (@Email,@message,@SentDate)
END
IF(@Action=2)
BEGIN
SELECT * FROM MailSentRecord order by SentDate 
END
END

select * from MailSentRecord




select * from People
