$(document).ready(function () {
    checkForNotifications();
});

$(document).on('click', function (e) {
    $('[data-toggle="popover"],[data-original-title]').each(function () {
        //the 'is' for buttons that trigger popups
        //the 'has' for icons within a button that triggers a popup
        if (!$(this).is(e.target) && $(this).has(e.target).length === 0 && $('.popover').has(e.target).length === 0) {
            (($(this).popover('hide').data('bs.popover') || {}).inState || {}).click = false;  // fix for BS 3.3.6
        }
    });
});

function checkForNotifications() {
    $.getJSON("/api/MedicineTakenApi/GetNotifications", function (notifications) {
        if (notifications.length === 0) {
            $(".js-notifications-count").addClass("hide");
            return;
        }

        $(".js-notifications-count")
            .text(notifications.length)
            .removeClass("hide")
            .addClass("animated bounceInDown");

        $(".notifications").popover({
            html: true,
            title: "Habit/Medicine Notifications",
            content: buildNotificationList(notifications),
            placement: "bottom",
            template: '<div class="popover popover-notifications" role="tooltip"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
        });
    });
}

function buildNotificationList(notifications) {
    var html = "";
    for (var i = 0; i < notifications.length; i++) {
        html += "<li><span class='highlight'>" + notifications[i].MedicineName + "</span> has not been completed yet. " +
            notifications[i].NumberCompleted + " of " + notifications[i].NumberNeeded + " completed.</li>";
    }

    return html;
}