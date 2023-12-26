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
	[password] varchar(50)
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
	unit_price varchar(50),
	[image] varchar(100),
	is_hidden int
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

