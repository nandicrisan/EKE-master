var s, g, x
MagazineList = {

    settings: {
        gridContainer: $(".magazinelist-grid"),
    },

    mvcgrid: {
        grid: $('.mvc-grid'),
    },

    xedit: {
        visibilityCheck: $(".visibleCheck"),
    },

    init: function () {
        s = this.settings;
        g = this.mvcgrid;
        x = this.xedit;
        this.initPage();
        this.bindUIActions();
        this.initMvcGrid();
    },

    initPage: function () {
        $(".select2").select2();
        MagazineList.sideBarActive(".f3");

        $("#image-uploader").fileinput({
            language: "hu",
            showUpload: false,
            allowedFileExtensions: ["jpg", "png"]
        });
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
                MagazineList.initXEdit();
                MagazineList.initCoverUploader();
            },
            reloadFailed: function (grid, result) {
                MagazineList.loadingOverlay(false, s.gridContainer)
            },
        });
    },

    initXEdit: function () {
        $('.visibleCheck').editable({
            source: [
                { value: 'True', text: 'Igen' },
                { value: 'False', text: 'Nem' },
            ],
            params: function (params) {
                params["__RequestVerificationToken"] = $('[name="__RequestVerificationToken"]').val();
                return params;
            }
        });
    },

    initCoverUploader: function () {
        $(".cover-uploader").fileinput({
            language: "hu",
            showUpload: false,
            allowedFileExtensions: ["jpg", "png"],
            uploadUrl: "/Magazine/UploadCover",
            uploadExtraData: function (previewId, index) {
                var elem = $(this)[0].$btnFile[0];
                return {
                    __RequestVerificationToken: $('[name="__RequestVerificationToken"]').val(),
                    id: $(elem).find(".cover-uploader").first().data('id'),
                };
            },
            maxFileCount: 1,
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

    sideBarActive: function (elem) {
        $(".active").removeClass("active");
        $("" + elem + "").addClass("active");
    },

};