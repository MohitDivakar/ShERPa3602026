<%@ Page Title="" Language="C#" MasterPageFile="~/TS/MasterTS.Master" AutoEventWireup="true" CodeBehind="frmIRReport.aspx.cs" Inherits="ShERPa360net.TS.frmIRReport" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;QC Result Update</strong></h3>
                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Report" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click" ValidationGroup="SearchDetail"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 style="color: #f05423">Dispatch Report</h4>
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
                                                                <asp:GridView ID="gvRepairReport"  OnRowDataBound="gvRepairReport_RowDataBound" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SR No">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Notification No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNotification" Text='<%# Bind("NDSNO") %>'></asp:Label>
                                                                                <asp:HiddenField runat="server" ID="hdPartsValue" Value='<%# Bind("PARTDETAILVALUE") %>'></asp:HiddenField>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="STBS Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblESNNo" Text='<%# Bind("ESNNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Fault - Technical (Object Part No.)">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFaultTechnicalObject" Text='<%# Bind("OBJECTPARTKEY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="RC Fault  Eng Symptoms(Damage Code No.)">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRcFaultEngDamage" Text='<%# Bind("FAULTOBSERVEDKEY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Reason Code No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReasonCode" Text='<%# Bind("FAULTREASONKEY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Tasks(Action Code)">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblTaskAction" Text='<%# Bind("ACTIONKEY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Reason For IR">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblTechnicianNo" Text='<%# Bind("REASONFORIRKEY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Activity -1">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblActivity1"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Activity -2">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblActivity2"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Activity -3">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblActivity3"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Activity -4">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblActivity4"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Activity -5">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblActivity5"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Activity -6">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblActivity6"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Activity -7">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblActivity7"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Cosmetic -1">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCosmetic1" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Cosmetic -2">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCosmetic2" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="DrySol-1">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDrySol1" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="DrySol-2">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDrySol2" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="DrySol-3">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDrySol3" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="DrySol-4">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDrySol4" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="DrySol-5">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDrySol5" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Plant">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPlant" Text="5300"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Screening Eng. ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblScreeningEngID" Text='<%# Bind("PRESCANNINGENGINEERKEYCODE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Repair Technician Code(Action Code)">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRepairTechnicianCode" Text='<%# Bind("REPARIENGINEERKEYCODE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FT Technician No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFTTechnicianNo" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="SOAK TECH CODE">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSoakTechCodeNo" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="soak test">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSoakTest" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Final Test">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFinalTest" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="soak failre reason">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSoakfailurereason" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FT failure reason">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblftfailurereason" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Notification User Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblUserStatus" Text="IR"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Task User Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblsTaskUserStatus" Text="IR"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="HDD TYPE">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblHDDType" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="HDD Serial Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblHDDSerialNumber" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Task Text">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblTaskText" Text=""></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Notification Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNotificationStatus" Text="NOCO"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Text">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblText" Text=""></asp:Label>
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
