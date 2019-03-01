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
    console.log(newText);
    console.log(elem.id);
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
        //no selected text so reset
        alert('no selected text');
        elem.innerHTML = "leeg";
    }
}

function changeText(id) {
    var newText = getSelectionText();
    console.log("selected text = " + "'" + newText + "'!");
    console.log(id);

    if (newText !== "" && newText !== null) {
        $('#' + id).val(newText);
    }
}

//$(function () {
//    $('#btnName').on('click', function () {
//        var text = $('#Name');
//        text.val(getSelectionText());
//    });
//    $('#btnPrefix').on('click', function () {
//        var text = $('#Prefix');
//        text.val(getSelectionText());
//    });
//    $('#btnSurname').on('click', function () {
//        var text = $('#Surname');
//        text.val(getSelectionText());
//    });
//    $('#btnResidence').on('click', function () {
//        var text = $('#Residence');
//        text.val(getSelectionText());
//    });
//    $('#btnCountry').on('click', function () {
//        var text = $('#Country');
//        text.val(getSelectionText());
//    });
//    $('#btnDob').on('click', function () {
//        var text = $('#DateOfBirth');
//        text.val(getSelectionText());
//    });
//    $('#btnLicense').on('click', function () {
//        var text = $('#LicenseCategory');
//        text.val(getSelectionText());
//    });
//    $('#btnProfile').on('click', function () {
//        var text = $('#Profile');
//        text.val(getSelectionText());
//    });

//});