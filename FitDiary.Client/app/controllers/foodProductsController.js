'use strict';
app.controller('foodProductsController', ['$scope', 'foodProductsService', function ($scope, foodProductsService) {

    $scope.foodProducts = [];

    foodProductsService.getFoodProducts().then(function (results) {

        $scope.foodProducts = results.data;

    }, function (error) {
        //alert(error.data.message);
    });
}]);
