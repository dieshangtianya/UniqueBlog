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

        //change this to load postlist synchronouly
        var loadPostListAsynchronously = false;

        domready(function () {
            sidebar.initialize();

            if (loadPostListAsynchronously) {
                var queryParam = {};
                postList.loadPostList($("#blogPostList"), queryParam);
            } else {
                postList.processPostItems();
            }
        });
    });