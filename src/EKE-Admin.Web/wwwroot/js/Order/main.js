var s, g,
Order = {

    settings: {
        gridContainer: $(".order-grid"),
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
        Order.sideBarActive(".f6")

        $('.mvc-grid').mvcgrid({
            reloadStarted: function (grid) {
                Order.loadingOverlay(true, s.gridContainer)
            },
            reloadEnded: function (grid) {
                Order.loadingOverlay(false, s.gridContainer)
            },
            reloadFailed: function (grid, result) {
                Order.loadingOverlay(false, s.gridContainer)
            },
        });
        
    },

    bindUIActions: function () {

    },

    sideBarActive: function (elem) {
        $(".active").removeClass("active");
        $("" + elem + "").addClass("active");
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