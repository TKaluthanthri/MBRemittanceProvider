/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      FirstName,LastName,PhoneNumber,AddressLine,Country,City,State,PostalCode,dateOfBirth,Email,CreatedDate,CustomerCode
  FROM [RemittanceProvider].[dbo].[Customer]