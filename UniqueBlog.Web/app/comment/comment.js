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
        registerCommentItemEvent();
        highlightCode();
        scrollToComment();
    });

    function configCommentCKEditor(){
        if (!CKEDITOR.instances['commentEditor']) {
            CKEDITOR.replace('commentEditor', {
                height: 200,
                toolbar:"Simple"
            });
        }
    }

    function highlightCode() {
        $('pre code').each(function (i, block) {
            window.hljs.highlightBlock(block);
        });
    }

    function reloadCommentList() {
        var postId = $("#postId").val();
        $("#postListContainer").load("/PostComment/PostCommentList?postId=" + postId, function () {
            registerCommentItemEvent();
            highlightCode();
        });
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
    }

    function registerCommentItemEvent() {
        $(".post-comment-item a.commentDelete").each(function () {
            var anchor = $(this);
            anchor.off()
            anchor.on("click", function () {
                var commentId = anchor.data("commentid");
                deleteComment(commentId);
            });
        });

        $(".post-comment-item a.commentReply").each(function () {
            var anchor = $(this);
            anchor.on("click", function () {
                var referenceUser = anchor.siblings(".comment-user").text();
                replyComment(referenceUser);
            });
        });

        $(".post-comment-item a.commentReference").each(function () {
            var anchor = $(this);
            anchor.on("click", function () {
                var referenceCommentContent = anchor.parents(".post-comment-item").find(".comment-content").html();
                var referenceUser = anchor.siblings(".comment-user").text();
                referenceComment(referenceCommentContent, referenceUser)
            });
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
                    bootbox.alert("发布评论成功");
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
                            bootbox.alert("删除评论成功");
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

    function referenceComment(commentHtml,referenceUser) {
        var editor = CKEDITOR.instances['commentEditor'];
        var currentContent = editor.getData();
        currentContent += "<p style=\"color:#F19825\">@" + referenceUser + "</p>";
        currentContent += "<blockquote>" + commentHtml + "</blockquote><p></p>";
        editor.setData(currentContent);
        scrollToElement("#formPublishComment");
    }

    function replyComment(referenceUser) {
        var editor = CKEDITOR.instances['commentEditor'];
        var currentContent = editor.getData();
        currentContent += "<p style=\"color:#F19825\">@" + referenceUser + "</p><p></p>";
        editor.setData(currentContent);
        scrollToElement("#formPublishComment");
    }

    function scrollToComment() {
        var hashId = window.location.hash;
        if (hashId && hashId !== "") {
            var id = hashId.substr(1, hashId.length - 1);
            scrollToElement("#postItem" + id);
        }
    }

    function scrollToElement(elementId) {
        var element = $(elementId);
        if (element.length === 1) {
            $("html, body").animate({
                scrollTop: element.offset().top
            }, 1000);
        }
    }
});