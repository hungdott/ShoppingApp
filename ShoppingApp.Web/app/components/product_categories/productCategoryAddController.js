/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController)
    productCategoryAddController.$inject = ['apiService', '$scope','notificationService','$state','commonService']
    function productCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.productCategory = {

            CreatedDate: new Date(),
            Status: true,
            HomeFlag:true
        }
        $scope.AddProductCategory = AddProductCategory
        $scope.GetSeoTittle = GetSeoTittle
        function GetSeoTittle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name)
        }
        function AddProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess('da Thêm mới thanh cong '+result.data.Name)
                $state.go('product_categories')
            }, function (err) {
                notificationService.displayError('da Thêm mới khong thanh cong')
                console.log('create failed.')
            })
        }
        $scope.parentCategories = []
        function loadParentCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories=result.data
            }, function () {
                console.log('cannot get parents')
            })
        }
        loadParentCategories();
    }
})(angular.module('shoppingapp.product_categories'))