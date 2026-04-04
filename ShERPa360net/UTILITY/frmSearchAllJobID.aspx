<%@ Page Title="Croma Job ID Search" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmSearchAllJobID.aspx.cs" Inherits="ShERPa360net.UTILITY.frmSearchAllJobID" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Croma Job ID Search</title>

    <style type="text/css">
        .rowGreen {
            background-color: lightgreen !important;
            /*color: white !important;*/
            /*background: #00FF00 !important;*/
        }

        .rowYellow {
            background-color: yellow !important;
            /*background: #00FF00 !important;*/
        }

        .rowOrange {
            background-color: orange !important;
            /*background: #00FF00 !important;*/
        }

        .hidden {
            display: none;
        }
    </style>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading2() {
            var txtCustName = $("#ContentPlaceHolder1_txtCustName").val();
            var txtCustMobileNo = $("#ContentPlaceHolder1_txtCustMobileNo").val();
            if (txtCustName != "" && txtCustMobileNo != "") {
                document.getElementById("busy-holder2").style.display = "";
                document.getElementById("ContentPlaceHolder1_btnCreateDoc").style.display = "none";
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Search Product</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">

                                        <div class="col-md-8">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Brand : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-8">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Category : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-8">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Item Desc. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtItemDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="col-md-2">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label"></label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" OnClick="btnSearch_Click" ValidationGroup="Search" />
                                                        <%-- </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label"></label>
                                                    <div class="col-md-7 col-xs-12">--%>
                                                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn btn-success fa fa-refresh" OnClick="btnRefresh_Click" />
                                                        <asp:Button ID="btnNew" runat="server" Text="New Req." CssClass="btn btn-success" OnClick="btnNew_Click" />
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
                                <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <%--<asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />--%>
                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:BoundField DataField="CMPID" HeaderText="CMP ID" Visible="false" />--%>
                                            <asp:TemplateField HeaderText="CMPID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMPID" runat="server" Text='<%# Eval("CMPID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%--<asp:Button ID="btnWhatsapp" runat="server" Text="Send MSG" OnClick="btnWhatsapp_Click" />--%>
                                                    <asp:LinkButton ID="btnWhatsapp" runat="server" Text="Send MSG" OnClick="btnWhatsapp_Click"><i class="fa fa-send"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:BoundField DataField="AVAILSTAT" HeaderText="Status" />--%>
                                            <asp:TemplateField HeaderText="Status" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAVAILSTAT" runat="server" Text='<%# Eval("AVAILSTAT") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%--<asp:BoundField DataField="JOBID" HeaderText="Job ID" />--%>
                                            <asp:TemplateField HeaderText="Job ID">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkJobid" runat="server" Text='<%# Eval("JOBID") %>' OnClick="lnkJobid_Click" Style="color: black; text-decoration-line: underline;"></asp:LinkButton>
                                                    <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:BoundField DataField="CATEGORY" HeaderText="Category" />--%>
                                            <asp:TemplateField HeaderText="Category" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCATEGORY" runat="server" Text='<%# Eval("CATEGORY") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%--<asp:BoundField DataField="BRAND" HeaderText="Brand" />--%>
                                            <asp:TemplateField HeaderText="Brand" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBRAND" runat="server" Text='<%# Eval("BRAND") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%--<asp:BoundField DataField="ITEMCODE" HeaderText="Article Code" />--%>
                                            <asp:TemplateField HeaderText="Article Code" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%--<asp:BoundField DataField="IETMDESC" HeaderText="Item Desc" />--%>
                                            <asp:TemplateField HeaderText="Item Desc" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIETMDESC" runat="server" Text='<%# Eval("IETMDESC") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%--<asp:BoundField DataField="SERIALNO" HeaderText="Serial No." />--%>
                                            <asp:TemplateField HeaderText="Serial No." Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSERIALNO" runat="server" Text='<%# Eval("SERIALNO") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%--<asp:BoundField DataField="GRADE" HeaderText="Grade" />--%>
                                            <asp:TemplateField HeaderText="Grade" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGRADE" runat="server" Text='<%# Eval("GRADE") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%--<asp:BoundField DataField="LOCATION" HeaderText="Plant" />--%>
                                            <asp:TemplateField HeaderText="Plant" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLOCATION" runat="server" Text='<%# Eval("LOCATION") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <%--<asp:BoundField DataField="LOCCD" HeaderText="Location" />--%>
                                            <asp:TemplateField HeaderText="Location" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLOCCD" runat="server" Text='<%# Eval("LOCCD") %>' Visible="true"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Color" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblColor" runat="server" Text='<%# Eval("ROWCOLOR") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Plant" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPLANTCD" runat="server" Text='<%# Eval("PLANTCD") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="URL" Visible="false">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblURL" runat="server" Text='<%# Eval("URL") %>'></asp:Label>--%>
                                                    <asp:HyperLink ID="lblURL" runat="server" Target="_blank" Text="Open Product" NavigateUrl='<%# Eval("URL") %>'></asp:HyperLink>
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


    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Customer</strong> Requirement</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblPopAVAILSTAT" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPopJOBID" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPopCATEGORY" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPopBRAND" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPopITEMCODE" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPopIETMDESC" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPopSERIALNO" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPopGRADE" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPopLOCATION" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPopLOCCD" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPopPLANTCD" runat="server" Visible="false"></asp:Label>



                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Cust. Name : </label>
                                        <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control" PlaceHolder="Customer Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCustName" runat="server" ControlToValidate="txtCustName" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Enter Customer Name" ValidationGroup="Check">Enter Customer Name</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Mobile No. : </label>
                                        <asp:TextBox ID="txtCustMobileNo" runat="server" CssClass="form-control" PlaceHolder="Customer Mobile No." MaxLength="10"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvCustMobileNo" runat="server" ControlToValidate="txtCustMobileNo" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Enter Customer Mobile No." ValidationGroup="Check">Enter Customer Mobile No.</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Reference By : </label>
                                        <asp:DropDownList ID="ddlReference" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvRef" runat="server" ControlToValidate="ddlReference" ErrorMessage="Select Reference by"
                                            ValidationGroup="Check" InitialValue="0" Display="Dynamic" ForeColor="Red">Select Reference by</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>



                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Button ID="btnCreateDoc" runat="server" OnClientClick="ShowLoading2()" CssClass="btn btn-success" Text="Save" OnClick="btnCreateDoc_Click" ValidationGroup="Check" />
                                        <div id="busy-holder2" style="display: none" class="clearfix inline pull-left">
                                            <img id="img2" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
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

    <asp:UpdatePanel ID="udp1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlNewProduct" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlNewSpecs" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <div class="modal fade" id="modal-New" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7"><strong>Customer</strong> Requirement</h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Cust. Name : </label>
                                                <asp:TextBox ID="txtNewCustName" runat="server" CssClass="form-control" PlaceHolder="Customer Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtNewCustName" runat="server" ControlToValidate="txtNewCustName" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Customer Name" ValidationGroup="NewCheck">Enter Customer Name</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Mobile No. : </label>
                                                <asp:TextBox ID="txtNewContact" runat="server" CssClass="form-control" PlaceHolder="Customer Mobile No." MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtNewContact" runat="server" ControlToValidate="txtNewContact" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Customer Mobile No." ValidationGroup="NewCheck">Enter Customer Mobile No.</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Reference By : </label>
                                                <asp:DropDownList ID="ddlNewReference" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlNewReference" runat="server" ControlToValidate="ddlNewReference" ErrorMessage="Select Reference by"
                                                    ValidationGroup="NewCheck" InitialValue="0" Display="Dynamic" ForeColor="Red">Select Reference by</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Product : </label>
                                                <asp:DropDownList ID="ddlNewProduct" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlNewProduct_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlNewProduct" runat="server" ControlToValidate="ddlNewProduct" ErrorMessage="Select Product"
                                                    ValidationGroup="NewCheck" InitialValue="0" Display="Dynamic" ForeColor="Red">Select Product</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Specification : </label>
                                                <asp:DropDownList ID="ddlNewSpecs" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlNewSpecs_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvddlNewSpecs" runat="server" ControlToValidate="ddlNewSpecs" ErrorMessage="Select Specification"
                                                    ValidationGroup="NewCheck" InitialValue="0" Display="Dynamic" ForeColor="Red">Select Specification</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Specification value : </label>
                                                <asp:DropDownList ID="ddlNewSpecValue" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvddlNewSpecValue" runat="server" ControlToValidate="ddlNewSpecValue" ErrorMessage="Select Specification Value"
                                            ValidationGroup="NewCheck" InitialValue="0" Display="Dynamic" ForeColor="Red">Select Specification Value</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">

                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Make : </label>
                                                <asp:TextBox ID="txtNewMake" runat="server" CssClass="form-control" PlaceHolder="Make"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewCustName" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Enter Customer Name" ValidationGroup="NewCheck">Enter Customer Name</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Model : </label>
                                                <asp:TextBox ID="txtNewModel" runat="server" CssClass="form-control" PlaceHolder="Model"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNewContact" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Enter Customer Mobile No." ValidationGroup="NewCheck">Enter Customer Mobile No.</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Cust. Remarks : </label>
                                                <asp:TextBox ID="txtNewCustRemarks" runat="server" CssClass="form-control" PlaceHolder="Customer Remarks"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNewContact" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Enter Customer Mobile No." ValidationGroup="NewCheck">Enter Customer Mobile No.</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Button ID="btnSaveNew" runat="server" CssClass="btn btn-success" Text="Save" OnClick="btnSaveNew_Click" ValidationGroup="NewCheck" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmCromaJobID" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
