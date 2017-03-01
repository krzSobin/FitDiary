'use strict';
app.factory('travelsService', ['$http', '$window', function ($http, $window) {

    var serviceBase = 'http://localhost:52493/';
    var travelsServiceFactory = {};

    var _getTravels = function () {

        return $http.get(serviceBase + 'api/foodproducts').then(function successCallback(response) {
            return response;
        }).catch(function errorCallback(response) {
            $window.alert(response);
            $window.alert("errrooorrrr");
        });
    };

    var _getMeals = function () {

        return $http.get(serviceBase + 'api/meals').then(function successCallback(response) {
            return response;
        }).catch(function errorCallback(response) {
            $window.alert(response);
            $window.alert("errrooorrrr");
        });
    };

    var _getDays = function () {

        return $http.get(serviceBase + 'api/days').then(function successCallback(response) {
            return response;
        }).catch(function errorCallback(response) {
            $window.alert(response);
            $window.alert("blaad");
        });
    };

    var _getCategories = function () {

        return $http.get(serviceBase + 'api/foodCategories').then(function successCallback(response) {
            return response;
        }).catch(function errorCallback(response) {
            $window.alert(response);
            $window.alert("errrooorrrrCategories");
        });
    };

    travelsServiceFactory.getDays = _getDays;
    travelsServiceFactory.getTravels = _getTravels;
    travelsServiceFactory.getCategories = _getCategories;
    travelsServiceFactory.getMeals = _getMeals;

    return travelsServiceFactory;

}]);