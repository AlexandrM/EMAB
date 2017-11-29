(function () {

    'use strict';

    angular.module('app')
        .service('UsersService', ['$resource', UsersService]);

    function UsersService($resource) {
        return $resource('api/Users/', {}, {
            query: { method: 'GET', params: {}, isArray: true },
            get: { method: 'GET', params: {}, isArray: false },
            post: { method: 'POST', params: { }, isArray: false },
        });
    };
})();
