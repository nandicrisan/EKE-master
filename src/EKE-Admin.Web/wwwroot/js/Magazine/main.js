var s, g,
Magazine = {

    settings: {
        numArticles: 5,
        mName: $(".mName"),
        mYear: $(".mYear"),
        mNumber: $(".mNumber"),
        filter: $(".filter"),
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
            if ($(this).children().hasClass("fa-times")) {
                $(this).children().removeClass("fa-times");
                $(this).children().addClass("fa-check");
                $(this).parent().children(".select2").removeAttr("disabled")
                Magazine.gridRequest();
            } else {
                $(this).children().addClass("fa-times");
                $(this).children().removeClass("fa-check");
                $(this).parent().children(".select2").attr("disabled", "disabled")
            }
        });
    },

    gridRequest: function () {
        if (s.mName.attr("disabled")) { null }
        var format = 0;
        var year = 0;
        var section = 0;

        $('.mvc-grid').mvcgrid({
            requestType: 'get', // defaults to get
            reload: true,
            data: {
                format: "",
                year: 1900,
                section: 0,
            }
        });
    },

    getMYears: function (numToGet) {
        // $.ajax or something
        // using numToGet as param
    }

};