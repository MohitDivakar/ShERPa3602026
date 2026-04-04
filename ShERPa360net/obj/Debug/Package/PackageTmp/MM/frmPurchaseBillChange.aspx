<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="frmPurchaseBillChange.aspx.cs" Inherits="ShERPa360net.MM.frmPurchaseBillChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Change Bill Number in PB</title>


    <%--    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {

            var bill = $("#ContentPlaceHolder1_txtBillDate").val();
            var vend = $("#ContentPlaceHolder1_txtVendor").val();
            var name = $("#ContentPlaceHolder1_txtVendorName").val();
            var blno = $("#ContentPlaceHolder1_txtBillNo").val();
            if (bill != "" && vend != "" && name != "" && blno != "") {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            }
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Change Bill No. </strong></h3>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <%--<h4 style="color: #f05423">PB Details</h4>--%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">PB No. :</label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtPBNO" runat="server" CssClass="form-control" OnTextChanged="txtPBNO_TextChanged" AutoPostBack="true" placeholder="PB No." ValidationGroup="SaveDet"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Current Bill No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtCurrentBillNo" runat="server" Enabled="false" CssClass="form-control" placeholder="Current Bill No." ReadOnly="true"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">New Bill No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control" placeholder="New Bill No." OnTextChanged="txtBillNo_TextChanged" AutoPostBack="true" ValidationGroup="SaveDet"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                    </div>

                                </div>

                            </div>
                        </div>

                        <div class="panel-footer">
                            <asp:HiddenField ID="hfVendorCode" runat="server" />
                            <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="SaveDet">SAVE</asp:LinkButton>
                        </div>
                    </div>
                </div>

            </div>
        </div>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMPO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
