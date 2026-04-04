<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="CreateList.aspx.cs" Inherits="ShERPa360net.UTILITY.CreateList" %>
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
            $("#lblDescalert").css("display", "none");
            if ($("#ContentPlaceHolder1_txtDesc").val().length == 0) {
                $("#lblDescalert").css("display", "block");
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Create  </strong>List</h3>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" Text="Cancel" PostBackUrl="/UTILITY/AddNewList.aspx"><i class="fa fa-times"></i></asp:LinkButton>
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
                                            <label class="col-md-5 control-label">List For : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList ID="ddlList" runat="server" CssClass="form-control" TabIndex="1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlList" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select List" InitialValue="0">Please Select List</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Description: </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:TextBox ID="txtDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="rfvVendorDetail" Style="color: red!important;" runat="server" ControlToValidate="txtDesc" ValidationGroup="SaveAll"
                                                 ErrorMessage="Please Enter Description" InitialValue="0">Please Enter Description</asp:RequiredFieldValidator>
                                                 <asp:Label ID="lblDescalert" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Please Enter Description</asp:Label>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">List Type For: : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlisttype" runat="server" CssClass="form-control " TabIndex="5"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlisttype" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select List Type" InitialValue="0">Please Select List Type</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddstatus" runat="server" CssClass="form-control " TabIndex="5">
                                                    <asp:ListItem Value="-1">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1" Selected="True">ACTIVE</asp:ListItem>
                                                    <asp:ListItem Value="0">INACTIVE</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddstatus" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select Status" InitialValue="-1">Please Select Status</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
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
    <input type="hidden" id="menutabid" value="tsmTranMMMR" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
