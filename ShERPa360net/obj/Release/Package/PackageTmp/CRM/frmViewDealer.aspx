<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmViewDealer.aspx.cs" Inherits="ShERPa360net.CRM.frmViewDealer" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Dealer Master</title>

    <style type="text/css">
        .margin-top {
            margin-top: 25px;
        }

        .new {
            height: 100px;
            width: 100px;
        }

        .col-md-12 .margin-bottom img {
            margin: 20px;
        }

        .red {
            background: none;
            color: red;
            border: none;
        }

        .zoom:hover {
            margin-top: 50px !important;
            -ms-transform: scale(12); /* IE 9 */
            -webkit-transform: scale(12); /* Safari 3-8 */
            transform: scale(12);
            transform-origin: 50px;
            z-index: 100;
        }
    </style>
    


    <%--  <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />--%>


    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
            //$("#myInput").on("keyup", function () {
            //    var value = $(this).val().toLowerCase();
            //    $("#ContentPlaceHolder1_gvList tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {
            if ($("#ContentPlaceHolder1_gvList tr").length > 2) {
                $("#ContentPlaceHolder1_gvList").DataTable({
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
            }
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Dealer List</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date From : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Dealer Name : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtDealerName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Category : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12" style="padding-top: 10px;">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkStatus" runat="server" CssClass="form-control" Checked="true" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label"></label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:LinkButton runat="server" ID="lnkNewDealer" CssClass="btn btn-success pull-left" Text="New Dealer" PostBackUrl="~/CRM/frmDealer.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success " Text="Search Invoice" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success" Text="Export " OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="mobile_View">
                                    <div class="col-md-1" style="margin-top: 5px;">&nbsp;</div>
                                    <div class="col-md-3" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-9 col-xs-12">
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
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap" Width="100%"
                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowDataBound="gvList_RowDataBound">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                
                                                <asp:TemplateField HeaderText="Contact No">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtcontactno" runat="server" Text='<%# Eval("CONTACTNO") %>'></asp:TextBox>
                                                        <asp:Label ID="lblcontactnoalert" runat="server"  Style="color: red!important; font-weight: bold!important;margin-left:21px!important; display: none">Please Enter Contact No</asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Contact No2">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtcontactno2" runat="server" Text='<%# Eval("CONTACTNO2") %>'></asp:TextBox>
                                                        <asp:Label ID="lblcontactno2alert" runat="server"  Style="color: red!important; font-weight: bold!important;margin-left:21px!important; display: none">Please Enter Contact No2</asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                 <asp:TemplateField HeaderText="Contact No3">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtcontactno3" runat="server" Text='<%# Eval("CONTACTNO3") %>'></asp:TextBox>
                                                        <asp:Label ID="lblcontactno3alert" runat="server"  Style="color: red!important; font-weight: bold!important;margin-left:21px!important; display: none">Please Enter Contact No3</asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                               <%-- <asp:BoundField DataField="CONTACTNO" HeaderText="Contact No" />--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HiddenField runat="server" ID="HDID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                        <asp:LinkButton Style="margin-top: 5px!important; margin-left: 38px!important;" runat="server" ID="btnUpdate" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="DEALERNAME" HeaderText="Dealer Name" />
                                                <asp:BoundField DataField="CATEGORY" HeaderText="Category" />
                                                <asp:BoundField DataField="ADDR1" HeaderText="Address 1" />
                                                <asp:BoundField DataField="ADDR2" HeaderText="Address 2" />
                                                <asp:BoundField DataField="ADDR3" HeaderText="Address 3" />
                                                <asp:BoundField DataField="AREA" HeaderText="Area" />
                                                <asp:BoundField DataField="POSTALCODE" HeaderText="Postal Code" />
                                                <asp:BoundField DataField="CITY" HeaderText="City" />
                                                <asp:BoundField DataField="STATE" HeaderText="State" />
                                                <asp:BoundField DataField="COUNTRY" HeaderText="Country" />
                                                <asp:BoundField DataField="BIKERNAME" HeaderText="Biker Name" />
                                                <asp:BoundField DataField="ASM" HeaderText="ASM Name" />
                                                <asp:BoundField DataField="CREATEBY" HeaderText="Create By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                <asp:BoundField DataField="UPDATEBY" HeaderText="Update By" />
                                                <asp:BoundField DataField="UPDATEDATE" HeaderText="Update Date" />
                                                <asp:BoundField DataField="ID" HeaderText="ID"/>
                                                <asp:TemplateField HeaderText="Image" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblImageData" runat="server" Text='<%# Eval("SHOPIMAGE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Image">
                                                    <ItemTemplate>
                                                        <asp:Image ID="imgImage" runat="server" Height="50" Width="50" CssClass="zoom" AlternateText='<%# Eval("SHOPIMAGE") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CONTACTNO" HeaderText="Contact No"/>
                                                <asp:BoundField DataField="CONTACTNO2" HeaderText="Contact No2"/>
                                                <asp:BoundField DataField="CONTACTNO3" HeaderText="Contact No3"/>
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

    <%--For Data Table Jquery--%>
    <%-- <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://editor.datatables.net/extensions/Editor/js/dataTables.editor.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/select/1.3.1/js/dataTables.select.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/colreorder/1.5.2/js/dataTables.colReorder.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.print.min.js"></script>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmMstCRMDlrMst" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmMstCRM" runat="server" />
    <input type="hidden" id="menucontactno" value="rptDealerContactNoupdate" runat="server" />
</asp:Content>
