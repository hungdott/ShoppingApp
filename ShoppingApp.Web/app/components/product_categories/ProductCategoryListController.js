﻿/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryListController', productCategoryListController)
    productCategoryListController.$inject = ['$scope','apiService']
    function productCategoryListController($scope, apiService) {
        $scope.productCategories = []
        $scope.page = 0
        $scope.pagesCount = 0

        $scope.getProductCategories = getProductCategories

        function getProductCategories(page) {
            page = page || 0

            var config = {
                params: {
                    page: page,
                    pageSize: 2
                }
            }
            apiService.get('/api/productcategory/getall', config, function (result) {
                $scope.productCategories = result.data.Item
                $scope.page = result.data.Page
                $scope.pagesCount = result.data.TotalPages
                $scope.totalCount = result.data.TotalCount
                console.log('sdfdsf')
            }, function () {
                console.log('load productCategory failed.')
            })
        }
        $scope.getProductCategories()
    }
})(angular.module('shoppingapp.product_categories'))