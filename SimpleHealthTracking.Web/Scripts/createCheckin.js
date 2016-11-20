// Will be used to call the Web API function to create a checkin from the home page.
$(document).ready(function () {
    $(".create-checkin").click(function (e) {
        var button = $(e.target);
        var weightTextBox = document.getElementById("Weight");
        var heartRateTextBox = document.getElementById("Heartrate");
        if (weightTextBox.value !== "" || heartRateTextBox.value !== "") {
            var time = new Date();
            var timeString = time.toLocaleDateString() + " " + time.toLocaleTimeString();
            $.post("/api/CheckinApi/AddCheckin", {
                Weight: weightTextBox.value, HeartRate: heartRateTextBox.value,
                TimeString: timeString
            })
            .done(function () {
                showCheckinAdded();
                weightTextBox.value = null;
                heartRateTextBox.value = null;
            })
            .fail(function () {
                alert("Could not create the checkin.");
            });
        } else {
            alert("You must add a weight or heartrate to create a checkin.");
        }

    });
});

function showCheckinAdded() {
    var checkinText = document.getElementById("CheckinAddedText");
    checkinText.visible = true;
    checkinText.innerText = "New Checkin Added";
}