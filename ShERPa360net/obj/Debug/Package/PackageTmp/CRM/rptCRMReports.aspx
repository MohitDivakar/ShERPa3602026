<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="rptCRMReports.aspx.cs" Inherits="ShERPa360net.CRM.rptCRMReports" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CRM Reports</title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        var FROMDATE, TODATE;
        $(window).load(function () {
            debugger;
            google.load('visualization', '1', { 'packages': ['corechart'] });
            google.load('visualization', '1', { 'packages': ['gauge'] });
            google.setOnLoadCallback(drawChart);
            google.setOnLoadCallback(drawChart1);
            google.setOnLoadCallback(drawChart2);
            google.setOnLoadCallback(drawChart3);

            FROMDATE = $('#ContentPlaceHolder1_txtFromDate').val();
            //FROMDATE = new Date().toJSON().slice(0, 10);
            TODATE = $('#ContentPlaceHolder1_txtToDate').val();
            //TODATE = new Date().toJSON().slice(0, 10);
            drawAll();
        });

        function drawAll() {
            drawChart();
            drawChart1();
            drawChart2();
            drawChart3();
        };



        function drawChart() {
            var options = {
                title: 'Lead Generated - Channel wise',
                titleTextStyle: {
                    color: "black",               // color 'red' or '#cc00cc'
                    fontName: "Times New Roman",    // 'Times New Roman'
                    fontSize: 25,               // 12, 18
                    bold: true,                 // true or false
                    italic: true,                // true of false
                },
                width: 1200,
                height: 600,
                bar: { groupWidth: "95%" },
                legend: { position: "none" },
                isStacked: true,
                is3D: true,
                hAxis: {
                    title: 'Channel Type',
                    titleTextStyle: {
                        color: "black",
                        fontName: "Times New Roman",
                        fontSize: 15,
                        bold: true,
                        italic: false
                    }
                },
                vAxis: {
                    title: 'Lead Count',
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
                url: "rptCRMReports.aspx/GetChartData",
                data: '{"FROMDATE" : "' + FROMDATE + '","TODATE" : "' + TODATE + '"}',
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

        function drawChart1() {
            var options = {
                title: 'Lead Assign to Agent',
                titleTextStyle: {
                    color: "black",               // color 'red' or '#cc00cc'
                    fontName: "Times New Roman",    // 'Times New Roman'
                    fontSize: 25,               // 12, 18
                    bold: true,                 // true or false
                    italic: true,                // true of false
                },

                width: 500,
                //height: 00,
                //bar: { groupWidth: "95%" },
                //legend: { position: "none" },
                //isStacked: true,
                is3D: true,
                marginleft: -60,
            };
            $.ajax({
                type: "POST",
                url: "rptCRMReports.aspx/GetChartData1",
                data: '{"FROMDATE" : "' + FROMDATE + '","TODATE" : "' + TODATE + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.PieChart(document.getElementById('ContentPlaceHolder1_chart1'));
                    chart.draw(data, options);
                },
                failure: function (r) {
                    //alert(r.d);
                },
                error: function (r) {
                    //alert(r.d);
                }
            });
        }

        function drawChart2() {
            var options = {
                title: 'Date wise Lead Generated & Call Attempted',
                titleTextStyle: {
                    color: "black",               // color 'red' or '#cc00cc'
                    fontName: "Times New Roman",    // 'Times New Roman'
                    fontSize: 25,               // 12, 18
                    bold: true,                 // true or false
                    italic: true,                // true of false
                },
                curveType: 'function',
                legend: { position: 'bottom', display: false },
               
                width: 1200,
                is3D: true,
                hAxis: {
                    title: 'Date',
                    titleTextStyle: {
                        color: "black",
                        fontName: "Times New Roman",
                        fontSize: 15,
                        bold: true,
                        italic: false
                    }
                },
                vAxis: {
                    title: 'Lead Count',
                    titleTextStyle: {
                        color: "black",
                        fontName: "Times New Roman",
                        fontSize: 15,
                        bold: true,
                        italic: false
                    }
                },
            };
            $.ajax({
                type: "POST",
                url: "rptCRMReports.aspx/GetChartData2",
                data: '{"FROMDATE" : "' + FROMDATE + '","TODATE" : "' + TODATE + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.LineChart(document.getElementById('ContentPlaceHolder1_chart2'));
                    chart.draw(data, options);
                },
                failure: function (r) {
                    //alert(r.d);
                },
                error: function (r) {
                    //alert(r.d);
                }
            });
        }

        function drawChart3() {
            var options = {
                title: 'Lead Assign to Agent',
                //width: 120, height: 120,
                //redFrom: 90, redTo: 100,
                //yellowFrom: 75, yellowTo: 90,
                //minorTicks: 5,
                fontSize: 10,
                is3D: true,
                width: 300, height: 300,
                animation: {
                    duration: 400,
                    easing: 'out',
                    startup: true,
                },
                forceIFrame: true,
                startup: true,
            };
            $.ajax({
                type: "POST",
                url: "rptCRMReports.aspx/GetChartData3",
                data: '{"FROMDATE" : "' + FROMDATE + '","TODATE" : "' + TODATE + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.Gauge(document.getElementById('ContentPlaceHolder1_chart4'));
                    chart.draw(data, options);

                },
                failure: function (r) {
                    //alert(r.d);
                },
                error: function (r) {
                    //alert(r.d);
                }
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; CRM  </strong>Data</h3>
                        <div class="col-md-12 pull-right">
                            <div class="col-md-2">&nbsp;</div>
                            <div class="col-md-2">&nbsp;</div>
                            <div class="col-md-2">&nbsp;</div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="col-md-2 control-label"></label>
                                    <div class="col-md-10 col-xs-12 pull-right">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="col-md-2 control-label pull-left" style="padding-top: 7px;">To</label>
                                    <div class="col-md-10 col-xs-12 pull-right">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClientClick='return drawAll();'>
<span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
                    <div class="panel-body">
                        <div class="row">
                            <%--<div class="box-body divhorizontal" style="overflow-x: scroll;">--%>
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="col-md-12" style="margin-top: 10px;">
                                        <center>
                                            <h4 style="font-weight: bold;">Total Lead Generated - Channel wise</h4>
                                        </center>
                                        <div id="chart" runat="server" style="width: 1300px; height: 700px; margin-left: 0px; border: thin; border-color: orange;">
                                        </div>

                                    </div>
                                    <div class="col-md-6" style="margin-top: 20px;">
                                        <div id="chart1" runat="server" style="width: 300px; height: 300px; margin-left: 0px; border: thin; border-color: orange;">
                                        </div>
                                        <center>
                                            <h4 style="font-weight: bold;">Lead Assign to Agent</h4>
                                        </center>
                                    </div>
                                    <div class="col-md-6" style="margin-top: 20px;">
                                        <div id="chart4" runat="server" style="width: 500px; height: 400px; margin-left: 0px; border: thin; border-color: orange;">
                                        </div>
                                        <h4 style="font-weight: bold;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Average Call per day</h4>

                                    </div>
                                    <div class="col-md-12" style="margin-top: 10px;">
                                        <div id="chart2" runat="server" style="width: 1300px; height: 600px; margin-left: 0px; border: thin; border-color: orange;">
                                        </div>
                                        <br />
                                        <center>
                                            <h4 style="font-weight: bold;">Lead Generated v/s Call Attempted - Datewise</h4>
                                        </center>
                                    </div>
                                </div>
                                <%--</div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptCRMCallLogs" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptCRM" />

</asp:Content>
