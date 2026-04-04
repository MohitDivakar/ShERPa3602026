<%@ Page Title="CRM Notification" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmOldCRMNotification.aspx.cs" Inherits="ShERPa360net.CRM.frmOldCRMNotification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CRM Notification</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Old Notification</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%--<asp:Button ID="btnWhatsapp" runat="server" Text="Send MSG" OnClick="btnWhatsapp_Click" />--%>
                                                        <asp:LinkButton ID="btnWhatsapp" runat="server" Text="Send MSG" OnClick="btnWhatsapp_Click"><i class="fa fa-send"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                                <asp:BoundField DataField="MESSAGE" HeaderText="Message" Visible="false" />
                                                <asp:BoundField DataField="ITEMID" HeaderText="Item Id" Visible="false" />
                                                <%--<asp:BoundField DataField="CUSTNAME" HeaderText="Cust. Name" />--%>

                                                <asp:TemplateField HeaderText="Cust. Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCustName" runat="server" Text='<%# Eval("CUSTNAME") %>' OnClick="lnkCustName_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="CONTACTNO" HeaderText="Contact No." />--%>

                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblContactno" runat="server" Text='<%# Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%# Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Spec. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECNAME" runat="server" Text='<%# Eval("SPECNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Spec. Value">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECVALUE" runat="server" Text='<%# Eval("SPECVALUE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Lead ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLEADID" runat="server" Text='<%# Eval("LEADID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="CUSTREMARKS" HeaderText="Cust. Remarks" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc" />
                                                <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                <asp:BoundField DataField="IMEINO" HeaderText="Serial No." />
                                                <asp:BoundField DataField="JOBSTATDESC" HeaderText="Job Status" />
                                                <asp:BoundField DataField="SONO" HeaderText="SO No." />
                                                <asp:BoundField DataField="SERIALNO" HeaderText="Serial No." Visible="false" />
                                                <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" Visible="false" />
                                                <asp:BoundField DataField="PLANTDESC" HeaderText="Plant" Visible="true" />
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" Visible="false" />
                                                <asp:BoundField DataField="INWARDBY" HeaderText="Inward By" Visible="false" />
                                                <asp:BoundField DataField="INWARDDATE" HeaderText="Inward Date" />
                                                <asp:BoundField DataField="EXTFLD1" HeaderText="Ext Fld 1" Visible="false" />
                                                <asp:BoundField DataField="EXTFLD2" HeaderText="Ext Fld 2" Visible="false" />

                                                <asp:BoundField DataField="INWARDBYNAME" HeaderText="Inward By" />
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranCRMNotification" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" />

</asp:Content>
