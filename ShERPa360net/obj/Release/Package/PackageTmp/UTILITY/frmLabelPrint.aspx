<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmLabelPrint.aspx.cs" Inherits="ShERPa360net.UTILITY.frmLabelPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>MRP Label Print</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;MRP Label Print</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">

                                <div class="col-md-12">


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Segment : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSegment_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Job No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtJobid" runat="server" CssClass="form-control" placeholder="Job ID"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Serial No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control" placeholder="Serial No."></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label"></label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
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
                                            AutoGenerateColumns="false" ShowHeaderWhenEmpty="true">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="MODEL" HeaderText="Modele" />
                                                <asp:BoundField DataField="COLOR" HeaderText="Color" />
                                                <asp:BoundField DataField="DISPLAYSIZE" HeaderText="Disp. Size" />
                                                <asp:BoundField DataField="RAM" HeaderText="RAM" />
                                                <asp:BoundField DataField="ROM" HeaderText="ROM" />
                                                <asp:BoundField DataField="REAR_CAMERA" HeaderText="Rear Camera" />
                                                <asp:BoundField DataField="FRONT_CAMERA" HeaderText="Front Camera" />
                                                <asp:BoundField DataField="VOLTE4G" HeaderText="Volt." />
                                                <asp:BoundField DataField="SERIALNO" HeaderText="Serial No." />
                                                <asp:BoundField DataField="MRP" HeaderText="MRP" />

                                                <asp:TemplateField HeaderText="Print" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnPrint" Text="Print" OnClick="btnPrint_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                <asp:BoundField DataField="JWREFNO" HeaderText="JW Ref." />
                                                <asp:BoundField DataField="JWREFNO2" HeaderText="JW Ref. 2" />
                                                <asp:BoundField DataField="JWREFNO3" HeaderText="JW Ref. 3" />
                                                <asp:BoundField DataField="JWREFNO4" HeaderText="JW Ref. 4" />

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



    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Refurb </strong>Label Print</h4>
                </div>
                <div class="modal-body">

                    <div class="box-body">
                        <div class="row">

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Job id :</label>
                                    <asp:Label ID="lblPopJobid" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Sales From :</label>
                                    <asp:DropDownList ID="ddlPopSalesFrom" runat="server" CssClass="form-control"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvPopSales" runat="server" ControlToValidate="ddlPopSalesFrom" ForeColor="Red" Display="Dynamic"
                                        InitialValue="0" ErrorMessage="Select Sales From" ValidationGroup="PopValidate">Select Sales From</asp:RequiredFieldValidator>
                                </div>
                            </div>

                            <div class="col-md-3" style="margin-top: 22px;">
                                <div class="form-group">
                                    <asp:LinkButton runat="server" ID="lnkPrintLabel" CssClass="btn btn-success pull-left" Text="PrintR" ValidationGroup="PopValidate" OnClick="lnkPrintLabel_Click" Visible="true"><i class="fa fa-check-circle"></i> Print</asp:LinkButton>

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

    <input type="hidden" id="menutabid" value="tsmRptSDBlynkBoxLabelData" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSD" runat="server" />

</asp:Content>
