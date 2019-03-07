﻿function newPDFArrived(url) {
    console.log("newPDFArrived Reached");
    showPDF(url);
}

function showPDF(url) {
    pdfjsLib.getDocument(url)
        .then(function (pdf) {
            console.log("showpdf reached");
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