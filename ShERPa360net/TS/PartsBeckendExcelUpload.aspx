<%@ Page Title="" Language="C#" MasterPageFile="~/TS/MasterTS.Master" AutoEventWireup="true" CodeBehind="PartsBeckendExcelUpload.aspx.cs" Inherits="ShERPa360net.TS.PartsBeckendExcelUpload" %>

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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Notification Parts Upload Entry</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 style="color: #f05423">Notification Parts Upload Entry</h4>
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Notification Parts Upload Entry</legend>
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
                                                                    <asp:Button runat="server" ClientIDMode="Static" OnClick="btnSaveDetail_Click" ID="btnSaveDetail" CssClass="btn btn-success" Text="Save" />
                                                                </div>


                                                            </div>
                                                            <div class="col-md-12 divhorizontal" style="overflow-x: scroll;">
                                                                <asp:GridView ID="gvAssignment" OnRowDataBound="gvAssignment_RowDataBound" TabIndex="9" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Notifctn">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdIscorrected" Value='<%# Bind("ISNOTIFICATIONCORRECTED") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblNDSNO" Text='<%# Bind("NDSNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Parts">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblParts" Text='<%# Bind("LOCATIONNAME") %>'></asp:Label>
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

