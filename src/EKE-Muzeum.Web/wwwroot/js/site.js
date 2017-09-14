var s, g,
    Museum = {
        settings: {
            scrollPosition: $(".scrollUpdate"),
            appendResult: $(".appendResult"),
            scrollable: true,
        },

        initPage: function () {
            s = this.settings;
            this.init();
            this.bindUIActions();
        },

        init: function () {
            window.localStorage.setItem("scrollable", true);
            $(window).load(function () {
                SEMICOLON.documentOnResize.init()
            });
        },

        bindUIActions: function () {
            Museum.scrollToAjax();
        },

        scrollToAjax: function () {
            function isScrolledIntoView(elem) {
                var docViewTop = $(window).scrollTop();
                var docViewBottom = docViewTop + $(window).height();

                var elemTop = $(elem).offset().top;
                var elemBottom = elemTop + $(elem).height();

                return ((elemBottom <= docViewBottom) && (elemTop >= docViewTop));
            }
        },

        getElementsByPage: function () {
            var category = s.scrollPosition.data("category");
            var page = parseInt(s.scrollPosition.data("page"), 10)
            $.ajax({
                type: "GET",
                url: "/Home/GetElements",
                context: document.body,
                data: {
                    page: parseInt(s.scrollPosition.data("page"), 10),
                    category: category
                },
                traditional: true,
                success: function (data) {
                    s.appendResult.html(data);
                    s.scrollPosition.data('page', parseInt(page + 1, 10));
                    s.scrollable = true;
                },
                error: function () {

                }
            });
        }
    };
