
function showPDF(url) {
    pdfjsLib.getDocument(url)
        .then(function (pdf) {

            // Get div#container and cache it for later use
            var container = document.getElementById("container");

            // Loop from 1 to total_number_of_pages in PDF document
            for (var i = 1; i <= pdf.numPages; i++) {

                // Get desired page
                pdf.getPage(i).then(function (page) {

                    var scale = 1.1;
                    var viewport = page.getViewport(scale);
                    var div = document.createElement("div");

                    // Set id attribute with page-#{pdf_page_number} format
                    div.setAttribute("id", "page-" + (page.pageIndex + 1));

                    // This will keep positions of child elements as per our needs
                    div.setAttribute("style", "position: relative");

                    // Append div within div#container
                    container.appendChild(div);

                    // Create a new Canvas element
                    var canvas = document.createElement("canvas");

                    // Append Canvas within div#page-#{pdf_page_number}
                    div.appendChild(canvas);

                    var context = canvas.getContext('2d');
                    canvas.height = viewport.height;
                    canvas.width = viewport.width;

                    var renderContext = {
                        canvasContext: context,
                        viewport: viewport
                    };

                    // Render PDF page
                    page.render(renderContext)
                        .then(function () {
                            // Get text-fragments
                            return page.getTextContent();
                        })
                        .then(function (textContent) {
                            // Create div which will hold text-fragments
                            var textLayerDiv = document.createElement("div");

                            // Set it's class to textLayer which have required CSS styles
                            textLayerDiv.setAttribute("class", "textLayer");

                            // Append newly created div in `div#page-#{pdf_page_number}`
                            div.appendChild(textLayerDiv);

                            // Create new instance of TextLayerBuilder class
                            var textLayer = new TextLayerBuilder({
                                textLayerDiv: textLayerDiv,
                                pageIndex: page.pageIndex,
                                viewport: viewport
                            });

                            // Set text-fragments
                            textLayer.setTextContent(textContent);

                            // Render text-fragments
                            textLayer.render();
                        });
                });
            }
        });
}

//let pdfDocument;
//let PAGE_HEIGHT;
//const DEFAULT_SCALE = 1;

//function showPDF(url) {
//    pdfjsLib.getDocument(url).then(pdf => {
//        pdfDocument = pdf;

//        let viewer = document.getElementById('view');
//        for (let i = 0; i < pdf.numPages; i++) {
//            let page = createEmptyPage(i + 1);
//            viewer.appendChild(page);
//        }

//        loadPage(1).then(pdfPage => {
//            let viewport = pdfPage.getViewport(DEFAULT_SCALE);
//            PAGE_HEIGHT = viewport.height;
//            document.body.style.width = `${viewport.width}px`;
//        });
//    });
//    window.addEventListener('scroll', handleWindowScroll);
//}

//function createEmptyPage(num) {
//    let page = document.createElement('div');
//    let canvas = document.createElement('canvas');
//    let wrapper = document.createElement('div');
//    let textLayer = document.createElement('div');

//    page.className = 'page';
//    wrapper.className = 'canvasWrapper';
//    textLayer.className = 'textLayer';

//    page.setAttribute('id', `pageContainer${num}`);
//    page.setAttribute('data-loaded', 'false');
//    page.setAttribute('data-page-number', num);

//    canvas.setAttribute('id', `page${num}`);

//    page.appendChild(wrapper);
//    page.appendChild(textLayer);
//    wrapper.appendChild(canvas);

//    return page;
//}

//function loadPage(pageNum) {
//    return pdfDocument.getPage(pageNum).then(pdfPage => {
//        let page = document.getElementById(`pageContainer${pageNum}`);
//        let canvas = page.querySelector('canvas');
//        let wrapper = page.querySelector('.canvasWrapper');
//        let container = page.querySelector('.textLayer');
//        let canvasContext = canvas.getContext('2d');
//        let viewport = pdfPage.getViewport(DEFAULT_SCALE);

//        canvas.width = viewport.width * 2;
//        canvas.height = viewport.height * 2;
//        page.style.width = `${viewport.width}px`;
//        page.style.height = `${viewport.height}px`;
//        wrapper.style.width = `${viewport.width}px`;
//        wrapper.style.height = `${viewport.height}px`;
//        container.style.width = `${viewport.width}px`;
//        container.style.height = `${viewport.height}px`;

//        pdfPage.render({
//            canvasContext,
//            viewport
//        });

//        pdfPage.getTextContent().then(textContent => {
//            pdfjsLib.renderTextLayer({
//                textContent,
//                container,
//                viewport,
//                textDivs: []
//            });
//        });

//        page.setAttribute('data-loaded', 'true');

//        return pdfPage;
//    });
//}

//function handleWindowScroll() {
//    let visiblePageNum = Math.round(window.scrollY / PAGE_HEIGHT) + 1;
//    let visiblePage = document.querySelector(`.page[data-page-number="${visiblePageNum}"][data-loaded="false"]`);
//    if (visiblePage) {
//        setTimeout(function () {
//            loadPage(visiblePageNum);
//        });
//    }
//}

