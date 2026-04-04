<%@ Page Title="Bank Payment" Language="C#" MasterPageFile="~/FI/MasterFI.Master" AutoEventWireup="true" CodeBehind="frmActualBankPayment.aspx.cs" Inherits="ShERPa360net.FI.frmActualBankPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Bank Payment</title>

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../img/collapse_blue.png";
            } else {
                div.style.display = "none";
                img.src = "../img/expand_blue.png";
            }
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Bank Payment  </strong>Entry</h3>
                            <%--<asp:LinkButton ID="imgDelete" runat="server" CssClass="btn btn-success pull-right" Text="Delete" TabIndex="2"><i class="fa fa-trash"></i></asp:LinkButton>--%>
                            <asp:LinkButton ID="imgSave" runat="server" CssClass="btn btn-success pull-right" Text="Save" TabIndex="1" OnClick="imgSave_Click"><i class="fa fa-save"></i></asp:LinkButton>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                                <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound" ShowHeader="true">
                                                    <EmptyDataTemplate>
                                                        No Record Found!
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle CssClass="header" />
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <a href="JavaScript:divexpandcollapse('div<%# Eval("DOCNO") %>');">
                                                                    <img id="imgdiv<%# Eval("DOCNO") %>" width="14px" border="0" src="../img/expand_blue.png" />
                                                                </a>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Select">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bank Account">
                                                            <HeaderTemplate>
                                                                <asp:DropDownList ID="ddlBankAccountAll" runat="server" OnSelectedIndexChanged="ddlBankAccountAll_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlBankAccount" runat="server"></asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Doc No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDOCNO" runat="server" Text='<%# Eval("DOCNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Doc Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDOCTYPE" runat="server" Text='<%# Eval("DOCTYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Doc Type Descr">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDESCR" runat="server" Text='<%# Eval("DESCR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Doc Dt.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDOCDT" runat="server" Text='<%# Eval("DOCDT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pay Flag">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPAYTYPE" runat="server" Text='<%# Eval("PAYTYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Party Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNAME1" runat="server" Text='<%# Eval("NAME1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amt.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDOCAMT" runat="server" Text='<%# Eval("DOCAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Adj. Amt.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblADJAMT" runat="server" Text='<%# Eval("ADJAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Pay Mode">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPAYMODE" runat="server" Text='<%# Eval("PAYMODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Create By">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Create Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCREATEDATE" runat="server" Text='<%# Eval("CREATEDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td colspan="100%">
                                                                        <div id="div<%# Eval("DOCNO") %>" style="display: none; overflow-x: scroll; max-height: 500px !important;" class="box-body divhorizontal">


                                                                            <asp:GridView ID="gvInnerList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered nowrap" CellSpacing="0"
                                                                                ShowHeaderWhenEmpty="true" Width="100%">

                                                                                <Columns>

                                                                                    <asp:TemplateField HeaderText="ID">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Doc Type">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPDOCTYPE" runat="server" Text='<%# Eval("PDOCTYPE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Doc Type Descr.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblDOCTYPE" runat="server" Text='<%# Eval("DOCTYPE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Doc No.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblPDOCNO" runat="server" Text='<%# Eval("PDOCNO") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Ref. Doc Type">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblREFTYPE" runat="server" Text='<%# Eval("REFTYPE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Ref. Doc No.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblREFNO" runat="server" Text='<%# Eval("REFNO") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Adj. Amt.">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblADJAMT" runat="server" Text='<%# Eval("ADJAMT") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="CR/DR">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblCRDR" runat="server" Text='<%# Eval("CRDR") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Create By">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                    <asp:TemplateField HeaderText="Create Date">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblCREATEDATE" runat="server" Text='<%# Eval("CREATEDATE") %>'></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>

                                                                                </Columns>

                                                                            </asp:GridView>

                                                                        </div>
                                                                    </td>
                                                                </tr>
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
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranFIBP" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranFI" runat="server" />

</asp:Content>
