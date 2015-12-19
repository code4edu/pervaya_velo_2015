/// <reference path="../angular.js" />
/// <reference path="../angular/angular-ui-router.js" />
var App = angular
    .module('App', ['ngAnimate', 'ui.router', 'anim-in-out']);

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
          })
          .state('about', {
          	url: "/about",
          	templateUrl: "/Home/About"
          })
          .state('signup', {
          	url: "/signup",
          	templateUrl: "/Home/SignUp",
          	controller: 'signupCtrl'
          })

          .state('users', {
          	url: "/users",
          	templateUrl: "/Home/Users",
          	controller: 'usersCtrl'
          });
    });

App.run(function ($rootScope) {
	$rootScope.role = '';

	$rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
		
	});
});

App.controller('loginCtrl', function ($scope, $http) {
	$scope.login = '';
	$scope.password = '';

	$scope.submit = function () {
		$http.post('/api/Authentication', {
			Email: $scope.login,
			Password: $scope.password
		}).then(function (response) {
			console.log(response);
		},
		function (error) {
			console.log(error);

		});
	};
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


App.controller('signupCtrl', function ($scope) {
	$scope.login = '';
	$scope.password1 = '';
	$scope.password2 = '';

	$scope.submit = function () {
		if ($scope.password1 !== $scope.password2) {
			$scope.passwordErr = true;
			return;
		}

		$scope.passwordErr = false;
	};
});

App.constant('pageOrder', [
'home',
'login',
'signup',
'about',
'signup'
]);

App.directive('accordion', function (pageOrder) {
	return {
		link: function (scope, elem, attrs) {
			scope.$on('$stateChangeSuccess', function (event, toState, toParams, fromState, fromParams) {
				var pages = elem.find('[ui-view]'),
					fromPage = angular.element(pages[1]),
					toPage = angular.element(pages[0]),
					directionClasses = directionOfMoving(fromState.name, toState.name);

				fromPage.addClass(directionClasses[0]);
				toPage.addClass(directionClasses[1]);

				scope.$on('animEnd', function () {
					clearAnim(fromPage);
					clearAnim(toPage);
				});

				function clearAnim(page) {
					page.removeClass('center-to-left')
					.removeClass('left-to-center')
					.removeClass('center-to-right')
					.removeClass('right-to-center');
				}

				function directionOfMoving(fromName, toName) {
					var fromPos = _.indexOf(pageOrder, fromName),
						toPos = _.indexOf(pageOrder, toName);

					if (fromPos < toPos) {
						return [
							'center-to-left',
							'left-to-center'
						];
					} else {
						return [
							'center-to-right',
							'right-to-center'
						];
					}
				}
			});
		}
	};
});

App.controller('usersCtrl', function ($scope) {

});