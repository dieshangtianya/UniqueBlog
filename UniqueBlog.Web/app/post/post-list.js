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
                   $container.html(data);
                   pageNavigation.registerPageEvent(loadPostList, $container);
                   setTimeout(function () {
                       $(".post-item-brief").dotdotdot({ watch: "window" });
                       $(".post-item-brief").addClass("post-item-visible");
                   }, 200)
               });
    }

    return {
        loadPostList: loadPostList
    }
})