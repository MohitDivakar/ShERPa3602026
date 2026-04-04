<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="CallLog.aspx.cs" Inherits="ShERPa360net.CRM.CallLog" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Call Logs</title>
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        .blockheader2 {
            width: 200px !important;
        }
    </style>

    <%--<link href="../Slider/assets/style.css" rel="stylesheet" />
    <script src="../Slider/assets/script.js"></script>
    <script src="https://unpkg.com/ionicons@5.0.0/dist/ionicons.js"></script>--%>

    <%--<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Call  </strong>Data</h3>
                    </div>
                    <div class="panel-body">
                        <asp:Label runat="server" ClientIDMode="Static" Style="margin-left: 11px!important; font-weight: bold; font-size: 14px;" Visible="false" ID="lblRecoretxt">Record:</asp:Label>
                        <asp:Label runat="server" ClientIDMode="Static" Style="font-weight: bold; font-size: 12px;" ID="lblRecord" Visible="false">0</asp:Label>


                        <%--<asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-success pull-right" Visible="true">Import</asp:LinkButton>--%>
                        <div class="panel-body">

                            <div class="row">
                                <div class="col-md-12 pull-right">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Product :</label>
                                            <div class="col-md-8 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <%--<span class="input-group-addon"><span class="fa fa-calendar"></span></span>--%>
                                                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Contact No. :</label>
                                            <div class="col-md-7 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <%--<span class="input-group-addon"><span class="fa fa-calendar"></span></span>--%>
                                                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Ref. :</label>
                                            <div class="col-md-9 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:DropDownList ID="ddlReference" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-10 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label pull-left">To</label>
                                            <div class="col-md-10 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearch_Click">
