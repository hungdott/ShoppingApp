/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController)
    productCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state','$stateParams','commonService']
    function productCategoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.productCategory = {

            CreatedDate: new Date(),
            Status: true,
            HomeFlag: true
        }
        $scope.UpdateProductCategory = UpdateProductCategory
        $scope.GetSeoTittle = GetSeoTittle
        function GetSeoTittle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name)
        }
        function loadProductCategoryDeltail() {
            apiService.get('/api/productcategory/getbyid/' + $stateParams.id, null, function (result) {
                $scope.productCategory=result.data
            }, function (err) {
                console.log(err)
                notificationService.displayError(err.data)
               
            })
        }
        function UpdateProductCategory() {
            apiService.put('/api/productcategory/update', $scope.productCategory, function (result) {
                notificationService.displaySuccess('cap nhat thanh cong ' + result.data.Name)
                $state.go('product_categories')
            }, function (err) {
                notificationService.displayError('cap nhat khong thanh cong')
                console.log('create failed.')
            })
        }
        $scope.parentCategories = []
        function loadParentCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.parentCategories = result.data
            }, function () {
                console.log('cannot get parents')
            })
        }
        loadParentCategories();
        loadProductCategoryDeltail()
    }
})(angular.module('shoppingapp.product_categories'))