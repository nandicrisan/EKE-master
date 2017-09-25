var s, g, e,
    Museum = {
        settings: {
            scrollPosition: $(".scrollUpdate"),
            appendResult: $(".appendResult"),
            appendedPortfolioItems: $(".appendResult .portfolio-item"),
        },

        elem: {
            nextElem: $(".nextElemDesc"),
            prevElem: $(".prevElemDesc"),
            closeElem: $(".closeElemDesc"),
            elemDesc: $("#elemDesc"),
        },

        initPage: function () {
            s = this.settings;
            e = this.elem;
            this.init();
            this.bindUIActions();
            this.initCookies();
        },

        init: function () {
            window.localStorage.setItem("scrollable", true);
            $(window).load(function () {
                SEMICOLON.documentOnResize.init()
            });
        },

        initCookies: function () {
            $.cookieBar({ message: 'Oldalainkon HTTP-sütiket használunk a jobb működésért. Elfogadom ezek használatát.', acceptText: 'Rendben', bottom: true, fixed: true })
        },

        bindUIActions: function () {
            $(window).scroll(Museum.scrollToAjax);

            $("#search").keyup(function () {
                if ($(this).val().length > 2) {
                    Museum.search($(this).val());
                }
            });

            $(".elemTitle").on("click", function () {
                Museum.getElement($(this).data("id"));
            });
        },

        closeElemDesc: function () {
            e.elemDesc.slideUp(500);
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

            $(".icon").addClass("loader");
            $(".fa-search").removeClass("fa-search");

            $.ajax({
                type: "GET",
                url: "/Home/Search",
                context: document.body,
                data: {
                    keyword: keyword
                },
                traditional: true,
                success: function (data) {
                    $(".fa").addClass("fa-search");
                    $(".icon").removeClass("loader");

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
        },

        getElement: function (id) {
            e.elemDesc.slideUp(500);
            $.ajax({
                type: "GET",
                url: "/Home/GetElement",
                context: document.body,
                data: {
                    id: id,
                },
                traditional: true,
                success: function (data) {
                    Museum.showElementDesc(data);
                },
                error: function () {

                }
            });
        },

        getPrevElement: function (id) {
            e.elemDesc.slideUp(500);
            $.ajax({
                type: "GET",
                url: "/Home/NextElement",
                context: document.body,
                data: {
                    id: id,
                },
                traditional: true,
                success: function (data) {
                    Museum.showElementDesc(data);
                },
                error: function () {

                }
            });
        },

        getNextElement: function (id) {
            e.elemDesc.slideUp(500);
            $.ajax({
                type: "GET",
                url: "/Home/PrevElement",
                context: document.body,
                data: {
                    id: id,
                },
                traditional: true,
                success: function (data) {
                    Museum.showElementDesc(data);
                },
                error: function () {

                }
            });
        },

        showElementDesc: function (data) {
            e.elemDesc.html(data);
            SEMICOLON.widget.loadFlexSlider();
            e.elemDesc.slideDown(750);
        }
    };
