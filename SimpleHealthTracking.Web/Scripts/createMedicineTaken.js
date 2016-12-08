$(document).ready(function () {
    $(".create-medicineTaken").click(function (e) {
        var button = $(e.target);
        var medicineName = document.getElementById("MedicineId");
        var dateTextBox = document.getElementById("DateAddedFor");
        var timeTextBox = document.getElementById("TimeAddedFor");
        if (dateTextBox.value !== "" && timeTextBox.value !== "") {
            $.post("/api/MedicineTakenApi/AddMedicineTaken", {
                MedicineId: medicineName.value, DateAddedFor: dateTextBox.value,
                TimeAddedFor: timeTextBox.value})
            .done(function () {
                showMedicineAdded();
                checkForNotifications();
                dateTextBox.value = null;
                timeTextBox.value = null;
            })
            .fail(function () {
                alert("Could not create the habit/medicine taken entry.");
            });
        } else {
            alert("You must add a date and time to create a habit/medicine taken entry.");
        }
    });
});

function showMedicineAdded() {
    var medicineTakenText = document.getElementById("MedicineTakenAddedText");
    medicineTakenText.visible = true;
    medicineTakenText.innerText = "New Habit/Medicine Taken Added";
}