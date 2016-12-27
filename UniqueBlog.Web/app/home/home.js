require(["./../config"], function () {
    require(["home.module"]);
}),
define(
    "home.module", [
		"jquery"
        , "domready"
        , "sidebar/sidebar"
    ], function ($, domready, sidebar) {

        domready(function () {
            sidebar.initialize();
        });
    });