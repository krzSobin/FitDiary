'use strict';
app.controller('foodProductController', ['$scope', 'travelsService', '$window', function ($scope, travelsService, $window) {

    $scope.selectedTestAccount = null;
    $scope.travels = [];
    $scope.testAccounts = [];

    travelsService.getTravels().then(function (response) {
        $window.alert("Please enter your name!");
        $scope.travels = response.data;

    }, function (error) {
        $window.alert(error.data.message);
        //alert(error.data.message);
    });

    travelsService.getCategories().then(function (response) {
        $window.alert("ok cat");
        $scope.testAccounts = response.data;

    }, function (error) {
        $window.alert(error.data.message);
        //alert(error.data.message);
    });

}]);