function saveJson(body) {
    var json;
    if (typeof body === 'string') {
        json = body;
    } else {
        json = JSON.stringify(body)
    }
    $.ajax({
        url: '/',
        type: 'PUT',
        contentType: 'application/json',
        data: json,
        async: false
    });
}

function saveSemester() {
    var classTables = document.getElementById('Classes').getElementsByTagName("table");
    var json = '{';
    json += '"Classes": [';
    for (var i = 0; i < classTables.length; i++) {
        json += '{';
        json += '"HomeWorks": [';
        var homeWorks = classTables[i].getElementsByTagName("tr");
        for (var j = 1; j < homeWorks.length - 1; j++) {
            json += '{';
            json += '"DueDate": "' + homeWorks[j].getElementsByTagName("input")[0].value  + '",';
            json += '"Status": ' + homeWorks[j].getElementsByTagName("select")[0].selectedIndex + ',';
            json += '"Title": "' + homeWorks[j].getElementsByTagName("td")[1].textContent + '"'
            json += '},';
        }
        json += '],';
        json += '"Title": "' + classTables[i].getElementsByTagName("th")[0].textContent + '"';
        json += '},';
    }
    json += '],';
    json += '"Season": "' + document.getElementById('Season').value + '",';
    json += '"Year": ' + document.getElementById('Year').value;
    json += '}';
    saveJson(json);
    window.location.reload(false);
}

function deleteRow(button) {
    var row = button.parentElement.parentElement;
    row.parentElement.removeChild(row);
}

function addNewRow(button) {
    var row = button.parentElement.parentElement;
    var tbody = row.parentElement;
    var index = tbody.getElementsByTagName('tr').length;
    var newRow = tbody.insertRow(index - 1);
    newRow.setAttribute('style', 'border: 1px solid black; background: white;');
    var td0 = newRow.insertCell(0);
    td0.setAttribute('width', '100%');
    td0.setAttribute('style', 'border: 1px solid black;');
    var buttonDelete = document.createElement('button');
    buttonDelete.setAttribute('onclick','deleteRow(this)');
    td0.appendChild(buttonDelete);
    var td1 = newRow.insertCell(1);
    td1.setAttribute('contenteditable','true');
    td1.setAttribute('width', '100%');
    td1.setAttribute('style','border: 1px solid black;')
    var td2 = newRow.insertCell(2);
    td2.setAttribute('width', '100%');
    td2.setAttribute('style', 'border: 1px solid black;');
    var dueDate = document.createElement('input');
    dueDate.setAttribute('type', 'date');
    dueDate.setAttribute('style', 'border: none; background:white;');
    dueDate.setAttribute('value','1901-01-01');
    td2.appendChild(dueDate);
    var td3 = newRow.insertCell(3);
    td3.setAttribute('width', '100%');
    td3.setAttribute('style', 'border: 1px solid black;')
    var status = document.createElement('select');
    status.setAttribute('style', 'border: none; background:white;');
    var notDone = document.createElement('option');
    notDone.setAttribute('selected', 'selected');
    notDone.text = 'Not Done';
    status.appendChild(notDone);
    var readyToSubmit = document.createElement('option');
    readyToSubmit.text = 'Ready To Submit';
    status.appendChild(readyToSubmit);
    var submitted = document.createElement('option');
    submitted.text = 'Submitted';
    status.appendChild(submitted);
    td3.appendChild(status);
}