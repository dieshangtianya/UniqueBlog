require(["./../config"], function () {
    require(["comment.publish"]);
}),
define("comment.publish", [
    "jquery"
	, "domready"
	, "ckeditor"
    , "highlight"
], function ($,domready) {
    domready(function () {
        configCommentCKEditor();
    })

    function configCommentCKEditor(){
        if (!CKEDITOR.instances['commentEditor']) {
            CKEDITOR.replace('commentEditor', {
                height: 200,
                toolbar:"Simple"
            });
        }
    }
});