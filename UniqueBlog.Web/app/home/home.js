require(["./../config"], function () {
    require(["home.module"]);
}),
define(
    "home.module", [
		"jquery"
        , "domready"
        , "sidebar/sidebar"
        ,"post/post-list"
    ], function ($, domready, sidebar) {

        domready(function () {
            sidebar.initialize();
        });
    });