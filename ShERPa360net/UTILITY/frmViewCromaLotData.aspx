<%@ Page Title="View Croma Lot" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmViewCromaLotData.aspx.cs" Inherits="ShERPa360net.UTILITY.frmViewCromaLotData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View Croma Lot</title>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {


            $("#ContentPlaceHolder1_gvPopDetail").DataTable({
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

    <style>
        .card-grid {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
            /*justify-content: center;*/
        }

        .card {
            width: auto;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            background-color: #fff;
            padding: 15px;
            margin-bottom: 20px;
            transition: transform 0.2s ease;
        }

            .card:hover {
                transform: scale(1.03);
                box-shadow: 0 4px 10px rgba(0,0,0,0.2);
            }

        .card-header {
            font-size: 1.3em;
            font-weight: bold;
            margin-bottom: 10px;
        }

        .card-body {
            margin-top: 10px;
        }

        .child-grid {
            width: 100%;
            border-collapse: collapse;
        }

            .child-grid th, .child-grid td {
                border: 1px solid #ccc;
                padding: 5px;
                text-align: left;
            }
    </style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <%--  <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="50" CellPadding="50" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="RPA Site Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRPASITECODE" runat="server" Text='<%# Eval("RPASITECODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Croma Lot No.">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lblCROMALOTNO" OnClick="lblCROMALOTNO_Click" Text='<%# Eval("CROMALOTNO") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QTEK Lot No.">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lblQTEKLOTNO" OnClick="lblQTEKLOTNO_Click" Text='<%# Eval("QTEKLOTNO") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTOTALQTY" runat="server" Text='<%# Eval("TOTALQTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MRP">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTOTALMRP" runat="server" Text='<%# Eval("TOTALMRP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Purchase Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTOTALPURPRICE" runat="server" Text='<%# Eval("TOTALPURPRICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sale Price">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTOTALSALPRICE" runat="server" Text='<%# Eval("TOTALSALPRICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%">
                                                                <div id="div<%# Eval("CROMALOTNO") %>" style="overflow-x: scroll; max-height: 500px !important;" class="box-body divhorizontal">

                                                                    <asp:GridView ID="gvInnerList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered nowrap" CellSpacing="0"
                                                                        ShowHeaderWhenEmpty="true" Width="100%">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Croma Lot No." Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCROMACATLOTNO" runat="server" Text='<%# Eval("CROMALOTNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="QTEK Lot No." Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblQTEKCATLOTNO" runat="server" Text='<%# Eval("QTEKLOTNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Product">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" ID="lblPRODUCT" OnClick="lblPRODUCT_Click" Text='<%# Eval("PRODUCT") %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTOTALCATQTY" runat="server" Text='<%# Eval("TOTALCATQTY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="MRP">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTOTALCATMRP" runat="server" Text='<%# Eval("TOTALCATMRP") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Purchase Price">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTOTALCATPURPRICE" runat="server" Text='<%# Eval("TOTALCATPURPRICE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Sale Price">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTOTALCATSALEPRICE" runat="server" Text='<%# Eval("TOTALCATSALEPRICE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>

                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>--%>

                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="box">
                                    <%--<div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">--%>

                                    <asp:GridView ID="GridViewParent" runat="server" AutoGenerateColumns="False" CssClass="card-grid" OnRowDataBound="GridViewParent_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="card">
                                                        <%--          <div class="card-header">
                                                                Lot No. :  <%# Eval("CROMALOTNO") %>
                                                                
                                                            </div>
                                                            <div class="card-header">
                                                                Totl Qty : <%# Eval("TOTALQTY") %>
                                                                
                                                            </div>--%>

                                                        <div class="card-header">
                                                            <table class="table text-center col-sm-12" style="border: ridge !important;">
                                                                <tr style="border-bottom-style: hidden;">
                                                                    <td style="empty-cells: hide" colspan="6">
                                                                        <p>
                                                                            Lot No. : 
                                                                            <asp:LinkButton runat="server" ID="lblCROMALOTNO" OnClick="lblCROMALOTNO_Click" Text='<%# Eval("CROMALOTNO") %>'></asp:LinkButton>
                                                                        </p>

                                                                        <asp:HiddenField ID="hfCromaLot" runat="server" Value='<%# Eval("CROMALOTNO") %>' />
                                                                    </td>
                                                                    <td style="empty-cells: hide" colspan="6">
                                                                        <p>
                                                                            Totl Qty :
                                                                            <asp:LinkButton runat="server" ID="lnkTOTALQTY" OnClick="lblCROMALOTNO_Click" Text='<%# Eval("TOTALQTY") %>'></asp:LinkButton>
                                                                        </p>
                                                                        <asp:HiddenField ID="hfTOTALQTY" runat="server" Value='<%# Eval("TOTALQTY") %>' />
                                                                    </td>
                                                                    <td style="empty-cells: hide" colspan="6">
                                                                        <p>
                                                                            MRP :
                                                                            <asp:LinkButton runat="server" ID="lnkTOTALMRP" OnClick="lblCROMALOTNO_Click" Text='<%# FormatIndianCurrency(Convert.ToDecimal(Eval("TOTALMRP"))) %>'></asp:LinkButton>
                                                                        </p>
                                                                        </p>
                                                                        <asp:HiddenField ID="hfTOTALMRP" runat="server" Value='<%# Eval("TOTALMRP") %>' />
                                                                    </td>
                                                                    <td style="empty-cells: hide" colspan="6">
                                                                        <p>
                                                                            Sale Price :
                                                                            <asp:LinkButton runat="server" ID="lnkTOTALSALPRICE" OnClick="lblCROMALOTNO_Click" Text='<%# FormatIndianCurrency(Convert.ToDecimal(Eval("TOTALSALPRICE"))) %>'></asp:LinkButton>
                                                                        </p>
                                                                        </p>
                                                                        <asp:HiddenField ID="hfTOTALSALPRICE" runat="server" Value='<%# Eval("TOTALSALPRICE") %>' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <div class="card-body">
                                                            <asp:GridView ID="GridViewChild" runat="server" AutoGenerateColumns="False" CssClass="child-grid">
                                                                <Columns>
                                                                    <%--<asp:BoundField DataField="PRODUCT" HeaderText="Item Name" />
                                                                    <asp:BoundField DataField="TOTALCATQTY" HeaderText="Quantity" />
                                                                    <asp:BoundField DataField="TOTALCATSALEPRICE" HeaderText="Sale Price" />--%>

                                                                    <asp:TemplateField HeaderText="Croma Lot No." Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCROMACATLOTNO" runat="server" Text='<%# Eval("CROMALOTNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="QTEK Lot No." Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQTEKCATLOTNO" runat="server" Text='<%# Eval("QTEKLOTNO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Product">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton runat="server" ID="lblPRODUCT" OnClick="lblPRODUCT_Click" Text='<%# Eval("PRODUCT") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Qty">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblTOTALCATQTY" runat="server" Text='<%# Eval("TOTALCATQTY") %>'></asp:Label>--%>
                                                                            <asp:LinkButton runat="server" ID="lblTOTALCATQTY" OnClick="lblPRODUCT_Click" Text='<%# Eval("TOTALCATQTY") %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="MRP">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblTOTALCATMRP" runat="server" Text='<%# FormatIndianCurrency(Convert.ToDecimal(Eval("TOTALCATMRP"))) %>'></asp:Label>--%>
                                                                            <asp:LinkButton runat="server" ID="lblTOTALCATMRP" OnClick="lblPRODUCT_Click" Text='<%# FormatIndianCurrency(Convert.ToDecimal(Eval("TOTALCATMRP"))) %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Purchase Price" Visible="false">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblTOTALCATPURPRICE" runat="server" Text='<%# FormatIndianCurrency(Convert.ToDecimal(Eval("TOTALCATPURPRICE"))) %>'></asp:Label>--%>
                                                                            <asp:LinkButton runat="server" ID="lblTOTALCATPURPRICE" OnClick="lblPRODUCT_Click" Text='<%# FormatIndianCurrency(Convert.ToDecimal(Eval("TOTALCATPURPRICE"))) %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sale Price">
                                                                        <ItemTemplate>
                                                                            <%--<asp:Label ID="lblTOTALCATSALEPRICE" runat="server" Text='<%# FormatIndianCurrency(Convert.ToDecimal(Eval("TOTALCATSALEPRICE"))) %>'></asp:Label>--%>
                                                                            <asp:LinkButton runat="server" ID="lblTOTALCATSALEPRICE" OnClick="lblPRODUCT_Click" Text='<%# FormatIndianCurrency(Convert.ToDecimal(Eval("TOTALCATSALEPRICE"))) %>'></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="card-header">
                                                        <table class="table text-center col-sm-12" style="border: ridge !important;">
                                                            <tr style="border-bottom-style: hidden; font-size: 12px !important; text-align: left !important;">
                                                                <td style="empty-cells: hide" colspan="6">
                                                                    <p>
                                                                        Bid Start Dt. : 
                                                                            <asp:Label runat="server" ID="lblBIDSTARTDATE" Text='<%# Eval("BIDSTARTDATE") %>'></asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        Bid End Dt. : 
                                                                            <asp:Label runat="server" ID="lblBIDENDDATE" Text='<%# Eval("BIDENDDATE") %>'></asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        Bid Start Amt. : 
                                                                            <asp:Label runat="server" ID="lblBIDSTARTAMT" Text='<%# Eval("BIDSTARTAMT") %>'></asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        Minimum Bid Increment Amt. : 
                                                                            <asp:Label runat="server" ID="lblMINIMUMBIDAMT" Text='<%# Eval("MINIMUMBIDAMT") %>'></asp:Label>
                                                                    </p>
                                                                    <p>
                                                                        Lot visible to Dealer : 
                                                                            <asp:Label runat="server" ID="lblSHOWLOT" Text='<%# Eval("SHOWLOT") %>' Visible="false"></asp:Label>
                                                                        <asp:Label runat="server" ID="lblLotVisible"></asp:Label>
                                                                    </p>
                                                                </td>
                                                            </tr>

                                                            <%--<tr style="border-bottom-style: hidden; font-size: 12px !important;">
                                                                <td style="empty-cells: hide" colspan="6">
                                                                   
                                                                </td>
                                                            </tr>--%>

                                                            <%--<tr style="border-bottom-style: hidden; font-size: 12px !important;">
                                                                <td style="empty-cells: hide" colspan="6">
                                                                    
                                                                </td>
                                                            </tr>--%>

                                                            <%--<tr style="border-bottom-style: hidden; font-size: 12px !important;">
                                                                <td style="empty-cells: hide" colspan="6">
                                                                    
                                                                </td>
                                                            </tr>--%>

                                                            <%--<tr style="border-bottom-style: hidden; font-size: 12px !important;">
                                                                <td style="empty-cells: hide" colspan="6">
                                                                    
                                                                </td>
                                                            </tr>--%>

                                                            <tr style="border-bottom-style: hidden; font-size: 25px !important;">
                                                                <td style="empty-cells: hide" colspan="6">
                                                                    <asp:LinkButton ID="lnkSetting" runat="server" CssClass="fa fa-cog pull-right" OnClick="lnkSetting_Click"></asp:LinkButton>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <%--</div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 1250px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Lot</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Croma Lot No. :</label>
                                    <asp:Label ID="lblPopCromaLotNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Qtek Lot No. :</label>
                                    <asp:Label ID="lblPopQtekLotNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="gvPopDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true" ShowFooter="false"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <Columns>
                                                <asp:BoundField DataField="INWARDSCANID" HeaderText="INWARDSCAN ID" />
                                                <asp:BoundField DataField="SRNO" HeaderText="SR NO" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="ITEM CODE" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="ITEM DESC" />
                                                <asp:BoundField DataField="BRAND" HeaderText="BRAND" />
                                                <asp:BoundField DataField="PRODUCT" HeaderText="PRODUCT" />
                                                <asp:BoundField DataField="SERIALNO" HeaderText="SERIAL NO" />
                                                <asp:BoundField DataField="MRP" HeaderText="MRP" />
                                                <asp:BoundField DataField="QTEKPRICE" HeaderText="QTEK PRICE" />
                                                <asp:BoundField DataField="SALESPRICE" HeaderText="SALES PRICE" />
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


    <div class="modal fade" id="modal-BidSetting" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 1250px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Bid</strong> Setting</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmCromaLotUpload" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
