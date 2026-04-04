<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="InquiryList.aspx.cs" Inherits="ShERPa360net.CRM.InquiryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inquiry List</title>
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <form class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong>Inquiry</strong> List</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">From</label>
                                            <div class="col-md-9 input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy">
                                                <asp:TextBox ID="txtFromDate" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <span class="glyphicon glyphicon-th"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">To</label>
                                            <div class="col-md-9 input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy">
                                                <asp:TextBox ID="txtToDate" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                <div class="input-group-addon">
                                                    <span class="glyphicon glyphicon-th"></span>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Segment</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlSegment" AutoPostBack="false" AppendDataBoundItems="true">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Inquriy No</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtInquiryNo" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Inquiry No"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-12" style="margin-top: 5px!important;">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Customer</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtCustomer" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Customer"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Contact No</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtContactNo" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Contact No"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Card No</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtCardNo" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Card No"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Status</label>
                                            <div class="col-md-9">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12 margin-top-10">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Pending</label>
                                            <div class="col-md-9">
                                                <asp:CheckBox ID="chkPending" runat="server" ClientIDMode="Static" Checked="true"></asp:CheckBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-9">
                                        <asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="Search" Style="margin-left: 200px!important;" ID="btnSearch" OnClick="btnSearch_Click" />
                                        <asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-success" Text="New" Style="margin-left: 10px!important;" ID="btnNew" OnClick="btnNew_Click" />
                                        <asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-danger" Text="Fetch" Style="margin-left: 10px!important;" ID="btnFetch" Visible="false" />
                                    </div>
                                </div>

                                <div class="col-md-12 margin-top-10 divhorizontal" style="overflow-y: scroll!important; overflow-x: scroll;">
                                    <asp:GridView ID="gvList" runat="server" CssClass="display nowrap"
                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                List is empty !
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkEdit" CssClass="btn-xs btn-primary" Style="padding: 3px 10px;" OnClick="lnkEdit_Click" Text="Edit"></asp:LinkButton>
                                                    <%--                                                <asp:Button runat="server" ID="lnkEdit" Text="Edit" CssClass="btn btn-success" Style="padding: 3px 10px;" OnClick="btnCreateJobsheet_Click"></asp:Button>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SEGMENTDESC" HeaderText="Segment" />
                                            <asp:BoundField DataField="ORDER_ID" HeaderText="Inquiry No." />
                                            <asp:BoundField DataField="LEADS" HeaderText="Lead" />
                                            <asp:BoundField DataField="STATUSDESC" HeaderText="Status" />
                                            <asp:BoundField DataField="INQUIRYDATE" HeaderText="Inquiry Date" />
                                            <asp:BoundField DataField="REF" HeaderText="Reference" />
                                            <asp:TemplateField HeaderText="Assign Pickup" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Button runat="server" ID="lnkView" Text="Click to Assign Pickup" CssClass="btn btn-warning" Style="padding: 3px 10px;" OnClick="btnAssign_Click"></asp:Button>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Create Jobsheet" Visible="false" style="display:none!important;">
                                            <ItemTemplate>
                                                <asp:Button runat="server" ID="lnkCreateJobsheet" Text="Click to Create Jobsheet" CssClass="btn btn-success" Style="padding: 3px 10px;" OnClick="btnCreateJobsheet_Click"></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            <asp:BoundField DataField="FULLNAME" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="MOB_NO" HeaderText="Mobile No." />
                                            <asp:BoundField DataField="LANDLINE" HeaderText="Mobile No. 2" />
                                            <asp:BoundField DataField="COUPANNO" HeaderText="Card No." />
                                            <asp:BoundField DataField="PACK" HeaderText="Pack Name" />
                                            <asp:BoundField DataField="PICKUPDATE" HeaderText="Pickup Date" />
                                            <asp:BoundField DataField="PICKUPTIME" HeaderText="Pickup Time" DataFormatString="{0:hh\:mm}" />
                                            <asp:BoundField DataField="ADDRESS1" HeaderText="Address 1" />
                                            <asp:BoundField DataField="ADDRESS2" HeaderText="Address 2" />
                                            <asp:BoundField DataField="LANDMARK" HeaderText="Landmark" />
                                            <asp:BoundField DataField="STATE" HeaderText="State" />
                                            <asp:BoundField DataField="CITY_NAME" HeaderText="City" />
                                            <asp:BoundField DataField="POSTCODE" HeaderText="Postal Code" />
                                            <asp:BoundField DataField="AREA" HeaderText="Area" />
                                            <asp:BoundField DataField="FEDEXCAP" HeaderText="Fedex Capability" />
                                            <asp:BoundField DataField="PAYMODE" HeaderText="Payment Mode" />
                                            <asp:BoundField DataField="BRAND_NAME" HeaderText="Brand" />
                                            <asp:BoundField DataField="MODEL_NAME" HeaderText="Model" />
                                            <asp:BoundField DataField="IMEINO" HeaderText="IMEI No" />
                                            <asp:BoundField DataField="WEBINQTXNID" HeaderText="Transaction Id" />
                                            <asp:BoundField DataField="MOBEXINQREFNO" HeaderText="Online Ref No." />
                                            <asp:BoundField DataField="ENTEREDBY" HeaderText="Create By" />
                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" DataFormatString="{0:dd-MM-yyyy hh:mm tt}" />
                                            <asp:BoundField DataField="UPDATEUSER" HeaderText="Update By" />
                                            <asp:BoundField DataField="UPDATEDATE" HeaderText="Update Date" DataFormatString="{0:dd-MM-yyyy hh:mm tt}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranInqEntry" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
