/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryListController', productCategoryListController)
    productCategoryListController.$inject = ['$scope','apiService','notificationService','$state']
    function productCategoryListController($scope, apiService, notificationService,$state) {
        $scope.productCategories = []
        $scope.page = 0
        $scope.pagesCount = 0
        $scope.getProductCategories = getProductCategories
        $scope.keyword = ''
        $scope.search = search
  
 

        function search() {
            getProductCategories()
        }

       
        function getProductCategories(page) {
            page = page || 0

            var config = {
                params: {
                    keyword:$scope.keyword,
                    page: page,
                    pageSize: 2
                }
            }
            apiService.get('/api/productcategory/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    if ($scope.keyword.length == 0) {
                        notificationService.displayWarning('nhập vào ô tìm kiếm')
                    }
                    else
                        notificationService.displaySuccess('Tìm thấy: ' + result.data.TotalCount+' bản ghi')
                }
                
                $scope.productCategories = result.data.Item
                $scope.page = result.data.Page
                $scope.pagesCount = result.data.TotalPages
                $scope.totalCount = result.data.TotalCount
            }, function () {
                console.log('load productCategory failed.')
            })
        }                        
              
        $scope.getProductCategories()
        
    }
})(angular.module('shoppingapp.product_categories'))