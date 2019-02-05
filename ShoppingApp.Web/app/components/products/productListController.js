/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productListController', productListController)
    productListController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$ngBootbox', '$filter']
    function productListController($scope, apiService, notificationService, $state, $ngBootbox, $filter) {
        $scope.products = []
        $scope.page = 0
        $scope.pagesCount = 0
        $scope.getProducts = getProducts
       
        $scope.keyword = ''
        $scope.search = search
        $scope.deleteProduct = deleteProduct
        $scope.selectAll = selectAll
        $scope.deleteMultiple = deleteMultiple
        $scope.isAll = false


        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true
                })
                $scope.isAll = true
            }
            else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false
                })
                $scope.isAll = false
            }
        }

        function deleteMultiple() {
            var listId = []

            $.each($scope.selected, function (i, item) {
                listId.push(item.ID)
            })
            config = {
                params: {
                    checkedProducts: JSON.stringify(listId)
                }
            }
            apiService.del('/api/product/deletemulti', config, function (result) {
                $scope.getProducts()
                notificationService.displaySuccess('xoa thanh cong ' + result.data + ' ban ghi')

            }, function () {
                notificationService.displayError('xoa khong thanh cong')
            })
        }

        $scope.$watch('products', function (n, o) {
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
            getProducts()
        }

        function getProducts(page) {
            page = page || 0

            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 2
                }
            }
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    if ($scope.keyword.length == 0) {
                        notificationService.displayWarning('nhập vào ô tìm kiếm')
                    }
                    else
                        notificationService.displaySuccess('Tìm thấy: ' + result.data.TotalCount + ' bản ghi')
                }

                $scope.products = result.data.Item
                $scope.page = result.data.Page
                $scope.pagesCount = result.data.TotalPages
                $scope.totalCount = result.data.TotalCount
            }, function () {
                console.log('load product failed.')
            })
        }

        function deleteProduct(id) {
            $ngBootbox.confirm('ban co chac muon xoa').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/product/delete', config, function (result) {
                    notificationService.displaySuccess('xoa thanh cong ban ghi ' + result.data.Name)
                    $scope.getProductCategories()
                }, function (err) {
                    notificationService.displayError('xoa khong thanh cong')
                })

            })
        }
       
        $scope.getProducts()


    }
})(angular.module('shoppingapp.products'))