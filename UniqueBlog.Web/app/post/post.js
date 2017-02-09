require(["./../config"], function () {
    require(["post.module"]);
}),
define(
    "post.module", [
		"jquery"
        , "domready"
        , "sidebar/sidebar"
        , "comment/comment"
        , "highlight"
    ], function ($, domready, sidebar,comment) {
       
        domready(function () {
            sidebar.initialize();
            $('pre code').each(function (i, block) {
                window.hljs.highlightBlock(block);
            });
        });
    });