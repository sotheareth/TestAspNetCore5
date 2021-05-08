IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[spGetAllCustomer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE dbo.spGetAllCustomer
END

GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'[dbo].[spGetCustomer]') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE dbo.spGetCustomer
END

GO

CREATE PROCEDURE spGetAllCustomer
AS
BEGIN
	select * FROM customer
END

GO

CREATE PROCEDURE [dbo].spGetCustomer
(
	@customerId int
)
AS
BEGIN
	select * FROM customer
	WHERE CustomerId = @customerId
END

GO

IF EXISTS (SELECT * FROM sysobjects WHERE id = object_id(N'dbo.spCountNameLength') AND OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
	DROP PROCEDURE dbo.spCountNameLength
END

GO

CREATE PROCEDURE spCountNameLength
	@name nvarchar(1000),
	@count int OUTPUT
AS
BEGIN
	SET @count = LEN(@name)
END

GO