var app = angular.module('App', ['ngRoute']);

app.config(function($routeProvider){
  $routeProvider.when("/home", {
      controller: "homeController",
      templateUrl: "/app/views/home.html"
  });

  $routeProvider.when("/products", {
      controller: "foodProductsController",
      templateUrl: "/app/views/foodProducts.html"
  });

  $routeProvider.otherwise({ redirectTo: "/home" });
})
.controller('ngAppDemoController', function($scope){
  $scope.a = 1;
  $scope.b = 2;
});
