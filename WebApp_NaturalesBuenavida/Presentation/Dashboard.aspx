<%@ Page Title="Bienvenidos" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Presentation.Dashboard" %>
  
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <script src="https://cdn.jsdelivr.net/npm/apexcharts"></script>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container m-2 bg-white border rounded">
          <div class="row">
            <div class="col-md-6 m-2">
                <div id="chart1" class="p-3 border rounded bg-light"></div>
            </div>
            <div class="col-md-6 m-2">
                <div id="chart2" class="p-3 border rounded bg-light"></div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-6 m-2">
                <div id="chart3" class="p-3 border rounded bg-light"></div>
            </div>
            <div class="col-md-6 m-2">
                <div id="chart4" class="p-3 border rounded bg-light"></div>
            </div>
        </div>
    </div>

    <script>
      
        var options = {
          series: [44, 55, 41, 17, 15],
          chart: {
          type: 'donut',
        },
        responsive: [{
          breakpoint: 480,
          options: {
            chart: {
              width: 200
            },
            legend: {
              position: 'bottom'
            }
          }
        }]
        };

        var chart = new ApexCharts(document.querySelector("#chart1"), options);
        chart.render();      
    </script>

</asp:Content>
