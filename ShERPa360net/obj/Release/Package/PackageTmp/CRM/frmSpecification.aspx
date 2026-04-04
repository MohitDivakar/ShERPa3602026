<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmSpecification.aspx.cs" Inherits="ShERPa360net.CRM.frmSpecification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Add  </strong>Laptop Job Specification</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <fieldset class="scheduler-border">
                                    <legend class="scheduler-border">laptop Job Specification Entry</legend>

                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Job ID : </label>
                                                <div class="col-md-9 col-xs-12">
                                                    <asp:TextBox ID="txtJobID" runat="server" CssClass="form-control" OnTextChanged="txtJobID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvJobID" runat="server" ControlToValidate="txtJobID" ValidationGroup="Save" Style="color: red;"
                                                        ErrorMessage="Please Enter Job ID" Display="Dynamic">Please Enter Job ID</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Make : </label>
                                                <div class="col-md-9 col-xs-12">
                                                    <asp:TextBox ID="txtMake" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvMake" runat="server" ControlToValidate="txtMake" ValidationGroup="Save" Style="color: red;"
                                                        ErrorMessage="Please Enter Make" Display="Dynamic">Please Enter Make</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Model : </label>
                                                <div class="col-md-9 col-xs-12">
                                                    <asp:TextBox ID="txtModel" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvModel" runat="server" ControlToValidate="txtModel" ValidationGroup="Save" Style="color: red;"
                                                        ErrorMessage="Please Enter Model" Display="Dynamic">Please Enter Model</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">RAM : </label>
                                                <div class="col-md-9 col-xs-12">
                                                    <asp:DropDownList ID="ddlRAM" runat="server" CssClass="form-control" TabIndex="1"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvRAM" runat="server" ControlToValidate="ddlRAM" ValidationGroup="Save" Style="color: red;"
                                                        ErrorMessage="Please Select RAM" Display="Dynamic" InitialValue="0">Please Select RAM</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">ROM : </label>
                                                <div class="col-md-9 col-xs-12">
                                                    <asp:DropDownList ID="ddlROM" runat="server" CssClass="form-control" TabIndex="1"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvROM" runat="server" ControlToValidate="ddlROM" ValidationGroup="Save" Style="color: red;"
                                                        ErrorMessage="Please Select ROM" Display="Dynamic" InitialValue="0">Please Select ROM</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Color : </label>
                                                <div class="col-md-9 col-xs-12">
                                                    <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control" TabIndex="1" Enabled="false"></asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="ddlColor" ValidationGroup="Save" Style="color: red;"
                                                        ErrorMessage="Please Select Color" Display="Dynamic" InitialValue="0">Please Select Color</asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Serial No. : </label>
                                                <div class="col-md-9 col-xs-12">
                                                    <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" ControlToValidate="txtSerialNo" ValidationGroup="Save" Style="color: red;"
                                                        ErrorMessage="Please Enter Serial No." Display="Dynamic">Please Enter Serial No.</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">MRP : </label>
                                                <div class="col-md-9 col-xs-12">
                                                    <asp:TextBox ID="txtMRP" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvMRP" runat="server" ControlToValidate="txtMRP" ValidationGroup="Save" Style="color: red;"
                                                        ErrorMessage="Please Enter MRP" Display="Dynamic">Please Enter MRP</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label">Item Code : </label>
                                                <div class="col-md-9 col-xs-12">
                                                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="txtItemCode" ValidationGroup="Save" Style="color: red;"
                                                        ErrorMessage="Please Enter Item Code" Display="Dynamic">Please Enter Item Code</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-6">
                                            <div class="form-group">

                                                <label class="col-md-3 control-label"></label>
                                                <div class="col-md-9 col-xs-12">
                                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-left" OnClick="imgSaveAll_Click" Text="Save All" TabIndex="4" ValidationGroup="Save"><i class="fa fa-save"></i></asp:LinkButton>
                                                    <%--<asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-left" PostBackUrl="~/CONFIGURATION/frmViewSpecGrpMaster.aspx" Text="Cancel"><i class="fa fa-times" tabindex="5"></i></asp:LinkButton>--%>
                                                </div>

                                            </div>
                                        </div>



                                    </div>

                                </fieldset>
                            </div>
                        </div>
                        <%--<div class="panel-footer">
                            <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="SaveItem" TabIndex="3"><i class="fa fa-plus-square"></i></asp:LinkButton>
                        </div>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranJobSpec" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />

</asp:Content>
