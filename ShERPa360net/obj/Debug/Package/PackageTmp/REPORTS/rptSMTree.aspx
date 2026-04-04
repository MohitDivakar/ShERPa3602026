<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptSMTree.aspx.cs" Inherits="ShERPa360net.REPORTS.rptSMTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Tree View</title>
    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <script type="text/javascript">
        google.load('visualization', '1', { 'packages': ['orgchart'] });
        google.setOnLoadCallback(drawChart3);
        function drawChart3() {
            debugger;
            var options = {
                title: 'ASM Tree View',
                //curveType: 'function',
                //legend: { position: 'bottom' },
                //width: 1500,
                //is3D: true
                allowCollapse: true,
                size: "large"

            };
            debugger;
            var state = $('#ContentPlaceHolder1_ddlState').val();
            //alert(state);
            $.ajax({
                type: "POST",
                url: "rptSMTree.aspx/GetChartData3",
                data: '{"STATE" : "' + state + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //alert(r.d);
                    var data = google.visualization.arrayToDataTable(r.d);
                    var chart = new google.visualization.OrgChart(document.getElementById('ContentPlaceHolder1_chart3'));
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
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; SM  </strong>Tree View</h3>
                        <div class="col-md-12 pull-right">
                            <div class="col-md-2">&nbsp;</div>
                            <div class="col-md-2">&nbsp;</div>
                            <div class="col-md-2">&nbsp;</div>
                            <div class="col-md-2">&nbsp;</div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="col-md-2 control-label pull-left" style="padding-top: 7px;">State </label>
                                    <div class="col-md-10 col-xs-12 pull-right">
                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-2">
                                <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClientClick='return drawChart3();'><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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

                                        <div id="chart3" runat="server" style="width: 900px; height: 500px; margin-left: -150px;">
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmrptSMTree" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmReportSys" />

</asp:Content>
