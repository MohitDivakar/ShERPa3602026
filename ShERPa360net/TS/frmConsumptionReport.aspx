<%@ Page Title="" Language="C#" MasterPageFile="~/TS/MasterTS.Master" AutoEventWireup="true" CodeBehind="frmConsumptionReport.aspx.cs" Inherits="ShERPa360net.TS.frmConsumptionReport" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Consumption Report</strong></h3>
                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Report" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click" ValidationGroup="SearchDetail"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 style="color: #f05423">Consumption Report</h4>
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Filter Detail</legend>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">From Date</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvfromdate" Style="color: red!important;" runat="server" ControlToValidate="txtFromDate" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter From Date To Search">Please Enter From Date To Search</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">To Date</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvToDate" Style="color: red!important;" runat="server" ControlToValidate="txtToDate" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter To Date To Search">Please Enter To Date To Search</asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="page-content-wrap">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel panel-default tabs">
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Details</a></li>
                                        </ul>
                                        <div class="panel-body tab-content">
                                            <div class="tab-pane active" id="tab-first">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <fieldset class="scheduler-border">
                                                            <legend class="scheduler-border" id="lgrecordcount" runat="server"></legend>
                                                            <div class="col-md-12 divhorizontal" style="overflow-x: scroll!important;">
                                                                <asp:GridView ID="gvRepairReport" OnRowDataBound="gvRepairReport_RowDataBound" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Notification No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNotification" Text='<%# Bind("NDSNO") %>'></asp:Label>
                                                                                <asp:HiddenField runat="server" ID="hdPartsValue" Value='<%# Bind("PARTDETAILVALUE") %>'></asp:HiddenField>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="User Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblsTaskUserStatus" Text='<%# Bind("NotificationStatus") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Start_Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStartDate" Text='<%# Bind("STARTDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Finish_Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFinishDate" Text='<%# Bind("FINISHDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Components1">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom1"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Comp2">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom2"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="comp3">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom3"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="comp4">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom4"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="com5">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom5"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="com6">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom6"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="com7">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom7"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="com8">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom8" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="com9">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom9" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="com10">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom10" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="com11">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom11" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="com12">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom12" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="com13">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom13" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="com14">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom14" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="com15">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom15" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="com16">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCom16" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Storage_Location(RESBD-LGORT)">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStorageLocation" Text="TGAH"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Requirement_quantity(RESBD-MENGE)">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReqQty" Text="1"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Document_Hea_Text">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDocumentHeaText" Text="consumption"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </fieldset>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--           </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranTATASKYREPORT" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranTATASKY" runat="server" />
</asp:Content>