<span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>



                        <%--<div class="row" style="margin-top: 20px !important;">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="grvLead" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="true" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>
                        </div>--%>





                        <section class="content">
                            <div class="row">
                                <asp:HiddenField ID="hfCallId" runat="server" />
                                <div class="col-md-12">
                                    <div class="nav-tabs-custom">

                                        <ul class="nav nav-pills">
                                            <li class="active"><a data-toggle="pill" href="#tab_1">New Calls</a></li>
                                            <li><a data-toggle="pill" href="#tab_2">Follow Up</a></li>
                                            <li><a data-toggle="pill" href="#tab_3">Cancelled Calls</a></li>
                                            <li><a data-toggle="pill" href="#tab_4">Converted Calls</a></li>
                                            <%--<li><a data-toggle="pill" href="#menu2">Menu 2</a></li>--%>
                                            <%--<li><a data-toggle="pill" href="#menu3">Menu 3</a></li>--%>
                                        </ul>
                                        <br />
                                        <div class="tab-content">
                                            <div class="tab-pane active" id="tab_1">

                                                <asp:DataList ID="dlListSaved" runat="server" RepeatDirection="Vertical" Width="100%" OnItemCommand="dlListSaved_ItemCommand">
                                                    <%-- OnItemCommand="dlListSaved_ItemCommand" OnItemDataBound="dlListSaved_ItemDataBound">--%>
                                                    <ItemTemplate>
                                                        <div class="timeline-item" style="border-bottom: 1px solid #f4f4f4">
                                                            <div class="box-body pad table-responsive" style="margin: 0px">
                                                                <table class="table text-center col-sm-12 no-border">
                                                                    <tr style="border-bottom-style: hidden;">
                                                                        <td style="empty-cells: hide" colspan="6">
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("MAKE") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("MODEL") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <%--</td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("RAM") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <%--</td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("ROM") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("COLOR") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("PRICERANGE") %></p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <%--<td style="vertical-align: middle">
                                                                    <p class="pull-left badge bg-green"><%# Eval("ID") %></p>
                                                                </td>--%>
                                                                        <%--<td style="vertical-align: middle">

                                                                            <p class="pull-left badge bg-green"><%# Eval("MAKE") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("MODEL") %></p>
                                                                            <br />
                                                                            <br />
                                                                            <p class="pull-left badge bg-green"><%# Eval("RAM") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("ROM") %></p>
                                                                            <br />
                                                                            <br />
                                                                            <p class="pull-left badge bg-green"><%# Eval("COLOR") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("PRICERANGE") %></p>
                                                                            <br />
                                                                            <br />
                                                                        </td>--%>

                                                                        <td class="col-md-2" style="vertical-align: middle;">
                                                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>' />
                                                                            <p align="left" style="display: none;"><%# Eval("ID") %></p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                                <%--<asp:TextBox ID="lblMobileNo" runat="server" Text='<%# Eval("CUSTNAME") %>' Enabled="false"></asp:TextBox>--%>
                                                                                <asp:LinkButton ID="lnkEditName" runat="server" Text="Edit" CommandName="EditName"><i class="fa fa-pencil-square-o" style="margin-left:5px !important;"></i></asp:LinkButton>
                                                                                <%--<asp:LinkButton runat="server" Style="margin-left: 2px!important;" ID="lnkSearhMRToPB" CssClass="btn btn-success pull-left" Text="Search MR To PB" OnClick="lnkSearhMRToPB_Click"><i class="fa fa-search"></i></asp:LinkButton>--%>
                                                                            </p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("CONTACTNO") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                <asp:LinkButton ID="lnkNotification" runat="server" Text="View Notification" CommandName="ViewNotification"></asp:LinkButton>
                                                                            </p>
                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle;">
                                                                            <p align="left">
                                                                                Product :
                                                                    <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("PRODUCT") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblATTRIBUTE" runat="server" Text='<%# Eval("ATTRIBUTE") %>'> </asp:Label>
                                                                                :
                                                                    <asp:Label ID="lblATTRVALUE" runat="server" Text='<%# Eval("ATTRVALUE") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                Cust. Remark :
                                                                    <asp:Label ID="lblCustRemarks" runat="server" Text='<%# Eval("CUSTREMARKS") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                Ref. : 
                                                                        <%# Eval("REFF") %>
                                                                            </p>

                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle;">

                                                                            <p align="left">
                                                                                Inq. Type :
                                                                                <%--<%# Eval("INQUIRYTYPE") %>--%>
                                                                                <asp:Label ID="lblInquiryType" runat="server" Text='<%# Eval("INQUIRYTYPE") %>'> </asp:Label>

                                                                                <asp:HiddenField ID="hfInqType" runat="server" Value='<%# Eval("INQTYPE") %>' />
                                                                            </p>
                                                                            <p align="left">
                                                                                Lead Type :
                                                                                <asp:Label ID="lblLeadType" runat="server" Text='<%# Eval("LETYPE") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                State :
                                                                                <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATENAME") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                City :
                                                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITYNAME") %>'> </asp:Label>
                                                                            </p>
                                                                            <%--<p align="center" style="margin: 0px;">
                                                                    <%# Eval("REF") %>
                                                                </p>--%>
                                                                        </td>

                                                                        <td class="col-md-3" style="vertical-align: middle; text-align: left;">
                                                                            <p align="left" style="padding-left: 5px;"><i class="fa fa-calendar-o"></i>&nbsp; Create Date : <%# Eval("CUSTUPDATEDATE")%></p>
                                                                            <p align="left" style="padding-left: 5px;"><i class="fa fa-calendar-o"></i>&nbsp; Start Date : <%# Eval("CALLSTART")%></p>
                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>

                                                                                        <p align="center">
                                                                                            <%--<asp:HyperLink ID="hlInquiry" runat="server" Enabled="false" Text="Inquiry" Width="80" Font-Size="10" BackColor="#00acd6" BorderColor="#00c0ef" Height="25" Font-Bold="true" Target="_blank" CssClass="btn btn-info btn-xs" NavigateUrl='<%# string.Format("Inquiry.aspx?CN={0}&CID={1}&CMK={2}&CMD={3}&SEG={4}",
                                            HttpUtility.UrlEncode(Eval("CONTACTNO").ToString()), HttpUtility.UrlEncode(Eval("ID").ToString()),HttpUtility.UrlEncode(Eval("MAKE").ToString()),HttpUtility.UrlEncode(Eval("MODEL").ToString()),HttpUtility.UrlEncode("1003")) %>'></asp:HyperLink>--%>
                                                                                            <asp:HyperLink ID="hlInquiry" runat="server" Enabled="true" Text="Inquiry" Width="80" Font-Size="10" BackColor="#00acd6" BorderColor="#00c0ef" Height="25" Font-Bold="true" Target="_blank" CssClass="btn btn-warning btn-xs" NavigateUrl='<%# string.Format("Inquiry.aspx?CN={0}&CID={1}&CMK={2}&CMD={3}&SEG={4}",
                                            HttpUtility.UrlEncode(Eval("CONTACTNO").ToString()), HttpUtility.UrlEncode(Eval("ID").ToString()),HttpUtility.UrlEncode(Eval("MAKE").ToString()),HttpUtility.UrlEncode(Eval("MODEL").ToString()),HttpUtility.UrlEncode("1003")) %>'></asp:HyperLink>
                                                                                        </p>

                                                                                        <p align="center" style="margin: unset;">
                                                                                            <%--<asp:HyperLink ID="hlSO" runat="server" Enabled="true" Text="SO" Target="_blank" Width="80" Font-Size="10" BackColor="#D58512" BorderColor="#985f0d" Font-Bold="true" Height="25" CssClass="btn btn-info btn-xs" NavigateUrl='<%# string.Format("../SD/frmSO.aspx?CN={0}&CID={1}&CMK={2}&CMD={3}",
                                            HttpUtility.UrlEncode(Eval("CONTACTNO").ToString()), HttpUtility.UrlEncode(Eval("ID").ToString()),HttpUtility.UrlEncode(Eval("MAKE").ToString()),HttpUtility.UrlEncode(Eval("MODEL").ToString())) %>'></asp:HyperLink>--%>
                                                                                            <asp:Button ID="hlEnterSO" runat="server" CommandName="EnterSO" Text="SO" Enabled="true" Width="80" Font-Size="10" BackColor="#D58512" BorderColor="#985f0d" Font-Bold="true" Height="25" CssClass="btn btn-info btn-xs"></asp:Button>
                                                                                        </p>

                                                                                    </td>
                                                                                    <td style="padding-left: 10px">
                                                                                        <p align="center">
                                                                                            <asp:Button ID="hlHold" runat="server" CommandName="Hold" Text="Hold" Enabled="true" Width="80" Font-Size="10" BackColor="#2e6da4" BorderColor="#337ab7" Font-Bold="true" Height="25" CssClass="btn btn-primary btn-xs"></asp:Button>
                                                                                        </p>
                                                                                        <p align="center" style="margin: unset;">
                                                                                            <asp:Button ID="hlCancel" runat="server" CommandName="Cancel" Text="Cancel" Enabled="true" Width="80" Font-Size="10" BackColor="#d9534f" BorderColor="#d43f3a" Font-Bold="true" Height="25" CssClass="btn btn-danger btn-xs"></asp:Button>
                                                                                        </p>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>

                                                                        </td>


                                                                        <td class="col-md-1" style="vertical-align: middle;">
                                                                            <asp:LinkButton ID="btnCallAttent" runat="server" CommandName="CallAttend" CssClass="btn btn-success btn-app bg-green margin" Font-Bold="true" Style="border-radius: 5px" Text="<i id='spancall' runat='server' class='fa fa-phone' style='color: #FFF'><asp:Label ID='lblCallText' CssClass='form-control' runat='server' Text='Call'></asp:Label></i><br>Attempt Call" />
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                <div style="text-align: center; color: red; font-size: 18px;">
                                                    <asp:Label Visible='<%#bool.Parse((dlListSaved.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="List is empty !"></asp:Label>
                                                </div>
                                            </FooterTemplate>--%>
                                                </asp:DataList>

                                            </div>

                                            <div class="tab-pane" id="tab_2">

                                                <asp:DataList ID="dlHList" runat="server" RepeatDirection="Vertical" Width="100%" OnItemCommand="dlListSaved_ItemCommand">
                                                    <%-- OnItemCommand="dlListSaved_ItemCommand" OnItemDataBound="dlListSaved_ItemDataBound">--%>
                                                    <ItemTemplate>
                                                        <div class="timeline-item" style="border-bottom: 1px solid #f4f4f4">
                                                            <div class="box-body pad table-responsive" style="margin: 0px">
                                                                <table class="table text-center col-sm-12 no-border">
                                                                    <tr style="border-bottom-style: hidden;">
                                                                        <td style="empty-cells: hide" colspan="6">
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("MAKE") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("MODEL") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <%--</td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("RAM") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <%--</td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("ROM") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("COLOR") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("PRICERANGE") %></p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <%--<td style="vertical-align: middle">
                                                                    <p class="pull-left badge bg-green"><%# Eval("ID") %></p>
                                                                </td>--%>
                                                                        <%--<td style="vertical-align: middle">

                                                                            <p class="pull-left badge bg-green"><%# Eval("MAKE") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("MODEL") %></p>
                                                                            <br />
                                                                            <br />
                                                                            <p class="pull-left badge bg-green"><%# Eval("RAM") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("ROM") %></p>
                                                                            <br />
                                                                            <br />
                                                                            <p class="pull-left badge bg-green"><%# Eval("COLOR") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("PRICERANGE") %></p>
                                                                            <br />
                                                                            <br />
                                                                        </td>--%>

                                                                        <td class="col-md-1" style="vertical-align: middle;">
                                                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>' />
                                                                            <p align="left" style="display: none;"><%# Eval("ID") %></p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("CONTACTNO") %>'> </asp:Label>
                                                                            </p>
                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle;">
                                                                            <p align="left">
                                                                                Product :
                                                                    <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("PRODUCT") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblATTRIBUTE" runat="server" Text='<%# Eval("ATTRIBUTE") %>'> </asp:Label>
                                                                                :
                                                                    <asp:Label ID="lblATTRVALUE" runat="server" Text='<%# Eval("ATTRVALUE") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                Cust. Remark :
                                                                    <asp:Label ID="lblCustRemarks" runat="server" Text='<%# Eval("CUSTREMARKS") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                Ref. : 
                                                                        <%# Eval("REFF") %>
                                                                            </p>

                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle;">
                                                                            <p align="left">
                                                                                Inq. Type :
                                                                                <%--<%# Eval("INQUIRYTYPE") %>--%>
                                                                                <asp:Label ID="lblInquiryType" runat="server" Text='<%# Eval("INQUIRYTYPE") %>'> </asp:Label>
                                                                                <asp:HiddenField ID="hfInqType" runat="server" Value='<%# Eval("INQTYPE") %>' />
                                                                            </p>
                                                                            <p align="left">
                                                                                Lead Type :
                                                                                <asp:Label ID="lblLeadType" runat="server" Text='<%# Eval("LETYPE") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                State :
                                                                                <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATENAME") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                City :
                                                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITYNAME") %>'> </asp:Label>
                                                                            </p>
                                                                            <%--<p align="center" style="margin: 0px;">
                                                                    <%# Eval("REF") %>
                                                                </p>--%>
                                                                        </td>

                                                                        <td class="col-md-3" style="vertical-align: middle; text-align: left;">
                                                                            <p style="padding-left: 0px;"><i class="fa fa-calendar"></i>&nbsp; Create Date : <%# Eval("CUSTUPDATEDATE","{0:dd-MM-yyyy}")%></p>
                                                                            <p style="padding-left: 0px;"><i class="fa fa-calendar-o"></i>&nbsp; Call On : <%# Eval("POSTPONEDDATE","{0:dd-MM-yyyy}")%></p>
                                                                            <p style="padding-left: 0px;"><i class="fa fa-clock-o"></i>&nbsp; Time : <%# Eval("POSTPONEDTIME") %> </p>
                                                                            <p style="padding-left: 0px;"><i class="fa fa-calendar-o"></i>&nbsp; Start Date : <%# Eval("CALLSTART")%></p>
                                                                            <p style="padding-left: 0px;"><i class="fa fa-calendar-o"></i>&nbsp; End Date : <%# Eval("CALLEND")%></p>
                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>

                                                                                        <p align="center" style="margin: unset;">

                                                                                            <asp:HyperLink ID="hlInquiry" runat="server" Enabled="true" Text="Inquiry" Target="_blank" Width="80" Font-Size="10" BackColor="#00acd6" BorderColor="#00c0ef" Height="25" Font-Bold="true" CssClass="btn btn-warning btn-xs" NavigateUrl='<%# string.Format("Inquiry.aspx?CN={0}&CID={1}&CMK={2}&CMD={3}&SEG={4}",
                                            HttpUtility.UrlEncode(Eval("CONTACTNO").ToString()), HttpUtility.UrlEncode(Eval("ID").ToString()),HttpUtility.UrlEncode(Eval("MAKE").ToString()),HttpUtility.UrlEncode(Eval("MODEL").ToString()),HttpUtility.UrlEncode("1003")) %>'></asp:HyperLink>
                                                                                        </p>

                                                                                        <p align="center">
                                                                                            <br />
                                                                                            <%--<asp:HyperLink ID="hlSO" runat="server" Enabled="true" Text="SO" Target="_blank" Width="80" Font-Size="10" BackColor="#D58512" BorderColor="#985f0d" Font-Bold="true" Height="25" CssClass="btn btn-info btn-xs" NavigateUrl='<%# string.Format("../SD/frmSO.aspx?CN={0}&CID={1}&CMK={2}&CMD={3}",
                                            HttpUtility.UrlEncode(Eval("CONTACTNO").ToString()), HttpUtility.UrlEncode(Eval("ID").ToString()),HttpUtility.UrlEncode(Eval("MAKE").ToString()),HttpUtility.UrlEncode(Eval("MODEL").ToString())) %>'></asp:HyperLink>--%>
                                                                                            <asp:Button ID="hlEnterSO" runat="server" CommandName="EnterSO" Text="SO" Enabled="true" Width="80" Font-Size="10" BackColor="#D58512" BorderColor="#985f0d" Font-Bold="true" Height="25" CssClass="btn btn-info btn-xs"></asp:Button>
                                                                                        </p>

                                                                                    </td>
                                                                                    <td style="padding-left: 10px">
                                                                                        <p align="center">
                                                                                            <asp:Button ID="hlHold" runat="server" CommandName="Hold" Text="Hold" Enabled="true" Width="80" Font-Size="10" BackColor="#2e6da4" BorderColor="#337ab7" Font-Bold="true" Height="25" CssClass="btn btn-primary btn-xs"></asp:Button>
                                                                                        </p>
                                                                                        <p align="center" style="margin: unset;">
                                                                                            <asp:Button ID="hlCancel" runat="server" CommandName="Cancel" Text="Cancel" Enabled="true" Width="80" Font-Size="10" BackColor="#d9534f" BorderColor="#d43f3a" Font-Bold="true" Height="25" CssClass="btn btn-danger btn-xs"></asp:Button>
                                                                                        </p>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>

                                                                        </td>

                                                                        <td class="col-md-1" style="vertical-align: middle;">
                                                                            <asp:LinkButton ID="btnCallAttent" runat="server" CommandName="CallAttend" CssClass="btn btn-success btn-app bg-green margin" Style="border-radius: 5px" Font-Bold="true" Text="<i id='spancall' runat='server' class='fa fa-phone' style='color: #FFF'><asp:Label ID='lblCallText' CssClass='form-control' runat='server' Text='Call'></asp:Label></i>  <br>Attempt Call " />
                                                                        </td>

                                                                        <td class="col-md-1" style="vertical-align: middle;">
                                                                            <asp:LinkButton ID="btnDetails" runat="server" CommandName="CallDetails" CssClass="btn btn-success bg-green margin" Style="border-radius: 5px; background-color: green;" Font-Bold="true" Text="<i id='spaninfo' class='fa fa-info-circle' style='color: #FFF'></i>   <asp:Label ID='lblCallText' runat='server' Text='Details'></asp:Label> <br>History" />
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                <div style="text-align: center; color: red; font-size: 18px;">
                                                    <asp:Label Visible='<%#bool.Parse((dlListSaved.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="List is empty !"></asp:Label>
                                                </div>
                                            </FooterTemplate>--%>
                                                </asp:DataList>

                                            </div>






                                            <div class="tab-pane" id="tab_3">

                                                <asp:DataList ID="dlCList" runat="server" RepeatDirection="Vertical" Width="100%" OnItemCommand="dlListSaved_ItemCommand">
                                                    <%-- OnItemCommand="dlListSaved_ItemCommand" OnItemDataBound="dlListSaved_ItemDataBound">--%>
                                                    <ItemTemplate>
                                                        <div class="timeline-item" style="border-bottom: 1px solid #f4f4f4">
                                                            <div class="box-body pad table-responsive" style="margin: 0px">
                                                                <table class="table text-center col-sm-12 no-border">
                                                                    <tr style="border-bottom-style: hidden;">
                                                                        <td style="empty-cells: hide" colspan="6">
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("MAKE") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("MODEL") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <%--</td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("RAM") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <%--</td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("ROM") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("COLOR") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("PRICERANGE") %></p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <%--<td style="vertical-align: middle">
                                                                    <p class="pull-left badge bg-green"><%# Eval("ID") %></p>
                                                                </td>--%>
                                                                        <%--<td style="vertical-align: middle">

                                                                            <p class="pull-left badge bg-green"><%# Eval("MAKE") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("MODEL") %></p>
                                                                            <br />
                                                                            <br />
                                                                            <p class="pull-left badge bg-green"><%# Eval("RAM") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("ROM") %></p>
                                                                            <br />
                                                                            <br />
                                                                            <p class="pull-left badge bg-green"><%# Eval("COLOR") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("PRICERANGE") %></p>
                                                                            <br />
                                                                            <br />
                                                                        </td>--%>

                                                                        <td class="col-md-2" style="vertical-align: middle;">
                                                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>' />
                                                                            <p align="left" style="display: none;"><%# Eval("ID") %></p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("CONTACTNO") %>'> </asp:Label>
                                                                            </p>
                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle;">
                                                                            <p align="left">
                                                                                Product :
                                                                    <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("PRODUCT") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblATTRIBUTE" runat="server" Text='<%# Eval("ATTRIBUTE") %>'> </asp:Label>
                                                                                :
                                                                    <asp:Label ID="lblATTRVALUE" runat="server" Text='<%# Eval("ATTRVALUE") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                Cust. Remark :
                                                                    <asp:Label ID="lblCustRemarks" runat="server" Text='<%# Eval("CUSTREMARKS") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                Ref. : 
                                                                        <%# Eval("REFF") %>
                                                                            </p>

                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle;">
                                                                            <p align="left">
                                                                                Inq. Type :
                                                                                <%--<%# Eval("INQUIRYTYPE") %>--%>
                                                                                <asp:Label ID="lblInquiryType" runat="server" Text='<%# Eval("INQUIRYTYPE") %>'> </asp:Label>
                                                                                <asp:HiddenField ID="hfInqType" runat="server" Value='<%# Eval("INQTYPE") %>' />
                                                                            </p>
                                                                            <p align="left">
                                                                                Lead Type :
                                                                                <asp:Label ID="lblLeadType" runat="server" Text='<%# Eval("LETYPE") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                State :
                                                                                <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATENAME") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                City :
                                                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITYNAME") %>'> </asp:Label>
                                                                            </p>
                                                                            <%--<p align="center" style="margin: 0px;">
                                                                    <%# Eval("REF") %>
                                                                </p>--%>
                                                                        </td>

                                                                        <td class="col-md-3" style="vertical-align: middle; text-align: left;">
                                                                            <p style="padding-left: 15px;"><i class="fa fa-calendar-o"></i>&nbsp; Create Date : <%# Eval("CUSTUPDATEDATE","{0:dd-MM-yyyy}")%></p>
                                                                            <p style="padding-left: 15px;"><i class="fa fa-calendar-o"></i>&nbsp; Start Date : <%# Eval("CALLSTART")%></p>
                                                                            <p style="padding-left: 15px;"><i class="fa fa-calendar-o"></i>&nbsp; End Date : <%# Eval("CALLEND")%></p>
                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle; text-align: left;">
                                                                            <p style="padding-left: 15px;"><i class="fa fa-calendar-o"></i>&nbsp; CancelDate : <%# Eval("UPDATEDATE","{0:dd-MM-yyyy}")%></p>
                                                                            <p style="padding-left: 15px; margin: unset;"><i class="fa  fa-clock-o"></i>&nbsp; CancelTime : <%# Eval("UPDATEDATE","{0:hh:mm:ss tt}") %> </p>
                                                                        </td>

                                                                        <td class="col-md-1" style="vertical-align: middle;">
                                                                            <asp:LinkButton ID="btnDetails" runat="server" CommandName="CallDetails" CssClass="btn btn-success bg-green margin" Style="border-radius: 5px; background-color: green;" Font-Bold="true" Text="<i id='spaninfo' class='fa fa-info-circle' style='color: #FFF'></i>   <asp:Label ID='lblCallText' runat='server' Text='Details'></asp:Label><br>History" />
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                <div style="text-align: center; color: red; font-size: 18px;">
                                                    <asp:Label Visible='<%#bool.Parse((dlListSaved.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="List is empty !"></asp:Label>
                                                </div>
                                            </FooterTemplate>--%>
                                                </asp:DataList>

                                            </div>



                                            <div class="tab-pane" id="tab_4">

                                                <asp:DataList ID="ConvertedList" runat="server" RepeatDirection="Vertical" Width="100%" OnItemCommand="dlListSaved_ItemCommand">
                                                    <%-- OnItemCommand="dlListSaved_ItemCommand" OnItemDataBound="dlListSaved_ItemDataBound">--%>
                                                    <ItemTemplate>
                                                        <div class="timeline-item" style="border-bottom: 1px solid #f4f4f4">
                                                            <div class="box-body pad table-responsive" style="margin: 0px">
                                                                <table class="table text-center col-sm-12 no-border">
                                                                    <tr style="border-bottom-style: hidden;">
                                                                        <td style="empty-cells: hide" colspan="6">
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("MAKE") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("MODEL") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <%--</td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("RAM") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <%--</td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("ROM") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("COLOR") %></p>
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                       <%-- </td>
                                                                        <td style="empty-cells: hide">--%>
                                                                            <p class="pull-left badge bg-green" style="background: none; color: black; font-weight: bold; border-style: groove;"><%# Eval("PRICERANGE") %></p>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <%--<td style="vertical-align: middle">
                                                                    <p class="pull-left badge bg-green"><%# Eval("ID") %></p>
                                                                </td>--%>
                                                                        <%--<td style="vertical-align: middle">

                                                                            <p class="pull-left badge bg-green"><%# Eval("MAKE") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("MODEL") %></p>
                                                                            <br />
                                                                            <br />
                                                                            <p class="pull-left badge bg-green"><%# Eval("RAM") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("ROM") %></p>
                                                                            <br />
                                                                            <br />
                                                                            <p class="pull-left badge bg-green"><%# Eval("COLOR") %></p>
                                                                            <p class="pull-left badge bg-green" style="margin-left: 10px !important;"><%# Eval("PRICERANGE") %></p>
                                                                            <br />
                                                                            <br />
                                                                        </td>--%>

                                                                        <td class="col-md-2" style="vertical-align: middle;">
                                                                            <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>' />
                                                                            <p align="left" style="display: none;"><%# Eval("ID") %></p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("CONTACTNO") %>'> </asp:Label>
                                                                            </p>
                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle;">
                                                                            <p align="left">
                                                                                Product :
                                                                    <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("PRODUCT") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                <asp:Label ID="lblATTRIBUTE" runat="server" Text='<%# Eval("ATTRIBUTE") %>'> </asp:Label>
                                                                                :
                                                                    <asp:Label ID="lblATTRVALUE" runat="server" Text='<%# Eval("ATTRVALUE") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                Cust. Remark :
                                                                    <asp:Label ID="lblCustRemarks" runat="server" Text='<%# Eval("CUSTREMARKS") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                Ref. : 
                                                                        <%# Eval("REFF") %>
                                                                            </p>

                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle;">
                                                                            <p align="left">
                                                                                Inq. Type :
                                                                                <%--<%# Eval("INQUIRYTYPE") %>--%>
                                                                                <asp:Label ID="lblInquiryType" runat="server" Text='<%# Eval("INQUIRYTYPE") %>'> </asp:Label>
                                                                                <asp:HiddenField ID="hfInqType" runat="server" Value='<%# Eval("INQTYPE") %>' />
                                                                            </p>
                                                                            <p align="left">
                                                                                Lead Type :
                                                                                <asp:Label ID="lblLeadType" runat="server" Text='<%# Eval("LETYPE") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                State :
                                                                                <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATENAME") %>'> </asp:Label>
                                                                            </p>
                                                                            <p align="left">
                                                                                City :
                                                                                <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITYNAME") %>'> </asp:Label>
                                                                            </p>
                                                                            <%--<p align="center" style="margin: 0px;">
                                                                    <%# Eval("REF") %>
                                                                </p>--%>
                                                                        </td>

                                                                        <td class="col-md-3" style="vertical-align: middle; text-align: left;">
                                                                            <p style="padding-left: 15px;"><i class="fa fa-calendar-o"></i>&nbsp; Create Date : <%# Eval("CUSTUPDATEDATE","{0:dd-MM-yyyy}")%></p>
                                                                            <p style="padding-left: 15px;"><i class="fa fa-calendar-o"></i>&nbsp; ConvertDate : <%# Eval("UPDATEDATE","{0:dd-MM-yyyy}")%></p>
                                                                            <p style="padding-left: 15px;"><i class="fa  fa-clock-o"></i>&nbsp; ConvertTime : <%# Eval("UPDATEDATE","{0:hh:mm:ss tt}") %> </p>
                                                                            <p style="padding-left: 15px;"><i class="fa fa-calendar-o"></i>&nbsp; Start Date : <%# Eval("CALLSTART")%></p>
                                                                            <p style="padding-left: 15px;"><i class="fa fa-calendar-o"></i>&nbsp; End Date : <%# Eval("CALLEND")%></p>
                                                                        </td>

                                                                        <td class="col-md-2" style="vertical-align: middle; text-align: left;">
                                                                            <p style="padding-left: 15px;"><i class="fa fa-question-circle"></i>&nbsp; INQ No. : <%# Eval("INQNO")%></p>
                                                                            <p style="padding-left: 15px;"><i class="fa fa-money"></i>&nbsp; SI No. : <%# Eval("SONO")%></p>
                                                                        </td>

                                                                        <td class="col-md-1" style="vertical-align: middle;">
                                                                            <asp:LinkButton ID="btnDetails" runat="server" CommandName="CallDetails" CssClass="btn btn-success bg-green margin" Style="border-radius: 5px; background-color: green;" Font-Bold="true" Text="<i id='spaninfo' class='fa fa-info-circle' style='color: #FFF'></i>   <asp:Label ID='lblCallText' runat='server' Text='Details'></asp:Label> <br>History" />
                                                                        </td>

                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                <div style="text-align: center; color: red; font-size: 18px;">
                                                    <asp:Label Visible='<%#bool.Parse((dlListSaved.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="List is empty !"></asp:Label>
                                                </div>
                                            </FooterTemplate>--%>
                                                </asp:DataList>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </section>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="modal-Holdstatusupdate" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Status Update</h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Hold Reason : </label>
                                            <asp:HiddenField ID="hfHoldID" runat="server" />
                                            <asp:HiddenField ID="hfHoldMobileNo" runat="server" />
                                            <asp:HiddenField ID="hfHoldInqType" runat="server" />

                                            <asp:DropDownList ID="ddlHoldReason" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlHoldReason"
                                                ValidationGroup="VgStatus" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
                                                InitialValue="0" ErrorMessage="Please select hold reason">Please select hold reason
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Cust. Remarks : </label>

                                            <asp:TextBox ID="txtCustHoldRemark" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCustHoldRemarks" runat="server" ControlToValidate="txtCustHoldRemark" ForeColor="Red" Display="Dynamic"
                                                ValidationGroup="VgStatus" ErrorMessage="Enter Cust. Hold Remarks">Enter Cust. Hold Remarks</asp:RequiredFieldValidator>


                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Our Remark :</label>
                                            <asp:TextBox ID="txtQTEKHoldRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQTEKHoldRemarks" ForeColor="Red" Display="Dynamic"
                                                ValidationGroup="VgStatus" ErrorMessage="Enter Our Hold Remarks">Enter Our Hold Remarks</asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col-md-4" style="padding-top: 10px;">
                                        <div class="form-group">
                                            <label>Date :</label>
                                            <div class="input-group">
                                                <%--<span class="input-group-addon"><span class="fa fa-calendar"></span></span>--%>
                                                <asp:TextBox ID="txtPostponedDate" runat="server" CssClass="form-control datepicker" TextMode="Date" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPosponedDate" runat="server" ControlToValidate="txtPostponedDate" ForeColor="Red" Display="Dynamic"
                                                    ValidationGroup="VgStatus" ErrorMessage="Select Postponed Date">Select Postponed Date</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <%--<div class="col-md-4" style="padding-top: 10px;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date From : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>


                                    <div class="col-md-4" style="padding-top: 10px;">
                                        <div class="form-group">
                                            <label>Time :</label>
                                            <div class="input-group">
                                                <%--<span class="input-group-addon"><span class="fa fa-clock-o"></span></span>--%>
                                                <asp:TextBox ID="txtPostponedTime" runat="server" CssClass="form-control timepickerpickup" TextMode="Time"></asp:TextBox>

                                                <%--<asp:TextBox ID="txttxt" runat="server" TextMode="Date"></asp:TextBox>--%>
                                                <%--<asp:TextBox ID="TextBox1" runat="server" TextMode="DateTime"></asp:TextBox>--%>
                                                <%--<asp:TextBox ID="TextBox2" runat="server" TextMode="Time" CssClass="form-control"></asp:TextBox>--%>
                                            </div>
                                        </div>
                                    </div>


                                    <%--<div class="col-md-3">
                                        <div class="form-group bootstrap-timepicker">
                                            <label>Requested Pickup Time :*</label>
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="dtpPickupTime" Display="None"
                                                    ValidationGroup="aValidationGroup" ErrorMessage="Pickup time is required">
                                                </asp:RequiredFieldValidator>
                                                <div class="input-group-addon">
                                                    <i class="fa fa-clock-o"></i>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="box-footer text-center">
                                <i class="fa fa-spin fa-refresh fa-lg" id="faLoading1" style="display: none"></i>
                                <p style="margin-bottom: 0px; margin-top: 5px">
                                    <asp:Button ID="btnHoldSubmit" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnHoldSubmit_Click" UseSubmitBehavior="false"
                                        OnClientClick="if (window.Page_ClientValidate('VgStatus') == true) {this.disabled = true; $('#faLoading1').show();}" />
                                    <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" ValidationGroup="VgStatus" />--%>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dlListSaved" />
            <asp:AsyncPostBackTrigger ControlID="dlHList" />
            <asp:AsyncPostBackTrigger ControlID="dlCList" />
            <asp:AsyncPostBackTrigger ControlID="ConvertedList" />
        </Triggers>
    </asp:UpdatePanel>







    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="modal-Cancelstatusupdate" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Status Update</h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Cancel Reason : </label>
                                            <asp:HiddenField ID="hfCancelID" runat="server" />
                                            <asp:HiddenField ID="hfCancelMobileNo" runat="server" />
                                            <asp:HiddenField ID="hfCancelInqType" runat="server" />
                                            <asp:DropDownList ID="ddlCancelReason" runat="server" CssClass="form-control select2">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="rfvCancelReason" runat="server" ControlToValidate="ddlCancelReason"
                                                ValidationGroup="VgCancelStatus" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
                                                InitialValue="0" ErrorMessage="Please select cancel reason">Please select hold reason
                                            </asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Cust. Remarks : </label>

                                            <asp:TextBox ID="txtCustCancelReamarka" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCustCancelReamarka" ForeColor="Red" Display="Dynamic"
                                                ValidationGroup="VgCancelStatus" ErrorMessage="Enter Cust. Cancel Remarks">Enter Cust. Cancel Remarks</asp:RequiredFieldValidator>


                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label>Our Remark :</label>
                                            <asp:TextBox ID="txtQTEKCancelRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvQTEKCancelRemark" runat="server" ControlToValidate="txtQTEKCancelRemarks" ForeColor="Red" Display="Dynamic"
                                                ValidationGroup="VgCancelStatus" ErrorMessage="Enter Our Cancel Remarks">Enter Our Cancel Remarks</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer text-center">
                                <i class="fa fa-spin fa-refresh fa-lg" id="faLoading2" style="display: none"></i>
                                <p style="margin-bottom: 0px; margin-top: 5px">
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnCancel_Click" UseSubmitBehavior="false"
                                        OnClientClick="if (window.Page_ClientValidate('VgCancelStatus') == true) {this.disabled = true; $('#faLoading1').show();}" />
                                    <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" ValidationGroup="VgStatus" />--%>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dlListSaved" />
            <asp:AsyncPostBackTrigger ControlID="dlHList" />
            <asp:AsyncPostBackTrigger ControlID="dlCList" />
            <asp:AsyncPostBackTrigger ControlID="ConvertedList" />
        </Triggers>
    </asp:UpdatePanel>



    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="modal-CallHistory" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Call History</h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Cust. Name</label>
                                                <asp:HiddenField ID="hfHisID" runat="server" />
                                                <asp:Label ID="lblHisCustName" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Contact No. :</label>
                                                <asp:Label ID="lblHisContactNo" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Email :</label>
                                                <asp:Label ID="lblHisMail" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">


                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Make :</label>
                                                <asp:Label ID="lblHisMake" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Model :</label>
                                                <asp:Label ID="lblHisModel" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Curr. Status :</label>
                                                <asp:Label ID="lblHisCurrStatus" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <%--<div class="col-md-6">--%>
                                        <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:BoundField DataField="CALLDATE" HeaderText="Call Date" />
                                                <asp:BoundField DataField="REASON" HeaderText="Hold/Cancel Reason" />
                                                <asp:BoundField DataField="CUSTREMARKS" HeaderText="Cust. Remarks" />
                                                <asp:BoundField DataField="CALLREMARKS" HeaderText="Our Remarks" />
                                                <asp:BoundField DataField="CALLBY" HeaderText="Call By" />
                                                <asp:BoundField DataField="CALLSTATUS" HeaderText="Call Status" />


                                                <%--<asp:TemplateField HeaderText="Image Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblImageType" runat="server" Text='<%# Eval("LISTDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="box-footer text-center">
                                <i class="fa fa-spin fa-refresh fa-lg" id="faLoading3" style="display: none"></i>
                                <p style="margin-bottom: 0px; margin-top: 5px">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnCancel_Click" UseSubmitBehavior="false"
                                        OnClientClick="if (window.Page_ClientValidate('VgCancelStatus') == true) {this.disabled = true; $('#faLoading1').show();}" />
                                </p>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dlListSaved" />
            <asp:AsyncPostBackTrigger ControlID="dlHList" />
            <asp:AsyncPostBackTrigger ControlID="dlCList" />
            <asp:AsyncPostBackTrigger ControlID="ConvertedList" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="modal-EditName" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Update Customer Name</h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:HiddenField ID="hfUpdateID" runat="server" />
                                            <label>Cust. Name : </label>
                                            <asp:TextBox ID="txtCustNewName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvCustNewName" runat="server" ControlToValidate="txtCustNewName" ForeColor="Red" Display="Dynamic"
                                                ValidationGroup="VgNameUpdate" ErrorMessage="Enter Customer Name">Enter Customer Name</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer text-center">
                                <i class="fa fa-spin fa-refresh fa-lg" id="faLoading2" style="display: none"></i>
                                <p style="margin-bottom: 0px; margin-top: 5px">
                                    <asp:Button ID="btnNameUpdate" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnNameUpdate_Click" UseSubmitBehavior="false"
                                        OnClientClick="if (window.Page_ClientValidate('VgNameUpdate') == true) {this.disabled = true; $('#faLoading1').show();}" />
                                    <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" ValidationGroup="VgStatus" />--%>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dlListSaved" />
            <asp:AsyncPostBackTrigger ControlID="dlHList" />
            <asp:AsyncPostBackTrigger ControlID="dlCList" />
            <asp:AsyncPostBackTrigger ControlID="ConvertedList" />
        </Triggers>
    </asp:UpdatePanel>

    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="modal-notification" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">View Notification</h4>
                        </div>

                        <div class="modal-body">
                            <div class="box-body">
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
                                                        <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnWhatsapp" runat="server" Text="Send MSG" OnClick="btnWhatsapp_Click"><i class="fa fa-send"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                        <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                                        <asp:BoundField DataField="MESSAGE" HeaderText="Message" Visible="false" />
                                                        <asp:BoundField DataField="ITEMID" HeaderText="Item Id" Visible="false" />
                                                        <%--<asp:BoundField DataField="CUSTNAME" HeaderText="Cust. Name" />--%>

                                                        <asp:TemplateField HeaderText="Cust. Name" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--<asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkCustName" runat="server" Text='<%# Eval("CUSTNAME") %>' OnClick="lnkCustName_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

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
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dlListSaved" />
            <asp:AsyncPostBackTrigger ControlID="dlHList" />
            <asp:AsyncPostBackTrigger ControlID="dlCList" />
            <asp:AsyncPostBackTrigger ControlID="ConvertedList" />
        </Triggers>
    </asp:UpdatePanel>


    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="modal-EnterSO" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Update Sales Order Number</h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:HiddenField ID="hfSOUpdateID" runat="server" />
                                            <label>SI Number : </label>
                                            <asp:TextBox ID="txtSONO" runat="server" CssClass="form-control" MaxLength="10" TextMode="Number" OnTextChanged="txtSONO_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSONO" runat="server" ControlToValidate="txtSONO" ForeColor="Red" Display="Dynamic"
                                                ValidationGroup="VgSOUpdate" ErrorMessage="Enter SI Number">Enter SI Number</asp:RequiredFieldValidator>
                                            <asp:Label ID="lblSOError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-footer text-center">
                                <i class="fa fa-spin fa-refresh fa-lg" id="faLoading2" style="display: none"></i>
                                <p style="margin-bottom: 0px; margin-top: 5px">
                                    <asp:Button ID="btnSOUpdate" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnSOUpdate_Click" UseSubmitBehavior="false"
                                        OnClientClick="if (window.Page_ClientValidate('VgSOUpdate') == true) {this.disabled = true; $('#faLoading1').show();}" />
                                    <%--<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" DisplayMode="BulletList" ValidationGroup="VgStatus" />--%>
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dlListSaved" />
            <asp:AsyncPostBackTrigger ControlID="dlHList" />
            <asp:AsyncPostBackTrigger ControlID="dlCList" />
            <asp:AsyncPostBackTrigger ControlID="btnSOUpdate" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ConvertedList" />
        </Triggers>
    </asp:UpdatePanel>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranCallLogs" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" />
