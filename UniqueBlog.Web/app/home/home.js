require(["./../config"], function () {
    require(["home.module"]);
}),
define(
    "home.module", [
		"jquery"
        , "domready"
        , "sidebar/sidebar"
        ,"post/post-list"
    ], function ($, domready, sidebar,postList) {

        domready(function () {
            sidebar.initialize();
            postList.loadPostList($("#blogPostList"), 1);
            //postList.processPostItems();
        });
    });