function saveSemester(body) {
    $.ajax({
        url: '/',
        type: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(body),
        dataType: 'json'
    });
    //$.put("localhost:44365", { json_string: body });
    
}