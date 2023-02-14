
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
	ProductImage nvarchar(150) null,
	Name nvarchar(150) not null,
	Description nvarchar(max) null,
	StockPrice money not null
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