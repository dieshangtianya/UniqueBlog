use UniqueBlogDB;

DROP PROCEDURE IF EXISTS sp_add_blogpost_relation;

CREATE PROCEDURE sp_add_blogpost_relation
(
PostId INT,
RelationData VARCHAR(10000)
)
BEGIN
	DECLARE Amount INT;
    DECLARE LIndex INT DEFAULT 0;
    DECLARE CategoryId INT;
    
	SET Amount=LENGTH(RelationData)-LENGTH(REPLACE(RelationData,',',''));
    
    simpleLoop:LOOP
		SET LIndex=LIndex+1;
        IF LIndex>Amount+1 THEN
			LEAVE simpleLoop;
        END IF;
	
	SET CategoryId = (SELECT func_split_str(RelationData,',',LIndex));
    
    INSERT INTO t_post_category(PostId,CategoryId) Values(PostId,CategoryId);
        
    END LOOP simpleLoop;
    
END