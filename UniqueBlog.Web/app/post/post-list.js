define(["jquery"
		, "domready"
        , "jquery-dotdotdot"
        , "pagination/paging-navigation"
], function ($, domready, dotdotdot, pageNavigation) {

    function loadPostList($container, page) {
        $.get(
               "/BlogPost/PostList",
               {
                   Page: page
               },
               function (data, status) {
                   var postList = $(data);
                   $container.empty().append(postList);
                   pageNavigation.registerPageEvent(function (page) {
                       loadPostList($container, page);
                   });
                   setTimeout(function () {
                       processPostItems();
                   }, 200)
               });
    }

    function processPostItems() {
        $(".post-item-brief").dotdotdot({ watch: "window" });
        $(".post-item-brief").addClass("post-item-visible");

        pageNavigation.registerPageEvent(gotoPage);
    }

    function gotoPage(page) {
        window.location.href = "/?page=" + page;
    }

    return {
        loadPostList: loadPostList,
        processPostItems:processPostItems
    }
})