<%@ Page Title="" Language="C#" MasterPageFile="~/HelpViewer/Help.Master" AutoEventWireup="true" CodeBehind="frmAprvPR.aspx.cs" Inherits="ShERPa360net.HelpViewer.frmAprvPR" %>

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
        <label style="vertical-align: top; font-weight: bold; font-size: 16px">Material Management System --> Approve PR  :</label>
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
                    <p lang="en-IN">Approve PR</p>
                </td>
            </tr>
            <tr valign="TOP">
                <td width="130" class="TextFormat">
                    <p lang="en-IN"><b>Description :</b></p>
                </td>
                <td width="423" class="TextFormat">
                    <p lang="en-IN">To Approve Created PR</p>
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
                    <p lang="en-IN">MM --> Indent --> Approve PR</p>
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
            <img src="Images/ViewAprvPR.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
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
                    <b>Search : To Show The List Of PR For Selected Period</b>
                </p>
                <li class="TextFormat1">
                    <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                        <b>Download : To Download Search Data</b>
                    </p>
        </ol>










        <p lang="en-IN" class="western TextFormat1" style="text-indent: 0.5in; margin-bottom: 0.14in">
            <img src="Images/Aprv PR Detail.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
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
                <b>Click On "Details" Link. It Will Show Details Of Item Which are Require At Paricular Location. </b>
            </p>
             <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                <b>This Screen Also Having A Functionality To Approve / Reject PR. </b>
            </p>
             <p lang="en-IN" style="margin-bottom: 0.14in" class="TextFormat1">
                <b>Approver Can Also Approve / Reject Line Item.  </b>
            </p>

        </ol>



        <p lang="en-IN" class="western TextFormat1" style="text-indent: 0.5in; margin-bottom: 0.14in">
            <img src="Images/Aprv PR Popup.png" name="Picture 14" align="BOTTOM" width="850" height="450" border="0">
        </p>


        <p lang="en-IN" style="margin-left: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Approve PR 
Screen:</b>
        </p>





        <%--<p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>Screen Detail:</b>
        </p>--%>
        <p lang="en-IN" style="margin-left: 0.5in; text-indent: 0.5in; margin-bottom: 0.14in" class="TextFormat1">
            <b>This Popup Will Show PR Number And Approve / Reject Buttons.</b>
        </p>

      

  



    </div>

</asp:Content>