//var __PDF_DOC,
//    __CURRENT_PAGE,
//    __TOTAL_PAGES,
//    __PAGE_RENDERING_IN_PROGRESS = 0;

//// Initialize and load the PDF
//function showPDF(pdf_url) {
//    // Show the pdf loader
//    $("#pdf-loader").show();

//    pdfjsLib.getDocument({ url: pdf_url }).then(function (pdf_doc) {
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

//        alert(error.message);
//    });
//}

//// Load and render a specific page of the PDF
//function showPage(page_no) {
//    __PAGE_RENDERING_IN_PROGRESS = 1;
//    __CURRENT_PAGE = page_no;

//    // Disable Prev & Next buttons while page is being loaded
//    $("#pdf-next, #pdf-prev").attr('disabled', 'disabled');

//    // While page is being rendered hide the canvas and show a loading message
//    $("#pdfCanvas").hide();
//    $("#page-loader").show();

//    // Update current page in HTML
//    $("#pdf-current-page").text(page_no);

//    // Fetch the page
//    __PDF_DOC.getPage(page_no).then(function (page) {
//        var canv = document.getElementById("pdfCanvas");
//        var context = canv.getContext("2d");

//        // As the canvas is of a fixed width we need to set the scale of the viewport accordingly
//        //var scale_required = canv.width / page.getViewport(1).width;

//        // Get viewport of the page at required scale
//        var viewport = page.getViewport(1);

//        // Set canvas height
//        canv.height = viewport.height;
//        canv.width = viewport.width;

//        var renderContext = {
//            canvasContext: context,
//            viewport: viewport
//        };

//        // Render the page contents in the canvas
//        //page.render(renderContext).then(function () {
//        //    __PAGE_RENDERING_IN_PROGRESS = 0;

//        //    // Re-enable Prev & Next buttons
//        //    $("#pdf-next, #pdf-prev").removeAttr('disabled');

//        //    // Show the canvas and hide the page loader
//        //    $("#pdfCanvas").show();
//        //    $("#page-loader").hide();
//        //});
//        page.render(renderContext).then(function () {
//            __PAGE_RENDERING_IN_PROGRESS = 0;

//            if (__TOTAL_PAGES > 1) {
//                // Re-enable Prev & Next buttons if there is more than 1 page
//                $("#pdf-next, #pdf-prev").removeAttr('disabled');
//            }

//            // Show the canvas and hide the page loader
//            $("#pdfCanvas").show();
//            $("#page-loader").hide();

//            // Return the text contents of the page after the pdf has been rendered in the canvas
//            return page.getTextContent();
//        }).then(function (textContent) {
//            // Get canvas offset
//            var canvas_offset = $("#pdfCanvas").offset();
//            var fixed_left = canvas_offset.left / 2 - 122;
//            var fixed_top = canvas_offset.top / 2 + 53;

//            let container = document.getElementById("text-layer");
//            container.style.width = `${viewport.width}px`;
//            container.style.height = `${viewport.height}px`;

//            // Clear HTML for text layer
//            $("#text-layer").html('');

//            // Assign the CSS created to the text-layer element
//            //$("#text-layer").css({ left: fixed_left + 'px', top: fixed_top + 'px', height: canv.height + 'px', width: canv.width + 'px' });

//            // Pass the data to the method for rendering of text over the pdf canvas.
//            pdfjsLib.renderTextLayer({
//                textContent: textContent,
//                container: container,
//                viewport: viewport,
//                textDivs: []
//            });
//        });
//    });
//}

//    $(document).ready(function () {
//        // Previous page of the PDF
//        $("#pdf-prev").on('click', function () {
//            if (__CURRENT_PAGE !== 1)
//                showPage(--__CURRENT_PAGE);
//        });

//        // Next page of the PDF
//        $("#pdf-next").on('click', function () {
//            if (__CURRENT_PAGE !== __TOTAL_PAGES)
//                showPage(++__CURRENT_PAGE);
//        });
//    });

function newPDFArrived(url) {
    var x = document.querySelectorAll("#personaliaGroup p");
    var i;
    for (i = 0; i < x.length; i++) {
        x[i].innerHTML = "";
        x[i].textContent = "";
    }
    showPDF(url);
}

////function showPDF(url) {
////    pdfjsLib.getDocument(url).then(doc => {
////        console.log("this file has " + doc._pdfInfo.numPages + " pages.");

////        //pdfjs starts counting from 1, NOT 0!!
////        doc.getPage(1).then(page => {
////            var canv = document.getElementById("pdfCanvas");
////            var context = canv.getContext("2d");

////            var viewport = page.getViewport(1.2);
////            canv.width = viewport.width;
////            canv.height = viewport.height;

////            page.render({
////                canvasContext: context,
////                viewport: viewport
////            });
////        });
////    });
////}