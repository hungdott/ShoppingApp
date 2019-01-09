/// <reference path="../plugins/angular/angular.js" />
var myApp = angular.module('myModule', [])
var heros = [{ id: 1, name: "hung" }, {id:2,name:"do"}]

//myApp.controller('studentController', studentController)
//myApp.controller('teacherController', teacherController)
myApp.controller('schoolController', schoolController)
myApp.service('validatorService', validatorService)
myApp.directive('shoppingAppDirective', shoppingAppDirective)
myApp.directive('shoppingAppDirective2', shoppingAppDirective2)
schoolController.$inject = ['$scope', 'validatorService']

//declare
//function studentController($rootScope,$scope) {
//    $rootScope.message='root'
//    $scope.message = 'message from student'
//}
//function studentController($scope) {

//    //$scope.message = 'message from student'
//}
//function teacherController($scope) {
//    //$scope.message = 'message from teacher'
//}
function schoolController($scope, validatorService) {
    //$scope.message = 'message from school'
    
    $scope.checkNumber = function () {
        $scope.message = validatorService.checkNumber($scope.num)
    }
}
function validatorService() {
    return{
        checkNumber: checkNumber
    }
    function checkNumber(input) {
        
        if (input % 2 == 0) {
            return 'even'
        }
        else {
            return 'odd'
        }
    }
}

function shoppingAppDirective() {
    return {
        //template:'<h1>directive</h1>'
        templateUrl:'/Scripts/spa/shopping.html'
    }
}
function shoppingAppDirective2() {
    return {
        restrict:"A",
        //template:'<h1>directive</h1>'
        templateUrl: '/Scripts/spa/shopping.html'
    }
}