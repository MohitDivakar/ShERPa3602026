<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="AddNewModel.aspx.cs" Inherits="ShERPa360net.UTILITY.AddNewModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Add  </strong>New List</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                         <div class="form-group">
                                            <label class="col-md-4 control-label">Brand Name : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlbrandlist" runat="server" CssClass="form-control " TabIndex="5" DataValueField="BRAND_ID"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="efvDepartment" runat="server" ControlToValidate="ddlbrandlist" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select Brand Name List" InitialValue="0">Please Select Brand Name List</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                             <label class="col-md-3 control-label">Model Name: </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtmodelname" runat="server" CssClass="form-control"></asp:TextBox>
                                             </div>
                                        </div>
                                    </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkNewMR" CssClass="btn btn-success pull-left" Text="New MR" PostBackUrl="CreateModelList.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSearhMR" CssClass="btn btn-success pull-left" Text="Search MR" OnClick="lnkSearhMR_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export MR" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                                        <asp:GridView ID="gvList1" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="BRAND_NAME" HeaderText="Brand Name" />
                                                <asp:BoundField DataField="MODEL_NAME" HeaderText="Modal Name" />
                                                <asp:BoundField DataField="DISP_NAME" HeaderText="Disp_Name" />
                                                <asp:BoundField DataField="DISP_NAME2" HeaderText=" Disp_Name2" />
                                                <asp:BoundField DataField="DISP_NAME3" HeaderText=" Disp_Name3" />
                                                <asp:BoundField DataField="PRODUCT" HeaderText=" Product" />
                                                  <asp:BoundField DataField="ISACTIVE" HeaderText="STATUS" />
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
     <input type="hidden" id="menutabid" value="tsmTranMMMR" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
