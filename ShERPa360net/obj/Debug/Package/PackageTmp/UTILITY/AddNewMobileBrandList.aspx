<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="AddNewMobileBrandList.aspx.cs" Inherits="ShERPa360net.UTILITY.AddNewMobileBrandList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script lang="javascript" type='text/javascript'>
        function ValidateBrandDetail() {
            debugger
            var Isvalidate = true;
            $("#lblbrandname").css("display", "none");
            if ($("#ContentPlaceHolder1_txtBrandName").val().length == 0) {
                $("#lblbrandname").css("display", "block");
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Create  </strong>Brand</h3>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" Text="Cancel" PostBackUrl="/UTILITY/MobileBrand.aspx"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" Text="Save All" ValidationGroup="SaveAll" OnClick="imgSaveAll_Click" OnClientClick="return ValidateBrandDetail()"><i class="fa fa-save"></i></asp:LinkButton>
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

                                            <label class="col-md-5 control-label">Brand Name: </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:TextBox ID="txtBrandName" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvVendorDetail" Style="color: red!important;" runat="server" ControlToValidate="txtBrandName" ValidationGroup="SaveAll"
                                                    ErrorMessage="Please Enter Description" InitialValue="0">Please Enter Description</asp:RequiredFieldValidator>

                                                <asp:Label ID="lblbrandname" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Please Enter Description</asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlstatus" runat="server" CssClass="form-control " TabIndex="5">
                                                    <asp:ListItem Value="-1">--SELECT--</asp:ListItem>
                                                    <asp:ListItem Value="1">ACTIVE</asp:ListItem>
                                                    <asp:ListItem Value="0">INACTIVE</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlstatus" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select Status" InitialValue="-1">Please Select Status</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div id="mobile_View">
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
    <input type="hidden" id="menutabid" value="tsmMstSDBrand" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmMstSD" runat="server" />
</asp:Content>
