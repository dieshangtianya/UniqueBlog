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
        var loadPostListAsynchronously = true;

        domready(function () {
            sidebar.initialize();
            loadPostList();
        });

        function loadPostList() {
            var queryParam = {};
            var searchStr = window.location.search;
            if (searchStr !== "") {
                var tempSearchStr = searchStr.substr(1, window.location.search.length);
                var parameters = tempSearchStr.split("&");
                for (var i = 0; i < parameters.length; i++) {
                    if (parameters[i].toLowerCase().startsWith("category=")) {
                        queryParam.category = parameters[i].split("=")[1];
                        break;
                    }
                }
            }
            if (loadPostListAsynchronously) {
                postList.loadPostList($("#blogPostList"), queryParam);
            } else {
                postList.processPostItems();
            }
        }
    });