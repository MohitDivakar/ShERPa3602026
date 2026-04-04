<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptSOData.aspx.cs" Inherits="ShERPa360net.REPORTS.rptSOData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <title>Pending SO Data</title>
    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />--%>
    <style>
        .icon1 {
            text-align: center;
            vertical-align: middle;
            height: 116px;
            width: 116px;
            border-radius: 50%;
            color: #999999;
            margin: 0 auto 20px;
            border: 4px solid #fe970a;
            transition: all 0.2s;
            -webkit-transition: all 0.2s;
        }

        .imgcircle {
            height: 65px !important;
            width: 65px !important;
            margin-top: 20px !important;
        }

        .shadow {
            -moz-box-shadow: 3px 3px 5px 6px #fe970a !important;
            -webkit-box-shadow: 0 0 5px #cccccc87 !important;
            border: 5px solid #cccccc87 !important;
            box-shadow: 0 0 5px #cccccc87 !important;
            background: #fff;
        }
    </style>

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
            top: -14px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
            background-color: #ffffff;
        }
    </style>

    <style type="text/css">
        .chclass {
        }

            .chclass label {
                position: relative;
                top: -2px;
                left: -5px;
            }

            .chclass input[type="checkbox"] {
                margin: 10px 10px 0px;
            }

            .chclass label {
                margin: 10px 10px 0px;
            }
        /*  .chclass input {
                position: absolute;
                top: 10px;
                left: 10px;
            }*/
    </style>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {
            if ($("#ContentPlaceHolder1_gvAllList tr").length > 2) {
                $("#ContentPlaceHolder1_gvAllList").DataTable({
                    paging: false,
                    dom: 'Bfrtip',
                    buttons: [
                        {
                            extend: 'collection',
                            text: 'Export',
                            buttons: [
                                'copy',
                                'excel',
                                'csv',
                                'pdf',
                                'print'
                            ]
                        }
                    ]

                });
                //$('#ContentPlaceHolder1_gvAllList_info').hide();
                //$('#ContentPlaceHolder1_gvAllList_paginate').hide();
                //$('#ContentPlaceHolder1_gvAllList_length').hide();
            }
            updateTimers();
            setInterval(updateTimers, 1000);
        }

        function updateTimers() {
            var rows = document.querySelectorAll("[id^='timer_']");
            var now = new Date();

            rows.forEach(function (row) {
                var startTime = new Date(row.getAttribute("data-starttime"));
                var elapsed = now - startTime;

                var hours = Math.floor(elapsed / 3600000);
                var minutes = Math.floor((elapsed % 3600000) / 60000);
                var seconds = Math.floor((elapsed % 60000) / 1000);

                row.innerText = hours.toString().padStart(2, '0') + ":" +
                    minutes.toString().padStart(2, '0') + ":" +
                    seconds.toString().padStart(2, '0');
            });
        }

        // Call the updateTimers function every second (adjust interval as needed)
        //setInterval(updateTimers, 1000);

        //function SetSoExpireTimer(id, socreateddate) {
        //    debugger
        //    var countDownDate = new Date(socreateddate).getTime();
        //    var x = setInterval(
        //        function () {
        //            // Get today's date and time
        //            var now = new Date().getTime();
        //            id = "#" + id;

        //            // Find the distance between now and the count down date
        //            var distance = countDownDate - now;

        //            // Time calculations for days, hours, minutes and seconds
        //            var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        //            var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        //            var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        //            var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        //            // Output the result in an element with id="demo"
        //            $("" + id + "").val(hours + "h " + minutes + "m " + seconds + "s ");

        //            // If the count down is over, write some text 
        //            if (distance < 0) {
        //                clearInterval(x);
        //                $("" + id + "").val("EXPIRED");
        //            }
        //        }, 1000
        //    );
        //}
    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<asp:UpdatePanel ID="updSalesFrom" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rblsalesFrom" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="gvAllList" />
        </Triggers>
        <ContentTemplate>--%>
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Pending SO Data  </strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label">Plant : </label>
                                            <div class="col-md-10 col-xs-12">
                                                <div class="input-group">
                                                    <%--<asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                    <asp:RadioButtonList ID="rblPlantList" runat="server" RepeatLayout="Table" CssClass="chclass" RepeatDirection="Horizontal"></asp:RadioButtonList>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label"></label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <%--<asp:LinkButton runat="server" ID="lnkNewPO" CssClass="btn btn-success " Text="New PO" PostBackUrl="~/MM/MaterialInwardFromPo.aspx?Mode=I"><i class="fa fa-file-text"></i></asp:LinkButton>--%>
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success " Text="Search Invoice" OnClick="lnkSerch_Click">
                                                        <span tooltip="Search" flow="down"><i class="fa fa-search"></i></span>
                                                    </asp:LinkButton>
                                                    <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success" Text="Export " OnClick="lnkExport_Click">
                                                    <span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Sales From : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <%--<asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control"></asp:DropDownList>--%>

                                                    <asp:RadioButtonList ID="rblsalesFrom" runat="server" RepeatLayout="Table" CssClass="chclass" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblsalesFrom_SelectedIndexChanged">
                                                        <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Amazon" Value="10906"></asp:ListItem>
                                                        <asp:ListItem Text="Mobex Website" Value="11193"></asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4" runat="server" visible="false" id="divAgent">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Agent : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <%--<asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                    <asp:RadioButtonList ID="rblAgent" runat="server" RepeatLayout="Table" CssClass="chclass" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="KIOSK" Value="0000050118"></asp:ListItem>
                                                        <asp:ListItem Text="OTHER THAN KIOSK" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </div>
                                            </div>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Pending SO Data</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divAll" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll; height: 500px;">
                                                    <asp:GridView ID="gvAllList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <emptydatatemplate>
                                                            No Record Found!
                                                        </emptydatatemplate>
                                                        <headerstyle cssclass="header" />
                                                        <columns>
                                                            <asp:BoundField DataField="AGEING" HeaderText="Ageing" />
                                                            <%--<asp:BoundField DataField="PLANTCD" HeaderText="Plant" />--%>
                                                            <asp:TemplateField HeaderText="Plant">
                                                                <itemtemplate>
                                                                    <asp:Label ID="lblPlant" Visible="false" runat="server" Text='<%# Eval("PLANTCD") %>'></asp:Label>
                                                                    <asp:Label ID="lblPlantDesc" runat="server" Text='<%# Eval("PLANTCDDESC") %>'></asp:Label>
                                                                </itemtemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="LOCCD" HeaderText="Location" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                            <asp:TemplateField HeaderText="TAT">
                                                                <itemtemplate>
                                                                    <span style="color: green!important; font-weight: bold!important; font-size: 15px!important" id='<%# "timer_" + Eval("SONO") + "_" + Eval("SoCreatedDate")  %>'>00:00:00</span>
                                                                </itemtemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="SODT" HeaderText="SO Dt." />
                                                            <asp:BoundField DataField="RESERVEDDATE" HeaderText="Assign Date" />
                                                            <asp:BoundField DataField="DELIDT" HeaderText="Deli. Dt." />
                                                            <%--<asp:BoundField DataField="SONO" HeaderText="SO No." />--%>
                                                            <asp:TemplateField HeaderText="SO No.">
                                                                <itemtemplate>
                                                                    <asp:LinkButton runat="server" ID="lnkSONO" Text='<%# Eval("SONO") %>' OnClick="lnkSONO_Click"></asp:LinkButton>
                                                                </itemtemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="REFNO" HeaderText="Order ID" />
                                                            <asp:BoundField DataField="CITY" HeaderText="So City" />
                                                            <asp:BoundField DataField="SALESCHANNEL" HeaderText="Sales From" />
                                                            <asp:BoundField DataField="SCHEMEDESC" HeaderText="Sales Scheme" />
                                                            <asp:BoundField DataField="PAYMODEDESC" HeaderText="Payment Mode" />
                                                            <asp:BoundField DataField="COMMAGENT" HeaderText="Agent" />
                                                            <asp:BoundField DataField="LISTINGID" HeaderText="Listing ID" />
                                                            <asp:BoundField DataField="LISTINGSTATUS" HeaderText="Listing Status" />
                                                            <asp:BoundField DataField="LISTINGTYPE" HeaderText="Listing Type" />
                                                            <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                            <asp:BoundField DataField="JOBCREATEDDATE" HeaderText="Job Created Date" />
                                                            <asp:BoundField DataField="JOBSTATDESC" HeaderText="Job Status" />
                                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                            <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc" />
                                                            <asp:BoundField DataField="SOQTY" HeaderText="SO Qty" />
                                                            <%--<asp:BoundField DataField="RATE" HeaderText="Rate" />
                                                            <asp:BoundField DataField="CAMOUNT" HeaderText="Total Amt." />--%>
                                                            <asp:BoundField DataField="STATUSDESC" HeaderText="SO Status" />
                                                            <asp:BoundField DataField="STODATE" HeaderText="STO Dt. (Sent/Inw.)" />
                                                            <asp:BoundField DataField="ENTRYBY" HeaderText="Create By" />
                                                        </columns>
                                                    </asp:GridView>


                                                </div>
                                            </div>


                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptSDOpenSO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSD" runat="server" />

</asp:Content>
