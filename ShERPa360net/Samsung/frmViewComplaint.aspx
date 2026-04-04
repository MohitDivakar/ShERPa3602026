<%@ Page Title="View Complaints" Language="C#" MasterPageFile="~/Samsung/Samsung.Master" AutoEventWireup="true" CodeBehind="frmViewComplaint.aspx.cs" Inherits="ShERPa360net.Samsung.frmViewComplaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View Complaints</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />


    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
            //$("#myInput").on("keyup", function () {
            //    var value = $(this).val().toLowerCase();
            //    $("#ContentPlaceHolder1_grvData tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {
            //debugger
            //if ($("#ContentPlaceHolder1_grvData tr").length > 2) {
            $("#ContentPlaceHolder1_grvData").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            'copy',
                            'excel',
                            'csv',
                            'pdf',
                            'print'
                        ]
                    }
                ]
            });
            //}
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;View Service Order</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Service Order No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtComplaintNo" runat="server" CssClass="form-control required_text_box" placeholder="Service Order No."></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvComplaitNo" runat="server" ControlToValidate="txtComplaintNo" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Complaint No.">Please Enter Complaint No.</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Engg. Name : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlEnggName" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-6">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label"></label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success" Text="Search Invoice" OnClick="lnkSerch_Click" ValidationGroup="SaveAll"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row" style="margin-top: 20px !important;">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="grvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Service Order No.">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkComplaintNo" runat="server" Text='<%#Eval("COMPLAINTNO") %>' OnClick="lnkComplaintNo_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="GP Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGPCODE" runat="server" Text='<%#Eval("GPCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Engg. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENGGNAME" runat="server" Text='<%#Eval("ENGGNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCustname" runat="server" Text='<%#Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Service Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSERVICETYPE" runat="server" Text='<%#Eval("SERVICETYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%#Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mobex AMC">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOBEXAMCDET" runat="server" Text='<%#Eval("MOBEXAMCDET") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Create Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedate" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- <asp:BoundField DataField="COMPLAINTNO" HeaderText="Complaint No." />
                                                <asp:BoundField DataField="MODELNO" HeaderText="Model No." />
                                                <asp:BoundField DataField="SERIALNO" HeaderText="Serial No." />
                                                <asp:BoundField DataField="CUSTNAME" HeaderText="Cust. Name" />
                                                <asp:BoundField DataField="CONTACTNO" HeaderText="Contact No." />
                                                <asp:BoundField DataField="ADDRESS" HeaderText="Address" />--%>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranViewComplaint" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSAMSUNG" runat="server" />

</asp:Content>
