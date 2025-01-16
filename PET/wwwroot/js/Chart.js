window.updateChart = function (chartId, dates, inflowData, outflowData) {
    var ctx = document.getElementById(chartId).getContext('2d');

    if (Chart.getChart(ctx)) {
        chart.destroy(); // Destroy the current chart instance
    }

    var chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: dates,  // Use exact transaction dates as labels
            datasets: [
                {
                    label: 'Inflow',
                    data: inflowData,  // Data for inflow over the dates
                    borderColor: '#00B2B2',
                    backgroundColor: '#00B2B2',
                    lineTension: 0.3,  
                    borderWidth: 2,  
                    pointRadius: 3, 
                    pointBackgroundColor: '#00B2B2',
                    pointBorderWidth: 2
                },
                {
                    label: 'Outflow',
                    data: outflowData,  // Data for outflow over the dates
                    borderColor: '#FF0000',
                    backgroundColor: '#FF0000',
                    fill: false,
                    lineTension: 0.3,
                    borderWidth: 2,
                    pointRadius: 3,
                    pointBackgroundColor: '#FF6384',
                    pointBorderWidth: 2
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    autoSkip: true,
                    maxTicksLimit: 10
                },
                x: {
                    ticks: {
                        autoSkip: true,
                        maxTicksLimit: 10,
                        minRotation: 45,
                        maxRotation: 90
                    }
                }
            },
            plugins: {
                legend: {
                    position: 'top',
                    labels: {
                        font: {
                            size: 14
                        }
                    }
                }
            }
        }
    });
};
