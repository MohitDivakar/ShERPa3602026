<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmASMAssign.aspx.cs" Inherits="ShERPa360net.UTILITY.frmASMAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ASM Mapping</title>

    <style type="text/css">
        #container {
            height: 100%;
            width: 100%;
            display: flex;
        }

        #leftThing {
            width: 40%;
            /*background-color: blue;*/
            margin-left: 05px;
            margin-right: 05px;
        }

        #content {
            width: 20%;
            background-color: lightgray;
            margin-left: 05px;
            margin-right: 05px;
            vertical-align: central;
        }

        #rightThing {
            width: 40%;
            /*background-color: yellow;*/
            margin-left: 05px;
            margin-right: 05px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; BDO  </strong>Mapping</h3>
                            </div>
                            <div class="panel-body">
                                <%--<asp:Label runat="server" ClientIDMode="Static" Style="margin-left: 11px!important; font-weight: bold; font-size: 14px;" Visible="false" ID="lblRecoretxt">Record:</asp:Label>
                        <asp:Label runat="server" ClientIDMode="Static" Style="font-weight: bold; font-size: 12px;" ID="lblRecord" Visible="false">0</asp:Label>--%>
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">BDO : </label>
                                                <div class="col-md-8 col-xs-12">
                                                    <asp:DropDownList ID="ddlASM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlASM_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvBDO" runat="server" ControlToValidate="ddlASM" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="Select ASM" InitialValue="0" ValidationGroup="Save">Select ASM</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="col-md-1">&nbsp;</div>
                                        <div class="col-md-10">
                                            <div id="container">

                                                <div id="leftThing">
                                                    <asp:Label runat="server" ClientIDMode="Static" Text="0" ID="lblUnMappedBiker" ></asp:Label>
                                                    <asp:ListBox ID="lstBoxAll" runat="server" CssClass="form-control" Height="300" SelectionMode="Single"></asp:ListBox>
                                                </div>

                                                <div id="content">
                                                    <center>
                                                        <br />
                                                        <br />
                                                        <br />
                                                        <asp:Button ID="btnAdd" runat="server" Text="Add" Width="82" OnClick="btnAdd_Click" />
                                                        <br />
                                                        <br />
                                                        <%--<asp:Button ID="btnAddAll" runat="server" Text="Add All" Width="82" OnClick="btnAddAll_Click" /><br />
                                                <br />--%>
                                                        <asp:Button ID="btnRemove" runat="server" Text="Remove" Width="82" OnClick="btnRemove_Click" />
                                                        <br />
                                                        <br />
                                                        <%--<asp:Button ID="btnRemoveAll" runat="server" Text="Remove All" Width="82" OnClick="btnRemoveAll_Click" /><br />
                                                <br />--%>
                                                        <br />
                                                        <br />
                                                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="82" OnClick="btnSave_Click" ValidationGroup="Save" />
                                                    </center>
                                                </div>

                                                <div id="rightThing">
                                                    <asp:Label runat="server" ClientIDMode="Static" ID="lblMappedBiker" ></asp:Label>
                                                    <asp:ListBox ID="lstBoxAssign" runat="server" CssClass="form-control" Height="300" SelectionMode="Single"></asp:ListBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-1">&nbsp;</div>

                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMobexMapping" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />

</asp:Content>
