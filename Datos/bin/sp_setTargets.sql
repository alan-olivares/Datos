GO
CREATE PROCEDURE sp_setTargets
AS
BEGIN
	SET NOCOUNT ON;
	declare  @val nvarchar(500) 
			,@pos int
			,@inicio int
			,@ind int
			,@dato float
	select @val=description from metersconfig where idmetersconfig=5
	SET @pos=0
	SET @inicio=0
	SET @ind=0
	WHILE @pos<LEN(@val)
	BEGIN
		SET @ind=@ind+1
		SELECT @pos=charindex(',',@val,@inicio+1)
		IF @pos=0
			SET @pos=LEN(@val)+1
		SELECT @dato=SUBSTRING(@val,@inicio+1,@pos-@inicio-1)
		SET @inicio=@pos

		IF @ind=1
			UPDATE En_Targets SET Value=@dato WHERE IndicadorId=36
		ELSE IF @ind=2
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=39
		ELSE IF @ind=3
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=40
		ELSE IF @ind=4
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=41
		ELSE IF @ind=5
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=54
		ELSE IF @ind=6
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=55
		ELSE IF @ind=7
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=56
		ELSE IF @ind=8
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=57
		ELSE IF @ind=9
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=58
		ELSE IF @ind=10
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=59
		ELSE IF @ind=11
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=60
		ELSE IF @ind=12
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=61
		ELSE IF @ind=13
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=62
		ELSE IF @ind=14
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=64
		ELSE IF @ind=15
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=65
		ELSE IF @ind=16
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=66
		ELSE IF @ind=17
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=67
		ELSE IF @ind=18
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=68
		ELSE IF @ind=19
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=69
		ELSE IF @ind=20
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=74
		ELSE IF @ind=21
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=75
		ELSE IF @ind=22
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=76
		ELSE IF @ind=23
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=77
		ELSE IF @ind=24
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=78
		ELSE IF @ind=25
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=80
		ELSE IF @ind=26
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=81
		ELSE IF @ind=27
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=82
		ELSE IF @ind=28
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=84
		ELSE IF @ind=29
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=85
		ELSE IF @ind=30
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=86
		ELSE IF @ind=31
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=96
		ELSE IF @ind=32
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=88
		ELSE IF @ind=33
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=89
		ELSE IF @ind=34
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=90
		ELSE IF @ind=35
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=91
		ELSE IF @ind=36
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=92
		ELSE IF @ind=37
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=93
		ELSE IF @ind=38
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=204
		ELSE IF @ind=39
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=205
		ELSE IF @ind=40
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=206
		ELSE IF @ind=41
		UPDATE En_Targets SET Value=@dato WHERE IndicadorId=207
	END
END
GO
