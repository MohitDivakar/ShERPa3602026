<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="ApprovedMR.aspx.cs" Inherits="ShERPa360net.MM.ApprovedMR" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        #ContentPlaceHolder1_gvDetail_Auto1_0_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_1_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_2_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_3_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_4_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_5_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_6_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_7_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_8_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_9_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_10_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_11_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_12_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_13_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_14_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_15_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_16_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_17_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_18_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_19_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_20_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_21_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_22_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_23_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_24_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_25_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_26_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_27_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_28_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_29_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_30_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_31_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_32_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_33_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_34_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_35_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_36_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_37_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_38_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_39_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_40_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_41_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_42_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_43_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_44_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_45_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_46_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_47_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_48_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_49_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_51_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_52_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_53_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_54_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_55_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_56_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_57_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_58_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_59_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_50_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_60_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_61_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_62_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_63_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_64_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_65_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_66_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_67_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_68_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_69_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_70_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_71_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_72_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_73_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_74_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_75_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_76_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_77_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_78_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_79_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_80_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_81_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_82_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_83_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_84_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_85_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_86_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_87_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_88_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_89_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_90_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_91_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_92_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_93_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_94_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_95_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_96_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_97_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_98_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_99_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_100_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_101_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_102_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_103_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_104_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_105_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_106_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_107_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_108_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_109_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_110_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_111_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_112_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_113_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_114_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_115_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_116_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_117_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_118_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_119_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_120_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_121_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_122_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_123_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_124_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_125_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_126_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_127_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_128_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_129_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_130_completionListElem,
        #ContentPlaceHolder1_gvDetail_Auto1_131_completionListElem {
            z-index: 9999 !important;
            width: 500px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <%-- <asp:UpdatePanel ID="updPanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkSearhMR" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="lnkExport" EventName="Click" />
            <asp:PostBackTrigger ControlID="gvList" />
            <asp:PostBackTrigger ControlID="gvDetail" />
        </Triggers>
        <ContentTemplate>--%>



    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Material Requisition</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-3 control-label">MR Date : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">

                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-3 control-label">To : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">



                                            <label class="col-md-4 control-label">MR No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtMrno" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">



                                            <label class="col-md-6 control-label">Plant Code : </label>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control" Width="150" OnSelectedIndexChanged="ddlPlantCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                                </div>
                                            </div>



                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export MR" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSearhMR" CssClass="btn btn-success pull-right" Text="Search MR" OnClick="lnkSearhMR_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>

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
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="MRTYPE" HeaderText="DOC Type" />
                                                <asp:BoundField DataField="MRNO" HeaderText="MR No." />
                                                <asp:BoundField DataField="MRDTD" HeaderText="MR Date" />
                                                <asp:BoundField DataField="DEPTNAME" HeaderText="Department Name" />
                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" />
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                                <asp:BoundField DataField="CREATEBY" HeaderText="Entered By" />
                                                <asp:BoundField DataField="APPROVEDBY" HeaderText="Approved By" />
                                                <asp:BoundField DataField="APRVDATE" HeaderText="Approve Date" />
                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                        <asp:Label ID="lblStick" runat="server" Text="|" Visible="false"></asp:Label>
                                                        <asp:LinkButton runat="server" ID="btnApprove" Text="Close" OnClick="btnApprove_Click" Visible="false"></asp:LinkButton>
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
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <%-- style="width: fit-content !important;">--%>
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>MR</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Doc Type :</label>
                                    <asp:Label ID="lblDoctype" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>MR No. :</label>
                                    <asp:Label ID="lblMRNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>MR Date :</label>
                                    <asp:Label ID="lblMRDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Remark :</label>
                                    <asp:Label ID="lblRemark" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap" OnRowDataBound="gvDetail_RowDataBound"
                                            OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowEditing="gvDetail_RowEditing" OnRowUpdating="gvDetail_RowUpdating">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                                <asp:Label runat="server" ID="lblEditID" Text='<%# Bind("ID") %>'></asp:Label>
                                                            </EditItemTemplate>--%>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="ID" HeaderText="Sr. No." />--%>

                                                <%--<asp:BoundField DataField="ITEMSPEC" HeaderText="Item Spec." />--%>
                                                <asp:TemplateField HeaderText="Item Spec.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMSPEC" Text='<%# Bind("ITEMSPEC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />--%>
                                                <asp:TemplateField HeaderText="Item Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMDESC" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMGROUP" HeaderText="Item Group" />--%>
                                                <asp:TemplateField HeaderText="Item Group">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMGROUP" Text='<%# Bind("ITEMGROUP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMUOM" HeaderText="UOM" />--%>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMUOM" Text='<%# Bind("ITEMUOM") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblUOMID" Text='<%# Bind("UOMID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMQTY" HeaderText="Qty" />--%>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMQTY" Text='<%# Bind("ITEMQTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtItemQty" runat="server" Text='<%# Bind("ITEMQTY") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMRATE" HeaderText="Rate" />--%>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMRATE" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMAMOUNT" HeaderText="Amount" />--%>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMAMOUNT" Text='<%# Bind("ITEMAMOUNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="COSTCENTER" HeaderText="Cost Center" />--%>
                                                <asp:TemplateField HeaderText="Cost Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblCOSTCENTER" Text='<%# Bind("COSTCENTER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMPLANTCD" HeaderText="Plant Code" />--%>
                                                <asp:TemplateField HeaderText="Plant Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMPLANTCD" Text='<%# Bind("ITEMPLANTCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMLOCCD" HeaderText="Location Code" />--%>
                                                <asp:TemplateField HeaderText="Location Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMLOCCD" Text='<%# Bind("ITEMLOCCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="REQUISITIONER" HeaderText="Requisitioner" />--%>
                                                <asp:TemplateField HeaderText="Requisitioner">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblREQUISITIONER" Text='<%# Bind("REQUISITIONER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />--%>
                                                <asp:TemplateField HeaderText="Tracking No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTRACKNO" Text='<%# Bind("TRACKNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remark" />--%>
                                                <asp:TemplateField HeaderText="Item Remark">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMTEXT" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox runat="server" ID="txtItemcode" Text='<%# Bind("ITEMCODE") %>' MaxLength="20" OnTextChanged="txtItemcode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <cc:AutoCompleteExtender ServiceMethod="GetItemCode" MinimumPrefixLength="1" CompletionInterval="10"
                                                            EnableCaching="false" CompletionSetCount="1" TargetControlID="txtItemcode" ID="Auto1" runat="server"
                                                            FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true">
                                                        </cc:AutoCompleteExtender>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <%--AVAILQTY--%>
                                                <asp:TemplateField HeaderText="AVAIL QTY">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAVAILQTY" Text='<%# Bind("AVAILQTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--ISSUEQTY--%>
                                                <asp:TemplateField HeaderText="ISSUE QTY">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblISSUEQTY" Text='<%# Bind("ISSUEQTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--PRNO--%>
                                                <asp:TemplateField HeaderText="PR NO">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPRNO" Text='<%# Bind("PRNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:CommandField ShowEditButton="true" />

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button runat="server" ID="btnIssue" Text="Issue" Visible="false" CssClass="form-control" OnClick="btnIssue_Click" />
                                                        <asp:Button runat="server" ID="btnPR" Text="PR" CssClass="form-control" Visible="false" OnClick="btnPR_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAPRV2" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>




                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>




    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--<div class="message-box animated fadeIn" data-sound="alert" id="mb-aprv">

        <div class="mb-container">

            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: #ffffff;">
                <span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title" style="color: #faa61a;"><strong>MR</strong> Approval</h4>
            <div class="mb-middle">
                <div class="mb-title" style="color: #faa61a"><span class="fa fa-check"></span>Approve <strong>MR</strong> ?</div>
                <div class="mb-content">
                    <h4 style="color: #ffffff;">MR No. : <strong>
                        <asp:Label runat="server" ID="lblPopupMRNO"></asp:Label></strong></h4>
                    <h4 style="color: #ffffff;">Are you sure you want to Approve this MR?</h4>
                </div>

              
            </div>
        </div>
    </div>--%>
    <div class="message-box animated fadeIn" data-sound="alert" id="mb-aprv">

        <div class="modal-dialog">
             <div class="modal-content">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: #ffffff;">
                <span aria-hidden="true">&times;</span></button>

                     <div class="modal-header" >MR Rejection</div>
           
             <div class="modal-body">
               
                <div class="mb-content">
                    <h4  class="display-flex" style="color: #184f90 !important;">MR No. : <strong>
                        <asp:Label runat="server" ID="lblPopupMRNO"></asp:Label></strong></h4>
                    <h4  class="display-flex" style="color: #184f90 !important;">Are you sure you want to Reject this MR?</h4>
                </div>

                <div class="mb-footer display-flex">
                    <div class="pull-right">
                        <asp:TextBox ID="txtAPREJReason" runat="server" CssClass="form-control" placeholder="Reject Reason"></asp:TextBox>
                        <asp:RequiredFieldValidator  ID="rfvVal" runat="server" ControlToValidate="txtAPREJReason" ValidationGroup="ValRej">Enter Reject Reason</asp:RequiredFieldValidator>
                        <br />
                        <asp:LinkButton runat="server" ID="lnkPopReject" style="margin-left: 30%;" CssClass="btn btn-success pull-left" Text="Reject MR" OnClick="lnkPopReject_Click" ValidationGroup="ValRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                        <%--<asp:LinkButton runat="server" ID="lnkPopApprove" CssClass="btn btn-success pull-left" Text="Approve MR" OnClick="lnkPopApprove_Click"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>--%>
                        <asp:Label runat="server" ID="lblRightsMessage" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
                 </div>
        </div>
    </div>


    <div class="message-box animated fadeIn" data-sound="alert" id="mb-close">

        <div class="modal-dialog">
            <div class="modal-content">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: #ffffff;">
                <span aria-hidden="true">&times;</span></button>
                  <div class="modal-header" >MR Short Close</div>
          
              <div class="modal-body">
               
              
                    <h4 class="display-flex" style="color: #184f90 !important;">MR No. : <strong>
                        <asp:Label runat="server" ID="Label1"></asp:Label></strong></h4>
                    <h4 class="display-flex" style="color: #184f90 !important;">Are you sure you want to Close this MR?</h4>
              

                <div class="modal-footer">
                    <div class="pull-right">
                        <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Reject Reason"></asp:TextBox>--%>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAPREJReason" ValidationGroup="ValRej">Enter Reject Reason</asp:RequiredFieldValidator>--%>
                        <br />
                        <asp:LinkButton runat="server" ID="LinkButton1" CssClass="btn btn-success pull-left" Text="Reject MR" OnClick="lnkShortClose_Click"><i class="fa fa-times-circle"></i> Close</asp:LinkButton>
                        <%--<asp:LinkButton runat="server" ID="lnkPopApprove" CssClass="btn btn-success pull-left" Text="Approve MR" OnClick="lnkPopApprove_Click"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>--%>
                        <%--<asp:Label runat="server" ID="Label2" Visible="false"></asp:Label>--%>
                    </div>
                </div>
                </div>
            </div>
        </div>
    </div>
    <div class="message-box animated fadeIn" data-sound="alert" id="mb-close">

        <div class="mb-container">

            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: #ffffff;">
                <span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title" style="color: #faa61a;"><strong>MR</strong> Short Close</h4>
            <div class="mb-middle">
                <div class="mb-title" style="color: #faa61a"><span class="fa fa-check"></span>Close <strong>MR</strong> ?</div>
                <div class="mb-content">
                    <h4 style="color: #ffffff;">MR No. : <strong>
                        <asp:Label runat="server" ID="lblPopupCloseMRNO"></asp:Label></strong></h4>
                    <h4 style="color: #ffffff;">Are you sure you want to Close this MR?</h4>
                </div>

                <div class="mb-footer">
                    <div class="pull-right">
                        <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Reject Reason"></asp:TextBox>--%>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAPREJReason" ValidationGroup="ValRej">Enter Reject Reason</asp:RequiredFieldValidator>--%>
                        <br />
                        <asp:LinkButton runat="server" ID="lnkShortClose" CssClass="btn btn-success pull-left" Text="Reject MR" OnClick="lnkShortClose_Click"><i class="fa fa-times-circle"></i> Close</asp:LinkButton>
                        <%--<asp:LinkButton runat="server" ID="lnkPopApprove" CssClass="btn btn-success pull-left" Text="Approve MR" OnClick="lnkPopApprove_Click"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>--%>
                        <%--<asp:Label runat="server" ID="Label2" Visible="false"></asp:Label>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMPR" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>

