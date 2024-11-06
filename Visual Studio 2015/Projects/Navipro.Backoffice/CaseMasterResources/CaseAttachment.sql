CREATE TABLE [dbo].[CaseAttachment]
(
	[Entry No_] int IDENTITY(1,1) PRIMARY KEY,
	[Case No_] nvarchar(20) NOT NULL,
	[Line No_] int not null,
	[File Name] nvarchar(100) not null,
	[Created Date Time] datetime,
	[From E-mail] nvarchar(100) not null,
	[From Name] nvarchar(100) not null,
	[Data] image not null

)
