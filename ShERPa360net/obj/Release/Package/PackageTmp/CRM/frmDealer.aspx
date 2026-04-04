<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmDealer.aspx.cs" Inherits="ShERPa360net.CRM.frmDealer" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Dealer Master</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Dealer Registration</strong></h3>
                        </div>

                        <%--   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtPincode" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="lnkNext1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkPrevious2" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkNext2" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkPrevious3" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkNext3" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkPrevious7" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkSave" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>--%>
                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <br />
                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Shop Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control required_text_box" placeholder="Shop Name" TabIndex="1" OnTextChanged="txtName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvName" Style="color: red;" ControlToValidate="txtName" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Please Enter Shop Name" Display="Dynamic">Please Enter Shop Name</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rfvNameSpecial" runat="server" Style="color: red;" ControlToValidate="txtName" ValidationGroup="Save1" ValidationExpression="^[A-Za-z0-9 _]*$"
                                                                Display="Dynamic" ErrorMessage="Special Character are not allowed.">Special Character are not allowed.</asp:RegularExpressionValidator>


                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divContactNo" runat="server" visible="true">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Contact No :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control required_text_box" placeholder="Contact No" TabIndex="1" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvContactNo" Style="color: red;" ControlToValidate="txtContactNo" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Please Enter Contact No" Display="Dynamic">Please Enter Contact No</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divContactNo2" runat="server" visible="true">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Contact No2 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtContactNo2" runat="server" CssClass="form-control required_text_box" placeholder="Contact No2" TabIndex="1"></asp:TextBox>
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                             <div class="col-md-12" id="divContactNo3" runat="server" visible="true">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Contact No3 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtContactNo3" runat="server" CssClass="form-control required_text_box" placeholder="Contact No3" TabIndex="1"></asp:TextBox>
                                                            
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divCategory" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Category :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control required_text_box" TabIndex="2"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvShortName" Style="color: red;" ControlToValidate="ddlCategory" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Select Category" InitialValue="0">Select Category</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext1" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" TabIndex="3" ID="lnkNext1" CssClass="btn btn-success pull-right" Text="Next" OnClick="lnkNext1_Click" ValidationGroup="Save1"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divAddress1" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Address 1 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control required_text_box" TabIndex="4" placeholder="Address 1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvAddress1" Style="color: red;" ControlToValidate="txtAddress1" runat="server" ValidationGroup="Save2"
                                                                ErrorMessage="Please Enter Dealer Address" Display="Dynamic">Please Enter Dealer Address</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rfvADrres1" runat="server" Style="color: red;" ControlToValidate="txtAddress1" ValidationGroup="Save2" ValidationExpression="^[A-Za-z0-9 _]*$"
                                                                Display="Dynamic" ErrorMessage="Special Character are not allowed.">Special Character are not allowed.</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divAddress2" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Address 2 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Address 2" TabIndex="5"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Dealer Short Name">Please Enter Dealer Short Name</asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator ID="rfvAdress2" runat="server" Style="color: red;" ControlToValidate="txtAddress2" ValidationGroup="Save2" ValidationExpression="^[A-Za-z0-9 _]*$"
                                                                Display="Dynamic" ErrorMessage="Special Character are not allowed.">Special Character are not allowed.</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divAddress3" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Address 3 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" placeholder="Address 3" TabIndex="6"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Dealer Short Name">Please Enter Dealer Short Name</asp:RequiredFieldValidator>--%>

                                                            <asp:RegularExpressionValidator ID="rfvaddress3" runat="server" Style="color: red;" ControlToValidate="txtAddress3" ValidationGroup="Save2" ValidationExpression="^[A-Za-z0-9 _]*$"
                                                                Display="Dynamic" ErrorMessage="Special Character are not allowed.">Special Character are not allowed.</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divArea" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Area :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlArea" runat="server" CssClass="form-control select2" Width="100%">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" ControlToValidate="ddlArea" runat="server" ValidationGroup="Save2"
                                                                InitialValue="0" ErrorMessage="Please Enter Area Name">Please Enter Area Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext2" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrevious2" CssClass="btn btn-success pull-left" Text="Previous" TabIndex="7" OnClick="lnkPrevious2_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext2" CssClass="btn btn-success pull-right" Text="Next" TabIndex="8" OnClick="lnkNext2_Click" ValidationGroup="Save2"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-12" id="divPincode" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Pin Code :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control required_text_box" placeholder="Pincode" TabIndex="9" OnTextChanged="txtPincode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvPincode" Style="color: red;" ControlToValidate="txtPincode" runat="server" ValidationGroup="Save3"
                                                                ErrorMessage="Please Enter Pincode" Display="Dynamic">Please Enter Pincode</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divState" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">State :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Address 2"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" TabIndex="10"></asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Dealer Short Name">Please Enter Dealer Short Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divCity" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">City :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control required_text_box" placeholder="City" TabIndex="11"></asp:TextBox>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <asp:RequiredFieldValidator ID="rfvCity" Style="color: red;" ControlToValidate="txtCity" runat="server" ValidationGroup="Save3"
                                                                ErrorMessage="Please Enter City Name" Display="Dynamic">Please Enter City Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divCountry" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Country :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Address 2"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" TabIndex="12"></asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Dealer Short Name">Please Enter Dealer Short Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext3" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrevious3" CssClass="btn btn-success pull-left" TabIndex="13" Text="Previous" OnClick="lnkPrevious3_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext3" CssClass="btn btn-success pull-right" TabIndex="14" Text="Next" OnClick="lnkNext3_Click" ValidationGroup="Save3"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divShop" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Shop Photo :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload ID="fuShopImage" runat="server" CssClass="form-control required_text_box" TabIndex="15" />
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="IFSC Code" TabIndex="26"></asp:TextBox>--%>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtCity" runat="server" ValidationGroup="Save3"
                                                                        ErrorMessage="Please Enter City Name">Please Enter City Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Biker :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlBiker" runat="server" CssClass="form-control select2" Width="100%">
                                                            </asp:DropDownList>
                                                            <%-- <asp:RequiredFieldValidator ID="rfvBikerName" Style="color: red;" ControlToValidate="ddlBiker" runat="server" ValidationGroup="FSave"
                                                                InitialValue="0" ErrorMessage="Please Select Biker Name">Please Select Biker Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">MaxKroDays :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlmaxday" ClientIDMode="Static" runat="server" CssClass="form-control ddlVendor required_text_box"></asp:DropDownList>
                                                            <asp:Label ID="lblmaxday" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none!important;">Please Select Max Day.</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">IsKRO : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <asp:CheckBox ID="chkIsKro" runat="server" CssClass="form-control" Checked="false" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>


                                            <div class="col-md-12" id="dviFinal" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrevious7" CssClass="btn btn-success pull-left" TabIndex="16" Text="Previous" OnClick="lnkPrevious7_Click" ><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-success pull-right" TabIndex="17" Text="SAVE" ValidationGroup="FSave" OnClick="lnkSave_Click" OnClientClick="return ShowErrorMessageMaxDay();"><%--<i class="fa fa-arrow-circle-right"></i>--%></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmMstCRMDlrMst" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmMstCRM" runat="server" />

</asp:Content>
