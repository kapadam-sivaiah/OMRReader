USE [OMRReader]
GO
/****** Object:  Table [dbo].[UserDetails]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDetails](
	[userID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nchar](20) NOT NULL,
	[Password] [nchar](10) NULL,
 CONSTRAINT [PK_UserDetails] PRIMARY KEY CLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReadingJobHistory]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReadingJobHistory](
	[JobID] [int] NOT NULL,
	[JobStartDate] [datetime] NULL,
	[JobEndDate] [datetime] NULL,
	[JobStatus] [varchar](50) NULL,
	[ProjectID] [int] NULL,
	[BasePath] [varchar](max) NULL,
	[RunType] [varchar](50) NULL,
	[Threshold] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ProjectMaster]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ProjectMaster](
	[ProjectID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectName] [varchar](100) NULL,
	[ProjectCode] [varchar](50) NULL,
 CONSTRAINT [PK__ProjectMas__9610CA388D415E86] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FrontBackPageMapping]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FrontBackPageMapping](
	[MapID] [int] IDENTITY(1,1) NOT NULL,
	[FrontTemplateID] [int] NULL,
	[BackTemplateID] [int] NULL,
 CONSTRAINT [PK_FrontBackPageMapping] PRIMARY KEY CLUSTERED 
(
	[MapID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DriveMaster]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DriveMaster](
	[DriveID] [int] IDENTITY(1,1) NOT NULL,
	[DriveName] [varchar](50) NULL,
	[DriveCode] [varchar](50) NULL,
 CONSTRAINT [PK__DriveMas__9610CA388D415E86] PRIMARY KEY CLUSTERED 
(
	[DriveID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[errors]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[errors](
	[SheetID] [nvarchar](255) NULL,
	[barcode] [nvarchar](255) NULL,
	[batch] [float] NULL,
	[omrstring] [nvarchar](255) NULL,
	[omrstring1] [nvarchar](255) NULL,
	[Path] [nvarchar](255) NULL,
	[flag] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApplicationLogs]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ApplicationLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[date] [datetime] NULL,
	[Thread] [varchar](255) NULL,
	[Level] [varchar](max) NULL,
	[Logger] [varchar](max) NULL,
	[Message] [varchar](max) NULL,
	[Exception] [varchar](max) NULL,
	[Source] [varchar](max) NULL,
	[HostName] [varchar](max) NULL,
	[ErrorMessage] [varchar](max) NULL,
	[InputValues] [varchar](max) NULL,
 CONSTRAINT [PK__Applicat__3214EC2791F29D5D] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[data]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[data](
	[BARCODE] [nvarchar](25) NULL,
	[REGNO] [nvarchar](8) NULL,
	[Center_Code] [nvarchar](3) NULL,
	[CentreCDbox] [nvarchar](3) NULL,
	[paper] [nvarchar](255) NULL,
	[TestPaperCode] [nvarchar](255) NULL,
	[QBNUMBER] [nvarchar](7) NULL,
	[omrstring] [nvarchar](255) NULL,
	[Qbseries] [nvarchar](255) NULL,
	[Combination] [nvarchar](255) NULL,
	[languageCD] [nvarchar](255) NULL,
	[A1] [nvarchar](255) NULL,
	[A2] [nvarchar](255) NULL,
	[A3] [nvarchar](255) NULL,
	[A4] [nvarchar](255) NULL,
	[A5] [nvarchar](255) NULL,
	[A6] [nvarchar](255) NULL,
	[A7] [nvarchar](255) NULL,
	[A8] [nvarchar](255) NULL,
	[A9] [nvarchar](255) NULL,
	[A10] [nvarchar](255) NULL,
	[A11] [nvarchar](255) NULL,
	[A12] [nvarchar](255) NULL,
	[A13] [nvarchar](255) NULL,
	[A14] [nvarchar](255) NULL,
	[A15] [nvarchar](255) NULL,
	[A16] [nvarchar](255) NULL,
	[A17] [nvarchar](255) NULL,
	[A18] [nvarchar](255) NULL,
	[A19] [nvarchar](255) NULL,
	[A20] [nvarchar](255) NULL,
	[A21] [nvarchar](255) NULL,
	[A22] [nvarchar](255) NULL,
	[A23] [nvarchar](255) NULL,
	[A24] [nvarchar](255) NULL,
	[A25] [nvarchar](255) NULL,
	[A26] [nvarchar](255) NULL,
	[A27] [nvarchar](255) NULL,
	[A28] [nvarchar](255) NULL,
	[A29] [nvarchar](255) NULL,
	[A30] [nvarchar](255) NULL,
	[A31] [nvarchar](255) NULL,
	[A32] [nvarchar](255) NULL,
	[A33] [nvarchar](255) NULL,
	[A34] [nvarchar](255) NULL,
	[A35] [nvarchar](255) NULL,
	[A36] [nvarchar](255) NULL,
	[A37] [nvarchar](255) NULL,
	[A38] [nvarchar](255) NULL,
	[A39] [nvarchar](255) NULL,
	[A40] [nvarchar](255) NULL,
	[A41] [nvarchar](255) NULL,
	[A42] [nvarchar](255) NULL,
	[A43] [nvarchar](255) NULL,
	[A44] [nvarchar](255) NULL,
	[A45] [nvarchar](255) NULL,
	[A46] [nvarchar](255) NULL,
	[A47] [nvarchar](255) NULL,
	[A48] [nvarchar](255) NULL,
	[A49] [nvarchar](255) NULL,
	[A50] [nvarchar](255) NULL,
	[A51] [nvarchar](255) NULL,
	[A52] [nvarchar](255) NULL,
	[A53] [nvarchar](255) NULL,
	[A54] [nvarchar](255) NULL,
	[A55] [nvarchar](255) NULL,
	[A56] [nvarchar](255) NULL,
	[A57] [nvarchar](255) NULL,
	[A58] [nvarchar](255) NULL,
	[A59] [nvarchar](255) NULL,
	[A60] [nvarchar](255) NULL,
	[A61] [nvarchar](255) NULL,
	[A62] [nvarchar](255) NULL,
	[A63] [nvarchar](255) NULL,
	[A64] [nvarchar](255) NULL,
	[A65] [nvarchar](255) NULL,
	[A66] [nvarchar](255) NULL,
	[A67] [nvarchar](255) NULL,
	[A68] [nvarchar](255) NULL,
	[A69] [nvarchar](255) NULL,
	[A70] [nvarchar](255) NULL,
	[A71] [nvarchar](255) NULL,
	[A72] [nvarchar](255) NULL,
	[A73] [nvarchar](255) NULL,
	[A74] [nvarchar](255) NULL,
	[A75] [nvarchar](255) NULL,
	[A76] [nvarchar](255) NULL,
	[A77] [nvarchar](255) NULL,
	[A78] [nvarchar](255) NULL,
	[A79] [nvarchar](255) NULL,
	[A80] [nvarchar](255) NULL,
	[A81] [nvarchar](255) NULL,
	[A82] [nvarchar](255) NULL,
	[A83] [nvarchar](255) NULL,
	[A84] [nvarchar](255) NULL,
	[A85] [nvarchar](255) NULL,
	[A86] [nvarchar](255) NULL,
	[A87] [nvarchar](255) NULL,
	[A88] [nvarchar](255) NULL,
	[A89] [nvarchar](255) NULL,
	[A90] [nvarchar](255) NULL,
	[A91] [nvarchar](255) NULL,
	[A92] [nvarchar](255) NULL,
	[A93] [nvarchar](255) NULL,
	[A94] [nvarchar](255) NULL,
	[A95] [nvarchar](255) NULL,
	[A96] [nvarchar](255) NULL,
	[A97] [nvarchar](255) NULL,
	[A98] [nvarchar](255) NULL,
	[A99] [nvarchar](255) NULL,
	[A100] [nvarchar](255) NULL,
	[A101] [nvarchar](255) NULL,
	[A102] [nvarchar](255) NULL,
	[A103] [nvarchar](255) NULL,
	[A104] [nvarchar](255) NULL,
	[A105] [nvarchar](255) NULL,
	[A106] [nvarchar](255) NULL,
	[A107] [nvarchar](255) NULL,
	[A108] [nvarchar](255) NULL,
	[A109] [nvarchar](255) NULL,
	[A110] [nvarchar](255) NULL,
	[A111] [nvarchar](255) NULL,
	[A112] [nvarchar](255) NULL,
	[A113] [nvarchar](255) NULL,
	[A114] [nvarchar](255) NULL,
	[A115] [nvarchar](255) NULL,
	[A116] [nvarchar](255) NULL,
	[A117] [nvarchar](255) NULL,
	[A118] [nvarchar](255) NULL,
	[A119] [nvarchar](255) NULL,
	[A120] [nvarchar](255) NULL,
	[A121] [nvarchar](255) NULL,
	[A122] [nvarchar](255) NULL,
	[A123] [nvarchar](255) NULL,
	[A124] [nvarchar](255) NULL,
	[A125] [nvarchar](255) NULL,
	[A126] [nvarchar](255) NULL,
	[A127] [nvarchar](255) NULL,
	[A128] [nvarchar](255) NULL,
	[A129] [nvarchar](255) NULL,
	[A130] [nvarchar](255) NULL,
	[A131] [nvarchar](255) NULL,
	[A132] [nvarchar](255) NULL,
	[A133] [nvarchar](255) NULL,
	[A134] [nvarchar](255) NULL,
	[A135] [nvarchar](255) NULL,
	[A136] [nvarchar](255) NULL,
	[A137] [nvarchar](255) NULL,
	[A138] [nvarchar](255) NULL,
	[A139] [nvarchar](255) NULL,
	[A140] [nvarchar](255) NULL,
	[A141] [nvarchar](255) NULL,
	[A142] [nvarchar](255) NULL,
	[A143] [nvarchar](255) NULL,
	[A144] [nvarchar](255) NULL,
	[A145] [nvarchar](255) NULL,
	[A146] [nvarchar](255) NULL,
	[A147] [nvarchar](255) NULL,
	[A148] [nvarchar](255) NULL,
	[A149] [nvarchar](255) NULL,
	[A150] [nvarchar](255) NULL,
	[A151] [nvarchar](255) NULL,
	[A152] [nvarchar](255) NULL,
	[A153] [nvarchar](255) NULL,
	[A154] [nvarchar](255) NULL,
	[A155] [nvarchar](255) NULL,
	[A156] [nvarchar](255) NULL,
	[A157] [nvarchar](255) NULL,
	[A158] [nvarchar](255) NULL,
	[A159] [nvarchar](255) NULL,
	[A160] [nvarchar](255) NULL,
	[A161] [nvarchar](255) NULL,
	[A162] [nvarchar](255) NULL,
	[A163] [nvarchar](255) NULL,
	[A164] [nvarchar](255) NULL,
	[A165] [nvarchar](255) NULL,
	[A166] [nvarchar](255) NULL,
	[A167] [nvarchar](255) NULL,
	[A168] [nvarchar](255) NULL,
	[A169] [nvarchar](255) NULL,
	[A170] [nvarchar](255) NULL,
	[A171] [nvarchar](255) NULL,
	[A172] [nvarchar](255) NULL,
	[A173] [nvarchar](255) NULL,
	[A174] [nvarchar](255) NULL,
	[A175] [nvarchar](255) NULL,
	[A176] [nvarchar](255) NULL,
	[A177] [nvarchar](255) NULL,
	[A178] [nvarchar](255) NULL,
	[A179] [nvarchar](255) NULL,
	[A180] [nvarchar](255) NULL,
	[A181] [nvarchar](255) NULL,
	[A182] [nvarchar](255) NULL,
	[A183] [nvarchar](255) NULL,
	[A184] [nvarchar](255) NULL,
	[A185] [nvarchar](255) NULL,
	[A186] [nvarchar](255) NULL,
	[A187] [nvarchar](255) NULL,
	[A188] [nvarchar](255) NULL,
	[A189] [nvarchar](255) NULL,
	[A190] [nvarchar](255) NULL,
	[A191] [nvarchar](255) NULL,
	[A192] [nvarchar](255) NULL,
	[A193] [nvarchar](255) NULL,
	[A194] [nvarchar](255) NULL,
	[A195] [nvarchar](255) NULL,
	[A196] [nvarchar](255) NULL,
	[A197] [nvarchar](255) NULL,
	[A198] [nvarchar](255) NULL,
	[A199] [nvarchar](255) NULL,
	[A200] [nvarchar](255) NULL,
	[A201] [nvarchar](255) NULL,
	[A202] [nvarchar](255) NULL,
	[A203] [nvarchar](255) NULL,
	[A204] [nvarchar](255) NULL,
	[A205] [nvarchar](255) NULL,
	[A206] [nvarchar](255) NULL,
	[A207] [nvarchar](255) NULL,
	[A208] [nvarchar](255) NULL,
	[A209] [nvarchar](255) NULL,
	[A210] [nvarchar](255) NULL,
	[A211] [nvarchar](255) NULL,
	[A212] [nvarchar](255) NULL,
	[A213] [nvarchar](255) NULL,
	[A214] [nvarchar](255) NULL,
	[A215] [nvarchar](255) NULL,
	[A216] [nvarchar](255) NULL,
	[A217] [nvarchar](255) NULL,
	[A218] [nvarchar](255) NULL,
	[A219] [nvarchar](255) NULL,
	[A220] [nvarchar](255) NULL,
	[A221] [nvarchar](255) NULL,
	[A222] [nvarchar](255) NULL,
	[A223] [nvarchar](255) NULL,
	[A224] [nvarchar](255) NULL,
	[A225] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[CheckUser]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CheckUser] 
(

@UserName nvarchar(20),
@Password nvarchar(20)
)
AS

BEGIN
  SELECT Username,Password FROM [dbo].[UserDetails] WHERE UserName = @UserName and Password=@Password
  
  END
GO
/****** Object:  StoredProcedure [dbo].[ApplicationLogs_Sp]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ApplicationLogs_Sp] 
(

@date DATETIME,
@Thread VARCHAR(255),
@Level VARCHAR(max),
@Logger VARCHAR(max),
@Message VARCHAR(max),
@Exception VARCHAR(max),
@Source VARCHAR(max),
@HostName VARCHAR(max)
)
AS
  BEGIN TRY
        INSERT INTO ApplicationLogs(date,Thread,Level,Logger,Message,Exception,Source,HostName)VALUES(@date,@Thread,@Level,@Logger,@Message,@Exception,@Source,@HostName)
  END TRY
  BEGIN CATCH
     RAISERROR ('invalid log', 16, 1)
  END CATCH
GO
/****** Object:  StoredProcedure [dbo].[DriveMaster_Sp]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DriveMaster_Sp] 
(

@DriveName VARCHAR(50),
@DriveCode VARCHAR(50)
)
AS
IF @DriveName IS NOT NULL and @DriveName<>''
BEGIN
    BEGIN TRY
       IF NOT EXISTS(SELECT DriveName FROM DriveMaster WHERE DriveName = @DriveName)
        BEGIN
            INSERT INTO DriveMaster(DriveName,DriveCode)values(@DriveName,@DriveCode)
        END
     --ELSE
       --BEGIN
       --   UPDATE DriveMaster set DriveName=@DriveName,DriveCode=@DriveCode where DriveID=@DriveID
       --END
     END TRY
  BEGIN CATCH
  DECLARE @ERROR_MSG VARCHAR(500),@XSTATE INT,@ERROR_NO INT
  SELECT @ERROR_MSG=ERROR_MESSAGE(),@XSTATE=XACT_STATE(),@ERROR_NO=ERROR_NUMBER()
     RAISERROR('ERROR',@ERROR_MSG,@XSTATE,@ERROR_NO)
  END CATCH
  END
  ELSE
  BEGIN
   RAISERROR ('INVALID DEVICE ID',16,1)
   END;
GO
/****** Object:  StoredProcedure [dbo].[GetProjectByID]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[GetProjectByID] 
(
@ProjectID int
)

AS
BEGIN
        SELECT ProjectID,ProjectName,ProjectCode from ProjectMaster where ProjectID=@ProjectID
END
GO
/****** Object:  StoredProcedure [dbo].[GetProject]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetProject] 

AS
BEGIN
        SELECT ProjectID,ProjectName,ProjectCode from ProjectMaster order by ProjectID
END
GO
/****** Object:  Table [dbo].[ReadingJobs]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReadingJobs](
	[JobID] [int] IDENTITY(1,1) NOT NULL,
	[JobStartDate] [datetime] NULL,
	[JobEndDate] [datetime] NULL,
	[JobStatus] [varchar](50) NULL,
	[ProjectID] [int] NULL,
	[BasePath] [varchar](max) NULL,
	[RunType] [varchar](50) NULL,
	[Threshold] [int] NULL,
 CONSTRAINT [PK__ReadingJ__056690E2CB335965] PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SaveProject]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveProject] 
(

@ProjectName VARCHAR(100),
@ProjectCode VARCHAR(100),
@ProjectID INT OUT
)
AS
BEGIN

	IF NOT EXISTS(SELECT ProjectID FROM ProjectMaster  WHERE ProjectName = @ProjectName)
	BEGIN
		INSERT INTO ProjectMaster( ProjectName, ProjectCode)
		VALUES(@ProjectName,@ProjectCode)

		SELECT @ProjectID = SCOPE_IDENTITY()

	END
	ELSE
	BEGIN
	RAISERROR( 'Project already exists',16,1)	
	END
	
END
GO
/****** Object:  Table [dbo].[TemplateMaster]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TemplateMaster](
	[TemplateID] [int] IDENTITY(1,1) NOT NULL,
	[TemplateName] [varchar](50) NULL,
	[TemplateCode] [varchar](50) NULL,
	[TemplateLeft] [int] NOT NULL,
	[TemplateTop] [int] NOT NULL,
	[TemplateIndex] [varchar](max) NOT NULL,
	[TrackCount] [int] NOT NULL,
	[PageType] [int] NOT NULL,
	[IsDuplex] [bit] NULL,
	[DriveID] [int] NULL,
	[TemplateFileName] [varchar](max) NULL,
	[TemplateFilePath] [varchar](max) NULL,
	[TemplateImage] [varchar](max) NULL,
	[ProjectID] [int] NULL,
	[AllowedErrorCharCount] [int] NULL,
 CONSTRAINT [PK__Template__F87ADD272DDE2522] PRIMARY KEY CLUSTERED 
(
	[TemplateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TemplateFieldMaster]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TemplateFieldMaster](
	[FieldId] [int] IDENTITY(1,1) NOT NULL,
	[TemplateID] [int] NOT NULL,
	[FieldOrderID] [int] NULL,
	[FieldName] [varchar](50) NOT NULL,
	[StartRowNo] [int] NULL,
	[StartIndex] [int] NULL,
	[FieldType] [char](10) NULL,
	[FieldTop] [int] NULL,
	[FieldLeft] [int] NULL,
	[FieldIndex] [varchar](max) NULL,
	[NoRows] [int] NULL,
	[NoColumns] [int] NULL,
	[BubbleWidth] [int] NULL,
	[BubbleHeight] [int] NULL,
	[BubbleRowGap] [int] NULL,
	[BubbleColumnGap] [int] NULL,
	[annotationWidth] [int] NULL,
	[annotationHeight] [int] NULL,
	[CreateOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
 CONSTRAINT [PK__Template__C8B6FF07EB4FC0C4] PRIMARY KEY CLUSTERED 
(
	[FieldId] ASC,
	[FieldName] ASC,
	[TemplateID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[UpdateJobEndDate]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateJobEndDate] 
(
@JobID INT
)
AS
BEGIN
UPDATE [dbo].[ReadingJobs] set JobEndDate=GETDATE() from [dbo].[ReadingJobs]
WHERE JobID=@JobID 
END
GO
/****** Object:  Table [dbo].[ReadTask]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReadTask](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[JobID] [int] NULL,
	[TemplateID] [int] NULL,
	[FolderPath] [varchar](max) NULL,
	[TotalFileCount] [int] NULL,
	[SuccessfulFileCount] [int] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[RunType] [varchar](50) NULL,
 CONSTRAINT [PK_ReadTask] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[GetTemplate]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTemplate] 
(
@ProjectID int
)
AS
BEGIN
        SELECT TemplateID,TemplateName,TemplateCode,TemplateLeft,TemplateTop,TemplateIndex,TrackCount,PageType,IsDuplex,DriveID,TemplateFileName,TemplateFilePath,TemplateImage,AllowedErrorCharCount from TemplateMaster where ProjectID=@ProjectID and PageType=0 order by TemplateID
END
GO
/****** Object:  StoredProcedure [dbo].[GetFrontPageTemplate]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetFrontPageTemplate] 
(
@ProjectID int
)
AS
BEGIN
        SELECT TemplateID,TemplateName,AllowedErrorCharCount from TemplateMaster where ProjectID=@ProjectID AND PageType=0
END
GO
/****** Object:  Table [dbo].[FileStatus]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FileStatus](
	[FileID] [int] IDENTITY(1,1) NOT NULL,
	[TaskID] [int] NULL,
	[FileName] [varchar](max) NULL,
	[FilePath] [varchar](max) NULL,
	[Status] [int] NULL,
	[ErrorMessage] [varchar](max) NULL,
	[IsDelete] [bit] NULL,
	[RunType] [varchar](50) NULL,
 CONSTRAINT [PK_FileStatus] PRIMARY KEY CLUSTERED 
(
	[FileID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[DeleteFieldID]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[DeleteFieldID] 
(
@FieldID int

)

AS
BEGIN
        delete from TemplateFieldMaster where FieldID=@FieldID
END
GO
/****** Object:  StoredProcedure [dbo].[GetTemplateFields]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTemplateFields] 
(

@TemplateID INT

)
AS
BEGIN
IF @TemplateID IS NOT NULL 
BEGIN
  SELECT FieldID,FieldName,FieldIndex,FieldLeft,StartRowNo,FieldTop,FieldLeft,NoColumns,NoRows,StartIndex,BubbleWidth,BubbleHeight,BubbleRowGap,BubbleColumnGap,AnnotationHeight,AnnotationWidth FROM TemplateFieldMaster WHERE TemplateID = @TemplateID order by StartIndex
  END
  ELSE
  BEGIN
  RAISERROR ('TemplateID Is Invalied',16,1)
  END
  END
GO
/****** Object:  StoredProcedure [dbo].[GetTemplateByID]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetTemplateByID] 
(

@TemplateID INT

)
AS
BEGIN
IF @TemplateID IS NOT NULL 
BEGIN
  SELECT TemplateID, TemplateName, TemplateCode, TemplateLeft, TemplateTop, TemplateIndex, TrackCount, PageType, IsDuplex, DriveID, TemplateFileName,TemplateFilePath, TemplateImage, ProjectID,AllowedErrorCharCount FROM [dbo].[TemplateMaster] WHERE TemplateID = @TemplateID 
  SELECT FieldId, TemplateID, FieldOrderID, FieldName, StartRowNo, StartIndex, FieldType, FieldTop, FieldLeft, FieldIndex, NoRows, NoColumns, BubbleWidth, BubbleHeight, BubbleRowGap, BubbleColumnGap, annotationWidth, annotationHeight, CreateOn, UpdateOn FROM TemplateFieldMaster WHERE TemplateID = @TemplateID 
  
  SELECT FrontTemplateID,BackTemplateID FROM FrontBackPageMapping WHERE BackTemplateID =@TemplateID
  
  END
  ELSE
  BEGIN
  RAISERROR ('TemplateID Is Invalied',16,1)
  END
  END
GO
/****** Object:  StoredProcedure [dbo].[GetJobByID]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetJobByID] 
(

@JobID INT

)
AS
BEGIN
IF @JobID IS NOT NULL 
BEGIN
  SELECT JobID, JobStartDate, JobEndDate, JobStatus, ProjectID, BasePath, RunType, Threshold FROM [dbo].[ReadingJobs] WHERE JobID = @JobID 
  
  --SELECT TaskID,  [dbo].[ReadingJobs].JobID, TemplateID, FolderPath, TotalFileCount, SuccessfulFileCount, StartDate, EndDate FROM [dbo].[ReadTask]
  --INNER JOIN [dbo].[ReadingJobs] ON [dbo].[ReadTask].JobID =  [dbo].[ReadingJobs].JobID AND [dbo].[ReadTask].RunType =  [dbo].[ReadingJobs].RunType
  --WHERE  [dbo].[ReadingJobs].JobID = @JobID 

    ;WITH cte AS
(
   SELECT  TaskID,  [dbo].[ReadingJobs].JobID, TemplateID, FolderPath, TotalFileCount, SuccessfulFileCount, StartDate, EndDate,
         ROW_NUMBER() OVER (PARTITION BY FolderPath ORDER BY TaskID DESC) AS rn
  FROM [dbo].[ReadTask]  INNER JOIN [dbo].[ReadingJobs] ON [dbo].[ReadTask].JobID =  [dbo].[ReadingJobs].JobID AND [dbo].[ReadTask].RunType =  [dbo].[ReadingJobs].RunType
  WHERE  [dbo].[ReadingJobs].JobID = @JobID 
)
SELECT *
FROM cte
WHERE rn = 1


  --SELECT TemplateID, TemplateName, TemplateCode, TemplateLeft, TemplateTop, TemplateIndex, TrackCount, PageType, IsDuplex, DriveID, TemplateFileName, TemplateImage, ProjectID FROM TemplateMaster WHERE TemplateID IN
  --( SELECT DISTINCT TemplateID FROM  [ReadTask] WHERE JobID = @JobID   )

  --SELECT FieldId, TemplateID, FieldOrderID, FieldName, StartRowNo, StartIndex, FieldType, FieldTop, FieldLeft, FieldIndex, NoRows, NoColumns, BubbleWidth, BubbleHeight, BubbleRowGap, BubbleColumnGap, annotationWidth, annotationHeight, CreateOn, UpdateOn FROM TemplateFieldMaster WHERE TemplateID IN 
  --( SELECT DISTINCT TemplateID FROM  [ReadTask] WHERE JobID = @JobID   )
  
  END
  ELSE
  BEGIN
  RAISERROR ('JobID Is Invalied',16,1)
  END
  END
GO
/****** Object:  Table [dbo].[SheetValues]    Script Date: 08/09/2016 11:19:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SheetValues](
	[SheetID] [int] IDENTITY(1,1) NOT NULL,
	[SheetName] [varchar](50) NULL,
	[TaskID] [int] NULL,
	[SheetValues] [varchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[CreateOn] [datetime] NULL,
	[UpdateOn] [datetime] NULL,
	[ExceptionMessage] [varchar](max) NULL,
	[IsDisplayed] [bit] NULL,
	[HasError] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK__SheetVal__30B27388681A464A] PRIMARY KEY CLUSTERED 
(
	[SheetID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[SaveTemplate]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveTemplate] 
(
@TemplateXML XML
)
AS
BEGIN
DECLARE @TemplateID INT,@PageType int

	INSERT INTO [dbo].[TemplateMaster](TemplateName, TemplateCode, TemplateLeft, TemplateTop, TemplateIndex, TrackCount, PageType, IsDuplex, DriveID, ProjectID,TemplateImage,TemplateFileName,TemplateFilePath,AllowedErrorCharCount)
	SELECT
		TemplateInfo.value('(TemplateName/text())[1]','varchar(100)') AS TemplateName,
		TemplateInfo.value('(TemplateCode/text())[1]','varchar(100)') AS TemplateCode,
		TemplateInfo.value('(TrackLeft/text())[1]','int') AS TemplateLeft,
		TemplateInfo.value('(TrackTop/text())[1]','int') AS TemplateTop,
		TemplateInfo.value('(TrackIndex/text())[1]','varchar(max)') AS TemplateIndex,
		TemplateInfo.value('(TrackCount/text())[1]','int') AS TrackCount,
		TemplateInfo.value('(Page/text())[1]','int') AS PageType,
		TemplateInfo.value('(IsDuplex/text())[1]','bit') AS IsDuplex,
		TemplateInfo.value('(DriveID/text())[1]','int') AS DriveID,
		TemplateInfo.value('(ProjectID/text())[1]','int') AS ProjectID,
		TemplateInfo.value('(TemplateImage/text())[1]','varchar(max)') AS TemplateImage,
		TemplateInfo.value('(TemplateFileName/text())[1]','varchar(max)') AS TemplateFileName,
		TemplateInfo.value('(TemplateFilePath/text())[1]','varchar(max)') AS TemplateFilePath,
		TemplateInfo.value('(AllowedErrorCharCount/text())[1]','int') AS AllowedErrorCharCount
	FROM @TemplateXML.nodes('/Template') AS TEMPTABLE(TemplateInfo)

	
	--GET THE TEMPLATE ID FIELD VALUE
	SELECT @TemplateID=SCOPE_IDENTITY()

	
	INSERT INTO [dbo].[TemplateFieldMaster](TemplateID,FieldOrderID, FieldName, StartRowNo, FieldType, FieldTop, 
	FieldLeft, StartIndex, NoRows, NoColumns, BubbleWidth, BubbleHeight, BubbleRowGap, BubbleColumnGap, 
	annotationWidth, annotationHeight, FieldIndex)
	SELECT @TemplateID,
	    TemplateInfo.value('(FieldID/text())[1]','int') AS FieldOrderID,
		TemplateInfo.value('(FieldName/text())[1]','varchar(100)') AS FieldName,
		TemplateInfo.value('(TrackRowNo/text())[1]','int') AS StartRowNo,
		TemplateInfo.value('(TemplateFieldType/text())[1]','char(3)') AS FieldType,	
		TemplateInfo.value('(FieldTopPosition/text())[1]','int') AS FieldTop,
		TemplateInfo.value('(FieldLeftPosition/text())[1]','int') AS FieldLeft,
		TemplateInfo.value('(StartIndex/text())[1]','int') AS StartIndex,
		TemplateInfo.value('(FieldRowNo/text())[1]','int') AS NoRows,
		TemplateInfo.value('(FieldColumnNo/text())[1]','int') AS NoColumns,
		TemplateInfo.value('(BubbleWidth/text())[1]','int') AS BubbleWidth,
		TemplateInfo.value('(BubbleHeight/text())[1]','int') AS BubbleHeight,
		TemplateInfo.value('(BubbleRowGap/text())[1]','int') AS BubbleRowGap,
		TemplateInfo.value('(BubbleColumnGap/text())[1]','int') AS BubbleColumnGap,
		TemplateInfo.value('(AnnotationWidth/text())[1]','int') AS annotationWidth,
		TemplateInfo.value('(AnnotationHeight/text())[1]','int') AS annotationHeight,
		TemplateInfo.value('(FieldIndex/text())[1]','varchar(max)') AS FieldIndex
	FROM @TemplateXML.nodes('/Template/FieldList/TemplateField') AS TEMPTABLE(TemplateInfo)


	INSERT INTO [dbo].[FrontBackPageMapping]( FrontTemplateID, BackTemplateID)
	SELECT  MapperInfo.value('(FrontPageTemplateID/text())[1]','int') as   FrontTemplateID ,@TemplateID
	FROM @TemplateXML.nodes('/Template/FrontBackTemplateMapperList/FrontBackPageMapper') AS TEMPTABLE(MapperInfo)

	--update front template as duplex
	UPDATE TemplateMaster SET IsDuplex = 1 WHERE TemplateID IN
	(SELECT FrontTemplateID FROM FrontBackPageMapping WHERE BackTemplateID= @TemplateID)



END
GO
/****** Object:  StoredProcedure [dbo].[SaveTask]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveTask] 
(
@JobID INT ,
@TemplateID int ,
@TotalFileCount int,
@RunType bit
)
AS
DECLARE @TaskID INT
BEGIN
			INSERT INTO [dbo].[ReadTask](JobID, TemplateID,  TotalFileCount,RunType)
				values(@JobID,@TemplateID,@TotalFileCount,@RunType)
	SELECT @TaskID=SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[GetBackTemplateByID]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetBackTemplateByID] 
(

@TemplateID INT

)
AS
BEGIN
IF @TemplateID IS NOT NULL 
BEGIN

DECLARE @BackTemplateID INT

SELECT @BackTemplateID =BackTemplateID FROM FrontBackPageMapping WHERE FrontTemplateID =@TemplateID

  SELECT TemplateID, TemplateName, TemplateCode, TemplateLeft, TemplateTop, TemplateIndex, TrackCount, PageType, IsDuplex, DriveID, TemplateFileName,TemplateFilePath, TemplateImage, ProjectID,AllowedErrorCharCount FROM [dbo].[TemplateMaster] WHERE TemplateID = @BackTemplateID 
  SELECT FieldId, TemplateID, FieldOrderID, FieldName, StartRowNo, StartIndex, FieldType, FieldTop, FieldLeft, FieldIndex, NoRows, NoColumns, BubbleWidth, BubbleHeight, BubbleRowGap, BubbleColumnGap, annotationWidth, annotationHeight, CreateOn, UpdateOn FROM TemplateFieldMaster WHERE TemplateID = @BackTemplateID 
   
  
  END
  ELSE
  BEGIN
  RAISERROR ('TemplateID Is Invalied',16,1)
  END
  END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTemplate]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTemplate] 
(
@TemplateXML XML
)
AS
BEGIN
DECLARE @TemplateID INT,@PageType int


CREATE TABLE #Template(
		[TemplateID] [int],
	[TemplateName] [varchar](50) NULL,
	[TemplateCode] [varchar](50) NULL,
	[TemplateLeft] [int] NOT NULL,
	[TemplateTop] [int] NOT NULL,
	[TemplateIndex] [varchar](max) NOT NULL,
	[TrackCount] [int] NOT NULL,
	[PageType] [int] NOT NULL,
	[IsDuplex] [bit] NULL,
	[DriveID] [int] NULL,
	[TemplateFileName] [varchar](max) NULL,
	[TemplateImage] [varchar](max) NULL,
	[ProjectID] [int] NULL,
	AllowedErrorCharCount INT NOT NULL
)

INSERT INTO #Template(TemplateID, TemplateName, TemplateCode, TemplateLeft, TemplateTop, TemplateIndex, TrackCount, PageType, IsDuplex, DriveID,ProjectID, TemplateImage, TemplateFileName,AllowedErrorCharCount)
SELECT
		TemplateInfo.value('(TemplateID/text())[1]','int') AS TemplateID,
		TemplateInfo.value('(TemplateName/text())[1]','varchar(100)') AS TemplateName,
		TemplateInfo.value('(TemplateCode/text())[1]','varchar(100)') AS TemplateCode,
		TemplateInfo.value('(TrackLeft/text())[1]','int') AS TemplateLeft,
		TemplateInfo.value('(TrackTop/text())[1]','int') AS TemplateTop,
		TemplateInfo.value('(TrackIndex/text())[1]','varchar(max)') AS TemplateIndex,
		TemplateInfo.value('(TrackCount/text())[1]','int') AS TrackCount,
		TemplateInfo.value('(Page/text())[1]','int') AS PageType,
		TemplateInfo.value('(IsDuplex/text())[1]','bit') AS IsDuplex,
		TemplateInfo.value('(DriveID/text())[1]','int') AS DriveID,
		TemplateInfo.value('(ProjectID/text())[1]','int') AS ProjectID,
		TemplateInfo.value('(TemplateImage/text())[1]','varchar(max)') AS TemplateImage,
		TemplateInfo.value('(TemplateFileName/text())[1]','varchar(max)') AS TemplateFileName,
		TemplateInfo.value('(AllowedErrorCharCount/text())[1]','int') AS AllowedErrorCharCount
	FROM @TemplateXML.nodes('/Template') AS TEMPTABLE(TemplateInfo)


	SELECT @TemplateID = TemplateID FROM  #Template 

	UPDATE TM SET TM.TemplateName = TT.TemplateName,TM.DriveID=TT.DriveID,TM.IsDuplex=TT.IsDuplex,TM.PageType=TT.PageType,TM.ProjectID=TT.ProjectID,TM.TemplateCode=TT.TemplateCode,
	TM.TemplateFileName=TT.TemplateFileName,TM.TemplateImage=TT.TemplateImage,TM.TemplateIndex=TT.TemplateIndex,TM.TemplateLeft=TT.TemplateLeft,TM.TemplateTop=TT.TemplateTop,
	TM.TrackCount=TT.TrackCount,TM.AllowedErrorCharCount=TT.AllowedErrorCharCount
	FROM [dbo].[TemplateMaster] AS TM, #Template AS TT
	WHERE TM.TemplateID = TT.TemplateID
	
	CREATE TABLE #TemplateFields
	(
	[FieldId] [int] NOT NULL,
	[TemplateID] [int] NOT NULL,
	[FieldOrderID] [int] NULL,
	[FieldName] [varchar](50) NOT NULL,
	[StartRowNo] [int] NULL,
	[StartIndex] [int] NULL,
	[FieldType] [char](10) NULL,
	[FieldTop] [int] NULL,
	[FieldLeft] [int] NULL,
	[FieldIndex] [varchar](max) NULL,
	[NoRows] [int] NULL,
	[NoColumns] [int] NULL,
	[BubbleWidth] [int] NULL,
	[BubbleHeight] [int] NULL,
	[BubbleRowGap] [int] NULL,
	[BubbleColumnGap] [int] NULL,
	[annotationWidth] [int] NULL,
	[annotationHeight] [int] NULL,
	[CreateOn] [datetime] NULL,
	[UpdateOn] [datetime]
	)

	INSERT INTO #TemplateFields (TemplateID,FieldID,FieldName, StartRowNo, FieldType, FieldTop, 
	FieldLeft, StartIndex, NoRows, NoColumns, BubbleWidth, BubbleHeight, BubbleRowGap, BubbleColumnGap, 
	annotationWidth, annotationHeight, FieldIndex)
	SELECT @TemplateID,
	    TemplateInfo.value('(FieldID/text())[1]','int') AS FieldID,
		TemplateInfo.value('(FieldName/text())[1]','varchar(100)') AS FieldName,
		TemplateInfo.value('(TrackRowNo/text())[1]','int') AS StartRowNo,
		TemplateInfo.value('(TemplateFieldType/text())[1]','char(3)') AS FieldType,	
		TemplateInfo.value('(FieldTopPosition/text())[1]','int') AS FieldTop,
		TemplateInfo.value('(FieldLeftPosition/text())[1]','int') AS FieldLeft,
		TemplateInfo.value('(StartIndex/text())[1]','int') AS StartIndex,
		TemplateInfo.value('(FieldRowNo/text())[1]','int') AS NoRows,
		TemplateInfo.value('(FieldColumnNo/text())[1]','int') AS NoColumns,
		TemplateInfo.value('(BubbleWidth/text())[1]','int') AS BubbleWidth,
		TemplateInfo.value('(BubbleHeight/text())[1]','int') AS BubbleHeight,
		TemplateInfo.value('(BubbleRowGap/text())[1]','int') AS BubbleRowGap,
		TemplateInfo.value('(BubbleColumnGap/text())[1]','int') AS BubbleColumnGap,
		TemplateInfo.value('(AnnotationWidth/text())[1]','int') AS annotationWidth,
		TemplateInfo.value('(AnnotationHeight/text())[1]','int') AS annotationHeight,
		TemplateInfo.value('(FieldIndex/text())[1]','varchar(max)') AS FieldIndex
	FROM @TemplateXML.nodes('/Template/FieldList/TemplateField') AS TEMPTABLE(TemplateInfo)


	--update 

	UPDATE FM SET FM.FieldName = FL.FieldName,FM.StartRowNo=FL.StartRowNo,FM.FieldIndex=FL.FieldIndex,FM.FieldLeft=FL.FieldLeft,FM.FieldOrderID=FL.FieldOrderID,FM.FieldTop=FL.FieldTop,FM.FieldType=FL.FieldType,
	FM.StartIndex=FL.StartIndex,FM.annotationHeight=FL.annotationHeight,FM.annotationWidth=FL.annotationWidth,FM.BubbleColumnGap=FL.BubbleColumnGap,FM.BubbleHeight=FL.BubbleHeight,FM.BubbleRowGap=FL.BubbleRowGap,FM.BubbleWidth=FL.BubbleWidth,
	FM.NoColumns=FL.NoColumns,FM.NoRows=FL.NoRows
	              
	FROM [dbo].[TemplateFieldMaster] AS FM, #TemplateFields FL
	WHERE FM.FieldId = FL.FieldId 

	--insert
	INSERT INTO [dbo].[TemplateFieldMaster](TemplateID,FieldName, StartRowNo, FieldType, FieldTop, 
	FieldLeft, StartIndex, NoRows, NoColumns, BubbleWidth, BubbleHeight, BubbleRowGap, BubbleColumnGap, 
	annotationWidth, annotationHeight, FieldIndex)
	SELECT @TemplateID,FieldName, StartRowNo, FieldType, FieldTop, 
	FieldLeft, StartIndex, NoRows, NoColumns, BubbleWidth, BubbleHeight, BubbleRowGap, BubbleColumnGap, 
	annotationWidth, annotationHeight, FieldIndex FROM #TemplateFields WHERE FieldId=0



	--DELETE existing mappings
	DELETE FROM [FrontBackPageMapping] WHERE BackTemplateID = @TemplateID

	INSERT INTO [dbo].[FrontBackPageMapping]( FrontTemplateID, BackTemplateID)
	SELECT  MapperInfo.value('(FrontPageTemplateID/text())[1]','int') as   FrontTemplateID ,@TemplateID
	FROM @TemplateXML.nodes('/Template/FrontBackTemplateMapperList/FrontBackPageMapper') AS TEMPTABLE(MapperInfo)

	--update front template as duplex
	UPDATE TemplateMaster SET IsDuplex = 1 WHERE TemplateID IN
	(SELECT FrontTemplateID FROM FrontBackPageMapping WHERE BackTemplateID= @TemplateID)

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTaskStartDate]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTaskStartDate] 
(
@TaskID INT
)
AS
BEGIN
UPDATE [dbo].[ReadTask] set StartDate=GETDATE() FROM [dbo].[ReadTask]
WHERE TaskID=@TaskID 

END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTaskEndDate]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTaskEndDate] 
(
@TaskID INT
)
AS
BEGIN
UPDATE [dbo].[ReadTask] set EndDate=GETDATE() from [dbo].[ReadTask]
WHERE TaskID=@TaskID 
END
GO
/****** Object:  StoredProcedure [dbo].[SaveJob]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveJob] 
(
@JobXML XML,
@JobID INT OUT,
@ErrorMessage VARCHAR(MAX) OUT
)
AS
BEGIN
CREATE TABLE #Job
(
    [JobID] [int] IDENTITY(1,1) NOT NULL,
	[JobStartDate] [datetime] NULL,
	[JobEndDate] [datetime] NULL,
	[JobStatus] [varchar](50) NULL,
	[ProjectID] [int] NULL,
	[BasePath] [varchar](max) NULL,
	[RunType] [varchar](50) NULL,
	Threshold INT
	)

	INSERT INTO #Job(ProjectID,JobStartDate,JobStatus,BasePath,RunType,Threshold)
	SELECT 
    	JobInfo.value('(ProjectID/text())[1]','int') AS ProjectID,
		GETDATE(),	
		JobInfo.value('(JobStatus/text())[1]','varchar(50)') AS JobStatus,
		JobInfo.value('(BasePath/text())[1]','varchar(max)') AS BasePath,
		JobInfo.value('(Type/text())[1]','varchar(50)') AS RunType,
		JobInfo.value('(Threshold/text())[1]','int') AS Threshold		
	FROM @JobXML.nodes('/JobDetails') AS TEMPTABLE(JobInfo)


	IF NOT EXISTS (SELECT M.JobID FROM ReadingJobs M INNER JOIN #JOB T
	ON M.ProjectID = T.ProjectID AND 
	--CAST(M.JobStartDate AS DATE) = CAST(T.JobStartDate AS DATE))
	M.BasePath=T.BasePath)
	BEGIN
		BEGIN TRY
			BEGIN TRANSACTION
				-- INSERT NEW RECORD TO JOB TABLE
				INSERT INTO [dbo].[ReadingJobs](JobStartDate, JobStatus, ProjectID, BasePath, RunType,Threshold)
				SELECT JobStartDate,JobStatus,ProjectID,BasePath,RunType,Threshold FROM #Job 

				SET @JobID = SCOPE_IDENTITY()

				INSERT INTO [dbo].[ReadTask](JobID, TemplateID, FolderPath, TotalFileCount,RunType)
				SELECT @JobID,
    			JobInfo.value('(TemplateID/text())[1]','int') AS TemplateID,
				JobInfo.value('(FolderPath/text())[1]','varchar(max)') AS FolderPath,
				JobInfo.value('(TotalFileCount/text())[1]','int') AS TotalFileCount	,	
				JobInfo.value('(RunType/text())[1]','varchar(50)') AS RunType		
				FROM @JobXML.nodes('/JobDetails/TaskList/TaskDetails') AS TEMPTABLE(JobInfo)
			COMMIT TRANSACTION
         END TRY
		 BEGIN CATCH
                ROLLBACK TRANSACTION 
				 RAISERROR( 'There was an error while creating the job',16,1)
         END CATCH
	END
	ELSE
	BEGIN
		SET @ErrorMessage = 'Job already exists'
		SELECT @JobID = M.JobID FROM ReadingJobs M INNER JOIN #JOB T
		ON M.ProjectID = T.ProjectID AND 	
		M.BasePath=T.BasePath
		 --RAISERROR( 'Job already exists',16,1)
	END
	
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateJob]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateJob] 
(
@UpdateJobXML XML
)
AS
BEGIN
DECLARE @JobID INT
CREATE TABLE #Job
(
	[JobID] [int] NOT NULL,
	[JobStartDate] [datetime] NULL,
	[JobEndDate] [datetime] NULL,
	[JobStatus] [varchar](50) NULL,
	[ProjectID] [int] NULL,
	[BasePath] [varchar](max) NULL,
	[RunType] [varchar](50) NULL,
	[Threshold] [int] NULL,
)
INSERT INTO #Job(JobID,ProjectID,JobStartDate,JobStatus,BasePath,RunType,Threshold)
	SELECT 
	    UpdateJobXML.value('(JobID/text())[1]','int') AS JobID,
    	UpdateJobXML.value('(ProjectID/text())[1]','int') AS ProjectID,
		GETDATE(),	
		UpdateJobXML.value('(JobStatus/text())[1]','varchar(50)') AS JobStatus,
		UpdateJobXML.value('(BasePath/text())[1]','varchar(max)') AS BasePath,
		UpdateJobXML.value('(Type/text())[1]','varchar(50)') AS RunType,
		UpdateJobXML.value('(Threshold/text())[1]','int') AS Threshold		
	FROM @UpdateJobXML.nodes('/JobDetails') AS TEMPTABLE(UpdateJobXML)

		UPDATE RJ set RJ.JobStartDate=J.JobStartDate,RJ.Threshold=J.Threshold,RJ.RunType=J.RunType,RJ.BasePath=J.BasePath,RJ.ProjectID=J.ProjectID,RJ.JobStatus=J.JobStatus
	FROM ReadingJobs AS RJ,#Job AS J WHERE RJ.JobID=J.JobID


	SELECT @JobID = JobID FROM #Job

	INSERT INTO [dbo].[ReadTask](JobID, TemplateID, FolderPath, TotalFileCount,RunType)
				SELECT @JobID,
    			JobInfo.value('(TemplateID/text())[1]','int') AS TemplateID,
				JobInfo.value('(FolderPath/text())[1]','varchar(max)') AS FolderPath,
				JobInfo.value('(TotalFileCount/text())[1]','int') AS TotalFileCount	,	
				JobInfo.value('(RunType/text())[1]','varchar(50)') AS RunType		
				FROM @UpdateJobXML.nodes('/JobDetails/TaskList/TaskDetails') AS TEMPTABLE(JobInfo)



END
GO
/****** Object:  StoredProcedure [dbo].[UpdateIsDeleteByJobID]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateIsDeleteByJobID] 
(
@JobID INT
)
AS
BEGIN
UPDATE SV SET IsDeleted=1
FROM SheetValues AS SV 
INNER JOIN [dbo].[ReadTask] AS RT ON SV.TaskID=RT.TaskID 
INNER JOIN [dbo].[ReadingJobs] AS RJ ON RJ.JobID=RT.JobID  
WHERE RJ.JobID=@JobID
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateIsDelete]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateIsDelete] 
(
@JobID INT
)
AS
BEGIN
UPDATE SV SET IsDeleted=1
FROM SheetValues AS SV 
INNER JOIN [dbo].[ReadTask] AS RT ON SV.TaskID=RT.TaskID 
INNER JOIN [dbo].[ReadingJobs] AS RJ ON RJ.JobID=RT.JobID  

END
GO
/****** Object:  StoredProcedure [dbo].[SaveFileStatus11]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveFileStatus11] 
(

@TaskID INT, @FileName VARCHAR(200), @FilePath VARCHAR(MAX), @Status INT, @ErrorMessage VARCHAR(MAX), @IsDelete BIT, @RunType VARCHAR(50)

)
AS
BEGIN
	INSERT INTO [dbo].FileStatus(TaskID, [FileName], FilePath, [Status], ErrorMessage,IsDelete,RunType)
	VALUES (@TaskID,@FileName,@FilePath,@Status,@ErrorMessage,@IsDelete,@RunType)

END
GO
/****** Object:  StoredProcedure [dbo].[SaveFileStatus]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveFileStatus] 
(
@FileStatusXML XML
)
AS
BEGIN
	INSERT INTO [dbo].[FileStatus](TaskID, [FileName], FilePath, [Status], ErrorMessage, RunType, IsDelete)
	SELECT
		
		FileStatusInfo.value('(TaskID/text())[1]','int') AS TaskID,
		FileStatusInfo.value('(FileName/text())[1]','varchar(100)') AS [FileName],
		FileStatusInfo.value('(FilePath/text())[1]','varchar(max)') AS FilePath,
		FileStatusInfo.value('(Status/text())[1]','int') AS [Status],
		FileStatusInfo.value('(ErrorMessage/text())[1]','varchar(max)') AS ErrorMessage,
		FileStatusInfo.value('(RunType/text())[1]','Varchar(50)') AS RunType,
		FileStatusInfo.value('(IsDelete/text())[1]','bit') AS IsDelete
		
		
	FROM @FileStatusXML.nodes('/FileStatus') AS TEMPTABLE(FileStatusInfo)
END
GO
/****** Object:  StoredProcedure [dbo].[SaveSheetValues]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveSheetValues] 
(
@SheetName VARCHAR(100), @TaskID INT, @SheetValues VARCHAR(MAX), @ImagePath VARCHAR(MAX),@ExceptionMessage VARCHAR(MAX),@HasError bit
)
AS
BEGIN
	INSERT INTO [dbo].SheetValues(TaskID,SheetName, SheetValues, ImagePath, ExceptionMessage,CreateOn,HasError)
	VALUES(@TaskID,@SheetName,@SheetValues,@ImagePath,@ExceptionMessage,GETDATE(),@HasError)

END
GO
/****** Object:  StoredProcedure [dbo].[GetSheetValues]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetSheetValues] 
(

@ProjectID INT

)
AS
BEGIN
IF @ProjectID IS NOT NULL 
BEGIN

CREATE TABLE #SheetValues (SlNo INT,SheetID INT, SheetName VARCHAR(100), SheetValues VARCHAR(MAX), ImagePath VARCHAR(MAX))

	   INSERT INTO #SheetValues(SlNo,SheetID, SheetName, SheetValues, ImagePath )
       SELECT ROW_NUMBER() OVER( ORDER BY SheetValues.[SheetID] ) , SheetID ,SheetName,SheetValues,ImagePath FROM SheetValues 
       INNER JOIN ReadTask ON SheetValues.TaskID = ReadTask.TaskID
       INNER JOIN ReadingJobs ON ReadTask.JobID = ReadingJobs.JobID
       INNER JOIN ProjectMaster ON ReadingJobs .ProjectID = ProjectMaster.ProjectID WHERE ProjectMaster.ProjectID =@ProjectID
	   AND ISNULL(SheetValues.IsDisplayed,0)=0

	   ---update isdisplayed flag
	   UPDATE SheetValues SET SheetValues.IsDisplayed = 1
	   FROM SheetValues , #SheetValues
	   WHERE SheetValues.SheetID =  #SheetValues.SheetID

	   SELECT SheetName,SheetValues,ImagePath FROM #SheetValues

  END
  ELSE
  BEGIN
  RAISERROR ('ProjectID Is Invalied',16,1)
  END
  END
GO
/****** Object:  StoredProcedure [dbo].[GetReProcessValues]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetReProcessValues] 
(

@JobID INT

)
AS
BEGIN
IF @JobID IS NOT NULL 
BEGIN

--CREATE TABLE #SheetValues (SlNo INT,SheetID INT, SheetName VARCHAR(100), SheetValues VARCHAR(MAX), ImagePath VARCHAR(MAX))

	  
       SELECT ImagePath FROM SheetValues 
       INNER JOIN ReadTask ON SheetValues.TaskID = ReadTask.TaskID
       INNER JOIN ReadingJobs ON ReadTask.JobID =@JobID AND ISNULL(SheetValues.IsDeleted,0)=0
	 
  END
  ELSE
  BEGIN
  RAISERROR ('JobID Is Invalied',16,1)
  END
  END
GO
/****** Object:  StoredProcedure [dbo].[GetRecoverValues]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetRecoverValues] 
(

@JobID INT

)
AS
BEGIN
IF @JobID IS NOT NULL 
BEGIN

--CREATE TABLE #SheetValues (SlNo INT,SheetID INT, SheetName VARCHAR(100), SheetValues VARCHAR(MAX), ImagePath VARCHAR(MAX))

	  
       SELECT ImagePath FROM SheetValues 
       INNER JOIN ReadTask ON SheetValues.TaskID = ReadTask.TaskID
       INNER JOIN ReadingJobs ON ReadTask.JobID =ReadTask.JobID WHERE ReadingJobs.JobID=   @JobID 
	   AND ISNULL(SheetValues.IsDeleted,0) =0
	 
  END
 END
GO
/****** Object:  StoredProcedure [dbo].[GetErrorValues]    Script Date: 08/09/2016 11:19:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetErrorValues] 
(

@JobID INT

)
AS
BEGIN
IF @JobID IS NOT NULL 
BEGIN

--CREATE TABLE #SheetValues (SlNo INT,SheetID INT, SheetName VARCHAR(100), SheetValues VARCHAR(MAX), ImagePath VARCHAR(MAX))

	  
       SELECT ImagePath FROM SheetValues 
       INNER JOIN ReadTask ON SheetValues.TaskID = ReadTask.TaskID
       INNER JOIN ReadingJobs ON ReadTask.JobID =@JobID AND ISNULL(SheetValues.HasError,0)=1
	 
  END
  ELSE
  BEGIN
  RAISERROR ('JobID Is Invalied',16,1)
  END
  END
GO
/****** Object:  ForeignKey [FK_FileStatus_ReadTask]    Script Date: 08/09/2016 11:19:30 ******/
ALTER TABLE [dbo].[FileStatus]  WITH CHECK ADD  CONSTRAINT [FK_FileStatus_ReadTask] FOREIGN KEY([TaskID])
REFERENCES [dbo].[ReadTask] ([TaskID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FileStatus] CHECK CONSTRAINT [FK_FileStatus_ReadTask]
GO
/****** Object:  ForeignKey [FK_ReadingJobs_ProjectMaster]    Script Date: 08/09/2016 11:19:30 ******/
ALTER TABLE [dbo].[ReadingJobs]  WITH CHECK ADD  CONSTRAINT [FK_ReadingJobs_ProjectMaster] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[ProjectMaster] ([ProjectID])
GO
ALTER TABLE [dbo].[ReadingJobs] CHECK CONSTRAINT [FK_ReadingJobs_ProjectMaster]
GO
/****** Object:  ForeignKey [FK_ReadTask_ReadingJobs]    Script Date: 08/09/2016 11:19:30 ******/
ALTER TABLE [dbo].[ReadTask]  WITH CHECK ADD  CONSTRAINT [FK_ReadTask_ReadingJobs] FOREIGN KEY([JobID])
REFERENCES [dbo].[ReadingJobs] ([JobID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ReadTask] CHECK CONSTRAINT [FK_ReadTask_ReadingJobs]
GO
/****** Object:  ForeignKey [FK_ReadTask_TemplateMaster]    Script Date: 08/09/2016 11:19:30 ******/
ALTER TABLE [dbo].[ReadTask]  WITH CHECK ADD  CONSTRAINT [FK_ReadTask_TemplateMaster] FOREIGN KEY([TemplateID])
REFERENCES [dbo].[TemplateMaster] ([TemplateID])
GO
ALTER TABLE [dbo].[ReadTask] CHECK CONSTRAINT [FK_ReadTask_TemplateMaster]
GO
/****** Object:  ForeignKey [FK_SheetValues_ReadTask]    Script Date: 08/09/2016 11:19:30 ******/
ALTER TABLE [dbo].[SheetValues]  WITH CHECK ADD  CONSTRAINT [FK_SheetValues_ReadTask] FOREIGN KEY([TaskID])
REFERENCES [dbo].[ReadTask] ([TaskID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SheetValues] CHECK CONSTRAINT [FK_SheetValues_ReadTask]
GO
/****** Object:  ForeignKey [FK_TemplateFieldMaster_TemplateMaster]    Script Date: 08/09/2016 11:19:30 ******/
ALTER TABLE [dbo].[TemplateFieldMaster]  WITH CHECK ADD  CONSTRAINT [FK_TemplateFieldMaster_TemplateMaster] FOREIGN KEY([TemplateID])
REFERENCES [dbo].[TemplateMaster] ([TemplateID])
GO
ALTER TABLE [dbo].[TemplateFieldMaster] CHECK CONSTRAINT [FK_TemplateFieldMaster_TemplateMaster]
GO
/****** Object:  ForeignKey [FK_TemplateMaster_ProjectMaster]    Script Date: 08/09/2016 11:19:30 ******/
ALTER TABLE [dbo].[TemplateMaster]  WITH CHECK ADD  CONSTRAINT [FK_TemplateMaster_ProjectMaster] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[ProjectMaster] ([ProjectID])
GO
ALTER TABLE [dbo].[TemplateMaster] CHECK CONSTRAINT [FK_TemplateMaster_ProjectMaster]
GO
