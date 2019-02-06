/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productEditController', productEditController)
    productEditController.$inject = [
        'apiService',
        '$scope',
        'notificationService',
        '$state',
        'commonService',
        '$stateParams'
    ]
    function productEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.product = {}
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px',

        }
        $scope.UpdateProduct = UpdateProduct
        $scope.GetSeoTittle = GetSeoTittle
        function GetSeoTittle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name)
        }
        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data
            }, function (err) {
                console.log(err)
                notificationService.displayError(err.data)

            })
        }
        function UpdateProduct() {
            apiService.put('/api/product/update', $scope.product, function (result) {
                notificationService.displaySuccess('da cap nhat thanh cong ' + result.data.Name)
                $state.go('products')
            }, function (err) {
                notificationService.displayError('cap nhat khong thanh cong')
                console.log('create failed.')
            })
        }
        function loadProductCategories() {
            apiService.get('/api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data
            }, function () {
                console.log('cannot get parents')
            })
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder()
            finder.selectActionFunction = function (fileUrl) {
                $scope.product.Image = fileUrl
            }
            finder.popup()
        }
        loadProductCategories()
        loadProductDetail();
    }

})(angular.module('shoppingapp.products'))