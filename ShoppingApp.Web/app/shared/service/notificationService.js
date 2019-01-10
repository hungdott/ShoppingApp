/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/toastr/toastr.js" />
/// <reference path="D:\asp.net\AppShopASP\Git\ShoppingApp.Web\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.factory('notificationService', notificationService)
    function notificationService() {
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "positionClass": "toast-top-right",
            "onclick": null,
            "fadeIn": 300,
            "fadeOut": 1000,
            "timeOut": 4000,
            "extendedTimeOut": 1000,
            "showEasing": "swing",
            "hideEasing": "linear",
        }
        
        function displaySuccess(message) {
            toastr["success"](message)
        }
        function displayError(message) {
            if (Array.isArray(message)) {
                message.each(function (err) {
                    toastr["error"](err)
                })
            }
            toastr["error"](message)
        }
        function displayWarning(message) {
            toastr["warning"](message)
        }
        function displayInfo(message) {
            toastr["info"](message)
        }
        return {
            displaySuccess: displaySuccess,
            displayError: displayError,
            displayWarning: displayWarning,
            displayInfo: displayInfo,
        }
    }
})(angular.module('shoppingapp.common'))