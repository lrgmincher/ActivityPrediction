$(document).ready(function () {
    $("#containerForButton").hide();

        $.ajax({
            url: 'https://www.strava.com/oauth/token?client_id=29906&client_secret=07dc460141e2dc25e81c3f1b9bb9bc01efb52a54&code=' + code,
            dataType: 'text',
            type: 'post',
            contentType: 'application/json; charset=UTF-8',
            data: "",
            success: function (data) {
                var accessToken = JSON.parse(data).access_token;
                $("#accessToken").html("Access Token: " + accessToken);
                $("#containerForButton").data('url', buttonPath + "?accessToken=" + accessToken);
                console.log($("#containerForButton").data('url'));
                $("#containerForButton").show();
            },
            error: function () {
                $("#accessToken").html("Error in finding Access Token");
            },
        });
 

    $("#containerForButton").click(function () {
        location.href = $(this).data('url');
    });

});


