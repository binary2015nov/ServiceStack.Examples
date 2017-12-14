//Simple string formatting enhancement
if (!String.format) { String.prototype.format = function () { var args = arguments; return this.replace(/\{(\d+)\}/g, function (match, number) { return (typeof args[number] !== 'undefined') ? args[number] : match; }); }; }

$(function () {
    $.ajaxSetup({ cache: false });

    var restLog = function (method, url) {
        if (method === "GET") {
            url = '<a href="{0}" target="_blank">{0}</a>'.format(url);
        }
        $("#restlog").prepend("<div><b class='ib'>{0}</b><i>{1}</i></div>".format(method, url));
    };

    var allGenres = [];

    var refreshExistingMovies = function () {
        var moviesUrl = "movies", jqFilter = $("SELECT[name=genres]"), filterGenre = jqFilter.val();
        if (filterGenre) {
            moviesUrl += "/genres/" + filterGenre;
        }

        restLog("GET", moviesUrl);
        $.getJSON(moviesUrl, function (r) {
            var html = "";
            GLOBALMOVIES = r;
            for (var i = 0, count = r.movies.length; i < count; i++) {
                var movie = r.movies[i];
                html += "<dd id='{0}'><label class='ib'>{1}</label><span class='ib lnk-update'>update</span><span class='ib lnk-delete'>delete</span></dd>".format(movie.id, movie.title);

                $.each(movie.genres, function (i, genre) {
                    if ($.inArray(genre, allGenres) == -1) {
                        allGenres.push(genre);
                    }
                });

                var genreHtml = '<option class="first" value="">[ All Genres ]</option>';
                $.each(allGenres, function (i, genre) {
                    var selected = filterGenre === genre ? ' selected="selected"' : '';
                    genreHtml += '<option value="{0}"{1}>{0}</option>'.format(genre, selected);
                });
                jqFilter.html(genreHtml);
            }

            $("#existing-movies").html(html);

            $("#existing-movies .lnk-delete").on('click', function () {
                var request = { type: 'delete', url: "movies/" + $(this).parent().attr('id') };
                restLog("DELETE", request.url);
                request.success = refreshExistingMovies;
                $.ajax(request);
            });

            $("#existing-movies .lnk-update").on('click', function () {
                var movieId = $(this).parent().attr('id');
                restLog("GET", "movies/" + movieId);
                $.getJSON("movies/" + movieId, function (r) {
                    showDetailsForm(r.movie);
                });
            });
        });
    };

    $("SELECT[name=genres]").change(function () {
        refreshExistingMovies();
    });

    $("#btnReset").click(function () {
        restLog("POST", "reset-movies");
        $.post("reset-movies", function (r) {
            refreshExistingMovies();
        });
    });

    var showDetailsForm = function (updateMovie) {
        var isUpdate = !!updateMovie;
        var newMovie = {
            id: 0,
            imdbId: "tt0110912",
            title: "Pulp Fiction",
            rating: 8.9,
            director: "Quentin Tarantino",
            releaseDate: new Date(1994, 10, 24),
            tagLine: "Girls like me don't make invitations like this to just anyone!",
            genres: ["Crime", "Drama", "Thriller"]
        };

        var movie = updateMovie || newMovie;

        $("FORM INPUT[type=submit]").val(isUpdate ? "Update movie" : "Add new movie");
        var action = "movies";
        $("FORM").attr('action', isUpdate ? action + "/" + movie.id : action);
        $("FORM").attr('method', isUpdate ? 'PUT' : 'POST');

        var title = isUpdate ? "Update " + movie.title : "Add a new movie";
        $("FORM H2").html(title);

        for (var name in movie) {
            $("INPUT[name=" + name + "]").val(movie[name]);
        }

        if (movie['releaseDate'] != null) {
            var convertedDate = typeof movie['releaseDate'] == "string" ? new Date(movie['releaseDate']) : movie['releaseDate'];

            convertedDate = (convertedDate.getFullYear() + "-" + (convertedDate.getMonth() + 1) + "-" + convertedDate.getDate());
            $("INPUT[name=releaseDate]").val(convertedDate);
        }

        $("FORM").fadeIn('fast');
    };

    $("#btnAdd").click(function () {
        showDetailsForm();
    });

    $("FORM").submit(function (e) {
        e.preventDefault();

        var data = {}, form = $(this);
        $("FORM INPUT[type=text]").each(function () {
            data[this.name] = this.value;
        });

        $.ajax({
            url: form.attr("action"),
            type: form.attr('method'),
            data: data,
            dataType: "json",
            success: function () {
                restLog(form.attr('method'), form.attr('action'));
                form.hide();
                refreshExistingMovies();
            }
        });
    });


    $("#csvformat B").click(function () {
        restLog("GET", "movies?format=csv");
        location.href = "movies?format=csv";
    });

    refreshExistingMovies();
});