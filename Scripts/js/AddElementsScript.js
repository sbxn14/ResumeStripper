//function addLicense() {
//    var i = document.getElementById("LicenseFields").getElementsByTagName("select").length;

//    var fields = $('#LicenseFields');
    
//    console.log('children: ' + i);

//    var template = '<select class="form-control" data-val="true" data-val-required="Het veld Type is vereist." id="ResultCv_LicenseCategory_' + i + '__Type" name="ResultCv.LicenseCategory[' + i + '].Type"><option value="">Select Type</option><option selected="selected" value="0">None</option><option value="1">A</option><option value="2">A1</option><option value="3">A2</option><option value="4">AM</option><option value="5">B</option><option value="6">BE</option><option value="7">Bplus</option><option value="8">C</option><option value="9">CE</option><option value="10">C1</option><option value="11">C1E</option><option value="12">D</option><option value="13">DE</option><option value="14">D1</option><option value="15">D1E</option><option value="16">T</option></select>';
    
//    $(template).appendTo(fields);
//}

//function addEducation() {
//    var i = document.getElementById("EducationFields").getElementsByClassName("eduRow").length;

//    var fields = $('#EducationFields');

//    console.log('children: ' + i);

//    var template = '<select class="form-control" data-val="true" data-val-required="Het veld Type is vereist." id="ResultCv_LicenseCategory_' + i + '__Type" name="ResultCv.LicenseCategory[' + i + '].Type"><option value="">Select Type</option><option selected="selected" value="0">None</option><option value="1">A</option><option value="2">A1</option><option value="3">A2</option><option value="4">AM</option><option value="5">B</option><option value="6">BE</option><option value="7">Bplus</option><option value="8">C</option><option value="9">CE</option><option value="10">C1</option><option value="11">C1E</option><option value="12">D</option><option value="13">DE</option><option value="14">D1</option><option value="15">D1E</option><option value="16">T</option></select>';

//    $(template).appendTo(fields);
//}

//function addWork() {
//    var parent = document.getElementById('workSection');

//    var totalRows = $('#workSection .row').length;

//    if (totalRows === 1) {
//        //removes old plus button
//        document.getElementById('btnWorkPlus').remove();
//    } else {
//        //removes old plus button
//        document.getElementById('btnWorkPlus' + totalRows).remove();
//    }

//    var row = document.createElement("div");
//    row.setAttribute("class", "row");

//    var column1 = document.createElement("div");
//    column1.setAttribute("class", "col-md-4");

//    var column2 = document.createElement("div");
//    column2.setAttribute("class", "col-md-4");

//    var column3 = document.createElement("div");
//    column3.setAttribute("class", "col-md-4");

//    var group1 = document.createElement("div");
//    group1.setAttribute("class", "buttongroup");

//    var group2 = document.createElement("div");
//    group2.setAttribute("class", "buttongroup");

//    var group3 = document.createElement("div");
//    group3.setAttribute("class", "buttongroup");

//    var group4 = document.createElement("div");
//    group4.setAttribute("class", "buttongroup");

//    var beginWorkBtn = document.createElement("button");
//    //sets id to for example btn1 or btn2
//    beginWorkBtn.setAttribute("id", "btnWorkBegin" + (totalRows + 1));
//    beginWorkBtn.setAttribute("onclick", "javascript: changeElem(workBeginLb)" + (totalRows + 1) + ")");
//    beginWorkBtn.innerHTML = "Set Begindatum";

//    var beginWorkLb = document.createElement("p");
//    beginWorkLb.setAttribute("id", "workBeginLb" + (totalRows + 1));

//    var dividerWorkLb = document.createElement("p");
//    dividerWorkLb.setAttribute("id", "workDividerLb" + (totalRows + 1));
//    dividerWorkLb.innerHTML = " - ";

