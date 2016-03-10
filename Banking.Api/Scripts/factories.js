angular.module('banking')
    .factory('myInterceptor', myHttpInterception);

myHttpInterception.$inject = ['$log', '$q', 'storageService'];

function myHttpInterception($log, $q, storageService) {
    $log.debug('$log is here to show you that this is a regular factory with injection');

    var responseInterceptor = {
        responseError: function (response) {
            if (response.status == 401) {
                storageService.clearAuthenticationToken();
                window.location = "#/login";
                return;
            }
            // otherwise
            return $q.reject(response);
        }
    };

    return responseInterceptor;
}