define([
		"jquery"
		, "jquery-ui"
], function ($) {
    return {
        initialize: function () {
            $("#widget-calendar").datepicker();
        }
    }
});