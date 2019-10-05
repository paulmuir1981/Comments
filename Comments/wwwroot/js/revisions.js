// on selection of a comment
function Load(commentHeaderId)
{
    // show loading div
    $('#divLoading' + commentHeaderId).show();

    // reset selections
    Reset();

    // highlight comment
    $('#commentRow' + commentHeaderId).addClass("alert-success");

    // toggle slect / hide buttons
    $('#select' + commentHeaderId).hide();
    $('#hide' + commentHeaderId).show();

    // if data not already loaded then load
    if ($('#revisionDiv' + commentHeaderId).children().length == 0)
    {
        $('#revisionDiv' + commentHeaderId).load('/index?handler=RevisionsPartial&commentHeaderId=' + commentHeaderId,
            function (response, status, xhr)
            {
                if (status == "error")
                {
                    $('#revisionDiv' + commentHeaderId).html("Sorry but there was an error: " + xhr.status + " " + xhr.statusText);
                }
            }
        );
    }

    // hide loading div
    $('#divLoading' + commentHeaderId).hide();
    // show div
    $('#revisionRow' + commentHeaderId).show();
}

// reset selections
function Reset()
{
    $("[id^='commentRow']").removeClass("alert-success");
    $("[id^='hide']").hide();
    $("[id^='select']").show();
    $("[id^='revisionRow']").hide();
}