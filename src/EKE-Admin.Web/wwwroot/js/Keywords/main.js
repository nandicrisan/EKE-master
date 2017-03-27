var s, g,
Keywords = {

    settings: {
        gridContainer: $(".keywords-grid"),
    },

    mvcgrid: {
        gridContainer: $('.keyword-grid'),
        grid: $('.mvc-grid'),
    },

    init: function () {
        s = this.settings;
        g = this.mvcgrid;
        this.initPage();
        this.initMvcGrid();
        this.bindUIActions();
    },

    initPage: function () {
        Keywords.sideBarActive(".f5");
    },

    bindUIActions: function () {
    },

    initMvcGrid: function () {
        $('.mvc-grid').mvcgrid({
            reloadStarted: function (grid) {
                Keywords.loadingOverlay(true, s.gridContainer)
            },
            reloadEnded: function (grid) {
                Keywords.loadingOverlay(false, s.gridContainer)
            },
            reloadFailed: function (grid, result) {
                Keywords.loadingOverlay(false, s.gridContainer)
            },
        });
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

    keywordAddSuccess: function () {
        $('.mvc-grid').mvcgrid({
            reload: true,
        });
    },

    deleteTag: function (id) {
        $.ajax({
            url: '/Magazine/RemoveTag',
            dataType: 'html',
            data: {
                id: id,
            },
            success: function () {
                $('.mvc-grid').mvcgrid({
                    reload: true,
                });
            },
        });
    },

    sideBarActive: function (elem) {
        $(".active").removeClass("active");
        $("" + elem + "").addClass("active");
    }
};