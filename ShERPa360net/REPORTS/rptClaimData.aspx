<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptClaimData.aspx.cs" Inherits="ShERPa360net.REPORTS.rptClaimData" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Claim Report</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />
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

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
            //$("#myInput").on("keyup", function () {
            //    var value = $(this).val().toLowerCase();
            //    $("#ContentPlaceHolder1_gvList tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {
            if ($("#ContentPlaceHolder1_gvAllList tr").length > 2) {
                $("#ContentPlaceHolder1_gvAllList").DataTable({
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
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; DashBoard  </strong></h3>
                    </div>
                    <div class="row" style="margin-top: 10px !important;">
                        <fieldset class="scheduler-border shadow" runat="server" id="Fieldset1">
                            <%--<legend class="scheduler-border">OverAll</legend>--%>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkPENDINGFORCLAIM" runat="server" CssClass="tile tile-info" BackColor="#3E8768" BorderColor="#3E8768">
                                    <asp:Label ID="lblPENDINGFORCLAIM" runat="server" Text="0"></asp:Label>
                                    <p>PENDING FOR CLAIM</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkPENDINGFORSETTLE" runat="server" CssClass="tile tile-success">
                                    <asp:Label ID="lblPENDINGFORSETTLE" runat="server" Text="0"></asp:Label>
                                    <p>PENDING FOR SETTLE</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkCLAIMSETTLED" runat="server" CssClass="tile tile-primary">
                                    <asp:Label ID="lblCLAIMSETTLED" runat="server" Text="0"></asp:Label>
                                    <p>CLAIM SETTLED</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkCLAIMREJECTED" runat="server" CssClass="tile tile-warning">
                                    <asp:Label ID="lblCLAIMREJECTED" runat="server" Text="0"></asp:Label>
                                    <p>CLAIM REJECTED</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkCLAIMCLOSED" runat="server" CssClass="tile tile-danger">
                                    <asp:Label ID="lblCLAIMCLOSED" runat="server" Text="0"></asp:Label>
                                    <p>CLAIM CLOSED</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkAMOUNTRECEIVED" runat="server" CssClass="tile tile-default" BackColor="Red" BorderColor="Red">
                                    <asp:Label ID="lblAMOUNTRECEIVED" runat="server" Text="0" ForeColor="White"></asp:Label>
                                    <p style="color: white;">AMOUNT RECEIVED</p>
                                </asp:LinkButton>
                            </div>

                            <%--<div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnktdapproved" runat="server" CssClass="tile tile-info" BackColor="#3E8768" BorderColor="#3E8768">
                                    <asp:Label ID="lbtdapproved" runat="server" Text="0"></asp:Label>
                                    <p>Total Approved</p>
                                </asp:LinkButton>
                            </div>--%>
                        </fieldset>

                        <%--        <fieldset class="scheduler-border shadow" runat="server" id="Fieldset2">
                            <legend class="scheduler-border">OverAll</legend>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovlisted" runat="server" CssClass="tile tile-info" BackColor="#3E8768" BorderColor="#3E8768">
                                    <asp:Label ID="lbovlisted" runat="server" Text="0"></asp:Label>
                                    <p>Total Listed</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovtested" runat="server" CssClass="tile tile-success">
                                    <asp:Label ID="lbovtested" runat="server" Text="0"></asp:Label>
                                    <p>Total Tested</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovpurchase" runat="server" CssClass="tile tile-primary">
                                    <asp:Label ID="lbovpurchase" runat="server" Text="0"></asp:Label>
                                    <p>Total Purchase</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovorderreceived" runat="server" CssClass="tile tile-warning">
                                    <asp:Label ID="lbovorderreceived" runat="server" Text="0"></asp:Label>
                                    <p>Total Order Received</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovreturn" runat="server" CssClass="tile tile-danger">
                                    <asp:Label ID="lbovreturn" runat="server" Text="0"></asp:Label>
                                    <p>Total Return</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovrejected" runat="server" CssClass="tile tile-default" BackColor="Red" BorderColor="Red">
                                    <asp:Label ID="lbovrejected" runat="server" Text="0" ForeColor="White"></asp:Label>
                                    <p style="color: white;">Total Rejected</p>
                                </asp:LinkButton>
                            </div>
                        </fieldset>--%>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Claim Dashboard  </strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date From : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Job ID : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtJobID" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Width="205"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>





                                </div>

                                <div class="col-md-12 TopMarg">
                                    <%--<div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">PO No : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtRefDocNo" runat="server" CssClass="form-control" Width="205"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Plant : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control" Width="205" OnSelectedIndexChanged="ddlPlantCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Location : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" Width="205"></asp:DropDownList>
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
                                                    <span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                    <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success" Text="Export " OnClick="lnkExport_Click">
                                                    <span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;ONS PO Register</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divAll" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvAllList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="true" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <%--<Columns>
                                                            <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                            <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                            <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                            <asp:BoundField DataField="VENDORNAME" HeaderText="Vendor" />
                                                            <asp:BoundField DataField="PMTTERMSDESC" HeaderText="Pay Terms" />
                                                            <asp:BoundField DataField="PLANTCODE" HeaderText="Plant" />
                                                            <asp:BoundField DataField="LOCATIONCODE" HeaderText="Location" />
                                                            <asp:BoundField DataField="DIFF" HeaderText="Day Diff." />
                                                            <asp:BoundField DataField="USERNAME" HeaderText="Create By" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                        </Columns>--%>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranCRMClaimReq" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />

</asp:Content>
