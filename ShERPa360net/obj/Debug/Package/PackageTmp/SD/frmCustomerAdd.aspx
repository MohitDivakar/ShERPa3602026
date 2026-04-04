<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmCustomerAdd.aspx.cs" Inherits="ShERPa360net.SD.frmCustomerAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker({
                dateFormat: 'dd-mm-yyyy'
            });
        });
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({
                    dateFormat: 'dd-mm-yyyy'
                });
            }
        });
    </script>
    <asp:HiddenField runat="server" ID="hdnSelectedTab" Value="tab-first"></asp:HiddenField>
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Create  </strong>Customer</h3>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/SD/frmCustomerList.aspx" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" ValidationGroup="SaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All"  OnClientClick="return isValidatForm();"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 style="color: #f05423">Customer Details</h4>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-4 control-label">Customer Type: </label>
                                                            <div class="col-md-8 col-xs-12">
                                                                <%--<asp:TextBox ID="txtDocType" runat="server" CssClass="form-control" Text="103" Enabled="false"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlCustomerType" OnSelectedIndexChanged="ddlCustomerType_SelectedIndexChanged" runat="server" placeholder="Customer Type" CssClass="form-control" Width="205" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="vddlCustomerType" runat="server" ControlToValidate="ddlCustomerType" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Select List" InitialValue="0">Please Cutomer Type</asp:RequiredFieldValidator>

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-4 control-label">Customer Code: </label>
                                                            <div class="col-md-8 col-xs-12">
                                                                <asp:TextBox ID="txtCustomerCode" onfocusin="onlynumber();" ClientIDMode="Static" runat="server" Enabled="false" placeholder="Customer Code" CssClass="form-control numberonly"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="vtxtCustomerCode" runat="server" ControlToValidate="txtCustomerCode" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please insert Customer code">Please insert customer code</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-4 control-label">Category Type: </label>
                                                            <div class="col-md-8 col-xs-12">
                                                                <%--<asp:TextBox ID="txtDocType" runat="server" CssClass="form-control" Text="103" Enabled="false"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlCategory" runat="server" placeholder="Category Type" CssClass="form-control" Width="205" AutoPostBack="false"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="vddlCategory" runat="server" ControlToValidate="ddlCategory" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please select Customer Type" InitialValue="0">Please Category Type</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-4 control-label">Title: </label>
                                                            <div class="col-md-8 col-xs-12">
                                                                <asp:DropDownList ID="ddlTitle" runat="server" CssClass="form-control pd-35 ddlCharger required_text_box">
                                                                    <asp:ListItem Text="SELECT" Selected="True" Value=""></asp:ListItem>
                                                                    <asp:ListItem Text="M/s" Value="1"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-4 control-label">Old Customer Code: </label>
                                                            <div class="col-md-8 col-xs-12">
                                                                <asp:TextBox ID="txtoldCustmerCode" runat="server" CssClass="form-control" placeholder="Old Customer Code"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                    </div>
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label class="col-md-4 control-label">Name: </label>
                                                                    <div class="col-md-8 col-xs-12">
                                                                        <asp:TextBox ID="txtName"  onfocusout="CustomerValidateName()"  runat="server" CssClass="form-control"  placeholder="Name"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="vtxtName" runat="server" ControlToValidate="txtName" ValidationGroup="SaveAll" Style="color: red;"
                                                                            ErrorMessage="Please insert Name1">Please insert Name1</asp:RequiredFieldValidator>
                                                                        <br />
                                                                              <asp:Label ID="lblName2" CssClass="lblName2" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Name already exists.</asp:Label>

                                                                        <%--<asp:CustomValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtName" ValidationGroup="SaveAll" ForeColor="Red"
                                                                            ErrorMessage="Name already exists" Enabled="false">Name already exists</asp:CustomValidator>--%>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-3">
                                                                <div class="form-group">
                                                                    <label class="col-md-4 control-label">Name2: </label>
                                                                    <div class="col-md-8 col-xs-12">
                                                                        <asp:TextBox ID="txtName2"  runat="server" CssClass="form-control" placeholder="Name2"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="vtxtName2" runat="server" ControlToValidate="txtName2" ValidationGroup="SaveAll" Style="color: red;"
                                                                            ErrorMessage="Please insert Name2">Please insert Name2</asp:RequiredFieldValidator>
                                                                        <br />
                                                                        <asp:CustomValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtName2" ValidationGroup="SaveAll" ForeColor="Red"
                                                                            ErrorMessage="Name already exists" Enabled="false">Name already exists</asp:CustomValidator>
                                                                        <br />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-4 control-label">Short Name: </label>
                                                            <div class="col-md-8 col-xs-12">
                                                                <asp:TextBox ID="txtShortname" runat="server" CssClass="form-control" placeholder="Short Name"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="vtxtShortname" runat="server" ControlToValidate="txtShortname" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please insert short name">Please insert short name</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-4 control-label">Customer Group: </label>
                                                            <div class="col-md-8 col-xs-12">
                                                                <%--<div class="input-group">--%>
                                                                <asp:DropDownList ID="ddlCustomerGroup" runat="server" CssClass="form-control" Width="205" placeholder="Customer Group" AutoPostBack="false"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="vddlCustomerGroup" runat="server" ControlToValidate="ddlCustomerGroup" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Select Shortname" InitialValue="0">Please Customer Group</asp:RequiredFieldValidator>
                                                                <%--</div>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="page-content-wrap">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="panel panel-default tabs">
                                                <ul class="nav nav-tabs" role="tablist">
                                                    <li class="active" id="tabfirstli"><a href="#tabfirst" role="tab" data-toggle="tab">Address</a></li>
                                                    <li id="tabsecondli"><a href="#tabsecond" role="tab" data-toggle="tab">Communication</a></li>
                                                    <li><a href="#tabthird" role="tab" data-toggle="tab">Taxation</a></li>
                                                    <li><a href="#tabfourth" role="tab" data-toggle="tab">Bank Details</a></li>
                                                </ul>
                                                <div class="panel-body tab-content">
                                                    <div class="tab-pane active" id="tabfirst">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">

                                                                    <div class="col-md-12">
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Address: </label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" placeholder="Address 1"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAddress1" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please insert Address">Please insert Address</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <%--                  </div>
                                                                    <div class="col-md-12">--%>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label"></label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Address 2"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="vtxtAddress2" runat="server" ControlToValidate="txtAddress2" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please insert Address">Please insert Address 2</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <%--</div>
                                                                    <div class="col-md-12">--%>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label"></label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtAdddress3" runat="server" CssClass="form-control" placeholder="Address 3"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                <div class="col-md-4">

                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Pincode:</label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:TextBox ID="txtPincode" runat="server" MaxLength="6" CssClass="form-control numberonly" placeholder="Pincode" OnTextChanged="txtPincode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="vtxtPincode" runat="server" ControlToValidate="txtPincode" ValidationGroup="SaveAll" Style="color: red;"
                                                                                                ErrorMessage="Please insert pincode">Please insert pincode</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>

                                                                                </div>
                                                                                <div class="col-md-4">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Zone:</label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:DropDownList ID="ddlzone" runat="server" placeholder="countries" CssClass="form-control" Width="205" AutoPostBack="false"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="vddlzone" runat="server" ControlToValidate="ddlzone" ValidationGroup="SaveAll" Style="color: red;"
                                                                                                ErrorMessage="Please select zone" InitialValue="0">Please select zone</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-4">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Region:</label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:TextBox ID="txtRegion" runat="server" CssClass="form-control" placeholder="Region"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRegion" ValidationGroup="SaveAll" Style="color: red;"
                                                                                                ErrorMessage="Please select region" InitialValue="0">Please select region</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>


                                                                            <div class="col-md-12" style="margin-top: 10px;">
                                                                                <div class="col-md-4">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Country:</label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:DropDownList ID="ddlcountry" runat="server" placeholder="countries" CssClass="form-control" Width="205" AutoPostBack="false"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="vddlcountry" runat="server" ControlToValidate="ddlcountry" ValidationGroup="SaveAll" Style="color: red;"
                                                                                                ErrorMessage="Please select country" InitialValue="0">Please select country</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-4">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">State:</label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:DropDownList ID="ddlstate" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" placeholder="countries" CssClass="form-control" Width="205" AutoPostBack="true"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlstate" ValidationGroup="SaveAll" Style="color: red;"
                                                                                                ErrorMessage="Please select state" InitialValue="0">Please select state</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-4">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">City:</label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:DropDownList ID="ddlcity" runat="server" placeholder="countries" CssClass="form-control" Width="205" AutoPostBack="false"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="vddlcity" runat="server" ControlToValidate="ddlcity" ValidationGroup="SaveAll" Style="color: red;"
                                                                                                ErrorMessage="Please select city" InitialValue="0">Please select city</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>

                                                                    <div class="col-md-12" style="margin-top: 0px;">
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Mobile:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" CssClass="form-control numberonly" placeholder="Mobile"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="vMobile" runat="server" ControlToValidate="txtMobile" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please insert Mobile">Please insert Mobile</asp:RequiredFieldValidator>

                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Email:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please insert email">Please insert email</asp:RequiredFieldValidator>
                                                                                    <br />
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                                                        ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                                                        Display="Dynamic" ErrorMessage="Invalid email address" />
                                                                                    <%--                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtEmail"
                                                                                        ForeColor="Red" Display="Dynamic" ErrorMessage="Required" />--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Website:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" placeholder="Website"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12" style="margin-top: 0px;">
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Contact Person:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" placeholder="Contact person"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="txt" runat="server" ControlToValidate="txtContactPerson" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please insert contact person">Please insert contact person</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-4">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Contact No:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control numberonly" MaxLength="10" placeholder="Contact No"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactNo" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please insert contact no">Please insert contact no</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="tab-pane" id="tabsecond">
                                                        <asp:UpdatePanel ID="pnldesignation" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="grvCommunication" EventName="RowCommand" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-12">

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Designation:</label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:HiddenField runat="server" ID="hndID" Value='0'></asp:HiddenField>
                                                                                            <asp:DropDownList ID="ddlDesignationdes" runat="server" placeholder="countries" CssClass="form-control" Width="205" AutoPostBack="false"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="vddlDesignationdes" runat="server" ControlToValidate="ddlDesignationdes" ValidationGroup="SaveAllCommuncation" Style="color: red;"
                                                                                                ErrorMessage="Please select designation" InitialValue="0">Please select designation</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">
                                                                                            Name:
                                                                                        </label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:TextBox ID="txtNamedes" runat="server" CssClass="form-control" placeholder="Name"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="vtxtNamedes" runat="server" ControlToValidate="txtNamedes" ValidationGroup="SaveAllCommuncation" Style="color: red;"
                                                                                                ErrorMessage="Please insert short name">Please insert Name</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">
                                                                                            Contact No:
                                                                                        </label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:TextBox ID="txtContactNodes" runat="server" CssClass="form-control numberonly" MaxLength="10" placeholder="Contact No"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="vtxtContactNodes" runat="server" ControlToValidate="txtContactNodes" ValidationGroup="SaveAllCommuncation" Style="color: red;"
                                                                                                ErrorMessage="Please insert short name">Please insert contact no</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">
                                                                                            Email:
                                                                                        </label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:TextBox ID="txtEmaildes" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="vtxtEmaildes" runat="server" ControlToValidate="txtEmaildes" ValidationGroup="SaveAllCommuncation" Style="color: red;"
                                                                                                ErrorMessage="Please insert short name">Please insert email</asp:RequiredFieldValidator>
                                                                                            <br />
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"  ValidationGroup="SaveAllCommuncation" ControlToValidate="txtEmaildes"
                                                                                                ForeColor="Red" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                                                                                Display="Dynamic" ErrorMessage="Invalid email address" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>
                                                                            <div class="col-md-12" style="margin-top: 15px;">
                                                                                <div class="panel-footer">
                                                                                    <asp:LinkButton ID="lnkSaveCharges" runat="server" CssClass="btn btn-success pull-right" Text="Save Charges" ValidationGroup="SaveAllCommuncation" OnClick="lnkSaveCharges_Click"><i class="fa fa-plus-square"></i></asp:LinkButton>
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
                                                                                                <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                                                                                    <asp:HiddenField runat="server" ID="hdISEdit" Value="false"></asp:HiddenField>
                                                                                                    <asp:GridView ID="grvCommunication" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%"
                                                                                                        OnRowCommand="grvCommunication_RowCommand">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:HiddenField runat="server" ID="hdngrdID" Value='<%# Bind("ID") %>'></asp:HiddenField>

                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="DesignationID" Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblgrdDesignationID" Text='<%# Bind("DesignationID") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Designation">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblgrddesignation" Text='<%# Bind("Designation") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Name">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblgrdname" Text='<%# Bind("Name") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="ContactNo">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblgrdcontactno" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Email">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblgrdEmail" Text='<%# Bind("Email") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Action">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                                                        CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
                                                                                                                    |
                                                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                                                        CommandName="eEdit">Edit</asp:LinkButton>
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


                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>

                                                    <div class="tab-pane" id="tabthird">

                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <h5 style="color: orangered">Taxation</h5>
                                                                    <div class="col-md-12">

                                                                        <div class="col-md-12" style="margin-top: 0px;">
                                                                            <div class="col-md-6">
                                                                                <div class="form-group">
                                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                                                        <Triggers>
                                                                                        </Triggers>
                                                                                        <ContentTemplate>
                                                                                            <label class="col-md-4 control-label">CST No.:</label>
                                                                                            <div class="col-md-8 col-xs-12">
                                                                                                <asp:TextBox ID="txtCstNo" runat="server" OnTextChanged="txtCstNo_TextChanged" AutoPostBack="true" CssClass="form-control numberonly" placeholder="CST No"></asp:TextBox>
                                                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCstNo" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            ErrorMessage="Please insert  CST No">Please insert CST No</asp:RequiredFieldValidator>--%>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Date :</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtCstdate" runat="server" CssClass="form-control datepicker" placeholder="Date"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCstdate" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please insert  CST Date" Enabled="false">Please insert CST Date</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12" style="margin-top: 0px;">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                                                    <Triggers>
                                                                                    </Triggers>
                                                                                    <ContentTemplate>

                                                                                        <label class="col-md-4 control-label">LST/CST No.:</label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:TextBox ID="txtLSTNo" runat="server" OnTextChanged="txtLSTNo_TextChanged" AutoPostBack="true" CssClass="form-control numberonly" placeholder="LST/CST No"></asp:TextBox>
                                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtLSTNo" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            ErrorMessage="Please insert LST/CST No">Please insert LST/CST No</asp:RequiredFieldValidator>--%>
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Date :</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtLstDate" runat="server" CssClass="form-control datepicker" placeholder="date"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtLstDate" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please insert LST/CST Date" Enabled="false">Please insert LST/CST Date</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-12" style="margin-top: 0px;">
                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">PAN card No.:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtPanno"  onfocusout="CustomerValidatePanno()"  runat="server" CssClass="form-control" placeholder="PAN card No"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPanno" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please insert PAN No">Please insert PAN </asp:RequiredFieldValidator>
                                                                                    <br />
                                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPanno" ValidationGroup="SaveAll"
                                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}"></asp:RegularExpressionValidator>
                                                                                    <br />
                                                                                    <asp:Label ID="lblPan" CssClass="lblPan" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">PAN card no already exists.</asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">PAN Image:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:FileUpload ID="fuPAN" runat="server" CssClass="form-control" />
                                                                                    <asp:RequiredFieldValidator ID="rfuPAN" Style="color: red;" ControlToValidate="fuPAN" runat="server" ValidationGroup="SaveAll"
                                                                                        ErrorMessage="Please Upload ID Proof Image" Display="Dynamic">Please Upload Pan Image</asp:RequiredFieldValidator>
                                                                                    <label id="lblIdfuPAN" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Adhar card No.:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtAdharNo" runat="server" onfocusout="CustomerValidateAdharNo()" CssClass="form-control numberonly" MaxLength="12" placeholder="Adhar card No"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtAdharNo" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please insert Adhar No ">Please insert Adhar No </asp:RequiredFieldValidator>
                                                                                    <br />
                                                                                    <asp:Label ID="lbladharno" CssClass="lbladharno" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Adhar no already exists.</asp:Label>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">ID Proof:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:FileUpload ID="fuIDProof" runat="server" CssClass="form-control" />
                                                                                    <asp:RequiredFieldValidator ID="rfvFUIDProof" Style="color: red;" ControlToValidate="fuIDProof" runat="server" ValidationGroup="SaveAll"
                                                                                        ErrorMessage="Please Upload ID Proof Image" Display="Dynamic">Please Upload ID Proof Image</asp:RequiredFieldValidator>
                                                                                    <label id="lblIdproofalert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12" style="margin-top: 0px;">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Service Tax Registeration no:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtServiceRNo" runat="server" CssClass="form-control numberonly" placeholder="Service Tax Registeration no"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                                                    <Triggers>
                                                                                    </Triggers>
                                                                                    <ContentTemplate>
                                                                                        <label class="col-md-4 control-label">GST No. :</label>
                                                                                        <div class="col-md-8 col-xs-12">
                                                                                            <asp:TextBox ID="txtGstNo"  onfocusout="CustomerValidateGST()"  runat="server" CssClass="form-control" placeholder="Gst no"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtGstNo" ValidationGroup="SaveAll" Style="color: red;"
                                                                                                ErrorMessage="Please insert Gst no ">Please insert Gst no </asp:RequiredFieldValidator>
                                                                                            <br />
                                                                                            <asp:RegularExpressionValidator ID="rgvGSTNo" runat="server" ControlToValidate="txtGstNo" ValidationGroup="SaveAll"
                                                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid GST No." ValidationExpression="[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}[A-Z]{1}[0-9A-Z]{1}$">Invalid GST No.</asp:RegularExpressionValidator>
                                                                                            <br />
                                                                                            <asp:Label ID="lblGSTNo" CssClass="lblGSTNo" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">GST no already exists.</asp:Label>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">GST Certi. :</label>
                                                                            <div class="col-md-7 col-xs-12">
                                                                                <asp:FileUpload ID="fuGSTCerti" runat="server" CssClass="form-control" />
                                                                                <asp:RequiredFieldValidator ID="rfvFUGST" Style="color: red;" ControlToValidate="fuGSTCerti" runat="server" ValidationGroup="SaveAll"
                                                                                    ErrorMessage="Please Upload GST Certificate Image">Please Upload GST Certificate Image</asp:RequiredFieldValidator>
                                                                                <label id="lblGStCeralert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <h5 style="color: orangered">Excise</h5>
                                                                <div class="col-md-12">
                                                                    <div class="col-md-12" style="margin-top: 0px;">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">ECC No.:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtECC" runat="server" CssClass="form-control numberonly" placeholder="ECC No"></asp:TextBox>
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtECC" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            ErrorMessage="Please insert ECC No ">Please insert ECC No</asp:RequiredFieldValidator>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Excise Registeration No:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtExciseRNO" runat="server" CssClass="form-control numberonly" placeholder="Excise Registeration No"></asp:TextBox>
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtExciseRNO" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            ErrorMessage="Please insert Excise Registeration No">Please insert Excise Registeration No</asp:RequiredFieldValidator>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12" style="margin-top: 0px;">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Excise Range:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtExciseRange" runat="server" CssClass="form-control" placeholder="Excise Range"></asp:TextBox>
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtExciseRange" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            ErrorMessage="Please insert Excise Range">Please insert Excise Range</asp:RequiredFieldValidator>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Excise Division:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" placeholder="Excise Division"></asp:TextBox>
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="TextBox7" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            ErrorMessage="Please insert Excise Division">Please insert Excise Division</asp:RequiredFieldValidator>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-12" style="margin-top: 0px;">
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Excise Commisionrate:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:TextBox ID="txtExciseCommisionrate" runat="server" CssClass="form-control" placeholder="Excise Commisionrate"></asp:TextBox>
                                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtExciseCommisionrate" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            ErrorMessage="Please insert Excise Commisionrate">Please insert Excise Commisionrate</asp:RequiredFieldValidator>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Excise vendor Type:</label>
                                                                                <div class="col-md-8 col-xs-12">
                                                                                    <asp:DropDownList ID="ddlVendorType" runat="server" placeholder="countries" CssClass="form-control" Width="205" AutoPostBack="false"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="vddlVendorType" runat="server" ControlToValidate="ddlVendorType" ValidationGroup="SaveAll" Style="color: red;"
                                                                                        ErrorMessage="Please select vendor type" InitialValue="0">Please select vendor type</asp:RequiredFieldValidator>
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
                                            <div class="tab-pane " id="tabfourth">

                                                <div class="panel-body">
                                                    <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="col-md-12">
                                                                <div class="col-md-12" style="margin-top: 0px;">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Bank Name:</label>
                                                                            <div class="col-md-8 col-xs-12">
                                                                                <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control" placeholder="Bank Name"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="vtxtBankName" runat="server" ControlToValidate="txtBankName" ValidationGroup="SaveAll" Style="color: red;"
                                                                                    ErrorMessage="Please insert bank name">Please insert bank name</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                                <div class="col-md-12" style="margin-top: 0px;">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Account No:</label>
                                                                            <div class="col-md-8 col-xs-12">
                                                                                <asp:TextBox ID="txtAccountNo" runat="server" CssClass="form-control " placeholder="Account No"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="vtxtAccountNo" runat="server" ControlToValidate="txtAccountNo" ValidationGroup="SaveAll" Style="color: red;"
                                                                                    ErrorMessage="Please insert bank name">Please insert account number </asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12" id="divAccType" runat="server">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Account Type :</label>
                                                                            <div class="col-md-8 col-xs-12">
                                                                                <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvAccountType" Style="color: red;" ControlToValidate="ddlAccountType" runat="server" ValidationGroup="SaveAll"
                                                                                    ErrorMessage="Select Account Type" Display="Dynamic" InitialValue="0">Select Account Type</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-12" style="margin-top: 0px;">
                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                        <Triggers>
                                                                        </Triggers>
                                                                        <ContentTemplate>
                                                                            <div class="col-md-6">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-4 control-label">ISFC Code:</label>
                                                                                    <div class="col-md-8 col-xs-12">
                                                                                        <asp:TextBox ID="txtISFC" runat="server" OnTextChanged="txtIFSCCode_TextChanged" AutoPostBack="true" CssClass="form-control" placeholder="ISFC Code"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="vISFCNo" runat="server" ControlToValidate="txtISFC" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            ErrorMessage="Please insert bank name">Please insert isfc code </asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div class="col-md-12" style="margin-top: 0px;">
                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Cancelled Cheque / Passbook :</label>
                                                                            <div class="col-md-7 col-xs-12">
                                                                                <asp:FileUpload ID="fuCheque" runat="server" CssClass="form-control" />
                                                                                <asp:RequiredFieldValidator ID="rfvFUCheque" Style="color: red;" ControlToValidate="fuCheque" runat="server" ValidationGroup="SaveAll"
                                                                                    ErrorMessage="Please Upload Cancelled Cheque / Passbook Image">Please Upload Cancelled Cheque / Passbook Image</asp:RequiredFieldValidator>
                                                                                <label id="lblChequealert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
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
    <input type="hidden" id="menutabid" value="TSMMSTSDCUST" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmMstSD" runat="server" />
</asp:Content>
