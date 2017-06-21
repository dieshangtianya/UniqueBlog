use UniqueBlogDB;

DROP PROCEDURE IF EXISTS sp_get_all_categories;

CREATE PROCEDURE sp_get_all_categories
(
BlogId INT
)
BEGIN
	SELECT CategoryId,BlogId,CategoryName,Description,CreatedDate,PostAmount FROM v_category_info A
	WHERE A.BlogId=BlogId;
END
