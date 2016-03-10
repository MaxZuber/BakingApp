angular
    .module('banking')
    .config(['$locationProvider', '$httpProvider', '$stateProvider', '$urlRouterProvider', cfg]);

function cfg($locationProvider, $httpProvider, $stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/login');

    $stateProvider
    .state('login', {
        url: '/login',
        templateUrl: '/Content/login.html',
        controller: 'authController as a'
    })
    .state('signup', {
        url: '/signup',
        templateUrl: '/Content/signup.html',
        controller: 'signupController as a'
    })
    .state('home', {
        url: '/home',
        "abstract": true,
        views: {
            '': {
                templateUrl: '/Content/home.html',
                controller: 'mainController as a'
            }
        }
    })
    .state('home.dashboard', {
        url: '/dashboard',
        templateUrl: '/Content/dashboard.html',
        controller: 'dashboardController as ds'
    })
    .state('home.history', {
        url: '/history',
        templateUrl: '/Content/history.html',
        controller: 'dashboardController as ds'
    })
    .state('home.actions', {
        url: '/actions',
        templateUrl: '/Content/actions.html',
        controller: 'dashboardController as ds'
    });



    //$routeProvider
    //    .when('/home/dashboard', {
    //        templateUrl: '/Content/dashboard.html',
    //        controller: 'mainController as a'
    //    })
    //    .when('/login', {
    //        templateUrl: '/Content/login.html',
    //        controller: 'authController as a'
    //    })
    //    .when('/signup', {
    //        templateUrl: '/Content/signup.html',
    //        controller: 'signupController as a'
    //    });

    $httpProvider.interceptors.push('myInterceptor');
    // configure html5 to get links working on jsfiddle
    //$locationProvider.html5Mode(true);
}