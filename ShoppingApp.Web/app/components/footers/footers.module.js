(function () {
	angular.module('shoppingapp.footers', ['shoppingapp.common']).config(config)

	config.$inject = ['$stateProvider', '$urlRouterProvider']
	function config($stateProvider, $urlRouterProvider) {
		$stateProvider.state('footers', {
			url: '/footers',
			parent: 'base',
			templateUrl: '/app/components/footers/footerView.html',
			controller: 'footerController'
		}).state('edit_footer', {
		    url: '/edit_footer',
		    parent: 'base',
		    templateUrl: '/app/components/footers/footerEditView.html',
		    controller: 'footerEditController'
		})
	}
})()