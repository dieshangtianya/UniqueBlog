require(["./../config"], function () {
    require(["post.newpost"]);
}),
define(
    "post.newpost", [
		"jquery"
		, "domready"
        , "bootbox"
        , "sidebar/sidebar"
		, "ckeditor"
        , "tag-input"
        , "highlight"
    ], function ($, domready, bootbox, sidebar) {

        var addPredefinedClassTimeOut;

        domready(function () {
            sidebar.initialize();
            configCKEditor();
            configTagInput();
            registerEventHandler();
            window.hljs.initHighlightingOnLoad();
        });

        function configCKEditor() {
            if (!CKEDITOR.instances['postEditor']) {
                CKEDITOR.replace('postEditor', {
                    filebrowserUploadUrl: "/FileManager/PostFileUpload"
                });
            }

            //add custom predefined css class to the iframe
            addPredefinedClass = window.setTimeout(addPredefinedClass, 500);
        }

        function addPredefinedClass()
        {
            var iframe = $("iframe");
            if (iframe[0] != null) {
                var head = iframe.contents().find("head");
                if (head != undefined) {
                    head.append('<link rel="stylesheet" href="/css/predefined-post-extra-style.css" type="text/css" />');
                    window.clearTimeout(addPredefinedClass);
                }
            }
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
            $("#btnSave").on("click", function (e) { savePost(e) });
            $("#btnSaveDraft").on("click", saveAsDraft);
            $("#btnCancel").on("click", cancel);
        }

        function savePost(e) {
            var postData = validateData();
            if (!postData) {
                return;
            }

            $.ajax({
                url: $('#postform').attr('action'),
                type: $('#postform').attr('method'),
                data: JSON.stringify(postData),
                contentType: "application/json;charset=utf-8",
                success: function (jsonResult) {
                    if (jsonResult.Result == true) {
                        window.location.href = '/Home/Index';
                    }else{
                        bootbox.alert(jsonResult.Message);
                    }
                },
                error: function () {

                }
            });
        }

        function saveAsDraft() {

        }

        function cancel() {

        }

        function validateData() {
            var postTitle = $("#PostTitle").val();

            var postContent = CKEDITOR.instances['postEditor'].getData();

            var postPlainContent = CKEDITOR.instances["postEditor"].document.getBody().getText();

            var categories = [];
            //traverse the all categories to choose selected category
            $("#categoryList").find("input[type='checkbox']").each(function () {
                var $checkbox = $(this);
                if ($checkbox[0].checked == true) {
                    var category = {
                        ItemId: $checkbox.attr("categoryId"),
                        ItemName: $checkbox.attr("categoryName"),
                        IsSelected: true
                    }
                    categories.push(category);
                }
            });

            if (postTitle == "") {
                bootbox.alert("博客标题不能为空!");
                return;
            }

            if (postContent == "") {
                bootbox.alert("博客内容不能为空!");
                return;
            }

            if (categories.length == 0) {
                bootbox.alert("请为博客选择所属类别!");
                return;
            }

            var tags = $("#postTag").tagsinput('items');

            var postId = $("#postId").val();

            var post = {
                PostId: postId,
                PostTitle: postTitle,
                PostContent: postContent,
                PostPlainContent: postPlainContent,
                CategoryList: categories,
                PostTags: tags,
            };

            return post;
        }
    });