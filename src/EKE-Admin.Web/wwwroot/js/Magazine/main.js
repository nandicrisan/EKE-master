var s,
Magazine = {

    settings: {
        numArticles: 5,
        mName: $("#mName"),
        mYear: $("#mYear"),
        mNumber: $("#mNumber"),
    },

    init: function () {
        s = this.settings;
        this.bindUIActions();
    },

    bindUIActions: function () {
        s.mName.on("click", function () {
            Magazine.getMYears($(this).data("id"));
        });

        s.mYear.on("click", function () {

        });

        s.mNumber.on("click", function () {

        });
    },

    getMYears: function (numToGet) {
        // $.ajax or something
        // using numToGet as param
    }

};