var isCounting = false;
var interval;
var button = document.getElementById('timer');
var seconds;
var temp;

function timerClick() {
    if (isCounting) {
        stopTimer();
        isCounting = false;
        seconds = 0;
        timer.innerHTML = "Start Timer";
    } else {
        temp = document.getElementById('counter');
        temp.innerHTML = 0;
        isCounting = true;
        interval = setInterval(function () { startTimer(); }, 1000);
        timer.innerHTML = "Stop Timer";
    }
}

function startTimer() {
    seconds = document.getElementById('counter').innerHTML;
    seconds = parseInt(seconds, 10);

    seconds++;
    temp = document.getElementById('counter');
    temp.innerHTML = seconds;
}

function stopTimer() {
    clearInterval(interval);
}