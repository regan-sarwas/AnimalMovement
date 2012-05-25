USE [Animal_Movement]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 05/25/2012 12:02:01 ******/
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\AMSolomon', N'file_format', N'A')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\AMSolomon', N'filter_projects', N'True')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\AMSolomon', N'project', N'test33')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\AMSolomon', N'species', N'Bear')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\resarwas', N'collar_manufacturer', N'telonics')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\resarwas', N'collar_model', N'AdhocUpload')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\resarwas', N'file_format', N'A')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\resarwas', N'filter_projects', N'False')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\resarwas', N'project', N'LACL_Sheep')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\resarwas', N'species', N'Sheep')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\SDMiller', N'file_format', N'A')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'NPS\SDMiller', N'project', N'test2')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'system', N'sa_email', N'regan_sarwas@nps.gov')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'system', N'upload_folder', N'C:\tmp')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'system', N'upload_timeout', N'60')
INSERT [dbo].[Settings] ([Username], [Key], [Value]) VALUES (N'system', N'version', N'1.0')
/****** Object:  Table [dbo].[ProjectInvestigators]    Script Date: 05/25/2012 12:02:01 ******/
INSERT [dbo].[ProjectInvestigators] ([Login], [Name], [Email], [Phone]) VALUES (N'NPS\BAMangipane', N'Buck Mangipane', N'Buck_Mangipane@nps.gov', N'907-781-2105')
INSERT [dbo].[ProjectInvestigators] ([Login], [Name], [Email], [Phone]) VALUES (N'NPS\JWBurch', N'John Burch', N'John_Burch@nps.gov', N'907-644-3574')
INSERT [dbo].[ProjectInvestigators] ([Login], [Name], [Email], [Phone]) VALUES (N'NPS\KCJoly', N'Kyle Joly', N'Kyle_Joly@nps.gov', N'907-455-0626')
INSERT [dbo].[ProjectInvestigators] ([Login], [Name], [Email], [Phone]) VALUES (N'NPS\RESarwas', N'Regan Sarwas', N'Regan_Sarwas@nps.gov', N'907-644-3548')
INSERT [dbo].[ProjectInvestigators] ([Login], [Name], [Email], [Phone]) VALUES (N'NPS\SDMiller', N'Scott Miller', N'Scott_Miller@nps.gov', N'907-455-0664')
/****** Object:  Table [dbo].[LookupSpecies]    Script Date: 05/25/2012 12:02:01 ******/
INSERT [dbo].[LookupSpecies] ([Species]) VALUES (N'Bear')
INSERT [dbo].[LookupSpecies] ([Species]) VALUES (N'Caribou')
INSERT [dbo].[LookupSpecies] ([Species]) VALUES (N'Moose')
INSERT [dbo].[LookupSpecies] ([Species]) VALUES (N'Muskox')
INSERT [dbo].[LookupSpecies] ([Species]) VALUES (N'Sheep')
INSERT [dbo].[LookupSpecies] ([Species]) VALUES (N'Wolf')
/****** Object:  Table [dbo].[LookupQueryLayerServers]    Script Date: 05/25/2012 12:02:01 ******/
INSERT [dbo].[LookupQueryLayerServers] ([Location], [Connection]) VALUES (N'Regan''s Desktop', N'INPAKRO39088\SQL2008R2')
/****** Object:  Table [dbo].[LookupGender]    Script Date: 05/25/2012 12:02:01 ******/
INSERT [dbo].[LookupGender] ([Sex]) VALUES (N'F')
INSERT [dbo].[LookupGender] ([Sex]) VALUES (N'M')
INSERT [dbo].[LookupGender] ([Sex]) VALUES (N'U')
/****** Object:  Table [dbo].[LookupCollarModels]    Script Date: 05/25/2012 12:02:01 ******/
INSERT [dbo].[LookupCollarModels] ([CollarModel]) VALUES (N'AdhocUpload')
INSERT [dbo].[LookupCollarModels] ([CollarModel]) VALUES (N'ArgosDoppler')
INSERT [dbo].[LookupCollarModels] ([CollarModel]) VALUES (N'ArgosGps')
INSERT [dbo].[LookupCollarModels] ([CollarModel]) VALUES (N'TelonicsGen3')
INSERT [dbo].[LookupCollarModels] ([CollarModel]) VALUES (N'TelonicsGen4')
INSERT [dbo].[LookupCollarModels] ([CollarModel]) VALUES (N'TelonicsGpsArgos')
INSERT [dbo].[LookupCollarModels] ([CollarModel]) VALUES (N'Unknown')
/****** Object:  Table [dbo].[LookupCollarManufacturers]    Script Date: 05/25/2012 12:02:01 ******/
INSERT [dbo].[LookupCollarManufacturers] ([CollarManufacturer], [Name], [Website], [Description]) VALUES (N'joe', N'Joe''s Animal Collars', NULL, NULL)
INSERT [dbo].[LookupCollarManufacturers] ([CollarManufacturer], [Name], [Website], [Description]) VALUES (N'Telonics', N'Telonics, Inc.', N'http://www.telonics.com', N'932 E. Impala Avenue Mesa, AZ, 85204-6699 USA Tel: 480-892-4444 FAX: 480-892-9139')
/****** Object:  Table [dbo].[LookupCollarFileStatus]    Script Date: 05/25/2012 12:02:01 ******/
INSERT [dbo].[LookupCollarFileStatus] ([Code], [Name], [Description]) VALUES (N'A', N'Active', N'File is archived in the database and used to create movement vectors')
INSERT [dbo].[LookupCollarFileStatus] ([Code], [Name], [Description]) VALUES (N'I', N'Inactive', N'File is archived in the database, but not used in calculating movement vectors')
/****** Object:  Table [dbo].[LookupCollarFileFormats]    Script Date: 05/25/2012 12:02:01 ******/
INSERT [dbo].[LookupCollarFileFormats] ([Code], [CollarManufacturer], [Name], [Description], [TableName], [HasCollarIdColumn]) VALUES (N'A', N'Telonics', N'Telonics Store On Board', NULL, N'CollarDataTelonicsStoreOnBoard', N'N')
INSERT [dbo].[LookupCollarFileFormats] ([Code], [CollarManufacturer], [Name], [Description], [TableName], [HasCollarIdColumn]) VALUES (N'B', N'Telonics', N'Ed Debevek''s File Format', NULL, N'CollarDataDebevekFormat', N'Y')
INSERT [dbo].[LookupCollarFileFormats] ([Code], [CollarManufacturer], [Name], [Description], [TableName], [HasCollarIdColumn]) VALUES (N'C', N'Telonics', N'Telonics Gen4 Condensed Output', N'This is the output file from the Telonics convertor', N'CollarDataTelonicsGen4Condensed', N'N')
/****** Object:  Table [dbo].[LookupCollarFileHeaders]    Script Date: 05/25/2012 12:02:01 ******/
INSERT [dbo].[LookupCollarFileHeaders] ([Header], [FileFormat]) VALUES (N'CollarID,MooseNo,Location,FixDate,FixTime,FixMonth,FixDay,FixYear,LatWGS84,LonWGS84,Temperature', N'B')
INSERT [dbo].[LookupCollarFileHeaders] ([Header], [FileFormat]) VALUES (N'CollarID,WolfNo,Pack,FixDate,FixTime,FixMonth,FixDay,FixYear,LatWGS84,LonWGS84', N'B')
INSERT [dbo].[LookupCollarFileHeaders] ([Header], [FileFormat]) VALUES (N'Fix #,Date,Time,Fix Status,Status Text,Velocity East(m/s),Velocity North(m/s),Velocity Up(m/s),Latitude,Longitude,Altitude(m),PDOP,HDOP,VDOP,TDOP,Temperature Sensor(deg.),Activity Sensor,Satellite Data,', N'A')
INSERT [dbo].[LookupCollarFileHeaders] ([Header], [FileFormat]) VALUES (N'Telonics Data Report', N'C')
