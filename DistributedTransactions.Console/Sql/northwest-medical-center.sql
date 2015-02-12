USE [master]

GO

IF NOT EXISTS
       (SELECT NAME
        FROM   sys.databases
        WHERE  NAME = N'NorthwestMedicalCenter') BEGIN
      CREATE DATABASE [NorthwestMedicalCenter]
  END

GO

USE [NorthwestMedicalCenter]

GO

IF NOT EXISTS
       (SELECT *
        FROM   sys.objects
        WHERE  object_id = Object_id(N'[dbo].[Cases]') AND
               type IN (N'U')) BEGIN
      CREATE TABLE [dbo].[Cases]
        ([CaseId]          [INT] IDENTITY(1, 1) NOT NULL,
         [PatientId]       [INT] NOT NULL,
         [CaseDescription] [VARCHAR](1000) NOT NULL,
         CONSTRAINT [PK_Cases] PRIMARY KEY CLUSTERED ( [CaseId] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
      ON [PRIMARY]
  END

GO

IF NOT EXISTS
       (SELECT *
        FROM   sys.objects
        WHERE  object_id = Object_id(N'[dbo].[Conditions]') AND
               type IN (N'U')) BEGIN
      CREATE TABLE [dbo].[Conditions]
        ([CaseId]    [INT] NOT NULL,
         [ConditionName] [VARCHAR](500) NOT NULL)
      ON [PRIMARY]
  END

GO

IF NOT EXISTS
       (SELECT *
        FROM   sys.objects
        WHERE  object_id = Object_id(N'[dbo].[Patients]') AND
               type IN (N'U')) BEGIN
      CREATE TABLE [dbo].[Patients]
        ([PatientId] [INT] IDENTITY(1, 1) NOT NULL,
         [FirstName] [VARCHAR](50) NOT NULL,
         [LastName]  [VARCHAR](50) NOT NULL,
         [City]      [VARCHAR](50) NOT NULL,
         [State]     [VARCHAR](50) NOT NULL,
         [ZipCode]   [VARCHAR](50) NOT NULL,
         CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED ( [PatientId] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY])
      ON [PRIMARY]
  END

GO

IF NOT EXISTS
       (SELECT *
        FROM   sys.foreign_keys
        WHERE  object_id = Object_id(N'[dbo].[FK_Cases_Patients]') AND
               parent_object_id = Object_id(N'[dbo].[Cases]'))
  ALTER TABLE [dbo].[Cases]
    WITH CHECK ADD CONSTRAINT [FK_Cases_Patients] FOREIGN KEY([PatientId]) REFERENCES [dbo].[Patients] ([PatientId])

GO

IF EXISTS
   (SELECT *
    FROM   sys.foreign_keys
    WHERE  object_id = Object_id(N'[dbo].[FK_Cases_Patients]') AND
           parent_object_id = Object_id(N'[dbo].[Cases]'))
  ALTER TABLE [dbo].[Cases]
    CHECK CONSTRAINT [FK_Cases_Patients]

GO

IF NOT EXISTS
       (SELECT *
        FROM   sys.foreign_keys
        WHERE  object_id = Object_id(N'[dbo].[FK_Conditions_Cases]') AND
               parent_object_id = Object_id(N'[dbo].[Conditions]'))
  ALTER TABLE [dbo].[Conditions]
    WITH CHECK ADD CONSTRAINT [FK_Conditions_Cases] FOREIGN KEY([CaseId]) REFERENCES [dbo].[Cases] ([CaseId])

GO

IF EXISTS
   (SELECT *
    FROM   sys.foreign_keys
    WHERE  object_id = Object_id(N'[dbo].[FK_Conditions_Cases]') AND
           parent_object_id = Object_id(N'[dbo].[Conditions]'))
  ALTER TABLE [dbo].[Conditions]
    CHECK CONSTRAINT [FK_Conditions_Cases]

GO 