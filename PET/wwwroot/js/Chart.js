window.updateChart = function (chartId, dates, inflowData, outflowData) {
    var ctx = document.getElementById(chartId).getContext('2d');

    // Destroy the existing chart
    var existingChart = Chart.getChart(ctx); 
    if (existingChart) {
        existingChart.destroy(); 
    }

    var chart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: dates,
            datasets: [
                {
                    label: 'Inflow',
                    data: inflowData,
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
                    data: outflowData,
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
