var s, g,
    Museum = {

        settings: {
            gridContainer: $(".elem-grid"),
            gridContainerCat: $(".category-grid"),
            addButton: $(".btn-bitbucket"),
        },

        mvcgrid: {
            grid: $('.mvc-grid'),
        },

        initElem: function () {
            s = this.settings;
            g = this.mvcgrid;
            this.initPage();
            this.bindUIActions();
        },

        initCategory: function () {
            s = this.settings;
            g = this.mvcgrid;
            this.initPageCat();
            this.bindUIActions();
        },

        initTag: function () {
            s = this.settings;
            g = this.mvcgrid;
            this.initPageTag();
        },

        initPage: function () {
            Museum.sideBarActive(".f10")
            $(".select2").select2();
            $('.mvc-grid').mvcgrid({
                reloadStarted: function (grid) {
                    Museum.loadingOverlay(true, s.gridContainer)
                },
                reloadEnded: function (grid) {
                    Museum.loadingOverlay(false, s.gridContainer)
                    Museum.initXEditElem();
                },
                reloadFailed: function (grid, result) {
                    Museum.loadingOverlay(false, s.gridContainer)
                },
            });

        },

        initPageCat: function () {
            Museum.sideBarActive(".f11")

            $('.mvc-grid').mvcgrid({
                reloadStarted: function (grid) {
                    Museum.loadingOverlay(true, s.gridContainer)
                },
                reloadEnded: function (grid) {
                    Museum.loadingOverlay(false, s.gridContainer)
                    Museum.initXEditCategory();
                },
                reloadFailed: function (grid, result) {
                    Museum.loadingOverlay(false, s.gridContainer)
                },
            });

        },

        initPageTag: function () {
            Museum.sideBarActive(".f12")

            $('.mvc-grid').mvcgrid({
                reloadStarted: function (grid) {
                    Museum.loadingOverlay(true, s.gridContainer)
                },
                reloadEnded: function (grid) {
                    Museum.loadingOverlay(false, s.gridContainer)
                },
                reloadFailed: function (grid, result) {
                    Museum.loadingOverlay(false, s.gridContainer)
                },
            });

        },

        initXEditCategory: function () {
            $('.categoryNameCheck').editable({
                params: function (params) {
                    params["__RequestVerificationToken"] = $('[name="__RequestVerificationToken"]').val();
                    return params;
                }
            });
        },

        initXEditElem: function () {
            $('.elemTitleCheck').editable({
                params: function (params) {
                    params["__RequestVerificationToken"] = $('[name="__RequestVerificationToken"]').val();
                    return params;
                }
            });

            $('.elemAuthorCheck').editable({
                params: function (params) {
                    params["__RequestVerificationToken"] = $('[name="__RequestVerificationToken"]').val();
                    return params;
                }
            });

            $('.elemDescriptionCheck').editable({
                params: function (params) {
                    params["__RequestVerificationToken"] = $('[name="__RequestVerificationToken"]').val();
                    return params;
                }
            });

            $('.elemVisibleCheck').editable({
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

        bindUIActions: function () {
            s.addButton.on("click", function () {
                Museum.loadingOverlay(true);
                Museum.createElemPartial($(this));
                Museum.loadingOverlay(false);
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
        },

        createElemPartial: function (elem) {
            $.ajax({
                url: '/Museum/CreateElemPartial',
                dataType: 'html',
                success: function (data) {
                    $('.add-elem').html(data);
                    CKEDITOR.replace('editor', {
                        height: 300,
                    });
                    $(".select2").select2();
                    $("#image-uploader").fileinput({
                        language: "hu",
                        showUpload: false,
                    });
                },
            });
        },
    };