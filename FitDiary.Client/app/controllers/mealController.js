'use strict';
app.controller('mealController', ['$scope', 'travelsService', '$window', function ($scope, travelsService, $window) {

    $scope.selectedTestAccount = null;
    $scope.meals = [];

    travelsService.getMeals().then(function (response) {
        $window.alert("ok meal");
        $scope.meals = response.data;

    }, function (error) {
        $window.alert(error.data.message);
        //alert(error.data.message);
    });
}]);