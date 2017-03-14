var s, g,
Magazine = {

    settings: {
        mName: $(".mName"),
        mYear: $(".mYear"),
        mNumber: $(".mNumber"),
        filter: $(".filter"),
        addButton: $(".btn-bitbucket"),
        addArticleContainer: $(".add-article"),
        selectors: $(".select2"),
        gridContainer: $(".article-grid"),
    },

    mvcgrid: {
        grid: $('.mvc-grid'),
    },

    init: function () {
        s = this.settings;
        g = this.mvcgrid;
        this.bindUIActions();
    },

    bindUIActions: function () {
        s.mName.change(function () {
            Magazine.getMYears($(this).data("id"));
        });

        s.mYear.change(function () {

        });

        s.mNumber.change(function () {

        });

        s.filter.on("click", function () {
            Magazine.hideArticlePartial();
            Magazine.tickFilters($(this));
        });

        s.selectors.change(function () {
            Magazine.hideArticlePartial();
            Magazine.gridRequest();
        });

        s.addButton.on("click", function () {
            Magazine.loadingOverlay(true);
            Magazine.createArticlePartial($(this));
            Magazine.loadingOverlay(false);
        });
    },

    tickFilters: function (elem) {
        if (elem.children().hasClass("fa-times")) {
            elem.children().removeClass("fa-times");
            elem.children().addClass("fa-check");
            elem.parent().children(".select2").removeAttr("disabled")
        } else {
            elem.children().addClass("fa-times");
            elem.children().removeClass("fa-check");
            elem.parent().children(".select2").attr("disabled", "disabled")
        }

        Magazine.gridRequest();

        if (s.mName.attr("disabled") !== "disabled" && s.mYear.attr("disabled") !== "disabled" && s.mNumber.attr("disabled") !== "disabled") {
            Magazine.showAddArticleButton(true);
        } else {
            Magazine.showAddArticleButton(false);
        }
    },

    gridRequest: function () {
        var format;
        if (s.mName.attr("disabled") !== "disabled") {
            format = $(".mName option:selected").data("id");
        }

        var year;
        if (s.mYear.attr("disabled") !== "disabled") {
            year = $(".mYear option:selected").text();
        }

        var number;
        if (s.mNumber.attr("disabled") !== "disabled") {
            number = $(".mNumber option:selected").text();
        }

        $('.mvc-grid').mvcgrid({
            reloadStarted: function (grid) {
                Magazine.loadingOverlay(true, s.gridContainer)
            },
            reloadEnded: function (grid) {
                Magazine.loadingOverlay(false, s.gridContainer)
            },
            reloadFailed: function (grid, result) {
                Magazine.loadingOverlay(false, s.gridContainer)
            },
            requestType: 'get', // defaults to get
            reload: true,
            data: {
                format: format,
                year: parseInt(year),
                section: parseInt(number),
            }
        });
    },

    createArticlePartial: function (elem) {
        var format = $(".mName option:selected").data("id");
        var year = $(".mYear option:selected").text();
        var number = $(".mNumber option:selected").text();
        $.ajax({
            url: '/Magazine/CreateArticlePartial',
            dataType: 'html',
            data: {
                format: format,
                year: parseInt(year),
                section: parseInt(number),
            },
            success: function (data) {
                $('.add-article').html(data);
                CKEDITOR.replace('editor', {
                    height: 500,
                });
                $(".select2").select2();
                $("#image-uploader").fileinput({
                    language: "hu",
                    showUpload: false,
                });
            },
        });
    },

    hideArticlePartial: function () {
        $(".add-article").html("");
    },

    showAddArticleButton: function (show) {
        if (show) {
            s.addButton.css("display", "block")
        }
        else {
            s.addButton.css("display", "none")
        }

    },

    loadingOverlay: function (show, elem) {
        if (elem === undefined) {
            if (show) {
                $.LoadingOverlay("show")
            } else {
                $.LoadingOverlay("hide")
            }
        } else {
            if (show) {
                elem.LoadingOverlay("show", "'" + elem + "'")
            } else {
                elem.LoadingOverlay("hide", "'" + elem + "'")
            }
        }
    },

    articleAddSuccess: function () {
        Magazine.hideArticlePartial();
        $('.mvc-grid').mvcgrid({
            reload: true,
        });
    },

};