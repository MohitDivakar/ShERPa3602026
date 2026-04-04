<%@ Page Title="Dealer Data" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptDealerRegi.aspx.cs" Inherits="ShERPa360net.REPORTS.rptDealerRegi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Dealer Data</title>

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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Dealer Registration Data</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12 pull-right">

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
                                            <label class="col-md-2 control-label pull-left">To</label>
                                            <div class="col-md-10 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearch_Click">
                                        <span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="CUSTCODE" HeaderText="Cust. Code" />
                                                <asp:BoundField DataField="USERCODE" HeaderText="User Code" Visible="false" />
                                                <asp:BoundField DataField="USERNAME" HeaderText="User Name" Visible="false" />
                                                <asp:BoundField DataField="NAME1" HeaderText="Dealer Name" />
                                                <asp:BoundField DataField="EMAILID" HeaderText="Email ID" />
                                                <asp:BoundField DataField="MOBILENO" HeaderText="mobile No." />
                                                <asp:BoundField DataField="ADDR1" HeaderText="Address 1" />
                                                <asp:BoundField DataField="ADDR2" HeaderText="Address 2" />
                                                <asp:BoundField DataField="ADDR3" HeaderText="Address 3" />
                                                <asp:BoundField DataField="CITY" HeaderText="City" />
                                                <asp:BoundField DataField="STATE" HeaderText="State" />

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

    <input type="hidden" id="menutabid" value="tsmRptDealerData" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptCRM" runat="server" />

</asp:Content>
