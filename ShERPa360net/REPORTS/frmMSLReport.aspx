<%@ Page Title="MSL Report" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="frmMSLReport.aspx.cs" Inherits="ShERPa360net.REPORTS.frmMSLReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>MSL Report</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>


    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        .btnAqua {
            width: 60px;
            background-color: #faa61a;
            color: white;
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
        /*  .chclass input {
                position: absolute;
                top: 10px;
                left: 10px;
            }*/
    </style>


    <style type="text/css">
        .columnwidth {
            width: 50px;
        }
    </style>
    <script>
        $(document).ready(function () {
            LoadMslGridJqueryPagination();
        });
        function LoadMslGridJqueryPagination() {

            //$("#ContentPlaceHolder1_gvMslReport").DataTable({
            //    dom: 'Bfrtip',
            //    buttons: [
            //        {
            //            extend: 'collection',
            //            text: 'Export',
            //            buttons: [
            //                'copy',
            //                'excel',
            //                'csv',
            //                'pdf',
            //                'print'
            //            ]
            //        }
            //    ]
            //});
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; MSL </strong><b>Report</b></h3>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label pull-left" style="padding-top: 7px;">Plant Code</label>
                                            <div class="col-md-9 col-xs-10">
                                                <asp:DropDownList ID="ddlPlancode" OnSelectedIndexChanged="ddlPlancode_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">

                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <label class="col-md-1 control-label pull-left" style="padding-top: 7px;">Location Code</label>
                                            <div class="col-md-11 col-xs-7">
                                                <asp:CheckBoxList ID="chkLocation" TextAlign="Right" runat="server" RepeatDirection="Horizontal" RepeatColumns="25" CssClass="chclass"></asp:CheckBoxList>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-" OnClick="lnkSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>


    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;MSL</strong>Report</h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">

                                                    <asp:GridView ID="gvMslReport" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">

                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField HeaderText="CMP ID" DataField="CMPID" Visible="false" />
                                                            <asp:BoundField HeaderText="ASIN" DataField="AMAZON" />
                                                            <asp:BoundField HeaderText="Item Code" DataField="ITEMCODE" />
                                                            <asp:BoundField HeaderText="Description" DataField="ITEMDESC" />
                                                            <asp:BoundField HeaderText="UNIT" DataField="UNIT" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="MSL" DataField="MSL" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="STK-HO" DataField="STKHO" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="STK-<br/>Bangalore" DataField="STKBLR" HtmlEncode="false" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="Total Ava.<br/>Stock" DataField="TOTALAVAILSTOCK" HtmlEncode="false" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="Req. Qty." DataField="REQQTY" DataFormatString="{0:N0}" />

                                                            <asp:BoundField HeaderText="HO <br/>Listing Qty." HtmlEncode="false" DataField="HOLISTING" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="BLR <br/>Listing Qty." HtmlEncode="false" DataField="BLRLISTING" DataFormatString="{0:N0}" />

                                                            <asp:BoundField HeaderText="Total <br/>Listing Qty." HtmlEncode="false" DataField="LISTING" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="Available<br/>@ Listing" DataField="AVAILATLISTING" HtmlEncode="false" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="STK FBA <br/>Ahmedabad" DataField="STKFB01" HtmlEncode="false" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="STK FBA <br/>Bangalore" DataField="STKFB02" HtmlEncode="false" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="STK FBA <br/>Haryana" DataField="STKFB03" HtmlEncode="false" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="STK FBA <br/>Mumbai" DataField="STKFB04" HtmlEncode="false" DataFormatString="{0:N0}" />


                                                            <asp:BoundField HeaderText="Purchase <br/>(Last Day)" DataField="PURCHASE" HtmlEncode="false" DataFormatString="{0:N0}" />
                                                            <asp:BoundField HeaderText="Dispatch <br/>(Last Day)" DataField="DISPATCH" HtmlEncode="false" DataFormatString="{0:N0}" />
                                                        </Columns>
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

    <input type="hidden" id="menutibid" value="tsmMSLReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" />

</asp:Content>
