'use strict';
app.controller('homeController', ['$scope', 'travelsService', function ($scope, travelsService) {
    $scope.days = [];

    travelsService.getDays().then(function (response) {
        $scope.days = response.data;
    }, function (error) {
        alert(error.data.message);
    });
}]);