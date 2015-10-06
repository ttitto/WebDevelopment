(function (app) {
    var ListController = function ($scope, movieService) {
        movieService
            .getAll()
            .success(function (data) {
                $scope.movies = data;
            });

        $scope.delete = function remove(movie) {
            movieService
                .delete(movie)
                .success(function remove() {
                    for (var i = 0; i < $scope.movies.length; i++) {
                        if ($scope.movies[i].Id == movie.Id) {
                            $scope.movies.splice(i, 1);
                            break;
                        }
                    }
                });
        }

        $scope.create = function create() {
            $scope.edit = {
                movie: {
                    Title: '',
                    Runtime: 0,
                    ReleaseYear: new Date().getFullYear()
                }
            };
        }
    };

    ListController.$inject = ["$scope", "movieService"];
    app.controller('ListController', ListController);
}(angular.module('atTheMovies')));