define([
		"jquery"
		,"domready"
		,"ckeditor"
], function ($, domready) {

	domready(function () {
		CKEDITOR.replace('post-textarea');
		configCKEditor();
	});

	function configCKEditor()
	{
		CKEDITOR.config.height = 400;
	}
});