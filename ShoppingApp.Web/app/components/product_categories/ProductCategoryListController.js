/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryListController', productCategoryListController)
    productCategoryListController.$inject = ['$scope','apiService']
    function productCategoryListController($scope, apiService) {
        $scope.productCategories = []
        $scope.getProductCategories = getProductCategories

        function getProductCategories() {
            apiService.get('/api/productcategory/getall', null, function (result) {
                $scope.productCategories = result.data
            }, function () {
                console.log('load productCategory failed.')
            })
        }
        $scope.getProductCategories()
    }
})(angular.module('shoppingapp.product_categories'))