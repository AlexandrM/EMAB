(function () {
    ROOT = $('base').attr('href');

    'use strict';

    angular.module('app', [
        'ui.router',
        'ngResource',
        'angularMoment',
        'ui.bootstrap',
        'ngAnimate',

        'app'
    ]);
    angular.module('app')
        .service('httpInterceptor', ['$q', '$state', function ($q, $state) {
            var service = this;

            service.responseError = function (response) {
                if (response.status === 401) {
                    $state.go('login');
                }
                return $q.reject(response);
            };
        }])
        .config(['$stateProvider', '$urlRouterProvider', '$httpProvider', function ($stateProvider, $urlRouterProvider, $httpProvider) {
            $urlRouterProvider.otherwise('/home');

            $stateProvider
                .state('home', {
                    url: '/home',
                    templateUrl: 'home/home'
                })
                .state('about', {
                    url: '/about',
                    templateUrl: 'home/about'
                })
                .state('contact', {
                    url: '/contact',
                    templateUrl: 'home/contact'
                })
                .state('users', {
                    url: '/users',
                    templateUrl: 'users/list',
                    controller: 'UsersCtrl',
                })
                .state('user', {
                    url: '/user/:id',
                    templateUrl: 'users/edit',
                    controller: 'UserEditCtrl',
                })
                .state('register', {
                    url: '/register',
                    templateUrl: 'account/register',
                })
                .state('login', {
                    url: '/login',
                    templateUrl: 'account/login',
                })
            ;

            $httpProvider.interceptors.push('httpInterceptor');
        }
    ])
    .run(function () {
    });
})();
