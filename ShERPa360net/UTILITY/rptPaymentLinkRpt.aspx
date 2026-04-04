<%@ Page Title="Payment Link Report" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="rptPaymentLinkRpt.aspx.cs" Inherits="ShERPa360net.UTILITY.rptPaymentLinkRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Payment Link Report</title>

      <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {
            $("#ContentPlaceHolder1_gvList").DataTable({
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
      </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Payment Link Report</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Date From : </label>
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

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Cust. Name : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Ref. ID : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtRefID" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>
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
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="CUSTNAME" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="CONTACTNO" HeaderText="Contact No." />
                                            <asp:BoundField DataField="EMAILID" HeaderText="Email ID" />
                                            <asp:BoundField DataField="DESCRIPTION" HeaderText="Payment Descr." />
                                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount" />
                                            <asp:BoundField DataField="REFID" HeaderText="Ref. ID" />
                                            <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />
                                            <asp:BoundField DataField="PAYMENTLINK" HeaderText="Payment Link" />
                                            <asp:BoundField DataField="EXPIREDBY" HeaderText="Expired On" />
                                            <asp:BoundField DataField="ENVIRONMENT" HeaderText="Environment" />
                                            <asp:BoundField DataField="USERNAME" HeaderText="Create By" />
                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Create Dtae" />
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
    
    <input type="hidden" id="menutabid" value="tsmPaymentLinkReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
