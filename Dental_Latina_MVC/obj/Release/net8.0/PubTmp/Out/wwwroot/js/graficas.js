//CONTADORR UWU 


//grafica de barras para productos clickeados
var barCtx = document.getElementById('barChart').getContext('2d');
var barChart = new Chart(barCtx, {
    type: 'bar',
    data: {
        labels: ['Producto A', 'Producto B', 'Producto C', 'Producto D', 'Producto E'],
        datasets: [{
            label: 'Clics por Producto',
            data: [120, 95, 180, 75, 130],
            backgroundColor: [
                'rgba(255, 99, 132, 0.2)',
                'rgba(54, 162, 235, 0.2)',
                'rgba(255, 206, 86, 0.2)',
                'rgba(75, 192, 192, 0.2)',
                'rgba(153, 102, 255, 0.2)'
            ],
            borderColor: [
                'rgba(255, 99, 132, 1)',
                'rgba(54, 162, 235, 1)',
                'rgba(255, 206, 86, 1)',
                'rgba(75, 192, 192, 1)',
                'rgba(153, 102, 255, 1)'
            ],
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
            y: {
                beginAtZero: true
            }
        },plugins: {
            legend: {
                display: false // Oculta la leyenda
            },
            title: {
                display: false // Oculta el título
            }
        }
    }
});

// Gráfico de dona para productos clickeados
var doughnutCtx = document.getElementById('doughnutChart').getContext('2d');
var doughnutChart = new Chart(doughnutCtx, {
    type: 'doughnut',
    data: {
        labels: ['Producto A', 'Producto B', 'Producto C', 'Producto D', 'Producto E'],
        datasets: [{
            label: 'Clics por Producto',
            data: [120, 95, 180, 75, 130],
            backgroundColor: [
                '#FF6384',
                '#36A2EB',
                '#FFCE56',
                '#4BC0C0',
                '#9966FF'
            ],
            hoverOffset: 10
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Productos Más Clickeados'
            }
        }
    }
});

//grafica de barras para mas interesados
var barCtx = document.getElementById('barChart2').getContext('2d');
        var barChart = new Chart(barCtx, {
            type: 'bar',
            data: {
                labels: ['Profesionales', 'Estudiantes'],
                datasets: [{
                    label: 'Cantidad de Interesados',
                    data: [120, 95],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                },
                plugins: {
                    legend: {
                        display: false // Oculta la leyenda
                    },
                    title: {
                        display: false // Oculta el título
                    }
                }
            }
        });
// Gráfico de dona para mas interesados
var doughnutCtx = document.getElementById('doughnutChart2').getContext('2d');
var doughnutChart = new Chart(doughnutCtx, {
    type: 'doughnut',
    data: {
        labels: ['Profesionales', 'Estudiantes'],
        datasets: [{
            label: 'Clics por Producto',
            data: [120, 95],
            backgroundColor: [
                '#FF6384',
                '#36A2EB'
            ],
            hoverOffset: 10
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'top',
            },
            title: {
                display: true,
                text: 'Clientes mas interesados'
            }
        }
    }
}); 