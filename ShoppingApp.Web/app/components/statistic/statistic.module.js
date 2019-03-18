(function () {
	angular.module('shoppingapp.statistics', ['shoppingapp.common']).config(config)

	config.$inject = ['$stateProvider', '$urlRouterProvider']
	function config($stateProvider, $urlRouterProvider) {
		$stateProvider.state('statistic_revenue', {
			url: '/statistic_revenue',
			parent: 'base',
			templateUrl: '/app/components/statistic/revenueStisticView.html',
			controller: 'revenueStisticController'
		})
	}
})()