define([
		"jquery"
		, "domready"
		, "ckeditor"
        , "tag-input"
], function ($, domready) {

    domready(function () {
        configCKEditor();
        configTagInput();
        registerEventHandler();
    });

    function configCKEditor() {
        CKEDITOR.replace('postEditor', {
            height: 400,
            language: 'zh-cn'
        });
    }

    function configTagInput() {
        $("#postTag").tagsinput({
            tagClass: function () {
                var random = parseInt(Math.random() * 5) + 1;
                return "label big-tag tag-label-" + random;
            }
        });
    }

    function registerEventHandler() {
        $("#btnPublish").on("click", savePost);
        $("#btnSaveDraft").on("click", saveAsDraft);
        $("#btnCancel").on("click", cancel);
    }

    function savePost() {
        var postTitle = $("#blogTitleId").val();
        var postContent = CKEDITOR.instances.postEditor.getData();
        var categories = [];
        //traverse the all categories to choose selected category
        $("#categoryList").find("input[type='checkbox']").each(function () {
            var $checkbox = $(this);
            if ($checkbox[0].checked == true) {
                categories.push($checkbox.attr("categoryItem"));
            }
        });

        var post = {
            Title: postTitle,
            Content: postContent,
            Categories: categories
        }

        $.ajax({
            url: "/BlogPost/SavePost",
            type: "post",
            data: JSON.stringify(post),
            contentType: "application/json;charset=utf-8",
            success: function () {

            },
            error: function () {

            }
        });
    }

    function saveAsDraft() {

    }

    function cancel() {

    }
});