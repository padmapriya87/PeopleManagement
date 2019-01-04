
CREATE PROCEDURE [dbo].[uspPersonUpsert]
@person_id int =0,
@first_name VARCHAR(50)='',
@last_name VARCHAR(50)='',
@state_id int,
@gender CHAR(1) ='',
@dob DATETIME
AS   
BEGIN
	IF @person_id =0
	BEGIN
		INSERT INTO [dbo].[person]
			   ([first_name]
			   ,[last_name]
			   ,[state_id]
			   ,[gender]
			   ,[dob])
		 VALUES
			   (@first_name
			   ,@last_name
			   ,@state_id
			   ,@gender
			   ,@dob)
	END
	ELSE
	BEGIN
	 UPDATE [dbo].[person]
		SET first_name = @first_name,
			last_name = @last_name,
			gender = @gender,
			state_id = @state_id,
			dob = @dob
		WHERE person_id = @person_id
	END
END
GO


