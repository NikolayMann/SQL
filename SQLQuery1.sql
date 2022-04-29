CREATE TABLE Shop
(
	id int not null,
	lastName nvarchar(20) not null,
	firstName nvarchar(20) not null,
	secondName nvarchar(20) not null,
	telephone nvarchar(12),
	eMail nvarchar(12) not null
)

insert into Shop
(
	[id], [lastName], [secondName], [firstName], [telephone], [eMail]
)values(1,'sa1d','d1sa','a1sdfgh','12345673','sd2hj')

select * from Shop

delete from Shop where id<2

DBCC CHECKIDENT ( Shop ,  RESEED, 0)
//ACCESS
CREATE TABLE Orders
(
	id int not null IDENTITY(1,1),
	eMail nvarchar(12) not null,
	productCode int not null,
	productName nvarchar(50) not null
)

DROP TABLE Shop
