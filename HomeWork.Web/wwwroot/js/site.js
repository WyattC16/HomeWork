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
        data: json
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
        for (var j = 1; j < homeWorks.length; j++) {
            json += '{';
            json += '"DueDate": "' + homeWorks[j].getElementsByTagName("input")[0].value  + '",';
            json += '"Status": ' + homeWorks[j].getElementsByTagName("select")[0].selectedIndex + ',';
            json += '"Title": "' + homeWorks[j].getElementsByTagName("td")[0].textContent + '"'
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
}