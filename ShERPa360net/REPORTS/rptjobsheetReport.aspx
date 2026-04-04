<%@ Page Title="JobSheet Report" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptjobsheetReport.aspx.cs" Inherits="ShERPa360net.REPORTS.rptjobsheetReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>JobSheet Report</title>

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
            //    $("#ContentPlaceHolder1_gvList tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {

            $("#ContentPlaceHolder1_gvList").DataTable({
                dom: 'Bfrtip',
                paging: false,
                sorting: false,
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View Jobsheet Report </strong></h3>
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-2" id="divfromdate" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date From : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2" id="divtodate" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10" Enabled="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Segment : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSegment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Plant : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" OnClick="lnkSearh_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span>   </asp:LinkButton>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Jobsheet Report Summary</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowCommand="gvList_RowCommand">
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                                List is empty !
                                                            </div>
                                                        </EmptyDataTemplate>

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Status ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTATUSID" runat="server" Text='<%# Eval("STATUS ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="<=3 Days">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblLESSTHANTHREEDAYS" runat="server" Text='<%# Eval("LESS THAN THREE DAYS") %>' CommandName="LESSTHANTHREEDAYS"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=">3 and <=7 Days">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblTHREETOSEVENDAYS" runat="server" Text='<%# Eval("THREE TO SEVEN DAYS") %>' CommandName="THREETOSEVENDAYS"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=">7 and <=15 Days">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblSEVENTOFIFTEENDAYS" runat="server" Text='<%# Eval("SEVEN TO FIFTEEN DAYS") %>' CommandName="SEVENTOFIFTEENDAYS"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=">15 and <=30 Days">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblFIFTEENTOTHIRTYDAYS" runat="server" Text='<%# Eval("FIFTEEN TO THIRTY DAYS") %>' CommandName="FIFTEENTOTHIRTYDAYS"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=">30 and <=60 Days">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblTHIRTYTOSIXTYDAYS" runat="server" Text='<%# Eval("THIRTY TO SIXTY DAYS") %>' CommandName="THIRTYTOSIXTYDAYS"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText=">60 Days">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblSIXTY" runat="server" Text='<%# Eval("SIXTY") %>' CommandName="GREATERTHANSIXTY"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Total">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblTOTALJOBCOUNT" runat="server" Text='<%# Eval("TOTAL JOB COUNT") %>' CommandName="TOTALJOBCOUNT"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
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



    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-square-o"></span>&nbsp;Status Detail</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-3">
                                                <dl>
                                                    <dt>
                                                        <h4>Cancelled</h4>
                                                    </dt>
                                                    <dd>- Cancelled</dd>
                                                </dl>

                                                <dl>
                                                    <dt>
                                                        <h4>Closed</h4>
                                                    </dt>
                                                    <dd>- Closed</dd>
                                                </dl>

                                                <dl>
                                                    <dt>
                                                        <h4>RFD</h4>
                                                    </dt>
                                                    <dd>- Phy. Doc. Var.</dd>
                                                </dl>
                                                <dl>
                                                    <dt>
                                                        <h4>Dispatched</h4>
                                                    </dt>
                                                    <dd>- Dispatched</dd>
                                                    <dd>- Forward Waybill Generated</dd>
                                                    <dd>- Forward Documents Generated</dd>
                                                    <dd>- Dispatch Email Sent</dd>
                                                </dl>

                                            </div>

                                            <div class="col-md-3">
                                                <dl>
                                                    <dt>
                                                        <h4>Saved</h4>
                                                    </dt>
                                                    <dd>- Saved</dd>
                                                    <dd>- Docs. Generated</dd>
                                                    <dd>- Docs. Emailed</dd>
                                                    <dd>- Docs. Received</dd>
                                                    <dd>- Docs. Verified</dd>
                                                    <dd>- Waiting for Store Pickup Confirmation</dd>
                                                    <dd>- Ready for Pickup</dd>
                                                    <dd>- Reverse Waybill Generated</dd>
                                                    <dd>- Reverse Documents Generated</dd>
                                                    <dd>- Waiting for Pickup</dd>
                                                    <dd>- Product Received</dd>
                                                    <dd>- Product Verified</dd>
                                                    <dd>- Product Picked Up</dd>
                                                    <dd>- Reverse Documents Sent</dd>
                                                </dl>
                                            </div>

                                            <div class="col-md-3">
                                                <dl>
                                                    <dt>
                                                        <h4>Under Production</h4>
                                                    </dt>
                                                    <dd>- JobCard Created</dd>
                                                    <dd>- JobCard Printed</dd>
                                                    <dd>- Under Production</dd>
                                                    <dd>- Estimated</dd>
                                                    <dd>- Waiting for Approval</dd>
                                                    <dd>- Approved</dd>
                                                    <dd>- Partially Approved</dd>
                                                    <dd>- Escalated for Docs.</dd>
                                                    <dd>- Escalated twice for Docs.</dd>
                                                    <dd>- Escalated for Packing</dd>
                                                    <dd>- Escalated twice for Packing</dd>
                                                </dl>
                                            </div>

                                            <div class="col-md-3">
                                                <dl>
                                                    <dt>
                                                        <h4>ASC/STO-SEND FOR SALES</h4>
                                                    </dt>
                                                    <dd>- At ASC</dd>
                                                    <dd>- Received from ASC</dd>
                                                    <dd>- Receipt Pending</dd>
                                                    <dd>- In Transit from ASC</dd>
                                                    <dd>- Send for Sales</dd>
                                                    <dd>- Received from Sales</dd>
                                                    <dd>- STO Sent</dd>
                                                    <dd>- STO Received</dd>
                                                </dl>

                                                
                                                <dl>
                                                    <dt>
                                                        <h4>PNA</h4>
                                                    </dt>
                                                    <dd>- PNA</dd>
                                                </dl>

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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptJobsheetCount" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptCRM" runat="server" />

</asp:Content>
