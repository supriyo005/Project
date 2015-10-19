--use master
--go

--drop database Mitchell

--Go

IF (NOT EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE 
('[' + name + ']' = 'Mitchell' OR name = 'Mitchell')))
BEGIN
	create database Mitchell
END

GO

use Mitchell
go

IF OBJECT_ID (N'Vehicles', N'U') IS not NULL 
BEGIN
 drop table Vehicles
END

IF OBJECT_ID (N'LossInfo', N'U') IS not NULL 
BEGIN
 drop table LossInfo
END


IF OBJECT_ID (N'MitchellClaim', N'U') IS not NULL 
BEGIN
 drop table MitchellClaim
END


IF OBJECT_ID (N'CauseOfLossCode', N'U') IS not NULL 
BEGIN
 drop table CauseOfLossCode
END


IF OBJECT_ID (N'StatusCode', N'U') IS not NULL 
BEGIN
 drop table StatusCode
END


IF OBJECT_ID (N'StatusCode', N'U') IS NULL 
BEGIN
	create table StatusCode
	(
		id tinyint NOT NULL,
		[status] varchar(50),
		PRIMARY KEY (id)
	)
	insert into StatusCode values (1,'OPEN'),(2,'CLOSED')
END

Go

IF OBJECT_ID (N'CauseOfLossCode', N'U') IS NULL 
BEGIN
	create table CauseOfLossCode
	(
		id tinyint NOT NULL,
		[cause] varchar(100),
		PRIMARY KEY (id)
	)
	
	insert into CauseOfLossCode values (1,'Collision'),(2,'Explosion'),(3,'Fire'),(4,'Hail'),(5,'Mechanical Breakdown'),(6,'Other')
END

GO

IF OBJECT_ID (N'MitchellClaim', N'U') IS NULL 
BEGIN
	create table MitchellClaim
	(
		id bigint IDENTITY(1,1),
		claimNumber nvarchar(max),
		claimantFirstName nvarchar(100) null,
		caimantLastName nvarchar(100) null,
		[status] tinyint null,
		lossdate datetime null,
		AssignedAdjusterID int null,
		PRIMARY KEY (id),
		FOREIGN KEY ([status]) REFERENCES StatusCode(id)
	)
END

Go

IF OBJECT_ID (N'LossInfo', N'U') IS NULL 
BEGIN
	create table LossInfo
	(
		id bigint IDENTITY(1,1),
		mitcheelclaim_id bigint,
		iCauseOfLoss int null,
		ReportedDate datetime null,
		LossDescription nvarchar(max) null
		,PRIMARY KEY (id)
		,FOREIGN KEY (mitcheelclaim_id) REFERENCES MitchellClaim(id)
	)
END

GO

IF OBJECT_ID (N'Vehicles', N'U') IS NULL 
BEGIN
	create table Vehicles
	(
		id bigint IDENTITY(1,1),
		mitcheelclaim_id bigint,
		ModelYear int,
		MakeDescription nvarchar(max) null,
		ModelDescription nvarchar(max) null,
		EngineDescription nvarchar(max) null,
		ExteriorColor nvarchar(max) null,
		Vin nvarchar(200) null,
		LicPlate nvarchar(200) null,
		LicPlateState nvarchar(200) null,
		LicPlateExpDate datetime null,
		DamageDescription nvarchar(200) null,
		Mileage int null
		,PRIMARY KEY (id)
		,FOREIGN KEY ([mitcheelclaim_id]) REFERENCES MitchellClaim(id)
	)
END


go

select * from [dbo].[StatusCode]
select * from [dbo].[CauseOfLossCode]
select * from [dbo].[MitchellClaim]
select * from [dbo].[LossInfo]
select * from [dbo].[Vehicles]