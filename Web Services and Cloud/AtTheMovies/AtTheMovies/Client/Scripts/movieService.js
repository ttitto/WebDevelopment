(function (app) {
    var movieService = function ($http, movieApiUrl) {
        var getAll = function getAll() {
            return $http.get(movieApiUrl);
        };

        var getById = function getById(id) {
            return $http.get(movieApiUrl + id);
        };

        var update = function update(movie) {
            $http.put(movieApiUrl + movie.Id, movie);
        };

        var create = function create(movie) {
            $http.post(movieApiUrl, movie);
        };

        var destroy = function destroy(movie) {
            return $http.delete(movieApiUrl + movie.Id);
        };

        return {
            getAll: getAll,
            getById: getById,
            update: update,
            create: create,
            delete: destroy
        };
    };

    app.factory("movieService", movieService);
})(angular.module("atTheMovies"));