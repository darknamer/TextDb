// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
angular.module('application', [])
    .config([function () {}])
    .controller('bodyCtrl', [
        '$log',
        '$scope',
        function ($log, $scope) {
            $scope.init = function () {
                $log.log('Application is initial.');
            };
        }])
    .controller('appCtrl', [
        '$log',
        '$scope',
        'Timesheet',
        function ($log, $scope, Timesheet) {
            $scope.timesheet = {};
            $scope.timesheets = [];

            $scope.init = function () {
                $log.log('Application is initial.');
                $scope.getTimesheetAll();
                $scope.getTimesheetModel();
            };

            $scope.getTimesheetModel = function () {
                $scope.timesheet = {
                    id: 0,
                    projectId: '',
                    username: '',
                    description: '',
                    hours: 0,
                    createdDate: '',
                };
            };

            $scope.getTimesheetAll = function () {
                Timesheet.getAll().then(function (value) {
                    var data = value.data;
                    $log.info(data);
                    $scope.timesheets = data;
                });
            };
            
            $scope.setForm = function (timesheet) {
                angular.copy(timesheet, $scope.timesheet);
            };
            
            $scope.clear = function () {
                $scope.timesheet = $scope.getTimesheetModel();
            };

            $scope.submit = function () {
                if ($scope.timesheet.id === 0) {
                    Timesheet.insert($scope.timesheet).then(function () {
                        $log.info('Timesheet is inserted.');
                        alert('Timesheet is inserted.');
                        $scope.getTimesheetAll();
                        $scope.getTimesheetModel();
                    });
                } else {
                    Timesheet.update($scope.timesheet).then(function () {
                        $log.info('Timesheet is updated.');
                        alert('Timesheet is updated.');
                        $scope.getTimesheetAll();
                        $scope.getTimesheetModel();
                    });
                }
            };

            $scope.delete = function (id) {
                if (confirm('Do you want to delete?')) {
                    Timesheet.delete(id).then(function () {
                        $log.info('Timesheet is deleted.');
                        $scope.getTimesheetAll();
                    })
                }
            }
        }])
    .factory('Timesheet', [
        '$http',
        function ($http) {
            var baseUrl = '/api/Timesheet';
            return {
                getAll: function () {
                    var url = baseUrl + '/GetAll';
                    return $http.get(url);
                },
                getById: function (id) {
                    var url = baseUrl + '/GetById/' + id;
                    return $http.get(url);
                },
                insert: function (timesheet) {
                    var url = baseUrl + '/Insert';
                    return $http.post(url, timesheet);
                },
                update: function (timesheet) {
                    var url = baseUrl + '/Update';
                    return $http.put(url, timesheet);
                },
                delete: function (id) {
                    var url = baseUrl + '/Delete/' + id;
                    return $http.delete(url);
                }
            };
        }])
;