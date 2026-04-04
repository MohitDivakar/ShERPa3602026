<%@ Page Title="Upload Listing on Various Platforms" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmListableDataUpload.aspx.cs" Inherits="ShERPa360net.UTILITY.frmListableDataUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Upload Listing on Various Platforms</title>

    <style type="text/css">
        .chclass {
        }

            .chclass label {
                position: relative;
                top: -2px;
                left: -5px;
            }

            .chclass input[type="checkbox"] {
                margin: 0px 10px 0px;
            }
    </style>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Upload Listing on Various Platforms  </strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <asp:UpdatePanel ID="updStock" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkStockTypeAll" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkStockType" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkLocationAll" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkLocation" EventName="SelectedIndexChanged" />
                                            <%--<asp:AsyncPostBackTrigger ControlID="chkCityAll" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkCity" EventName="SelectedIndexChanged" />--%>
                                            <asp:AsyncPostBackTrigger ControlID="chkGradeAll" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkGrade" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Stock Type : </label>
                                                    <div class="col-md-8 col-xs-12" style="overflow-x: scroll; overflow-y: scroll; height: 150px;">
                                                        <%--<div class="input-group">--%>
                                                        <%--<asp:DropDownList ID="ddlStockType" runat="server" CssClass="form-control" TabIndex="1"></asp:DropDownList>--%>

                                                        <%--<asp:ListBox ID="lstStockType" runat="server" CssClass="form-control mandator_select" TabIndex="1" Style="border: outset; height: 100px;"></asp:ListBox>--%>
                                                        <asp:CheckBox ID="chkStockTypeAll" runat="server" CssClass="form-control chclass" OnCheckedChanged="chkStockTypeAll_CheckedChanged" AutoPostBack="true" Text="ALL" />
                                                        <asp:CheckBoxList ID="chkStockType" runat="server" CssClass="form-control chclass" TabIndex="2" Style="height: auto;" OnSelectedIndexChanged="chkStockType_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                                        <%--</div>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <%--</ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkLocationAll" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkLocation" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>--%>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Location (Plant) : </label>
                                                    <div class="col-md-8 col-xs-12" style="overflow-x: scroll; overflow-y: scroll; height: 150px;">
                                                        <div class="input-group">
                                                            <%--<asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" TabIndex="2"></asp:DropDownList>--%>
                                                            <%--<asp:ListBox ID="lstLocation" runat="server" SelectionMode="Multiple" CssClass="form-control" TabIndex="2" Style="border: outset; height: 100px;"></asp:ListBox>--%>
                                                            <asp:CheckBox ID="chkLocationAll" runat="server" CssClass="form-control chclass" OnCheckedChanged="chkLocationAll_CheckedChanged" AutoPostBack="true" Text="ALL" />
                                                            <asp:CheckBoxList ID="chkLocation" runat="server" CssClass="form-control chclass" TabIndex="2" Style="height: auto;" OnSelectedIndexChanged="chkLocation_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                                            <%--<asp:CheckBoxList ID="chk" runat="server" CssClass="form-control"></asp:CheckBoxList>--%>

                                                            <%--<asp:ListBox ID="lstStudents" CssClass="dropdown dropdown-menu" runat="server" SelectionMode="Multiple">
                                                        <asp:ListItem Text="Nikunj Satasiya" Value="1" />
                                                        <asp:ListItem Text="Ronak Rabadiya" Value="2" />
                                                        <asp:ListItem Text="Hiren Dobariya" Value="3" />
                                                        <asp:ListItem Text="Vivek Ghadiya" Value="4" />
                                                        <asp:ListItem Text="Pratik Pansuriya" Value="5" />
                                                        <asp:ListItem Text="Kishan Patel" Value="6" />
                                                    </asp:ListBox>--%>
                                                            <%--<br />
                                                            <br />
                                                            <br />
                                                            <br />
                                                            <br />--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <%-- </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkCityAll" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkCity" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>--%>
                                            <%--<div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">City : </label>
                                                    <div class="col-md-9 col-xs-12" style="overflow-x: scroll; overflow-y: scroll; height: 150px;">
                                                        <div class="input-group">
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" TabIndex="3"></asp:DropDownList>

                                                            <asp:ListBox ID="lstCity" runat="server" CssClass="form-control" SelectionMode="Multiple" TabIndex="3" Style="border: outset; height: 100px;"></asp:ListBox>
                                                            <asp:CheckBox ID="chkCityAll" runat="server" CssClass="form-control chclass" OnCheckedChanged="chkCityAll_CheckedChanged" AutoPostBack="true" Text="ALL" />
                                                            <asp:CheckBoxList ID="chkCity" runat="server" CssClass="form-control chclass" TabIndex="2" Style="height: auto;" OnSelectedIndexChanged="chkCity_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                            <%-- </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chkGradeAll" EventName="CheckedChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="chkGrade" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>--%>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Grade : </label>
                                                    <div class="col-md-8 col-xs-12" style="overflow-x: scroll; overflow-y: scroll; height: 150px;">
                                                        <div class="input-group">
                                                            <%--<asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control" TabIndex="4"></asp:DropDownList>--%>

                                                            <%--<asp:ListBox ID="lstGrade" runat="server" CssClass="form-control" SelectionMode="Multiple" TabIndex="4" Style="border: outset; height: 100px;"></asp:ListBox>--%>
                                                            <asp:CheckBox ID="chkGradeAll" runat="server" CssClass="form-control chclass" OnCheckedChanged="chkGradeAll_CheckedChanged" AutoPostBack="true" Text="ALL" />
                                                            <asp:CheckBoxList ID="chkGrade" runat="server" CssClass="form-control chclass" TabIndex="2" Style="height: auto;" OnSelectedIndexChanged="chkGrade_SelectedIndexChanged" AutoPostBack="true"></asp:CheckBoxList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="updQty" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <%--<asp:AsyncPostBackTrigger ControlID="btnCondition" EventName="Click" />--%>
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">
                                                        Qty Limit : 
                                               
                                                        <%--<asp:Button ID="btnCondition" runat="server" Text="=" CssClass="btn" TabIndex="5" OnClick="btnCondition_Click" />--%>
                                                    </label>

                                                    <div class="col-md-8 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:GridView ID="grvQtyLimit" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered nowrap">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Stk. Qty.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblStkQty" runat="server" Text='<%# Eval("LISTDESC") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="To be upload">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="txtToBeQty" runat="server" Text='<%# Eval("EXTFIELD1") %>' TextMode="Number"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                            <%--<asp:TextBox ID="txtQtyLimit" runat="server" CssClass="form-control" TabIndex="6"></asp:TextBox>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>--%>
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




    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Upload Listing on Various Platforms  </strong></h3>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                                <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowHeader="true">
                                                    <EmptyDataTemplate>
                                                        No Record Found!
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle CssClass="header" />
                                                    <Columns>
                                                        <asp:BoundField DataField="STOCKTYPE" HeaderText="Stock Type" />
                                                        <asp:BoundField DataField="PLANT" HeaderText="Plant" />
                                                        <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                        <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                        <asp:BoundField DataField="QTY" HeaderText="Qty." />
                                                        <asp:BoundField DataField="GRADE" HeaderText="Grade" />
                                                        <asp:BoundField DataField="AMAZON" HeaderText="ASIN" />
                                                        <asp:BoundField DataField="NEWAMAZON" HeaderText="New ASIN" />
                                                        <asp:BoundField DataField="CITY_NAME" HeaderText="City" />
                                                        <asp:BoundField DataField="PLANTCD" HeaderText="Plant CD" Visible="false" />
                                                        <asp:BoundField DataField="CMPID" HeaderText="CMP ID" Visible="false" />
                                                        <asp:BoundField DataField="FinalApproveListingAmount" HeaderText="Lock Price" />
                                                        <asp:BoundField DataField="SALEPRICE" HeaderText="Sale Price (25% of Lock Price)" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="col-md-6">
                                            <asp:LinkButton runat="server" ID="lnkDownload" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkDownload_Click"><i class="fa fa-download"></i>Download</asp:LinkButton>
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


    <%--<div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Upload Listing on Various Platforms  </strong></h3>
                            <div class="panel-body">
                                <div class="row">
                                   
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmListingUpload" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />

</asp:Content>
