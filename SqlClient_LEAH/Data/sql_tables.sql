CREATE TABLE Addresses (
	Id int  not null identity(1,1) primary key,
	StreetName nvarchar(50) not null,
	StreetNumber int null,
	PostalCode char(5) not null,
	City nvarchar(50) not null
)
CREATE TABLE Categories (
	Id int not null identity primary key,
	CategoryImage nvarchar(150) null,
	CategoryName nvarchar(150) not null,
	Description nvarchar(max) null
)
GO

CREATE TABLE Products (
	Id int not null identity primary key,
	CategoryId int not null references Categories(Id),
	ProductImage nvarchar(150) null primary key,
	Name nvarchar(150) not null,
	Description nvarchar(max) null,
	StockPrice money not null
)
GO

CREATE TABLE Customers (
	Id int not null identity primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	Email nvarchar(150) not null unique,
	Password nvarchar(max) not null,
	AddressId int not null references Addresses(Id)
)
GO
CREATE TABLE Cart (
	Id int not null identity primary key,
	ProductId int not null references Products(Id),
	UserId int not null references Customers(Id),
	CartCount int null


)
GO

CREATE TABLE Orders (
	Id Int not null identity primary key,
	CartId int not null references Cart(Id), 
	CustomerId int not null references Customers (Id),
	OrderDate datetime2 not null,
	DueDate datetime2 not null,
	TotalPrice money not null,
	VAT money null
)
GO



CREATE TABLE OrderRows (
	OrderId int not null references Orders(Id),
	ProductId int not null references Products(Id),
	Quantity int not null,
	Price money not null

	primary key (OrderId, ProductId)
)