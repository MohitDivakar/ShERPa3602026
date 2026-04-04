<%@ Page Title="" Language="C#" MasterPageFile="~/TS/MasterTS.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="ShERPa360net.TS.UserMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgReset" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgSaveAll" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkSearh" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="grvUserMaster" EventName="RowCommand" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;User Master</strong></h3>
                                    <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">Entry</h4>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">First Name. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdUserId" Value="0" />
                                                                <asp:TextBox ID="txtfirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvfirstName" Style="color: red!important;" runat="server" ControlToValidate="txtfirstName" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter First Name">Please Enter First Name</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Last Name. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtlastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvLastName" Style="color: red!important;" runat="server" ControlToValidate="txtlastName" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Last Name">Please Enter Last Name</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">User Code. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtUserCode" runat="server" CssClass="form-control" placeholder="User Code"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvUserCode" Style="color: red!important;" runat="server" ControlToValidate="txtUserCode" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter User Code">Please Enter User Code</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Plant. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPlant" Style="color: red;" ControlToValidate="ddlPlant" runat="server" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Department. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDepartment" Style="color: red;" ControlToValidate="ddlDepartment" runat="server" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Select Department" InitialValue="0">Please Select Department</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Role. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvRole" Style="color: red;" ControlToValidate="ddlRole" runat="server" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Select Role" InitialValue="0">Please Select Role</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Status. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvStatus" Style="color: red;" ControlToValidate="ddlStatus" runat="server" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Select Status" InitialValue="-1">Please Select Status</asp:RequiredFieldValidator>
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
                                            <div class="panel panel-default tabs">
                                                <ul class="nav nav-tabs" role="tablist">
                                                    <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Details</a></li>
                                                </ul>
                                                <div class="panel-body tab-content">
                                                    <div class="tab-pane active" id="tab-first">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12 divhorizontal" style="overflow-x: scroll;">
                                                                    <asp:GridView ID="grvUserMaster" OnRowCommand="grvUserMaster_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" ID="lnkEdit" CommandArgument='<%#Eval("USERID") %>' CommandName="eEdit">Edit</asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="First Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblFirstName" Text='<%# Bind("USERFIRSTNAME") %>'></asp:Label>
                                                                                    <asp:Label Visible="false" runat="server" ID="lblPlantId" Text='<%# Bind("USERPLANT") %>'></asp:Label>
                                                                                    <asp:Label Visible="false" runat="server" ID="lblDepartmentId" Text='<%# Bind("USERDEPARTMENT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Last Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblLastName" Text='<%# Bind("USERLASTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="User Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblUserCode" Text='<%# Bind("USERCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Plant">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblPlant" Text='<%# Bind("USERPLANTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Department">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblDepartment" Text='<%# Bind("USERDEPARTMENTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Role">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblRole" Text='<%# Bind("USERROLE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("USERSTATUSVALUE") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblID" Text='<%# Bind("USERID") %>' Visible="false"></asp:Label>
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranTATASKYUSER" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranTATASKY" runat="server" />
</asp:Content>

