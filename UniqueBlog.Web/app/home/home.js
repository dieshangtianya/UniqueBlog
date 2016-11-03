define([
		"jquery"
		, "domready"
		, "jquery-ui"
], function ($, domready) {

	domready(function () {
		$("#widget-calendar").datepicker();
	});
});