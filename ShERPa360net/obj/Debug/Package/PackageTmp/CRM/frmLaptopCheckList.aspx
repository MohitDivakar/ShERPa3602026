<%@ Page Title="Quality Check List - Laptop" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmLaptopCheckList.aspx.cs" Inherits="ShERPa360net.CRM.frmLaptopCheckList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Quality Check List - Laptop </title>

    <style type="text/css">
        .chclass {
        }

            .chclass label {
                position: relative;
                top: -2px;
                left: -5px;
            }

            .chclass input[type="checkbox"] {
                margin: 10px 10px 0px;
            }

            .chclass label {
                margin: 10px 10px 0px;
            }
        /*  .chclass input {
                position: absolute;
                top: 10px;
                left: 10px;
            }*/
    </style>

    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        .btnAqua {
            width: 60px;
            background-color: #faa61a;
            color: white;
        }

        .error {
            color: Red;
            display: none;
        }

        body .table thead th {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }

        .header {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }
    </style>


    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {

            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_btnSave").style.display = "none";

        }
    </script>


    <script type="text/javascript">
        function Validate() {




            debugger;
            //Reference the GridView.
            var grid = document.getElementById("<%=grvCheckList.ClientID %>");

            //Reference all INPUT elements.
            var inputs = grid.getElementsByTagName("INPUT");

            //Set the Validation Flag to True.
            var isValid = true;
            for (var i = 0; i < inputs.length; i++) {
                //If TextBox.
                if (inputs[i].type == "file") {
                    //Reference the Error Label.
                    var label = inputs[i].parentNode.getElementsByTagName("SPAN")[0];

                    //If Blank, display Error Label.
                    if (inputs[i].value == "") {
                        label.style.display = "block";
                        isValid = false;
                    } else {
                        label.style.display = "none";
                    }
                }
            }
            if (isValid == true) {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_btnSave").style.display = "none";
            }
            else {
                document.getElementById("busy-holder1").style.display = "none";
                document.getElementById("ContentPlaceHolder1_btnSave").style.display = "block";
            }

            return isValid;
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Laptop Check List  </strong></h3>
                        </div>


                        <div class="panel-body">


                            <div class="row">

                                <div class="col-md-12" style="border: ridge;">
                                    <div class="col-md-4" style="text-align: center;">
                                        <img src="../img/mobexLogo.png" style="height: 30px; width: 180px; text-align: center;" />
                                    </div>
                                    <div class="col-md-4" style="text-align: center; padding-top: 10px;">
                                        <h3><strong>Quality Check List - Laptop  </strong></h3>
                                    </div>
                                    <div class="col-md-4" style="text-align: center;">
                                        <img src="../img/QarmatekLogo.png" style="height: 30px; width: 180px; text-align: center;" />
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
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12" style="padding-top: 10px;">

                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Job ID : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtJobID" runat="server" CssClass="form-control" MaxLength="10" OnTextChanged="txtJobID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvJobid" runat="server" ControlToValidate="txtJobID" ForeColor="Red"
                                                    ErrorMessage="Enter Job ID" ValidationGroup="Val" Display="Dynamic">Enter Job ID</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Project : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvProject" runat="server" ControlToValidate="ddlProject" ForeColor="Red" InitialValue="0"
                                                    ErrorMessage="Select Project" ValidationGroup="Val" Display="Dynamic">Select Project</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Color : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="ddlColor" ForeColor="Red" InitialValue="0"
                                                    ErrorMessage="Select Color" ValidationGroup="Val" Display="Dynamic">Select Color</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" style="padding-top: 10px;">

                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SR. No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtSRNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSRNo" runat="server" ControlToValidate="txtSRNo" ForeColor="Red"
                                                    ErrorMessage="Enter Serial No." ValidationGroup="Val" Display="Dynamic">Enter Serial No.</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" ForeColor="Red" InitialValue="0"
                                                    ErrorMessage="Select Status" ValidationGroup="Val" Display="Dynamic">Select Status</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Grade : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvGrade" runat="server" ControlToValidate="ddlGrade" ForeColor="Red" InitialValue="0"
                                                    ErrorMessage="Select Grade" ValidationGroup="Val" Display="Dynamic">Select Grade</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" style="padding-top: 10px;">

                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDate" ForeColor="Red"
                                                        ErrorMessage="Enter Date" ValidationGroup="Val" Display="Dynamic">Enter Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Make : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtMake" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMake" runat="server" ControlToValidate="txtMake" ForeColor="Red"
                                                    ErrorMessage="Enter Make" ValidationGroup="Val" Display="Dynamic">Enter Make</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Model : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtModel" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvModel" runat="server" ControlToValidate="txtModel" ForeColor="Red"
                                                    ErrorMessage="Enter Model" ValidationGroup="Val" Display="Dynamic">Enter Model</asp:RequiredFieldValidator>
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


    <div class="page-content-wrap" id="divMob" runat="server">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="box">
                                        <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-width: unset !important; max-height: unset !important;">
                                            <asp:GridView ID="grvCheckList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap" CellSpacing="0" AutoGenerateColumns="false"
                                                Style="overflow-x: scroll" ShowHeaderWhenEmpty="true" OnRowDataBound="grvCheckList_RowDataBound">
                                                <RowStyle Wrap="true" />
                                                <EmptyDataTemplate>
                                                    No Records Found !
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="header" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Sr. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSrNo" runat="server" Text='<%# Eval("SORTORDER") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Area">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArea" runat="server" Text='<%# Eval("AREA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Question">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("QUESTION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Answer">
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="updRadio" runat="server">
                                                                <%--<Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="rblAnswer" EventName="SelectedIndexChanged" />
                                                                    <asp:PostBackTrigger ControlID="rblAnswer" />
                                                                </Triggers>--%>
                                                                <ContentTemplate>
                                                                    <asp:RadioButtonList ID="rblAnswer" runat="server" RepeatLayout="Table" CssClass="chclass" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblAnswer_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true">
                                                                        <asp:ListItem Value="1" Text="Pass" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Value="0" Text="Fail"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>


                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlRemarks" runat="server" CssClass="form-control" Visible="false"></asp:DropDownList>
                                                            <span class="error">Required</span>
                                                            <asp:RequiredFieldValidator ID="rfvRemarks" runat="server" ControlToValidate="txtRemarks" ForeColor="Red"
                                                                ErrorMessage="Enter Value" ValidationGroup="Val" Display="Dynamic" Enabled="false" Visible="false">Enter Value</asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ID="rfvDDLRemarks" runat="server" ControlToValidate="ddlRemarks" ForeColor="Red" InitialValue="0"
                                                                ErrorMessage="Select Remarks" ValidationGroup="Val" Display="Dynamic" Enabled="false" Visible="false">Select Remarks</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Phot Req." Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPhotoReq" runat="server" Text='<%# Eval("PHOTOREQ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Upload Photo" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fuImage" runat="server" Visible="true" />
                                                            <span class="error">Required</span>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" style="padding-top: 10px;">
                                    <div class="col-md-4" style="text-align: center;">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Verified By : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlVerifiedBy" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rvVerified" runat="server" ControlToValidate="ddlVerifiedBy" ForeColor="Red" InitialValue="0"
                                                    ErrorMessage="Select Verified By" ValidationGroup="Val" Display="Dynamic">Select Verified By</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <center style="padding-top: 10px !important;">
                                <asp:Button ID="btnCancel" runat="server" Text="CANCEL" CssClass="btn btn-success pull-center" PostBackUrl="~/CRM/frmViewLaptopCheckList.aspx" />
                                <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="btn btn-success pull-center" ValidationGroup="Val" OnClick="btnSave_Click" /><%-- OnClientClick="return Validate();" />--%>
                                <div id="busy-holder1" style="display: none" class="clearfix inline">
                                    <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                    <label>Please wait...</label>
                                </div>
                            </center>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmCRMCheckList" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />

</asp:Content>
