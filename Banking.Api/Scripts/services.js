angular
    .module('banking')
    .service('storageService', localStorageService)
    .service('authService', authSerivice)
    .service('quickUrlSerivce', quickUrlSerivce);

function localStorageService() {
    var vm = this;
    vm.getAuthenticationToken = getAuthenticationToken;
    vm.setAuthenticationToken = setAuthenticationToken;
    vm.clearAuthenticationToken = clearAuthenticationToken;
    vm.isUserAuthenticated = isUserAuthenticated;

    if (vm.storage == undefined) {
        vm.storage = {};
        if (typeof (Storage) !== "undefined") {
            vm.storage = localStorage;
        }
    }

    function getAuthenticationToken() {
        if (!vm.storage.hasOwnProperty('authToken')) {
            vm.storage.authToken = '';
        }
        return vm.storage.authToken;
    }

    function setAuthenticationToken(token) {
        vm.storage.authToken =  token;
    }

    function clearAuthenticationToken() {
        vm.storage.authToken = '';
    }

    function isUserAuthenticated() {
       return  vm.storage.authToken !== '';
    }
}

authSerivice.$inject = ['$http', '$q', 'storageService', 'quickUrlSerivce'];

function authSerivice($http, $q, storageService, quickUrlSerivce) {
    var vm = this;
    vm.login = login;
    vm.register = register;
    vm.isUsernameBusy = isUsernameBusy;


    function login(username, password) {
        var defered = $q.defer();
        $http.post(quickUrlSerivce.loginUrl, { Username: username, Password: password })
        .then(function (response) {
                storageService.setAuthenticationToken(response.data);
                defered.resolve({success: true});
            }, function(response) {
                defered.reject({success: false, message: 'Username or password is incorrect' });
            });
        return defered.promise;
    }

    function isUsernameBusy(username) {
        var defered = $q.defer();
        $http.get(quickUrlSerivce.validateUsernameUrl + username + "/")
        .then(function (response) {
            defered.resolve({ success: response.data, Message: response.data.Message });
        }, function (response) {
                defered.reject({ success: false, Message: "Some error happend when try to check username is already exist" });
            });
        return defered.promise;
    }

    function register(username, password, confirmPassword) {
        var defered = $q.defer();

        $http.post(quickUrlSerivce.signup, { Username: username, Password: password, ConfirmPassword: confirmPassword })
            .then(function(response) {
                debugger;
                defered.resolve();
            }, function (response) {
                debugger 
                defered.reject({Message: response.data});
            });
        return defered.promise;
    }
}

function quickUrlSerivce() {
    var vm = this;

    vm.validateUsernameUrl = '/account/';
    vm.loginUrl = '/account/login/';
    vm.signup = '/account/signup/';
}