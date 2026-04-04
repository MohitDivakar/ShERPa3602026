<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ProductOtherDeviceEntry.aspx.cs" Inherits="ShERPa360net.UTILITY.ProductOtherDeviceEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            $("#lblfilealert").css("display", "none");
            $("#lblvalidRate").css("display", "none");
            var Isvalidate      = true;
            var purchasevalue   = 0;
            var newratevalue    = 0;
            var purpervalue     = 0;
            var vendorgrade     = "";
            var make            = "";

            // Initiate the value
            var fkratevalue     = 0;
            var amzratevalue    = 0;
            var webratevalue    = 0;
            var fkpervalue      = 0;
            var amzpervalue     = 0;
            var webpervalue     = 0;
            var recomendedrate  = 0;

            var Isflipkartlisted = false;
            var Isamzlisted      = false;
            var Isweblisted      = false;

            vendorgrade = $(".ddlGrade option:selected").val();
            make        = $(".ddlMake option:selected").val();

            // Initiate the value

            purchasevalue  = $(".txtVendorRate").val().length > 0 ? parseFloat($(".txtVendorRate").val()) : 0;
            recomendedrate = $("#ContentPlaceHolder1_txtRecomendRate").val().length > 0 ? parseFloat($("#ContentPlaceHolder1_txtRecomendRate").val()) : 0;
            newratevalue   = $("#ContentPlaceHolder1_hdNewRate").val().length > 0 ? parseFloat($("#ContentPlaceHolder1_hdNewRate").val()) : 0;

            if (purchasevalue != 0 && newratevalue != 0) {
                purpervalue = (((purchasevalue * 100)) / newratevalue).toFixed(0);
            }

            if (purchasevalue != 0)
            {
                fkratevalue = (((purchasevalue)) * (1.234));
                fkratevalue = (Math.floor(fkratevalue * Math.pow(10, -2)) / Math.pow(10, -2));
            }

            if (purchasevalue != 0) {
                amzratevalue = (((purchasevalue + 700)) * (1.175));
                amzratevalue = (Math.floor(amzratevalue * Math.pow(10, -2)) / Math.pow(10, -2));
            }

            if (purchasevalue != 0) {
                webratevalue = (((amzratevalue)) - (350));
            }

            if (purchasevalue != 0 && fkratevalue != 0) {
                fkpervalue = (((fkratevalue * 100)) / newratevalue).toFixed(0);
            }

            if (purchasevalue != 0 && amzratevalue != 0) {
                amzpervalue = (((amzratevalue * 100)) / newratevalue).toFixed(0);
            }

            if (purchasevalue != 0 && webratevalue != 0) {
                webpervalue = (((webratevalue * 100)) / newratevalue).toFixed(0);
            }

            //|| (make.toUpperCase() == "APPLE")

            if ((fkratevalue > 45000) || (fkpervalue >= 90)) {
                    Isflipkartlisted = false;
            }
            else {
                    Isflipkartlisted = true;
            }

            //vendorgrade == "B" ||
            if ((make.toUpperCase() == "APPLE") || (vendorgrade == "C") || (amzpervalue >= 90)) {
                Isamzlisted = false;
            }
            else {
                Isamzlisted = true;
            }

            //|| (vendorgrade == "B")
            if ((vendorgrade == "C")  || (webpervalue >= 90)) {
                Isweblisted = false;
            }
            else {
                Isweblisted = true;
            }

            if ($(".ddlMake option:selected").val() == "0" || $(".ddlMake option:selected").val() == "") {
                Isvalidate = false;
            }

            if ($(".ddlModel option:selected").val() == "0" || $(".ddlModel option:selected").val() == "") {
                Isvalidate = false;
            }

            if ($(".ddlRam option:selected").val() == "0" || $(".ddlRam option:selected").val() == "") {
                Isvalidate = false;
            }

            if ($(".ddlRom option:selected").val() == "0" || $(".ddlRom option:selected").val() == "") {
                Isvalidate = false;
            }

            if ($(".ddlColor option:selected").val() == "0" || $(".ddlColor option:selected").val() == "" || $(".ddlColor option:selected").val() == "-- SELECT --") {
                Isvalidate = false;
            }

            if ($(".ddlGrade option:selected").val() == "SELECT") {
                Isvalidate = false;
            }

            if ($(".txtVendorRate").val().length == 0) {
                Isvalidate = false;
            }

            if ($(".ddlVendor option:selected").val() == "0" || $(".ddlVendor option:selected").val() == "") {
                Isvalidate = false;
            }

            if ($(".ddlInvoice option:selected").val() == "-1") {
                Isvalidate = false;
            }

            if ($(".ddlBox option:selected").val() == "-1") {
                Isvalidate = false;
            }

            if ($(".ddlCharger option:selected").val() == "-1") {
                Isvalidate = false;
            }

            if (purchasevalue > 0) {
                if (purpervalue < 40) {  // change from 40 to 80
                    var Isconfirm = confirm("Are you sure you want to enter " + $(".ddlMake option:selected").text() + " " + $(".ddlModel option:selected").text() + " Rom:" + $(".ddlRom option:selected").text() + " Ram:" + $(".ddlRam option:selected").text() + " Grade:" + $(".ddlGrade option:selected").val() + " with Rate:" + $(".txtVendorRate").val() + "?");
                    if (!Isconfirm) {
                        Isvalidate = false;
                    }
                    //$("#lblvalidRate").css("display", "block");
                    //Isvalidate = false;
                }
                else
                {
                    //Isflipkartlisted == false &&
                    if ( Isamzlisted == false && Isweblisted == false) {
                        var Isconfirm = confirm("Are you sure you want to enter " + $(".ddlMake option:selected").text() + " " + $(".ddlModel option:selected").text() + " Rom:" + $(".ddlRom option:selected").text() + " Ram:" + $(".ddlRam option:selected").text() + " Grade:" + $(".ddlGrade option:selected").val()  + " with Rate:" + $(".txtVendorRate").val() + "? Due to Vendor Rate it will be Rejected");
                        if (!Isconfirm) {
                            Isvalidate = false;
                        }
                    }
                }
            }
           
            if ($("#fuImage").get(0).files.length > 0) {        
                var isimglfile = false;
                var filename = $("#fuImage").get(0).files[0].name;
                var fileextensionarray = filename.split(".");
                var fileextension = fileextensionarray[(fileextensionarray.length - 1)];
                if ((fileextension.toUpperCase()).includes("JPEG") || (fileextension.toUpperCase()).includes("JPG") || (fileextension.toUpperCase()).includes("TIFF")
                    || (fileextension.toUpperCase()).includes("PNG")) {
                    var isimglfile = true;
                }

                if (isimglfile == false) {
                    $("#lblfilealert").text("Please Select the only JPEG,JPG,TIFF,PNG.");
                    $("#lblfilealert").css("display", "block");
                    Isvalidate = false;
                }
            }

            //}

            if (Isvalidate) {
                $("#progress").show();
                //$("#ContentPlaceHolder1_btnAdd").click();
            }
            else {
                    return Isvalidate;
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
                                <asp:PostBackTrigger ControlID="btnAdd" />
                                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="ddlItemGroup" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlItemSubGroup" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlMake" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlModel" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlRam" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlRom" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlGrade" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlColor" EventName="SelectedIndexChanged" />
                            </Triggers>

                            <ContentTemplate>
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Product Entry</strong></h3>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <fieldset class="scheduler-border">
                                                    <legend class="scheduler-border">Product Entry</legend>
                                                    <div class="col-md-12">
                                                        <div class="col-md-3" runat="server" id="dvItemGroup" visible="true">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Item Group. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlItemGroup" OnSelectedIndexChanged="ddlItemGroup_SelectedIndexChanged" ClientIDMode="Static" AutoPostBack="false" runat="server" CssClass="form-control ddlItemGroup required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvItemGroup" Style="color: red;" ControlToValidate="ddlItemGroup" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Item Group" InitialValue="0">Please Select Item Group</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvItemSubGroup" visible="true">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Item Sub Group. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlItemSubGroup"  ClientIDMode="Static" OnSelectedIndexChanged="ddlItemSubGroup_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control ddlItemSubGroup required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvItemSubGroup" Style="color: red;" ControlToValidate="ddlItemSubGroup" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Item Sub Group" InitialValue="0">Please Select Item Sub Group</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvItemMake" visible="true">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Make. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlMake" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" runat="server" CssClass="form-control ddlMake required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvMake" Style="color: red;" ControlToValidate="ddlMake" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Make" InitialValue="0">Please Select Make</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvItemModel" visible="true">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Model. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlModel" AutoPostBack="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" runat="server" CssClass="form-control ddlModel required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvModel" Style="color: red;" ControlToValidate="ddlModel" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Model" InitialValue="0">Please Select Model</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">
                                                       <div class="col-md-3" runat="server" id="dvRam" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">RAM. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlRam" OnSelectedIndexChanged="ddlRam_SelectedIndexChanged" AutoPostBack="true" ClientIDMode="Static" runat="server" CssClass="form-control ddlRam required_text_box"></asp:DropDownList>
                                                                    <%--                                                                <asp:Label Style="color: red; display:none;" ID="lblRamalert" ClientIDMode="Static">Please Select Ram</asp:Label>--%>
                                                                    <asp:RequiredFieldValidator ID="rfvRam" Style="color: red;" ControlToValidate="ddlRam" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Ram" InitialValue="0">Please Select Ram</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                       <div class="col-md-3" runat="server" id="dvRom" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">ROM. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlRom" OnSelectedIndexChanged="ddlRom_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control ddlRom required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvRom" Style="color: red;" ControlToValidate="ddlRom" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Rom" InitialValue="0">Please Select Rom</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvColor" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Color. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlColor" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="form-control ddlColor required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvColor" Style="color: red;" ControlToValidate="ddlColor" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Color" InitialValue="0">Please Select Color</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvGrade" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Grade. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlGrade" AutoPostBack="true" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" runat="server" CssClass="form-control ddlGrade required_text_box">
                                                                        <asp:ListItem Text="SELECT" Selected="True" Value="SELECT"></asp:ListItem>
                                                                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                                        <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvGrade" Style="color: red;" ControlToValidate="ddlGrade" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select Grade" InitialValue="SELECT">Please Select Grade</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">
                                                        <div class="col-md-3" runat="server" id="dvVendorStock" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Vendor Stock. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="3" ID="txtVendorStock" Enabled="false" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Vendor Stock" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvVendorStock" Style="color: red!important;" runat="server" ControlToValidate="txtVendorStock" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Stock">Please Enter Vendor Stock</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        
                                                        <div class="col-md-3" runat="server" id="dvRecomendedRate" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Recomend Rate. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="3" Enabled="false" ID="txtRecomendRate" Style="font-weight: bold!important; color: green!important;" runat="server" placeholder="Recomend Rate" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:TextBox TabIndex="3" Visible="false" ID="txtNewRate" Style="font-weight: bold!important; color: green!important;" runat="server" placeholder="New Rate" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:HiddenField   ID="hdNewRate"  runat="server"></asp:HiddenField>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvSuggestRate" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Vendor Rate. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="3" ID="txtVendorRate" onblur="IsAllowonlyNumeric();" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Vendor Rate" CssClass="form-control txtVendorRate required_text_box"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvVendorRate" Style="color: red!important;" runat="server" ControlToValidate="txtVendorRate" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Rate">Please Enter Vendor Rate</asp:RequiredFieldValidator>
                                                                    <label id="lblVendorratealert" style="display: none!important; font-weight: bold; color: red!important;">Vendor Rate should be numeric and more then 1000</label>
                                                                    <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important;" runat="server" ClientIDMode="Static" ID="lblvalidRate">Vendor Rate should not be less then 50% of New Rate.</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvVendorName" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Vendor Name. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ClientIDMode="Static" ID="ddlVendor" runat="server" AutoPostBack="false" CssClass="form-control ddlVendor required_text_box"></asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvVendorDetail" Style="color: red!important;" runat="server" ControlToValidate="ddlVendor" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Name" InitialValue="0">Please Select Vendor Name</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        
                                                    </div>

                                                    <div class="col-md-12" style="margin-top: 10px!important;">
                                                        <div class="col-md-3" runat="server" id="dvInvoice" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Invoice. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlInvoice" runat="server" CssClass="form-control ddlInvoice required_text_box">
                                                                        <asp:ListItem Text="SELECT" Selected="True" Value="-1"></asp:ListItem>
                                                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvInvoice" Style="color: red!important;" runat="server" ControlToValidate="ddlInvoice" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select the Invoice" InitialValue="-1">Please Select the Invoice</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                       <div class="col-md-3" runat="server" id="dvBox" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Box. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlBox" runat="server" CssClass="form-control ddlBox required_text_box">
                                                                        <asp:ListItem Text="SELECT" Selected="True" Value="-1"></asp:ListItem>
                                                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvBox" Style="color: red!important;" runat="server" ControlToValidate="ddlBox" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select the Box" InitialValue="-1">Please Select the Box</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvCharger" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Charger. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList ID="ddlCharger" runat="server" CssClass="form-control ddlCharger required_text_box">
                                                                        <asp:ListItem Text="SELECT" Selected="True" Value="-1"></asp:ListItem>
                                                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="rfvCharger" Style="color: red!important;" runat="server" ControlToValidate="ddlCharger" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Select the Charger" InitialValue="-1">Please Select the Charger</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" id="dvIMEI" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">IMEI No. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="3" ID="txtIMEINo" MaxLength="15" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="IMEI No" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Image. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:FileUpload ID="fuImage" ClientIDMode="Static" runat="server"  CssClass="file-simple" />
                                                                    <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important;" runat="server" ClientIDMode="Static" ID="lblfilealert">Please Select the File to Upload.</asp:Label>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-12 text-center">
                                                        <div class="col-md-12">
                                                            <asp:Button TabIndex="3" ID="btnAdd" OnClick="imgSaveAll_Click"  ValidationGroup="SaveAll" runat="server" Text="Add" CssClass="btn btn-primary"></asp:Button>
                                                            <asp:Button TabIndex="3" ID="btnReset" OnClick="imgReset_Click" Style="margin-left: 40px!important" runat="server" Text="Reset" CssClass="btn btn-danger"></asp:Button>
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
    <input type="hidden" id="menutabid" value="tsmTranMobexOtherProductEntry" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>

