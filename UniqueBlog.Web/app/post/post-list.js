define(["jquery"
		, "domready"
        , "jquery-dotdotdot"
], function ($, domready, dotdotdot) {
    domready(function () {
        $(".post-item-brief").dotdotdot({ watch: "window" });

        $(".post-item-brief").addClass("post-item-visible");
    })
})