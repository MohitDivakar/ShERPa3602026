<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptGradewiselisting.aspx.cs" Inherits="ShERPa360net.REPORTS.rptGradewiselisting" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Brand wise Listing Report</title>
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        body .table thead th {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }

        .header {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }
    </style>

    <%--    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.aspsnippets.com/questions/922520/Create-multi-series-Area-Line-Chart-from-database-using-ChartJS-in-ASPNet"></script>
    --%>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.7.2/Chart.js"></script>
    <script>
        function ShowLoading() {
            $("#progress").show();
        }
        function HideLoading() {
            $("#progress").hide();
        }

        $("#lblstate").css("display", "none");
        function LoadChartReport() {
            ShowLoading();

            $(".divhorizontal").html("");
            $(".divhorizontal").html("<canvas id=\"makepurchasecanvas\" class=\"makepurchasecanvas\" height=\"100px\"></canvas>");
            // document.getElementById("makepurchasecanvas").destroy();
            var obj = {};
            obj.frmdate = $('.txtFromDocDate').val();
            obj.todate = $('.txtToDocDate').val();
            obj.plantid = $('.ddlPlantCode :selected').val();

            //if (obj.stateId == 0) {

            //    $("#lblstate").css("display", "block");
            //    return false;
            //}
            //else {

            //    $("#lblstate").css("display", "none");
            //}
            jQuery.ajax({
                url: 'rptGradewiselisting.aspx/GetChartData',
                type: "POST",
                dataType: "json",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    var label = response.d.Labels;
                    var datasetLabels = response.d.DatasetLabels;
                    var datasetDatas = response.d.DatasetDatas;
                    var dataSets = [];
                    for (var i = 0; i < datasetDatas.length; i++) {
                        var dataSet = {
                            label: datasetLabels[i],
                            backgroundColor: GetRandomColor(),
                            data: datasetDatas[i]
                        };
                        dataSets.push(dataSet);
                    }
                    HideLoading();
                    /*$(".makepurchasecanvas").clearRect(0, 0, canvas.width, canvas.height);*/
                    var ctx = document.getElementById("makepurchasecanvas").getContext('2d');
                    new Chart(ctx, {
                        type: 'line',
                        data: { labels: label, datasets: dataSets },
                        //options: {
                        //    responsive: true,
                        //    maintainAspectRatio: false,
                        //    title: { display: true, text: 'Month wise sale' },
                        //    legend: { display: true, position: "top" },
                        //    scales: { yAxes: [{ ticks: { beginAtZero: true } }] }
                        //}
                        options: {
                            title:
                            {
                                display: true,
                                fontColor: '#2E4053',
                                fontStyle: 'bold',
                                fontSize: 14
                            },
                            legend: {
                                display: false
                            },
                            responsive: true,
                            interaction: {
                                mode: 'index',
                                intersect: true,
                            },
                            stacked: true,
                            maintainAspectRatio: true,
                            plugins: {
                                title: {
                                    display: true,
                                    text: 'Chart.js Line Chart - Multi Axis'
                                }
                            },
                            tooltips: {
                                "enabled": true,
                                "intersect": false,
                                title:
                                {
                                    display: true,
                                    fontColor: '#2E4053',
                                    fontStyle: 'bold',
                                    fontSize: 14
                                },


                            },
                            scales: {
                                y: {
                                    type: 'linear',
                                    display: true,
                                    position: 'left',
                                },
                                y1: {
                                    type: 'linear',
                                    display: true,
                                    position: 'right',

                                    // grid line settings
                                    grid: {
                                        drawOnChartArea: false, // only want the grid lines for one axis to show up
                                    },
                                },
                            }
                            ,
                            animation: {
                                onComplete: function () {
                                    var ctx = this.chart.ctx;
                                    ctx.font = Chart.helpers.fontString(Chart.defaults.global.defaultFontFamily, 'normal', Chart.defaults.global.defaultFontFamily);
                                    ctx.fillStyle = "black";
                                    ctx.textAlign = 'left';
                                    ctx.textBaseline = 'middle';
                                    //ctx.html("<div>manan</div>");
                                    this.data.datasets.forEach(function (dataset) {
                                        for (var i = 0; i < dataset.data.length; i++) {
                                            for (var key in dataset._meta) {
                                                var model = dataset._meta[key].data[i]._model;
                                                ctx.fillText(dataset.data[i], model.x, model.y);
                                                ctx.fillStyle = "steelblue";
                                                //ctx.backgroundColor = "#ccc";   
                                                //dataset.data[i].showTooltips();
                                            }
                                        }
                                    });
                                    // ctx.destroy();
                                }
                            }
                        },
                    });




                },
                error: function (response) {
                    var label = response.d.Labels;
                    var datasetLabels = response.d.DatasetLabels;
                    var datasetDatas = response.d.DatasetDatas;
                    var dataSets = [];
                    for (var i = 0; i < datasetDatas.length; i++) {
                        var dataSet = {
                            label: datasetLabels[i],
                            backgroundColor: GetRandomColor(),
                            data: datasetDatas[i]
                        };
                        dataSets.push(dataSet);
                    }

                    var ctx = document.getElementById("makepurchasecanvas").getContext('2d');
                    //new Chart(ctx, {
                    //    type: 'line',
                    //    data: { labels: label, datasets: dataSets },
                    //    options: {
                    //        responsive: true,
                    //        maintainAspectRatio: false,
                    //        title: { display: true, text: 'Month wise sale' },
                    //        legend: { display: true, position: "top" },
                    //        scales: { yAxes: [{ ticks: { beginAtZero: true } }] }
                    //    }
                    //});
                    new Chart(ctx).Line(chartData, {
                        showTooltips: false,
                        onAnimationComplete: function () {

                            var ctx = this.chart.ctx;
                            ctx.font = this.scale.font;
                            ctx.fillStyle = this.scale.textColor
                            ctx.textAlign = "center";
                            ctx.textBaseline = "bottom";

                            this.datasets.forEach(function (dataset) {
                                dataset.points.forEach(function (points) {
                                    ctx.fillText(points.value, points.x, points.y - 10);
                                });
                            })
                        }
                    });
                }
            });
            ;

            return false;
        }



        function GetRandomColor() {
            var trans = '0.3';
            var color = 'rgba(';
            for (var i = 0; i < 3; i++) {
                color += Math.floor(Math.random() * 255) + ',';
            }
            color += trans + ')';
            return color;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Grade wise Listing </strong>Report</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">From : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDocDate" runat="server" CssClass="form-control datepicker txtFromDocDate" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDocDate" runat="server" CssClass="form-control datepicker txtToDocDate" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%-- <div class="col-md-3" runat="server">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">State : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control ddlState" ClientIDMode="Static" placeholder="state">
                                                    <asp:ListItem Text="SELECT" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="GUJARAT" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="KARNATAKA" Value="18"></asp:ListItem>
                                                </asp:DropDownList>
                                              <asp:Label ID="lblstate" CssClass="lblstate"  runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Please Select Sate</asp:Label>
                                            </div>
                                        </div>
                                    </div>--%>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Plant Name. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control ddlPlantCode"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label"></label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success " Text="Search Report" OnClientClick="return LoadChartReport();" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i></span></asp:LinkButton>
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
    </div>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">

                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 1000px !important;">
                                        <%--<canvas id="makepurchasecanvas" class="makepurchasecanvas"></canvas>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



    <%--For Data Table Jquery--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMobexVendorVisit" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
