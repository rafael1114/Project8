var queueCtrl = function ($scope, $http) {

    $scope.initQueue = function () {
        $scope.getWaitingList();
        $scope.getActiveCustomer();
    }

    $scope.getNextCustomer = function () {
        $scope.updateNextCustomer();
    }

    $scope.getWaitingList = function () {
        $http.get("/Queue/waitingList")
            .then(function (response) {
                $scope.waitList = response.data;
            });
    }

    $scope.getActiveCustomer = function () {
        $http.get("/Queue/activeCustomer")
            .then(function (response) {
                $scope.nextCustomer = response.data;
            });
    }

    $scope.updateNextCustomer = function () {
        $http.get("/Queue/UpdateNextCustomer")
            .then(function (response) {
                $scope.waitList = response.data;
            }).then(function (response) {
                $scope.getActiveCustomer();
            });

    }

    $scope.insertNewCustomer = function () {
        if ($scope.firstName && $scope.lastName) {
            $http.get("/Queue/insertNewCustomer/?firstName=" + $scope.firstName + "&lastName=" + $scope.lastName)
                .then(function (response) {
                    $scope.waitList = response.data;
                });
        }
    }

    $scope.initQueue();
}

var app = angular.module('myApp', []);
app.controller('queueCtrl', queueCtrl);
