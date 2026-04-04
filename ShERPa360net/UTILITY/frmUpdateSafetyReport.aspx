<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmUpdateSafetyReport.aspx.cs" Inherits="ShERPa360net.UTILITY.frmUpdateSafetyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
            var grid = document.getElementById("<%=grvQuestions.ClientID %>");

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

            <%--function Validate1() {
                debugger;
                //Reference the GridView.
                var grid = document.getElementById("<%=grvQuestions.ClientID %>");

                //Reference all INPUT elements.
                var inputs = grid.getElementsByTagName("INPUT");

                //Set the Validation Flag to True.
                var isValid = true;
                for (var i = 0; i < inputs.length; i++) {
                    //If TextBox.
                    if (inputs[i].type == "text") {
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
                }--%>

            //for (var i = 0; i < inputs.length; i++) {
            //    debugger;
            //    //If TextBox.
            //    if (inputs[i].type == "radio") {
            //        //Reference the Error Label.
            //        var label = inputs[i].parentNode.getElementsByTagName("SPAN")[0];
            //        //var text = inputs[i].parentNode.getElementById("txtRemarks")[0];
            //        //If Blank, display Error Label.
            //        if (inputs[i].value == "0") {
            //          //  text.style.display = "block";
            //            //isValid = false;

            //            label.style.display = "block";
            //            isValid = false;
            //        } else {
            //            label.style.display = "none";
            //            text.style.display = "none";
            //        }
            //    }
            //}

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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Upload  </strong>Safety Report</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Date : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDate" ForeColor="Red"
                                                        ErrorMessage="Enter Date" ValidationGroup="valSafety" Display="Dynamic">Enter Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Location : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="ddlLocation" InitialValue="0" ForeColor="Red"
                                                    ErrorMessage="Select Location" ValidationGroup="valSafety" Display="Dynamic">Select Location</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Inspect By : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtInspectedBy" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvInspectedBy" runat="server" ControlToValidate="txtInspectedBy" ForeColor="Red"
                                                    ErrorMessage="Enter Inspected By Name" ValidationGroup="valSafety" Display="Dynamic">Enter Inspected By Name</asp:RequiredFieldValidator>
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

                                            <%--<asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                                    <asp:PostBackTrigger ControlID="grvQuestions" />

                                                    <asp:AsyncPostBackTrigger ControlID="rblAnswer" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>--%>


                                            <asp:GridView ID="grvQuestions" runat="server" CssClass="table table-hover table-striped table-bordered nowrap" CellSpacing="0" AutoGenerateColumns="false"
                                                Style="overflow-x: scroll" ShowHeaderWhenEmpty="true" OnRowDataBound="grvQuestions_RowDataBound">
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

                                                    <asp:TemplateField HeaderText="Area">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblArea" runat="server" Text='<%# Eval("AREA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Question">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("QUESTION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Answer">
                                                        <ItemTemplate>
                                                            <asp:RadioButtonList ID="rblAnswer" runat="server" OnSelectedIndexChanged="rblAnswer_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="Yes" Selected="True"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <asp:TextBox ID="txtRemarks" runat="server" Visible="false"></asp:TextBox>
                                                            <span class="error">Required</span>
                                                            <asp:RequiredFieldValidator ID="rfvRemarks" runat="server" ControlToValidate="txtRemarks" ForeColor="Red"
                                                                ErrorMessage="*" ValidationGroup="valSafety" Display="Dynamic" Enabled="false" Visible="false">*</asp:RequiredFieldValidator>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Phot Req." Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPhotoReq" runat="server" Text='<%# Eval("PHOTOREQ") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Upload Photo">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fuImage" runat="server" Visible="true" />
                                                            <span class="error">Required</span>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>


                                            <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <center style="padding-top: 10px !important;">
                                    <asp:Button ID="btnSave" runat="server" Text="SAVE" CssClass="btn btn-success pull-center"  ValidationGroup="valSafety" OnClick="btnSave_Click" OnClientClick="return Validate();"  />
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

    <input type="hidden" id="menutabid" value="tsmUltSafetyReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
