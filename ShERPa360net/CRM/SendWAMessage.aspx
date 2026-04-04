<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="SendWAMessage.aspx.cs" Inherits="ShERPa360net.CRM.SendWAMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>WhatsApp Msg</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <%--<div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Call  </strong>Data</h3>
                    </div>--%>
                    <div class="panel-body">
                        <section class="content-header">
                            <h1>Send WhatsApp Message</h1>
                        </section>




                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Mobile No. : </label>
                                        <div class="col-md-10 col-xs-12 pull-right">
                                            <asp:TextBox ID="txtMobileNo" runat="server" MaxLength="10" CssClass="form-control" placeholder="Customer Mobile No." autofocus></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Enter Mobile No."
                                                ValidationGroup="WA" Display="Dynamic">Enter Mobile No.</asp:RequiredFieldValidator>

                                            <asp:RegularExpressionValidator ID="revMobile" runat="server"
                                                ControlToValidate="txtMobileNo" ErrorMessage="Enter 10 digit Mobile No."
                                                ValidationExpression="[0-9]{10}" Display="Dynamic">Enter 10 digit Mobile No.</asp:RegularExpressionValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Message : </label>
                                        <div class="col-md-10 col-xs-12 pull-right">
                                            <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" placeholder="Message" TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage" ErrorMessage="Enter Message"
                                                ValidationGroup="WA" Display="Dynamic">Enter Message</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                             <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Media URL : </label>
                                        <div class="col-md-10 col-xs-12 pull-right">
                                            <asp:TextBox ID="txtURL" runat="server" CssClass="form-control" placeholder="Media URL" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="col-md-2 control-label"></label>
                                        <div class="col-md-10 col-xs-12 pull-right">
                                            <asp:Button ID="btnSend" runat="server" CssClass="btn btn-success margin-r-5 " Text="Send" UseSubmitBehavior="false" ValidationGroup="WA" OnClientClick="ShowLoad(this.id)" OnClick="btnSend_Click" />
                                            <i class="fa fa-spin fa-refresh fa-lg" id="faLoading" style="display: none"></i>
                                        </div>
                                    </div>
                                </div>
                            </div>

                           

                            <script>
                                function ShowLoad(clicked_id) {
                                    if ($('#ContentPlaceHolder1_txtMobileNo').val() != '' || $('#ContentPlaceHolder1_txtMessage').val() != '') {
                                        $('#' + clicked_id).prop('disabled', true);
                                        $('#faLoading').show();
                                    }
                                }
                            </script>
                        </div>


                        <%--<section class="content">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="box box-primary">
                                        <div class="box-body">

                                                                                        <div class="col-md-12 no-padding">
                                                <div class="form-group">
                                                    <label>Mobile No. :*</label>
                                                </div>
                                            </div>
                                            <div class="col-md-12 no-padding">
                                                <div class="form-group">
                                                    <label>Message :*</label>
                                                 
                                                </div>
                                            </div>

                                            <div class="margin-bottom">
                                               
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </section>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmUtlWhatsappMsg" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" />

</asp:Content>
