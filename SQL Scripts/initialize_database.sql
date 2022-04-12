CREATE DATABASE [RemittanceProvider]

USE [RemittanceProvider]

CREATE TABLE Country
(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(500),
	Code VARCHAR(100)
);

CREATE TABLE State
(
	StateId INT IDENTITY PRIMARY KEY,
	Name VARCHAR(100),
	Country_Id INT UNIQUE FOREIGN KEY REFERENCES Country(Id)
);

CREATE TABLE Bank
(
	Id INT IDENTITY PRIMARY KEY,
	Name VARCHAR(100),
	BankCode  VARCHAR(100),
	Country_Id INT FOREIGN KEY REFERENCES Country(Id)
);

CREATE TABLE Account
(
	Id INT IDENTITY PRIMARY KEY,
	AccountNumber VARCHAR(200),
	BankAccountName  VARCHAR(Max),
	FirstName VARCHAR(Max),
	LastName VARCHAR(Max),
	CreatedDate DateTime,
	UpdatedDate DateTime,
	Bank_Id INT  FOREIGN KEY REFERENCES Bank(Id)
)

CREATE TABLE Customer
(
	Id INT IDENTITY PRIMARY KEY,
	FirstName VARCHAR(Max),
	LastName VARCHAR(Max),
	PhoneNumber VARCHAR(100),
	AddressLine VARCHAR(Max),
	Country VARCHAR(200),
	City VARCHAR(Max),
	State VARCHAR(Max),
	PostalCode VARCHAR(100),
	dateOfBirth DateTime,
	Email VARCHAR(500),
	CreatedDate DateTime,
	CustomerCode VARCHAR(500)
);

CREATE TABLE TransactionDetails
(
	Id INT IDENTITY PRIMARY KEY,
	TransactionNumber VARCHAR(Max),
	Amount  Decimal,
	exchangeRate  VARCHAR(Max),
	fees Decimal,
	Transaction_Date DateTime,
	Transaction_Status Varchar(200),
	Country Varchar(200),
	Sender_Id INT  FOREIGN KEY REFERENCES Customer(Id),
	Receiver_Id INT  FOREIGN KEY REFERENCES Account(Id)
);