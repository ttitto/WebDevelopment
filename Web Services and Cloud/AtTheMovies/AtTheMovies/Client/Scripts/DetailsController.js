(function (app) {
    var DetailsController = function ($scope, movieService, $routeParams) {
        var movieId = $routeParams.id;
        movieService.getById(movieId)
        .success(function (data) {
            $scope.movie = data;
        });

        $scope.edit = function edit() {
            $scope.edit.movie = angular.copy($scope.movie);
        };
    };

    DetailsController.$inject = ["$scope", "movieService", "$routeParams"];
    app.controller('DetailsController', DetailsController);
})(angular.module("atTheMovies"));