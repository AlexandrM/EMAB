(function () {

    'use strict';

    angular.module('app')
        .controller('UsersCtrl', ['$scope', '$timeout', 'UsersService', UsersCtrl]);

    function UsersCtrl($scope, $timeout, UsersService) {
        $scope.users = [];
        $scope.messages = { success: '', items: [] };

        $scope.notify = function (success, items, delay) {
            $scope.messages = { success: success, items: items };
            $timeout(function () {
                $scope.messages.items = [];
            }, delay || 3000);
        };

        $scope.refresh = function () {
            UsersService.query({}, function (data) {
                $scope.users = data;
            });
        };

        $scope.refresh();

        $scope.editUser = function (user) {
            user.editUser = { UserName: user.UserName, Email: user.Email, FirstName: user.FirstName, LastName: user.LastName };
            user.isEditMode = true;
        };

        $scope.saveUser = function (user) {
            $scope.errors = '';
            UsersService.post(user.editUser, function (data) {
                if (data.result) {
                    $scope.notify(true, ['Successfully saved!']);
                    user.isEditMode = false;
                    $scope.refresh();
                } else {
                    $scope.notify(false, data.messages);
                }
            });
        };
    };
})();

