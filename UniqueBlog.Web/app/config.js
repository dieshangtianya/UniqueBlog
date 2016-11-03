﻿requirejs.config({
	baseUrl: '/app',
	paths: {
		'jquery':'../vendor/jquery/2.1.4/jquery',
		'lodash': '../vendor/lodash/4.6.1/lodash.min',
		'bootstrap': '../vendor/bootstrap/3.3.5/js/bootstrap.min',
		'domready': '../vendor/requirejs-domready/domReady',
		'jqueryValidate': "../vendor/jquery-validate/1.15.0/jquery.validate.min",
		'jqueryValidateUnobtrusive': "../vendor/jquery-unobtrusive/jquery.validate.unobtrusive",
		'jquery-ui': '../vendor/jquery-ui/1.12.1/jquery-ui.min'
		//other javascript dependencies and custom module which used in other modules 
	},
	shim: {
		'bootstrap': {
			deps: ["jquery"]
		},
		'jqueryValidate': {
			deps: ["jquery"]
		},
		'jqueryValidateUnobtrusive': {
			deps: [
				"jquery",
				"jqueryValidate"
			]
		},
		'jquery-ui': {
			exports: "$",
			deps: ["jquery"]
		}
	}
});

//Libraries and modules that will be needed on all pages of the site
//require(['jquery', 'lodash', 'bootstrap'], function () {

//});