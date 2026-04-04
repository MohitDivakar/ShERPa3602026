<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="PriceApproveDetail.aspx.cs" Inherits="ShERPa360net.UTILITY.PriceApproveDetail" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Product Qc Approve Detail</strong></h3>
                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Report" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><span tooltip="Detail" flow="down"><i class="fa fa-undo"></i> </span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click" ValidationGroup="SearchDetail"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Filter Detail</legend>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">From Date</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvfromdate" Style="color: red!important;" runat="server" ControlToValidate="txtFromDate" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter From Date To Search">Please Enter From Date To Search</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">To Date</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvToDate" Style="color: red!important;" runat="server" ControlToValidate="txtToDate" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter To Date To Search">Please Enter To Date To Search</asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Vendor Name. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlVendor" runat="server" CssClass="form-control ddlVendor"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="page-content-wrap">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel panel-default tabs">
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Details</a></li>
                                        </ul>
                                        <div class="panel-body tab-content">
                                            <div class="tab-pane active" id="tab-first">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <fieldset class="scheduler-border">
                                                            <legend class="scheduler-border" id="lgrecordcount" runat="server"></legend>
                                                            <div class="col-md-12 divhorizontal" style="overflow-x: scroll!important;">
                                                                <div class="col-md-12" runat="server">
                                                                    <div class="col-md-5">
                                                                    </div>

                                                                    <div class="col-md-7 text-center">
                                                                        <asp:Button runat="server" Style="margin-bottom: 10px!important;" ID="btnSaveAll" OnClick="btnSaveAll_Click" ClientIDMode="Static" Text="App/Reject" CssClass="btn btn-success" />
                                                                    </div>
                                                                </div>

                                                                <asp:GridView ID="gvProduct" OnRowDataBound="gvProduct_RowDataBound" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="Qc" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 64px!important;" runat="server" ID="btnQc" Text="Qc" CssClass="btn btn-primary" OnClick="btnQc_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobex Rate" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtNewRate" Placeholder="New Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" ID="txtFinalListingAmount" Enabled="false" Text='<%# Bind("FinalApproveListingAmount") %>' Placeholder="Final Listing Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" ID="txtBasicPurRate" Enabled="false" Text='<%# Bind("BASICPURRATE") %>' Placeholder="Purchase Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" ID="txtMobexRate" Style="margin-top: 5px!important;" Placeholder="Mobex Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRejectReason" Style="margin-top: 5px!important;" Placeholder="Reject Reason" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:Label runat="server" Style="margin-top: 5px!important;" ID="lblPurcPerValue"></asp:Label>
                                                                                <asp:Label runat="server" Style="margin-top: 5px!important;" ID="lblFkPerValue"></asp:Label>
                                                                                <asp:Label runat="server" Style="margin-top: 5px!important;" ID="lblAmzPerValue"></asp:Label>
                                                                                <asp:Label runat="server" Style="margin-top: 5px!important;" ID="lblWebPerValue"></asp:Label>
                                                                                <asp:TextBox runat="server" ID="txtFKRate" Enabled="false" Style="margin-top: 5px!important;" Placeholder="Flipcart Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" ID="txtAmzRate" Enabled="false" Style="margin-top: 5px!important;" Placeholder="Amazon Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" ID="txtWebRate" Enabled="false" Style="margin-top: 5px!important;" Placeholder="Web Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" TextMode="MultiLine" Enabled="false" ID="txtManualApprovalReason" Text='<%# Bind("MNLAPRREASON") %>' Style="margin-top: 5px!important; font-weight: bold!important;" Placeholder="Manual Apprv Reason" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:LinkButton runat="server" Style="margin-top: 5px!important;" ID="btnApprove" CssClass="btn btn-primary" Text="Approved" OnClick="btnApprove_Click"></asp:LinkButton>
                                                                                <asp:LinkButton runat="server" Style="margin-top: 5px!important;" ID="btnReject" CssClass="btn btn-danger" Text="Reject" OnClick="btnReject_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Listed Select" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="chkSelection"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--Action Column --%>


                                                                        <%--Show Hide Column --%>

                                                                        <asp:TemplateField HeaderText="New Price" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNewPrice" Text='<%# Bind("MOBILENEWRATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobex Price" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobexPrice" Text='<%# Bind("MOBEXPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Rate" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorRate" Text='<%# Bind("VENDORPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobex Grade" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobexGrade" Text='<%# Bind("MOBEXGRADE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--Show Hide Column --%>


                                                                        <%--Regular Column --%>
                                                                        <asp:TemplateField HeaderText="Make" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdVendorGrade" Value='<%# Bind("VENDORGRADE") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemGrpId" Value='<%# Bind("ITEMGRPID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemGrpName" Value='<%# Bind("ITEMGRPNAME") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemGrpShortName" Value='<%# Bind("ITEMGRPSHORTNAME") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemSubGrpId" Value='<%# Bind("ITEMSUBGRPID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemSubGrpName" Value='<%# Bind("ITEMSUBGRPNAME") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemSubGrpShortName" Value='<%# Bind("ITEMSUBGRPSHORTNAME") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblMake" Text='<%# Bind("MAKE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Model" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Rom" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRom" Text='<%# Bind("ROM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Ram" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRam" Text='<%# Bind("RAM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Color" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblColor" Text='<%# Bind("COLOR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Orignal Kit" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblOrignalKit" Text='<%# Bind("ORIGINALKIT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Name" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorName" Text='<%# Bind("VENDORNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Grade" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorGrade" Text='<%# Bind("VENDORGRADE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Entry Date" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ID" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="IMEINo" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblIMEINo" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Basic Pur Price" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPurPrice" Text='<%# Bind("BASICPURRATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--Regular Column --%>


                                                                        <%--Other Column --%>

                                                                        <%--<asp:TemplateField HeaderText="Vendor Stock">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorStock" Text='<%# Bind("VENDORQTY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Qc Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblQcDate" Text='<%# Bind("QCDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Qc By">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblQcByName" Text='<%# Bind("QCBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Qc Result">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblQcResult" Text='<%# Bind("QCRESULT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Purchase Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPurchaseQty" Text='<%# Bind("PURQTY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Purchase Percentage">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPurchasePerc" Text='<%# Bind("MOBILEPURCHASEPERCENTAGE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Approved Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblApprovedDate" Text='<%# Bind("NEGAPRVDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Approved By">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblApprovedBy" Text='<%# Bind("APRVBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                        

                                                                        <asp:TemplateField HeaderText="List">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton runat="server" ID="btnListed" CssClass="btn btn-primary" Text="List" OnClick="btnListed_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>

                                                                        <%--Other Column --%>
                                                                    </Columns>
                                                                </asp:GridView>
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
                    <h4 class="modal-title" style="color: #337ab7"><strong>QC</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Result :</label>
                                    <asp:DropDownList ID="ddlQcResult" ClientIDMode="Static" runat="server" Width="170" CssClass="form-control">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="SELECT"></asp:ListItem>
                                        <asp:ListItem Text="PASS" Value="PASS"></asp:ListItem>
                                        <asp:ListItem Text="FAIL" Value="FAIL"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblQcResultalert" Style="color: red!important; display: none;" runat="server" ClientIDMode="Static">Please Select the Qc Result</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Reason :</label>
                                    <asp:TextBox TabIndex="3" ClientIDMode="Static" ID="txtReason" Style="font-weight: bold!important; color: black!important;" runat="server" TextMode="MultiLine" placeholder="Fail Reason" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblReasonalert" Style="color: red!important; display: none;" runat="server" ClientIDMode="Static">Please Enter the Reason</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Grade :</label>
                                    <asp:DropDownList TabIndex="2" ClientIDMode="Static" ID="ddlQcGrade" runat="server" CssClass="form-control required_text_box">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="SELECT"></asp:ListItem>
                                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                        <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblGradealert" Style="color: red!important; display: none;" runat="server" ClientIDMode="Static">Please Select the Grade</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12 text-center">
                                <div class="col-md-12" style="margin-top: 10px!important;">
                                    <asp:HiddenField runat="server" ID="hdprimarykey" />
                                    <asp:Button TabIndex="3" ID="btnSaveQc" OnClick="btnSaveQc_Click" OnClientClick="return ValidateQcResult();" runat="server" Text="Update" CssClass="btn btn-primary"></asp:Button>
                                    <asp:Button TabIndex="3" ID="btnResetQc" OnClick="btnResetQc_Click" Style="margin-left: 40px!important" runat="server" Text="Reset" CssClass="btn btn-danger"></asp:Button>
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
    <input type="hidden" id="menutabApproverid" value="tsmTranMobexSellerApprover" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
