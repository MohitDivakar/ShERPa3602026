<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="frmCustomerList.aspx.cs" Inherits="ShERPa360net.SD.frmCustomerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>
    <script>
        $(document).ready(function () {
            $(".ddlCity").select2();
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Customers</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Customer Code: </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Customer Type : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <%--<asp:TextBox ID="txtDocType" runat="server" CssClass="form-control" Text="103" Enabled="false"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="form-control" Width="205" AutoPostBack="false"></asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Name : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" Width="205"></asp:TextBox>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">City : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control ddlCity"></asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12 TopMarg">


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Postal Code : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtPostalCode" runat="server" CssClass="form-control" Width="205"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Zone : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlzone" runat="server" placeholder="countries" CssClass="form-control" Width="205" AutoPostBack="false"></asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label"></label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:LinkButton runat="server" ID="lnkAddNewCustomer" CssClass="btn btn-success " Text="Add Customer" PostBackUrl="~/SD/frmCustomerAdd.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success " Text="Search Customer" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success" Text="Export Customer" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="Customer Code" HeaderText="Customer code" />
                                                <asp:BoundField DataField="Type" HeaderText="Type" />
                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                <asp:BoundField DataField="Short Name" HeaderText="Short name" />
                                                <asp:BoundField DataField="ZONE" HeaderText="Zone" />
                                                <asp:BoundField DataField="REGION" HeaderText="Region" />
                                                <asp:BoundField DataField="STATE" HeaderText="State" />
                                                <asp:BoundField DataField="CITY" HeaderText="City" />
                                                <asp:BoundField DataField="POSTALCODE" HeaderText="Postal code" />
                                                <%--      <asp:BoundField DataField="EMAILID" HeaderText="Email" />--%>
                                                <asp:BoundField DataField="STORECONTACT" HeaderText="Store contact" />
                                                <asp:BoundField DataField="STOREMOBILE" HeaderText="Store mobile" />
                                                <%--       <asp:BoundField DataField="AOM" HeaderText="AOM" />
                                                <asp:BoundField DataField="AOM_CONTACTNO" HeaderText="AOM Contact no" />
                                                <asp:BoundField DataField="AOM_EMAILID" HeaderText="AOM Email" />--%>
                                                <asp:BoundField DataField="CreatedBy" HeaderText="Created by" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Created date" />
                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                        | 
                                                        <asp:LinkButton runat="server" ID="btnImage" Text="Image" OnClick="btnImage_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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
        <div class="modal-dialog modal-lg top-0">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Customer</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">

                            <%--<div class="page-content-wrap">--%>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-horizontal">
                                        <div class="panel panel-default">
                                            <div class="panel-heading">
                                                <%-- <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Create  </strong>Customer</h3>
                                    <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewPO.aspx" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" ValidationGroup="SaveAll"  runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All"><i class="fa fa-save"></i></asp:LinkButton>
                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>--%>
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <h4 style="color: #f05423">Customer Details</h4>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-4 control-label">Customer Type: </label>
                                                                        <div class="col-md-8 col-xs-12">
                                                                            <asp:Label ID="lblCustomerType" runat="server" CssClass="form-control"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-4 control-label">Customer Code: </label>
                                                                        <div class="col-md-8 col-xs-12">
                                                                            <asp:Label ID="lblCustomerCode" runat="server" CssClass="form-control"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-4 control-label">Category Type: </label>
                                                                        <div class="col-md-8 col-xs-12">
                                                                            <asp:Label ID="lblCategoryType" runat="server" CssClass="form-control"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-4 control-label">Title: </label>
                                                                        <div class="col-md-8 col-xs-12">
                                                                            <asp:Label ID="lblTitle" runat="server" CssClass="form-control"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-4 control-label">Name: </label>
                                                                        <div class="col-md-8 col-xs-12">
                                                                            <asp:Label ID="lblName" runat="server" CssClass="form-control"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-4 control-label">Name2: </label>
                                                                        <div class="col-md-8 col-xs-12">
                                                                            <asp:Label ID="lblName2" runat="server" CssClass="form-control"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-4 control-label">Old Customer Code: </label>
                                                                        <div class="col-md-8 col-xs-12">
                                                                            <asp:Label ID="lblOldCustomerCode" runat="server" CssClass="form-control"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-4 control-label">Short Name: </label>
                                                                        <div class="col-md-8 col-xs-12">
                                                                            <asp:Label ID="lblShortName" runat="server" CssClass="form-control"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-4 control-label">Customer Group: </label>
                                                                        <div class="col-md-8 col-xs-12">
                                                                            <asp:Label ID="lblCustomerGroup" runat="server" CssClass="form-control"></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="page-content-wrap">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="panel panel-default tabs">
                                                                    <ul class="nav nav-tabs" role="tablist">
                                                                        <li class="active" id="tabfirstli"><a href="#tabfirst" role="tab" data-toggle="tab">Address</a></li>
                                                                        <li id="tabsecondli"><a href="#tabsecond" role="tab" data-toggle="tab">Communication</a></li>
                                                                        <li><a href="#tabthird" role="tab" data-toggle="tab">Taxation</a></li>
                                                                        <li><a href="#tabfourth" role="tab" data-toggle="tab">Bank Details</a></li>
                                                                    </ul>
                                                                    <div class="panel-body tab-content">
                                                                        <div class="tab-pane active" id="tabfirst">
                                                                            <div class="panel-body">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-12">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-2 control-label">Address: </label>
                                                                                                    <div class="col-md-10 col-xs-12">
                                                                                                        <asp:Label ID="lblAddress1" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-12">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-2 control-label"></label>
                                                                                                    <div class="col-md-10 col-xs-12">
                                                                                                        <asp:Label ID="lblAddress2" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-12">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-2 control-label"></label>
                                                                                                    <div class="col-md-10 col-xs-12">
                                                                                                        <asp:Label ID="lblAddress3" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-12" style="margin-top: 10px;">
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">Country</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblCountry" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">State</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblState" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-12" style="margin-top: 0px;">
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">City</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblCity" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">Pincode</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblPincode" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-12" style="margin-top: 0px;">
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">Mobile</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblMobile" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">Email</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblEmail" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-12" style="margin-top: 0px;">
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">Contact Person</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblContactPerson" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">Contact No</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblContactNo" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-12" style="margin-top: 0px;">
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">Zone</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblZone" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">Region</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblRegion" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <div class="col-md-12" style="margin-top: 0px;">
                                                                                            <div class="col-md-6">
                                                                                                <div class="form-group">
                                                                                                    <label class="col-md-4 control-label">Website</label>
                                                                                                    <div class="col-md-8 col-xs-12">
                                                                                                        <asp:Label ID="lblWebsite" runat="server" CssClass="form-control"></asp:Label>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="tab-pane" id="tabsecond">
                                                                            <div class="page-content-wrap">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="panel panel-default">
                                                                                            <div class="panel-body">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                                                                                        <div class="box">
                                                                                                            <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                                                                                                <asp:HiddenField runat="server" ID="hdISEdit" Value="false"></asp:HiddenField>
                                                                                                                <asp:GridView ID="grvCommunication" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                                                                    <%-- OnRowCommand="OnRowCommand" --%>
                                                                                                                    <Columns>
                                                                                                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:HiddenField runat="server" ID="hdngrdID" Value='<%# Bind("ID") %>'></asp:HiddenField>

                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>

                                                                                                                        <asp:TemplateField HeaderText="Designation">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label runat="server" ID="lblgrddesignation" Text='<%# Bind("DESIGNATIONVALUE") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Contact name">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label runat="server" ID="lblgrdname" Text='<%# Bind("CONTNAME") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Contact No">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label runat="server" ID="lblgrdcontactno" Text='<%# Bind("CONTNO") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <asp:TemplateField HeaderText="Email">
                                                                                                                            <ItemTemplate>
                                                                                                                                <asp:Label runat="server" ID="lblgrdEmail" Text='<%# Bind("EMAILID") %>'></asp:Label>
                                                                                                                            </ItemTemplate>
                                                                                                                        </asp:TemplateField>
                                                                                                                        <%-- <asp:TemplateField HeaderText="Action">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                                                        CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
                                                                                                                    |
                                                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                                                        CommandName="eEdit">Edit</asp:LinkButton>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>--%>
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
                                                                        </div>
                                                                        <div class="tab-pane" id="tabthird">
                                                                            <div class="panel-body">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <h5 style="color: orangered">Taxation</h5>
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">CST No.:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblCSTNo" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Date</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblCSTDate" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">LST/CST No.:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblLSTNo" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Date</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblLSTDate" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">PAN card No.:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblPanCardNo" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Adhar card No.:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblAdharNo" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Service Tax Registeration no:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblService" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Gst no:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblGstNo" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                        <h5 style="color: orangered">Excise</h5>
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">ECC No.:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblECCNo" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Excise Registeration No:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblExciseRegisterNo" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Excise Range:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblExciseRange" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Excise Division:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblExciseDivision" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Excise Commisionrate:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblExciseComm" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Excise vendor Type:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblExciseVend" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="tab-pane " id="tabfourth">
                                                                            <div class="panel-body">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="col-md-12">
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Bank Name:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblBankName" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>

                                                                                            </div>
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Account No:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblAccountNo" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">Account Type:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblAccountType" runat="server" CssClass="form-control"></asp:Label>
                                                                                                        </div>
                                                                                                    </div>
                                                                                                </div>
                                                                                            </div>
                                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                                <div class="col-md-6">
                                                                                                    <div class="form-group">
                                                                                                        <label class="col-md-4 control-label">ISFC Code:</label>
                                                                                                        <div class="col-md-8 col-xs-12">
                                                                                                            <asp:Label ID="lblISFCCode" runat="server" CssClass="form-control"></asp:Label>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--</div>--%>



                            <div id="dialog" style="display: none"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="modal-image" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Customer</strong> Document Images</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-6">
                                    <asp:HiddenField ID="hfcustomercode" runat="server" />
                                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                        CssClass="table table-hover table-striped table-bordered nowrap" OnRowDataBound="gvDetail_RowDataBound">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Image Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageType" runat="server" Text='<%# Eval("IMAGETYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageData" runat="server" Text='<%# Eval("IMAGE") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Image Type">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgImage" runat="server" Height="50" Width="50" CssClass="zoom" AlternateText='<%# Eval("IMAGETYPE") %>' Visible="true" />
                                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" Text="Download" Visible="true"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Extension" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageExtension" runat="server" Text='<%# Eval("EXTENSION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
            margin-top: -50px !important;
            -ms-transform: scale(15); /* IE 9 */
            -webkit-transform: scale(15); /* Safari 3-8 */
            transform: scale(15);
            transform-origin: -1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="TSMMSTSDCUST" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmMstSD" runat="server" />
</asp:Content>

