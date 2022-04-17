USE [RemittanceProvider]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 17/04/2022 6:16:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AccountNumber] [varchar](200) NULL,
	[BankAccountName] [varchar](max) NULL,
	[FirstName] [varchar](max) NULL,
	[LastName] [varchar](max) NULL,
	[CreatedDate] [datetime] NULL,
	[UpdatedDate] [datetime] NULL,
	[Bank_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 17/04/2022 6:16:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[BankCode] [varchar](100) NULL,
	[Country_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 17/04/2022 6:16:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[Code] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 17/04/2022 6:16:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](max) NULL,
	[LastName] [varchar](max) NULL,
	[PhoneNumber] [varchar](100) NULL,
	[AddressLine] [varchar](max) NULL,
	[Country] [varchar](200) NULL,
	[City] [varchar](max) NULL,
	[State] [varchar](max) NULL,
	[PostalCode] [varchar](100) NULL,
	[dateOfBirth] [datetime] NULL,
	[Email] [varchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[CustomerCode] [varchar](500) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExchangeFee]    Script Date: 17/04/2022 6:16:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeFee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromAmount] [decimal](8, 3) NULL,
	[ToAmount] [decimal](8, 3) NULL,
	[Fee] [decimal](8, 3) NULL,
	[LastUpdatedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExchangeRates]    Script Date: 17/04/2022 6:16:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExchangeRates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FromCountry] [varchar](max) NULL,
	[ToCountry] [varchar](max) NULL,
	[ExchangeRate] [decimal](8, 3) NULL,
	[CurrencyType] [varchar](max) NULL,
	[LastUpdatedDate] [datetime] NULL,
	[ExchangeRateToken] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 17/04/2022 6:16:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NULL,
	[Code] [varchar](100) NULL,
	[Country_Id] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[StateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionInfo]    Script Date: 17/04/2022 6:16:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TransactionNumber] [varchar](max) NULL,
	[TransactionId] [varchar](max) NULL,
	[Amount] [decimal](8, 3) NULL,
	[ExchangeRate] [varchar](max) NULL,
	[Fees] [decimal](8, 3) NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [varchar](200) NULL,
	[Country] [varchar](200) NULL,
	[SenderId] [int] NULL,
	[ReceiverId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 
GO
INSERT [dbo].[Account] ([Id], [AccountNumber], [BankAccountName], [FirstName], [LastName], [CreatedDate], [UpdatedDate], [Bank_Id]) VALUES (1, N'2715500356', N'Test Account', N'Egon', N'Spengler', CAST(N'2022-04-10T00:00:00.000' AS DateTime), CAST(N'2022-04-10T00:00:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Account] ([Id], [AccountNumber], [BankAccountName], [FirstName], [LastName], [CreatedDate], [UpdatedDate], [Bank_Id]) VALUES (2, N'2735600359', N'Savings Account', N'Glinda', N'Southgood', CAST(N'2022-04-10T00:00:00.000' AS DateTime), CAST(N'2022-04-10T00:00:00.000' AS DateTime), 1)
GO
INSERT [dbo].[Account] ([Id], [AccountNumber], [BankAccountName], [FirstName], [LastName], [CreatedDate], [UpdatedDate], [Bank_Id]) VALUES (8, N'ACM0008989787', N'FixRateAccount', N'FixRateAccount', NULL, CAST(N'2022-04-14T13:48:21.660' AS DateTime), CAST(N'2022-04-14T13:48:21.660' AS DateTime), 3)
GO
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Bank] ON 
GO
INSERT [dbo].[Bank] ([Id], [Name], [BankCode], [Country_Id]) VALUES (1, N'Wells Fargo', N'WFBIUS6S', 5)
GO
INSERT [dbo].[Bank] ([Id], [Name], [BankCode], [Country_Id]) VALUES (2, N'Northrim bank', N'125200934', 5)
GO
INSERT [dbo].[Bank] ([Id], [Name], [BankCode], [Country_Id]) VALUES (3, N'Jyske Bank', N'DKW56257', 6)
GO
SET IDENTITY_INSERT [dbo].[Bank] OFF
GO
SET IDENTITY_INSERT [dbo].[Country] ON 
GO
INSERT [dbo].[Country] ([Id], [Name], [Code]) VALUES (1, N'Albania', N'AL')
GO
INSERT [dbo].[Country] ([Id], [Name], [Code]) VALUES (2, N'Algeria', N'DZ')
GO
INSERT [dbo].[Country] ([Id], [Name], [Code]) VALUES (3, N'American Samoa', N'AS')
GO
INSERT [dbo].[Country] ([Id], [Name], [Code]) VALUES (4, N'Algeria', N'DZ')
GO
INSERT [dbo].[Country] ([Id], [Name], [Code]) VALUES (5, N'United States of America', N'US')
GO
INSERT [dbo].[Country] ([Id], [Name], [Code]) VALUES (6, N'Denmark', N'DK')
GO
INSERT [dbo].[Country] ([Id], [Name], [Code]) VALUES (7, N'Sweden', N'SE')
GO
SET IDENTITY_INSERT [dbo].[Country] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 
GO
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [PhoneNumber], [AddressLine], [Country], [City], [State], [PostalCode], [dateOfBirth], [Email], [CreatedDate], [CustomerCode]) VALUES (4, N'Alfreds', N'Futterkiste', N'0112222222', N'Obere Str. 57 ,Berlin', N'US', NULL, N'', N'299979', CAST(N'1967-04-14T00:00:00.000' AS DateTime), N'alfreds123@gmail.com', CAST(N'2022-04-14T13:48:21.570' AS DateTime), N'afa05414-83a6-406c-b7cc-32b90a835215')
GO
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [PhoneNumber], [AddressLine], [Country], [City], [State], [PostalCode], [dateOfBirth], [Email], [CreatedDate], [CustomerCode]) VALUES (5, N'Berglunds', N'snabbköp', N'0112222222', N'Obere Str. 57 ,Berlin', N'US', NULL, N'', N'299979', CAST(N'1967-04-14T00:00:00.000' AS DateTime), N'snabbköp@gmail.com', CAST(N'2022-04-14T15:05:18.523' AS DateTime), N'0af5255a-83bb-4e04-b5e0-054062cdaef0')
GO
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[ExchangeFee] ON 
GO
INSERT [dbo].[ExchangeFee] ([Id], [FromAmount], [ToAmount], [Fee], [LastUpdatedDate]) VALUES (1, CAST(0.000 AS Decimal(8, 3)), CAST(199.990 AS Decimal(8, 3)), CAST(2.120 AS Decimal(8, 3)), CAST(N'2022-04-10T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ExchangeFee] ([Id], [FromAmount], [ToAmount], [Fee], [LastUpdatedDate]) VALUES (2, CAST(200.000 AS Decimal(8, 3)), CAST(399.990 AS Decimal(8, 3)), CAST(5.250 AS Decimal(8, 3)), CAST(N'2022-04-10T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ExchangeFee] ([Id], [FromAmount], [ToAmount], [Fee], [LastUpdatedDate]) VALUES (3, CAST(400.000 AS Decimal(8, 3)), CAST(599.990 AS Decimal(8, 3)), CAST(8.330 AS Decimal(8, 3)), CAST(N'2022-04-10T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ExchangeFee] OFF
GO
SET IDENTITY_INSERT [dbo].[ExchangeRates] ON 
GO
INSERT [dbo].[ExchangeRates] ([Id], [FromCountry], [ToCountry], [ExchangeRate], [CurrencyType], [LastUpdatedDate], [ExchangeRateToken]) VALUES (1, N'US', N'DK', CAST(6.870 AS Decimal(8, 3)), N'DKK', CAST(N'2022-04-13T00:00:00.000' AS DateTime), N'DKK987965909090')
GO
INSERT [dbo].[ExchangeRates] ([Id], [FromCountry], [ToCountry], [ExchangeRate], [CurrencyType], [LastUpdatedDate], [ExchangeRateToken]) VALUES (2, N'US', N'SE', CAST(9.522 AS Decimal(8, 3)), N'SEK', CAST(N'2022-04-13T00:00:00.000' AS DateTime), N'SEK9879659023420')
GO
SET IDENTITY_INSERT [dbo].[ExchangeRates] OFF
GO
SET IDENTITY_INSERT [dbo].[State] ON 
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (1, N'Alabama', N'AL', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (2, N'Alaska', N'AK', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (3, N'Arizona', N'AZ', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (4, N'Arkansas', N'AR', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (5, N'California', N'CA', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (6, N'Colorado', N'CO', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (7, N'Connecticut', N'CT', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (8, N'Delaware', N'DE', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (9, N'District of Columbia	', N'DC', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (10, N'Florida', N'FL', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (11, N'Georgia', N'GA', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (12, N'Hawaii', N'HI', 5)
GO
INSERT [dbo].[State] ([StateId], [Name], [Code], [Country_Id]) VALUES (13, N'Illinois', N'IL', 5)
GO
SET IDENTITY_INSERT [dbo].[State] OFF
GO
SET IDENTITY_INSERT [dbo].[TransactionInfo] ON 
GO
INSERT [dbo].[TransactionInfo] ([Id], [TransactionNumber], [TransactionId], [Amount], [ExchangeRate], [Fees], [CreatedDate], [Status], [Country], [SenderId], [ReceiverId]) VALUES (4, N'xdffdsdg001', N'3b908a9e-9c0c-4d74-a332-bbb7690a6540', CAST(3000.560 AS Decimal(8, 3)), N'', CAST(25.760 AS Decimal(8, 3)), CAST(N'2022-04-14T13:48:25.937' AS DateTime), N'201', N'DK', 4, 8)
GO
INSERT [dbo].[TransactionInfo] ([Id], [TransactionNumber], [TransactionId], [Amount], [ExchangeRate], [Fees], [CreatedDate], [Status], [Country], [SenderId], [ReceiverId]) VALUES (5, N'xdffdsdg002', N'3f159a63-3c41-4c02-8087-27c8663ca48a', CAST(3000.560 AS Decimal(8, 3)), N'', CAST(25.760 AS Decimal(8, 3)), CAST(N'2022-04-14T15:05:18.697' AS DateTime), N'201', N'DK', 5, 8)
GO
INSERT [dbo].[TransactionInfo] ([Id], [TransactionNumber], [TransactionId], [Amount], [ExchangeRate], [Fees], [CreatedDate], [Status], [Country], [SenderId], [ReceiverId]) VALUES (6, N'xdffdsdg0021', N'dc9d6a09-2176-4c55-a96f-cc851d16b9b2', CAST(3000.560 AS Decimal(8, 3)), N'', CAST(25.760 AS Decimal(8, 3)), CAST(N'2022-04-15T16:11:16.063' AS DateTime), N'201', N'DK', 5, 8)
GO
SET IDENTITY_INSERT [dbo].[TransactionInfo] OFF
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD FOREIGN KEY([Bank_Id])
REFERENCES [dbo].[Bank] ([Id])
GO
ALTER TABLE [dbo].[Bank]  WITH CHECK ADD FOREIGN KEY([Country_Id])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[State]  WITH CHECK ADD FOREIGN KEY([Country_Id])
REFERENCES [dbo].[Country] ([Id])
GO
ALTER TABLE [dbo].[TransactionInfo]  WITH CHECK ADD FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[TransactionInfo]  WITH CHECK ADD FOREIGN KEY([SenderId])
REFERENCES [dbo].[Customer] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[GetBeneficiaryName]    Script Date: 17/04/2022 6:16:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Thilini Kaluthanthri
-- Create date: 2022-04-12
-- Description:	Get Beneficiary Name by Account Number 
-- =============================================
CREATE PROCEDURE [dbo].[GetBeneficiaryName]
@AccoutNumber VARCHAR(200),
@BankCode VARCHAR(100)
AS
BEGIN
	select ac.Id, ac.AccountNumber, ac.BankAccountName, ac.FirstName, ac.LastName 
	from [dbo].[Account] ac join [dbo].[Bank] ba
	ON ac.Bank_Id = ba.Id
	where ac.AccountNumber = @AccoutNumber and ba.BankCode =@BankCode
    
END
GO
