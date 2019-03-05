(function (app) {
    app.controller('footerController', footerController)
    footerController.$inject = [
        'apiService',
        '$scope',
        'notificationService',
        '$state',
        'commonService',
        '$stateParams'
    ]
    function footerController(apiService, $scope, notificationService, $state, commonService, $stateParams) {
        function getFooter() {
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
        getFooter();

    }
})(angular.module('shoppingapp.footers'))