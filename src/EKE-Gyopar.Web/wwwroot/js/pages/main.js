var s, p, searchV, t

Main = {
    months: [
        "Január", "Február", "Március",
        "Április", "Május", "Június",
        "Július", "Augusztus", "Szeptember",
        "Október", "November", "December"
    ],

    partials: {
        article: $(".selectedArticles"),
        magazine: $(".lastMagazines"),
        searchpartial: $(".searchResults"),
        articleList: $(".articleList"),
        articleModal: $(".article"),
        paginationResults: $(".paginationResults")
    },

    settings: {
        searchButton: $('.searchBtn'),
    },

    searchVariables: {
        objectType: $("#objectType"),
        tagsSelected: $("#tagsSelected"),
        otherText: $("#otherText"),
        yearRange: $("#yearRange"),
        magazineRange: $("#magazineRange"),
        authorText: $("#author"),

        magazineSelected: $("#magazineSelected"),
        yearSelected: $("#yearSelected"),

        magazineYearSelector: $(".magazineYearSelector"),
        foundMagazineElem: $(".foundMagazineElem"),

        tabs: $("#realestate-tabs"),
    },

    templates: {
        loader: "<div id='articleListMagazine'><i class='fa fa-spinner fa-spin spin-normal'></i></div>",
        searchLoader: "<div id='searchLoader'><i class='fa fa-spinner fa-spin spin-normal'></i></div>",
    },

    init: function () {
        s = this.settings;
        p = this.partials;
        searchV = this.searchVariables;
        t = this.templates;

        this.initIndex();
        this.initAjax();
        this.bindUIActions();
        this.initNoty();
        this.initCookies();
    },

    initCookies: function () {
        $.cookieBar({ message: 'Oldalainkon HTTP-sütiket használunk a jobb működésért. Elfogadom ezek használatát.', acceptText: 'Rendben', bottom: true, fixed: true, expireDays: 1 })
    },

    bindUIActions: function () {
        searchV.magazineYearSelector.on("click", function () {
            var _elem = $(this);
            _elem.find("div").css("display", "none")
            _elem.find(".yearLoading").css("display", "inline-block");
            p.articleList.fadeOut();

            $.ajax({
                type: "GET",
                url: localStorage.siteRoot + "/Home/SearchByMagazineYear",
                context: document.body,
                data: {
                    year: $(this).text(),
                },
                traditional: true,
                success: function (data) {
                    p.articleModal.css("display", "none");

                    p.searchpartial.fadeIn();
                    p.searchpartial.html(data);
                    $("body, html").animate({
                        scrollTop: $($("#posts")).offset().top - 200
                    }, 600);

                    _elem.find("div").css("display", "initial");
                    _elem.find(".yearLoading").css("display", "none");
                },
                error: function () {
                    _elem.find("div").css("display", "initial");
                    _elem.find(".yearLoading").css("display", "none");

                    s.searchButton.button("reset");
                    Main.setNoty('Hiba a lapszámok lekérése során!', 'error');
                }
            });
        });
    },

    initNoty: function () {
        setTimeout(function () {
            new Noty({
                theme: 'relax',
                type: 'info',
                layout: 'topRight',
                text: 'Figyelem! Az oldal fejlesztés alatt áll!',
                timeout: 3000,
            }).show();
        }, 1500);
    },

    setNoty: function (text, type) {
        new Noty({
            theme: 'relax',
            type: type,
            layout: 'topRight',
            text: text,
            timeout: 5000,
        }).show();
    },

    getMoreArticles: function (numToGet) {
        // $.ajax or something
        // using numToGet as param
    },

    initIndex: function () {
        $(".price-range-slider").ionRangeSlider({
            type: "string",
            grid: true,
            min: 1892,
            max: (new Date).getFullYear(),
            //max_postfix: "",
            //prefix: "",
        });

        $(".area-range-slider").ionRangeSlider({
            type: "string",
            grid: true,
            values: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12],
            prettify: function (num) {
                return Main.months[num - 1];
            }
        });
        jQuery(".bt-switch").bootstrapSwitch();

        setTimeout(function () {
            // Google Map
            jQuery('#headquarters-map').gMap({
                address: 'Strada Memorandumului 4, Cluj-Napoca 400000',
                mapTypeId: 'satellite',
                zoom: 16,
                markers: [
                    {
                        address: "Strada Memorandumului 4, Cluj-Napoca 400000",
                        html: "Cluj-Napoca, Romania",
                        icon: {
                            image: "images/icons/map-icon-red.png",
                            iconsize: [32, 36],
                            iconanchor: [14, 44]
                        }
                    }
                ],
                doubleclickzoom: false,
                controls: {
                    panControl: true,
                    zoomControl: true,
                    mapTypeControl: true,
                    scaleControl: true,
                    streetViewControl: true,
                    overviewMapControl: true
                },
                styles: [{ "featureType": "administrative", "elementType": "labels.text.fill", "stylers": [{ "color": "#444444" }] }, { "featureType": "landscape", "elementType": "all", "stylers": [{ "color": "#f2f2f2" }] }, { "featureType": "poi", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "all", "stylers": [{ "color": "#C0B480" }, { "lightness": 60 }] }, { "featureType": "road.highway", "elementType": "all", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "road.arterial", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "transit", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "water", "elementType": "all", "stylers": [{ "color": "#2D522D" }, { "visibility": "on" }] }]
            });
        }, 3000);

        s.searchButton.on('click', function () {
            var $this = $(this);
            $this.button('loading');
        });
    },

    initFacebook: function () {
        (function (d, s, id) {
            var js, fjs = d.getElementsByTagName(s)[0];
            if (d.getElementById(id)) return;
            js = d.createElement(s); js.id = id;
            js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.9&appId=1915861481986606";
            fjs.parentNode.insertBefore(js, fjs);
        }(document, 'script', 'facebook-jssdk'));
    },

    initAjax: function () {

        $.ajax({
            url: localStorage.siteRoot + "/Home/GetLastMagazines",
            success: function (data) {
                p.magazine.html(data);
                SEMICOLON.widget.carousel();
                $("#overlayMagazine").css("display", "none");
                p.magazine.css("display", "none");
                p.magazine.fadeIn(3000);
            },
            error: function () {
                Main.setNoty('Hiba a lapszámok lekérése során!', 'error');
            }
        });

        $.ajax({
            url: localStorage.siteRoot + "/Home/GetSelectedArticles",
            success: function (data) {
                p.article.html(data);
                $("#overlayArticle").css("display", "none");
                p.article.css("display", "none");
                p.article.fadeIn(3000);
            },
            error: function () {
                Main.setNoty('Hiba a cikkek lekérése során!', 'error');
            }
        });
    },

    searchMethod: function (page, firstLoad) {

        if (firstLoad) {
            $('#pagination-search').twbsPagination('destroy');
        }

        var searchFilter = {};
        searchFilter.Page = page;
        searchFilter.RangeTypeYear = true;
        searchFilter.RangeTypeSection = true;

        searchFilter.PublishYearRange = searchV.yearRange.val().split(";");
        if (searchV.yearSelected.val() != null) {
            searchFilter.RangeTypeYear = false;
            searchFilter.PublishYearRange = searchV.yearSelected.val();
        }

        searchFilter.PublishSectionRange = searchV.magazineRange.val().split(";");
        if (searchV.magazineSelected.val() != null) {
            searchFilter.RangeTypeSection = false;
            searchFilter.PublishSectionRange = searchV.magazineSelected.val();
        }

        searchFilter.ObjectType = 0;
        if (searchV.objectType.val() == "on") {
            searchFilter.ObjectType = 1;
        }

        searchFilter.Text = searchV.otherText.val();
        searchFilter.Author = searchV.authorText.val();

        $.ajax({
            type: "GET",
            url: localStorage.siteRoot + "/Home/Search",
            context: document.body,
            data: {
                PublishYearRange: searchFilter.PublishYearRange,
                PublishSectionRange: searchFilter.PublishSectionRange,
                ObjectType: searchFilter.ObjectType,
                Author: searchFilter.Author,
                Text: searchFilter.Text,
                Keyword: searchFilter.Text,
                RangeTypeYear: searchFilter.RangeTypeYear,
                RangeTypeSection: searchFilter.RangeTypeSection,
                Page: searchFilter.Page
            },
            traditional: true,
            success: function (data) {
                p.articleModal.css("display", "none");
                p.articleList.css("display", "none");

                p.searchpartial.fadeIn();
                p.searchpartial.html(data);
                s.searchButton.button("reset");

                Main.initPagination(firstLoad);

                $("body, html").animate({
                    scrollTop: $($("#posts")).offset().top - 250
                }, 600);
            },
            error: function () {
                s.searchButton.button("reset");
                Main.setNoty('Hiba a lekérés során!', 'error');
            }
        });
    },

    initPagination: function (firstLoad) {
        $('#pagination-search').twbsPagination({
            totalPages: Math.ceil(parseInt($(".foundItems").text(), 10) / 8),
            visiblePages: 5,
            initiateStartPageClick: false,
            onPageClick: function (event, page) {
                p.searchpartial.children("#posts").html(t.searchLoader);
                Main.searchMethod(page, false);
            },
        });
    },

    findMagazine: function (id) {
        p.articleModal.css("display", "none");
        p.searchpartial.css("display", "none");
        p.paginationResults.html("");
        p.articleList.html(t.loader);
        p.articleList.fadeIn();
        $.ajax({
            type: "GET",
            url: localStorage.siteRoot + "/Home/SearchMagazineById",
            context: document.body,
            data: {
                magId: id,
            },
            traditional: true,
            success: function (data) {
                p.articleModal.css("display", "none");
                p.articleList.html(data);
                $("body, html").animate({
                    scrollTop: p.articleList.offset().top - 100
                }, 600);
            },
            error: function () {
                Main.setNoty('Hiba a lekérés során!', 'error');
            }
        });
    },

    changeImageState: function (obj) {
        var image = $(".imageState");
        var selectionList = $(".articleListSelection");
        if (image.css("display") != "block") {
            $(".imageState").fadeIn();
            selectionList.addClass("col-md-8");
            obj.removeClass("btn-success");
        } else {
            $(".imageState").fadeOut();
            obj.addClass("btn-success");
            setTimeout(
                function () {
                    selectionList.removeClass("col-md-8");
                }, 500);
        }
    },

    changeFontSize: function (increase) {
        var fontSize = parseInt($(".panel-body").css("font-size"));
        if (increase) {
            fontSize = fontSize + 1 + "px";
        } else {
            fontSize = fontSize - 1 + "px";
        }
        $(".panel-body").css({ 'font-size': fontSize });
    },

    searchArticle: function (id) {
        p.articleList.css("display", "none");
        p.articleModal.html(t.loader);
        p.articleModal.fadeIn();
        $.ajax({
            type: "GET",
            url: localStorage.siteRoot + "/Home/SearchArticleById",
            context: document.body,
            data: {
                slug: id,
            },
            traditional: true,
            success: function (data) {
                p.searchpartial.css("display", "none");
                p.paginationResults.html("");
                p.articleModal.html(data);
                Main.initFacebook();
                SEMICOLON.widget.flickrFeed();
            },
            error: function () {
                Main.setNoty('Hiba a lekérés során!', 'error');
            }
        });
    },

    backToMagazines: function () {
        p.articleList.fadeOut();
        p.searchpartial.fadeIn();
    },

    backToArticles: function () {
        p.articleModal.fadeOut();
        p.articleList.fadeIn();
    },

    takeMeTo: function (number) {
        if (number == 4) {
            $("body, html").animate({
                scrollTop: $(".contact").offset().top - 100
            }, 600);
        } else if (number == 5) {
            $("body, html").animate({
                scrollTop: $("#orderMagazine").offset().top - 100
            }, 600);
        } else {
            $("body, html").animate({
                scrollTop: searchV.tabs.offset().top - 100
            }, 600);

            $(".tab_" + number).click();
        }
    },
};