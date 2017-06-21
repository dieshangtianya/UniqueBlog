/*****************************************
**create sql script about category view
*****************************************/

USE UniqueBlogDB;

DROP VIEW IF EXISTS v_category_info;

CREATE VIEW v_category_info
AS
	SELECT B.*,(CASE WHEN(C.PostAmount>0) then C.PostAmount ELSE 0 END) AS PostAmount   
	FROM 
		t_category B 
	LEFT JOIN 
		(SELECT B.CategoryId, COUNT(*) AS PostAmount FROM t_post_category B GROUP BY(B.CategoryId))C 
	ON B.CategoryId=C.CategoryId;