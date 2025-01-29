<%@ Page Title="Bienvenidos" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Presentation.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="resources/js/jquery.min.js"></script>
    <script type="text/javascript" src="resources/js/moment.min.js"></script>
    <script type="text/javascript" src="resources/js/daterangepicker.js"></script>
    <script type="text/javascript" src="resources/js/highcharts.js"></script>
    <script type="text/javascript" src="resources/js/highcharts-more.js"></script>
    <script type="text/javascript" src="resources/js/modules/exporting.js"></script>
    <script type="text/javascript" src="resources/js/modules/export-data.js"></script>
    <script type="text/javascript" src="resources/js/modules/accessibility.js"></script>
    <link rel="stylesheet" type="text/css" href="resources/css/daterangepicker.css" />

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center main-title">Dashboard</h2>
    <div class="container m-2 bg-white border rounded">
        <div class="d-flex justify-content-end p-2">
            <div id="reportrange" role='button' class="rounded-pill p-1 border">
                <i class="bi bi-calendar-fill me-2"></i><span></span>
            </div>
        </div>

        <div class="row p-2">
            <div class="col-md-3">
                <figure class="border rounded p-2 highcharts-figure">
                    <div id="container-kpi-1"></div>
                </figure>
            </div>
            <div class="col-md-3">
                <figure class="border rounded p-2 highcharts-figure">
                    <div id="container-kpi-2"></div>
                </figure>
            </div>
            <div class="col-md-3">
                <figure class="border rounded p-2 highcharts-figure">
                    <div id="container-kpi-3"></div>
                </figure>
            </div>
            <div class="col-md-3">
                <figure class="border rounded p-2 highcharts-figure">
                    <div id="container-kpi-4"></div>
                </figure>
            </div>
        </div>
        <div class="row p-2">
            <div class="col-md-6">
                <figure class="border rounded p-2 highcharts-figure">
                    <div id="container-categorias"></div>
                </figure>
            </div>
            <div class="col-md-6">
                <figure class="border rounded p-2 highcharts-figure">
                    <div id="container-ventas-compras"></div>
                </figure>
            </div>
        </div>
        <div class="row p-2">
            <div class="col-md-6">
                <figure class="border rounded p-2 highcharts-figure">
                    <div id="container-clientes"></div>
                </figure>
            </div>
            <div class="col-md-6 border rounded p-2 ">
                <h5 class="fw-bold text-center mb-4">Existencias por producto</h5>
                <table class="table table-striped table-bordered">
                    <thead class="table-dark">
                        <tr>
                            <th>Producto</th>
                            <th>Existencia</th>
                            <th>Promedio de ordenes (Últimos 30 días)</th>
                            <th>Precio unitario</th>
                            <th>Valor existencias</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>Producto 1</td>
                            <td>82</td>
                            <td>8</td>
                            <td>30.45</td>
                            <td>2496.90</td>
                        </tr>
                        <tr>
                            <td>Product 2</td>
                            <td>70</td>
                            <td>11</td>
                            <td>31.70</td>
                            <td>2219.00</td>
                        </tr>
                        <tr>
                            <td>Product 3</td>
                            <td>93</td>
                            <td>17</td>
                            <td>48.44</td>
                            <td>4504.92</td>
                        </tr>
                        <tr>
                            <td>Product 4</td>
                            <td>95</td>
                            <td>11</td>
                            <td>12.44</td>
                            <td>1181.80</td>
                        </tr>
                        <tr>
                            <td>Product 5</td>
                            <td>40</td>
                            <td>14</td>
                            <td>29.52</td>
                            <td>1180.80</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <script>


        $(function () {

            var start = moment().subtract(29, 'days');
            var end = moment();

            function cb(start, end) {
                $('#reportrange span').html(start.format('MM/DD/YYYY') + ' - ' + end.format('MM/DD/YYYY'));
            }

            $('#reportrange').daterangepicker({
                locale: {
                    format: 'MM/DD/YYYY',
                    separator: ' - ',
                    applyLabel: 'Filtrar',
                    cancelLabel: 'Cancelar',
                    fromLabel: 'Desde',
                    toLabel: 'Hasta',
                    customRangeLabel: 'Elegir fechas',
                    weekLabel: 'S',
                    daysOfWeek: [
                        'Dom',
                        'Lun',
                        'Mar',
                        'Mie',
                        'Jue',
                        'Vie',
                        'Sab'
                    ],
                    monthNames: [
                        'Enero',
                        'Febreo',
                        'Marzo',
                        'Abril',
                        'Mayo',
                        'Junio',
                        'Julio',
                        'Agosto',
                        'Septiembre',
                        'Octubre',
                        'Noviembre',
                        'Diciembre'
                    ],
                    firstDay: 1
                },
                startDate: start,
                endDate: end,
                ranges: {
                    'Hoy': [moment(), moment()],
                    'Ayer': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                    'Ultimos 7 dias': [moment().subtract(6, 'days'), moment()],
                    'Ultimos 30 dias': [moment().subtract(29, 'days'), moment()],
                    'Este mes': [moment().startOf('month'), moment().endOf('month')],
                    'Ultimo mes': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
                }
            }, cb);

            cb(start, end);


            Highcharts.chart('container-categorias', {
                chart: {
                    type: 'pie'
                },
                exporting: {
                    enabled: false
                },
                title: {
                    text: 'Las categorias mas vendidas'
                },
                tooltip: {
                    valueSuffix: '%'
                },
                plotOptions: {
                    series: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: [{
                            enabled: true,
                            distance: 20
                        }, {
                            enabled: true,
                            distance: -40,
                            format: '{point.percentage:.1f}%',
                            style: {
                                fontSize: '1.2em',
                                textOutline: 'none',
                                opacity: 0.7
                            },
                            filter: {
                                operator: '>',
                                property: 'percentage',
                                value: 10
                            }
                        }]
                    }
                },
                series: [
                    {
                        name: 'Categorias',
                        colorByPoint: true,
                        data: [
                            {
                                name: 'Vitaminas',
                                y: 55.02
                            },
                            {
                                name: 'Suplementos',
                                y: 26.71
                            },
                            {
                                name: 'Lipo reductores',
                                y: 1.09
                            },
                            {
                                name: 'Vegetarianos',
                                y: 15.5
                            },
                            {
                                name: 'Carnicos',
                                y: 1.68
                            }
                        ]
                    }
                ]
            });
        });

        Highcharts.chart('container-ventas-compras', {
            chart: {
                zooming: {
                    type: 'xy'
                }
            },
            exporting: {
                enabled: false
            },
            title: {
                text: 'Ventas vs Compras por mes',
                align: 'left'
            },
            xAxis: [{
                categories: [
                    'Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
                    'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'
                ],
                crosshair: true
            }],
            yAxis: [{ // Primary yAxis
                labels: {
                    format: '${value}',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                },
                title: {
                    text: 'Ventas',
                    style: {
                        color: Highcharts.getOptions().colors[1]
                    }
                }
            }, { // Secondary yAxis
                title: {
                    text: 'Compras',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                labels: {
                    format: '${value}',
                    style: {
                        color: Highcharts.getOptions().colors[0]
                    }
                },
                opposite: true
            }],
            tooltip: {
                shared: true
            },
            legend: {
                align: 'left',
                verticalAlign: 'top',
                backgroundColor:
                    Highcharts.defaultOptions.legend.backgroundColor || // theme
                    'rgba(255,255,255,0.25)'
            },
            series: [{
                name: 'Ventas',
                type: 'column',
                yAxis: 1,
                data: [
                    45.7, 37.0, 28.9, 17.1, 39.2, 18.9, 90.2, 78.5, 74.6,
                    18.7, 17.1, 16.0
                ],
                tooltip: {
                    valueSuffix: '$'
                }

            }, {
                name: 'Compras',
                type: 'spline',
                data: [
                    -11.4, -9.5, -14.2, 0.2, 7.0, 12.1, 13.5, 13.6, 8.2,
                    -2.8, -12.0, -15.5
                ],
                tooltip: {
                    valueSuffix: '$'
                }
            }]
        });


        Highcharts.chart('container-clientes', {
            chart: {
                type: 'bar'
            },
            title: {
                text: 'Los 10 clientes mas representativos'
            },
            exporting: {
                enabled: false
            },
            xAxis: {
                categories: ['Andres peres', 'Jaime Hurtado', 'Rodolfo quinallas'],
                title: {
                    text: null
                },
                gridLineWidth: 1,
                lineWidth: 0
            },
            yAxis: {
                min: 0,
                title: {
                    text: 'miles cop',
                    align: 'high'
                },
                labels: {
                    overflow: 'justify'
                },
                gridLineWidth: 0
            },
            tooltip: {
                valueSuffix: ' miles cop'
            },
            plotOptions: {
                bar: {
                    borderRadius: '50%',
                    dataLabels: {
                        enabled: true
                    },
                    groupPadding: 0.1
                }
            },
            legend: {
                layout: 'vertical',
                align: 'right',
                verticalAlign: 'top',
                x: -40,
                y: 80,
                floating: true,
                borderWidth: 1,
                backgroundColor:
                    Highcharts.defaultOptions.legend.backgroundColor || '#FFFFFF',
                shadow: true
            },
            credits: {
                enabled: false
            },
            series: [{
                name: 'Total compras',
                data: [632, 345, 534]
            }]
        });

        Highcharts.chart('container-kpi-1', {

            chart: {
                type: 'gauge',
                plotBackgroundColor: null,
                plotBackgroundImage: null,
                plotBorderWidth: 0,
                plotShadow: false,
                height: '80%'
            },
            exporting: {
                enabled: false
            },
            title: {
                text: 'Inventario'
            },

            pane: {
                startAngle: -90,
                endAngle: 89.9,
                background: null,
                center: ['50%', '75%'],
                size: '110%'
            },

            // the value axis
            yAxis: {
                min: 0,
                max: 100,
                tickPixelInterval: 72,
                tickPosition: 'inside',
                tickColor: Highcharts.defaultOptions.chart.backgroundColor || '#FFFFFF',
                tickLength: 20,
                tickWidth: 2,
                minorTickInterval: null,
                labels: {
                    distance: 20,
                    style: {
                        fontSize: '14px'
                    }
                },
                lineWidth: 0,
                plotBands: [{
                    from: 0,
                    to: 33,
                    color: '#DF5353', // red
                    thickness: 20,
                    borderRadius: '50%'
                }, {
                    from: 33,
                    to: 66,
                    color: '#DDDF0D', // green
                    thickness: 20,
                    borderRadius: '50%'
                }, {
                    from: 67,
                    to: 100,
                    color: '#55BF3B', // yellow
                    thickness: 20
                }]
            },

            series: [{
                name: 'Speed',
                data: [80],
                dataLabels: {
                    format: '{y}',
                    borderWidth: 0,
                    color: (
                        Highcharts.defaultOptions.title &&
                        Highcharts.defaultOptions.title.style &&
                        Highcharts.defaultOptions.title.style.color
                    ) || '#333333',
                    style: {
                        fontSize: '16px'
                    }
                },
                dial: {
                    radius: '80%',
                    backgroundColor: 'gray',
                    baseWidth: 12,
                    baseLength: '0%',
                    rearLength: '0%'
                },
                pivot: {
                    backgroundColor: 'gray',
                    radius: 6
                }

            }]

        });

        Highcharts.chart('container-kpi-2', {

            chart: {
                type: 'gauge',
                plotBackgroundColor: null,
                plotBackgroundImage: null,
                plotBorderWidth: 0,
                plotShadow: false,
                height: '80%'
            },
            exporting: {
                enabled: false
            },
            title: {
                text: 'Ventas'
            },

            pane: {
                startAngle: -90,
                endAngle: 89.9,
                background: null,
                center: ['50%', '75%'],
                size: '110%'
            },

            // the value axis
            yAxis: {
                min: 0,
                max: 100,
                tickPixelInterval: 72,
                tickPosition: 'inside',
                tickColor: Highcharts.defaultOptions.chart.backgroundColor || '#FFFFFF',
                tickLength: 20,
                tickWidth: 2,
                minorTickInterval: null,
                labels: {
                    distance: 20,
                    style: {
                        fontSize: '14px'
                    }
                },
                lineWidth: 0,
                plotBands: [{
                    from: 0,
                    to: 33,
                    color: '#DF5353', // red
                    thickness: 20,
                    borderRadius: '50%'
                }, {
                    from: 33,
                    to: 66,
                    color: '#DDDF0D', // green
                    thickness: 20,
                    borderRadius: '50%'
                }, {
                    from: 67,
                    to: 100,
                    color: '#55BF3B', // yellow
                    thickness: 20
                }]
            },

            series: [{
                name: 'Speed',
                data: [80],
                dataLabels: {
                    format: '{y}',
                    borderWidth: 0,
                    color: (
                        Highcharts.defaultOptions.title &&
                        Highcharts.defaultOptions.title.style &&
                        Highcharts.defaultOptions.title.style.color
                    ) || '#333333',
                    style: {
                        fontSize: '16px'
                    }
                },
                dial: {
                    radius: '80%',
                    backgroundColor: 'gray',
                    baseWidth: 12,
                    baseLength: '0%',
                    rearLength: '0%'
                },
                pivot: {
                    backgroundColor: 'gray',
                    radius: 6
                }

            }]

        });

        Highcharts.chart('container-kpi-3', {

            chart: {
                type: 'gauge',
                plotBackgroundColor: null,
                plotBackgroundImage: null,
                plotBorderWidth: 0,
                plotShadow: false,
                height: '80%'
            },
            exporting: {
                enabled: false
            },
            title: {
                text: 'Compras'
            },

            pane: {
                startAngle: -90,
                endAngle: 89.9,
                background: null,
                center: ['50%', '75%'],
                size: '110%'
            },

            // the value axis
            yAxis: {
                min: 0,
                max: 100,
                tickPixelInterval: 72,
                tickPosition: 'inside',
                tickColor: Highcharts.defaultOptions.chart.backgroundColor || '#FFFFFF',
                tickLength: 20,
                tickWidth: 2,
                minorTickInterval: null,
                labels: {
                    distance: 20,
                    style: {
                        fontSize: '14px'
                    }
                },
                lineWidth: 0,
                plotBands: [{
                    from: 0,
                    to: 33,
                    color: '#DF5353', // red
                    thickness: 20,
                    borderRadius: '50%'
                }, {
                    from: 33,
                    to: 66,
                    color: '#DDDF0D', // green
                    thickness: 20,
                    borderRadius: '50%'
                }, {
                    from: 67,
                    to: 100,
                    color: '#55BF3B', // yellow
                    thickness: 20
                }]
            },

            series: [{
                name: 'Speed',
                data: [80],
                dataLabels: {
                    format: '{y}',
                    borderWidth: 0,
                    color: (
                        Highcharts.defaultOptions.title &&
                        Highcharts.defaultOptions.title.style &&
                        Highcharts.defaultOptions.title.style.color
                    ) || '#333333',
                    style: {
                        fontSize: '16px'
                    }
                },
                dial: {
                    radius: '80%',
                    backgroundColor: 'gray',
                    baseWidth: 12,
                    baseLength: '0%',
                    rearLength: '0%'
                },
                pivot: {
                    backgroundColor: 'gray',
                    radius: 6
                }

            }]

        });


        Highcharts.chart('container-kpi-4', {

            chart: {
                type: 'gauge',
                plotBackgroundColor: null,
                plotBackgroundImage: null,
                plotBorderWidth: 0,
                plotShadow: false,
                height: '80%'
            },
            exporting: {
                enabled: false
            },
            title: {
                text: 'Devoluciones'
            },

            pane: {
                startAngle: -90,
                endAngle: 89.9,
                background: null,
                center: ['50%', '75%'],
                size: '110%'
            },

            // the value axis
            yAxis: {
                min: 0,
                max: 100,
                tickPixelInterval: 72,
                tickPosition: 'inside',
                tickColor: Highcharts.defaultOptions.chart.backgroundColor || '#FFFFFF',
                tickLength: 20,
                tickWidth: 2,
                minorTickInterval: null,
                labels: {
                    distance: 20,
                    style: {
                        fontSize: '14px'
                    }
                },
                lineWidth: 0,
                plotBands: [{
                    from: 0,
                    to: 33,
                    color: '#55BF3B', 
                    thickness: 20,
                    borderRadius: '50%'
                }, {
                    from: 33,
                    to: 66,
                    color: '#DDDF0D', 
                    thickness: 20,
                    borderRadius: '50%'
                }, {
                    from: 67,
                    to: 100,
                    color: '#DF5353', 
                    thickness: 20
                }]
            },

            series: [{
                name: 'Devoluciones',
                data: [80],
                dataLabels: {
                    format: '{y}',
                    borderWidth: 0,
                    color: (
                        Highcharts.defaultOptions.title &&
                        Highcharts.defaultOptions.title.style &&
                        Highcharts.defaultOptions.title.style.color
                    ) || '#333333',
                    style: {
                        fontSize: '16px'
                    }
                },
                dial: {
                    radius: '80%',
                    backgroundColor: 'gray',
                    baseWidth: 12,
                    baseLength: '0%',
                    rearLength: '0%'
                },
                pivot: {
                    backgroundColor: 'gray',
                    radius: 6
                }

            }]

        });
    </script>

</asp:Content>
