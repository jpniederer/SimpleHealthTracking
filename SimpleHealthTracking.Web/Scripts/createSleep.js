// Will be used to call the Web API function to create a Sleep entry from the home page.
$(document).ready(function () {
    $(".create-sleep").click(function (e) {
        var button = $(e.target);
        var startDateTextBox = document.getElementById("StartDate");
        var startTimeTextBox = document.getElementById("StartTime");
        var endDateTextBox = document.getElementById("EndDate");
        var endTimeTextBox = document.getElementById("EndTime");
        $.post("/api/SleepApi", {
            StartDate: startDateTextBox.value, StartTime: startTimeTextBox.value,
            EndDate: endDateTextBox.value, EndTime: endTimeTextBox.value
        }).done(function () {
            alert("Sleep added.");
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