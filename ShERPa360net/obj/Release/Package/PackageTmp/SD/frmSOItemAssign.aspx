<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmSOItemAssign.aspx.cs" Inherits="ShERPa360net.SD.frmSOItemAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        body .table thead th {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }

        .header {
            position: sticky !important;
            top: -14px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }
    </style>

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

        .rowGreen {
            background-color: green !important;
            /*background: #00FF00 !important;*/
        }
    </style>

    <%--<script language="javascript" type="text/javascript">  
        function divexpandcollapse(divname) {
            debugger;
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display = "none") {
                div.style.display = "inline";
                img.src = "../img/collapse_blue.png";
            } else {
                div.style.display = "none";
                img.src = "../img/expand_blue.png";
            }
        }
</script> pt>--%>

    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../img/collapse_blue.png";
            } else {
                div.style.display = "none";
                img.src = "../img/expand_blue.png";
            }
        }
    </script>
    ipt>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Penoding SO List</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">From : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Plant : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SO No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Sales From : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <%--<asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control"></asp:DropDownList>--%>

                                                    <asp:RadioButtonList ID="rblsalesFrom" runat="server" RepeatLayout="Table" CssClass="chclass" RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="All" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Amazon" Value="10906"></asp:ListItem>
                                                        <asp:ListItem Text="Mobex Website" Value="11193"></asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </div>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-md-2">
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
                                    <asp:LinkButton ID="lnkSend" runat="server" Text="Send Message" OnClick="lnkSend_Click" Font-Size="Larger"><i class="fa fa-send fa-3x"></i> Send </asp:LinkButton>
                                    <br />
                                    <br />
                                    <div class="box">
                                        <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                            <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="header" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                                                        </HeaderTemplate>
                                                        <%--<ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>--%>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <a href="JavaScript:divexpandcollapse('div<%# Eval("ID") %>');">
                                                                <img id="imgdiv<%# Eval("ID") %>" width="14px" border="0" src="../img/expand_blue.png" />
                                                            </a>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Biding">
                                                        <ItemTemplate>
                                                            <div>
                                                                <asp:TextBox runat="server" CssClass='<%# "form-control txtbid" + Eval("ITEMCODE") %>' ID="txtbid" placeplholder="bid amount" aria-label="bid amount" aria-describedby="basic-addon2"></asp:TextBox>
                                                                <%--<input type="text" >--%>
                                                                <div class="input-group-append">
                                                                    <%--<button >Bid</button>--%>
                                                                    <asp:Button runat="server" CssClass="btn btn-outline-secondary" ID="btnBid" OnClientClick='<%# "SubmitBid(\""+ Eval("ITEMCODE")+"\");return false;" %>' Text="Bid" />
                                                                </div>
                                                                <asp:Label ID="lblBid" runat="server" Text='Not Applicable'></asp:Label>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SO No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSONO" runat="server" Text='<%# Eval("SONO") %>'></asp:Label>
                                                            <asp:HiddenField runat="server" ID="hdCount" Value='<%# Bind("TOTCOUNT2") %>'></asp:HiddenField>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Plant">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPLANTCD" runat="server" Text='<%# Eval("PLANTCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Ref. No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblREFNO" runat="server" Text='<%# Eval("REFNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Deli. Dt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDELIDT" runat="server" Text='<%# Eval("DELIDT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Desc.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITEMID" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SO Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSOQTY" runat="server" Text='<%# Eval("SOQTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cust. Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="State">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSTATE" runat="server" Text='<%# Eval("STATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="City">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCITY" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Sales Chnl.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPLATFORM" runat="server" Text='<%# Eval("PLATFORM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    
                                                    <asp:TemplateField HeaderText="Entity ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblENTITYID" runat="server" Text='<%# Eval("ENTITYID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <tr>
                                                                <td colspan="100%">
                                                                    <div id="div<%# Eval("ID") %>" style="display: none; overflow-x: scroll; max-height: 500px !important;" class="box-body divhorizontal">
                                                                        <asp:LinkButton ID="lnkSendSingle" runat="server" Text="Send Message" OnClick="lnkSendSingle_Click" Font-Size="Larger"><i class="fa fa-send fa-1x"></i> Send </asp:LinkButton>

                                                                        <asp:GridView ID="gvInnerList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered nowrap" CellSpacing="0"
                                                                            ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvInnerList_RowDataBound">
                                                                            <Columns>

                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkItem" runat="server" />
                                                                                        <asp:LinkButton Style="margin-top: 5px!important; margin-left: 10px!important;" runat="server" ID="btnUnlist" Text="Unlist" CssClass="btn btn-primary" OnClick="btnUnlist_Click"></asp:LinkButton>
                                                                                        <asp:LinkButton Style="margin-top: 5px!important; margin-left: 10px!important;" runat="server" ID="btnCallAttent" Text="<i id='spancall' runat='server' class='fa fa-phone' style='color: #FFF'><asp:Label ID='lblCallText' CssClass='form-control' runat='server' Text='Call'></asp:Label></i> Call" CssClass="btn btn-success btn-app bg-green margin" OnClick="btnCallAttent_Click"></asp:LinkButton>
                                                                                        <%--<asp:LinkButton ID="btnCallAttent" runat="server" CommandName="CallAttend" CssClass="btn btn-success btn-app bg-green margin" Font-Bold="true" Style="border-radius: 5px" Text="<i id='spancall' runat='server' class='fa fa-phone' style='color: #FFF'><asp:Label ID='lblCallText' CssClass='form-control' runat='server' Text='Call'></asp:Label></i><br>Attempt Call" />--%>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Plant">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPLANTCD" runat="server" Text='<%# Eval("PLANTCD") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Item Code">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Vendor">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblVENDORNAME" runat="server" Text='<%# Eval("VENDORNAME") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Vendor Contact">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%# Eval("CONTACTNO") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="State">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSTATE" runat="server" Text='<%# Eval("STATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Vend. Price">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblVENDORPRICE" runat="server" Text='<%# Eval("VENDORPRICE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Mobex Price">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMOBEXPRICE" runat="server" Text='<%# Eval("MOBEXPRICE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSTATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Status Desc.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%# Eval("STATUSDESC") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Biker">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBIKERNAME" runat="server" Text='<%# Eval("BIKERNAME") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                                <asp:TemplateField HeaderText="Create Dt.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%# Eval("CREATEDATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Create By" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCREATEBY" runat="server" Text='<%# Eval("CREATEBY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLISTEDBY" runat="server" Text='<%# Eval("LISTEDBY") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="List Dt.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLISTEDDATE" runat="server" Text='<%# Eval("LISTEDDATE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Biker Cont.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBIKERCONTACT" runat="server" Text='<%# Eval("BIKERCONTACT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="ASM Cont.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblASMCONTACT" runat="server" Text='<%# Eval("ASMCONTACT") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="PM Cont.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPM" runat="server" Text='<%# Eval("PM") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="BSM Cont.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblBSM" runat="server" Text='<%# Eval("BSM") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="ZSM Cont.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblZSM" runat="server" Text='<%# Eval("ZSM") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="RSM Cont.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRSM" runat="server" Text='<%# Eval("RSM") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="BSM Cont.">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblNSM" runat="server" Text='<%# Eval("NSM") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="COLOR" Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblColor" runat="server" Text='<%# Eval("COLOR") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <%--Make--%>
                                                                                <asp:TemplateField HeaderText="MAKE" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPRODMAKE" runat="server" Text='<%# Eval("PRODMAKE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <%--Model--%>
                                                                                <asp:TemplateField HeaderText="MODEL" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMODELDESC" runat="server" Text='<%# Eval("MODELDESC") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <%--RAM--%>
                                                                                <asp:TemplateField HeaderText="RAM" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRAMSIZE" runat="server" Text='<%# Eval("RAMSIZE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <%--ROM--%>
                                                                                <asp:TemplateField HeaderText="ROM" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblROMSIZE" runat="server" Text='<%# Eval("ROMSIZE") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <%--COLOR--%>
                                                                                <asp:TemplateField HeaderText="COLOR" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPRODCOLOR" runat="server" Text='<%# Eval("PRODCOLOR") %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>


                                                                                <%--<asp:TemplateField HeaderText="List By">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLISTBY" runat="server" Text='<%# Eval("LISTBY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>


                                                                                <%--<asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMobile" runat="server" Text="9723433664"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMMIRPOINV" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
