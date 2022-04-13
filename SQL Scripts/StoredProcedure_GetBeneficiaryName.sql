
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
