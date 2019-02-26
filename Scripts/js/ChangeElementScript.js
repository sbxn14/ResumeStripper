function getSelectionText() {
    var selectedText = ""
    if (window.getSelection) { // all modern browsers and IE9+
        selectedText = window.getSelection().toString()
    }
    return selectedText.replace(/\s/g, '');
}

function changeElem(elem) {
    var newText = getSelectionText();
    console.log(newText);
    console.log(elem.id);
    if (newText !== null) {
        if (elem.innerHTML !== newText) {
            elem.innerHTML = newText;
            if (elem.id == 'nameLb') document.getElementById("topFirst").innerHTML = newText;
            if (elem.id == 'prefixLb') document.getElementById("topPrefix").innerHTML = " " + newText;
            if (elem.id == 'surnameLb') document.getElementById("topSur").innerHTML = " " + newText;
        }
    } else {
        if (elem.id == 'nameLb') document.getElementById("topFirst").innerHTML = "";
        if (elem.id == 'prefixLb') document.getElementById("topPrefix").innerHTML = "";
        if (elem.id == 'surnameLb') document.getElementById("topSur").innerHTML = "";
        //no selected text so reset
        alert('no selected text');
        elem.innerHTML = "leeg";
    }
}