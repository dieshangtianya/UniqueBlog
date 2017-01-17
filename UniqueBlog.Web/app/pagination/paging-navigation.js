define(["jquery"
], function ($, domready, dotdotdot) {
    function registerPageEvent(callback, $container) {
        $(".pagination").find("a").each(function () {
            $(this).click(function () {
                var pageNumber = $(this).data("page");
                callback($container, pageNumber);
            });
        });
    }

    return {
        registerPageEvent: registerPageEvent
    };
})