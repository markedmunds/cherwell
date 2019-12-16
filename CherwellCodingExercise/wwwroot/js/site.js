$(function () {
    getRows();
    getColumns();
    $("#triangle_response").on('click', 'a', function () {
        getTriangleReverseLookup($(this).attr("data"));
    });
});

$("#get_triangle").submit(function (event) {
    event.preventDefault();

    var alert = $("#triangle_response");
    alert.removeClass("collapse.show");
    alert.html('');

    var row = $("#row").val();
    var column = $("#column").val();

    $.getJSON("/api/triangles/coordinates?row=" + row + "&column=" + column, function (data) {
        var coords = $("<a>")
            .attr("href", "javascript:void(0);")
            .attr("data", JSON.stringify(data))
            .text(`Triangle Coords: A(${data.a}); B(${data.b}), C(${data.c})`);
        alert.html(coords)
            .removeClass("alert-danger collapse")
            .addClass("alert-success collapse.show");
    }).fail(function (response) {
        alert.html(`Error calling API: ${response.responseText}`)
            .removeClass("alert-success collapse")
            .addClass("alert-danger collapse.show");
    });
});

$("#clear").click(function (event) {
    $("#row").val('');
    $("#column").val('');
    $("#triangle_response").removeClass("collapse.show").addClass("collapse");
});

function getTriangleReverseLookup(triangle) {
    $.ajax({
        beforeSend: function (xhrObj) {
            xhrObj.setRequestHeader("Content-Type", "application/json");
            xhrObj.setRequestHeader("Accept", "application/json");
        },
        type: "POST",
        url: "/api/triangles/row_and_column",
        data: triangle,
        dataType: "json",
        success: function (data) {
            $("#triangle_response").append($("<p></p>")
                .text(`Triangle Reverse Lookup: ${data};`));
        }
    }).fail(function (response) {
        $("#triangle_response").append($("<p></p>")
            .text(`***Error performing triangle reverse lookup; StatusCode: ${response.status};***`));
    });
}

function getRows() {
    $.getJSON("/api/triangles/rows", function (data) {
        addOptions($("#row"), data);
    }).fail(function (response) {
        addError(`Error acquiring valid rows from the API; StatusCode: ${response.status};`);
        $("#row").attr("disabled", "disabled");
        $("#get_triangle :submit").attr("disabled", "disabled");

    });
}

function getColumns() {
    $.getJSON("/api/triangles/columns", function (data) {
        addOptions($("#column"), data);
    }).fail(function (response) {
        addError(`Error acquiring valid columns from the API; StatusCode: ${response.status};`);
        $("#column").attr("disabled", "disabled");
        $("#get_triangle :submit").attr("disabled", "disabled");
    });
}

function addOptions(select, data) {
    $.each(data, function (index, data) {
        select.append($("<option></option>").attr("value", data).text(data));
    });
}

function addError(message) {
    $("#errors").append($("<div></div>")
        .addClass("alert alert-danger")
        .attr("role", "alert")
        .text(message)
        .fadeIn());
}