/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController)
    productCategoryAddController.$inject = ['apiService', '$scope','notificationService','$state']
    function productCategoryAddController(apiService, $scope, notificationService,$state) {
        $scope.productCategory = {

            CreatedDate: new Date(),
            Status: true,
            HomeFlag:true
        }
        $scope.AddProductCategory = AddProductCategory
        function AddProductCategory() {
            apiService.post('/api/productcategory/create', $scope.productCategory, function (result) {
                notificationService.displaySuccess('da them moi thanh cong '+result.data.Name)
                $state.go('product_categories')
            }, function (err) {
                notificationService.displayError('da them moi khong thanh cong')
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