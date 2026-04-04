<%@ Page Title="" Language="C#" MasterPageFile="~/TS/MasterTS.Master" AutoEventWireup="true" CodeBehind="NotificationBeckendExcelUpload.aspx.cs" Inherits="ShERPa360net.TS.NotificationBeckendExcelUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:HiddenField runat="server" ID="hdIsAvailable" Value="0" />
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Notification Beckend Upload Entry</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 style="color: #f05423">Notification Beckend Upload Entry</h4>
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Notification Beckend Upload Entry</legend>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Select File. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload runat="server" ClientIDMode="Static" ID="flNotification" />
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <asp:Button runat="server" ClientIDMode="Static" ID="btnUpload" OnClick="btnUpload_Click" OnClientClick="return ValidateNotificationUpload();" Text="Upload" CssClass="btn btn-primary" />

                                                    <asp:Button runat="server" Style="margin-left: 5px!important;" OnClick="btnCancel_Click" ClientIDMode="Static" ID="btnCancel" Text="Cancel" CssClass="btn btn-danger" />
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

                                                            <div class="col-md-12" style="margin-top: 5px!important; margin-bottom: 5px!important;">
                                                                <div class="col-md-2">
                                                                    <legend class="scheduler-border" id="lgrecordcount" runat="server"></legend>
                                                                </div>

                                                                <div class="col-md-8 text-center">
                                                                <div class="col-md-8 text-center">
                                                                    <asp:Button runat="server" ClientIDMode="Static" OnClick="btnSaveDetail_Click" ID="btnSaveDetail" CssClass="btn btn-success" Text="Save" />
                                                                </div>


                                                            </div>
                                                            <div class="col-md-12 divhorizontal" style="overflow-x: scroll;">
                                                                <asp:GridView ID="gvAssignment" OnRowDataBound="gvAssignment_RowDataBound" TabIndex="9" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Serial No.">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdModelKey" Value='<%# Bind("MODELSKEY") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdREPARIENGINEERKEY" Value='<%# Bind("REPARIENGINEERKEY") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdPRESCANNINGPROBLEMKEY" Value='<%# Bind("PRESCANNINGPROBLEMKEY") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdFAULTOBSERVEDKEY" Value='<%# Bind("FAULTOBSERVEDKEY") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdOBJECTPARTKEY" Value='<%# Bind("OBJECTPARTKEY") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdFAULTREASONKEY" Value='<%# Bind("FAULTREASONKEY") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdREPARITASKDESCRIPTIONKEY" Value='<%# Bind("REPARITASKDESCRIPTIONKEY") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdIscorrected" Value='<%# Bind("ISNOTIFICATIONCORRECTED") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblESNNo" Text='<%# Bind("ESNNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Notifctn">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNDSNO" Text='<%# Bind("NDSNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDescription" Text='<%# Bind("PRESCANNINGPROBLEMKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Notif.date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNotificationdt" Text='<%# Bind("ASSIGNDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ISP Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblISPCode" Text='<%# Bind("ISPFAULTCODE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Model">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODELSKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mn.wk.ctr">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblEngineerName" Text='<%# Bind("REPARIENGINEERKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Problem code text">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblProblemcode" Text='<%# Bind("FAULTOBSERVEDKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Object part code text">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblObjectPart" Text='<%# Bind("OBJECTPARTKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Cause code text">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCausecode" Text='<%# Bind("FAULTREASONKEYVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("REPARITASKDESCRIPTIONKEYVALUE") %>'></asp:Label>
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

