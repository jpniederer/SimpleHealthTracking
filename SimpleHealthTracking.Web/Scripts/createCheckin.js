// Will be used to call the Web API function to create a checkin from the home page.
$(document).ready(function () {
    $(".create-checkin").click(function (e) {
        var button = $(e.target);
        var weightTextBox = document.getElementById("Weight");
        var heartRateTextBox = document.getElementById("Heartrate");
        $.post("/api/CheckinApi", { Weight: weightTextBox.value, HeartRate: heartRateTextBox.value})
            .done(function () {
                //alert("Checkin added.");
                showCheckinAdded();
                weightTextBox.value = null;
                heartRateTextBox.value = null;
            })
            .fail(function () {
                alert("Could not create the checkin.");
            });
    });
})

function showCheckinAdded() {
    var checkinLabel = document.getElementById("CheckinAddedText");
    checkinLabel.visible = true;
    checkinLabel.innerText = "New Checkin Added";
}