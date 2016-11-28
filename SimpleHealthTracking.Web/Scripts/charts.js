var sleepTimesJson, checkinWeightsJson, checkinHeartsJson;

function setupDataSetsHome() {
    $.get("api/CheckinApi/GetLastCheckinsForWeights?count=30", function (d) {
        checkinWeightsJson = d;
    })
    .done(function () {
        homeGraphGeneral(setupWeights, "#weightChart", 1, '');
    });
    $.get("api/CheckinApi/GetLastCheckinsForHeartrates?count=30", function (d) {
        checkinHeartratesJson = d;
    })
    .done(function () {
        homeGraphGeneral(setupHeartrates, "#heartChart", 5, '');
    });
    $.get("api/SleepApi/GetLastFullSleeps?count=30", function (d) {
        sleepTimesJson = d;
    })
    .done(function () {
        homeGraphGeneral(setupSleeps, "#sleepChart", 0.1, '');
    });
};

function homeGraphGeneral(setupFunction, idName, scaleModifier, name) {
    var items = setupFunction();
    var parseTime = d3.timeParse("%Y-%m-%dT%H:%M:%S");
    var graph = d3.select(idName),
        margins = { top: 20, right: 20, bottom: 20, left: 50 },
        width = 400,
        height = 400,
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
      .attr("transform", "translate(" + (margins.left) + ",0)")
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
        .text(name)
};

function setupSleeps() {
    var sleeps = sleepTimesJson.map(function (item) {
        return {
            x: item.StartTime,
            y: item.MinutesSlept / 60
        };
    });

    return sleeps;
};

function setupWeights() {
    var weights = checkinWeightsJson.map(function (item) {
        return {
            x: item.TimeAdded,
            y: item.Weight
        };
    });

    return weights;
};

function setupHeartrates() {
    var heartrates = checkinHeartratesJson.map(function (item) {
        return {
            x: item.TimeAdded,
            y: item.Heartrate
        };
    });

    return heartrates;
};