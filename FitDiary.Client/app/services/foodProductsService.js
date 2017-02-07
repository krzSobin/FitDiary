'use strict';
app.factory('foodProductsService', ['$http', function ($http) {

    var serviceBase = 'http://localhost:52493/';
    var foodProductsServiceFactory = {};

    var _getfoodProducts = function () {

        return $http.get(serviceBase + 'api/foodProducts').then(function (results) {
            return results;
        });
    };

    foodProductsServiceFactory.getfoodProducts = _getfoodProducts;

    return foodProductsServiceFactory;
}]);
