require(["./../config"], function () {
    require(["post.module"]);
}),
define(
    "post.module", [
		"jquery"
        , "domready"
        , "sidebar/sidebar"
        , "highlight"
    ], function ($, domready, sidebar) {
       
        domready(function () {
            sidebar.initialize();
            $('pre code').each(function (i, block) {
                window.hljs.highlightBlock(block);
            });
        });
    });