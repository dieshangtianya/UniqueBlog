define(["jquery"
], function ($, domready, dotdotdot) {
    function registerPageEvent(callback) {
        var param = $(".pagination").data("param");
        $(".pagination").find("a").each(function () {
            $(this).click(function () {
                var pageNumber = $(this).data("page");
                param.page = pageNumber;
                callback(param);
            });
        });
    }

    return {
        registerPageEvent: registerPageEvent
    };
})