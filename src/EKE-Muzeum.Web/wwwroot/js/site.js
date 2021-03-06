﻿var s, g, e,
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
            elemLoader: $(".page-title-loading"),
            elemBottomLoader: $(".page-bottom-loading")
        },

        initPage: function () {
            s = this.settings;
            e = this.elem;
            this.init();
            this.initMenuBar();
            this.bindUIActions();
            this.initCookies();
            this.initModal();
        },

        init: function () {
            window.localStorage.setItem("scrollable", true);
            $(window).load(function () {
                SEMICOLON.documentOnResize.init();
            });
            $(window).scroll(Museum.scrollToAjax);
        },

        initMenuBar: function () {
            $.ajax({
                type: "GET",
                url: "/Home/GetCategoriesMenu",
                context: document.body,
                traditional: true,
                success: function (data) {
                    $(".primary-menu").html(data);
                    SEMICOLON.header.init();
                    Museum.unbindUIActions();
                    Museum.bindUIActions();
                },
                error: function () {

                }
            });
        },

        initCookies: function () {
            $.cookieBar({ message: 'Oldalainkon HTTP-sütiket használunk a jobb működésért. Elfogadom ezek használatát.', acceptText: 'Rendben', bottom: true, fixed: true, expireDays: 1 })
        },

        initModal: function () {
            $("#initModal").iziModal({
                title: 'Megragadni a pillanatot',
                subtitle: '',
                headerColor: '#C0B480',
                borderBottom: true,
                padding: 25,
                
            });

            $('#initModal').iziModal('open');
        },

        unbindUIActions: function () {
            $("#search").off();
            $("#search2").off();
            $(".elemTitle").off();
            $("#primary-menu div").off();
        },

        bindUIActions: function () {
            $("#search").keyup(function () {
                if ($(this).val().length > 2) {
                    Museum.search($(this).val());
                }
            });

            $("#search2").keyup(function () {
                if ($(this).val().length > 2) {
                    Museum.search($(this).val());
                }
            });

            $(".elemTitle").click(function () {
                Museum.getElement($(this).data("id"));
            });

            $("#primary-menu div").click(function () {
                Museum.getElementsByCategory($(this).text());
            })
        },

        closeElemDesc: function (id) {
            e.elemDesc.slideUp(500);
            if (id != undefined) {
                setTimeout(function () { Museum.goTo($("a.elemTitle[data-id=" + id + "]")); }, 600);
            }

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
            var page = parseInt(s.scrollPosition.data("page"), 10);
            var keyword = s.scrollPosition.data("keyword");

            if (page % 5 === 0 && page != 0) {
                $(".appendResult .portfolio-item").slice(0, 48).remove();
                SEMICOLON.documentOnResize.init();
            }

            Museum.showElemBottomLoading();

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
                    if (data != "") {
                        s.scrollPosition.data('page', parseInt(page + 1));
                        s.appendResult.append(data);

                        Museum.unbindUIActions();
                        Museum.bindUIActions();
                        Museum.hideElemBottomLoading();

                        SEMICOLON.documentOnResize.init();
                        SEMICOLON.widget.loadFlexSlider();
                    } else {
                        $(".page-bottom-loading h4").text("Az összes elem betöltődött!");
                    }
                    setTimeout(function () { $(window).on("scroll", Museum.scrollToAjax) }, 1500);
                },
                error: function () {

                }
            });
            SEMICOLON.documentOnResize.init();
        },

        search: function (keyword) {
            s.scrollPosition.data("keyword", keyword);
            s.scrollPosition.data("page", 0);
            s.scrollPosition.data("category", "");

            Museum.showElemLoading();

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

                    Museum.closeElemDesc();
                    Museum.hideElemLoading();
                    Museum.unbindUIActions();
                    Museum.bindUIActions();

                    SEMICOLON.documentOnResize.init();
                    SEMICOLON.widget.loadFlexSlider();
                },
                error: function () {

                }
            });

            SEMICOLON.documentOnResize.init();
        },

        getElementsByCategory: function (category) {
            s.scrollPosition.data("keyword", "");
            s.scrollPosition.data("page", 1);
            s.scrollPosition.data("category", category);

            Museum.showElemLoading();
            Museum.closeElemDesc();

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

                    Museum.hideElemLoading();
                    Museum.unbindUIActions();
                    Museum.bindUIActions();

                    SEMICOLON.widget.loadFlexSlider();
                    SEMICOLON.documentOnResize.init();
                },
                error: function () {

                }
            });
            SEMICOLON.documentOnResize.init();
        },

        getElement: function (id) {
            $('html, body').animate({ scrollTop: 0 }, 'slow');
            e.elemDesc.slideUp(500);
            Museum.showElemLoading();
            $.ajax({
                type: "GET",
                url: "/Home/GetElement",
                context: document.body,
                data: {
                    id: id,
                },
                traditional: true,
                success: function (data) {
                    Museum.hideElemLoading();
                    Museum.showElementDesc(data);
                },
                error: function () {

                }
            });
        },

        getPrevElement: function (id) {
            e.elemDesc.slideUp(500);
            Museum.showElemLoading();
            $.ajax({
                type: "GET",
                url: "/Home/PrevElement",
                context: document.body,
                data: {
                    id: id,
                },
                traditional: true,
                success: function (data) {
                    Museum.hideElemLoading();
                    Museum.showElementDesc(data);
                },
                error: function () {

                }
            });
        },

        getNextElement: function (id) {
            e.elemDesc.slideUp(500);
            Museum.showElemLoading();
            $.ajax({
                type: "GET",
                url: "/Home/NextElement",
                context: document.body,
                data: {
                    id: id,
                },
                traditional: true,
                success: function (data) {
                    Museum.hideElemLoading();
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
        },

        showElemLoading: function () {
            e.elemLoader.slideDown(500);
        },

        hideElemLoading: function () {
            e.elemLoader.slideUp(500);
        },

        showElemBottomLoading: function () {
            e.elemBottomLoader.slideDown(250);
        },

        hideElemBottomLoading: function () {
            e.elemBottomLoader.slideUp(250);
        },

        goTo: function (obj) {
            $('html, body').animate({
                scrollTop: obj.offset().top - 200 + 'px'
            }, 'slow');
        }
    };
