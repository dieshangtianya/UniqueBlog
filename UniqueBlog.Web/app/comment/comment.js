require(["./../config"], function () {
    require(["comment.publish"]);
}),
define("comment.publish", [
    "jquery"
	, "domready"
    , "bootbox"
	, "ckeditor"
    , "highlight"
], function ($,domready,bootbox) {
    domready(function () {
        configCommentCKEditor();
        registerCommentEvent();
    })

    function configCommentCKEditor(){
        if (!CKEDITOR.instances['commentEditor']) {
            CKEDITOR.replace('commentEditor', {
                height: 200,
                toolbar:"Simple"
            });
        }
    }

    function reloadCommentList() {
        var postId = $("#postId").val();
        $("#postListContainer").load("/PostComment/PostCommentList?postId=" + postId);
    }

    function clearEditor() {
        CKEDITOR.instances['commentEditor'].setData("");
        var nickName = $("#txtNickName").val("");
    }

    function registerCommentEvent() {
        $("#btnPublishComment").on("click", function () {
            publishComment();
        });

        $("#btnClearComment").on("click", function () {
            clearEditor();
        });

        $(".post-comment-item a.commentDelete").each(function () {
            var anchor = $(this);
            anchor.on("click", function () {
                var commentId = anchor.data("commentid");
                deleteComment(commentId);
            });
        });

        $(".post-comment-item a.commentReply").each(function () {
            var anchor = $(this);
        });

        $(".post-comment-item a.commentReference").each(function () {
            var anchor = $(this);
        });
    }

    function publishComment() {
        var nickName = $("#txtNickName").val();
        var comment = CKEDITOR.instances['commentEditor'].getData();
        var postId = $("#postId").val();

        if (nickName == "") {
            bootbox.alert("请输入昵称!");
            return;
        }

        if (comment == "") {
            bootbox.alert("请输入评论内容");
            return;
        }

        var commentData = {
            nickName: nickName,
            comment: comment,
            postId:postId
        };

        $.ajax({
            url: $('#formPublishComment').attr('action'),
            type: $('#formPublishComment').attr('method'),
            data: JSON.stringify(commentData),
            contentType: "application/json;charset=utf-8",
            success: function (jsonResult) {
                if (jsonResult.Result == true) {
                    reloadCommentList();
                    clearEditor();
                } else {
                    bootbox.alert(jsonResult.Message);
                }
            },
            error: function () {
                bootbox.alert("发生错误");
            }
        });
    }

    function deleteComment(commentId) {
        bootbox.confirm("确认要删除该评论吗?", function (result) {
            if (result == true) {
                $.ajax({
                    url: "/PostComment/DeleteComment",
                    type: "post",
                    data: JSON.stringify({commentId:commentId}),
                    contentType: "application/json;charset=utf-8",
                    success: function (jsonResult) {
                        if (jsonResult.Result == true) {
                            reloadCommentList();
                        } else {
                            bootbox.alert(jsonResult.Message);
                        }
                    },
                    error: function () {
                        bootbox.alert("发生错误");
                    }
                });
            }
        });
    }
});