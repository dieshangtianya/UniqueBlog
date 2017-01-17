define(["jquery"
], function ($, domready, dotdotdot) {
    function registerPageEvent(callback) {
        $(".pagination").find("a").each(function () {
            $(this).click(function () {
                var pageNumber = $(this).data("page");
                callback(pageNumber);
            });
        });
    }

    return {
        registerPageEvent: registerPageEvent
    };
})