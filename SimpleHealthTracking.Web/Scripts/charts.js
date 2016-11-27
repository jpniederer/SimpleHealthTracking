var sleepTimesJson, checkinWeightsJson, checkinHeartsJson;

function setupDataSetsHome() {
    $.get("api/CheckinApi/GetLastCheckinsForWeights?count=30", function (d) {
        checkinWeightsJson = d;
    })
    .done(function () {
        setupWeightGraphHome();
    });
    $.get("api/CheckinApi/GetLastCheckinsForHeartrates?count=30", function (d) {
        checkinHeartratesJson = d;
    })
    .done(function () {
        setupHeartrateGraphHome();
    });
    $.get("api/SleepApi/GetLastFullSleeps?count=30", function (d) {
        sleepTimesJson = d;
    })
    .done(function () {
        setupSleepGraphHome();
    });
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

function setupSleepGraphHome() {
    var sleeps = setupSleeps();
    var parseTime = d3.timeParse("%Y-%m-%dT%H:%M:%S");
    var weight = d3.select("#sleepChart"),
        margins = { top: 20, right: 20, bottom: 20, left: 50 },
        width = 400,
        height = 400,
        xRange = d3.scaleTime().range([margins.left, width - margins.right]).domain([
            d3.min(sleeps, function (d) {
                return parseTime(d.x);
            }),
            d3.max(sleeps, function (d) {
                return parseTime(d.x);
            })
        ]),
        yRange = d3.scaleLinear().range([height - margins.top, margins.bottom]).domain([
            d3.min(sleeps, function (d) {
                return d.y - 0.1;
            }),
            d3.max(sleeps, function (d) {
                return d.y + 0.1;
            })
        ]),
        xAxis = d3.axisBottom(xRange)
                .ticks(5)
                .tickFormat(d3.timeFormat("%m/%d")),
        yAxis = d3.axisLeft(yRange);


    weight.append("svg:g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + (height - margins.bottom) + ")")
        .call(xAxis);

    weight.append("svg:g")
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

    weight.append('svg:path')
        .attr('d', lineFunc(sleeps))
        .attr('stroke', 'blue')
        .attr('stroke-width', 2)
        .attr('fill', 'none')
        .text("Recent Heartrates")
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

function setupWeightGraphHome() {
    var weights = setupWeights();
    // d3 converts local time to GMT.
    var parseTime = d3.timeParse("%Y-%m-%dT%H:%M:%S");
    var weight = d3.select("#weightChart"),
        margins = { top: 20, right: 20, bottom: 20, left: 50 },
        width = 400,
        height = 400,
        xRange = d3.scaleTime().range([margins.left, width - margins.right]).domain([
            d3.min(weights, function (d) {
                return parseTime(d.x);
            }),
            d3.max(weights, function (d) {
                return parseTime(d.x);
            })
        ]),
        yRange = d3.scaleLinear().range([height - margins.top, margins.bottom]).domain([
            d3.min(weights, function (d) {
                return d.y - 1;
            }),
            d3.max(weights, function (d) {
                return d.y + 1;
            })
        ]),
        xAxis = d3.axisBottom(xRange)
                .ticks(5)
                .tickFormat(d3.timeFormat("%m/%d")),
        yAxis = d3.axisLeft(yRange);

    weight.append("svg:g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + (height - margins.bottom) + ")")
        .call(xAxis);

    weight.append("svg:g")
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

    weight.append('svg:path')
        .attr('d', lineFunc(weights))
        .attr('stroke', 'blue')
        .attr('stroke-width', 2)
        .attr('fill', 'none')
        .text("Recent Heartrates")
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

function setupHeartrateGraphHome() {
    var heartrates = setupHeartrates();
    // d3 converts local time to GMT.
    var parseTime = d3.timeParse("%Y-%m-%dT%H:%M:%S");
    var heart = d3.select("#heartChart"),
        margins = { top: 20, right: 20, bottom: 20, left: 50 },
        width = 400, //+.attr("width") - margin.left - margin.right,
        height = 400, //+svg.attr("height") - margin.top - margin.bottom,
        //g = svg.append("g").attr("transform", "translate(" + margin.left + ", " + margin.top + ")");
        xRange = d3.scaleTime().range([margins.left, width - margins.right]).domain([
            d3.min(heartrates, function (d) {
                return parseTime(d.x);
            }),
            d3.max(heartrates, function (d) {
                return parseTime(d.x);
            })
        ]),
        yRange = d3.scaleLinear().range([height - margins.top, margins.bottom]).domain([
            d3.min(heartrates, function (d) {
                return d.y - 5;
            }),
            d3.max(heartrates, function (d) {
                return d.y + 5;
            })
        ]),
        xAxis = d3.axisBottom(xRange)
                .ticks(5)
                .tickFormat(d3.timeFormat("%m/%d")),
        yAxis = d3.axisLeft(yRange);

    heart.append("svg:g")
        .attr("class", "x axis")
        .attr("transform", "translate(0," + (height - margins.bottom) + ")")
        .call(xAxis);

    heart.append("svg:g")
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

    heart.append('svg:path')
        .attr('d', lineFunc(heartrates))
        .attr('stroke', 'blue')
        .attr('stroke-width', 2)
        .attr('fill', 'none')
        .text("Recent Heartrates")
};