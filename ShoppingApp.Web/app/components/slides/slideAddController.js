/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('slideAddController', slideAddController)
    slideAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService']
    function slideAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.slide = {
            Status: true,  
        }
        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px',

        }
        $scope.AddSlide = AddSlide
        $scope.GetSeoTittle = GetSeoTittle
        function GetSeoTittle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name)
        }
        function AddSlide() {
            apiService.post('/api/slide/create', $scope.slide, function (result) {
                notificationService.displaySuccess('da them moi thanh cong ' + result.data.Name)
                $state.go('slides')
            }, function (err) {
                notificationService.displayError('da them moi khong thanh cong')
                console.log('create failed.')
            })
        }
       

        $scope.ChooseImage = function () {

            var finder = new CKFinder()
            finder.selectActionFunction = function (fileUrl) {
                $scope.slide.Image = fileUrl
            }
            finder.popup()
        }

       ;

    }

})(angular.module('shoppingapp.slides'))