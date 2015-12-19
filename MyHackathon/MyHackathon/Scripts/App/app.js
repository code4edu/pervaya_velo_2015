/// <reference path="../angular.js" />
/// <reference path="../angular/angular-ui-router.js" />
var fileId = 0;

var App = angular
    .module('App', ['ngAnimate', 'ui.router', 'anim-in-out', 'ngFileUpload']);

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
          })
          .state('addDoc', {
          	url: "/addDoc",
          	templateUrl: "/Home/AddDoc",
          	controller: 'addDocCtrl'
          });
    });

App.run(function ($rootScope, $state) {
	var adminPages = ['users'];
	$rootScope.role = '';
	$rootScope.userName = '';



	$rootScope.$on('$stateChangeStart', function (event, toState, toParams, fromState, fromParams) {
		if (_.contains(adminPages, toState.name) && $rootScope.role !== 'Admin') {
			event.preventDefault();
			$state.go('home');
		}
	});
});

App.controller('loginCtrl', function ($scope, $http, $rootScope, $state) {
	$scope.login = '';
	$scope.password = '';

	$scope.submit = function () {
		var login = $scope.login;
		$http.post('/api/Authentication', {
			Email: $scope.login,
			Password: $scope.password
		}).then(function (response) {
			console.log(response);
			$rootScope.role = response.data.roles;
			$rootScope.userName = login;
			$state.go('home');
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


App.controller('signupCtrl', function ($scope, $state, $http) {
	var login;
	$scope.login = '';
	$scope.password1 = '';
	$scope.password2 = '';

	$scope.submit = function () {
		if ($scope.password1 !== $scope.password2) {
			$scope.passwordErr = true;
			return;
		}
		login = $scope.login;
		$scope.passwordErr = false;
		$http.post('/api/Users', {
			Mail: $scope.login,
			Password: $scope.password1,
			Role: 'Student'
		}).then(function (response) {
			$rootScope.role = response.data.roles;
			$rootScope.userName = login;
			$state.go('home');
		});
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

App.controller('usersCtrl', function ($scope, $http) {
	$scope.usersList = [];

	$http.get('/api/Users').then(function (response) {
		$scope.usersList = response.data;
	});
});

App.controller('MainCtrl', function ($scope, $state, $rootScope, $http) {
	$scope.logout = function () {
		$http.delete('/api/Authentication').then(function () {
			$rootScope.role = '';
			$state.go('home');
		});
	}
});

App.controller('addDocCtrl', function ($scope, $http, Upload) {
	$scope.submit = function () {
		if ($scope.file) {
			$scope.upload($scope.file);
		}
	};


	// upload on file select or drop
	$scope.upload = function (file) {
		Upload.upload({
			url: 'api/Files',
			data: { file: file, 'name': ''+fileId++ }
		}).then(function (resp) {
			console.log('Success ' + resp.config.data.file.name + 'uploaded. Response: ' + resp.data);
		}, function (resp) {
			console.log('Error status: ' + resp.status);
		}, function (evt) {
			var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
			console.log('progress: ' + progressPercentage + '% ' + evt.config.data.file.name);
		});
	};


	$scope.$watch('file', function (newVal) {
		if (!newVal) {
			return;
		}
		$scope.upload($scope.file);
	});
});