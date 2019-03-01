$(document).ready(function () {
    $("#chooseBtn").on("click", function () {
        var el = $(this);
        var x = document.getElementById("pdfChoice");
        var y = document.getElementById("textChoice");
        if (el.text() === el.data("text-swap")) {
            el.text(el.data("text-original"));
            x.style.display = "block";
            y.style.display = "none";
        } else {
            el.data("text-original", el.text());
            el.text(el.data("text-swap"));
            x.style.display = "none";
            y.style.display = "block";
        }
    });

    $('#textChoice').mouseup(function () {
        var text = getSelectedText();
        if (text != '') result.innerHTML = text;
    });

    function getSelectedText() {
        if (window.getSelection) {
            return window.getSelection().toString();
        } else if (document.selection) {
            return document.selection.createRange().text;
        }
        return '';
    }
});