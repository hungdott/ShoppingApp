/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('orderListController', orderListController)
    orderListController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$ngBootbox', '$filter']
    function orderListController($scope, apiService, notificationService, $state, $ngBootbox, $filter) {
        $scope.orders = []
        $scope.page = 0
        $scope.pagesCount = 0
        $scope.getOrders = getOrders
       
        $scope.keyword = ''
        $scope.search = search
        $scope.deleteProduct = deleteProduct
        $scope.selectAll = selectAll
        $scope.deleteMultiple = deleteMultiple
        $scope.isAll = false

        
        var config = {
            params: {
                keyword: $scope.keyword,
                page: 1,
                pageSize: 4
            }
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.orders, function (item) {
                    item.checked = true
                })
                $scope.isAll = true
            }
            else {
                angular.forEach($scope.orders, function (item) {
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
                $scope.getOrders()
                notificationService.displaySuccess('Xóa thanh cong ' + result.data + ' ban ghi')

            }, function () {
                notificationService.displayError('Xóa khong thanh cong')
            })
        }

        $scope.$watch('orders', function (n, o) {
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
            getOrders(null, true)
           
        }
        //column
        $scope.orderColumns = [
            {
                field: "",
                title: "Tên khách hàng",
                width: "50px",
                template: "{{this.dataItem.Order.CustomerName}}"
            }]
       

        function getOrders(page, isSerch= false) {
            page = page || 0

            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 4
                }
            }
            apiService.get('/api/order/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    //notificationService.displayWarning('Không có bản ghi nào được tìm thấy')
                }
                else {
                    if ($scope.keyword.length == 0 && isSerch == true) {
                        notificationService.displayWarning('nhập vào ô tìm kiếm')
                    }
                    else
                        notificationService.displaySuccess('Tìm thấy: ' + result.data.TotalCount + ' bản ghi')
                }

                $scope.orders = result.data.Items
                console.log($scope.orders,'order')
                $scope.page = result.data.Page
                $scope.pagesCount = result.data.TotalPages
                $scope.totalCount = result.data.TotalCount
            }, function () {
                console.log('load product failed.')
            })
        }

        function deleteProduct(id) {
            $ngBootbox.confirm('ban co chac muon Xóa').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/product/delete', config, function (result) {
                    notificationService.displaySuccess('Xóa thanh cong ban ghi ' + result.data.Name)
                    $scope.getProductCategories()
                }, function (err) {
                    notificationService.displayError('Xóa khong thanh cong')
                })

            })
        }
       
        getOrders()


    }
})(angular.module('shoppingapp.orders'))