var s, g,
MagazineList = {

    settings: {
        gridContainer: $(".magazinelist-grid"),
    },

    mvcgrid: {
        grid: $('.mvc-grid'),
    },

    init: function () {
        s = this.settings;
        g = this.mvcgrid;
        this.initPage();
        this.bindUIActions();
        this.initMvcGrid();
    },

    initPage: function () {
        $(".select2").select2();
    },

    bindUIActions: function () {

    },

    initMvcGrid: function () {
        $('.mvc-grid').mvcgrid({
            reloadStarted: function (grid) {
                MagazineList.loadingOverlay(true, s.gridContainer)
            },
            reloadEnded: function (grid) {
                MagazineList.loadingOverlay(false, s.gridContainer)
            },
            reloadFailed: function (grid, result) {
                MagazineList.loadingOverlay(false, s.gridContainer)
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

};