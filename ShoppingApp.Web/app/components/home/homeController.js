/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('homeController', homeController)
    homeController.$inject = ['$state', '$scope', 'apiService','notificationService']
    function homeController($state, $scope, apiService, notificationService) {
        apiService.get('/api/home/TestMethod',
                null,function () {
                       $state.go('home')
                },function () {
                    notificationService.displayError("bạn không có quyền đăng nhập.")
                })
    }
})(angular.module('shoppingapp'))