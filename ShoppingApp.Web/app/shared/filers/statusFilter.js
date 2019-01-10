/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == true) {
                return 'kích hoạt'
            }
            else {
                return 'khóa'
            }
        }
    })
})(angular.module('shoppingapp.common'))