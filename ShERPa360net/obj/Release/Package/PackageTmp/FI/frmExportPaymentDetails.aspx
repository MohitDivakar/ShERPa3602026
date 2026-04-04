<%@ Page Title="Export Bank Payment" Language="C#" MasterPageFile="~/FI/MasterFI.Master" AutoEventWireup="true" CodeBehind="frmExportPaymentDetails.aspx.cs" Inherits="ShERPa360net.FI.frmExportPaymentDetails" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Export Bank Payment</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Export Bank Payment Entry </strong>Entry</h3>
                            <asp:LinkButton ID="imgDownload" runat="server" CssClass="btn btn-success pull-right" Text="Download" TabIndex="2" OnClick="lnkDownLoadAll_Click">
<span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                            <%--<asp:LinkButton ID="imgSave" runat="server" CssClass="btn btn-success pull-right" Text="Save" TabIndex="1"><i class="fa fa-save"></i></asp:LinkButton>--%>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                                <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowFooter="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="VENDORNAME" HeaderText="Party Name" />
                                                        <asp:BoundField DataField="DOCNO" HeaderText="Doc No." />
                                                        <asp:BoundField DataField="DOCDT" HeaderText="Doc Dt." />
                                                        <asp:BoundField DataField="DOCAMT" HeaderText="Doc Amt." ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField DataField="ADJAMT" HeaderText="Adj Amt" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField DataField="ADJTB" HeaderText="Paid Amt." ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField DataField="CRDR" HeaderText="CR / DR" />
                                                        <asp:BoundField DataField="PDOCNO" HeaderText="Doc No." />
                                                        <asp:BoundField DataField="PDOCTYPE" HeaderText="Doc Type" />
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
        </div>
        </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranFIBP" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranFI" runat="server" />

</asp:Content>
