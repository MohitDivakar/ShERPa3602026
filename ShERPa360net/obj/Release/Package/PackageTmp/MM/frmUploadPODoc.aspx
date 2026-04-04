<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="frmUploadPODoc.aspx.cs" Inherits="ShERPa360net.MM.frmUploadPODoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Upload PO Documents</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Upload PO Documents</strong></h3>
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
                                                    <label class="col-md-5 control-label">PO No. :</label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtPONo" runat="server" CssClass="form-control" OnTextChanged="txtPONo_TextChanged" AutoPostBack="true" placeholder="PO No." ValidationGroup="SaveDet"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPONo" Style="color: red;" runat="server" ControlToValidate="txtPONo" ValidationGroup="SaveDet"
                                                            ErrorMessage="Please Enter PO No.">Please Enter PO No.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">GRN No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtGRNNo" runat="server" CssClass="form-control" placeholder="GRN No." OnTextChanged="txtGRNNo_TextChanged" AutoPostBack="true" ValidationGroup="SaveDet"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvGRN" Style="color: red;" runat="server" ControlToValidate="txtGRNNo" ValidationGroup="SaveDet"
                                                            ErrorMessage="Please Enter GRN No.">Please Enter GRN No.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Upload Invoice : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <%--<asp:TextBox ID="TextBox1" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc No"></asp:TextBox>--%>
                                                        <asp:FileUpload ID="fuInvDoc" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                                        <asp:RequiredFieldValidator ID="rfvFileUpload" Style="color: red;" runat="server" ControlToValidate="fuInvDoc" ValidationGroup="SaveDet"
                                                            ErrorMessage="Please Upload Invoice Image">
                                                            Please Upload Invoice Image</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revFileUpload" runat="server" ErrorMessage="Only Image files are allowed" Style="color: red;"
                                                            ControlToValidate="fuInvDoc" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG|.png|.PNG|.jpeg|.JPEG|.pdf|.PDF)$">
                                                        </asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Upload PO : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <%--<asp:TextBox ID="TextBox1" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc No"></asp:TextBox>--%>
                                                        <asp:FileUpload ID="fuPODoc" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                                        <asp:RequiredFieldValidator ID="rfvPOUpload" Style="color: red;" runat="server" ControlToValidate="fuPODoc" ValidationGroup="SaveDet"
                                                            ErrorMessage="Please Upload PO Image">
                                                            Please Upload PO Image</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revPOUpload" runat="server" ErrorMessage="Only Image files are allowed" Style="color: red;"
                                                            ControlToValidate="fuPODoc" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG|.png|.PNG|.jpeg|.JPEG|.pdf|.PDF)$">
                                                        </asp:RegularExpressionValidator>
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

    <input type="hidden" id="menutabid" value="tsmTranMMMIRPO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