</asp:Content>





<%--<div class="row" style="margin-top: 20px !important;">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">

                                        <asp:DataList ID="DataList1" runat="server" RepeatDirection="Vertical" Width="100%">
                                             OnItemCommand="dlListSaved_ItemCommand" OnItemDataBound="dlListSaved_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="timeline-item" style="border-bottom: 1px solid #f4f4f4">
                                                    <div class="box-body pad table-responsive" style="margin: 0px">
                                                        <table class="table text-center col-sm-12 no-border">
                                                            <tr>
                                                                <td class="col-md-2" style="vertical-align: middle;">

                                                                    <table style="width: 260px !important;">
                                                                        <tr>
                                                                            <td>


                                                                                <p align="left" style="width: 125px !important;">
                                                                                    <asp:Label ID="lblMake" CssClass="btn btn-success btn-primary btn-xs" Style="background-color: gray; border-color: green;" runat="server" Text='<%# Eval("MAKE") %>'></asp:Label>
                                                                                </p>
                                                                                <p align="left" style="width: 125px !important;">
                                                                                    <asp:Label ID="lblRAM" CssClass="btn btn-success btn-primary btn-xs" Style="background-color: gray; border-color: green;" runat="server" Text='<%# Eval("RAM") %>'></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                            <td style="padding-left: 10px">

                                                                                <p align="left" style="width: 125px !important;">

                                                                                    <asp:Label ID="lblModel" CssClass="btn btn-success btn-primary btn-xs" Style="background-color: gray; border-color: green;" runat="server" Text='<%# Eval("MODEL") %>'></asp:Label>
                                                                                </p>
                                                                                <p align="left" style="width: 125px !important;">
                                                                                    <asp:Label ID="lblROM" CssClass="btn btn-success btn-primary btn-xs" Style="background-color: gray; border-color: green;" runat="server" Text='<%# Eval("ROM") %>'></asp:Label>
                                                                                </p>
                                                                            </td>
                                                                            <td style="padding-left: 10px">

                                                                                <p align="left" style="width: 125px !important;">
                                                                                    <asp:Label ID="lblColor" CssClass="btn btn-success btn-primary btn-xs" Style="background-color: gray; border-color: green;" runat="server" Text='<%# Eval("COLOR") %>'></asp:Label>
                                                                                </p>
                                                                                <p align="left" style="width: 125px !important;">
                                                                                    <asp:Label ID="lblPriceRange" CssClass="btn btn-success btn-primary btn-xs" Style="background-color: gray; border-color: green;" runat="server" Text='<%# Eval("PRICERANGE") %>'></asp:Label>
                                                                                </p>

                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>


                                                                <td class="col-md-2" align="left" style="vertical-align: middle; width: 100px !important;">
                                                                    <p align="left">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%# Eval("MAKE") %>'></asp:Label>
                                                                    </p>
                                                                    <p align="left">
                                                                        <asp:Label ID="Label3" runat="server" Text='<%# Eval("MODEL") %>'> </asp:Label>
                                                                    </p>
                                                                </td>


                                                                <td class="col-md-2" align="left" style="vertical-align: middle; width: 100px !important;">
                                                                    <asp:HiddenField ID="lblid" runat="server" Value='<%# Eval("ID") %>' />
                                                                    <p align="left">
                                                                        <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("CONTACTNO") %>'></asp:Label>
                                                                    </p>
                                                                    <p align="left">
                                                                        <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("CUSTNAME") %>'> </asp:Label>
                                                                    </p>
                                                                </td>

                                                                <td class="col-md-2" style="vertical-align: middle; width: 300px !important;">
                                                                    <p align="left">
                                                                        Cust. Remarks : 
                                                                    <asp:Label ID="lblCallId" runat="server" Text='<%# Eval("CUSTREMARKS") %>'> </asp:Label>
                                                                    </p>
                                                                    <p align="left">
                                                                        Ref. :
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("REFF") %>'> </asp:Label>
                                                                    </p>
                                                                    <p align="center">
                                                                        <%# Eval("CALLREF") %>
                                                                    </p>
                                                                    <p align="center" style="margin: 0px;">
                                                                    <%# Eval("REF") %>
                                                                </p>
                                                                </td>

                                                                <td class="col-md-2" style="vertical-align: middle; text-align: left; width: 100px !important;">
                                                                    <p style="padding-left: 15px;"><i class="fa fa-calendar-check-o"></i>&nbsp; Date : <%# Eval("CUSTUPDATEDATE","{0:dd-MM-yyyy}")%></p>
                                                                    <p style="padding-left: 15px; margin: unset;"><i class="fa  fa-clock-o"></i>&nbsp; Time : <%# Eval("CREATEDTAE","{0:hh:mm:ss tt}") %> </p>
                                                                </td>

                                                                <td class="col-md-2" style="vertical-align: middle">
                                                                    <table>
                                                                        <tr>
                                                                            <td>

                                                                                <p align="center">
                                                                                    <asp:Button ID="hlConvert" runat="server" CommandName="Convert" Text="Convert" Enabled="true" CssClass="btn btn-success btn-primary btn-xs"></asp:Button>
                                                                                </p>
                                                                                <p align="center" style="margin: unset;">
                                                                                    <asp:Button ID="hlInq" runat="server" CommandName="Inquiry" Text="Inquiry" Enabled="true" CssClass="btn btn-success btn-danger btn-xs"></asp:Button>
                                                                                </p>
                                                                                <p align="center">

                                                                                    <asp:HyperLink ID="hlReg" runat="server" Enabled="false" Text="Registration" Target="_blank" CssClass="btn btn-info btn-xs" NavigateUrl='<%# string.Format("CustReg.aspx?CN={0}&CID={1}&CMK={2}&CMD={3}",
                                            HttpUtility.UrlEncode(Eval("CALLEDNUMBER").ToString()), HttpUtility.UrlEncode(Eval("ID").ToString()),HttpUtility.UrlEncode(Eval("BRAND_ID").ToString()),HttpUtility.UrlEncode(Eval("MODEL").ToString())) %>'></asp:HyperLink>
                                                                                </p>
                                                                                <p align="center" style="margin: unset;">

                                                                                    <asp:HyperLink ID="hlInquiry" runat="server" Enabled="false" Text="Inquiry" Target="_blank" CssClass="btn btn-warning btn-xs" NavigateUrl='<%# string.Format("Inquiry.aspx?CN={0}&CID={1}&CMK={2}&CMD={3}&SEG={4}",
                                            HttpUtility.UrlEncode(Eval("CALLEDNUMBER").ToString()), HttpUtility.UrlEncode(Eval("ID").ToString()),HttpUtility.UrlEncode(Eval("BRAND_ID").ToString()),HttpUtility.UrlEncode(Eval("MODEL").ToString()),HttpUtility.UrlEncode(Eval("SEGMENT").ToString())) %>'></asp:HyperLink>
                                                                                </p>
                                                                            </td>
                                                                            <td style="padding-left: 10px">
                                                                                <p align="center">
                                                                                    <asp:Button ID="hlHold" runat="server" CommandName="Hold" Text="Hold" Enabled="true" CssClass="btn btn-success btn-primary btn-xs"></asp:Button>
                                                                                </p>
                                                                                <p align="center" style="margin: unset;">
                                                                                    <asp:Button ID="hlCancel" runat="server" CommandName="Cancel" Text="Cancel" Enabled="true" CssClass="btn btn-success btn-danger btn-xs"></asp:Button>
                                                                                </p>

                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>

                                                                <td class="col-md-1" style="vertical-align: middle;">
                                                                    <asp:LinkButton ID="btnCallAttent" runat="server" CommandName="CallAttend" CssClass="btn btn-success btn-app bg-green margin" Style="border-radius: 5px" Text="<i id='spancall' runat='server' class='fa fa-phone' style='color: #FFF'><asp:Label ID='lblCallText' CssClass='form-control' runat='server' Text='Call'></asp:Label></i>   " />


                                                                <td class="col-md-1" style="vertical-align: middle;">
                                                                    <asp:LinkButton ID="btnDetails" runat="server" CommandName="CallDetails" CssClass="btn btn-success bg-green margin" Style="border-radius: 5px" Text="<i id='spaninfo' class='fa fa-info-circle' style='color: #FFF'></i>   <asp:Label ID='lblCallText' runat='server' Text='Details'></asp:Label>" />
                                                                </td>

                                                                <td class="col-md-3" style="vertical-align: middle; text-align: left">
                                                                    <p>
                                                                        <span>Attempt By :<strong>
                                                                            <asp:Label ID="lblAttmBy" runat="server" Text='<%# Eval("CALLATTMBY") %>'> </asp:Label></strong></span>
                                                                    </p>
                                                                    <p>
                                                                        <span>Start : <strong>
                                                                            <asp:Label ID="lblCallSt" runat="server" Text='<%# Eval("CALLATTMST") %>'> </asp:Label></strong></span>
                                                                    </p>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:DataList>

                                    </div>
                                </div>
                            </div>
                        </div>--%>