<%@ Page Title="Demo Installation Report" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="rptDemoInstall.aspx.cs" Inherits="ShERPa360net.UTILITY.rptDemoInstall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Demo Installation Report</title>

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
            background-color: #f1f5f9 !important;
            color: #56688A !important;
        }
    </style>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });

        function BindMakeAssociateModel() {
            $("#ContentPlaceHolder1_gvList").DataTable({
                paging: true,
                dom: 'Bfrtip',
                destroy: true,
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

    </script>

    <%--<style type="text/css">
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
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;   Demo / Installation </strong>Report</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">SI Date : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">To : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">SI No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:TextBox ID="txtSINo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Cust. Type :  </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:DropDownList ID="ddlCustType" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="ALL" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Retail Customer" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Dealer" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Final Entry :  </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:DropDownList ID="ddlFinalEntry" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="ALL" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Completed" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Disptach :  </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:DropDownList ID="ddlActualDispatch" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="ALL" Value="0" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Completed" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Pending" Value="2"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearhSI" CssClass="btn btn-success pull-left" Text="Search SI" OnClick="lnkSearhSI_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO" OnClick="lnkExport_Click"><i class="fa fa-download"></i></asp:LinkButton>
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
                                <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="header" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SI Det.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSIDT" runat="server" Text='<%# Eval("SIDT") %>'></asp:Label><br />
                                                    <asp:Label ID="lblSINO" runat="server" Text='<%# Eval("SINO") %>'></asp:Label><br />
                                                    <asp:Label ID="lblSONO" runat="server" Text='<%# Eval("SONO") %>'></asp:Label><br />
                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--  <asp:TemplateField HeaderText="SI No.">
                                                <ItemTemplate>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SO No.">
                                                <ItemTemplate>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Item ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEMID" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Det.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label><br />
                                                    <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label><br />
                                                    <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payment Det.">
                                                <ItemTemplate>
                                                    Paymen  t Mode :<br />
                                                    <asp:Label ID="lblPAYMODEDESC" runat="server" Text='<%# Eval("PAYMODEDESC") %>'></asp:Label><br />
                                                    Inv. Amt. :<br />
                                                    <asp:Label ID="lblINVAMT" runat="server" Text='<%# Eval("INVAMT") %>'></asp:Label><br />
                                                    Rcpt Amt. :<br />
                                                    <asp:Label ID="lblRCPTAMT" runat="server" Text='<%# Eval("RCPTAMT") %>'></asp:Label><br />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%-- <asp:TemplateField HeaderText="Item Desc.">
                                                <ItemTemplate>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Job ID">
                                                <ItemTemplate>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Cust. Det." ItemStyle-Width="120px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label><br />
                                                    <asp:Label ID="lblCUSTMOBILENO" runat="server" Text='<%# Eval("CUSTMOBILENO") %>'></asp:Label><br />
                                                    Inst. Req. :
                                                    <asp:Label ID="lblINSTREQ" runat="server" Text='<%# Eval("INSTREQ") %>'></asp:Label><br />
                                                    Demo Req. :
                                                    <asp:Label ID="lblDEMOREQ" runat="server" Text='<%# Eval("DEMOREQ") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--  <asp:TemplateField HeaderText="Cust. Mobile No.">
                                                <ItemTemplate>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Actual Disp. Dt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtActualDispDate" runat="server" Text='<%# Eval("ACTUALDISPDATE","{0:dd-MM-yyyy}") %>'></asp:Label><br />
                                                    Esti. Deli. Dt. :<br />
                                                    <asp:Label ID="txtEstDelDt" runat="server" MaxLength="10" Style="width: 92px;" Text='<%# Eval("ESTIDISPDT","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Installation Req. on">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtInstReqOn" runat="server" Text='<%# Eval("INSTALLATIONREQON","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Demo Req. on">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDemoReqOn" runat="server" Text='<%# Eval("DEMOREQON","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Demo/Installation Compl. by">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDemoInstDoneBy" runat="server" Text='<%# Eval("DEMOINSTBY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Installation Compl. on">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtInstCompOn" runat="server" Text='<%# Eval("INSTALLATIONDONEON","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Demo Compl. on">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDemoCompOn" runat="server" Text='<%# Eval("DEMODONEON","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Charges To be taken">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtChargestobe" runat="server" Text='<%# Eval("CHARGESTOBETAKEN") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Charges Rcvd to Store">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtChargercvdstore" runat="server" Text='<%# Eval("CHARGESRCVDSTORE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Changes Rcvd on (Store)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtChargesrcvdon" runat="server" Text='<%# Eval("CHARGESRCVDONSTORE","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Charges Rcvd to Account">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtChargercvdaccount" runat="server" Text='<%# Eval("CHARGESRCVDACCOUNT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Changes Rcvd on (Account)">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtChargesrcvdonaccount" runat="server" Text='<%# Eval("CHARGESRCVDONACCOUNT","{0:dd-MM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Final Entry Done">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFINENTRY" runat="server" Text='<%# Eval("FINENTRY")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" Visible="True">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
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


    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Demo / Installation Details</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>SI No. : </label>
                                        <asp:Label ID="lblPopSINO" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>SO No. : </label>
                                        <asp:Label ID="lblPopSONO" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sr. No. : </label>
                                        <asp:Label ID="lblPopSrNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Job Id : </label>
                                        <asp:Label ID="lblPopJobid" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cust. Name : </label>
                                        <asp:Label ID="lblPopCustName" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Contact No. : </label>
                                        <asp:Label ID="lblPopContactNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Code : </label>
                                        <asp:Label ID="lblPopItemCode" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Desc. : </label>
                                        <asp:Label ID="lblPopItemDesc" runat="server" CssClass="form-control" Style="height: 50px !important;"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <asp:GridView ID="gvDetails" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                    CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                    <EmptyDataTemplate>
                                        No Record Found!
                                    </EmptyDataTemplate>
                                    <Columns>

                                        <asp:TemplateField HeaderText="Details">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDATACHANGE" runat="server" Text='<%# Eval("DATADETAILES") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details Data">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDATACHANGEVALUE" runat="server" Text='<%# Eval("DATACHANGEVALUE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details change on">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCHANGEDATE" runat="server" Text='<%# Eval("CHANGEDATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Details Change by">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptDemoInstall" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmReportUtility" runat="server" />

</asp:Content>
