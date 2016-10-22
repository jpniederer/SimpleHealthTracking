// Will be used to call the Web API function to create a Sleep entry from the home page.
$(document).ready(function () {
    $(".create-sleep").click(function (e) {
        var button = $(e.target);
        var startDateTextBox = document.getElementById("StartDateText");
        var startTimeTextBox = document.getElementById("StartTimeText");
        var endDateTextBox = document.getElementById("EndDateText");
        var endTimeTextBox = document.getElementById("EndTimeText");
        $.post("/api/SleepApi", {
            StartDateText: startDateTextBox.value, StartTimeText: startTimeTextBox.value,
            EndDateText: endDateTextBox.value, EndTimeText: endTimeTextBox.value
        }).done(function () {
            showSleepAdded();
            startDateTextBox.value = null;
            startTimeTextBox.value = null;
            endDateTextBox.value = null;
            endTimeTextBox.value = null;
        })
        .fail(function () {
            alert("Could not create the sleep entry.");
        });
    });
})

function showSleepAdded() {
    var sleepText = document.getElementById("SleepAddedText");
    sleepText.visible = true;
    sleepText.innerText = "New Sleep Added";
}