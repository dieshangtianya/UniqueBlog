define(["jquery"
		, "domready"
        , "jquery-dotdotdot"
        , "pagination/paging-navigation"
], function ($, domready, dotdotdot, pageNavigation) {

    var timeoutRef;

    function loadPostList($container, queryParam) {
        if (!queryParam.page) {
            queryParam.page = 1;
        }

        $container.load("../BlogPost/PostList", queryParam, function () {
            pageNavigation.registerPageEvent(function (param) {
                loadPostList($container, param);
            });

            $container.hide();
            timeoutRef = setTimeout(function () {
                $container.show();
                processPostItems();
                window.clearTimeout(timeoutRef);
            }, 200)
        });
    }

    function processPostItems() {
        $(".post-item-brief").dotdotdot({ watch: "window" });
        $(".post-item-brief").addClass("post-item-visible");
    }

    return {
        loadPostList: loadPostList,
        processPostItems: processPostItems
    }
})