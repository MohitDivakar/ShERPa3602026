<%@ Page Title="Demo Installation List" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmDemoInstallationList.aspx.cs" Inherits="ShERPa360net.UTILITY.frmDemoInstallationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Demo Installation List</title>

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


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;   Demo / Installation </strong>List</h3>
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
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtSINo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearhSI" CssClass="btn btn-success pull-left" Text="Search SI" OnClick="lnkSearhSI_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>--%>
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
                        <div class="row" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">
                            <div class="col-md-12">
                                <div class="box">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
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
                                                    <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label><br />
                                                    <asp:Label ID="lblPLANT" runat="server" Text='<%# Eval("PLANT") %>'></asp:Label>
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
                                                    <asp:TextBox ID="txtActualDispDate" runat="server" CssClass="form-control datepicker" MaxLength="10" Style="width: 92px;" Text='<%# Eval("ACTUALDISPDATE") %>'></asp:TextBox><br />
                                                    Esti. Deli. Dt. :<br />
                                                    <asp:TextBox ID="txtEstDelDt" runat="server" CssClass="form-control" MaxLength="10" Style="width: 92px;" Text='<%# Eval("DELIDT","{0:dd-MM-yyyy}") %>' Enabled="false"></asp:TextBox><br />
                                                    <%--Remark : <br />--%>
                                                    <%--<asp:TextBox ID="txtInstReqOnRemarks"--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Installation Req. on">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInstReqOn" runat="server" CssClass="form-control datepicker" MaxLength="10" Style="width: 92px;" Text='<%# Eval("INSTALLATIONREQON") %>'></asp:TextBox><br />
                                                    <%--Remark : <br />--%>
                                                    <%--<asp:TextBox ID="txtInstReqOnRemarks"--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Demo Req. on">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDemoReqOn" runat="server" CssClass="form-control datepicker" MaxLength="10" Style="width: 92px;" Text='<%# Eval("DEMOREQON") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Demo/Installation Compl. by">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDemoInstDoneBy" runat="server" CssClass="form-control" Style="width: 92px;" Text='<%# Eval("DEMOINSTBY") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Installation Compl. on">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtInstCompOn" runat="server" CssClass="form-control datepicker" MaxLength="10" Style="width: 92px;" Text='<%# Eval("INSTALLATIONDONEON") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Demo Compl. on">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDemoCompOn" runat="server" CssClass="form-control datepicker" MaxLength="10" Style="width: 92px;" Text='<%# Eval("DEMODONEON") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Charges To be taken">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtChargestobe" runat="server" CssClass="form-control" TextMode="Number" Style="width: 75px;" Text='<%# Eval("CHARGESTOBETAKEN") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Charges Rcvd to Store">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtChargercvdstore" runat="server" CssClass="form-control" TextMode="Number" Style="width: 75px;" Text='<%# Eval("CHARGESRCVDSTORE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Changes Rcvd on (Store)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtChargesrcvdon" runat="server" CssClass="form-control datepicker" MaxLength="10" Style="width: 92px;" Text='<%# Eval("CHARGESRCVDONSTORE") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Charges Rcvd to Account">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtChargercvdaccount" runat="server" CssClass="form-control" TextMode="Number" Style="width: 75px;" Text='<%# Eval("CHARGESRCVDACCOUNT") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Changes Rcvd on (Account)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtChargesrcvdonaccount" runat="server" CssClass="form-control datepicker" MaxLength="10" Style="width: 92px;" Text='<%# Eval("CHARGESRCVDONACCOUNT") %>'></asp:TextBox><br />
                                                    <asp:CheckBox ID="chkFinalEntry" runat="server" Text="Final Entry" CssClass="checkbox-inline" />
                                                    <asp:Label ID="lblFinalENtry" runat="server" Visible="false" Text='<%# Eval("FINALENTRY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Update">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Update" OnClick="btnUpdate_Click" />
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%--<asp:TemplateField HeaderText="Action" Visible="True">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                    | 
                                                        <asp:LinkButton runat="server" ID="btnDownload" Text="Download" OnClick="btnDownload_Click"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmGetDemoInstallData" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
