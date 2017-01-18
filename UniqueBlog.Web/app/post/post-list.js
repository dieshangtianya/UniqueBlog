define(["jquery"
		, "domready"
        , "jquery-dotdotdot"
        , "pagination/paging-navigation"
], function ($, domready, dotdotdot, pageNavigation) {

    var timeoutRef;

    function loadPostList($container, page) {
        $container.load("../BlogPost/PostList", { Page: page }, function () {
            pageNavigation.registerPageEvent(function (page) {
                loadPostList($container, page);
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
        //uncomment this sentence if change to synchronously load post list
        //pageNavigation.registerPageEvent(gotoPage);
    }

    function gotoPage(page) {
        window.location.href = "../?page=" + page;
    }

    return {
        loadPostList: loadPostList,
        processPostItems: processPostItems
    }
})