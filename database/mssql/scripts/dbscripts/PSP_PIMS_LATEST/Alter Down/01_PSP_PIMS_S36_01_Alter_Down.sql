-- Script generated by Aqua Data Studio Schema Synchronization for MS SQL Server 2016 on Fri Sep 02 13:05:11 PDT 2022
-- Execute this script on:
-- 		PSP_PIMS_S36_01/dbo - This database/schema will be modified
-- to synchronize it with MS SQL Server 2016:
-- 		PSP_PIMS_S36_00/dbo

-- We recommend backing up the database prior to executing the script.

SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
GO
BEGIN TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop trigger dbo.PIMS_AIPAFL_I_S_U_TR
PRINT N'Drop trigger dbo.PIMS_AIPAFL_I_S_U_TR'
GO
DROP TRIGGER IF EXISTS [dbo].[PIMS_AIPAFL_I_S_U_TR]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop trigger dbo.PIMS_AIPAFL_I_S_I_TR
PRINT N'Drop trigger dbo.PIMS_AIPAFL_I_S_I_TR'
GO
DROP TRIGGER IF EXISTS [dbo].[PIMS_AIPAFL_I_S_I_TR]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop trigger dbo.PIMS_AIPRFL_I_S_U_TR
PRINT N'Drop trigger dbo.PIMS_AIPRFL_I_S_U_TR'
GO
DROP TRIGGER IF EXISTS [dbo].[PIMS_AIPRFL_I_S_U_TR]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop trigger dbo.PIMS_AIPRFL_I_S_I_TR
PRINT N'Drop trigger dbo.PIMS_AIPRFL_I_S_I_TR'
GO
DROP TRIGGER IF EXISTS [dbo].[PIMS_AIPRFL_I_S_I_TR]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop trigger dbo.PIMS_AIPRFL_A_S_IUD_TR
PRINT N'Drop trigger dbo.PIMS_AIPRFL_A_S_IUD_TR'
GO
DROP TRIGGER IF EXISTS [dbo].[PIMS_AIPRFL_A_S_IUD_TR]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop foreign key constraint dbo.PIM_ACTINS_PIM_AIPRFL_FK
PRINT N'Drop foreign key constraint dbo.PIM_ACTINS_PIM_AIPRFL_FK'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE]
	DROP CONSTRAINT IF EXISTS [PIM_ACTINS_PIM_AIPRFL_FK]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop foreign key constraint dbo.PIM_PRSCRC_PIM_AIPRFL_FK
PRINT N'Drop foreign key constraint dbo.PIM_PRSCRC_PIM_AIPRFL_FK'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE]
	DROP CONSTRAINT IF EXISTS [PIM_PRSCRC_PIM_AIPRFL_FK]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop foreign key constraint dbo.PIM_PRACQF_PIM_AIPAFL_FK
PRINT N'Drop foreign key constraint dbo.PIM_PRACQF_PIM_AIPAFL_FK'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE]
	DROP CONSTRAINT IF EXISTS [PIM_PRACQF_PIM_AIPAFL_FK]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop foreign key constraint dbo.PIM_ACTINS_PIM_AIPAFL_FK
PRINT N'Drop foreign key constraint dbo.PIM_ACTINS_PIM_AIPAFL_FK'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE]
	DROP CONSTRAINT IF EXISTS [PIM_ACTINS_PIM_AIPAFL_FK]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop index dbo.AIPRFL_PROPERTY_RESEARCH_FILE_ID_IDX
PRINT N'Drop index dbo.AIPRFL_PROPERTY_RESEARCH_FILE_ID_IDX'
GO
DROP INDEX IF EXISTS [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE].[AIPRFL_PROPERTY_RESEARCH_FILE_ID_IDX]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop index dbo.AIPRFL_ACTIVITY_INSTANCE_ID_IDX
PRINT N'Drop index dbo.AIPRFL_ACTIVITY_INSTANCE_ID_IDX'
GO
DROP INDEX IF EXISTS [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE].[AIPRFL_ACTIVITY_INSTANCE_ID_IDX]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop trigger dbo.PIMS_AIPAFL_A_S_IUD_TR
PRINT N'Drop trigger dbo.PIMS_AIPAFL_A_S_IUD_TR'
GO
DROP TRIGGER IF EXISTS [dbo].[PIMS_AIPAFL_A_S_IUD_TR]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop index dbo.AIPAFL_PROPERTY_ACQUISITION_FILE_ID_IDX
PRINT N'Drop index dbo.AIPAFL_PROPERTY_ACQUISITION_FILE_ID_IDX'
GO
DROP INDEX IF EXISTS [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE].[AIPAFL_PROPERTY_ACQUISITION_FILE_ID_IDX]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop index dbo.AIPAFL_ACTIVITY_INSTANCE_ID_IDX
PRINT N'Drop index dbo.AIPAFL_ACTIVITY_INSTANCE_ID_IDX'
GO
DROP INDEX IF EXISTS [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE].[AIPAFL_ACTIVITY_INSTANCE_ID_IDX]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop unique constraint dbo.PIMS_AIPRFL_H_UK
PRINT N'Drop unique constraint dbo.PIMS_AIPRFL_H_UK'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE_HIST]
	DROP CONSTRAINT IF EXISTS [PIMS_AIPRFL_H_UK]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop primary key constraint dbo.PIMS_AIPRFL_H_PK
