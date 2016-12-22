use UniqueBlogDB;

IF EXISTS (SELECT * 
			FROM sys.types
			WHERE name='udt_post_category_relationship' AND is_table_type='1')
DROP type udt_post_category_relationship
GO

create type udt_post_category_relationship as table
(
	PostId int,
	CategoryId int
)

GO