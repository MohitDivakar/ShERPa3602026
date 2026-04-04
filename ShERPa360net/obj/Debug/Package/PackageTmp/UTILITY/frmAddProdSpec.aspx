<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmAddProdSpec.aspx.cs" Inherits="ShERPa360net.UTILITY.frmAddProdSpec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function IsAllowonlyNumericKeyFinalAmount() {
            debugger
            if ($("#txtFinalAmount").val().length > 0) {
                if (!$.isNumeric($("#txtFinalAmount").val())) {
                    $("#txtFinalAmount").val("");
                }
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
                        <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnAddconfirm" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="ddlMake" EventName="SelectedIndexChanged" />
                                <%--<asp:AsyncPostBackTrigger ControlID="ddlModel" EventName="SelectedIndexChanged" />--%>
                            </Triggers>

                            <ContentTemplate>
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Product Specification Entry</strong></h3>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <fieldset class="scheduler-border">
                                                    <legend class="scheduler-border">Product Specification Entry</legend>
                                                    <div class="col-md-12">
                                                        <div class="col-md-3" runat="server" id="dvItemGroup" visible="true">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Item Group. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlItemGroup" ClientIDMode="Static" AutoPostBack="false" runat="server" CssClass="form-control ddlItemGroup required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvItemGroup" Style="color: red;" ControlToValidate="ddlItemGroup" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Item Group" InitialValue="0">Please Select Item Group</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvItemSubGroup" visible="true">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Item Sub Group. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlItemSubGroup" ClientIDMode="Static" OnSelectedIndexChanged="ddlItemSubGroup_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control ddlItemSubGroup required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvItemSubGroup" Style="color: red;" ControlToValidate="ddlItemSubGroup" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Item Sub Group" InitialValue="0">Please Select Item Sub Group</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Item Type. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlItemType" ClientIDMode="Static" AutoPostBack="true" runat="server" CssClass="form-control ddlItemType required_text_box"></asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvItemType" Style="color: red;" ControlToValidate="ddlItemType" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Item Type" InitialValue="0">Please Select Item Type</asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-3" runat="server" id="dvMake" visible="true">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Make. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlMake" onchange="GetEachProdSpecPrimaryDetail();" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" runat="server" CssClass="form-control ddlMake required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvMake" Style="color: red;" ControlToValidate="ddlMake" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Make" InitialValue="0">Please Select Make</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvModel" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Model. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlModel" onchange="GetEachProdSpecPrimaryDetail();" runat="server" CssClass="form-control ddlModel required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvModel" Style="color: red;" ControlToValidate="ddlModel" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Model" InitialValue="0">Please Select Model</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">
                                                        <div class="col-md-3" runat="server" id="dvModelDisplay" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Model Display. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtModelDisplay" TextMode="MultiLine" runat="server" CssClass="form-control" placeholder="Model Display" />
                                                                    <asp:RequiredFieldValidator ID="rfvModelDisplay" Style="color: red;" ControlToValidate="txtModelDisplay" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Model Display">Please Enter Model Display</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvRam" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">RAM. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlRam" onchange="GetEachProdSpecPrimaryDetail();"  ClientIDMode="Static" runat="server" CssClass="form-control ddlRam required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvRam" Style="color: red;" ControlToValidate="ddlRam" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Ram" InitialValue="0">Please Select Ram</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>


                                                        <div class="col-md-3" runat="server" id="dvRom" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">ROM. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlRom" onchange="GetEachProdSpecPrimaryDetail();"  runat="server" CssClass="form-control ddlRom required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvRom" Style="color: red;" ControlToValidate="ddlRom" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Rom" InitialValue="0">Please Select Rom</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvColor" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Color. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlColor" runat="server" CssClass="form-control ddlColor required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvColor" Style="color: red;" ControlToValidate="ddlColor" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Color" InitialValue="0">Please Select Color</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-md-3" runat="server" id="dvActive" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Is Active :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:CheckBox ID="chkActive" runat="server" CssClass="form-control" Checked="true" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">


                                                        <div class="col-md-3" runat="server" id="dvNewRate" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">New Rate :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtNewRate" runat="server" CssClass="form-control" placeholder="New Rate" />
                                                                    <asp:RequiredFieldValidator ID="rfvNewRate" Style="color: red;" ControlToValidate="txtNewRate" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter New Rate" InitialValue="0">Please Enter New Rate</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvSuggestRate" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Basic Pur Rate :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtBasiPurRate" runat="server" CssClass="form-control" placeholder="Basic Rate" />
                                                                    <asp:RequiredFieldValidator ID="rfvBasiPurRate" Style="color: red;" ControlToValidate="txtBasiPurRate" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Basic Pur Rate" InitialValue="0">Please Enter Basic Pur Rate</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvLaunchYear" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Launch Year :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:HiddenField runat="server" ID="hdBasicPrice" />
                                                                    <asp:HiddenField runat="server" ID="hdNewRate" />
                                                                    <asp:HiddenField runat="server" ID="hdLockAmount" />
                                                                    <asp:TextBox ID="txtLaunchYear" runat="server" CssClass="form-control" placeholder="Launch Year" />
                                                                    <asp:RequiredFieldValidator ID="rfvLaunchYear" Style="color: red;" ControlToValidate="txtLaunchYear" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Launch Year" InitialValue="0">Please Enter Launch Year</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvFinalAmount" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-6 control-label">Final Price Amount :</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:TextBox ID="txtFinalAmount" onkeyup="IsAllowonlyNumericKeyFinalAmount();" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Final Price Amount" />
                                                                    <asp:RequiredFieldValidator ID="FinalpriceAmount" Style="color: red;" ControlToValidate="txtFinalAmount" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Model Display">Please Enter Final Price Amount</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-12 text-center">
                                                        <div class="col-md-3" runat="server" id="dvMntTopSrNo" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-6 control-label">Monthly Top Sr No :</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:TextBox ID="txtMntTopSrNo"  ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Monthly Top Sales Sr No" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvIsInstantSelling" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-6 control-label">Is Instant Selling :</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:CheckBox runat="server" ID="chkInstantSelling" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvInstantSellingAmount" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-6 control-label">Instant Selling Amount :</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:TextBox ID="txtInstantAmount"  ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Instant Selling Amount" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                         <div class="col-md-3" runat="server" id="dvStockFinalPrice" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-6 control-label">Final Stock Price Amount :</label>
                                                                <div class="col-md-6 col-xs-12">
                                                                    <asp:TextBox ID="txtFinalStockPrice"  ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Final Stock Price Amount" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-9">
                                                            <asp:Button TabIndex="3" ID="btnAddconfirm" ClientIDMode="Static" OnClick="btnAdd_Click" Style="display: none!important;" ValidationGroup="SaveAll" runat="server" Text="Add" CssClass="btn btn-primary"></asp:Button>
                                                            <asp:Button TabIndex="3" ID="btnAdd" OnClick="btnAdd_Click" OnClientClick="return GetEachProdSpecalreadyExist();" ValidationGroup="SaveAll" runat="server" Text="Add" CssClass="btn btn-primary"></asp:Button>
                                                            <asp:Button TabIndex="3" ID="btnReset" OnClick="btnReset_Click" Style="margin-left: 40px!important" runat="server" Text="Reset" CssClass="btn btn-danger"></asp:Button>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 text-center" style="margin-top: 10px!important;">
                                                        <div class="col-md-12">
                                                            <asp:Label runat="server" ClientIDMode="Static" ID="lblalreadyexist" Text="Model is Already Exist" Style="text-align: center!important; color: red!important; font-weight: bold!important; font-size: 20px!important; display: none!important;">
                                                            </asp:Label>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMobexNewModel" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
