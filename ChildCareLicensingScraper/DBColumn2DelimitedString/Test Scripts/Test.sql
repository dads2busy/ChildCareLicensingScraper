﻿-- Examples for queries that exercise different SQL objects implemented by this assembly

-----------------------------------------------------------------------------------------
-- Stored procedure
-----------------------------------------------------------------------------------------
-- exec StoredProcedureName


-----------------------------------------------------------------------------------------
-- User defined function
-----------------------------------------------------------------------------------------
-- select dbo.FunctionName()


-----------------------------------------------------------------------------------------
-- User defined type
-----------------------------------------------------------------------------------------
-- CREATE TABLE test_table (col1 UserType)
--
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 1'))
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 2'))
-- INSERT INTO test_table VALUES (convert(uri, 'Instantiation String 3'))
--
-- select col1::method1() from test_table



-----------------------------------------------------------------------------------------
-- User defined type
-----------------------------------------------------------------------------------------
-- select dbo.AggregateName(Column1) from Table1


SELECT top 10
	     [InspectionID]
      ,[FacilityLicID]	  
	  ,page
      ,[HANDS_VDSS_CHILDCARE].[dbo].[DBStringOccurenceCount]('22VAC', [Page]) Cnt
  FROM [HANDS_VDSS_CHILDCARE].[dbo].[FACILITY_INSPECTIONS]
