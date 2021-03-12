CREATE TABLE Users (
    idUsers int NOT NULL PRIMARY KEY IDENTITY(1,1),
	FirstName varchar(80),
	LastName varchar(80),
	Email varchar(80),
	UserPass varchar(50)
);

CREATE TABLE Products(
	idProducts int NOT NULL PRIMARY KEY IDENTITY(1,1),
	ProductDesc varchar(450),
	Price float,
	Color varchar(50),
	Dimensions varchar(60),
	WoodType varchar(60),
	ProductType varchar(60),
	Quantity int,
	StockQ int,
	ImageURL varchar(200)
);

CREATE TABLE Orders(
	idOrder int NOT NULL PRIMARY KEY IDENTITY(1,1),
	idClient int,
	idProduct int,
	DateOrder date
);

CREATE TABLE CustomOrders(
	idOrder int NOT NULL PRIMARY KEY IDENTITY(1,1),
	idClient int,
	ProductDesc varchar(450),
	Price float,
	Color varchar(50),
	Dimensions varchar(60),
	WoodType varchar(60),
	ProductType varchar(60),
	Quantity int,
	StockQ int
);


INSERT INTO Users VALUES('Andrés', 'Díaz', 'andres@gmail.com', 'ad2001');