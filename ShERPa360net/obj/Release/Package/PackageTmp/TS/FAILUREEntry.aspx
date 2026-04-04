<%@ Page Title="" Language="C#" MasterPageFile="~/TS/MasterTS.Master" AutoEventWireup="true" CodeBehind="FAILUREEntry.aspx.cs" Inherits="ShERPa360net.TS.FAILUREEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgReset" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgSaveAll" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkSearh" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="gvAssignment" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="txtEsnNo" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Failure Entry</strong></h3>
                                    <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">Entry</h4>
                                            <div class="row">
                                                <fieldset class="scheduler-border">
                                                    <legend class="scheduler-border">Failure Entry</legend>
                                                    <div class="col-md-12">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">ESN No. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtEsnNo" TabIndex="1" OnTextChanged="txtEsnNo_TextChanged" AutoPostBack="true" Style="font-weight: bold!important; color: black!important;" MaxLength="12" runat="server" placeholder="ESN No" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvEsnNo" Style="color: red!important;" runat="server" ControlToValidate="txtEsnNo" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter ESN No">Please Enter ESN No</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Model. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" Enabled="false" ID="ddlModel" runat="server" CssClass="form-control ddlModel"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" style="display: none;">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">SR No. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="3" Enabled="false" ID="txtISPCode" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="SR No" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" style="display: none;">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">TAG. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="4" Enabled="false" ID="txtISPFault" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="TAG" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Notification No. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="5" ID="txtNDSNO" Enabled="false" Style="font-weight: bold!important; color: black!important;" MaxLength="10" runat="server" placeholder="Notification No" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdAssignmentId" Value="0" />
                                                                <label class="col-md-5 control-label">Assignment No. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtAssignmentNo" TabIndex="6" runat="server" ClientIDMode="Static" placeholder="Assognment No" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-12">
                                                        <div class="col-md-3" style="display: none;">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Fault Reported. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="7" Enabled="false" ID="txtCIDReason" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Fault Reported" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>



                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Received Date</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                        <asp:TextBox Enabled="false" TabIndex="8" ID="txtReceivedDate" AutoCompleteType="None" runat="server" Text="DD/MM/YYYY" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">PreScan Date</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                        <asp:TextBox ID="txtPreScanDate" TabIndex="9" PlacHolder="DD-MM-YYYY" Enabled="false" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Prescan Prob. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlPreScanProblem" TabIndex="10" Enabled="false" runat="server" CssClass="form-control ddlPreScanProblem"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Prescan Engin. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlPreScanEngineer" TabIndex="11" Enabled="false" runat="server" CssClass="form-control ddlPreScanEngineer"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Tech Allocation. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlEngineer" TabIndex="12" Enabled="false" runat="server" CssClass="form-control ddlEngineer"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Repair Date</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                        <asp:TextBox ID="txtRepairDate" TabIndex="13" Enabled="false" PlacHolder="DD-MM-YYYY" runat="server" CssClass="form-control datepicker required_text_box" MaxLength="10"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Rep Tech Name. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlRepairTechName" Enabled="false" TabIndex="14" runat="server" CssClass="form-control ddlRepairTechName required_text_box"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Obj Part Desc. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlObjectPartDescription" Enabled="false" TabIndex="15" runat="server" CssClass="form-control ddlObjectPartDescription required_text_box"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-3" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Condition. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlCondition" Enabled="false" TabIndex="16" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvCondition" Style="color: red;" ControlToValidate="ddlCondition" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Condition" InitialValue="0">Please Select Condition</asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Repair. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlRepair" Enabled="false" TabIndex="17" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvRepair" Style="color: red;" ControlToValidate="ddlRepair" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Repair" InitialValue="0">Please Select Repair</asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-12" style="margin-top: 10px!important;">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Damage Desc. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlDamageDescription" Enabled="false" TabIndex="18" runat="server" CssClass="form-control ddlDamageDescription required_text_box"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Cause Desc. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlCauseDescription" Enabled="false" TabIndex="19" runat="server" CssClass="form-control ddlCauseDescription required_text_box"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Action. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlAction" TabIndex="20" Enabled="false" runat="server" CssClass="form-control ddlAction required_text_box"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                         <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Failure Date</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                        <asp:TextBox ID="txtFailureDate" TabIndex="22" Enabled="false" PlacHolder="DD-MM-YYYY" runat="server" CssClass="form-control datepicker required_text_box" MaxLength="10"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" style="display:none;">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Repair Task. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlRepariTask" TabIndex="21" Enabled="false" runat="server" CssClass="form-control ddlRepariTask"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">
                                                       

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Failure Stage. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlFailureStage" TabIndex="23" runat="server" CssClass="form-control ddlFailureStage"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvFailureStage" Style="color: red;" ControlToValidate="ddlFailureStage" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Failure Stage" InitialValue="0">Please Select Failure Stage</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Fail Fault. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlFailFault" TabIndex="24" runat="server" CssClass="form-control ddlFailFault"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvFailFault" Style="color: red;" ControlToValidate="ddlFailFault" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Fail Fault" InitialValue="0">Please Select Fail Fault</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Inspector. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlInspector" TabIndex="25" runat="server" CssClass="form-control ddlInspector"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvInspector" Style="color: red;" ControlToValidate="ddlInspector" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Inspector" InitialValue="0">Please Select Inspector</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 text-center">
                                                        <asp:Label runat="server" Style="color: red; font-weight: bold!important;" Visible="false" ID="lblESNExitalert" ClientIDMode="Static">ESN Notification Entry Not Available</asp:Label>
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
                                                                    <div class="col-md-12 divhorizontal" style="overflow-x: scroll;">
                                                                        <asp:GridView ID="gvAssignment" TabIndex="26" OnRowCommand="gvAssignment_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="ESN No">
                                                                                    <ItemTemplate>
                                                                                        <asp:HiddenField runat="server" ID="hdEngineerKey" Value='<%# Bind("ENGINEERKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdModelKey" Value='<%# Bind("MODELSKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdConditionKey" Value='<%# Bind("CONDITIONKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdLevelKey" Value='<%# Bind("LEVELKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdProblemKey" Value='<%# Bind("PROBLEMKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="lblPreScanProblemKey" Value='<%# Bind("PRESCANNINGPROBLEMKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdPresScanningEngieerKey" Value='<%# Bind("PRESCANNINGENGINEERKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdIRKEY" Value='<%# Bind("REASONFORIRKEY") %>'></asp:HiddenField>
                                                                                        <asp:Label runat="server" ID="lblESNNo" Text='<%# Bind("ESNNO") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Model">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODELSKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Notification No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblNDSNO" Text='<%# Bind("NDSNO") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Received Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblReceivedDate" Text='<%# Bind("RECEIVEDATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Assignment No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblAssignmentNo" Text='<%# Bind("ASSIGNMENTNO") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="PreScanning Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblPreScanningDate" Text='<%# Bind("PRESCANNINGDATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Pre Scan Problem">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblPreScanningProblem" Text='<%# Bind("PRESCANNINGPROBLEMKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="PreScanning Engineer">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblPreScanningEngineer" Text='<%# Bind("PRESCANNINGENGINEERKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Tech Allocation">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblEngineer" Text='<%# Bind("ENGINEERKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Rep Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblRepairDate" Text='<%# Bind("REPAIRDATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Rep Tech Name">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblRepairTechEngineer" Text='<%# Bind("REPARIENGINEERKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Obj Part Desc">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblObjectPartDesc" Text='<%# Bind("OBJECTPARTKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Damage Desc">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblDamageDesc" Text='<%# Bind("FAULTOBSERVEDKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Cause Desc">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblCauseDesc" Text='<%# Bind("FAULTREASONKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Action">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblAction" Text='<%# Bind("ACTIONKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Repair Task">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblRepairTask" Text='<%# Bind("REPARITASKDESCRIPTIONKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Failure Entry Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblFailureEntryDate" Text='<%# Bind("FAILUREENTRYDATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Failure Stage">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblFailureStage" Text='<%# Bind("FAILURESTAGEKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Fail Fault">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblFailFaultReason" Text='<%# Bind("FAILUREFAULTEKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Inspector">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblInspectorName" Text='<%# Bind("INSPECTORYKEYVALUE") %>'></asp:Label>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranTATASKYFAILUREENTRY" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranTATASKY" runat="server" />
</asp:Content>

