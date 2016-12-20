define([
		"jquery"
		, "domready"
        , "bootbox"
		, "ckeditor"
        , "tag-input"
], function ($, domready,bootbox) {

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
        $("#btnPublish").on("click", function (e) { savePost(e) });
        $("#btnSaveDraft").on("click", saveAsDraft);
        $("#btnCancel").on("click", cancel);
    }

    function savePost(e) {
        var postData=validateData();
        if(!postData){
            return;
        }
        $.ajax({
            url: $('#postform').attr('action'),
            type: $('#postform').attr('method'),
            data: JSON.stringify(postData),
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


    function validateData()
    {
        var postTitle = $("#PostTitle").val();

        var postContent = CKEDITOR.instances['postEditor'].getData()

        var categories = [];
        //traverse the all categories to choose selected category
        $("#categoryList").find("input[type='checkbox']").each(function () {
            var $checkbox = $(this);
            if ($checkbox[0].checked == true) {
                var category = {
                    ItemId:$checkbox.attr("categoryId"),
                    ItemName: $checkbox.attr("categoryName"),
                    IsSelected:true
                }
                categories.push(category);
            }
        });

        if (postTitle == "") {
            bootbox.alert("博客标题不能为空");
            return;
        }

        if (postContent == "") {
            bootbox.alert("博客内容不能为空");
            return;
        }

        if (categories.length == 0) {
            bootbox.alert("请为博客选择所属类别");
            return;
        }

        var tags = $("#postTag").tagsinput('items');

        var post = {
            PostTitle: postTitle,
            PostContent: postContent,
            CategoryList:categories,
            PostTags:tags,
        };

        return post;
    }
});