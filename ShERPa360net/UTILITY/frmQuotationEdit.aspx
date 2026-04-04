<%@ Page Title="Quotation" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmQuotationEdit.aspx.cs" Inherits="ShERPa360net.UTILITY.frmQuotationEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Quotation</title>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Edit  </strong>Quotation</h3>

                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/UTILITY/frmApproveQuotation.aspx" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" Text="Save All" OnClick="imgSaveAll_Click" ValidationGroup="SaveAll" Visible="false"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>

                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">

                                        <div class="col-md-6">
                                            <div class="form-group" style="text-align-last: center;">
                                                <asp:Label ID="lblDoctype" runat="server" CssClass="pull-left" Style="font-size: 15px;"></asp:Label>
                                                <br />
                                                <asp:HiddenField ID="hfSeq" runat="server" />
                                                <asp:HiddenField ID="hfPlant" runat="server" />
                                                <asp:HiddenField ID="hfPODate" runat="server" />
                                                <asp:HiddenField ID="hfMail" runat="server" />
                                                <asp:HiddenField ID="hfMobile" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Quotation To :</label>
                                            <asp:TextBox ID="txtInvoiceTo" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Delivery To :</label>
                                            <asp:TextBox ID="txtDeliveryTo" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Supplier :</label>
                                            <asp:TextBox ID="txtSupplier" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label>Quotation Details :</label>
                                            <asp:TextBox ID="txtPODetail" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                            <asp:Label ID="lblPONo" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-2" style="margin-top: 5px;">
                                        <div class="input-group">
                                            <label>Sale Amount :</label>
                                            <asp:TextBox ID="lblMaterialAmt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="margin-top: 5px;">
                                        <div class="input-group">
                                            <label>Base Amount :</label>
                                            <asp:TextBox ID="lblOtherChg" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="margin-top: 5px;">
                                        <div class="input-group">
                                            <label>Discount Per. (%) :</label>
                                            <asp:TextBox ID="lblDiscountpercentage" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="margin-top: 5px;">
                                        <div class="input-group">
                                            <label>Discount Amount :</label>
                                            <asp:TextBox ID="lblDiscountAmt" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="margin-top: 5px;">
                                        <div class="input-group">
                                            <label>Tax Amount :</label>
                                            <asp:TextBox ID="lblTaxAmt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-2" style="margin-top: 5px;">
                                        <div class="input-group">
                                            <label>Total Amount :</label>
                                            <asp:TextBox ID="lblPOTotalAmt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" style="margin-top: 5px;">
                                    <div class="box">
                                        <div class="page-content-wrap">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-default tabs">
                                                        <ul class="nav nav-tabs" role="tablist">
                                                            <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Material</a></li>
                                                            <li><a href="#tab-second" role="tab" data-toggle="tab">Taxation </a></li>
                                                        </ul>
                                                        <div class="panel-body tab-content">

                                                            <div class="tab-pane active" id="tab-first">
                                                                <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll;">
                                                                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                        CssClass="table table-hover table-striped table-bordered nowrap" ShowHeader="true" GridLines="None">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="CMP ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCMPID" runat="server" Text='<%# Eval("CMPID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="QUOTNO" HeaderText="Quot. No." />--%>
                                                                            <asp:TemplateField HeaderText="Quot. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblQUOTNO" runat="server" Text='<%# Eval("QUOTNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="QUOTID" HeaderText="Quot. Sr. No." />--%>
                                                                            <asp:TemplateField HeaderText="Quot. Sr. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblQUOTID" runat="server" Text='<%# Eval("QUOTID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMID" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />--%>
                                                                            <asp:TemplateField HeaderText="Article Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />--%>
                                                                            <asp:TemplateField HeaderText="Item Desc.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="TRACKNO" HeaderText="Job ID" />--%>
                                                                            <asp:TemplateField HeaderText="Job ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTRACKNO" runat="server" Text='<%# Eval("TRACKNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="IMEINO" HeaderText="Serial No." />--%>
                                                                            <asp:TemplateField HeaderText="Serial No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIMEINO" runat="server" Text='<%# Eval("IMEINO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Group ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMGROUPID" runat="server" Text='<%# Eval("ITEMGROUPID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="ITEMGROUP" HeaderText="Item Group" />--%>
                                                                            <asp:TemplateField HeaderText="Item Group">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMGROUP" runat="server" Text='<%# Eval("ITEMGROUP") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="UOM ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblUOMID" runat="server" Text='<%# Eval("UOMID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="ITEMUOM" HeaderText="UOM" />--%>
                                                                            <asp:TemplateField HeaderText="UOM">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMUOM" runat="server" Text='<%# Eval("ITEMUOM") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="POQTY" HeaderText="Qty." />--%>
                                                                            <asp:TemplateField HeaderText="Qty.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPOQTY" runat="server" Text='<%# Eval("POQTY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Rate" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMRATE" runat="server" Text='<%# Eval("ITEMRATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="ITEMBRATE" HeaderText="Sale Amt." />--%>
                                                                            <asp:TemplateField HeaderText="Sale Amt.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMBRATE" runat="server" Text='<%# Eval("ITEMBRATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="CAMOUNT" HeaderText="Base Amt." />--%>
                                                                            <asp:TemplateField HeaderText="Base Amt.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCAMOUNT" runat="server" Text='<%# Eval("CAMOUNT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="DISCOUNTAMT" HeaderText="Disc. Amt." />--%>
                                                                            <asp:TemplateField HeaderText="Disc. Amt.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDISCOUNTAMT" runat="server" Text='<%# Eval("DISCOUNTAMT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="TAXABLE" HeaderText="Taxable Amt." />--%>
                                                                            <asp:TemplateField HeaderText="Taxable Amt.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTAXABLE" runat="server" Text='<%# Eval("TAXABLE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="TAXAMT" HeaderText="Tax Amt." />--%>
                                                                            <asp:TemplateField HeaderText="Tax Amt.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTAXAMT" runat="server" Text='<%# Eval("TAXAMT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Plant ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMPLANTID" runat="server" Text='<%# Eval("ITEMPLANTID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="ITEMPLANTCD" HeaderText="Plant" />--%>
                                                                            <asp:TemplateField HeaderText="Plant">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMPLANTCD" runat="server" Text='<%# Eval("ITEMPLANTCD") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Location ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLOCCDID" runat="server" Text='<%# Eval("LOCCDID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="ITEMLOCCD" HeaderText="Location" />--%>
                                                                            <asp:TemplateField HeaderText="Location">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMLOCCD" runat="server" Text='<%# Eval("ITEMLOCCD") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="GL Code" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGLCODE" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Cost Center ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCSTCENTCD" runat="server" Text='<%# Eval("CSTCENTCD") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="COSTCENTER" HeaderText="Cost Center" />--%>
                                                                            <asp:TemplateField HeaderText="Cost Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCOSTCENTER" runat="server" Text='<%# Eval("COSTCENTER") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Profit Center" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPROFITCENTER" runat="server" Text='<%# Eval("PROFITCENTER") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item text" Visible="true">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMTEXT" runat="server" Text='<%# Eval("ITEMTEXT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="MAKE" HeaderText="Make" />--%>
                                                                            <asp:TemplateField HeaderText="Make">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMAKE" runat="server" Text='<%# Eval("MAKE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="MODEL" HeaderText="Model" />--%>
                                                                            <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMODEL" runat="server" Text='<%# Eval("MODEL") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="GSTRATE" HeaderText="GST Rate" />--%>
                                                                            <asp:TemplateField HeaderText="GST Rate">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGSTRATE" runat="server" Text='<%# Eval("GSTRATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkTempDelete" runat="server" Text="Remove" OnClick="lnkTempDelete_Click"></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>

                                                                    <asp:LinkButton runat="server" ID="lnkAddNew" CssClass="btn btn-success pull-left" Text="Re-Calculate" OnClick="lnkAddNew_Click" Visible="true"><i class="bx bx-calculator"></i> Add New Item</asp:LinkButton>

                                                                </div>
                                                            </div>

                                                            <div class="tab-pane" id="tab-second">
                                                                <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; height: 300px;">
                                                                    <asp:GridView ID="grvTaxation" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                        CssClass="table table-hover table-striped table-bordered nowrap">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="CMP ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCMPID" runat="server" Text='<%# Eval("CMPID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Quot. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblQUOTNO" runat="server" Text='<%# Eval("QUOTNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="TAXSRNO" HeaderText="Sr. No." />--%>
                                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTAXSRNO" runat="server" Text='<%# Eval("TAXSRNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="POSRNO" HeaderText="Quot. Sr. No." />--%>
                                                                            <asp:TemplateField HeaderText="Quot. Sr. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPOSRNO" runat="server" Text='<%# Eval("POSRNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Cond. ID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCONDID" runat="server" Text='<%# Eval("CONDID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="CONDTYPE" HeaderText="Cond. Type" />--%>
                                                                            <asp:TemplateField HeaderText="Cond. Type">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCONDTYPE" runat="server" Text='<%# Eval("CONDTYPE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="TAXRATE" HeaderText="Rate" />--%>
                                                                            <asp:TemplateField HeaderText="Rate">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTAXRATE" runat="server" Text='<%# Eval("TAXRATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="TAXBASEAMOUNT" HeaderText="Base Amt." />--%>
                                                                            <asp:TemplateField HeaderText="Base Amt.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTAXBASEAMOUNT" runat="server" Text='<%# Eval("TAXBASEAMOUNT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="TAXAMOUNT" HeaderText="Tax Amt." />--%>
                                                                            <asp:TemplateField HeaderText="Tax Amt.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTAXAMOUNT" runat="server" Text='<%# Eval("TAXAMOUNT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:BoundField DataField="OPERATOR" HeaderText="Operator" />--%>
                                                                            <asp:TemplateField HeaderText="Operator">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblOPERATOR" runat="server" Text='<%# Eval("OPERATOR") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="GL Code" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGLCODE" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="PID" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID") %>'></asp:Label>
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
                                </div>


                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkRecalc" CssClass="btn btn-success pull-right" Text="Re-Calculate" OnClick="lnkRecalc_Click" Visible="true"><i class="bx bx-calculator"></i> Re-Calculate</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-5" style="margin-top: 5px;">
                                    <div class="form-group">
                                        <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>

                                <%--<div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:TextBox ID="txtApRejDetReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApRejDetReason" ValidationGroup="ValDetRej">Enter Reject Reason</asp:RequiredFieldValidator>
                                    <br />
                                    <asp:LinkButton runat="server" ID="lnkReject" CssClass="btn btn-success pull-left" Text="Reject Quotation" OnClick="lnkReject_Click" Visible="false" ValidationGroup="ValDetRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lnkApprove" CssClass="btn btn-success pull-left" Text="Approve Quotation" OnClick="lnkApprove_Click" Visible="false"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>
                                </div>
                            </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modal-detail" data-backdrop="static" style="padding-right: 800px;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" style="width: 1550px !important;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Quotation</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">

                            <div class="page-content-wrap">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-horizontal">
                                            <div class="panel panel-default">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12">

                                                            <div class="col-md-3">
                                                                <div class="col-md-10">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label">Job ID : </label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:TextBox ID="txtJobID" runat="server" CssClass="form-control" placeholder="Job ID"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <div class="col-md-10">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label">Serial No. : </label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control" placeholder="Serial No."></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <div class="col-md-10">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label">Article Code : </label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" placeholder="Article Code"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>



                                                        </div>
                                                        <div class="col-md-12">

                                                            <div class="col-md-3">
                                                                <div class="col-md-10">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label">Brand : </label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <div class="col-md-10">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label">Category : </label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-3" runat="server" visible="false">
                                                                <div class="col-md-10">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label">Show All : </label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:CheckBox ID="chkAll" runat="server" CssClass="form-control" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <div class="col-md-10">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label">Size : </label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="col-md-3">
                                                                <div class="col-md-10">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label"></label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success" Text="Search Rate" OnClick="lnkSerch_Click" ValidationGroup="SaveAll"><span tooltip="Search" flow="down"><i class="fa fa-search"></i></span></asp:LinkButton>
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

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Price: </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:RadioButtonList ID="rblPrice" runat="server" RepeatDirection="Horizontal" CssClass="chclass" RepeatLayout="Table">
                                                    <asp:ListItem Value="1" Selected="True" Text="Partner Price"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Customer Price"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important; width: 1500px !important;">

                                    <asp:GridView ID="grvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <%--<HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                                        </HeaderTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="upd1" runat="server">
                                                        <Triggers>
                                                            <%--<asp:AsyncPostBackTrigger ControlID="chkSelect" EventName="CheckedChanged" />--%>
                                                            <%--<asp:PostBackTrigger ControlID="chkSelect" />--%>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <%--<asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" Enabled="false" />--%>
                                                            <asp:LinkButton runat="server" ID="lnkSelect" CssClass="btn btn-success pull-left" Text="Select" OnClick="lnkSelect_Click" Visible="true">Select</asp:LinkButton>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSIZE" runat="server" Text='<%# Eval("SIZE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCATEGORY" runat="server" Text='<%# Eval("CATEGORY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBRAND" runat="server" Text='<%# Eval("BRAND") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Article Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIETMDESC" runat="server" Text='<%# Eval("IETMDESC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Job ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Serial No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSERIALNO" runat="server" Text='<%# Eval("SERIALNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MRP">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Partner Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDEALERPRICE" runat="server" Text='<%# Eval("DEALERPRICE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Purchase Price" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPURCHASEPRICE" runat="server" Text='<%# Eval("PURCHASEPRICE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Customer Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCUSTOMERPRCE" runat="server" Text='<%# Eval("CUSTOMERPRCE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Price Differ (%)" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDIFFER" runat="server" Text='<%# Eval("DIFFER") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sold Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSALEDONE" runat="server" Text='<%# Eval("SALEDONE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Job Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJOBSTATDESC" runat="server" Text='<%# Eval("JOBSTATDESC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLOCATION" runat="server" Text='<%# Eval("LOCATION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Create By">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Create Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCREATEDATE" runat="server" Text='<%# Eval("CREATEDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tax Rate" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEMID" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Grp ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEMGRP" runat="server" Text='<%# Eval("ITEMGRP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="UOM" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Base Amount" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBASEAMOUNT" runat="server" Text='<%# Eval("BASEAMOUNT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cond. ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCONDID" runat="server" Text='<%# Eval("CONDID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cond. Type" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCONDTYPE" runat="server" Text='<%# Eval("CONDTYPE") %>'></asp:Label>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmCromaQuotationApproval" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
