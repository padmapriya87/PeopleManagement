

CREATE PROCEDURE [dbo].[uspPersonSearch]     
	@first_name VARCHAR(50)='',
@last_name VARCHAR(50)='',
@state_id int,
@gender VARCHAR(1) ='',
@dob VARCHAR(100) = ''
AS   
BEGIN
    SET NOCOUNT ON; 
	
	DECLARE @state_code varchar(2) = ''
	IF @state_id !=0
	BEGIN
		SELECT @state_code = state_code FROM dbo.States
		WHERE state_id = @state_id
	END
    SELECT * 
    FROM dbo.person  P
	JOIN dbo.states S
	ON p.state_Id = S.state_id
    WHERE P.First_Name like '%'+@first_name+'%' AND P.Last_Name like '%'+ @last_name  +'%'
    AND P.gender like '%'+@gender+'%' 
	AND  convert(nvarchar(MAX), P.DOB, 101) like  '%' + @dob +'%'
	AND S.state_code like '%'+@state_code + '%';  
END  
GO


