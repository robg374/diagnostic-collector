IF (OBJECT_ID(N'[dbo].[tbl_Sensor_Fabric]', 'U') IS NULL)
BEGIN
  CREATE TABLE [dbo].[tbl_Sensor_Fabric]
  ([nvc_sensor] [nvarchar](50) NULL
	,[nvc_region] [nvarchar](50) NULL
  ,[nvc_regiontag] [nvarchar](10) NULL
	,[nvc_location] [nvarchar](50) NULL
  ,[nvc_probe] [nvarchar](150) NULL)
END
