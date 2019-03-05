/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('footerEditController', footerEditController)
    footerEditController.$inject = [
        'apiService',
        '$scope',
        'notificationService',
        '$state',
        'commonService',
        '$stateParams'
    ]
    function footerEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
  
        //$scope.ckeditorOptions = {
        //    language: 'vi',
        //    height: '200px',

        //}
        $scope.UpdateFooter = UpdateFooter
       
        function loadFooterDetail() {
            var config = {
                params: {
                    id: "default"
                }
            }
            apiService.get('/api/footer/getfooter', config, function (result) {
                $scope.footer = result.data
                console.log(result.data)

            }, function (err) {
                notificationService.displayError(err.data)
            })
        }

        loadFooterDetail();

        function UpdateFooter() {
            
            apiService.put('/api/footer/update', $scope.footer, function (result) {
                notificationService.displaySuccess('da cap nhat thanh cong ')
                $state.go('footers')
            }, function (err) {
                notificationService.displayError('cap nhat khong thanh cong')
                console.log('create failed.')
            })
        }
       
       
    }

})(angular.module('shoppingapp.footers'))