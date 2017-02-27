$(document).ready(function () {

    $("#gpssearchbtn").on("click", function () {
        loadPeriod();
    });

});

function loadPeriod() {
    var FromPeriod = $('#fromPeriod').val();
    var ToPeriod = $('#toPeriod').val();

    var loadPeriod = 'GetPeriodList';
    //if ($("#GPSSummery").validate()) {
    $.ajax({
        url: loadPeriod,
        data: {
            FromPeriod: FromPeriod,
            ToPeriod: ToPeriod,
        },
        type: "GET",
        success: function (result) {
            debugger;
            var list = [];
            if (result.length > 0) {
                for (var i = 0; i < result.length; i++) {
                    var input = $(result[i].Text)
                    list.push("<li>" + input.selector + "</li>")
                }
                $("#periodList").html(list)
            }
        },
        error: function () {
            alert("error occured");
        }

    });
    //}
}