//    var endWorkBtn = document.createElement("button");
//    //sets id to for example btn1 or btn2
//    endWorkBtn.setAttribute("id", "btnWorkEnd" + (totalRows + 1));
//    endWorkBtn.setAttribute("onclick", "javascript: changeElem(workEndLb)" + (totalRows + 1) + ")");
//    endWorkBtn.innerHTML = "Set Einddatum";

//    var endWorkLb = document.createElement("p");
//    beginWorkLb.setAttribute("id", "workEndLb" + (totalRows + 1));

//    group1.appendChild(beginWorkBtn);
//    group1.appendChild(beginWorkLb);

//    group1.appendChild(dividerWorkLb);

//    group1.appendChild(endWorkBtn);
//    group1.appendChild(endWorkLb);

//    column1.appendChild(group1);

//    var nameWorkBtn = document.createElement("button");
//    //sets id to for example btn1 or btn2
//    nameWorkBtn.setAttribute("id", "btnWorkName" + (totalRows + 1));
//    nameWorkBtn.setAttribute("onclick", "javascript: changeElem(workNameLb)" + (totalRows + 1) + ")");
//    nameWorkBtn.innerHTML = "Set Bedrijfsnaam";

//    var nameWorkLb = document.createElement("p");
//    nameWorkLb.setAttribute("id", "workNameLb" + (totalRows + 1));

//    group2.appendChild(nameWorkBtn);
//    group2.appendChild(nameWorkLb);

//    column2.appendChild(group2);

//    var placeWorkBtn = document.createElement("button");
//    //sets id to for example btn1 or btn2
//    placeWorkBtn.setAttribute("id", "btnWorkPlace" + (totalRows + 1));
//    placeWorkBtn.setAttribute("onclick", "javascript: changeElem(workPlaceLb)" + (totalRows + 1) + ")");
//    placeWorkBtn.innerHTML = "Set Locatie Bedrijf";

//    var placeWorkLb = document.createElement("p");
//    placeWorkLb.setAttribute("id", "workPlaceLb" + (totalRows + 1));

//    group3.appendChild(placeWorkBtn);
//    group3.appendChild(placeWorkLb);

//    column3.appendChild(group3);

//    var jobWorkBtn = document.createElement("button");
//    //sets id to for example btn1 or btn2
//    jobWorkBtn.setAttribute("id", "btnWorkJob" + (totalRows + 1));
//    jobWorkBtn.setAttribute("onclick", "javascript: changeElem(workJobLb)" + (totalRows + 1) + ")");
//    jobWorkBtn.innerHTML = "Set Naam Functie";

//    var jobWorkLb = document.createElement("p");
//    jobWorkLb.setAttribute("id", "workJobLb" + (totalRows + 1));

//    var descriptionWorkBtn = document.createElement("button");
//    //sets id to for example btn1 or btn2
//    descriptionWorkBtn.setAttribute("id", "btnWorkDescription" + (totalRows + 1));
//    descriptionWorkBtn.setAttribute("onclick", "javascript: changeElem(workDescriptionLb" + (totalRows + 1) + ")");
//    descriptionWorkBtn.innerHTML = "Set Taakomschrijving";

//    var descriptionWorkLb = document.createElement("p");
//    descriptionWorkLb.setAttribute("id", "workDescriptionLb" + (totalRows + 1));

//    group4.appendChild(jobWorkBtn);
//    group4.appendChild(jobWorkLb);
//    group4.appendChild(descriptionWorkBtn);
//    group4.appendChild(descriptionWorkLb);

//    var plusWorkBtn = document.createElement("button");
//    //sets id to for example btn1 or btn2
//    plusWorkBtn.setAttribute("id", "btnWorkPlus" + (totalRows + 1));
//    plusWorkBtn.setAttribute("onclick", "javascript: addWork()");
//    plusWorkBtn.innerHTML = "+";

//    row.appendChild(column1);
//    row.appendChild(column2);
//    row.appendChild(column3);
//    row.appendChild(group4);
//    row.appendChild(plusWorkBtn);

//    parent.appendChild(row);
//}

//function addCourse() {

//}

//function addLanguage() {

//}