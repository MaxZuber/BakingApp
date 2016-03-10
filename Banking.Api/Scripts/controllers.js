angular.module('banking')
    .controller('authController', authController)
    .controller('mainController', mainController)
    .controller('signupController', signupController)
    .controller('dashboardController', dashboardController);

authController.$inject = ['$scope', 'authService', '$state'];

function authController($scope, authService, $state) {
    var vm = this;
    vm.username = '';
    vm.password = '';
    vm.loginFailed = false;
    vm.loginFailedMessage = '';
    vm.login = login;

    function login() {

        authService.login(vm.username, vm.password)
        .then(function () {
            $state.go('home.dashboard');
        }, function (response) {
            vm.loginFailed = true;
            vm.loginFailedMessage = response.message;
        });
    }
}

mainController.$inject = ['$http'];

function mainController($http) {
    var vm = this;   
}

signupController.$inject = ['$http', 'authService', '$scope'];

function signupController($http, authService, $scope) {
    var vm = this;

    vm.isUsernameBusy = false;
    vm.username = '';
    vm.password = '';
    vm.confirmPassword = '';
    vm.signupSuccess = false;
    vm.signupError = false;
    vm.signupErrorMessage = '';
    vm.signup = signup;
    vm.isUsernameExist = isUsernameExist;

    $scope.$watch('a.username', function (current, original) {
        if (vm.isUsernameBusy) {
            vm.isUsernameBusy = false;
        }
    });

    function isUsernameExist() {
        if (vm.username !== '') {
            authService.isUsernameBusy(vm.username)
            .then(function (result) {
                vm.isUsernameBusy = result.success;
            }, function (result) {
                vm.isUsernameBusy = false;
            });
        }
    };
    function signup() {
        debugger 
        authService.register(vm.username, vm.password, vm.confirmPassword)
            .then(function() {
                vm.signupError = false;
                vm.signupSuccess = true;
            }, function(result) {
                vm.signupError = true;
                vm.signupSuccess = false;
                vm.signupErrorMessage = result.Message;
            });
    };
}

function dashboardController() {
    vm = this;

    vm.curentBalance = 250;
    vm.transactions = [{ amount: 10, from: 'text' }, { amount: -10, from: 'max' }];
}