var webServiceJsonResult;
var maxInt = 2147483647;

function setupDataSetsHome() {
    $.get("api/CheckinApi/GetLastCheckinsForWeights?count=30", function (d) {
        webServiceJsonResult = d;
    })
    .done(function () {
        homeGraphGeneral(setupWeights, "#weightChart", 1, '', 400, 400);
    });
    $.get("api/CheckinApi/GetLastCheckinsForHeartrates?count=30", function (d) {
        webServiceJsonResult = d;
    })
    .done(function () {
        homeGraphGeneral(setupHeartrates, "#heartChart", 5, '', 400, 400);
    });
    $.get("api/SleepApi/GetLastFullSleeps?count=30", function (d) {
        webServiceJsonResult = d;
    })
    .done(function () {
        homeGraphGeneral(setupSleeps, "#sleepChart", 0.1, '', 400, 400);
    });
}

function setupFullHeartrateChart() {
    $.get("/api/CheckinApi/GetLastCheckinsForHeartrates?count=" + maxInt, function (d) {
        webServiceJsonResult = d;
    })
    .done(function () {
        homeGraphGeneral(setupHeartrates, "#heartChart", 5, '', 1200, 800);
    });
}

function setupFullWeightChart() {
    $.get("/api/CheckinApi/GetLastCheckinsForWeights?count=" + maxInt, function (d) {
        webServiceJsonResult = d;
    })
    .done(function () {
        homeGraphGeneral(setupWeights, "#weightChart", 1, '', 1200, 800);
    });
}

function setupFullSleepChart() {
    $.get("/api/SleepApi/GetLastFullSleeps?count=" + maxInt, function (d) {
        webServiceJsonResult = d;
    })
    .done(function () {
        homeGraphGeneral(setupSleeps, "#sleepChart", 0.1, '', 1200, 800);
    });
}

function updateChart(webService, setupFunction, idName, scaleModifier, name) {
    $.get(webService, function (d) {
        webServiceJsonResult = d;
    })
    .done(function () {
        deletePreviousChart(idName);
        homeGraphGeneral(setupFunction, idName, scaleModifier, name, 400, 400);
    });
}

function deletePreviousChart(idName) {
    var svg = d3.select(idName);
    svg.selectAll("*").remove();
}

function parseTime(dateString) {
    var pt1 = d3.timeParse("%Y-%m-%dT%H:%M:%S");

    if (pt1(dateString) !== null) {
        return pt1(dateString);
    } else {
        var pt2 = d3.timeParse("%Y-%m-%dT%H:%M:%S.%L");
        return pt2(dateString);
    }
}

function homeGraphGeneral(setupFunction, idName, scaleModifier, name, width, height) {
    var items = setupFunction();
    var graph = d3.select(idName),
        margins = { top: 20, right: 20, bottom: 20, left: 50 },
        // d3 converts local time to GMT.
        xRange = d3.scaleTime().range([margins.left, width - margins.right]).domain([
            d3.min(items, function (d) {
                return parseTime(d.x);
            }),
            d3.max(items, function (d) {
                return parseTime(d.x);
            })
        ]),
        yRange = d3.scaleLinear().range([height - margins.top, margins.bottom]).domain([
            d3.min(items, function (d) {
                return d.y - scaleModifier;
            }),
            d3.max(items, function (d) {
                return d.y + scaleModifier;
            })
        ]),
        xAxis = d3.axisBottom(xRange)
                .ticks(5)
                .tickFormat(d3.timeFormat("%m/%d")),
        yAxis = d3.axisLeft(yRange);

    graph.append("svg:g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + (height - margins.bottom) + ")")
        .call(xAxis);

    graph.append("svg:g")
      .attr("class", "y axis")
      .attr("transform", "translate(" + margins.left + ",0)")
      .call(yAxis);

    var lineFunc = d3.line()
        .x(function (d) {
            return xRange(parseTime(d.x));
        })
        .y(function (d) {
            return yRange(d.y);
        });

    graph.append('svg:path')
        .attr('d', lineFunc(items))
        .attr('stroke', 'blue')
        .attr('stroke-width', 2)
        .attr('fill', 'none')
        .text(name);
}

function setupSleeps() {
    var sleeps = webServiceJsonResult.map(function (item) {
        return {
            x: item.StartTime,
            y: item.MinutesSlept / 60
        };
    });

    return sleeps;
}

function setupWeights() {
    var weights = webServiceJsonResult.map(function (item) {
        return {
            x: item.TimeAdded,
            y: item.Weight
        };
    });

    return weights;
}

function setupHeartrates() {
    var heartrates = webServiceJsonResult.map(function (item) {
        return {
            x: item.TimeAdded,
            y: item.Heartrate
        };
    });

    return heartrates;
}