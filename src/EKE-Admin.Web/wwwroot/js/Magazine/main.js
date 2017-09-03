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

        noSection: $(".noSection"),
        noYear: $(".noYear"),
        noClick: "no-click",
    },

    mvcgrid: {
        grid: $('.mvc-grid'),
    },

    init: function () {
        s = this.settings;
        g = this.mvcgrid;
        this.initPage();
        this.bindUIActions();
    },

    initPage: function () {
        $(".select2").select2();
        $('.mvc-grid').mvcgrid();

        Magazine.sideBarActive(".f4");
        Magazine.setLocalStorageElements();
    },

    bindUIActions: function () {
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

        if (s.mName.attr("disabled") !== "disabled")
        {
            s.noYear.removeClass(s.noClick);
        }
        else
        {
            s.mYear.attr("disabled", "disabled");
            s.noYear.addClass(s.noClick);
            s.noYear.children().addClass("fa-times");
            s.noYear.children().removeClass("fa-check");

            s.mNumber.attr("disabled", "disabled");
            s.noSection.addClass(s.noClick);
            s.noSection.children().addClass("fa-times");
            s.noSection.children().removeClass("fa-check");
        }

        if (s.mName.attr("disabled") !== "disabled" && s.mYear.attr("disabled") !== "disabled")
        {
            s.noSection.removeClass(s.noClick);
        }
        else
        {
            s.mNumber.attr("disabled", "disabled");
            s.noSection.addClass(s.noClick);
            s.noSection.children().addClass("fa-times");
            s.noSection.children().removeClass("fa-check");
        }

        if (s.mName.attr("disabled") !== "disabled" && s.mYear.attr("disabled") !== "disabled" && s.mNumber.attr("disabled") !== "disabled") {
            Magazine.showAddArticleButton(true);
        } else {
            Magazine.showAddArticleButton(false);
        }
    },

    gridRequest: function () {
        var format;
        if (s.mName.attr("disabled") !== "disabled") {
            format = $(".mName option:selected").val();
        }

        var year;
        if (s.mYear.attr("disabled") !== "disabled") {
            year = $(".mYear option:selected").val();
        }

        var number;
        if (s.mNumber.attr("disabled") !== "disabled") {
            number = $(".mNumber option:selected").val();
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
                format: parseInt(format),
                year: parseInt(year),
                section: parseInt(number),
            }
        });
    },

    createArticlePartial: function (elem) {
        var format = $(".mName option:selected").val();
        var year = $(".mYear option:selected").val();
        var number = $(".mNumber option:selected").val();
        $.ajax({
            url: location.href + '/CreateArticlePartial',
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

                window.localStorage.setItem("MFormat", format);
                window.localStorage.setItem("MYear", year);
                window.localStorage.setItem("MNumber", number);
            },
        });
    },

    hideArticlePartial: function () {
        $(".add-article").html("");
    },

    showAddArticleButton: function (show) {
        if (show) {
            s.addButton.css("display", "block");
        }
        else {
            s.addButton.css("display", "none");
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

    removeArticleSuccess: function () {
        $('.mvc-grid').mvcgrid({
            reload: true,
        });
    },

    editArticleSuccess: function () {
        CKEDITOR.replace('editor2', {
            height: 400,
        });
        $(".select2").select2();
        $("#image-uploader").fileinput({
            language: "hu",
            showUpload: false,
        });
    },

    sideBarActive: function (elem) {
        $(".active").removeClass("active");
        $("" + elem + "").addClass("active");
    },

    setLocalStorageElements: function () {
        var format = window.localStorage.getItem("MFormat");
        var number = window.localStorage.getItem("MNumber");
        var year = window.localStorage.getItem("MYear");

        if (format != null && number != null && year != null) {
            s.noYear.removeClass(s.noClick);
            s.noSection.removeClass(s.noClick);

            $('.select2').removeAttr("disabled");

            $('.fa-times').addClass('fa-check');
            $('.fa-check').removeClass('fa-times');

            Magazine.showAddArticleButton(true);

            $('.mName').val(parseInt(format)).trigger('change');
            $('.mYear').val(parseInt(year)).trigger('change');
            $('.mNumber').val(parseInt(number)).trigger('change');

            Magazine.gridRequest();

            setTimeout(function () {
                $('.mvc-grid').mvcgrid({
                    reload: true
                });;
            }, 1000);
           
        }
    }
};