<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="CreateModelList.aspx.cs" Inherits="ShERPa360net.UTILITY.CreateModelList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>
    <script src="../HelpViewer/js/jquery.min.js"></script>
    <script lang="javascript" type='text/javascript'>


        function ShowLoading() {
            var Isvalidate = true;
            $("#lblvendor").css("display", "none");
            if ($("#ContentPlaceHolder1_txtmodelname").val().length == 0) {
                $("#lblvendor").css("display", "block");
                Isvalidate = false;
            }
            $("#Label1").css("display", "none");
            if ($("#ContentPlaceHolder1_txtdisplayname").val().length == 0) {
                $("#Label1").css("display", "block");
                Isvalidate = false;
            }
            $("#Label2").css("display", "none");
            if ($("#ContentPlaceHolder1_txtdisplayname2").val().length == 0) {
                $("#Label2").css("display", "block");
                Isvalidate = false;
            }
            $("#Label3").css("display", "none");
            if ($("#ContentPlaceHolder1_txtcustomerdesc").val().length == 0) {
                $("#Label3").css("display", "block");
                Isvalidate = false;
            }
            return Isvalidate;
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Create  </strong>New Model</h3>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" Text="Cancel" PostBackUrl="/UTILITY/AddNewModel.aspx"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" Text="Save All" OnClientClick="ShowLoading()" ValidationGroup="SaveAll" OnClick="imgSaveAll_Click"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">
                                    <h4 style="color: #f05423"></h4>

                                    <div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-5 control-label">Brand Name : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList ID="ddlbrandname" runat="server" CssClass="form-control" TabIndex="1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlbrandname" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select List" InitialValue="0">Please Select List</asp:RequiredFieldValidator>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">


                                            <label class="col-md-5 control-label">Model Name: </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:TextBox ID="txtmodelname" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvVendorDetail" Style="color: red!important;" runat="server" ControlToValidate="txtmodelname" ValidationGroup="SaveAll"
                                                    ErrorMessage="Please Enter Description" InitialValue="0">Please Enter Description</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblvendor" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Please Enter Description</asp:Label>
                                            </div>



                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Display Name: </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtdisplayname" runat="server" CssClass="form-control" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red!important;" runat="server" ControlToValidate="txtdisplayname" ValidationGroup="SaveAll"
                                                    ErrorMessage="Please Enter Description" InitialValue="0">Please Enter Description</asp:RequiredFieldValidator>
                                                <asp:Label ID="Label1" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Please Enter Description</asp:Label>
                                            </div>

                                        </div>

                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">DIS(For Blynk) : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtdisplayname2" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red!important;" runat="server" ControlToValidate="txtdisplayname2" ValidationGroup="SaveAll"
                                                    ErrorMessage="Please Enter Description" InitialValue="0">Please Enter Description</asp:RequiredFieldValidator>
                                                <asp:Label ID="Label2" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Please Enter Description</asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="row" style="padding-left: 56px">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Customer Desc : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtcustomerdesc" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red!important;" runat="server" ControlToValidate="txtcustomerdesc" ValidationGroup="SaveAll"
                                                    ErrorMessage="Please Enter Description" InitialValue="0">Please Enter Description</asp:RequiredFieldValidator>
                                                <asp:Label ID="Label3" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Please Enter Description</asp:Label>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Product Category : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList ID="ddlproductcateg" runat="server" CssClass="form-control" TabIndex="1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlproductcateg" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select List" InitialValue="0">Please Select List</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddstatus" runat="server" CssClass="form-control " TabIndex="5">
                                                    <asp:ListItem Value="-1">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="True" Selected="True">ACTIVE</asp:ListItem>
                                                    <asp:ListItem Value="False">INACTIVE</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddstatus" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select Status" InitialValue="-1">Please Select Status</asp:RequiredFieldValidator>

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
</asp:Content>
