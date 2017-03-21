var s, g,
UserManagement = {

    settings: {
        gridContainer: $(".usermanagemet-grid"),
        deleteUser: $(".delete-user"),
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
        s.deleteUser.on("click", function () {
            UserManagement.deleteUser($(this).data("id"));
        });
    },

    initMvcGrid: function () {
        $('.mvc-grid').mvcgrid({
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

    deleteUser: function (id) {
        $.ajax({
            url: '/Account/DeleteConfirmed/',
            dataType: 'html',
            data: {
                id: id,
            },
            method:'post',
            success: function (data) {
                $('.mvc-grid').mvcgrid({
                    reload: true,
                });
            },
        });
    },

};