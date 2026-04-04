<%@ Page Title="" Language="C#" MasterPageFile="~/TS/MasterTS.Master" AutoEventWireup="true" CodeBehind="AssignmentQcReport.aspx.cs" Inherits="ShERPa360net.TS.AssignmentQcReport" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <%--       <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="lnkSearh" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgReset" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkExport" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>--%>
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;QC Result Update</strong></h3>
                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Report" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click" ValidationGroup="SearchDetail"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 style="color: #f05423">Report</h4>
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


                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Assognment No. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAssignmentNo" runat="server" placeholder="Assognment No" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Notification No. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtNDSNO" runat="server" placeholder="Notification No" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" style="margin-top: 10px!important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">ESN No. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtEsnNo" Style="font-weight: bold!important; color: black!important;" MaxLength="12" runat="server" placeholder="ESN No" CssClass="form-control required_text_box"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Engineer. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlEngineer" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Model. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Status. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Condition. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlCondition" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Repair. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlRepair" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                                                <asp:GridView ID="gvAssignmentQcRepor" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CONVERTEDASSIGNDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Time">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblTime" Text='<%# Bind("ASSIGNTIME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("REPARITASKDESCRIPTIONKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Repaired Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRepairedDate" Text='<%# Bind("REPAIREDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Assignment No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAssignmentNo" Text='<%# Bind("ASSIGNMENTNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Received Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReceivedDate" Text='<%# Bind("RECEIVEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Notification No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNDSNO" Text='<%# Bind("NDSNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ESN No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblESNNo" Text='<%# Bind("ESNNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Model">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODELSKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Engineer">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblEngineer" Text='<%# Bind("ENGINEERKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="PreScan Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPrescanDate" Text='<%# Bind("PRESCANNINGDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="PreScan Problem">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPrescanProblem" Text='<%# Bind("PRESCANNINGPROBLEMKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ISP Fault">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblISPFault" Text='<%# Bind("ISPFAULTVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="CID Reason">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCIDReason" Text='<%# Bind("CIDREASON") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Object Part">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblObjectPart" Text='<%# Bind("OBJECTPARTKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Fault Observed">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFaultObserved" Text='<%# Bind("FAULTOBSERVEDKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Fault Reason">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFaultReason" Text='<%# Bind("FAULTREASONKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAction" Text='<%# Bind("ACTIONKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Part Name" ControlStyle-Width="90">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPartName" Text='<%# Bind("PARTNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Part Location" ControlStyle-Width="90">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPartLocation" Text='<%# Bind("PARTLOCATION") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Reason For IR">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReasonforIR" Text='<%# Bind("REASONFORIRKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Qc Result">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblQcResult" Text='<%# Bind("QCRESULTKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Qc Fail Reason">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblQcFailReason" Text='<%# Bind("QCFAILREASONKEYVALUE") %>'></asp:Label>
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
