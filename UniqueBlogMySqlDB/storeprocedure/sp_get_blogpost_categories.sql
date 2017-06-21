use UniqueBlogDB;

DROP PROCEDURE IF EXISTS sp_get_blogpost_categories;

CREATE PROCEDURE sp_get_blogpost_categories
(
postId INT
)
BEGIN
	SELECT CategoryId,BlogId,CategoryName,Description,CreatedDate,PostAmount FROM v_category_info A
		WHERE EXISTS (SELECT CategoryId FROM t_post_category B WHERE B.PostId=postId and B.CategoryId=A.CategoryId);
END