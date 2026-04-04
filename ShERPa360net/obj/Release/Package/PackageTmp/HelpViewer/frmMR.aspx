<%@ Page Title="" Language="C#" MasterPageFile="~/HelpViewer/Help.Master" AutoEventWireup="true" CodeBehind="frmMR.aspx.cs" Inherits="ShERPa360net.HelpViewer.frmMR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <style type="text/css">
        .TextFormat {
            border: 1px solid #00000a;
            padding-top: 0in;
            padding-bottom: 0in;
            padding-left: 0.08in;
            padding-right: 0.08in;
            font-size: 12px;
        }

        .TextFormat1 {
            font-size: 12px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div style="font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; padding-left: 300px;">
        <label style="vertical-align: top; font-weight: bold; font-size: 16px">Material Management System --> Material Request :</label>
        <br />
        <br />
        <table width="583" cellpadding="7" cellspacing="0">
            <col width="130">
            <col width="423">
            <tr valign="TOP">
                <td width="130" class="TextFormat">
                    <p lang="en-IN"><b>Screen Name:</b></p>
                </td>
                <td width="423" class="TextFormat">
                    <p lang="en-IN">Create MR</p>
                </td>
            </tr>
            <tr valign="TOP">
                <td width="130" class="TextFormat">
                    <p lang="en-IN"><b>Description :</b></p>
                </td>
                <td width="423" class="TextFormat">
                    <p lang="en-IN">To Create New MR</p>
                </td>
            </tr>
            <%--<tr valign="TOP">
                <td width="130" class="TextFormat">
                    <p lang="en-IN"><b>Originator:</b></p>
                </td>
                <td width="423" class="TextFormat">
                    <p lang="en-IN">Stock keeper</p>
                </td>
            </tr>--%>
            <%--<tr valign="TOP">
                <td width="130" class="TextFormat">
                    <p lang="en-IN"><b>Department:</b></p>
                </td>
                <td width="423" class="TextFormat">
                    <p lang="en-IN">Store</p>
                </td>
            </tr>--%>
            <tr valign="TOP">
                <td width="130" class="TextFormat">
                    <p lang="en-IN"><b>Navigation :</b></p>
                </td>
                <td width="423" class="TextFormat">
                    <p lang="en-IN">MM --> Indent --> Create MR</p>
                </td>
            </tr>
            <%--<tr valign="TOP">
                <td width="130" class="TextFormat">
                    <p lang="en-IN"><b>Document type (Movement type):</b></p>
                </td>
                <td width="423" class="TextFormat">
                    <p lang="en-IN">103 – Material Inward (PO)</p>
                </td>
            </tr>--%>
        </table>
        <p lang="en-IN" class="western" style="margin-bottom: 0in; line-height: 100%">
            <br />
        </p>

        <p lang="en-IN" class="western TextFormat1" style="text-indent: 0.5in; margin-bottom: 0.14in">
            <img src="Images/View MR.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
        </p>


        <p lang="en-IN" style="margin-left: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>View
Screen:</b>
        </p>





        <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Screen Detail:</b>
        </p>

        <ol>
            <li class="TextFormat1">
                <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                    <b>Search : To Show The List Of MR For Selected Period</b>
                </p>
                <li class="TextFormat1">
                    <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                        <b>Create : To Create New MR</b>
                    </p>
                    <li class="TextFormat1">
                        <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                            <b>Download : To Download Search MR Data</b>
                        </p>
        </ol>










        <p lang="en-IN" class="western TextFormat1" style="text-indent: 0.5in; margin-bottom: 0.14in">
            <img src="Images/MR Details.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
        </p>


        <p lang="en-IN" style="margin-left: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>MR Details
Screen:</b>
        </p>





        <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Screen Detail:</b>
        </p>

        <ol>

            <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                <b>Click On "Details" Link. It Will Show Details Of Material Which Are Added At Create Time. </b>
            </p>
        </ol>



        <p lang="en-IN" class="western TextFormat1" style="text-indent: 0.5in; margin-bottom: 0.14in">
            <img src="Images/Create MR.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
        </p>


        <p lang="en-IN" style="margin-left: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Create MR 
Screen:</b>
        </p>





        <%--<p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Screen Detail:</b>
        </p>--%>
        <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Master Part :</b>
        </p>

        <ol>
            <li class="TextFormat1">
                <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                    <b>Doc. Type : 
	MMR – Material Request</b> (Auto Filled)
                </p>

                <li class="TextFormat1">
                    <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                        <b>MR No. : 
	Material Request Number</b> (Auto Filled)
                    </p>

                    <li class="TextFormat1">
                        <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                            <b>MR Date. : 
	MR Creataion Date </b>(Auto Filled / Can Change Also)
                        </p>
                         <li class="TextFormat1">
                            <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                                <b>Remark : 
	Free text for any special instruction. </b>
                            </p>
                        <li class="TextFormat1">
                            <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                                <b>Department : 
	For Which Department The Material Is Required It Needs To Be Selected. </b>
                            </p>
        </ol>

        <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Detail Part :</b>
        </p>

        <ol>
            <li class="TextFormat1">
                <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                    <b>Item Desc. : 
	Enter Item Description Which Is Not In Our System</b>
                </p>

                <li class="TextFormat1">
                    <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                        <b>Item Spec. :
	Item Specification</b>
                    </p>

                    <li class="TextFormat1">
                        <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                            <b>Item Group :
	Select Item Group For Material Like, Mobile Parts, Mobile Device, Etc. </b>(Mobile Parts : MP Is Selected By Default)
                        </p>

                        <li class="TextFormat1">
                            <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                                <b>Qty : 
	Quantity to request. </b>
                            </p>

                            <li class="TextFormat1">
                                <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                                    <b>UOM : 
	Select Unit Of Measurement. </b>
                                </p>


                                <li class="TextFormat1">
                                    <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                                        <b>Requisitioner :
	Name Of Actual User Who  Require The Material. </b>
                                    </p>

                                    <li class="TextFormat1">
                                        <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                                            <b>Plant Code :
	Select PLant Name Where Material Is Require. </b>
                                        </p>

                                        <li class="TextFormat1">
                                            <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                                                <b>Location :
	Select Location Where Material Is Require. </b>
                                            </p>


                                            <li class="TextFormat1">
                                                <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                                                    <b>Cost Center :
	Auto Fill On the Basis Of Plant Code. </b>
                                                </p>

                                                <li class="TextFormat1">
                                                    <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                                                        <b>Tracking Number :
	Reference No. For Example Job No. For Which Material Is Required. </b>
                                                    </p>

                                                    <li class="TextFormat1">
                                                        <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                                                            <b>Remark :
	Free text for any special instruction. </b>
                                                        </p>


                                                        <p class="western" lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in"><b>User Can Enter Multiple Material Request In One MR Screen.</b></p>
                                                        <p class="western" lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in"><b>Add Material Detail Button Will Generate A Temporary View Of Added Material.</b></p>

                                                        <p class="western" lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in"><b>Save MR Button Will Save Material Request.</b></p>
                                                        <p class="western" lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in"><b>Cancel MR Button Will Discard Material Request.</b></p>
        </ol>



    </div>

</asp:Content>
