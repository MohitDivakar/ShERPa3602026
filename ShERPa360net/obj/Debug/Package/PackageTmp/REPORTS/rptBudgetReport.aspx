<%@ Page Title="Budget Report" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptBudgetReport.aspx.cs" Inherits="ShERPa360net.REPORTS.rptBudgetReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Budget Report</title>

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../img/collapse_blue.png";
            } else {
                div.style.display = "none";
                img.src = "../img/expand_blue.png";
            }
        }

    </script>

    <style type="text/css">
        /*body {
            font-family: Times New Roman;
            font-size: 10pt;
        }*/

        .Grid th {
            /*background-color: #DF7401 !important;*/
            color: black;
            /*font-size: 10pt;*/
            line-height: 200%;
            text-align: center !important;
            /*font-weight: 300;*/
            /*border-color: gray;*/
        }

        .Grid td {
            /*background-color: #F3E2A9;*/
            color: black;
            /*font-size: 10pt;*/
            line-height: 200%;
            text-align: center;
            font-weight: 300;
        }

        .ChildGrid th {
            /*background-color: Maroon !important;*/
            color: #faa61a;
            /*font-size: 10pt;*/
            /*font-weight: 200;*/
        }

        .ChildGrid td {
            /*background-color: Orange;*/
            color: #faa61a;
            /*font-size: 10pt;*/
            text-align: center;
            font-weight: 200;
        }

        .ChildGrid2 th {
            /*background-color: blue !important;*/
            color: #184f90;
            /*font-size: 10pt;*/
            /*font-weight: 200;*/
        }

        .ChildGrid2 td {
            /*background-color: darkgoldenrod;*/
            color: #184f90;
            /*font-size: 10pt;*/
            text-align: center;
            font-weight: 200;
        }
        /* svg g text{
font-size:.7em;
}*/


        #ContentPlaceHolder1_chart2 text {
            fill: white !important;
            font-family: 'Comfortaa' !important;
            font-size: 10px !important;
        }

            #ContentPlaceHolder1_chart2 text:nth-child(1) {
                fill: yellow;
                font-size: 33;
                font-weight: 700;
                font-family: 'Orbitron';
            }

        /*svg g g  text{*/ /*from w  w  w  .j a v  a2s  .  com*/
        /*font-size:10px !important;
        }*/
    </style>

    <%--<script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>--%>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        var PLANTCODE, BUDGETCODE;
        $(window).load(function () {
            //debugger;
            //google.load('visualization', '1', { 'packages': ['corechart'] });
            //google.load('visualization', '1', { 'packages': ['gauge'] });
            //google.setOnLoadCallback(drawChart);
            //google.setOnLoadCallback(drawChart1);
            //google.setOnLoadCallback(drawChart2);
            //google.setOnLoadCallback(drawChart3);

            //FROMDATE = $('#ContentPlaceHolder1_txtFromDate').val();
            //FROMDATE = new Date().toJSON().slice(0, 10);
            //TODATE = $('#ContentPlaceHolder1_txtToDate').val();
            //TODATE = new Date().toJSON().slice(0, 10);
            //drawAll();
        });



        function GetGraph() {
            //debugger;
            google.load('visualization', '1', { 'packages': ['corechart'] });
            google.load('visualization', '1', { 'packages': ['gauge'] });
            //google.charts.load('current', { 'packages': ['corechart', 'bar'] });
            google.setOnLoadCallback(drawChart1);
            PLANTCODE = $('#ContentPlaceHolder1_ddlPlantCode option:selected').val();
            BUDGETCODE = $('#ContentPlaceHolder1_ddlBudgetCode option:selected').val();
            //drawChart();
            drawChart1();
            //drawChart2();
            drawChart2();
        };
        function drawChart1() {
            var options = {

                title: 'Budget',
                subtitle: "Total v/s Used",
                titleTextStyle: {
                    color: "black",               // color 'red' or '#cc00cc'
                    fontName: "Times New Roman",    // 'Times New Roman'
                    fontSize: 55,               // 12, 18
                    bold: true,                 // true or false
                    italic: true,                // true of false

                },
                width: 600,
                height: 300,
                bar: { groupWidth: "55%" },
                legend: {
                    position: "right"
                },
                isStacked: true,
                is3D: true,
                hAxis: {
                    title: 'Budget For',
                    titleTextStyle: {
                        color: "black",
                        fontName: "Times New Roman",
                        fontSize: 20,
                        bold: true,
                        italic: false
                    }
                },
                vAxis: {
                    title: 'Budget (Rs.)',
                    titleTextStyle: {
                        color: "black",
                        fontName: "Times New Roman",
                        fontSize: 20,
                        bold: true,
                        italic: false
                    }
                },
                annotations: {
                    alwaysOutside: false,
                    textStyle: {
                        fontSize: 12,
                        auraColor: 'none'
                    }
                }
            };
            $.ajax({
                type: "POST",
                url: "rptBudgetReport.aspx/GetChartData1",
                data: '{"PLANTCODE" : "' + PLANTCODE + '","BUDGETCODE" : "' + BUDGETCODE + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    //alert(data);
                    //debugger;
                    var view = new google.visualization.DataView(data);
                    view.setColumns([0, 1, {
                        calc: 'stringify',
                        sourceColumn: 1,
                        type: 'string',
                        role: 'annotation'
                    },
                        2,
                        {
                            calc: 'stringify',
                            sourceColumn: 2,
                            type: 'string',
                            role: 'annotation'
                        },
                        3,
                        {
                            calc: 'stringify',
                            sourceColumn: 3,
                            type: 'string',
                            role: 'annotation'
                        }]);

                    var chart = new google.visualization.ColumnChart(document.getElementById('ContentPlaceHolder1_chart1'));

                    //google.visualization.events.addListener(chart, 'ready', function () {
                    //    var rows = [];
                    //    data.getSortedRows({ column: 0 }).forEach(function (row) {
                    //        rows.push({ row: row, column: 1 });
                    //    });
                    //    chart.setSelection(rows);
                    //});
                    chart.draw(view, options);
                },
                failure: function (r) {
                    //alert('Fail : ' + r.d);
                },
                error: function (r) {
                    //debugger;
                    //alert('Error : ' + r.d);
                }
            });
        }

        function drawChart2() {
            var options = {
                title: 'Budget Used in Percentage (%)',
                //width: 120, height: 120,
                redFrom: 90, redTo: 100,
                yellowFrom: 75, yellowTo: 90,
                minorTicks: 5,
                fontSize: 2,
                textStyle: {
                    fontSize: 100
                },
                fontSize: 2,
                is3D: true,
                width: 200, height: 200,
                animation: {
                    duration: 400,
                    easing: 'out',
                    startup: true,
                },
                forceIFrame: true,
                startup: true,
                legend: {
                    position: "bottom"
                },
            };
            $.ajax({
                type: "POST",
                url: "rptBudgetReport.aspx/GetChartData3",
                data: '{"PLANTCODE" : "' + PLANTCODE + '","BUDGETCODE" : "' + BUDGETCODE + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.Gauge(document.getElementById('ContentPlaceHolder1_chart2'));
                    //setInterval(function () {
                    //    data.setValue(0, 1, 40 + Math.round(60 * Math.random()));
                    //    chart.draw(data, options);
                    //}, 13000);
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
    <asp:UpdatePanel ID="upd1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlPlantCode" EventName="SelectedIndexChanged" />
            <%--<asp:PostBackTrigger ControlID="ddlPlantCode" />--%>
        </Triggers>
        <ContentTemplate>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Budget Report  </strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Plant : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPlantCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Budget Code : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlBudgetCode" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearh_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div id="mobile_View">
                                    <div class="col-md-1" style="margin-top: 5px;">&nbsp;</div>
                                    <div class="col-md-3" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-9 col-xs-12">
                                            </div>
                                        </div>
                                    </div>
                                </div>
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
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="col-md-3" style="margin-top: 20px;">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3" style="margin-top: 20px;">
                                        <center>
                                            <div id="chart1" runat="server">
                                            </div>
                                            <br />
                                            <br />
                                            <h4 style="font-weight: bold;">Budget - Total v/s Used</h4>
                                        </center>
                                    </div>
                                    <div class="col-md-3" style="margin-top: 20px;">
                                        &nbsp;
                                    </div>
                                    <div class="col-md-3" style="margin-top: 20px;">
                                        <center>
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <div id="chart2" runat="server">
                                            </div>
                                            <br />
                                            <br />
                                            <br />
                                            <h4 style="font-weight: bold;">Budget Used (%)</h4>
                                        </center>
                                    </div>
                                </div>
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
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="nowrap Grid" Style="border-color: whitesmoke !important;"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <a href="JavaScript:divexpandcollapse('div<%# Eval("PLANTCODE") %>');">
                                                            <img id="imgdiv<%# Eval("PLANTCODE") %>" width="14px" border="0" src="../img/expand_blue.png" />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Plant Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPLANTCODE" Text='<%# Bind("PLANTCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Plant Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPLANT" Text='<%# Bind("PLANT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Plant Budget">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTOTALPLANTBUDGET" Text='<%# Bind("TOTALPLANTBUDGET") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budget Used">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPLANTUSED" Text='<%# Bind("PLANTUSED") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Budget Remains">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPLANTREMAIN" Text='<%# Bind("PLANTREMAIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%">
                                                                <div id="div<%# Eval("PLANTCODE") %>" style="display: none; overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;" class="box-body divhorizontal">
                                                                    <asp:GridView ID="gvInnerList" runat="server" AutoGenerateColumns="false" CssClass="nowrap ChildGrid" CellSpacing="0" Style="border-color: whitesmoke !important;"
                                                                        ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvInnerList_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <a href="JavaScript:divexpandcollapse('div<%# Eval("BUDGETCODE") %>');">
                                                                                        <img id="imgdiv<%# Eval("BUDGETCODE") %>" width="14px" border="0" src="../img/expand_blue.png" />
                                                                                    </a>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Budget Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBUDGETCODE" runat="server" Text='<%# Eval("BUDGETCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Budget For">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblBUDGETFOR" runat="server" Text='<%# Eval("BUDGETFOR") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total Budget">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTOTALBUDGET" runat="server" Text='<%# Eval("TOTALBUDGET") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Budget Used">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUSED" runat="server" Text='<%# Eval("USED") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Budget Remains">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblREMAIN" runat="server" Text='<%# Eval("REMAIN") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <tr>
                                                                                        <td colspan="100%">
                                                                                            <div id="div<%# Eval("BUDGETCODE") %>" style="display: none; overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;" class="box-body divhorizontal">
                                                                                                <asp:GridView ID="gvInnerListNew" runat="server" AutoGenerateColumns="false" CssClass="nowrap ChildGrid2" CellSpacing="0" Style="border-color: whitesmoke !important;"
                                                                                                    ShowHeaderWhenEmpty="true" Width="100%" EmptyDataText="No Record Found!">

                                                                                                    <Columns>
                                                                                                        <asp:TemplateField HeaderText="PO Dt.">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblPODT" runat="server" Text='<%# Eval("PODT") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="PO No.">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblPONO" runat="server" Text='<%# Eval("PONO") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Vendor">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblVENDOR" runat="server" Text='<%# Eval("VENDOR") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Item Id" Visible="false">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblITEMID" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Item Code">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Item Desc.">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="PO Qty">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblPOQTY" runat="server" Text='<%# Eval("POQTY") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Rate">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblBRATE" runat="server" Text='<%# Eval("BRATE") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Tax">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblTAXAMT" runat="server" Text='<%# Eval("TAXAMT") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="Total Amt.">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="lblCAMOUNT" runat="server" Text='<%# Eval("CAMOUNT") %>'></asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                </asp:GridView>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
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

    <input type="hidden" id="menutabid" value="tsmRptBudgetReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptFI" runat="server" />

</asp:Content>
