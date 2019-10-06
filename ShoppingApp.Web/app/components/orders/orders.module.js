(function () {
    angular.module('shoppingapp.orders', ['shoppingapp.common']).config(config)

    config.$inject = ['$stateProvider', '$urlRouterProvider']
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('order', {
            url: '/orders',
            parent: 'base',
            templateUrl: '/app/components/orders/orderListView.html',
            controller: 'orderListController'
        })
    }
})()