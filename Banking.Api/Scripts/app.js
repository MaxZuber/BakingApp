angular
    .module('banking', [
        'ui.router'
    ])
    .run(['$http', 'storageService', function ($http, storageService) {
        $http.defaults.headers.common.Authorization = storageService.getAuthenticationToken;
    }]);

