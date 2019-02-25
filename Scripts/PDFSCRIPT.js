//var __PDF_DOC,
//    __CURRENT_PAGE,
//    __TOTAL_PAGES,
//    __PAGE_RENDERING_IN_PROGRESS = 0,
//    __CANVAS = $('#pdf-canvas').get(0),
//    __CANVAS_CTX = __CANVAS.getContext('2d');

//// Initialize and load the PDF
//function showPDF(pdf_url) {
//    console.log(pdf_url);
//    // Show the pdf loader
//    $("#pdf-loader").show();

//    PDFJS.getDocument({ url: pdf_url }).then(function (pdf_doc) {
//        __PDF_DOC = pdf_doc;
//        __TOTAL_PAGES = __PDF_DOC.numPages;

//        // Hide the pdf loader and show pdf container in HTML
//        $("#pdf-loader").hide();
//        $("#pdf-contents").show();
//        $("#pdf-total-pages").text(__TOTAL_PAGES);

//        // Show the first page
//        showPage(1);
//    }).catch(function (error) {
//        // If error re-show the upload button
//        $("#pdf-loader").hide();
//        $("#upload-button").show();

//        alert(error.message);
//    });;
//}

//// Load and render a specific page of the PDF
//function showPage(page_no) {
//    console.log('igottoshowpage');
//    __PAGE_RENDERING_IN_PROGRESS = 1;
//    __CURRENT_PAGE = page_no;

//    // Disable Prev & Next buttons while page is being loaded
//    $("#pdf-next, #pdf-prev").attr('disabled', 'disabled');

//    // While page is being rendered hide the canvas and show a loading message
//    $("#pdf-canvas").hide();
//    $("#page-loader").show();

//    // Update current page in HTML
//    $("#pdf-current-page").text(page_no);

//    // Fetch the page
//    __PDF_DOC.getPage(page_no).then(function (page) {
//        // As the canvas is of a fixed width we need to set the scale of the viewport accordingly
//        var scale_required = __CANVAS.width / page.getViewport(1).width;

//        // Get viewport of the page at required scale
//        var viewport = page.getViewport(scale_required);

//        // Set canvas height
//        __CANVAS.height = viewport.height;

//        var renderContext = {
//            canvasContext: __CANVAS_CTX,
//            viewport: viewport
//        };

//        // Render the page contents in the canvas
//        page.render(renderContext).then(function () {
//            __PAGE_RENDERING_IN_PROGRESS = 0;

//            // Re-enable Prev & Next buttons
//            $("#pdf-next, #pdf-prev").removeAttr('disabled');

//            // Show the canvas and hide the page loader
//            $("#pdf-canvas").show();
//            $("#page-loader").hide();
//        });
//    });
//}

//$(document).ready(function () {
//    // Previous page of the PDF
//    $("#pdf-prev").on('click', function () {
//        if (__CURRENT_PAGE != 1)
//            showPage(--__CURRENT_PAGE);
//    });

//    // Next page of the PDF
//    $("#pdf-next").on('click', function () {
//        if (__CURRENT_PAGE != __TOTAL_PAGES)
//            showPage(++__CURRENT_PAGE);
//    });
//});

function newPDFArrived(url) {
    var x = document.querySelectorAll("#personaliaGroup p");
    console.log('i got to newPDF method');
    var i;
    for (i = 0; i < x.length; i++) {
        x[i].innerHTML = "";
        x[i].textContent = "";
    }

    if (document.getElementById("topName").innerHTML !== "" && document.getElementById("topPrefix").innerHTML !== "" && document.getElementById("topSur").innerHTML !== "") {
        document.getElementById("topFirst").innerHTML = "";
        document.getElementById("topPrefix").innerHTML = "";
        document.getElementById("topSur").innerHTML = "";
    }
    console.log(url);
    showPDF(url);
}


// simple.js
function showPDF(url) {
    console.log('in showPDF, url =' + url);
    var loadingTask = PDFJS.getDocument(url);
    loadingTask.promise.then(
        function (pdf) {
            // Load information from the first page.
            pdf.getPage(1).then(function (page) {
                var scale = 1;
                var viewport = page.getViewport(scale);

                // Apply page dimensions to the <canvas> element.
                var canvas = document.getElementById("viewer");
                var context = canvas.getContext("2d");
                canvas.height = viewport.height;
                canvas.width = viewport.width;

                // Render the page into the <canvas> element.
                var renderContext = {
                    canvasContext: context,
                    viewport: viewport
                };
                page.render(renderContext).then(function () {
                    console.log("Page rendered!");
                });
            });
        },
        function (reason) {
            console.error(reason);
        }
    );
}