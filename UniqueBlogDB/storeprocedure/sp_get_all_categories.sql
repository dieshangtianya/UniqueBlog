use UniqueBlogDB;

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_get_all_categories' AND xtype='p')
DROP PROCEDURE sp_get_all_categories
GO

CREATE PROCEDURE sp_get_all_categories
(
@BlogId INT
)
AS
BEGIN
	SELECT CategoryId,BlogId,CategoryName,Description,CreatedDate,PostAmount FROM v_category_info A
	WHERE A.BlogId=@BlogId
END
