<%@ Page Title="" Language="C#" MasterPageFile="~/TS/MasterTS.Master" AutoEventWireup="true" CodeBehind="Assignment.aspx.cs" Inherits="ShERPa360net.TS.Assignment" %>

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
                                <asp:AsyncPostBackTrigger ControlID="gvAssignment" EventName="RowUpdating" />
                                <asp:AsyncPostBackTrigger ControlID="txtEsnNo" EventName="TextChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="panel-heading">
                                    <asp:HiddenField runat="server" ID="hdIsAvailable" Value="0" />
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Create Notification</strong></h3>
                                    <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClientClick="return AssigmentValidation();" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">Entry</h4>
                                            <div class="row">
                                                <fieldset class="scheduler-border">
                                                    <legend class="scheduler-border">Create Notification</legend>
                                                    <div class="col-md-12">

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Date. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                        <asp:TextBox ID="txtDate" Style="font-weight: bold!important; color: red!important;" runat="server" CssClass="form-control datepicker required_text_box" Enabled="false" MaxLength="10"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Time. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtTime" Style="font-weight: bold!important; color: red!important;" runat="server" placeholder="Time" Enabled="false" CssClass="form-control required_text_box"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">ESN No. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtEsnNo" TabIndex="1" OnTextChanged="txtEsnNo_TextChanged" AutoPostBack="true"  Style="font-weight: bold!important; color: black!important;" MaxLength="12" runat="server" placeholder="ESN No" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvEsnNo" Style="color: red!important;" runat="server" ControlToValidate="txtEsnNo" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter ESN No">Please Enter ESN No</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Model. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlModel" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvModels" Style="color: red;" ControlToValidate="ddlModel" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Model" InitialValue="0">Please Select Model</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">SR No. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="3" ID="txtISPCode" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="SR No" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvISPCode" Style="color: red!important;" runat="server" ControlToValidate="txtISPCode" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter ISP Code">Please Enter SR No</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">TAG. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="4" ID="ddlTAG" runat="server" ClientIDMode="Static" CssClass="form-control ddlTAG"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvTag" Style="color: red;" ControlToValidate="ddlTAG" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select TAG" InitialValue="0">Please Select TAG</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Fault Reported. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                     <asp:DropDownList TabIndex="5" ID="ddLFaultReported" ClientIDMode="Static" runat="server" CssClass="form-control ddLFaultReported"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvFaultReported" Style="color: red;" ControlToValidate="ddLFaultReported" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Fault Reported" InitialValue="0">Please Select Fault Reported</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Notification No. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="6" ID="txtNDSNO" Style="font-weight: bold!important; color: black!important;" MaxLength="10" runat="server" placeholder="Notification No" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvNDSNO" Style="color: red!important;" runat="server" ControlToValidate="txtNDSNO" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Notification No">Please Enter Notification No</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" style="display: none">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">PreScan Date</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                        <asp:TextBox ID="txtPreScanDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                                    </div>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvPreScanDate" Style="color: red!important;" runat="server" ControlToValidate="txtPreScanDate" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter PreScan Date">Please Enter PreScan Date</asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdAssignmentId" Value="0" />
                                                                <label class="col-md-5 control-label">Assignment No. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtAssignmentNo" TabIndex="7" runat="server" placeholder="Assognment No" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Received Date</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <div class="input-group">
                                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                        <asp:TextBox TabIndex="8" ID="txtReceivedDate" AutoCompleteType="None" runat="server" Text="DD/MM/YYYY" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RequiredFieldValidator ID="rfvfromdate" Style="color: red!important;" runat="server" ControlToValidate="txtReceivedDate" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Received Date">Please Enter Received Date</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-3" style="display: none">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Engineer. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlEngineer" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvEngineer" Style="color: red;" ControlToValidate="ddlEngineer" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Engineer" InitialValue="0">Please Select Engineer</asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" style="display: none">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">PreScan Problem. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlPreScanProblem" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvProblems" Style="color: red;" ControlToValidate="ddlPreScanProblem" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select PreScan Problem" InitialValue="0">Please Select PreScan Problem</asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Condition. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlCondition" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvCondition" Style="color: red;" ControlToValidate="ddlCondition" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Condition" InitialValue="0">Please Select Condition</asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Repair. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlRepair" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvRepair" Style="color: red;" ControlToValidate="ddlRepair" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Repair" InitialValue="0">Please Select Repair</asp:RequiredFieldValidator>--%>
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
                                                                        <asp:GridView ID="gvAssignment" TabIndex="9" OnRowCommand="gvAssignment_RowCommand" OnRowUpdating="gvAssignment_RowUpdating" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="Notification Date">
                                                                                    <ItemTemplate>
                                                                                        <asp:HiddenField runat="server" ID="hdEngineerKey" Value='<%# Bind("ENGINEERKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdModelKey" Value='<%# Bind("MODELSKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdConditionKey" Value='<%# Bind("CONDITIONKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdLevelKey" Value='<%# Bind("LEVELKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdProblemKey" Value='<%# Bind("PROBLEMKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="lblPreScanProblemKey" Value='<%# Bind("PRESCANNINGPROBLEMKEY") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdTag" Value='<%# Bind("TAGKEYVALUE") %>'></asp:HiddenField>
                                                                                        <asp:HiddenField runat="server" ID="hdFaultReported" Value='<%# Bind("FAULTREPORTEDKEYVALUE") %>'></asp:HiddenField>
                                                                                        <asp:Label runat="server" ID="lblDate" Text='<%# Bind("ASSIGNDATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Time">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblTime" Text='<%# Bind("ASSIGNTIME") %>'></asp:Label>
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

                                                                                <asp:TemplateField HeaderText="SR No">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblISPCode" Text='<%# Bind("ISPFAULTCODE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                 <asp:TemplateField HeaderText="TAG">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblTAG" Text='<%# Bind("TAGKEYVALUE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Fault Reported">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label runat="server" ID="lblFaultReported" Text='<%# Bind("FAULTREPORTEDKEYVALUE") %>'></asp:Label>
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
    <input type="hidden" id="menutabid" value="tsmTranTATASKYNOTIFICATIONENTRY" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranTATASKY" runat="server" />
</asp:Content>

