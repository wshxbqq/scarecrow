USE [Warden]
GO
/****** Object:  Table [dbo].[tb_Error]    Script Date: 2016/1/7 10:00:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Error](
	[error_id] [bigint] IDENTITY(1,1) NOT NULL,
	[error_task_id] [bigint] NOT NULL,
	[error_img] [nvarchar](max) NOT NULL,
	[error_x] [int] NOT NULL,
	[error_y] [int] NOT NULL,
	[error_time] [datetime] NOT NULL,
 CONSTRAINT [PK_tb_error] PRIMARY KEY CLUSTERED 
(
	[error_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_Tasks]    Script Date: 2016/1/7 10:00:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_Tasks](
	[Task_id] [bigint] IDENTITY(1,1) NOT NULL,
	[Task_url] [nvarchar](max) NOT NULL,
	[Task_width] [int] NOT NULL,
	[Task_height] [int] NOT NULL,
	[Task_type] [nvarchar](100) NOT NULL,
	[Task_before_script] [nvarchar](max) NULL,
	[Task_delay_time] [int] NULL,
	[Task_monitoring_count] [int] NULL,
	[Task_error_count] [int] NULL,
	[Task_enable] [bit] NULL,
	[Task_alert] [bit] NULL,
	[Task_time] [datetime] NOT NULL,
 CONSTRAINT [PK_tb_Tasks] PRIMARY KEY CLUSTERED 
(
	[Task_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[tb_Tasks] ADD  CONSTRAINT [DF_tb_Tasks_Task_delay_time]  DEFAULT ((20000)) FOR [Task_delay_time]
GO
ALTER TABLE [dbo].[tb_Tasks] ADD  CONSTRAINT [DF_tb_Tasks_Task_monitoring_count]  DEFAULT ((0)) FOR [Task_monitoring_count]
GO
ALTER TABLE [dbo].[tb_Tasks] ADD  CONSTRAINT [DF_tb_Tasks_Task_error_count]  DEFAULT ((0)) FOR [Task_error_count]
GO
ALTER TABLE [dbo].[tb_Tasks] ADD  CONSTRAINT [DF_tb_Tasks_Task_enable]  DEFAULT ((0)) FOR [Task_enable]
GO
ALTER TABLE [dbo].[tb_Tasks] ADD  CONSTRAINT [DF_tb_Tasks_Task_alert]  DEFAULT ((1)) FOR [Task_alert]
GO
