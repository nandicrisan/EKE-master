var s, g,
    Museum = {
        settings: {
            scrollPosition: $(".scrollUpdate"),
            appendResult: $(".appendResult"),
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
            $(window).scroll(Museum.scrollToAjax);
        },

        scrollToAjax: function () {
            var hT = s.scrollPosition.offset().top,
                hH = s.scrollPosition.outerHeight(),
                wH = $(window).height();
            if ($(window).scrollTop() > hT + hH - wH) {
                $(window).off("scroll", Museum.scrollToAjax);
                Museum.getElementsByPage();
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
                    data = $("#portfolio").html();
                    s.appendResult.html(data);
                    $(window).on("scroll", Museum.scrollToAjax);
                },
                error: function () {

                }
            });
        }
    };
