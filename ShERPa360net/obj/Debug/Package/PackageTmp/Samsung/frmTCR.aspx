<%@ Page Title="Samsung TCR" Language="C#" MasterPageFile="~/Samsung/Samsung.Master" AutoEventWireup="true" CodeBehind="frmTCR.aspx.cs" Inherits="ShERPa360net.Samsung.frmTCR" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Samsung TCR</title>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            var Rcptno = $("#ContentPlaceHolder1_txtRcptno").val();
            var ComplaintNo = $("#ContentPlaceHolder1_txtComplaintNo").val();
            var CustName = $("#ContentPlaceHolder1_txtCustName").val();
            var Address = $("#ContentPlaceHolder1_txtAddress").val();
            var MobileNo = $("#ContentPlaceHolder1_txtMobileNo").val();
            var TotalCost = $("#ContentPlaceHolder1_txtTotalCost").val();
            var emailid = $("#ContentPlaceHolder1_txtEmailID").val();
            var PayMode = $("#ContentPlaceHolder1_ddlPayMode option:selected").val();
            var TransactionID = $("#ContentPlaceHolder1_txtTransactionID").val();

            var GSTFirm = $("#ContentPlaceHolder1_txtGSTFirm").val();
            var GSTNO = $("#ContentPlaceHolder1_txtGSTNO").val();

            //document.getElementById("fuImgDoc").files.length == 0

            if (Rcptno != "" && ComplaintNo != "" && CustName != "" && Address != "" && MobileNo != "" && TotalCost != "" && emailid != "") {
                debugger;
                if (PayMode == 2 || PayMode == 5 || PayMode == 6 || PayMode == 10 || PayMode == 11 || PayMode == 12 || PayMode == 14 || PayMode == 15) {
                    if (document.getElementById("ContentPlaceHolder1_fuImgDoc").files.length != 0) {
                        if ($('#ContentPlaceHolder1_chkGST').is(":checked")) {
                            if (GSTFirm != "" && GSTNO != "") {
                                document.getElementById("busy-holder1").style.display = "";
                                document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
                            }
                            else {

                            }
                        }
                        else {
                            document.getElementById("busy-holder1").style.display = "";
                            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
                        }
                    }
                }
                else {
                    if ($('#ContentPlaceHolder1_chkGST').is(":checked")) {
                        if (GSTFirm != "" && GSTNO != "") {
                            document.getElementById("busy-holder1").style.display = "";
                            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
                        }
                        else {

                        }
                    }
                    else {
                        document.getElementById("busy-holder1").style.display = "";
                        document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
                    }
                }
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <rpt:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Visible="false"></rpt:ReportViewer>

    <%--<asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <Triggers>--%>
    <%--<asp:PostBackTrigger ControlID="txtComplaintNo" />
            <asp:PostBackTrigger ControlID="txtLabourCost" />
            <asp:PostBackTrigger ControlID="txtOtherCost" />
            <asp:PostBackTrigger ControlID="txtPartCost" />
            <asp:PostBackTrigger ControlID="ddlPayMode" />--%>

    <%--<asp:AsyncPostBackTrigger ControlID="txtComplaintNo" EventName="TextChanged" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="txtLabourCost" EventName="TextChanged" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="txtOtherCost" EventName="TextChanged" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="txtPartCost" EventName="TextChanged" />--%>
    <%--<asp:AsyncPostBackTrigger ControlID="ddlPayMode" EventName="SelectedIndexChanged" />--%>
    <%--<asp:PostBackTrigger ControlID="fuImgDoc" />--%>
    <%--</Triggers> 
        <ContentTemplate>--%>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Samsung TCR</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Cash Receipt No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtRcptno" runat="server" CssClass="form-control required_text_box" placeholder="Cash Receipt No." Enabled="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvRcptNo" runat="server" ControlToValidate="txtRcptno" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Cash Receipt No.">Please Enter Cash Receipt No.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Service Order No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtComplaintNo" runat="server" CssClass="form-control required_text_box" placeholder="Complaint No." OnTextChanged="txtComplaintNo_TextChanged" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                        <asp:Label ID="lblAMC" runat="server" Visible="false"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="rfvComplaitNo" runat="server" ControlToValidate="txtComplaintNo" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Complaint No.">Please Enter Complaint No.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Customer Name : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control required_text_box" placeholder="Customer Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCustname" runat="server" ControlToValidate="txtCustName" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Name">Please Enter Customer Name</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Address : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control required_text_box" placeholder="Customer Address" TextMode="MultiLine"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Address">Please Enter Customer Address</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Mobile No. (WhatsApp) : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control required_text_box" placeholder="Customer Mobile No."></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ControlToValidate="txtMobileNo" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Mobile No.">Please Enter Customer Mobile No.</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revMobileNo" runat="server" ControlToValidate="txtMobileNo"
                                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Mobile No." ValidationExpression="[0-9]{10}$">Invalid Mobile No.</asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Other Contact No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtOthrContact" runat="server" CssClass="form-control" placeholder="Customer Other Contact No."></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvOthrContact" runat="server" ControlToValidate="txtOthrContact" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Other Contact No.">Please Enter Customer Other Contact No.</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Email ID : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" placeholder="Customer Email ID"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvEmailid" runat="server" ControlToValidate="txtEmailID" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Email ID">Please Enter Email ID</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Model No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtModelNo" runat="server" CssClass="form-control" placeholder="Model No."></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvOthrContact" runat="server" ControlToValidate="txtOthrContact" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Other Contact No.">Please Enter Customer Other Contact No.</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Serial No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control" placeholder="Model No."></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvOthrContact" runat="server" ControlToValidate="txtOthrContact" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Other Contact No.">Please Enter Customer Other Contact No.</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <%--<div class="col-md-12">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Labour Cost : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtLabourCost" Text="0" runat="server" CssClass="form-control required_text_box" placeholder="Labour Cost" OnTextChanged="txtLabourCost_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvLabuorCost" runat="server" ControlToValidate="txtLabourCost" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Labour Cost">Please Enter Labour Cost</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                        <%--<div class="col-md-12">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Other Cost : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtOtherCost" Text="0" runat="server" CssClass="form-control" placeholder="Other Cost" OnTextChanged="txtLabourCost_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvOtherCost" runat="server" ControlToValidate="txtOtherCost" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Other Cost">Please Enter Other Cost</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                        <%--<div class="col-md-12">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Part Name : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtPartname" runat="server" CssClass="form-control required_text_box" placeholder="Replaced Part Name"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvpartName" runat="server" ControlToValidate="txtPartname" ValidationGroup="SavePart" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Replaced Part Name">Please Enter Replaced Part Name</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                        <%--<div class="col-md-12">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Part Cost : </label>
                                                            <div class="col-md-5 col-xs-12">
                                                                <asp:TextBox ID="txtPartCost" Text="0" runat="server" CssClass="form-control required_text_box" placeholder="Replaced Part Cost"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPartCost" runat="server" ControlToValidate="txtPartCost" ValidationGroup="SavePart" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Replaced Part Cost">Please Enter Replaced Part Cost</asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="col-md-2 col-xs-12">
                                                                <asp:Button ID="btnAdd" runat="server" CssClass="form-control" Text="Add" OnClick="btnAdd_Click" ValidationGroup="SavePart" />
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>--%>

                                        <%--<div class="col-md-12">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label"></label>
                                                            <div class="col-md-5 col-xs-12">
                                                                <asp:GridView ID="gvPartList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                                    EmptyDataText="No records has been added.">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="PartName" HeaderText="Part Name" />
                                                                        <asp:BoundField DataField="PartCost" HeaderText="Part Cost" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Total Cost : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtTotalCost" runat="server" CssClass="form-control required_text_box" placeholder="Total Cost" TextMode="Number"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvTotalCost" runat="server" ControlToValidate="txtTotalCost" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Total Cost">Please Enter Total Cost</asp:RequiredFieldValidator>
                                                        <%--<asp:RegularExpressionValidator ID="revTotalCos" runat="server" ControlToValidate="txtTotalCost"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Amount" ValidationExpression="[0-9]{10}$">Invalid Amount</asp:RegularExpressionValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">GST : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:CheckBox ID="chkGST" runat="server" CssClass="form-control" AutoPostBack="true" OnCheckedChanged="chkGST_CheckedChanged" />
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTotalCost" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Total Cost">Please Enter Total Cost</asp:RequiredFieldValidator>--%>
                                                        <%--<asp:RegularExpressionValidator ID="revTotalCos" runat="server" ControlToValidate="txtTotalCost"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Amount" ValidationExpression="[0-9]{10}$">Invalid Amount</asp:RegularExpressionValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" id="divGSTNO" runat="server" visible="false">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">GST No : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtGSTNO" runat="server" CssClass="form-control" placeholder="GST No."></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvGSTNo" runat="server" ControlToValidate="txtGSTNO" ValidationGroup="SaveAll" Style="color: red;"
                                                            Enabled="false" Display="Dynamic" ErrorMessage="Please Enter GST No.">Please Enter GST No</asp:RequiredFieldValidator>
                                                        <%--<asp:RegularExpressionValidator ID="revTotalCos" runat="server" ControlToValidate="txtTotalCost"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Amount" ValidationExpression="[0-9]{10}$">Invalid Amount</asp:RegularExpressionValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" id="divGSTName" runat="server" visible="false">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Firm/Person Name : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtGSTFirm" runat="server" CssClass="form-control" placeholder="GST Firm or Person Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvGSTFirm" runat="server" ControlToValidate="txtGSTFirm" ValidationGroup="SaveAll" Style="color: red;"
                                                            Enabled="false" Display="Dynamic" ErrorMessage="Please Enter GST Firm/Person Name">Please Enter GST Firm/Person Name</asp:RequiredFieldValidator>
                                                        <%--<asp:RegularExpressionValidator ID="revTotalCos" runat="server" ControlToValidate="txtTotalCost"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Amount" ValidationExpression="[0-9]{10}$">Invalid Amount</asp:RegularExpressionValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <asp:UpdatePanel ID="updPaymode" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <%--<asp:AsyncPostBackTrigger ControlID="ddlPayMode" EventName="SelectedIndexChanged" />--%>
                                                <asp:PostBackTrigger ControlID="ddlPayMode" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <div class="col-md-12">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Payment Mode : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="form-control required_text_box" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                <%--<asp:DropDownList ID="ddlPayMode" runat="server" CssClass="form-control required_text_box"></asp:DropDownList>--%>
                                                                <asp:RequiredFieldValidator ID="rfvPayMode" runat="server" ControlToValidate="ddlPayMode" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Select Paymode" InitialValue="0">Please Select Paymode</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>


                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Transaction ID : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtTransactionID" runat="server" CssClass="form-control required_text_box" placeholder="Transaction ID"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvTransactionID" runat="server" ControlToValidate="txtTransactionID" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Transaction ID">Please Enter Transaction ID</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Image Upload : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:FileUpload ID="fuImgDoc" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                                        <asp:RequiredFieldValidator ID="rfvImgUpload" Style="color: red; visibility: visible !important;" runat="server" ControlToValidate="fuImgDoc" ValidationGroup="SaveAll" Display="Dynamic"
                                                            ErrorMessage="Please Upload Image" Enabled="false">
                                                            Please Upload Image</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revImgUpload" runat="server" ErrorMessage="Only Image files are allowed" Style="color: red;"
                                                            ControlToValidate="fuImgDoc" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG|.png|.PNG|.jpeg|.JPEG|.pdf|.PDF)$">
                                                        </asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-9">
                                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/Samsung/frmViewComplaint.aspx" Text="Cancel" TabIndex="16"><i class="fa fa-times"></i> Cancel</asp:LinkButton>
                                            <asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll" TabIndex="15"><i class="fa fa-save"></i> Save</asp:LinkButton>
                                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-left">
                                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                                <label>Please wait...</label>
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

    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>QR Code for Payment</strong> </h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <%--<div class="row" id="div1001" runat="server" visible="false">
                            <center>
                                <asp:Image ID="imgQRCode1001" runat="server" ImageUrl="~/Samsung/QTEKPAYMENT.png" AlternateText="QR Code" Height="330" Width="330" />
                            </center>
                        </div>

                        <div class="row" id="div1014" runat="server" visible="false">
                            <center>
                                <asp:Image ID="imgQRCode1014" runat="server" ImageUrl="~/Samsung/QTEKPAYMENTBARODA.png" AlternateText="QR Code" Height="330" Width="330" />
                            </center>
                        </div>

                        <div class="row" id="div1007" runat="server" visible="false">
                            <center>
                                <asp:Image ID="imgQRCode1007" runat="server" ImageUrl="~/Samsung/QTEKPAYMENTRAJKOT.png" AlternateText="QR Code" Height="330" Width="330" />
                            </center>
                        </div>--%>
                        <div class="row">
                            <center>
                                <asp:Image ID="imgQRCode" runat="server" ImageUrl="" AlternateText="QR Code" Height="330" Width="330" />
                            </center>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranSamsungTCR" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSAMSUNG" runat="server" />

</asp:Content>
