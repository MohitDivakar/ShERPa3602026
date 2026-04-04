using com.citizen.sdk.LabelPrint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Diagnostics;



namespace ShERPa360net.UTILITY
{
    public partial class frmJIOLabelPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {


                LabelPrinter printer = new LabelPrinter();
                string[] aa = null;
                int ab = 0;

                aa = printer.SearchLabelPrinter(LabelConst.CLS_PORT_USB, 10, out ab);

                string type = Convert.ToString(aa[0]);

                LabelDesign design = new LabelDesign();

                

                #region Comment Code...

                //design.DrawLine(20, 20, 40, 20, 2);
                //design.DrawLine(20, 20, 20, 40, 2);
                //design.DrawLine(60, 20, 40, 40, 2);
                //design.DrawLine(20, 60, 40, 40, 2);


                //design.DrawLine(5, 8, 5, 305, 2);
                //design.DrawLine(5, 8, 305, 8, 2);
                //design.DrawLine(5, 305, 305, 305, 2);
                //design.DrawLine(305, 8, 305, 305, 2);


                //design.DrawLine(5, 120, 305, 120, 2);
                //design.DrawLine(5, 140, 305, 140, 2);
                //design.DrawLine(5, 160, 305, 160, 2);
                //design.DrawLine(5, 180, 305, 180, 2);
                //design.DrawLine(5, 200, 305, 200, 2);
                //design.DrawLine(5, 220, 305, 220, 2);
                //design.DrawLine(5, 240, 305, 240, 2);
                //design.DrawLine(5, 260, 305, 260, 2);
                //design.DrawLine(5, 280, 305, 280, 2);

                //design.DrawLine(95, 120, 95, 280, 2);
                //design.DrawLine(150, 200, 150, 280, 2);
                //design.DrawLine(255, 200, 255, 280, 2);

                //design.DrawTextPtrFont("JIO Phone Swap Device", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_12, 50, 15);

                //design.DrawTextPtrFont("RETURN TO BER TAG", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_8, 100, 285);



                //design.DrawTextPtrFont("L3 VENDOR NAME", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 8, 265);

                //design.DrawTextPtrFont("RRL JOB NO", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 8, 245);

                //design.DrawTextPtrFont("L3 VENDOR JOB NO", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 8, 225);

                //design.DrawTextPtrFont("MODEL", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 8, 205);

                //design.DrawTextPtrFont("RSN", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 8, 185);

                //design.DrawTextPtrFont("IMEI", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 8, 165);

                //design.DrawTextPtrFont("RRL SYMPTOM", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 8, 145);

                //design.DrawTextPtrFont("BER REASON", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 8, 125);



                //design.DrawTextPtrFont("LOCATION", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 155, 265);

                //design.DrawTextPtrFont("RRL JOB DATE", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 155, 245);

                //design.DrawTextPtrFont("VENDOR RETURN DATE", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 155, 225);

                //design.DrawTextPtrFont("JC Code", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 155, 205);

                //Fill data

                //design.DrawTextPtrFont("QARMATEK", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 100, 265);

                //design.DrawTextPtrFont("8045335873", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 100, 245);

                //design.DrawTextPtrFont("699242", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_8, 100, 225);

                //design.DrawTextPtrFont("F-320B", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 100, 205);

                //design.DrawTextPtrFont("RMPBGLGH1815808", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 100, 185);

                //design.DrawTextPtrFont("353944309961862", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 100, 165);

                //design.DrawTextPtrFont("AUDIO : MIC NOT WORKING", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 100, 145);

                //design.DrawTextPtrFont("MIC COMPONENT TRACK PAD OFF", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 100, 125);


                //design.DrawTextPtrFont("A'BAD", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 260, 265);

                //design.DrawTextPtrFont("30-07-2021", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 260, 245);

                //design.DrawTextPtrFont("31-08-2021", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 260, 225);

                //design.DrawTextPtrFont("I194", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_6, 260, 205);




                //design.DrawTextPtrFont("920002815", LabelConst.CLS_LOCALE_JP, LabelConst.CLS_PRT_FNT_TRIUMVIRATE_B, LabelConst.CLS_RT_NORMAL, 1, 1, LabelConst.CLS_PRT_FNT_SIZE_12, 100, 45);

                #endregion

                design.DrawBarCode("920002815", LabelConst.CLS_BCS_CODE128, LabelConst.CLS_RT_NORMAL, 5, 5, 45, 50, 70, LabelConst.CLS_BCS_TEXT_HIDE);



                int errcode;
                CitizenPrinterInfo[] list = printer.SearchCitizenPrinter(
                LabelConst.CLS_PORT_USB, 3, out errcode);


                printer.SetFormatAttribute(1);
                int ret = printer.Connect(LabelConst.CLS_PORT_USB, type);


                int qw = printer.PrinterCheck();
                CitizenPrinterInfo[] aas = printer.SearchCitizenPrinter(LabelConst.CLS_PORT_USB, 10, out ab);



                int result;


                //int result = printer1.Connect(3, "USB002");

                //if (LabelConst.CLS_SUCCESS == result)
                //{
                int printDarkness = printer.GetPrintDarkness();


                result = printer.Connect(3, "USB002");


                if (LabelConst.CLS_PROPERTY_DEFAULT == printDarkness)
                {
                    printer.SetPrintDarkness(10);
                     printer.Print(design, 1);
                }
                else
                {
                    result = printer.Print(design, 1);
                }
                //}

                printer.Disconnect();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}