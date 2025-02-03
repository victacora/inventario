<%@ Page Title="Bienvenidos" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Presentation.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="resources/js/moment.min.js"></script>
    <script type="text/javascript" src="resources/js/daterangepicker.js"></script>
    <script type="text/javascript" src="resources/js/highcharts.js"></script>
    <script type="text/javascript" src="resources/js/highcharts-more.js"></script>
    <script type="text/javascript" src="resources/js/modules/exporting.js"></script>
    <script type="text/javascript" src="resources/js/modules/export-data.js"></script>
    <script type="text/javascript" src="resources/js/modules/accessibility.js"></script>

    <link rel="stylesheet" type="text/css" href="resources/css/daterangepicker.css" />
   <link href="resources/css/dataTables.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2 class="text-center main-title">Dashboard</h2>
    <div id="loading" class="d-none justify-content-center m-auto">
        <div class="spinner-border" role="status">
            <span class="visually-hidden">Loading...</span>
        </div>
    </div>
    <div id="graficas" class="container m-2 bg-white border rounded" style="display: none">
        <div class="d-flex justify-content-end p-2">
            <div id="reportrange" role='button' class="rounded-pill p-1 border">
                <i class="bi bi-calendar-fill me-2"></i><span></span>
            </div>
        </div>

        <div class="row p-2">
            <div class="col-md-3">
                <div id="container-kpi-1" class="text-center border rounded p-2 highcharts-figure">
                </div>
            </div>
            <div class="col-md-3">
                <div id="container-kpi-2" class="text-center border rounded p-2 highcharts-figure">
                </div>
            </div>
            <div class="col-md-3">
                <div id="container-kpi-3" class="text-center border rounded p-2 highcharts-figure">
                </div>
            </div>
            <div class="col-md-3">
                <div id="container-kpi-4" class="text-center border rounded p-2 highcharts-figure">
                </div>
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
            <div class="col-md-6 border rounded p-2 table-responsive">
                <h5 class="fw-bold text-center mb-4">Existencias por producto</h5>
                <table id="stock-productos" class="table display" style="width: 100%">
                    <thead class="table-dark">
                        <tr>
                            <th>Producto</th>
                            <th>Existencia</th>
                            <th>Promedio de ordenes (Periodo seleccionado)</th>
                            <th>Precio unitario</th>
                            <th>Valor existencias</th>
                        </tr>
                    </thead>
                    <tbody>
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
                $('#reportrange span').html(start.format('DD/MM/YYYY') + ' - ' + end.format('DD/MM/YYYY'));
                $('#loading').addClass('d-flex').removeClass('d-none');
                $('#graficas').hide();

                $.ajax({
                    url: 'Dashboard.aspx/loadDashboardData',
                    type: 'POST',
                    data: JSON.stringify({ fechaInicial: start.format('DD/MM/YYYY'), fechaFinal: end.format('DD/MM/YYYY') }),
                    contentType: 'application/json',
                    success: function (response) {
                        if (response.d.Success) {
                            $('#loading').removeClass('d-flex').addClass('d-none');

                            console.log('response.d.Result.ventasPorCategoria', response.d.Result.ventasPorCategoria);
                            console.log('response.d.Result.stockProductosVentasTotales', response.d.Result.stockProductosVentasTotales);
                            console.log('response.d.Result.comprasPorClientes', response.d.Result.comprasPorClientes);
                            console.log('response.d.Result.ventasPorAnioYMesList', response.d.Result.ventasPorAnioYMesList);
                            console.log('response.d.Result.comprasPorAnioYMesList', response.d.Result.comprasPorAnioYMesList);
                            console.log('response.d.Result.totalInventario', response.d.Result.totalInventario);
                            console.log('response.d.Result.totalVentasPorPeriodo', response.d.Result.totalVentasPorPeriodo);
                            console.log('response.d.Result.totalComprasPorPeriodo', response.d.Result.totalComprasPorPeriodo);
                            console.log('response.d.Result.cantidadVentasPorPeriodo', response.d.Result.cantidadVentasPorPeriodo);
                            console.log('response.d.Result.cantidadDevolucionesPorPeriodo', response.d.Result.cantidadDevolucionesPorPeriodo);

                            var meses = ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'];

                            var categorias = [];
                            var ventas = [];
                            var compras = [];
                            for (var anio = start.year(); anio <= end.year(); anio++) {
                                for (var mes = 0; mes < 12; mes++) {
                                    categorias.push(anio + '-' + meses[mes]);
                                    var valorVenta = response.d.Result.ventasPorAnioYMesList.find(item => item.year === anio && item.month === mes + 1);
                                    var valorCompra = response.d.Result.comprasPorAnioYMesList.find(item => item.year === anio && item.month === mes + 1);
                                    if (valorVenta) {
                                        ventas.push(valorVenta.total);
                                    } else {
                                        ventas.push(0);
                                    }
                                    if (valorCompra) {
                                        compras.push(valorCompra.total);
                                    } else {
                                        compras.push(0);
                                    }
                                }
                            }


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
                                    categories: categorias,
                                    crosshair: true
                                }],
                                yAxis: [{
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
                                }, {
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
                                        Highcharts.defaultOptions.legend.backgroundColor ||
                                        'rgba(255,255,255,0.25)'
                                },
                                series: [{
                                    name: 'Ventas',
                                    type: 'column',
                                    yAxis: 1,
                                    data: ventas,
                                    tooltip: {
                                        valueSuffix: '$'
                                    }

                                }, {
                                    name: 'Compras',
                                    type: 'spline',
                                    data: compras,
                                    tooltip: {
                                        valueSuffix: '$'
                                    }
                                }]
                            });

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
                                        data: response.d.Result.ventasPorCategoria
                                    }
                                ]
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
                                    categories: response.d.Result.comprasPorClientes.map(item => item.cliente),
                                    title: {
                                        text: null
                                    },
                                    gridLineWidth: 1,
                                    lineWidth: 0
                                },
                                yAxis: {
                                    min: 0,
                                    title: {
                                        text: 'COP',
                                        align: 'high'
                                    },
                                    labels: {
                                        overflow: 'justify'
                                    },
                                    gridLineWidth: 0
                                },
                                tooltip: {
                                    valueSuffix: ' COP'
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
                                    data: response.d.Result.comprasPorClientes.map(item => item.total),
                                    showInLegend: false
                                }]
                            });
                            let format = new Intl.NumberFormat('es-CO', { style: 'currency', currency: 'COP' });
                            
                            $("#container-kpi-1").html("<b>Inventario: </b><br>" + format.format(response.d.Result.totalInventario));
                            $("#container-kpi-2").html("<b>Ventas: </b><br>" + format.format(response.d.Result.totalVentasPorPeriodo));
                            $("#container-kpi-3").html("<b>Compras: </b><br>" + format.format(response.d.Result.totalComprasPorPeriodo));

                            format = new Intl.NumberFormat('es-CO', { style: 'percent', minimumFractionDigits: 0 });

                            $("#container-kpi-4").html("<b>Devoluciones: </b><br>" + format.format((response.d.Result.cantidadDevolucionesPorPeriodo != 0 && response.d.Result.cantidadVentasPorPeriodo != 0 ?response.d.Result.cantidadDevolucionesPorPeriodo / response.d.Result.cantidadVentasPorPeriodo:0)));

                            $('#graficas').show();

                            $('#stock-productos').DataTable({
                                "processing": true,
                                "serverSide": false,
                                "bDestroy": true,
                                "data": response.d.Result.stockProductosVentasTotales,
                                "columns": [
                                    { "data": "producto" },
                                    { "data": "cantidad" },
                                    { "data": "promedio" },
                                    { "data": "precio" },
                                    { "data": "valor" }
                                ],
                                "language": {
                                    "emptyTable": "No hay datos disponibles en la tabla",
                                    "loadingRecords": "Cargando...",
                                    "processing": "Procesando...",
                                    "lengthMenu": "Mostrar _MENU_ registros por página",
                                    "zeroRecords": "No se encontraron resultados",
                                    "info": "Mostrando página _PAGE_ de _PAGES_",
                                    "infoEmpty": "No hay registros disponibles",
                                    "infoFiltered": "(filtrado de _MAX_ registros totales)",
                                    "search": "Buscar:",
                                    "paginate": {
                                        "first": "Primero",
                                        "last": "Último",
                                        "next": "Siguiente",
                                        "previous": "Anterior"
                                    }
                                }
                            });

                        } else {
                            alert('Error al consultar los datos del tablero: ' + response.d.Message);
                            $('#loading').removeClass('d-flex').addClass('d-none');
                        }
                    },
                    error: function () {
                        alert("Hubo un error al consultar los datos del tablero.");
                        $('#loading').removeClass('d-flex').addClass('d-none');
                    }
                });

            }

            $('#reportrange').daterangepicker({
                locale: {
                    format: 'DD/MM/YYYY',
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



        });

    </script>

</asp:Content>
