var s, p,

Main = {
    partials: {
        article: $(".selectedArticles"),
        magazine: $(".lastMagazines")
    },

    settings: {
    },

    init: function () {
        s = this.settings;
        p = this.partials;

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
            min: 1800,
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
                p.article.css("display","none");
                p.article.fadeIn(3000);
            }
        });
    }
};