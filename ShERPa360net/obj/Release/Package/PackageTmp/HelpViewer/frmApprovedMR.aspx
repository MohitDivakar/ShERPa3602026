<%@ Page Title="" Language="C#" MasterPageFile="~/HelpViewer/Help.Master" AutoEventWireup="true" CodeBehind="frmApprovedMR.aspx.cs" Inherits="ShERPa360net.HelpViewer.frmApprovedMR" %>

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
        <label style="vertical-align: top; font-weight: bold; font-size: 16px">Material Management System --> Approved MR  :</label>
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
                    <p lang="en-IN">Approved MR</p>
                </td>
            </tr>
            <tr valign="TOP">
                <td width="130" class="TextFormat">
                    <p lang="en-IN"><b>Description :</b></p>
                </td>
                <td width="423" class="TextFormat">
                    <p lang="en-IN">To Create PR / Issue Material Against MR</p>
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
                    <p lang="en-IN">MM --> Indent --> Approved MR</p>
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
            <img src="Images/Approved MR.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
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
                        <b>Download : To Download Search Data</b>
                    </p>
        </ol>










        <p lang="en-IN" class="western TextFormat1" style="text-indent: 0.5in; margin-bottom: 0.14in">
            <img src="Images/Approved MR Details.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
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
                <b>Click On "Details" Link. It Will Show Details Of Material . </b>
            </p>
            <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                <b>It Will Show The Approver Name, Approved Time And Reason Of Approval. </b>
            </p>
            

        </ol>



        <p lang="en-IN" class="western TextFormat1" style="text-indent: 0.5in; margin-bottom: 0.14in">
            <img src="Images/Approved Edit.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
        </p>


        <p lang="en-IN" style="margin-left: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Approved MR 
Popup:</b>
        </p>





        <%--<p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Screen Detail:</b>
        </p>--%>
        <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>It Will Show Material Details.</b>
        </p>

                <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>It Is Having One Edit Button. If You Click On That It Will Open The Textbox To Enter Itemcode.</b>
        </p>




         <p lang="en-IN" class="western TextFormat1" style="text-indent: 0.5in; margin-bottom: 0.14in">
            <img src="Images/MR Item Code.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
        </p>


        <p lang="en-IN" style="margin-left: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Approved MR 
Popup:</b>
        </p>





        <%--<p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Screen Detail:</b>
        </p>--%>
        <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>User Can Enter Item Description To Search Itemcode</b>
        </p>

                <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>After Entering Itemcode User Can Update It For Further Process.</b>
        </p>




          <p lang="en-IN" class="western TextFormat1" style="text-indent: 0.5in; margin-bottom: 0.14in">
            <img src="Images/MR Issue Button.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
        </p>


        <p lang="en-IN" style="margin-left: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Approved MR 
Popup:</b>
        </p>





        <%--<p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Screen Detail:</b>
        </p>--%>
        <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>If Material Is Available In Store Then It Will Show "ISSUE" Button.</b>
        </p>

                <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>If User Click On "ISSUE" Button It Will Redirect to IST Screen.</b>
        </p>





         <p lang="en-IN" class="western TextFormat1" style="text-indent: 0.5in; margin-bottom: 0.14in">
            <img src="Images/MR PR Button.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
        </p>


        <p lang="en-IN" style="margin-left: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Approved MR 
Popup:</b>
        </p>





        <%--<p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Screen Detail:</b>
        </p>--%>
        <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>If Material Is Not Available In Store Then It Will Show "PR" Button.</b>
        </p>

                <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>If User Click On "PR" Button It Will Redirect to Create PR Screen.</b>
        </p>



    </div>
</asp:Content>
