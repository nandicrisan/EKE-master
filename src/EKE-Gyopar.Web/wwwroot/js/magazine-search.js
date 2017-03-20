var Search = {};

Search.Search = function () {
    var keyWord = $("#search-keyword").val();
    $("#search-result").html("");
    $("#search-resum").hide();
    $.ajax({
        url: localStorage.siteRoot + "/home/SearchMagazine",
        context: document.body,
        data: {
            Keyword: keyWord,
            PublishYear: $("#search-year").val(),
            PublishSection: $("#search-section").val()
        }
    }).success(function (data) {
        $("#search-resum").show();
        $("#search-keyword-text").html('"' + keyWord + '"');
        $("#result-count").html(data.foundItem);
        $.each(data.result, function (i, article) {
            $("#search-result").append(Search.GetResultRow(article));
        })
    }).error(function () {

    });
}

Search.GetResultRow = function (articleJson) {
    var tmplClone = $("#result-template").clone();
    tmplClone.find(".article-content-part").html(Search.GetContentPart(articleJson.content));
    tmplClone.find(".publish-year").html(articleJson.publishYear);
    tmplClone.find(".publish-section").html("/" + articleJson.publishSection);
    tmplClone.find(".article-author").html(articleJson.authorName);
    tmplClone.find(".article-title").html(articleJson.title);
    return tmplClone.html();
}

Search.GetContentPart = function (content) {
    var keyWord = $("#search-keyword").val();
    if (keyWord.length == 0) {
        return content.substring(0, 250) + "...";
    }
    else {
        var ret = "";
        var sentences = content.split(".");
        $.each(sentences, function (i, sentence) {
            var keyWordIndex = sentence.indexOf(keyWord);
            if (keyWordIndex > 0) {
                ret = sentence.replace(keyWord, "<span class='mark-keyword'>" + keyWord + "</span>") + "</br>";
            }
        });
        return ret;
    }
}