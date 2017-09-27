var s, g, x
Synonym = {

    settings: {
        gridContainer: $(".synonym-grid"),
    },

    mvcgrid: {
        grid: $('.mvc-grid'),
    },

    init: function () {
        s = this.settings;
        g = this.mvcgrid;
        x = this.xedit;
        this.initPage();
        this.bindUIActions();
    },

    initPage: function () {
        Synonym.sideBarActive(".f7")

        $('.mvc-grid').mvcgrid({
            reloadStarted: function (grid) {
                Synonym.loadingOverlay(true, s.gridContainer)
            },
            reloadEnded: function (grid) {
                Synonym.loadingOverlay(false, s.gridContainer)
                Synonym.initXEdit();
            },
            reloadFailed: function (grid, result) {
                Synonym.loadingOverlay(false, s.gridContainer)
            },
        });

    },

    initXEdit: function () {
        $('.synonymCheck').editable({
            params: function (params) {
                params["__RequestVerificationToken"] = $('[name="__RequestVerificationToken"]').val();
                return params;
            }
        });
    },

    bindUIActions: function () {

    },

    updateAddSynonym: function (id) {
        var text = $(".synonymAddCheck[data-id=" + id + "]").val()

        $.ajax({
            url: localStorage.siteRoot + '/Synonym/ConnectSynonym',
            dataType: 'html',
            data: {
                id: parseInt(id),
                text: text,
            },
            success: function (data) {
                $('.mvc-grid').mvcgrid({
                    reload: true,
                });
            },
        });
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

    addSuccess: function () {
        $('.mvc-grid').mvcgrid({
            reload: true,
        });
    }
};