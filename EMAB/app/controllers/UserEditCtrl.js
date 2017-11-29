(function () {

    'use strict';

    angular.module('app')
        .controller('UserEditCtrl', ['$scope', '$stateParams', '$timeout', '$state', 'UsersService', UserEditCtrl]);

    function UserEditCtrl($scope, $stateParams, $timeout, $state, UsersService) {
        $scope.user = { UserName: '', Email: '', FirstName: '', LastName: '', Id: '' };
        $scope.messages = { success: '', items: [] };

        $scope.notify = function (success, items, delay) {
            $scope.messages = { success: success, items: items };
            $timeout(function () {
                $scope.messages.items = [];
            }, delay || 3000);
        };

        $scope.refresh = function () {
            UsersService.get({ id: $stateParams.id }, function (data) {
                $scope.user = data;
            });
        };

        if ($stateParams.id !== '') {
            $scope.refresh();
        }

        $scope.saveUser = function (isValid) {
            $scope.submitted = true;
            if (!isValid) {
                return;
            }
            $scope.errors = '';
            UsersService.post($scope.user, function (data) {
                if (data.result) {
                    $scope.notify(true, ['Successfully saved!']);
                    if ($stateParams.id === '') {
                        $state.go('user', { id: data.id });
                    } else {
                        $scope.refresh();
                    }
                } else {
                    $scope.notify(false, data.messages);
                }
            });
        };
    };
})();