PRINT N'Drop primary key constraint dbo.PIMS_AIPRFL_H_PK'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE_HIST]
	DROP CONSTRAINT IF EXISTS [PIMS_AIPRFL_H_PK]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop unique constraint dbo.AIPRFL_ACT_INST_PROP_RSRCH_FL_TUC
PRINT N'Drop unique constraint dbo.AIPRFL_ACT_INST_PROP_RSRCH_FL_TUC'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE]
	DROP CONSTRAINT IF EXISTS [AIPRFL_ACT_INST_PROP_RSRCH_FL_TUC]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop primary key constraint dbo.AIPRFL_PK
PRINT N'Drop primary key constraint dbo.AIPRFL_PK'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE]
	DROP CONSTRAINT IF EXISTS [AIPRFL_PK]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop table dbo.PIMS_ACT_INST_PROP_RSRCH_FILE
PRINT N'Drop table dbo.PIMS_ACT_INST_PROP_RSRCH_FILE'
GO
DROP TABLE IF EXISTS [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop unique constraint dbo.AIPAFL_ACT_INST_PROP_ACQ_FL_TUC
PRINT N'Drop unique constraint dbo.AIPAFL_ACT_INST_PROP_ACQ_FL_TUC'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE]
	DROP CONSTRAINT IF EXISTS [AIPAFL_ACT_INST_PROP_ACQ_FL_TUC]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop primary key constraint dbo.AIPAFL_PK
PRINT N'Drop primary key constraint dbo.AIPAFL_PK'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE]
	DROP CONSTRAINT IF EXISTS [AIPAFL_PK]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop table dbo.PIMS_ACT_INST_PROP_ACQ_FILE
PRINT N'Drop table dbo.PIMS_ACT_INST_PROP_ACQ_FILE'
GO
DROP TABLE IF EXISTS [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop unique constraint dbo.PIMS_AIPAFL_H_UK
PRINT N'Drop unique constraint dbo.PIMS_AIPAFL_H_UK'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE_HIST]
	DROP CONSTRAINT IF EXISTS [PIMS_AIPAFL_H_UK]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop primary key constraint dbo.PIMS_AIPAFL_H_PK
PRINT N'Drop primary key constraint dbo.PIMS_AIPAFL_H_PK'
GO
ALTER TABLE [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE_HIST]
	DROP CONSTRAINT IF EXISTS [PIMS_AIPAFL_H_PK]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop table dbo.PIMS_ACT_INST_PROP_RSRCH_FILE_HIST
PRINT N'Drop table dbo.PIMS_ACT_INST_PROP_RSRCH_FILE_HIST'
GO
DROP TABLE IF EXISTS [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE_HIST]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop sequence dbo.PIMS_ACT_INST_PROP_RSRCH_FILE_H_ID_SEQ
PRINT N'Drop sequence dbo.PIMS_ACT_INST_PROP_RSRCH_FILE_H_ID_SEQ'
GO
DROP SEQUENCE IF EXISTS [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE_H_ID_SEQ]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop sequence dbo.PIMS_ACT_INST_PROP_RSRCH_FILE_ID_SEQ
PRINT N'Drop sequence dbo.PIMS_ACT_INST_PROP_RSRCH_FILE_ID_SEQ'
GO
DROP SEQUENCE IF EXISTS [dbo].[PIMS_ACT_INST_PROP_RSRCH_FILE_ID_SEQ]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop sequence dbo.PIMS_ACT_INST_PROP_ACQ_FILE_ID_SEQ
PRINT N'Drop sequence dbo.PIMS_ACT_INST_PROP_ACQ_FILE_ID_SEQ'
GO
DROP SEQUENCE IF EXISTS [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE_ID_SEQ]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop table dbo.PIMS_ACT_INST_PROP_ACQ_FILE_HIST
PRINT N'Drop table dbo.PIMS_ACT_INST_PROP_ACQ_FILE_HIST'
GO
DROP TABLE IF EXISTS [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE_HIST]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

-- Drop sequence dbo.PIMS_ACT_INST_PROP_ACQ_FILE_H_ID_SEQ
PRINT N'Drop sequence dbo.PIMS_ACT_INST_PROP_ACQ_FILE_H_ID_SEQ'
GO
DROP SEQUENCE IF EXISTS [dbo].[PIMS_ACT_INST_PROP_ACQ_FILE_H_ID_SEQ]
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO

COMMIT TRANSACTION
GO
IF @@ERROR <> 0 SET NOEXEC ON
GO
DECLARE @Success AS BIT
SET @Success = 1
SET NOEXEC OFF
IF (@Success = 1) PRINT 'The database update succeeded'
ELSE BEGIN
   IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION
   PRINT 'The database update failed'
END
GO