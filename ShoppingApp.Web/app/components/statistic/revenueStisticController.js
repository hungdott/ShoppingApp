/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('revenueStisticController', revenueStisticController)
    revenueStisticController.$inject = ['apiService', '$scope', 'commonService', 'notificationService', '$filter']
    function revenueStisticController(apiService, $scope, commonService, notificationService, $filter) {
        $scope.tabledata = []
        $scope.labels = []
        $scope.series = ['Doanh số', 'Lợi nhuận']

        $scope.SearchDate = SearchDate
        function SearchDate() {
            var config = {
                params: {
                    fromDate: $scope.fromDate,
                    toDate: $scope.toDate
                }
            }
            apiService.get('/api/statistic/getrevenue?fromDate=' + config.params.fromDate + '&toDate=' + config.params.toDate, null, function (response) {
                $scope.tabledata = response.data
                var labels = []
                var chartData = []
                var revenues = []
                var benefits = []
                $.each(response.data, function (i, item) {
                    labels.push($filter('date')(item.Date, 'dd/MM/yyyy'))
                    revenues.push(item.Revenues)
                    benefits.push(item.Benefit)
                })
                chartData.push(revenues)
                chartData.push(benefits)

                $scope.chartdata = chartData
                $scope.labels = labels
            }, function (err) {
                notificationService.displayError('Không thể tải dữ liệu')
            })
        }
        $scope.chartdata = []
        function getSttatistic() {
            var config = {
                params: {
                    fromDate: '01/01/2019',
                    toDate:'01/01/2021'
                }
            }
            apiService.get('/api/statistic/getrevenue?fromDate=' + config.params.fromDate + '&toDate=' + config.params.toDate, null, function (response) {
                $scope.tabledata = response.data
                var labels = []
                var chartData = []
                var revenues = []
                var benefits = []
                $.each(response.data, function (i,item) {
                    labels.push($filter('date')(item.Date,'dd/MM/yyyy'))
                    revenues.push(item.Revenues)
                    benefits.push(item.Benefit)
                })
                chartData.push(revenues)
                chartData.push(benefits)

                $scope.chartdata = chartData
                $scope.labels = labels
            }, function (err) {
                notificationService.displayError('Không thể tải dữ liệu')
            })
        }
        getSttatistic()
    }

})(angular.module('shoppingapp.statistics'))