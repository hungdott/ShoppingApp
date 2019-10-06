/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productAddController', productAddController)
    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService']
    function productAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true,
            HomeFlag: true,
            HotFlag:true
        }
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px',

        }
        $scope.AddProduct = AddProduct
        $scope.GetSeoTittle = GetSeoTittle
        function GetSeoTittle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name)
        }
        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages)
            apiService.post('/api/product/create', $scope.product, function (result) {
                notificationService.displaySuccess('da Thêm mới thanh cong ' + result.data.Name)
                $state.go('products')
            }, function (err) {
                notificationService.displayError('da Thêm mới khong thanh cong')
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
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
                //$scope.product.Image = fileUrl
            }
            finder.popup()
        }

        $scope.moreImages = [];

        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })

            }
            finder.popup();
        }
        loadProductCategories()

    }

})(angular.module('shoppingapp.products'))