<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSherpa360.Master" AutoEventWireup="true" CodeBehind="SalesDashboard.aspx.cs" Inherits="ShERPa360net.SalesDashboard" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <%--<script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-doughnutlabel"></script>--%>
    <script src="https://cdn.jsdelivr.net/npm/chart.js@2.9.4"></script>
    <script src="https://cdn.jsdelivr.net/npm/chartjs-plugin-doughnutlabel@1.0.3/dist/chartjs-plugin-doughnutlabel.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>



    <style type="text/css">
        .HeaderClass {
            font-weight: 1000 !important;
            color: darkblue !important;
            width: -webkit-fill-available !important;
            font-size: large !important;
        }

        .lblclass {
            font-weight: 1000 !important;
            color: darkblue !important;
            width: -webkit-fill-available !important;
            font-size: large !important;
        }
    </style>


    <style>
        #gaugeChart {
            width: 100% !important;
            max-width: 250px;
            height: 100% !important;
            max-height: 205px;
        }

        #gaugeChart2 {
            width: 100% !important;
            max-width: 250px;
            height: 100% !important;
            max-height: 205px;
        }

        #gaugeChart3 {
            width: 100% !important;
            max-width: 250px;
            height: 100% !important;
            max-height: 205px;
        }

        #gaugeChart4 {
            width: 100% !important;
            max-width: 250px;
            height: 100% !important;
            max-height: 205px;
        }
    </style>

    <script>
        window.onload = function () {
            const gaugeValue = <%= GaugeValue %>; // Value from server
            const gaugeChart = new Chart(document.getElementById('gaugeChart'), {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [gaugeValue, 500000 - gaugeValue],
                        backgroundColor: ['#00008B', '#FAA61A'],
                        borderWidth: 0,
                        circumference: 180,
                        rotation: 270,
                        cutout: '75%'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { display: true },
                        tooltip: { enabled: true }
                    }
                },
                plugins: [{
                    id: 'gaugeText',
                    beforeDraw: chart => {
                        const { width } = chart / 2;
                        const { height } = chart.chartArea / 2;
                        const ctx = chart.ctx;
                        ctx.restore();
                        const fontSize = 20;
                        ctx.font = fontSize + "px Arial";
                        ctx.textBaseline = "middle";
                        ctx.fillStyle = "#333";
                        const text = gaugeValue;
                        //const textX = Math.round((width - ctx.measureText(text).width) / 2);
                        //const textY = height / 1.1;
                        //const textX = 80;
                        //const textY = 150;
                        //ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }]
            });


            //const ctx = document.getElementById('gaugeChart2').getContext('2d');

            //const gradient = ctx.createLinearGradient(0, 0, 300, 0);
            //gradient.addColorStop(0, "#f1c40f");
            //gradient.addColorStop(0.5, "#e67e22");
            //gradient.addColorStop(1, "#e74c3c");

            //const gradient = ctx.createRadialGradient(150, 75, 30, 150, 75, 150);
            //gradient.addColorStop(0, "#ffffff");
            //gradient.addColorStop(1, "#00ffcc"); // neon glow

          <%--  const gaugeValue2 = <%= GaugeValue2 %>; // Value from server
            const gaugeChart2 = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [gaugeValue2, 50000000 - gaugeValue2],
                        backgroundColor: [gradient, '#ecf0f1'],
                        //backgroundColor: ['#e74c3c', '#f39c12', '#2ecc71'],
                        borderWidth: 0,
                        circumference: 180,
                        rotation: 270,
                        cutout: '95%',
                        borderRadius: 5,
                        //backgroundColor: ['#3498db', 'transparent']
                        //backgroundColor: ctx.createLinearGradient(0, 0, 300, 0),


                    }]
                },
                options: {
                    responsive: true,
                    animation: {
                        animateRotate: true,
                        duration: 2000
                    },
                    plugins: {
                        legend: { display: true },
                        tooltip: { enabled: true },
                        shadow: {
                            shadowColor: 'rgba(0,0,0,0.3)',
                            shadowBlur: 10
                        }
                    }
                },
                plugins: [{
                    id: 'gaugeText',
                    beforeDraw: chart => {
                        const { width } = chart / 2;
                        const { height } = chart.chartArea / 2;
                        const ctx = chart.ctx;
                        ctx.restore();

                        const fontSize = 20;
                        ctx.font = fontSize + "px Arial";
                        ctx.textBaseline = "middle";
                        ctx.textAlign = "center";
                        ctx.fillStyle = "#333";
                        ctx.shadowColor = "rgba(0,0,0,0.3)";
                        ctx.shadowBlur = 4;
                        const text = gaugeValue2;
                        //const textX = Math.round((width - ctx.measureText(text).width) / 2);
                        //const textY = height / 1.1;
                        //const textX = 80;
                        //const textY = 150;
                        //ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }]
            });--%>


            const gaugeValue2 = <%= GaugeValue2 %>; // Value from server
            const gaugeChart2 = new Chart(document.getElementById('gaugeChart2'), {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [gaugeValue2, 50000000 - gaugeValue2],
                        backgroundColor: ['#00008B', '#FAA61A'],
                        borderWidth: 0,
                        circumference: 180,
                        rotation: 270,
                        cutout: '75%'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { display: true },
                        tooltip: { enabled: true }
                    }
                },
                plugins: [{
                    id: 'gaugeText',
                    beforeDraw: chart => {
                        const { width } = chart / 2;
                        const { height } = chart.chartArea / 2;
                        const ctx = chart.ctx;
                        ctx.restore();
                        const fontSize = 20;
                        ctx.font = fontSize + "px Arial";
                        ctx.textBaseline = "middle";
                        ctx.fillStyle = "#333";
                        const text = gaugeValue2;
                        //const textX = Math.round((width - ctx.measureText(text).width) / 2);
                        //const textY = height / 1.1;
                        //const textX = 80;
                        //const textY = 150;
                        //ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }]
            });

            const gaugeValue3 = <%= GaugeValue3 %>; // Value from server
            const gaugeChart3 = new Chart(document.getElementById('gaugeChart3'), {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [gaugeValue3, 50000000 - gaugeValue3],
                        backgroundColor: ['#00008B', '#FAA61A'],
                        borderWidth: 0,
                        circumference: 180,
                        rotation: 270,
                        cutout: '75%'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { display: true },
                        tooltip: { enabled: true }
                    }
                },
                plugins: [{
                    id: 'gaugeText',
                    beforeDraw: chart => {
                        const { width } = chart / 2;
                        const { height } = chart.chartArea / 2;
                        const ctx = chart.ctx;
                        ctx.restore();
                        const fontSize = 20;
                        ctx.font = fontSize + "px Arial";
                        ctx.textBaseline = "middle";
                        ctx.fillStyle = "#333";
                        const text = gaugeValue3;
                        //const textX = Math.round((width - ctx.measureText(text).width) / 2);
                        //const textY = height / 1.1;
                        //const textX = 80;
                        //const textY = 150;
                        //ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }]
            });

            const gaugeValue4 = <%= GaugeValue4 %>; // Value from server
            const gaugeChart4 = new Chart(document.getElementById('gaugeChart4'), {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [gaugeValue4, 100 - gaugeValue4],
                        backgroundColor: ['#00008B', '#FAA61A'],
                        borderWidth: 0,
                        circumference: 180,
                        rotation: 270,
                        cutout: '75%'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { display: true },
                        tooltip: { enabled: true }
                    }
                },
                plugins: [{
                    id: 'gaugeText',
                    beforeDraw: chart => {
                        const { width } = chart / 2;
                        const { height } = chart.chartArea / 2;
                        const ctx = chart.ctx;
                        ctx.restore();
                        const fontSize = 20;
                        ctx.font = fontSize + "px Arial";
                        ctx.textBaseline = "middle";
                        ctx.fillStyle = "#333";
                        const text = gaugeValue4;
                        //const textX = Math.round((width - ctx.measureText(text).width) / 2);
                        //const textY = height / 1.1;
                        //const textX = 80;
                        //const textY = 150;
                        //ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }]
            });

            const gaugeValue5 = <%= GaugeValue5 %>; // Value from server
            const gaugeChart5 = new Chart(document.getElementById('gaugeChart5'), {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [gaugeValue5, 10000000 - gaugeValue5],
                        backgroundColor: ['#00008B', '#FAA61A'],
                        borderWidth: 0,
                        circumference: 180,
                        rotation: 270,
                        cutout: '75%'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { display: true },
                        tooltip: { enabled: true }
                    }
                },
                plugins: [{
                    id: 'gaugeText',
                    beforeDraw: chart => {
                        const { width } = chart / 2;
                        const { height } = chart.chartArea / 2;
                        const ctx = chart.ctx;
                        ctx.restore();
                        const fontSize = 20;
                        ctx.font = fontSize + "px Arial";
                        ctx.textBaseline = "middle";
                        ctx.fillStyle = "#333";
                        const text = gaugeValue5;
                        //const textX = Math.round((width - ctx.measureText(text).width) / 2);
                        //const textY = height / 1.1;
                        //const textX = 80;
                        //const textY = 150;
                        //ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }]
            });

            const GaugeValue6 = <%= GaugeValue6 %>; // Value from server
            const gaugeChart6 = new Chart(document.getElementById('gaugeChart6'), {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [GaugeValue6, 10000000 - GaugeValue6],
                        backgroundColor: ['#00008B', '#FAA61A'],
                        borderWidth: 0,
                        circumference: 180,
                        rotation: 270,
                        cutout: '75%'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { display: true },
                        tooltip: { enabled: true }
                    }
                },
                plugins: [{
                    id: 'gaugeText',
                    beforeDraw: chart => {
                        const { width } = chart / 2;
                        const { height } = chart.chartArea / 2;
                        const ctx = chart.ctx;
                        ctx.restore();
                        const fontSize = 20;
                        ctx.font = fontSize + "px Arial";
                        ctx.textBaseline = "middle";
                        ctx.fillStyle = "#333";
                        const text = GaugeValue6;
                        //const textX = Math.round((width - ctx.measureText(text).width) / 2);
                        //const textY = height / 1.1;
                        //const textX = 80;
                        //const textY = 150;
                        //ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }]
            });

            const GaugeValue7 = <%= GaugeValue7 %>; // Value from server
            const gaugeChart7 = new Chart(document.getElementById('gaugeChart7'), {
                type: 'doughnut',
                data: {
                    datasets: [{
                        data: [GaugeValue7, 10000000 - GaugeValue7],
                        backgroundColor: ['#00008B', '#FAA61A'],
                        borderWidth: 0,
                        circumference: 180,
                        rotation: 270,
                        cutout: '75%'
                    }]
                },
                options: {
                    responsive: true,
                    plugins: {
                        legend: { display: true },
                        tooltip: { enabled: true }
                    }
                },
                plugins: [{
                    id: 'gaugeText',
                    beforeDraw: chart => {
                        const { width } = chart / 2;
                        const { height } = chart.chartArea / 2;
                        const ctx = chart.ctx;
                        ctx.restore();
                        const fontSize = 20;
                        ctx.font = fontSize + "px Arial";
                        ctx.textBaseline = "middle";
                        ctx.fillStyle = "#333";
                        const text = GaugeValue7;
                        //const textX = Math.round((width - ctx.measureText(text).width) / 2);
                        //const textY = height / 1.1;
                        //const textX = 80;
                        //const textY = 150;
                        //ctx.fillText(text, textX, textY);
                        ctx.save();
                    }
                }]
            });

            google.load('visualization', '1', { 'packages': ['corechart'] });
            google.load('visualization', '1', { 'packages': ['gauge'] });
            google.setOnLoadCallback(drawChart);
            google.setOnLoadCallback(drawChart2);

            function drawChart() {
                var options = {
                    title: 'Todays Purchase - Plant wise',
                    titleTextStyle: {
                        color: "black",               // color 'red' or '#cc00cc'
                        fontName: "Times New Roman",    // 'Times New Roman'
                        fontSize: 25,               // 12, 18
                        bold: true,                 // true or false
                        italic: true,                // true of false
                    },
                    //width: 600,
                    height: 600,
                    bar: { groupWidth: "95%" },
                    legend: { position: "none" },
                    isStacked: true,
                    is3D: true,
                    hAxis: {
                        title: 'Plant Name',
                        titleTextStyle: {
                            color: "black",
                            fontName: "Times New Roman",
                            fontSize: 15,
                            bold: true,
                            italic: false
                        }
                    },
                    vAxis: {
                        title: 'Amount (RS.)',
                        titleTextStyle: {
                            color: "black",
                            fontName: "Times New Roman",
                            fontSize: 15,
                            bold: true,
                            italic: false
                        }
                    },
                    annotations: {
                        alwaysOutside: true,
                        textStyle: {
                            fontSize: 50,
                            auraColor: 'none'
                        }
                    }

                };
                $.ajax({
                    type: "POST",
                    url: "SalesDashboard.aspx/BarChart1",
                    //data: '{"FROMDATE" : "' + FROMDATE + '","TODATE" : "' + TODATE + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        var data = google.visualization.arrayToDataTable(r.d);
                        var chart = new google.visualization.ColumnChart(document.getElementById('ContentPlaceHolder1_chart'));
                        chart.draw(data, options);
                    },
                    failure: function (r) {
                        //alert('Fail : ' + r);
                    },
                    error: function (r) {
                        //alert('Error : ' + r);
                    }
                });
            }

            function drawChart2() {
                var options = {
                    title: 'This Months Purchase - Plant wise',
                    titleTextStyle: {
                        color: "black",               // color 'red' or '#cc00cc'
                        fontName: "Times New Roman",    // 'Times New Roman'
                        fontSize: 25,               // 12, 18
                        bold: true,                 // true or false
                        italic: true,                // true of false
                    },
                    //width: 600,
                    height: 600,
                    bar: { groupWidth: "95%" },
                    legend: { position: "none" },
                    isStacked: true,
                    is3D: true,
                    hAxis: {
                        title: 'Plant Name',
                        titleTextStyle: {
                            color: "black",
                            fontName: "Times New Roman",
                            fontSize: 15,
                            bold: true,
                            italic: false
                        }
                    },
                    vAxis: {
                        title: 'Amount (Rs.)',
                        titleTextStyle: {
                            color: "black",
                            fontName: "Times New Roman",
                            fontSize: 15,
                            bold: true,
                            italic: false
                        }
                    },
                    annotations: {
                        alwaysOutside: true,
                        textStyle: {
                            fontSize: 50,
                            auraColor: 'none'
                        }
                    }

                };
                $.ajax({
                    type: "POST",
                    url: "SalesDashboard.aspx/BarChart2",
                    //data: '{"FROMDATE" : "' + FROMDATE + '","TODATE" : "' + TODATE + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        var data = google.visualization.arrayToDataTable(r.d);
                        var chart = new google.visualization.ColumnChart(document.getElementById('ContentPlaceHolder1_chart1'));
                        chart.draw(data, options);
                    },
                    failure: function (r) {
                        //alert('Fail : ' + r);
                    },
                    error: function (r) {
                        //alert('Error : ' + r);
                    }
                });
            }
        };
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Sales Data (Croma)</strong></h3>
                    </div>
                    <div class="panel-body">
                        <div class="row" style="margin-top: 20px !important;">

                            <div class="col-md-4">
                                <%--<div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">--%>
                                <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowFooter="true">
                                    <EmptyDataTemplate>
                                        <div style="text-align: center; color: red; font-size: 18px;">
                                            List is empty !
                                        </div>
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="HeaderClass" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblLabel" runat="server" CssClass="pull-right lblclass" Text='<%# Eval("Label") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="All SO" Visible="false">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblALLSO" runat="server" CssClass="btn btn-success btn-lg lblclass" Text='<%# Eval("ALLSO") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DC / SI<br/>Pending">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblDCSIPENDING" runat="server" CssClass="btn btn-success btn-lg lblclass" Text='<%# Eval("DCSIPENDING") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Dispatch<br/>Pending">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblDISPATCHPENDING" runat="server" CssClass="btn btn-success btn-lg lblclass" Text='<%# Eval("DISPATCHPENDING") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Demo/Inst<br/>Pending">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblDEMOINSTAPENDING" runat="server" CssClass="btn btn-success btn-lg lblclass" Text='<%# Eval("DEMOINSTAPENDING") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                                <%--</div>--%>
                            </div>

                            <div class="col-md-2">

                                <%--<canvas id="gaugeChart" width="400" height="200"></canvas>--%>
                                <center>
                                    <canvas id="gaugeChart"></canvas>
                                    <br />

                                    <asp:Label ID="lblTodays" runat="server" Text="Today's Sales" Style="font-size: medium;"></asp:Label><br />
                                    <asp:Label ID="lblTodaysCount" runat="server" Style="font-size: x-large; color: red;"></asp:Label>
                                </center>
                            </div>

                            <div class="col-md-2">
                                <center>
                                    <canvas id="gaugeChart2"></canvas>
                                    <br />

                                    <asp:Label ID="lblThisMonth" runat="server" Text="This Month's Sales" Style="font-size: medium;"></asp:Label><br />
                                    <asp:Label ID="lblThisMonthCount" runat="server" Style="font-size: x-large; color: red;"></asp:Label>
                                </center>
                            </div>

                            <div class="col-md-2">
                                <center>
                                    <canvas id="gaugeChart3"></canvas>
                                    <br />

                                    <asp:Label ID="lblLastMonth" runat="server" Text="Last Month's Sales" Style="font-size: medium;"></asp:Label><br />
                                    <asp:Label ID="lblLastMonthCount" runat="server" Style="font-size: x-large; color: red;"></asp:Label>
                                </center>
                            </div>

                            <div class="col-md-2">
                                <center>
                                    <canvas id="gaugeChart4"></canvas>
                                    <br />

                                    <asp:Label ID="lblThisMonthSalr" runat="server" Text="This Month's SLR" Style="font-size: medium;"></asp:Label><br />
                                    <asp:Label ID="lblThisMonthSalrCount" runat="server" Style="font-size: x-large; color: red;"></asp:Label>
                                </center>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Purchase Data (Croma Plants)</strong></h3>
                    </div>
                    <div class="panel-body">
                        <div class="row" style="margin-top: 20px !important;">

                            <div class="col-md-4">
                                <asp:GridView ID="grvPurchase" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowFooter="true">
                                    <EmptyDataTemplate>
                                        <div style="text-align: center; color: red; font-size: 18px;">
                                            List is empty !
                                        </div>
                                    </EmptyDataTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="HeaderClass" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblLabel" runat="server" CssClass="pull-right lblclass" Text='<%# Eval("Label") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="All PO" Visible="false">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblALLPO" runat="server" CssClass="btn btn-success btn-lg lblclass" Text='<%# Eval("ALLPO") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pending<br/>PO">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblPENDINGPO" runat="server" CssClass="btn btn-success btn-lg lblclass" Text='<%# Eval("PENDINGPO") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="All GRN" Visible="false">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblALLGRN" runat="server" CssClass="btn btn-success btn-lg lblclass" Text='<%# Eval("ALLGRN") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pending<br/>GRN">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblPENDINGGRN" runat="server" CssClass="btn btn-success btn-lg lblclass" Text='<%# Eval("PENDINGGRN") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="All PB" Visible="false">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblALLPB" runat="server" CssClass="btn btn-success btn-lg lblclass" Text='<%# Eval("ALLPB") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pending<br/>to Sent AC">
                                            <ItemTemplate>
                                                <center>
                                                    <asp:Label ID="lblACSENTPENDING" runat="server" CssClass="btn btn-success btn-lg lblclass" Text='<%# Eval("ACSENTPENDING") %>'></asp:Label>
                                                </center>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>


                            <div class="col-md-2">

                                <%--<canvas id="gaugeChart" width="400" height="200"></canvas>--%>
                                <center>
                                    <canvas id="gaugeChart5"></canvas>
                                    <br />

                                    <asp:Label ID="lblTodayPur" runat="server" Text="Today's Purchase" Style="font-size: medium;"></asp:Label><br />
                                    <asp:Label ID="lblTodaysPurchase" runat="server" Style="font-size: x-large; color: red;"></asp:Label>
                                </center>
                            </div>

                            <div class="col-md-2">
                                <center>
                                    <canvas id="gaugeChart6"></canvas>
                                    <br />

                                    <asp:Label ID="lblThisMoPur" runat="server" Text="This Month's Purchase" Style="font-size: medium;"></asp:Label><br />
                                    <asp:Label ID="lblThisMonthPurchase" runat="server" Style="font-size: x-large; color: red;"></asp:Label>
                                </center>
                            </div>

                            <div class="col-md-2">
                                <center>
                                    <canvas id="gaugeChart7"></canvas>
                                    <br />

                                    <asp:Label ID="lblLastMoPur" runat="server" Text="Last Month's Purchase" Style="font-size: medium;"></asp:Label><br />
                                    <asp:Label ID="lblLastMonthPurchase" runat="server" Style="font-size: x-large; color: red;"></asp:Label>
                                </center>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Purchase Data - Plant wise </strong></h3>
                    </div>
                    <div class="panel-body">
                        <div class="row" style="margin-top: 20px !important;">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div id="chart" runat="server" style="margin-left: 0px; border: thin; border-color: orange;">
                                    </div>
                                    <center>
                                        <h4 style="font-weight: bold;">Today's Purchase - Plant wise</h4>
                                    </center>
                                </div>

                                <div class="col-md-6">
                                    <div id="chart1" runat="server" style="margin-left: 0px; border: thin; border-color: orange;">
                                    </div>
                                    <center>
                                        <h4 style="font-weight: bold;">This Month's Purchase - Plant wise</h4>
                                    </center>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptSDDashboard" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSD" />

</asp:Content>
