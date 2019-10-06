/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('slideEditController', slideEditController)
    slideEditController.$inject = [
        'apiService',
        '$scope',
        'notificationService',
        '$state',
        'commonService',
        '$stateParams'
    ]
    function slideEditController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        $scope.slide = {}
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px',

        }
        $scope.UpdateSlide = UpdateSlide
       
        function loadSlide() {
            apiService.get('/api/slide/getbyid/' + $stateParams.id, null, function (result) {
                $scope.slide = result.data
            }, function (err) {
                console.log(err)
                notificationService.displayError(err.data)

            })
        }
        function UpdateSlide() {
          
            apiService.put('/api/slide/update', $scope.slide, function (result) {
                notificationService.displaySuccess('da cap nhat thanh cong ' + result.data.Name)
                $state.go('slides')
            }, function (err) {
                notificationService.displayError('cap nhat khong thanh cong')
                console.log('create failed.')
            })
        }
        

        $scope.ChooseImage = function () {
            var finder = new CKFinder()
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.slide.Image = fileUrl;
                })
                //$scope.product.Image = fileUrl
            }
            finder.popup()
        }

        loadSlide();
    }

})(angular.module('shoppingapp.products'))