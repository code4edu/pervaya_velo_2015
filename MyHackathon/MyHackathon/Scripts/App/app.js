/// <reference path="../angular.js" />
/// <reference path="../angular/angular-ui-router.js" />
var App = angular
    .module('App', ['ui.router', 'ngAnimate']);

App
    .config(function ($stateProvider, $urlRouterProvider) {
    	//
    	// For any unmatched url, redirect to /state1
    	$urlRouterProvider.otherwise("/");
    	//
    	// Now set up the states
    	$stateProvider
          .state('home', {
          	url: "/",
          	templateUrl: "/Home/Home",
          	controller: 'homeCtrl'
          })
          .state('login', {
          	url: "/login",
          	templateUrl: "/Home/Login",
          	controller: 'loginCtrl'
          });
    });

App.controller('loginCtrl', function ($scope) {
	$scope.login = '';
	$scope.password = '';
});

App.controller('homeCtrl', function ($scope) {
	$scope.worksList = [
		{
			Date: new Date(),
			Title: '123',
			Author: 'Автор1'
		},
		{
			Date: new Date(),
			Title: 'qwe',
			Author: 'Автор2'
		},
		{
			Date: new Date(),
			Title: 'asd',
			Author: 'Автор3'
		},
	];
});