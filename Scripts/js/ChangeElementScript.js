function getSelectionText() {
    var text = "";
    if (window.getSelection) {
        text = window.getSelection().toString();
    }
    else if (document.getSelection) {
        text = document.getSelection();
    }
    else if (document.selection) {
        text = document.selection.createRange().text;
    }
    return $.trim(text);

}

function changeElem(elem) {
    var newText = getSelectionText();
    if (newText !== null) {
        if (elem.innerHTML !== newText) {
            elem.innerHTML = newText;
            if (elem.id === 'nameLb') document.getElementById("topFirst").innerHTML = newText;
            if (elem.id === 'prefixLb') document.getElementById("topPrefix").innerHTML = " " + newText;
            if (elem.id === 'surnameLb') document.getElementById("topSur").innerHTML = " " + newText;
        }
    } else {
        if (elem.id === 'nameLb') document.getElementById("topFirst").innerHTML = "";
        if (elem.id === 'prefixLb') document.getElementById("topPrefix").innerHTML = "";
        if (elem.id === 'surnameLb') document.getElementById("topSur").innerHTML = "";
    }
}

function changeText(id) {
    var newText = getSelectionText();

    if (newText !== "" && newText !== null) {
        $('#' + id).val(newText);
    }
}