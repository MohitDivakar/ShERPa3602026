<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ProductEntry.aspx.cs" Inherits="ShERPa360net.UTILITY.ProductEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="ddlMake" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlModel" EventName="SelectedIndexChanged" />
                            </Triggers>

                            <ContentTemplate>
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Product Entry</strong></h3>
                                    <%--<asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>--%>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <fieldset class="scheduler-border">
                                                    <legend class="scheduler-border">Product Entry</legend>
                                                    <div class="col-md-12">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Make. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlMake" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" runat="server" CssClass="form-control ddlMake required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvMake" Style="color: red;" ControlToValidate="ddlMake" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Make" InitialValue="0">Please Select Make</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Model. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlModel" AutoPostBack="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" runat="server" CssClass="form-control ddlModel required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvModel" Style="color: red;" ControlToValidate="ddlModel" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Model" InitialValue="0">Please Select Model</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                       <div class="col-md-3" runat="server">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">RAM. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlRam" ClientIDMode="Static" runat="server" CssClass="form-control ddlRam required_text_box"></asp:DropDownList>
<%--                                                                <asp:Label Style="color: red; display:none;" ID="lblRamalert" ClientIDMode="Static">Please Select Ram</asp:Label>--%>
                                                                    <asp:RequiredFieldValidator ID="rfvRam" Style="color: red;" ControlToValidate="ddlRam" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Ram" InitialValue="0">Please Select Ram</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">ROM. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlRom" runat="server" CssClass="form-control ddlRom required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvRom" Style="color: red;" ControlToValidate="ddlRom" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Rom" InitialValue="0">Please Select Rom</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Color. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlColor" runat="server" CssClass="form-control ddlColor required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvColor" Style="color: red;" ControlToValidate="ddlColor" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Color" InitialValue="0">Please Select Color</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Grade. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlGrade" runat="server" CssClass="form-control required_text_box">
                                                                        <asp:ListItem Text="SELECT" Selected="True" Value="SELECT"></asp:ListItem>
                                                                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                                        <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvGrade" Style="color: red;" ControlToValidate="ddlGrade" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Grade" InitialValue="SELECT">Please Select Grade</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Vendor Stock. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="3" ID="txtVendorStock" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Vendor Stock" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvVendorStock" Style="color: red!important;" runat="server" ControlToValidate="txtVendorStock" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Stock">Please Enter Vendor Stock</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Vendor Rate. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="3" ID="txtVendorRate" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Vendor Rate" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvVendorRate" Style="color: red!important;" runat="server" ControlToValidate="txtVendorRate" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Rate">Please Enter Vendor Rate</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Vendor Name. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="3" ID="txtVendorDetail" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Vendor Name" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvVendorDetail" Style="color: red!important;" runat="server" ControlToValidate="txtVendorDetail" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Name">Please Enter Vendor Name</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Invoice. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlInvoice" runat="server" CssClass="form-control required_text_box">
                                                                        <asp:ListItem Text="SELECT" Selected="True" Value="-1"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"  Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvInvoice" Style="color: red!important;" runat="server" ControlToValidate="ddlInvoice" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select the Invoice" InitialValue="-1">Please Select the Invoice</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Box. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlBox" runat="server" CssClass="form-control required_text_box">
                                                                        <asp:ListItem Text="SELECT" Selected="True" Value="-1"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"  Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvBox" Style="color: red!important;" runat="server" ControlToValidate="ddlBox" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select the Box" InitialValue="-1">Please Select the Box</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                         <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Charger. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlCharger" runat="server" CssClass="form-control required_text_box">
                                                                        <asp:ListItem Text="SELECT" Selected="True" Value="-1"></asp:ListItem>
                                                                        <asp:ListItem Text="NO"  Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvCharger" Style="color: red!important;" runat="server" ControlToValidate="ddlCharger" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select the Charger" InitialValue="-1">Please Select the Charger</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-12 text-center">
                                                        <div class="col-md-12">
                                                            <asp:Button TabIndex="3" ID="btnAdd"  OnClick="imgSaveAll_Click" ValidationGroup="SaveAll" runat="server" Text="Add" CssClass="btn btn-primary"></asp:Button>
                                                            <asp:Button TabIndex="3" ID="btnReset" OnClick="imgReset_Click" Style="margin-left: 40px!important" runat="server" Text="Reset" CssClass="btn btn-danger"></asp:Button>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMobexSeller" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>

