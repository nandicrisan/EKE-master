﻿var s, g,
Magazine = {

    settings: {
        numArticles: 5,
        mName: $(".mName"),
        mYear: $(".mYear"),
        mNumber: $(".mNumber"),
        filter: $(".filter"),
        addButton: $(".btn-bitbucket"),
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
            Magazine.tickFilters($(this));
        });

        s.addButton.on("click", function () {
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
                    CKEDITOR.replace('editor');
                },
            });
        });
    },

    tickFilters: function (elem) {
        if (elem.children().hasClass("fa-times")) {
            elem.children().removeClass("fa-times");
            elem.children().addClass("fa-check");
            elem.parent().children(".select2").removeAttr("disabled")
            Magazine.gridRequest();
        } else {
            elem.children().addClass("fa-times");
            elem.children().removeClass("fa-check");
            elem.parent().children(".select2").attr("disabled", "disabled")
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
            requestType: 'get', // defaults to get
            reload: true,
            data: {
                format: format,
                year: parseInt(year),
                section: parseInt(number),
            }
        });
    },

    showAddArticleButton: function (show) {
        if (show) {
            s.addButton.css("display", "block")
        }
        else {
            s.addButton.css("display", "none")
        }

    },

};