var s, g,
    Museum = {
        settings: {
            scrollPosition: $(".scrollUpdate"),
            appendResult: $(".appendResult"),
            appendedPortfolioItems: $(".appendResult .portfolio-item"),
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

            $("#search").keyup(function () {
                if ($(this).val().length > 2) {
                    Museum.search($(this).val());
                }
            });
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
            var keyword = s.scrollPosition.data("keyword");

            if (page % 5 === 0 && page != 0) {
                $(".appendResult .portfolio-item").slice(0, 48).remove();
                SEMICOLON.documentOnResize.init()
            }

            $.ajax({
                type: "GET",
                url: "/Home/GetElements",
                context: document.body,
                data: {
                    page: parseInt(s.scrollPosition.data("page"), 10),
                    category: category,
                    keyword: keyword
                },
                traditional: true,
                success: function (data) {
                    //data = $(".loadElems").html();
                    s.scrollPosition.data('page', parseInt(page + 1));
                    s.appendResult.append(data);
                    SEMICOLON.documentOnResize.init()
                    setTimeout(function () { $(window).on("scroll", Museum.scrollToAjax) }, 3000);
                },
                error: function () {

                }
            });
        },

        search: function (keyword) {
            s.scrollPosition.data("keyword", keyword);
            s.scrollPosition.data("page", 0);
            s.scrollPosition.data("category", "");

            $.ajax({
                type: "GET",
                url: "/Home/Search",
                context: document.body,
                data: {
                    keyword: keyword
                },
                traditional: true,
                success: function (data) {
                    $(".portfolio-item").remove();
                    s.appendResult.append(data);
                    SEMICOLON.documentOnResize.init()
                    SEMICOLON.widget.loadFlexSlider();
                },
                error: function () {

                }
            });
        },

        getElementsByCategory: function (category) {
            s.scrollPosition.data("keyword", "");
            s.scrollPosition.data("page", 0);
            s.scrollPosition.data("category", category);

            $.ajax({
                type: "GET",
                url: "/Home/GetElements",
                context: document.body,
                data: {
                    category: category,
                    page: 0
                },
                traditional: true,
                success: function (data) {
                    $(".portfolio-item").remove();
                    s.appendResult.append(data);
                    SEMICOLON.documentOnResize.init()
                    SEMICOLON.widget.loadFlexSlider();
                },
                error: function () {

                }
            });
        }
    };
