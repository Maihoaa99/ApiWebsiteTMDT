create database websiteTMDT
go
use websiteTMDT
go

create table cities 
(
	Id uniqueidentifier primary key default newId(),
	code int,
	[name] nvarchar(150)
)

create table district
(
	Id uniqueidentifier primary key default newId(),
	code int,
	[name] nvarchar(150),
	city_id uniqueidentifier foreign key references cities(Id)
)

create table communes
(
	Id uniqueidentifier primary key default newId(),
	code int,
	[name] nvarchar(150),
	district_id uniqueidentifier foreign key references district(Id)
)


create table accounts 
(
	id uniqueidentifier primary key default newId(),
	[name] nvarchar(200),
	age int,
	username varchar(100),
	email varchar(100),
	[password] varchar(250)
)

create table addresses
(
	id uniqueidentifier primary key default newId(),
	account_id uniqueidentifier foreign key references accounts(id),
	detail_address nvarchar(200),
	commune_id uniqueidentifier foreign key references communes(Id)
)

create table categories 
(
	id uniqueidentifier primary key default newId(),
	code int,
	[name] nvarchar(200)
)

create table products
(
	id uniqueidentifier primary key default newId(),
	code int,
	[name] nvarchar(200),
	category_id uniqueidentifier foreign key references categories(id),
	unit_id uniqueidentifier default newId(),
	unit_price int,
	[image] varchar(100),
	is_hidden bit
)

create table [order]
(
	id uniqueidentifier primary key default newId(),
	account_id uniqueidentifier foreign key references accounts(id),
	create_at datetime,
	shipping_status int
)

create table order_detail
(
	id uniqueidentifier primary key default newId(),
	order_id uniqueidentifier foreign key references [order](id),
	product_id uniqueidentifier foreign key references products(id),
	quantity int
)

create table cart
(
	account_id uniqueidentifier foreign key references accounts(id),
	product_id uniqueidentifier foreign key references products(id),
	quantity int, 
	created_at datetime
)
select * from categories
insert into categories (code, [name])
values (1, N'Túi Xách'), (2, N'Áo Nam'), (3, N'Áo Nữ'), (4, N'Quần Nam'), (5, N'Quần Nữ'), (6, N'Giày Nam'), (7, N'Giày Nữ')
		
select * from products
insert into products (code, [name], category_id, unit_price, is_hidden)
values (1, N'Túi Da Sành Điệu', '86599BDF-0363-4E2A-B0C8-1A954553BB7C', 600000, 1),
		(2, N'Túi Tote Trẻ Trung', '86599BDF-0363-4E2A-B0C8-1A954553BB7C', 200000, 1),
		(3, N'Balo Học Sinh', '86599BDF-0363-4E2A-B0C8-1A954553BB7C', 150000, 1),
		(4, N'Túi Đeo Chéo Nam Nữ', '86599BDF-0363-4E2A-B0C8-1A954553BB7C', 180000, 1),
		(5, N'Sơ Mi Nam', '3867D261-64B4-4CC2-93BD-7CC665D7BB62', 250000, 1),
		(6, N'Sơ Mi Cổ Tàu', '3867D261-64B4-4CC2-93BD-7CC665D7BB62', 300000, 1),
		(7, N'Polo Năng Động', '3867D261-64B4-4CC2-93BD-7CC665D7BB62', 270000, 1),
		(8, N'Áo Phông', '3867D261-64B4-4CC2-93BD-7CC665D7BB62', 150000, 1),
		(9, N'Yếm Xưa', '0F2F9FF2-7643-4180-B13D-527B6B239F78', 240000, 1),
		(10, N'Áo Hai Dây Bản To', '0F2F9FF2-7643-4180-B13D-527B6B239F78', 230000, 1),
		(11, N'Áo Dài Cách Tân', '0F2F9FF2-7643-4180-B13D-527B6B239F78', 500000, 1),
		(12, N'Trễ Vai Duyên Dáng', '0F2F9FF2-7643-4180-B13D-527B6B239F78', 210000, 1),
		(13, N'Quần Âu Lịch Sự', 'FDE0E1FD-16A7-4AA3-B090-ED42527ACD64', 370000, 1),
		(14, N'Quần Bò Phá Cách', 'FDE0E1FD-16A7-4AA3-B090-ED42527ACD64', 220000, 1),
		(15, N'Quần Thun Thoải Mái', 'FDE0E1FD-16A7-4AA3-B090-ED42527ACD64', 140000, 1),
		(16, N'Quần Lửng Mát Mẻ', 'FDE0E1FD-16A7-4AA3-B090-ED42527ACD64', 100000, 1),
		(17, N'Quần Bò Ống Xuông', '57AF4888-D002-4AEF-A723-00AF8CD3FD69', 300000, 1),
		(18, N'Quần Ngắn Năng Động', '57AF4888-D002-4AEF-A723-00AF8CD3FD69', 320000, 1),
		(19, N'Quần Giả Váy', '57AF4888-D002-4AEF-A723-00AF8CD3FD69', 190000, 1),
		(20, N'Quần Legging', '57AF4888-D002-4AEF-A723-00AF8CD3FD69', 80000, 1),
		(21, N'Giày Da Lịch Lãm', '5D6DAE36-969A-4CEA-8320-D2FD6DBB79B7', 900000, 1),
		(22, N'Giày Thể Thao', '5D6DAE36-969A-4CEA-8320-D2FD6DBB79B7', 800000, 1),
		(23, N'Giày Đá Bóng', '5D6DAE36-969A-4CEA-8320-D2FD6DBB79B7', 670000, 1),
		(24, N'Giày Lao Động', '5D6DAE36-969A-4CEA-8320-D2FD6DBB79B7', 200000, 1),
		(25, N'Giày Cao Gót', 'D5CB37DC-A7E5-491E-A3C5-7D0AC614202B', 460000, 1),
		(26, N'Giày Thể Thao', 'D5CB37DC-A7E5-491E-A3C5-7D0AC614202B', 420000, 1),
		(25, N'Giày Đế Cao', 'D5CB37DC-A7E5-491E-A3C5-7D0AC614202B', 580000, 1),
		(26, N'Boot Cao Cổ', 'D5CB37DC-A7E5-491E-A3C5-7D0AC614202B', 690000, 1)

