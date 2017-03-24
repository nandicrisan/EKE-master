var s, g,
UserManagement = {

    settings: {
        gridContainer: $(".usermanagemet-grid"),
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
        g.grid.mvcgrid({
            reloadStarted: function (grid) {
                UserManagement.loadingOverlay(true, s.gridContainer)
            },
            reloadEnded: function (grid) {
                UserManagement.loadingOverlay(false, s.gridContainer)
            },
            reloadFailed: function (grid, result) {
                UserManagement.loadingOverlay(false, s.gridContainer)
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

    reloadMvcGrid: function() {
        g.grid.mvcgrid({
            reload: true
        });
    },
        
};