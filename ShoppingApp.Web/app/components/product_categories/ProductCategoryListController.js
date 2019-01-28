/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryListController', productCategoryListController)
    productCategoryListController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$ngBootbox', '$filter']
    function productCategoryListController($scope, apiService, notificationService, $state, $ngBootbox, $filter) {
        $scope.productCategories = []
        $scope.page = 0
        $scope.pagesCount = 0
        $scope.getProductCategories = getProductCategories
        $scope.keyword = ''
        $scope.search = search
        $scope.deleteProductCategory = deleteProductCategory
        $scope.selectAll = selectAll
        $scope.deleteMultiple = deleteMultiple
        $scope.isAll=false

       
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked=true
                })
                $scope.isAll=true
            }
            else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked=false
                })
                $scope.isAll = false
            }
        }

        function deleteMultiple() {
            var listId = []
            
            $.each($scope.selected,function (i,item) {
                listId.push(item.ID)
            })
            config={
                params:{
                    checkedProductCategory: JSON.stringify(listId)
                }
            }
            apiService.del('/api/productcategory/deletemulti', config, function (result) {
                $scope.getProductCategories()
                notificationService.displaySuccess('xoa thanh cong ' + result.data + ' ban ghi')

            }, function () {
                notificationService.displayError('xoa khong thanh cong')
            })
        }

        $scope.$watch('productCategories', function (n, o) {
            var checked = $filter('filter')(n, { checked: true })
            if (checked.length) {
                $scope.selected = checked
                $('#btnDelete').removeAttr('disabled')
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled')
            }
        }, true)

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
        function deleteProductCategory(id) {
            $ngBootbox.confirm('ban co chac muon xoa').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/productcategory/delete', config, function (result) {
                    notificationService.displaySuccess('xoa thanh cong ban ghi ' + result.data.Name)
                    $scope.getProductCategories()   
                }, function (err) {
                    notificationService.displayError('xoa khong thanh cong')
                })

            })
        }
            
        
    }
})(angular.module('shoppingapp.product_categories'))