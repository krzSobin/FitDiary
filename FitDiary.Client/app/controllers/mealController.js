'use strict';
app.controller('mealController', ['$scope', 'travelsService', '$window', function ($scope, travelsService, $window) {

    $scope.selectedTestAccount = null;
    $scope.meals = [];

    travelsService.getMeals().then(function (response) {
        $scope.meals = response.data;

    }, function (error) {
        $window.alert("kupa");
        //alert(error.data.message);
    });
}]);