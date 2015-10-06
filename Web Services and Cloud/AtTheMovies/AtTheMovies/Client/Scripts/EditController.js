(function (app) {
    var EditController = function ($scope, movieService) {
        $scope.isEditable = function isEditable() {
            return $scope.edit && $scope.edit.movie;
        }

        $scope.cancel = function cancel() {
            $scope.edit.movie = null;
        }

        $scope.save = function save() {
            if ($scope.edit.movie.Id) {
                updateMovie();
            } else {
                createMovie();
            }
        };

        var updateMovie = function updateMovie() {
            movieService.update($scope.edit.movie)
            .success(function updateMovieSuccess() {
                angular.extend($scope.movie, $scope.edit.movie);
                $scope.edit.movie = null;
            });
        };

        var createMovie = function createMovie() {
            movieService.create($scope.edit.movie)
            .success(function createMovieSuccess(movie) {
                $scope.movies.push(movie);
                $scope.edit.movie = null;
            });
        }
    };

    EditController.$inject = ["$scope", "movieService"];
    app.controller("EditController", EditController);

})(angular.module("atTheMovies"));