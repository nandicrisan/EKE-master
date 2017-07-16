var s, p, searchV

Main = {
    partials: {
        article: $(".selectedArticles"),
        magazine: $(".lastMagazines"),
        searchpartial: $(".searchResults"),
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

        magazineSelected: $("#magazineSelected"),
        yearSelected: $("#yearSelected"),
    },

    init: function () {
        s = this.settings;
        p = this.partials;
        searchV = this.searchVariables;

        this.initIndex();
        this.initAjax();
        this.bindUIActions();
    },

    bindUIActions: function () {

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
            values: [
                "Január", "Február", "Március",
                "Április", "Május", "Június",
                "Július", "Augusztus", "Szeptember",
                "Október", "November", "December"
            ]
        });
        jQuery(".bt-switch").bootstrapSwitch();


        // Google Map
        jQuery('#headquarters-map').gMap({
            address: 'Cluj-Napoca, Romania',
            maptype: 'SATELLITE',
            zoom: 14,
            markers: [
                {
                    address: "Cluj-Napoca, Romania",
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
                panControl: false,
                zoomControl: false,
                mapTypeControl: false,
                scaleControl: false,
                streetViewControl: false,
                overviewMapControl: false
            },
            styles: [{ "featureType": "administrative", "elementType": "labels.text.fill", "stylers": [{ "color": "#444444" }] }, { "featureType": "landscape", "elementType": "all", "stylers": [{ "color": "#f2f2f2" }] }, { "featureType": "poi", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "road", "elementType": "all", "stylers": [{ "color": "#C0B480" }, { "lightness": 60 }] }, { "featureType": "road.highway", "elementType": "all", "stylers": [{ "visibility": "simplified" }] }, { "featureType": "road.arterial", "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "transit", "elementType": "all", "stylers": [{ "visibility": "off" }] }, { "featureType": "water", "elementType": "all", "stylers": [{ "color": "#254B32" }, { "visibility": "on" }] }]
        });

        s.searchButton.on('click', function () {
            var $this = $(this);
            $this.button('loading');
        });

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
            }
        });

        $.ajax({
            url: localStorage.siteRoot + "/Home/GetSelectedArticles",
            success: function (data) {
                p.article.html(data);
                $("#overlayArticle").css("display", "none");
                p.article.css("display", "none");
                p.article.fadeIn(3000);
            }
        });
    },

    searchMethod: function () {
        var searchFilter = {};
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

        searchFilter.Tags = searchV.tagsSelected.val();
        searchFilter.Text = searchV.otherText.val();

        $.ajax({
            type: "GET",
            url: localStorage.siteRoot + "/Home/Search",
            context: document.body,
            data: {
                PublishYearRange: searchFilter.PublishYearRange,
                PublishSectionRange: searchFilter.PublishSectionRange,
                ObjectType: searchFilter.ObjectType,
                Tags: searchFilter.Tags,
                Text: searchFilter.Text,
                Keyword: searchFilter.Text,
                RangeTypeYear: searchFilter.RangeTypeYear,
                RangeTypeSection: searchFilter.RangeTypeSection,
            },
            traditional: true,
            success: function (data) {
                p.searchpartial.html(data);
                s.searchButton.button("reset");
                $("body, html").animate({
                    scrollTop: $($("#posts")).offset().top - 250
                }, 600);
            }
        });
    },
};