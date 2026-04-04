using Newtonsoft.Json;
using RestSharp;
using ShERPa360net.Class;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ShERPa360net
{
    public class MainClass
    {
        public SqlConnection ConnSherpa = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringSherpa"].ConnectionString);
        public SqlConnection ConnQuike = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringQuike"].ConnectionString);
        public SqlConnection ConnMI = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnStringMI"].ConnectionString);
        public int intCmpId = 1;
        public string PORT = "587";
        public static string strQuikeSegment = "1003";
        public static string CommonQTEKCardNo = "QTEK149REPAIRNOW";
        public static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        public DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        public const int BackGroundUser = 17;
        public string strBackGroundUser = "17";
        public static string strBasePackMRP = "149.00";
        public static string strBasePackMRate = "50.00";
        public string EmailID = "info@qarmatek.com";
        public string Password = "Hof75626";
        public int intTrigeerId = 1;

        //public MainClass()
        //{
        //    if (TestForServer("http://14.98.132.190", 7989) == true)
        //    {
        //        ConnSherpa = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSherpa3"].ConnectionString);
        //    }
        //    else
        //    {
        //        ConnSherpa = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringSherpa2"].ConnectionString);
        //    }
        //}


        public static bool TestForServer(string address, int port)
        {
            int timeout = 500;
            if (ConfigurationManager.AppSettings["RemoteTestTimeout"] != null)
                timeout = int.Parse(ConfigurationManager.AppSettings["RemoteTestTimeout"]);
            var result = false;
            try
            {
                using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IAsyncResult asyncResult = socket.BeginConnect(address, port, null, null);
                    result = asyncResult.AsyncWaitHandle.WaitOne(timeout, true);
                    socket.Close();
                }
                return result;
            }
            catch { return false; }
        }

        public string LtrimZero(string str, string strChar)
        {
            str = str.Replace(strChar, " ");
            str = str.TrimStart();
            str = str.Replace(" ", strChar);
            return str;
        }

        public string strReplicate(string str, int intD)
        {
            string functionReturnValue = null;
            int i = 0;
            functionReturnValue = "";
            for (i = 1; i <= intD; i++)
            {
                functionReturnValue = functionReturnValue + str;
            }
            return functionReturnValue;
        }

        public string strConvertZeroPadding(string strText, string padChar = "0", int intTimes = 10)
        {
            strText = strReplicate(padChar, intTimes - strText.Length) + strText;
            return strText;
        }

        public string RevertString(string str)
        {
            string reversestr = str;

            StringBuilder reversedBuilder = new StringBuilder();
            for (int i = str.Length - 1; i >= 0; i--)
            {
                reversedBuilder.Append(str[i]);
            }
            reversestr = reversedBuilder.ToString();

            return reversestr;
        }

        public void ShowMessage(Page Page, string Message)
        {
            //Page.ClientScript.RegisterClientScriptBlock(
            //   Page.GetType(),
            //   "MessageBox",
            //   "<script language='javascript'>alert('" + Message + "');</script>"
            //);

            //Page.ClientScript.RegisterStartupScript(
            //    Page.GetType(),
            //    "MessageBox",
            //    "alert('" + Message + "');"
            // );
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + Message + "');", true);
        }

        public void ShowMessageRedirect(Page Page, string Message, string URL)
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + Message + "');window.location.href = '" + URL + "';", true);
        }

        public DataTable AuthenticateLogin(string strUserCode, string strPassword)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_SHERPAUSER_LOGIN", ConnSherpa);
            cmd.Parameters.AddWithValue("@USERCODE", strUserCode);
            cmd.Parameters.AddWithValue("@PASSWORD", strPassword);
            cmd.Parameters.AddWithValue("@STATUS", 1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }



        public DataTable SysAuthenticateLogin(string strUserCode, string strPassword)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SYS_SELECT_SHERPAUSER_LOGIN", ConnSherpa);
            cmd.CommandTimeout = 500;
            cmd.Parameters.AddWithValue("@USERCODE", strUserCode);
            //cmd.Parameters.AddWithValue("@PASSWORD", strPassword);
            cmd.Parameters.AddWithValue("@STATUS", 1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }


        public DataTable SELECT_USER_DEPT(int EMPID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_USER_DEPT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@EMPID", EMPID);
                cmd.Parameters.AddWithValue("@CMPID", intCmpId);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public string Encrypt(string PASSWORD)
        {

            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(PASSWORD);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    PASSWORD = Convert.ToBase64String(ms.ToArray());
                }
            }

            return PASSWORD;
        }


        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public DataTable GetPRData(string PRNO, string FROMDATE, string TODATE, string MAINQUERY, int CREATEBY, int DEPTCD)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PR", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@PRNO", PRNO);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@DEPTCD", DEPTCD);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        //SP_SELECT_MATERIALINWARDFROMPODATA
        public DataTable GetISTData(int CMPID, string DOCTYPE, string DOCNO, string FROMDATE, string TODATE, string PLANTCD)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_MMMST", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetEachMaterialDetails(string DOCNO, int CMPID, int ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_TRAN_MM_MATERIALRETURNDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public string SaveMaterialReturn(Int64 FINYEAR, string DOCNO, string DOCTYPE, string DOCDATE, string TRANCODE, string REFDOCNO, string REMARK, string MODE, GridView grvListItem, string UserId)
        {
            MainClass objMainClass = new MainClass();
            bool IsAddUpdate = false;
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DOCNO = MAXPRNO(DOCTYPE, "MR");
                    #region Insert TRAN_MMMST...
                    SqlCommand cmdM = new SqlCommand("SP_PO_MATERIAL_INWARDTRAN_MMMSTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@FINYEAR", FINYEAR);
                    cmdM.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                    cmdM.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    cmdM.Parameters.AddWithValue("@DOCDATE", setDateFormat(DOCDATE, true));
                    cmdM.Parameters.AddWithValue("@POSTDATE", setDateFormat(DOCDATE, true));
                    cmdM.Parameters.AddWithValue("@TRANCODE", strConvertZeroPadding(TRANCODE));
                    cmdM.Parameters.AddWithValue("@REFDOCNO", strConvertZeroPadding(REFDOCNO));
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    if (result > 0)
                    {
                        IsAddUpdate = true;
                    }
                    #endregion

                    #region Update MMNORANGE...
                    if (MODE == "I")
                    {
                        SqlCommand MMcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                        MMcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                        MMcmd.Parameters.AddWithValue("@CURRNO", Convert.ToInt64(DOCNO));
                        MMcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                        MMcmd.Parameters.AddWithValue("@DOCTYPE", "MR");
                        MMcmd.CommandType = CommandType.StoredProcedure;
                        MMcmd.Connection.Open();
                        result = MMcmd.ExecuteNonQuery();
                        MMcmd.Connection.Close();
                        if (result > 0)
                        {
                            IsAddUpdate = true;
                        }
                    }
                    #endregion

                    for (int i = 0; i < grvListItem.Rows.Count; i++)
                    {
                        GridViewRow row = grvListItem.Rows[i];
                        SqlCommand cmdD = new SqlCommand("SP_PO_MATERIAL_INWARDTRAN_MMCRUDOPERATION", ConnSherpa);
                        cmdD.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                        cmdD.Parameters.AddWithValue("@FINYEAR", FINYEAR);
                        cmdD.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                        cmdD.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                        cmdD.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblID")).Text);
                        cmdD.Parameters.AddWithValue("@PONO", ((Label)row.FindControl("lblPoNo")).Text);
                        cmdD.Parameters.AddWithValue("@POSRNO", ((Label)row.FindControl("lblPoSrNo")).Text);
                        cmdD.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblUOMID")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblItemId")).Text);
                        cmdD.Parameters.AddWithValue("@QTY", ((Label)row.FindControl("lblReturnQty")).Text);
                        cmdD.Parameters.AddWithValue("@CHLNQTY", ((Label)row.FindControl("lblchalantQty")).Text);
                        cmdD.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblRate")).Text);
                        cmdD.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblCAAmount")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblRemarks")).Text);
                        cmdD.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblCostCenter")).Text);
                        cmdD.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblPrfcnt")).Text);
                        cmdD.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblPlant")).Text);
                        cmdD.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblLocation")).Text);
                        cmdD.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGlCde")).Text);
                        cmdD.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblAssetCode")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblItemDescription")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblItemGroupId")).Text);
                        cmdD.Parameters.AddWithValue("@TRACKNO", ((Label)row.FindControl("lblTrackNo")).Text);
                        cmdD.Parameters.AddWithValue("@ACPTQTY", ((Label)row.FindControl("lblAcceptedqty")).Text);
                        cmdD.Parameters.AddWithValue("@MMFINYEAR", ((Label)row.FindControl("lblMMFinalYear")).Text);
                        cmdD.Parameters.AddWithValue("@MMDOCNO", ((Label)row.FindControl("lblMMDocNo")).Text);
                        cmdD.Parameters.AddWithValue("@MMSRNO", ((Label)row.FindControl("lblMMSrNo")).Text);
                        cmdD.Parameters.AddWithValue("@REQUESTFROM", "MR");
                        cmdD.Parameters.AddWithValue("@MODE", MODE);
                        cmdD.CommandType = CommandType.StoredProcedure;
                        cmdD.Connection.Open();
                        result = cmdD.ExecuteNonQuery();
                        cmdD.Connection.Close();
                        if (result > 0)
                        {
                            IsAddUpdate = true;
                        }
                    }
                    scope.Complete();
                    scope.Dispose();
                }
                if (IsAddUpdate)
                {
                    return DOCNO;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                IsAddUpdate = false;
                Console.WriteLine(ex);
                return "";
            }
        }



        public DataTable GetMaterialDetailFromPoDocNo(string VENDCODE, string PONO, string DOCNO, string DOCTYPE, Int64 MMFINYEAR, Int64 MMSrnO, string MODE = "I")
        {
            MainClass objMainClass = new MainClass();
            DataTable dtMaerialDetail = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MATERIALRETURN_TRAN_MMCRUDEOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@VENDCODE", (VENDCODE.Length > 0 ? strConvertZeroPadding(VENDCODE) : ""));
                cmd.Parameters.AddWithValue("@PONO", (PONO.Length > 0 ? strConvertZeroPadding(PONO) : ""));
                cmd.Parameters.AddWithValue("@MMDOCNO", (DOCNO.Length > 0 ? strConvertZeroPadding(DOCNO) : ""));
                cmd.Parameters.AddWithValue("@MMFINYEAR", MMFINYEAR);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@SRNO", MMSrnO);
                cmd.Parameters.AddWithValue("@MODE", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtMaerialDetail);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dtMaerialDetail;
        }

        //GetMRData
        public DataTable GetMRData(string MRNO, string FROMDATE, string TODATE, string MAINQUERY, int CREATEBY, int DEPTCD)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MR", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@MRNO", MRNO);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@DEPTCD", DEPTCD);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public DataTable GetOpenMRData(int CMPID, string MRNO, string FROMDATE, string TODATE, int APPROVED, string MAINQUERY)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_OPEN_MR", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MRNO", MRNO);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@APPROVED", APPROVED);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public DataTable GetReqRcptName(int CMPID, string REQTYPE, string PLANTCD, string NAME, string MOBILENO, string MAILTO, string MAILCC, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_REQ_TO_SENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@REQTYPE", REQTYPE);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@NAME", NAME);
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                cmd.Parameters.AddWithValue("@MAILTO", MAILTO);
                cmd.Parameters.AddWithValue("@MAILCC", MAILCC);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GETCOUPONDATA(int CMPID, string COUPONCODE, int STATUS, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_COUPON", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@COUPONCODE", COUPONCODE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetSingleWindowTracker(int CMPID, string CREATEDATE, string SEGMENT, string PLANTCODE, int JOBSTATUS, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SINGLEWINDOW_TRACKER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CREATEDATE", CREATEDATE);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);
                cmd.Parameters.AddWithValue("@JOBSTATUS", JOBSTATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetMRStatusData(int CMPID, string MRNO, string FROMDATE, string TODATE, int ACTION, string MAINQUERY)
        {
            //SP_MR_STATUS
            DataTable dt = new DataTable();
            MRNO = strConvertZeroPadding(MRNO);
            SqlCommand cmd = new SqlCommand("SP_MR_STATUS", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MRNO", MRNO);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetApprover(int CMPID, string MRNO, int ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MR_STATUS", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        //GetAPRVPRData
        public DataTable GetAPRVPRData(string PRNO, string FROMDATE, string TODATE, string MAINQUERY, int CREATEBY, string DEPTCD, int STAGESEQ, int APRVBY, string PLANTCD)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PR_BY_SEQ", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@PRNO", PRNO);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@DEPTCD", DEPTCD);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.Parameters.AddWithValue("@STAGESEQ", STAGESEQ);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@APRVBY", APRVBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public DataTable GetPOforApprove(int CMPID, string PRNO, string PONO, string FROMDT, string TODT, string MAINQUERY, int APRVBY, int STAGESEQ, string PLANTCD, decimal MAXAMOUNT, decimal MINAMOUNT, string ACTION, string DEPTCODE = "")
        {
            PONO = strConvertZeroPadding(PONO);
            PRNO = strConvertZeroPadding(PRNO);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PO_FOR_APPROVE", ConnSherpa);
            //SqlCommand cmd = new SqlCommand("SP_SELECT_PO_FOR_APPROVE_TEST", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PRNO", PRNO);
                cmd.Parameters.AddWithValue("@PONO", PONO);
                cmd.Parameters.AddWithValue("@FROM", FROMDT == "" ? FROMDT : setDateFormat(FROMDT, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TO", TODT == "" ? TODT : setDateFormat(TODT, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.Parameters.AddWithValue("@APRVBY", APRVBY);
                cmd.Parameters.AddWithValue("@STAGESEQ", STAGESEQ);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@MAXAMOUNT", MAXAMOUNT);
                cmd.Parameters.AddWithValue("@MINAMOUNT", MINAMOUNT);
                cmd.Parameters.AddWithValue("@DEPTCD", DEPTCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetQuotforApprove(int CMPID, string QUOTNO, string FROMDT, string TODT, string MAINQUERY, int APRVBY, int STAGESEQ, string PLANTCD, decimal MAXAMOUNT, decimal MINAMOUNT, string ACTION, string DEPTCODE = "")
        {
            if (QUOTNO != "")
            {
                QUOTNO = strConvertZeroPadding(QUOTNO);
            }
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_QO_FOR_APPROVE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@QUOTNO", QUOTNO);
                cmd.Parameters.AddWithValue("@FROM", FROMDT == "" ? FROMDT : setDateFormat(FROMDT, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TO", TODT == "" ? TODT : setDateFormat(TODT, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.Parameters.AddWithValue("@APRVBY", APRVBY);
                cmd.Parameters.AddWithValue("@STAGESEQ", STAGESEQ);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@MAXAMOUNT", MAXAMOUNT);
                cmd.Parameters.AddWithValue("@MINAMOUNT", MINAMOUNT);
                cmd.Parameters.AddWithValue("@DEPTCD", DEPTCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 300;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }



        public DataTable GetWAData(string APPNAME, int STATUS, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELLER_API", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@APPNAME", APPNAME);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public int InsertPaymentLink(int CMPID, string CUSTNAME, string CONTACTNO, string EMAILID, string DESCRIPTION, decimal AMOUNT, string REFID, string REMARKS, DateTime EXPIREDBY, string PAYMENTLINK, string STATUS,
            string ENVIRONMENT, string PAYMENTGATEWAY, int CREATEBY, string PMTID, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_PAYMENTLINK", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
                cmd.Parameters.AddWithValue("@DESCRIPTION", DESCRIPTION);
                cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);
                cmd.Parameters.AddWithValue("@REFID", REFID);
                cmd.Parameters.AddWithValue("@REMARKS", REMARKS);
                cmd.Parameters.AddWithValue("@EXPIREDBY", EXPIREDBY);
                cmd.Parameters.AddWithValue("@PAYMENTLINK", PAYMENTLINK);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ENVIRONMENT", ENVIRONMENT);
                cmd.Parameters.AddWithValue("@PAYMENTGATEWAY", PAYMENTGATEWAY);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@EXTFIELD1", PMTID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }


        public DataTable SelectPaymentLinkData(int CMPID, string FROMDATE, string TODATE, string CUSTNAME, string REFID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PAYMENTLINK", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                cmd.Parameters.AddWithValue("@REFID", REFID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? FROMDATE : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? TODATE : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        public DataTable GetDealerSearchLog(int CMPID, string FROMDATE, string TODATE, string CUSTCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DEALERSEARCH_LOG", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? FROMDATE : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? TODATE : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@CUSTCODE", CUSTCODE == "" ? "" : strConvertZeroPadding(CUSTCODE));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        public DataTable GetOnSales(int CMPID, string PMTERMS, string FROMDATE, string TODATE, string PONO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GETONSALESDATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PMTTERMS", PMTERMS);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@PONO", PONO != "" ? strConvertZeroPadding(PONO) : "");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetSeqApprovedPO(int APRVBY, string FROMDATE, string TODATE, string DOCTYPE, string DOCNO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SEQPOAPPROVED", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@APRVBY", APRVBY);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? FROMDATE : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? TODATE : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@DOCNO", DOCNO != "" ? strConvertZeroPadding(DOCNO) : "");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //SP_SELECT_APPROVED_MR
        public DataTable GetApprovedMR(string MRNO, string FROMDATE, string TODATE, string DOCTYPE, int STATUS, int CMPID, string PLANTCD)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_APPROVED_MR", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                cmd.Parameters.AddWithValue("@MAINQUERY", "");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //GetAPRVMRData
        public DataTable GetAPRVMRData(string MRNO, string FROMDATE, string TODATE, string MAINQUERY, int CREATEBY, string DEPTCD, int STAGESEQ, int APRVBY, string PLANTCD)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MR_BY_SEQ", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@MRNO", MRNO);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@DEPTCD", DEPTCD);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.Parameters.AddWithValue("@STAGESEQ", STAGESEQ);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@APRVBY", APRVBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public DataTable GetBudgetData(string PLANTCODE, int STATUS, string BUDGETCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BUDGETREPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@BUDGETCODE", BUDGETCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public List<string> GetItemData(string ItemDesc)
        {
            //SP_SELECT_ITEMMSATER
            List<string> itemcode = new List<string>();
            SqlCommand cmd = new SqlCommand("SP_SELECT_ITEMMSATER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ITEMDESC", ItemDesc);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                using (SqlDataReader sda = cmd.ExecuteReader())
                {
                    while (sda.Read())
                    {
                        itemcode.Add(sda["ITEMCODE"].ToString() + " - " + sda["Desciption"].ToString());
                    }
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return itemcode;
        }

        public List<string> GetLikeJobData(string ItemDesc, string ACTION)
        {
            //SP_SELECT_ITEMMSATER
            List<string> itemcode = new List<string>();
            SqlCommand cmd = new SqlCommand("SP_CROMA_RATECARD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@IETMDESC", ItemDesc);
                //cmd.Parameters.AddWithValue("@ACTION", "GETLIKEJOBID");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                using (SqlDataReader sda = cmd.ExecuteReader())
                {
                    while (sda.Read())
                    {
                        itemcode.Add(sda["ITEM"].ToString());
                    }
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return itemcode;
        }

        public DataTable GetCavitakProject(int CMPID, string SEGMENT, string FROMDATE, string TODATE, string STAGEID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CAVITAK_PROJECT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@STAGEID", STAGEID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetInvData(int CMPID, string JOBID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_INWARD_INVENTORY", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dt;
        }

        public DataTable GetQCData(int CMPID, string JOBID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_QCPARAMETER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return dt;
        }

        public DataTable dtGetLikeJobData(string ItemDesc, string BRAND, string CATEGORY, string ACTION)
        {
            //SP_SELECT_ITEMMSATER
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMA_RATECARD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@IETMDESC", ItemDesc);
                cmd.Parameters.AddWithValue("@BRAND", BRAND);
                cmd.Parameters.AddWithValue("@CATEGORY", CATEGORY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }




        //SELECT_PR_LOG

        public DataTable SELECT_REQUISITION_LOG(string PRNO)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_PR_LOG", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public DataTable GetPRLogData(string PRNO, string FROMDATE, string TODATE, string MAINQUERY, int CREATEBY, int DEPTCD)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_LOG_PR", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@PRNO", PRNO);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@DEPTCD", DEPTCD);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public DataTable GetPOData(int CMPID, string PRNO, string PONO, string FROMDATE, string TODATE, string MAINQUERY)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            //SP_SELECT_PODATA
            SqlCommand cmd = new SqlCommand("SP_SELECT_PODATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PRNO", PRNO);
                cmd.Parameters.AddWithValue("@PONO", PONO);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetPartReqData(int CMPID, string SEGMENT, string ID, string JOBID, string FROMDATE, string TODATE, int STATUS, string CREATEBY, string @QUERY)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PARTREQ_LIST", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@ID", ID);
                if (JOBID != null && JOBID != "" && JOBID != string.Empty)
                {
                    cmd.Parameters.AddWithValue("@JOBID", JOBID);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@JOBID", "");
                }
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? (object)DBNull.Value : Convert.ToDateTime(FROMDATE));
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? (object)DBNull.Value : Convert.ToDateTime(TODATE));
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@QUERY", QUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetItemDetailByID(int CMPID, string ITEMID)
        {
            DataTable dt = new DataTable();

            //SP_SELECT_ITEMDTL_BY_ID_CODE
            SqlCommand cmd = new SqlCommand("SP_SELECT_ITEMDTL_BY_ID_CODE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMID", ITEMID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        //SP_GET_SPLIT_ITEM_DETAIL
        public DataTable GetSplitItem(int CMPID, string MAKE, string MODEL, string PLANTCDID, string LOCCDID, string PLANTCD, string LOCCD)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GET_SPLIT_ITEM_DETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);

                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCDID);
                cmd.Parameters.AddWithValue("@LOCCD", LOCCDID);
                cmd.Parameters.AddWithValue("@ITEMPLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@ITEMLOCCD", LOCCD);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;

        }


        public DataTable GetPartReqSummery(int CMPID, string SEGMENT, string ID, string JOBID, string FROMDATE, string TODATE, string @QUERY)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PARTREQ_SUMMERY", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@ID", ID);
                if (JOBID != null && JOBID != "" && JOBID != string.Empty)
                {
                    cmd.Parameters.AddWithValue("@JOBID", JOBID);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@JOBID", "");
                }
                cmd.Parameters.AddWithValue("@FROMDATE", Convert.ToDateTime(FROMDATE));
                cmd.Parameters.AddWithValue("@TODATE", Convert.ToDateTime(TODATE));
                cmd.Parameters.AddWithValue("@QUERY", QUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }



        public DataTable GetNewItemReq(int CMPID, string FROMDATE, string TODATE, int STATUS, string QUERY)
        {
            //SP_NEW_ITEM_ADD_REQ
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_NEW_ITEM_ADD_REQ", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", Convert.ToDateTime(FROMDATE));
                cmd.Parameters.AddWithValue("@TODATE", Convert.ToDateTime(TODATE));
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@QUERY", QUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                //return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetPRDetails(string PRNO, int ACTION)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PR_DETAILS", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //SELECT_TRAN_MM_ISTDETAIL
        public DataTable GetISTDetails(string DOCNO, int CMPID, int ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_TRAN_MM_ISTDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //SP_SELECT_DOCTYPE_MMDTL
        public DataTable GetMMDtlDoctypewise(int CMPID, string DOCTYPE, string DOCNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_DOCTYPE_MMDTL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCTYE", DOCTYPE);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //SP_SELECT_IS_MMDTL
        public DataTable GetISData(int CMPID, string DOCTYPE, string DOCNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_IS_MMDTL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCTYE", DOCTYPE);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public bool UpdateMRDtl(string MRNO, string ID, string ITEMCODE, int CMPID, int ACTION, string ISSUEQTY, string UOMID, string QTY)
        {
            bool result = false;
            SqlCommand cmd = new SqlCommand("SP_UPDATE_MR_ITEMCODE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MRNO", MRNO);
                cmd.Parameters.AddWithValue("@ID", ID == "" ? 0 : Convert.ToInt32(ID));
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@ISSUEQTY", ISSUEQTY == "" ? 0 : Convert.ToDecimal(ISSUEQTY));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@UOM", UOMID == "" ? 0 : Convert.ToInt32(UOMID));
                cmd.Parameters.AddWithValue("@MRQTY", QTY == "" ? 0 : Convert.ToDecimal(QTY));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                result = true;

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return result;
        }

        public bool UpdateMRDtlStatus(string MRNO, string ID, int STATUS, int CMPID, int ACTION)
        {

            bool result = false;
            SqlCommand cmd = new SqlCommand("SP_UPDATE_MR_ITEMCODE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MRNO", MRNO);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                result = true;

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return result;
        }

        public bool UpdatePRDtlStatus(string PRNO, string ID, int STATUS, int CMPID, int ACTION)
        {
            bool result = false;
            SqlCommand cmd = new SqlCommand("SP_UPDATE_PR_DTL_DATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PRNO", PRNO);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                result = true;

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return result;
        }



        public decimal SP_GET_STOCK_FUNCTION(int CMPID, int FINYEAR, string ITEMCODE, string ASONDATE, string PLANT, string LOCATION)
        {
            //SP_GET_STOCK_FUNCTION
            decimal STKBAL = 0;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GET_STOCK_FUNCTION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FINYEAR", FINYEAR);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@ASONDATE", ASONDATE);
                cmd.Parameters.AddWithValue("@PLANT", PLANT);
                cmd.Parameters.AddWithValue("@LOCATION", LOCATION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                if (dt.Rows.Count > 0)
                {
                    STKBAL = Convert.ToDecimal(dt.Rows[0]["STKBAL"]);
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return STKBAL;
        }

        public decimal SP_CAL_AVGRATE(int CMPID, string PLANTCD, string LOCCD, string UNIT, string ITEMID)
        {
            decimal AVGRATE = 0;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CAL_AVGRATE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
                cmd.Parameters.AddWithValue("@UNIT", UNIT);
                cmd.Parameters.AddWithValue("@ITEMID", ITEMID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                if (dt.Rows.Count > 0)
                {
                    AVGRATE = Convert.ToDecimal(dt.Rows[0]["MRATE"]);
                }

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return AVGRATE;
        }


        public DataTable GetMRDetails(string MRNO, int ACTION)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MR_DETAILS", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetPODetails(string PONO, int ACTION)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PO_DETAILS", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public string setDateFormat(string StrDate, bool ForQuery)
        {
            string strDateFormat = string.Empty;
            string strDateSeprator = string.Empty;
            string strSysDateFormat = string.Empty;
            string[] strDt;
            string strString = string.Empty;
            strSysDateFormat = StrDate;//System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;

            if (strSysDateFormat.Contains("-"))
            {
                strDateSeprator = "-";
            }
            else if (strSysDateFormat.Contains("/"))
            {
                strDateSeprator = "/";
            }
            if (strSysDateFormat.StartsWith("M"))
            {
                strSysDateFormat = "MM" + strDateSeprator + "dd" + strDateSeprator + "yyyy";
            }
            else
            {
                strSysDateFormat = "dd" + strDateSeprator + "mm" + strDateSeprator + "yyyy";
            }


            string str = string.Empty;
            if (StrDate == "  /  /" || StrDate == "  -  -" || StrDate == "")
            {

            }
            else
            {
                strDt = StrDate.Split(Convert.ToChar(strDateSeprator));

                if (strDt[0].Length == 1)
                {
                    strDt[0] = strDt[0].PadLeft(2, '0');
                }
                else
                {
                    strDt[0] = strDt[0];
                }

                if (strDt[1].Length == 1)
                {
                    strDt[1] = strDt[1].PadLeft(2, '0');
                }
                else
                {
                    strDt[1] = strDt[1];
                    strDt[2] = strDt[2].Substring(0, 4);
                }
                str = strDt[2] + "-" + strDt[1] + "-" + strDt[0];

            }

            return str;
        }


        public string MAXPRNO(string TRANTYPE, string DOCTYPE)
        {
            string MAXPR = string.Empty;
            SqlCommand cmd = new SqlCommand("SP_GET_MAX_PRNO", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@TRANTYPE", TRANTYPE);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                object obj = cmd.ExecuteScalar();
                if ((obj) != null)
                {
                    MAXPR = obj.ToString();
                    MAXPR = Convert.ToString(Convert.ToInt64(MAXPR) + 1);
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return MAXPR;
        }

        public int getFinYear(string strDate)
        {
            int retYear, intMonth, intYear;
            intMonth = Convert.ToDateTime(strDate).Month;
            intYear = Convert.ToDateTime(strDate).Year;
            if (intMonth >= 1 && intMonth <= 3)
            {
                retYear = intYear - 1;
            }
            else
            {
                retYear = intYear;
            }

            return retYear;
        }

        public DataTable GetItemDetails(string ITEMCODE, string PLANTCODE, string LOCATIONCODE)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GET_ITEM_DETAILS", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@ITEMCODE", strConvertZeroPadding(ITEMCODE));
                cmd.Parameters.AddWithValue("@FINYEAR", getFinYear(setDateFormat(DateTime.Now.ToShortDateString(), true)));
                //cmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DateTime.Now.ToShortDateString()));
                cmd.Parameters.AddWithValue("@ASONDATE", setDateFormat(DateTime.Now.ToShortDateString(), true));
                cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);
                cmd.Parameters.AddWithValue("@LOCATIONCODE", LOCATIONCODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetPRFCost(string PLANTCD, string USERID)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PRF_COST_CENTER", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetJobDetails(string JOBID)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GET_PRDETAILS", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable SelectItem(string MAKE, string MODEL, string ITEMCODE, string ITEMGROUP, string ITEMSUBGROUP, string ITEMCAT, string ITEMDESC)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_ITEMMSATER", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@ITEMDESC", ITEMDESC);
                cmd.Parameters.AddWithValue("@ITEMGROUP", ITEMGROUP);
                cmd.Parameters.AddWithValue("@ITEMSUBGROUP", ITEMSUBGROUP);
                cmd.Parameters.AddWithValue("@ITEMCAT", ITEMCAT);
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@SQLQUERY", "");

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable SelectVendor(int CMPID, string VENDCODE, string NAME, string VENDTYPEID, string CITY)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_VENDOR_LIST", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@VENDCODE", VENDCODE);
                cmd.Parameters.AddWithValue("@NAME", NAME);
                cmd.Parameters.AddWithValue("@VENDTYPEID", VENDTYPEID);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                cmd.Parameters.AddWithValue("@MAINQUERY", "");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public string SavePPR(string PRTYPE, string PRDT, string REMARK, string DEPTID, GridView grvListItem, string UserId, string MRITEMSR, string QMRNO)
        {
            MainClass objMainClass = new MainClass();
            string PRNO = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    PRNO = MAXPRNO(PRTYPE, "PR");
                    PRNO = strConvertZeroPadding(PRNO);

                    #region Update MMNORANGE...
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(PRNO));
                    PRcmd.Parameters.AddWithValue("@TRANTYPE", PRTYPE);
                    PRcmd.Parameters.AddWithValue("@DOCTYPE", "PR");
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    #endregion


                    #region Insert MST PR...
                    SqlCommand cmdM = new SqlCommand("SP_INSERT_MST_PR", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@PRTYPE", PRTYPE);
                    cmdM.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                    cmdM.Parameters.AddWithValue("@PRDT", setDateFormat(PRDT, true));
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@DEPTID", Convert.ToInt32(DEPTID));
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion



                    for (int i = 0; i < grvListItem.Rows.Count; i++)
                    {
                        GridViewRow row = grvListItem.Rows[i];

                        SqlCommand cmdD = new SqlCommand("SP_INSERT_PR_DETAILS", ConnSherpa);
                        cmdD.Parameters.AddWithValue("@cmpid", objMainClass.intCmpId);
                        cmdD.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                        cmdD.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblID")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblItemId")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblItemDesc")).Text);
                        cmdD.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblPlantID")).Text);
                        cmdD.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblLocationCDID")).Text);
                        cmdD.Parameters.AddWithValue("@TRNUM", ((Label)row.FindControl("lblTrackNo")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGroupId")).Text);
                        cmdD.Parameters.AddWithValue("@PRQTY", ((Label)row.FindControl("lblQty")).Text);
                        cmdD.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblUOMID")).Text);
                        cmdD.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblRate")).Text);
                        cmdD.Parameters.AddWithValue("@DELIDT", setDateFormat(((Label)row.FindControl("lblDeliDate")).Text, true));
                        cmdD.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblAmount")).Text);
                        cmdD.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGLCode")).Text);
                        cmdD.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblCostCenter")).Text);
                        cmdD.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblProfitCenter")).Text);
                        cmdD.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblAssetCode")).Text);
                        cmdD.Parameters.AddWithValue("@PRBY", ((Label)row.FindControl("lblRequisitioner")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblItemText")).Text);
                        cmdD.Parameters.AddWithValue("@PARTREQNO", ((Label)row.FindControl("lblPartReqNo")).Text == "" ? "0" : ((Label)row.FindControl("lblPartReqNo")).Text);
                        cmdD.CommandType = CommandType.StoredProcedure;
                        cmdD.Connection.Open();
                        cmdD.ExecuteNonQuery();
                        cmdD.Connection.Close();

                        bool UpdateReq = UpdatePartRequest(Convert.ToInt32(((Label)row.FindControl("lblPartReqNo")).Text == "" ? "0" : ((Label)row.FindControl("lblPartReqNo")).Text), "", "", "", "", "", "", "", Convert.ToString((int)STATUS.PRCreated), UserId, "", 2);








                    }



                    if (MRITEMSR != null && MRITEMSR != "" && MRITEMSR != string.Empty)
                    {
                        string[] itemsr = Convert.ToString(MRITEMSR).Split(',');
                        if (itemsr.Count() > 0)
                        {
                            for (int j = 0; j < itemsr.Count(); j++)
                            {

                                bool iResult = objMainClass.UpdateMRDtl(Convert.ToString(QMRNO), Convert.ToString(itemsr[j]), PRNO, objMainClass.intCmpId, 2, "", "", "");
                            }
                        }
                    }


                    scope.Complete();
                    scope.Dispose();
                    return PRNO;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }

        }


        public string SavePPRFromListing(string PRTYPE, string PRDT, string REMARK, string DEPTID, GridView grvListItem, string UserId, string LISTINGID, string VENDCODE)
        {
            MainClass objMainClass = new MainClass();
            string PRNO = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    PRNO = MAXPRNO(PRTYPE, "PR");
                    PRNO = strConvertZeroPadding(PRNO);

                    #region Update MMNORANGE...
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(PRNO));
                    PRcmd.Parameters.AddWithValue("@TRANTYPE", PRTYPE);
                    PRcmd.Parameters.AddWithValue("@DOCTYPE", "PR");
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    #endregion


                    #region Insert MST PR...
                    SqlCommand cmdM = new SqlCommand("SP_PRPO_FORM_LISTING", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@PRTYPE", PRTYPE);
                    cmdM.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                    cmdM.Parameters.AddWithValue("@PRDT", setDateFormat(PRDT, true));
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@DEPTID", Convert.ToInt32(DEPTID));
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@LISTINGID", LISTINGID);
                    cmdM.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(VENDCODE));
                    cmdM.Parameters.AddWithValue("@ACTION", "CREATEPR");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion



                    for (int i = 0; i < grvListItem.Rows.Count; i++)
                    {
                        GridViewRow row = grvListItem.Rows[i];

                        SqlCommand cmdD = new SqlCommand("SP_INSERT_PR_DETAILS", ConnSherpa);
                        cmdD.Parameters.AddWithValue("@cmpid", objMainClass.intCmpId);
                        cmdD.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                        cmdD.Parameters.AddWithValue("@SRNO", Convert.ToString(grvListItem.Rows[0].Cells[16].Text));
                        cmdD.Parameters.AddWithValue("@ITEMID", Convert.ToString(grvListItem.Rows[0].Cells[5].Text));
                        cmdD.Parameters.AddWithValue("@ITEMDESC", Convert.ToString(grvListItem.Rows[0].Cells[6].Text));
                        cmdD.Parameters.AddWithValue("@PLANTCD", Convert.ToString(grvListItem.Rows[0].Cells[9].Text));
                        cmdD.Parameters.AddWithValue("@LOCCD", Convert.ToString(grvListItem.Rows[0].Cells[10].Text));
                        cmdD.Parameters.AddWithValue("@TRNUM", Convert.ToString(grvListItem.Rows[0].Cells[23].Text));
                        cmdD.Parameters.AddWithValue("@ITEMGRPID", Convert.ToInt32(grvListItem.Rows[0].Cells[7].Text));
                        cmdD.Parameters.AddWithValue("@PRQTY", Convert.ToInt32(grvListItem.Rows[0].Cells[17].Text));
                        cmdD.Parameters.AddWithValue("@UOM", grvListItem.Rows[0].Cells[8].Text == "&nbsp;" ? 1 : Convert.ToInt32(grvListItem.Rows[0].Cells[8].Text));
                        cmdD.Parameters.AddWithValue("@RATE", Convert.ToString(grvListItem.Rows[0].Cells[18].Text));
                        cmdD.Parameters.AddWithValue("@DELIDT", setDateFormat(DateTime.Now.ToString(), true));
                        cmdD.Parameters.AddWithValue("@CAMOUNT", Convert.ToString(grvListItem.Rows[0].Cells[25].Text));
                        cmdD.Parameters.AddWithValue("@GLCD", Convert.ToString(grvListItem.Rows[0].Cells[11].Text));
                        cmdD.Parameters.AddWithValue("@CSTCENTCD", Convert.ToString(grvListItem.Rows[0].Cells[12].Text));
                        cmdD.Parameters.AddWithValue("@PRFCNT", Convert.ToString(grvListItem.Rows[0].Cells[13].Text));
                        cmdD.Parameters.AddWithValue("@ASSETCD", Convert.ToString(grvListItem.Rows[0].Cells[19].Text) == "&nbsp;" ? "" : Convert.ToString(grvListItem.Rows[0].Cells[19].Text));
                        cmdD.Parameters.AddWithValue("@PRBY", Convert.ToString(grvListItem.Rows[0].Cells[20].Text));
                        cmdD.Parameters.AddWithValue("@ITEMTEXT", Convert.ToString(grvListItem.Rows[0].Cells[14].Text) + " " + Convert.ToString(grvListItem.Rows[0].Cells[24].Text));
                        cmdD.Parameters.AddWithValue("@PARTREQNO", 0);
                        cmdD.CommandType = CommandType.StoredProcedure;
                        cmdD.Connection.Open();
                        cmdD.ExecuteNonQuery();
                        cmdD.Connection.Close();

                    }

                    scope.Complete();
                    scope.Dispose();
                    return PRNO;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }

        }


        public DataTable GetDetailFromListingID(int CMPID, int LISTINGID, string ACTION)
        {
            //SP_PRPO_FORM_LISTING
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PRPO_FORM_LISTING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@LISTINGID", LISTINGID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }




        public string SaveMPO(int CMPID, string DOCTYPE, string PODT, string VENDCODE, string TRANCODE, string PAYTERMS, string PAYTERMSDESC, string NETAMOUNT, string TAXAMOUNT, string DISCOUNTAMT, string NETPOAMOUNT, string REMARKS, GridView ITEMDETAIL, GridView TAXDETAIL, GridView CHARGESDETAIL, string CREATEBY, int DEPTID)
        {
            MainClass objMainClass = new MainClass();
            string PONO = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    PONO = MAXPRNO(DOCTYPE, "PO");
                    PONO = strConvertZeroPadding(PONO);


                    #region Update MMNORANGE...
                    SqlCommand POcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    POcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    POcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(PONO));
                    POcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                    POcmd.Parameters.AddWithValue("@DOCTYPE", "PO");
                    POcmd.CommandType = CommandType.StoredProcedure;
                    POcmd.Connection.Open();
                    POcmd.ExecuteNonQuery();
                    POcmd.Connection.Close();
                    #endregion

                    //SP_INSERT_POMST
                    #region Insert MST PO...
                    SqlCommand cmdM = new SqlCommand("SP_INSERT_POMST", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdM.Parameters.AddWithValue("@POTYPE", DOCTYPE);
                    cmdM.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                    cmdM.Parameters.AddWithValue("@PODT", setDateFormat(PODT, true));
                    cmdM.Parameters.AddWithValue("@VENDCODE", VENDCODE);
                    cmdM.Parameters.AddWithValue("@TRANCODE", TRANCODE);
                    cmdM.Parameters.AddWithValue("@PMTTERMS", PAYTERMS);
                    cmdM.Parameters.AddWithValue("@REMARK", REMARKS);
                    cmdM.Parameters.AddWithValue("@NETMATVALUE", NETAMOUNT);
                    cmdM.Parameters.AddWithValue("@NETTAXAMT", TAXAMOUNT);
                    cmdM.Parameters.AddWithValue("@DISCOUNT", DISCOUNTAMT);
                    cmdM.Parameters.AddWithValue("@NETPOAMT", NETPOAMOUNT);
                    cmdM.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmdM.Parameters.AddWithValue("@PMTTERMSDESC", PAYTERMSDESC);
                    cmdM.Parameters.AddWithValue("@DEPTID", DEPTID);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion

                    for (int i = 0; i < ITEMDETAIL.Rows.Count; i++)
                    {
                        GridViewRow row = ITEMDETAIL.Rows[i];

                        SqlCommand cmdD = new SqlCommand("SP_INSERT_PODTL", ConnSherpa);

                        cmdD.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdD.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                        cmdD.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblGVID")).Text);
                        cmdD.Parameters.AddWithValue("@PRNO", ((Label)row.FindControl("lblGVPrNo")).Text);
                        cmdD.Parameters.AddWithValue("@PRSRNO", ((Label)row.FindControl("lblGVPRSrNo")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblGVItemId")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblGVItemDesc")).Text);
                        cmdD.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblGVPlantID")).Text);
                        cmdD.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblGVLocationCDID")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGVGroupId")).Text);
                        cmdD.Parameters.AddWithValue("@POQTY", ((Label)row.FindControl("lblGVQty")).Text);
                        cmdD.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblGVUOMID")).Text);
                        cmdD.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblGVRate")).Text);
                        cmdD.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblGVAmount")).Text);
                        cmdD.Parameters.AddWithValue("@DISCAMT", ((Label)row.FindControl("lblGVDiscount")).Text);
                        cmdD.Parameters.AddWithValue("@DELIDT", setDateFormat(((Label)row.FindControl("lblGVDeliDate")).Text, true));
                        cmdD.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGVGLCode")).Text);
                        cmdD.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblGVCostCenter")).Text);
                        cmdD.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblGVProfitCenter")).Text);
                        cmdD.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblGVAssetCode")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblGVRemarks")).Text);
                        cmdD.Parameters.AddWithValue("@TAXAMT", "0.00");
                        cmdD.Parameters.AddWithValue("@TRNUM", strConvertZeroPadding(((Label)row.FindControl("lblGVTrackNo")).Text));
                        cmdD.Parameters.AddWithValue("@REFNO", ((Label)row.FindControl("lblGVRefNo")).Text);
                        cmdD.Parameters.AddWithValue("@SHORTCLSQTY", "0.00");
                        cmdD.Parameters.AddWithValue("@IMEINO", ((Label)row.FindControl("lblGVIMEI")).Text);
                        cmdD.Parameters.AddWithValue("@BRATE", ((Label)row.FindControl("lblGVBaseRate")).Text);
                        cmdD.Parameters.AddWithValue("@DEVREASON", ((Label)row.FindControl("lblDEVREASON")).Text);

                        if (((Label)row.FindControl("lblDEVREASON")).Text == "OK")
                        {
                            cmdD.Parameters.AddWithValue("@APRVSTATUS", 260);
                            cmdD.Parameters.AddWithValue("@APRVBY", BackGroundUser);
                            cmdD.Parameters.AddWithValue("@APRVDATE", DateTime.Now);
                        }
                        else
                        {

                        }

                        cmdD.CommandType = CommandType.StoredProcedure;
                        cmdD.Connection.Open();
                        cmdD.ExecuteNonQuery();
                        cmdD.Connection.Close();
                    }

                    for (int j = 0; j < TAXDETAIL.Rows.Count; j++)
                    {
                        GridViewRow row = TAXDETAIL.Rows[j];

                        SqlCommand cmdT = new SqlCommand("SP_INSERT_POCOND", ConnSherpa);

                        cmdT.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdT.Parameters.AddWithValue("@CONDORDER", ((Label)row.FindControl("lblTaxSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                        cmdT.Parameters.AddWithValue("@POSRNO", ((Label)row.FindControl("lblTaxPOSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@CONDID", ((Label)row.FindControl("lblCONDID")).Text);
                        cmdT.Parameters.AddWithValue("@CONDTYPE", ((Label)row.FindControl("lblTaxCondType")).Text);
                        cmdT.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblTaxRate")).Text);
                        cmdT.Parameters.AddWithValue("@BASEAMT", ((Label)row.FindControl("lblTaxBaseAmount")).Text);
                        cmdT.Parameters.AddWithValue("@PID", ((Label)row.FindControl("lblPID")).Text);
                        cmdT.Parameters.AddWithValue("@TAXAMT", ((Label)row.FindControl("lblTaxAmount")).Text);
                        cmdT.Parameters.AddWithValue("@OPERATOR", ((Label)row.FindControl("lblTaxOperator")).Text);
                        cmdT.CommandType = CommandType.StoredProcedure;
                        cmdT.Connection.Open();
                        cmdT.ExecuteNonQuery();
                        cmdT.Connection.Close();
                    }

                    for (int k = 0; k < CHARGESDETAIL.Rows.Count; k++)
                    {
                        //SP_INSERT_POCHARGES
                        GridViewRow row = CHARGESDETAIL.Rows[k];
                        SqlCommand cmdC = new SqlCommand("SP_INSERT_POCHARGES", ConnSherpa);

                        cmdC.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdC.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                        cmdC.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblChrgSrNo")).Text);
                        cmdC.Parameters.AddWithValue("@CHGTYPE", (((Label)row.FindControl("lblChrgCondType")).Text).Split('-')[0].Trim());
                        cmdC.Parameters.AddWithValue("@CHGAMT", ((Label)row.FindControl("lblChrgAmount")).Text);
                        cmdC.CommandType = CommandType.StoredProcedure;
                        cmdC.Connection.Open();
                        cmdC.ExecuteNonQuery();
                        cmdC.Connection.Close();
                    }

                    scope.Complete();
                    scope.Dispose();
                    return PONO;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }




        public DataTable CheckPOSOImeiNo(int CMPID, string IMEINO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CHECK_IMEINO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;

        }


        public DataTable SuggestedPrice(int CMPID, string PRNO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CHECK_IMEINO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;

        }


        public DataTable POPriceRange(int CMPID, string ITEMCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CHECK_IMEINO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMCODE", strConvertZeroPadding(ITEMCODE));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;

        }


        //SP_PO_MATERIAL_INWARDTRAN_MMMSTCRUDOPERATION
        public string InsertMaterialIssue(string DOCTYPE, string DOCNO, string DOCDT, string REFNO, string REMARKS, GridView GRVLIST, string USERID, int CMPID, string MRNO, int UPDATEACTION)
        {
            MainClass objMainClass = new MainClass();
            string DOCKNO = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    DOCKNO = MAXPRNO(DOCTYPE, "IST");
                    DOCKNO = strConvertZeroPadding(DOCKNO);
                    #region Update MMNORANGE...
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(DOCKNO));
                    PRcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                    PRcmd.Parameters.AddWithValue("@DOCTYPE", "IST");
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    #endregion

                    #region Insert MMMST

                    SqlCommand MMcmd = new SqlCommand("SP_PO_MATERIAL_INWARDTRAN_MMMSTCRUDOPERATION", ConnSherpa);
                    MMcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    MMcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                    MMcmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCKNO));
                    MMcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    MMcmd.Parameters.AddWithValue("@DOCDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@POSTDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@REFNO", REFNO);
                    MMcmd.Parameters.AddWithValue("@REMARK", REMARKS);
                    MMcmd.Parameters.AddWithValue("@CREATEBY", USERID);
                    MMcmd.Parameters.AddWithValue("@MODE", "I");
                    MMcmd.CommandType = CommandType.StoredProcedure;
                    MMcmd.Connection.Open();
                    MMcmd.ExecuteNonQuery();
                    MMcmd.Connection.Close();

                    #endregion

                    #region Insert MM Details

                    #endregion
                    for (int i = 0; i < GRVLIST.Rows.Count; i++)
                    {
                        GridViewRow row = GRVLIST.Rows[i];
                        SqlCommand MDcmd = new SqlCommand("SP_INSERT_TRAN_MM", ConnSherpa);
                        MDcmd.Parameters.AddWithValue("@CMPID", CMPID);
                        MDcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                        MDcmd.Parameters.AddWithValue("@DOCNO", DOCKNO);
                        MDcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                        MDcmd.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblID")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblItemId")).Text);
                        MDcmd.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblFromPlantID")).Text);
                        MDcmd.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblFromLocationCDID")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGroupId")).Text);
                        MDcmd.Parameters.AddWithValue("@itemdesc", ((Label)row.FindControl("lblItemDesc")).Text);
                        MDcmd.Parameters.AddWithValue("@QTY", ((Label)row.FindControl("lblQty")).Text);
                        MDcmd.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblUOMID")).Text);
                        MDcmd.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGLCode")).Text);
                        MDcmd.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblCostCenter")).Text);
                        MDcmd.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblProfitCenter")).Text);
                        MDcmd.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblAssetCode")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblRemarks")).Text);
                        MDcmd.Parameters.AddWithValue("@TOPLANTCD", ((Label)row.FindControl("lblPlantID")).Text);
                        MDcmd.Parameters.AddWithValue("@TOLOCCD", ((Label)row.FindControl("lblLocationCDID")).Text);
                        MDcmd.Parameters.AddWithValue("@TOITEMID", 0);
                        MDcmd.Parameters.AddWithValue("@TRACKNO", ((Label)row.FindControl("lblTrackNo")).Text == "" ? "0" : ((Label)row.FindControl("lblTrackNo")).Text);
                        MDcmd.Parameters.AddWithValue("@MODE", "I");
                        MDcmd.CommandType = CommandType.StoredProcedure;
                        MDcmd.Connection.Open();
                        MDcmd.ExecuteNonQuery();
                        MDcmd.Connection.Close();



                        if (MRNO != null && MRNO != "" && MRNO != string.Empty)
                        {
                            bool iResult = objMainClass.UpdateMRDtl(MRNO, ((Label)row.FindControl("lblREFSRNO")).Text, DOCKNO, objMainClass.intCmpId, UPDATEACTION, ((Label)row.FindControl("lblQty")).Text, ((Label)row.FindControl("lblUOMID")).Text, ((Label)row.FindControl("lblQty")).Text);
                        }



                    }

                    scope.Complete();
                    scope.Dispose();
                    return DOCKNO;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }







        public string SaveMMR(string PRTYPE, string PRDT, string REMARK, string DEPTID, GridView grvListItem, string UserId, byte[] bytes = null, string EXTENSION = null)
        {
            MainClass objMainClass = new MainClass();
            string PRNO = string.Empty;
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    PRNO = MAXPRNO(PRTYPE, "MMR");

                    #region Update MMNORANGE...
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(PRNO));
                    PRcmd.Parameters.AddWithValue("@TRANTYPE", PRTYPE);
                    PRcmd.Parameters.AddWithValue("@DOCTYPE", "MMR");
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    #endregion


                    #region Insert MST PR...
                    SqlCommand cmdM = new SqlCommand("SP_INSERT_MST_MR", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@MRTYPE", PRTYPE);
                    cmdM.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(PRNO));
                    cmdM.Parameters.AddWithValue("@MRDT", setDateFormat(PRDT, true));
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@DEPTID", Convert.ToInt32(DEPTID));
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@MRINVIMAGE", bytes);
                    cmdM.Parameters.AddWithValue("@MREXTENSION", EXTENSION);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion



                    for (int i = 0; i < grvListItem.Rows.Count; i++)
                    {
                        GridViewRow row = grvListItem.Rows[i];

                        SqlCommand cmdD = new SqlCommand("SP_INSERT_MR_DETAILS", ConnSherpa);
                        cmdD.Parameters.AddWithValue("@cmpid", objMainClass.intCmpId);
                        cmdD.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(PRNO));
                        cmdD.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblID")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMSPEC", ((Label)row.FindControl("lblItemSpec")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblItemDesc")).Text);
                        cmdD.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblPlantID")).Text);
                        cmdD.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblLocationCDID")).Text);
                        cmdD.Parameters.AddWithValue("@TRNUM", ((Label)row.FindControl("lblTrackNo")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGroupId")).Text);
                        cmdD.Parameters.AddWithValue("@MRQTY", ((Label)row.FindControl("lblQty")).Text);
                        cmdD.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblUOMID")).Text);
                        cmdD.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblRate")).Text);
                        cmdD.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblAmount")).Text);
                        cmdD.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblCostCenter")).Text);
                        cmdD.Parameters.AddWithValue("@MRBY", ((Label)row.FindControl("lblRequisitioner")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblItemText")).Text);
                        //cmdD.Parameters.AddWithValue("@PARTREQNO", ((Label)row.FindControl("lblPartReqNo")).Text == "" ? "0" : ((Label)row.FindControl("lblPartReqNo")).Text);
                        cmdD.CommandType = CommandType.StoredProcedure;
                        cmdD.Connection.Open();
                        cmdD.ExecuteNonQuery();
                        cmdD.Connection.Close();

                        //bool UpdateReq = UpdatePartRequest(Convert.ToInt32(((Label)row.FindControl("lblPartReqNo")).Text == "" ? "0" : ((Label)row.FindControl("lblPartReqNo")).Text), "", "", "", "", "", "", "", Convert.ToString((int)STATUS.PRCreated), UserId, "", 2);
                    }
                    scope.Complete();
                    scope.Dispose();
                    return PRNO;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }

        }

        public void SaveMailNotification(string MAILFROM, string PASSWORD, string MAILTO, string MAILCC, string SUBJECT, string BODY, string PORT, string DOCNO, string CREATEBY, string DOCTYPE)
        {
            SqlCommand cmd = new SqlCommand("SP_INSERT_MAIL_NOTIFICATION", ConnSherpa);

            try
            {

                cmd.Parameters.AddWithValue("@MAILFROM", MAILFROM);
                cmd.Parameters.AddWithValue("@PASSWORD", PASSWORD);
                cmd.Parameters.AddWithValue("@MAILTO", MAILTO);
                cmd.Parameters.AddWithValue("@MAILCC", MAILCC);
                cmd.Parameters.AddWithValue("@SUBJECT", SUBJECT);
                cmd.Parameters.AddWithValue("@BODY", BODY);
                cmd.Parameters.AddWithValue("@PORT", PORT);
                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public bool SavePartRequest(GridView grvListItem, string USERID)
        {
            MainClass objMainClass = new MainClass();
            bool iResult = false;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    for (int i = 0; i < grvListItem.Rows.Count; i++)
                    {
                        GridViewRow row = grvListItem.Rows[i];
                        SqlCommand cmd = new SqlCommand("INSERT_TRAN_PARTREQ", ConnSherpa);
                        cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                        cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(((Label)row.FindControl("lblJobID")).Text));
                        cmd.Parameters.AddWithValue("@DOCDT", DateTime.Now.Date);
                        cmd.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblGvItemId")).Text);
                        cmd.Parameters.AddWithValue("@QTY", ((Label)row.FindControl("lblQTY")).Text);
                        cmd.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblPlantcd")).Text);
                        cmd.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblLoccd")).Text);
                        cmd.Parameters.AddWithValue("@REQBY", ((Label)row.FindControl("lblReqBy")).Text);
                        cmd.Parameters.AddWithValue("@STATUS", ((Label)row.FindControl("lblStatus")).Text);
                        cmd.Parameters.AddWithValue("@CREATEBY", USERID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                    }
                    scope.Complete();
                    scope.Dispose();
                    iResult = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iResult;
        }


        public bool UpdatePartRequest(int ID, string JOBID, string ITEMID, string QTY, string PLANTCD, string LOCCD, string UOM, string REQBY, string STATUS, string UPDATEBY, string REMARK, int ACTION)
        {
            bool iResult = false;


            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlCommand cmd = new SqlCommand("SP_UPDATE_PARTREQUEST", ConnSherpa);
                    cmd.Parameters.AddWithValue("@ID", ID);
                    //cmd.Parameters.AddWithValue("@CMPID", intCmpId);
                    //cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                    //cmd.Parameters.AddWithValue("@ITEMID", ITEMID);
                    //cmd.Parameters.AddWithValue("@QTY", QTY);
                    //cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                    //cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
                    //cmd.Parameters.AddWithValue("@UOM", UOM);
                    //cmd.Parameters.AddWithValue("@REQBY", REQBY);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS == "" ? 71 : Convert.ToInt32(STATUS));
                    cmd.Parameters.AddWithValue("@REMARK", REMARK);
                    cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    scope.Complete();
                    scope.Dispose();
                    iResult = true;


                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return iResult;
        }


        public string UpdatePPR(string PRNO, string PRTYPE, string PRDT, string REMARK, string DEPTID, GridView grvListItem, string UserId)
        {
            MainClass objMainClass = new MainClass();
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {


                    #region Update MST PR...
                    SqlCommand cmdM = new SqlCommand("SP_UPDATE_MST_PR", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@DEPTID", Convert.ToInt32(DEPTID));
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion

                    SqlCommand DelCMD = new SqlCommand("SP_DELETE_PR_DETAILS", ConnSherpa);
                    DelCMD.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    DelCMD.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                    DelCMD.CommandType = CommandType.StoredProcedure;
                    DelCMD.Connection.Open();
                    DelCMD.ExecuteNonQuery();
                    DelCMD.Connection.Close();


                    for (int i = 0; i < grvListItem.Rows.Count; i++)
                    {
                        GridViewRow row = grvListItem.Rows[i];

                        SqlCommand cmdD = new SqlCommand("SP_INSERT_PR_DETAILS", ConnSherpa);
                        cmdD.Parameters.AddWithValue("@cmpid", objMainClass.intCmpId);
                        cmdD.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                        cmdD.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblID")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblItemId")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblItemDesc")).Text);
                        cmdD.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblPlantID")).Text);
                        cmdD.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblLocationCDID")).Text);
                        cmdD.Parameters.AddWithValue("@TRNUM", ((Label)row.FindControl("lblTrackNo")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGroupId")).Text);
                        cmdD.Parameters.AddWithValue("@PRQTY", ((Label)row.FindControl("lblQty")).Text);
                        cmdD.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblUOMID")).Text);
                        cmdD.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblRate")).Text);
                        cmdD.Parameters.AddWithValue("@DELIDT", setDateFormat(((Label)row.FindControl("lblDeliDate")).Text, true));
                        cmdD.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblAmount")).Text);
                        cmdD.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGLCode")).Text);
                        cmdD.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblCostCenter")).Text);
                        cmdD.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblProfitCenter")).Text);
                        cmdD.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblAssetCode")).Text);
                        cmdD.Parameters.AddWithValue("@PRBY", ((Label)row.FindControl("lblRequisitioner")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblItemText")).Text);
                        cmdD.Parameters.AddWithValue("@PARTREQNO", ((Label)row.FindControl("lblPartReqNo")).Text == "" ? "0" : ((Label)row.FindControl("lblPartReqNo")).Text);
                        cmdD.CommandType = CommandType.StoredProcedure;
                        cmdD.Connection.Open();
                        cmdD.ExecuteNonQuery();
                        cmdD.Connection.Close();
                    }
                    scope.Complete();
                    scope.Dispose();
                    return PRNO;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }

        }


        public string UpdateMPO(int CMPID, string DOCTYPE, string PONO, string PODT, string VENDCODE, string TRANCODE, string PAYMENTTERMS, string PAYMENTTERMSDESC, string NETAMOUNTVALUE, string NETTAXAMOUNT, string DISCOUNT, string NETPOAMT, string REMARK, GridView ITEMDETAIL, GridView TAXDETAIL, GridView CHARGESDETAIL, string UPDATEBY)
        {
            MainClass objMainClass = new MainClass();
            PONO = strConvertZeroPadding(PONO);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlCommand cmdM = new SqlCommand("SP_UPDATE_POMST", ConnSherpa);

                    cmdM.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdM.Parameters.AddWithValue("@PONO", PONO);
                    cmdM.Parameters.AddWithValue("@VENDCODE", VENDCODE);
                    cmdM.Parameters.AddWithValue("@TRANCODE", TRANCODE);
                    cmdM.Parameters.AddWithValue("@PMTTERMS", PAYMENTTERMS);
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@NETMATVALUE", NETAMOUNTVALUE);
                    cmdM.Parameters.AddWithValue("@NETTAXAMT", NETTAXAMOUNT);
                    cmdM.Parameters.AddWithValue("@DISCOUNT", DISCOUNT);
                    cmdM.Parameters.AddWithValue("@NETPOAMT", NETPOAMT);
                    cmdM.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                    cmdM.Parameters.AddWithValue("@PMTTERMSDESC", PAYMENTTERMSDESC);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();


                    SqlCommand DEL_PODTL_CMD = new SqlCommand("SP_DELETE_PODTL_POCOND_POCHG", ConnSherpa);
                    DEL_PODTL_CMD.Parameters.AddWithValue("@CMPID", CMPID);
                    DEL_PODTL_CMD.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                    DEL_PODTL_CMD.Parameters.AddWithValue("@ACTION", 1);
                    DEL_PODTL_CMD.CommandType = CommandType.StoredProcedure;
                    DEL_PODTL_CMD.Connection.Open();
                    DEL_PODTL_CMD.ExecuteNonQuery();
                    DEL_PODTL_CMD.Connection.Close();



                    for (int i = 0; i < ITEMDETAIL.Rows.Count; i++)
                    {
                        GridViewRow row = ITEMDETAIL.Rows[i];

                        SqlCommand cmdD = new SqlCommand("SP_INSERT_PODTL", ConnSherpa);

                        cmdD.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdD.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                        cmdD.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblGVID")).Text);
                        cmdD.Parameters.AddWithValue("@PRNO", ((Label)row.FindControl("lblGVPrNo")).Text);
                        cmdD.Parameters.AddWithValue("@PRSRNO", ((Label)row.FindControl("lblGVPRSrNo")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblGVItemId")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblGVItemDesc")).Text);
                        cmdD.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblGVPlantID")).Text);
                        cmdD.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblGVLocationCDID")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGVGroupId")).Text);
                        cmdD.Parameters.AddWithValue("@POQTY", ((Label)row.FindControl("lblGVQty")).Text);
                        cmdD.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblGVUOMID")).Text);
                        cmdD.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblGVRate")).Text == "" || ((Label)row.FindControl("lblGVRate")).Text == "0" ? ((Label)row.FindControl("lblGVBaseRate")).Text : ((Label)row.FindControl("lblGVRate")).Text);
                        cmdD.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblGVAmount")).Text);
                        cmdD.Parameters.AddWithValue("@DISCAMT", ((Label)row.FindControl("lblGVDiscount")).Text);
                        cmdD.Parameters.AddWithValue("@DELIDT", ((Label)row.FindControl("lblGVDeliDate")).Text);
                        cmdD.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGVGLCode")).Text);
                        cmdD.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblGVCostCenter")).Text);
                        cmdD.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblGVProfitCenter")).Text);
                        cmdD.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblGVAssetCode")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblGVItemText")).Text);
                        cmdD.Parameters.AddWithValue("@TAXAMT", "0.00");
                        cmdD.Parameters.AddWithValue("@TRNUM", ((Label)row.FindControl("lblGVTrackNo")).Text);
                        cmdD.Parameters.AddWithValue("@REFNO", ((Label)row.FindControl("lblGVRefNo")).Text);
                        cmdD.Parameters.AddWithValue("@SHORTCLSQTY", "0.00");
                        cmdD.Parameters.AddWithValue("@IMEINO", ((Label)row.FindControl("lblGVIMEI")).Text);
                        cmdD.Parameters.AddWithValue("@BRATE", ((Label)row.FindControl("lblGVBaseRate")).Text);
                        cmdD.CommandType = CommandType.StoredProcedure;
                        cmdD.Connection.Open();
                        cmdD.ExecuteNonQuery();
                        cmdD.Connection.Close();
                    }


                    SqlCommand DEL_POCOND_CMD = new SqlCommand("SP_DELETE_PODTL_POCOND_POCHG", ConnSherpa);
                    DEL_POCOND_CMD.Parameters.AddWithValue("@CMPID", CMPID);
                    DEL_POCOND_CMD.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                    DEL_POCOND_CMD.Parameters.AddWithValue("@ACTION", 2);
                    DEL_POCOND_CMD.CommandType = CommandType.StoredProcedure;
                    DEL_POCOND_CMD.Connection.Open();
                    DEL_POCOND_CMD.ExecuteNonQuery();
                    DEL_POCOND_CMD.Connection.Close();


                    for (int j = 0; j < TAXDETAIL.Rows.Count; j++)
                    {
                        GridViewRow row = TAXDETAIL.Rows[j];

                        SqlCommand cmdT = new SqlCommand("SP_INSERT_POCOND", ConnSherpa);

                        cmdT.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdT.Parameters.AddWithValue("@CONDORDER", ((Label)row.FindControl("lblTaxSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                        cmdT.Parameters.AddWithValue("@POSRNO", ((Label)row.FindControl("lblTaxPOSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@CONDID", ((Label)row.FindControl("lblCONDID")).Text);
                        cmdT.Parameters.AddWithValue("@CONDTYPE", ((Label)row.FindControl("lblTaxCondType")).Text);
                        cmdT.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblTaxRate")).Text);
                        cmdT.Parameters.AddWithValue("@BASEAMT", ((Label)row.FindControl("lblTaxBaseAmount")).Text);
                        cmdT.Parameters.AddWithValue("@PID", ((Label)row.FindControl("lblPID")).Text);
                        cmdT.Parameters.AddWithValue("@TAXAMT", ((Label)row.FindControl("lblTaxAmount")).Text);
                        cmdT.Parameters.AddWithValue("@OPERATOR", ((Label)row.FindControl("lblTaxOperator")).Text);
                        cmdT.CommandType = CommandType.StoredProcedure;
                        cmdT.Connection.Open();
                        cmdT.ExecuteNonQuery();
                        cmdT.Connection.Close();
                    }


                    SqlCommand DEL_POCHG_CMD = new SqlCommand("SP_DELETE_PODTL_POCOND_POCHG", ConnSherpa);
                    DEL_POCHG_CMD.Parameters.AddWithValue("@CMPID", CMPID);
                    DEL_POCHG_CMD.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                    DEL_POCHG_CMD.Parameters.AddWithValue("@ACTION", 3);
                    DEL_POCHG_CMD.CommandType = CommandType.StoredProcedure;
                    DEL_POCHG_CMD.Connection.Open();
                    DEL_POCHG_CMD.ExecuteNonQuery();
                    DEL_POCHG_CMD.Connection.Close();


                    for (int k = 0; k < CHARGESDETAIL.Rows.Count; k++)
                    {
                        //SP_INSERT_POCHARGES
                        GridViewRow row = CHARGESDETAIL.Rows[k];
                        SqlCommand cmdC = new SqlCommand("SP_INSERT_POCHARGES", ConnSherpa);

                        cmdC.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdC.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                        cmdC.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblChrgSrNo")).Text);
                        cmdC.Parameters.AddWithValue("@CHGTYPE", (((Label)row.FindControl("lblChrgCondType")).Text).Split('-')[0].Trim());
                        cmdC.Parameters.AddWithValue("@CHGAMT", ((Label)row.FindControl("lblChrgAmount")).Text);
                        cmdC.CommandType = CommandType.StoredProcedure;
                        cmdC.Connection.Open();
                        cmdC.ExecuteNonQuery();
                        cmdC.Connection.Close();
                    }








                    scope.Complete();
                    scope.Dispose();
                    return PONO;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        //UpdateMMR

        public string UpdateMMR(string MRNO, string MRTYPE, string MRDT, string REMARK, string DEPTID, GridView grvListItem, string UserId)
        {
            MainClass objMainClass = new MainClass();
            try
            {

                using (TransactionScope scope = new TransactionScope())
                {


                    #region Update MST PR...
                    SqlCommand cmdM = new SqlCommand("SP_UPDATE_MST_MR", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@DEPTID", Convert.ToInt32(DEPTID));
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion

                    SqlCommand DelCMD = new SqlCommand("SP_DELETE_MR_DETAILS", ConnSherpa);
                    DelCMD.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    DelCMD.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                    DelCMD.CommandType = CommandType.StoredProcedure;
                    DelCMD.Connection.Open();
                    DelCMD.ExecuteNonQuery();
                    DelCMD.Connection.Close();


                    for (int i = 0; i < grvListItem.Rows.Count; i++)
                    {
                        GridViewRow row = grvListItem.Rows[i];
                        SqlCommand cmdD = new SqlCommand("SP_INSERT_MR_DETAILS", ConnSherpa);
                        cmdD.Parameters.AddWithValue("@cmpid", objMainClass.intCmpId);
                        cmdD.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                        cmdD.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblID")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMSPEC", ((Label)row.FindControl("lblItemSpec")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblItemDesc")).Text);
                        cmdD.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblPlantID")).Text);
                        cmdD.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblLocationCDID")).Text);
                        cmdD.Parameters.AddWithValue("@TRNUM", ((Label)row.FindControl("lblTrackNo")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGroupId")).Text);
                        cmdD.Parameters.AddWithValue("@MRQTY", ((Label)row.FindControl("lblQty")).Text);
                        cmdD.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblUOMID")).Text);
                        cmdD.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblRate")).Text);
                        cmdD.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblAmount")).Text);
                        cmdD.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblCostCenter")).Text);
                        cmdD.Parameters.AddWithValue("@MRBY", ((Label)row.FindControl("lblRequisitioner")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblItemText")).Text);
                        // cmdD.Parameters.AddWithValue("@PARTREQNO", ((Label)row.FindControl("lblPartReqNo")).Text == "" ? "0" : ((Label)row.FindControl("lblPartReqNo")).Text);
                        cmdD.CommandType = CommandType.StoredProcedure;
                        cmdD.Connection.Open();
                        cmdD.ExecuteNonQuery();
                        cmdD.Connection.Close();
                    }
                    scope.Complete();
                    scope.Dispose();
                    return MRNO;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        public DataTable SelectPRMST(string PRNO, int CMPID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PR_MST", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable DOC_APRROVAL(string DOCTYPE, string DOCNO, int STATUS, int ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_DOC_APRROVAL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable SelectPOData(int CMPID, string PONO, int ACTION)
        {
            //SP_SELECT_PO_ALLDATA
            DataTable dt = new DataTable();
            PONO = strConvertZeroPadding(PONO);
            SqlCommand cmd = new SqlCommand("SP_SELECT_PO_ALLDATA", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PONO", PONO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                //SqlDataReader dr = cmd.ExecuteReader();
                //dt.Load(dr);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable SelectQuotData(int CMPID, string QOTNO, int ACTION)
        {
            DataTable dt = new DataTable();
            QOTNO = strConvertZeroPadding(QOTNO);
            SqlCommand cmd = new SqlCommand("SP_SELECT_QUOTATION_ALLDATA", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@QUOTNO", QOTNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                //SqlDataReader dr = cmd.ExecuteReader();
                //dt.Load(dr);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable SelectMRMST(string MRNO, int CMPID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MR_MST", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable PRREPORT(string CMPID, string PRNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PR_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable ISTREPORT(int CMPID, string DOCNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_TRAN_MM_ISTDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //SP_INWARD_REPORT
        public DataTable INWARDREPORT(int CMPID, string DOCNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_INWARD_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //SP_STO_DC_REPORT
        public DataTable STOREPORT(int CMPID, string DOCTYPE, string DOCNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_STO_DC_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable SelectPRDetail(string PRNO, int CMPID, int ACTION, string PRSRNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PR_DETAILS", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SRNO", PRSRNO);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }













        public DataTable SelectMRDetail(string MRNO, int CMPID, int ACTION, string MRSRNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MR_DETAILS", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SRNO", MRSRNO);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public bool SaveNewItem(string ITEMNAME, string ITEMDESC, string ITEMSPECIFICATION, string ITEMCAT, string ITEMGRP, string ITEMSUBGRP, string CREATEBY)
        {
            bool iReturn = false;
            SqlCommand cmd = new SqlCommand("SP_INSERT_ITEM_REQUIRED", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", intCmpId);
                cmd.Parameters.AddWithValue("@ITEMNAME", ITEMNAME);
                cmd.Parameters.AddWithValue("@ITEMDESC", ITEMDESC);
                cmd.Parameters.AddWithValue("@ITEMSPEC", ITEMSPECIFICATION);
                cmd.Parameters.AddWithValue("@ITEMCAT", ITEMCAT);
                cmd.Parameters.AddWithValue("@ITEMGRP", ITEMGRP);
                cmd.Parameters.AddWithValue("@ITEMSUBGRP", ITEMSUBGRP);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                cmd.Connection.Close();
                iReturn = true;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();

                throw ex;
            }

            return iReturn;
        }

        //EMAIL_SENDER_RECIEVER
        public DataTable MailSenderReceiver(string DOCTYPE, int STATUS, int DEPTCD, string PLANTCODE, int ACTION = 0, decimal AMOUNT = 0, string PONO = "", int APRVSEQ = 0)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("EMAIL_SENDER_RECIEVER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@DEPTCD", DEPTCD);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);

                cmd.Parameters.AddWithValue("@PONO", PONO == "" ? PONO : strConvertZeroPadding(PONO));
                cmd.Parameters.AddWithValue("@APRVSEQ", APRVSEQ);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public void SendMail(string MAILBODY, string SUBJECT, DataTable SENDER_RECEIVER)
        {
            try
            {
                MailMessage Msg;
                if (SENDER_RECEIVER.Rows.Count > 0)
                {
                    Msg = new MailMessage("info@qarmatek.com", Convert.ToString(SENDER_RECEIVER.Rows[0]["EMAILID"]));
                    for (int i = 1; i < SENDER_RECEIVER.Rows.Count; i++)
                    {
                        Msg.CC.Add(Convert.ToString(SENDER_RECEIVER.Rows[i]["EMAILID"]));
                    }
                }
                else
                {
                    Msg = new MailMessage("info@qarmatek.com", "mohit.diwakar@qarmatek.com");
                    //Msg.Bcc.Add("sentitem@qarma-tek.com");
                    //Msg.CC.Add("mohit.diwakar@qarmatek.com");
                    //Msg.CC.Add("mohit.divakar007@gmail.com");
                    //Msg.CC.Add("dhaval.vakta@qarmatek.com");
                    //Msg.From = new MailAddress("login@mobex.in", "uF&vg221");
                }

                Msg.Subject = SUBJECT;
                Msg.Body = MAILBODY;
                Msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                //smtp.Host = "dallas124.mysitehosted.com";
                smtp.Host = "smtp.office365.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                //NetworkCred.UserName = "login@mobex.in";
                //NetworkCred.Password = "uF&vg221";
                NetworkCred.UserName = "info@qarmatek.com";
                NetworkCred.Password = "Hof75626";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(Msg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void SendMailWithAttachment(string MAILTO, string MAILCC, string MAILFROM, string PASSWORD, int PORT, string SUBJECT, string MESSAGE, string ATTACHURL)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            try
            {
                MailMessage Msg = new MailMessage();
                Msg.To.Add(MAILTO);
                string[] CC = MAILCC.Split(';'); //Convert.ToString(dt.Rows[i]["SENDCC"]).Split(';');
                if (CC.Count() > 0)
                {
                    for (int j = 0; j < CC.Count(); j++)
                    {
                        if (Convert.ToString(CC[j]) != "" && Convert.ToString(CC[j]) != string.Empty && Convert.ToString(CC[j]) != null)
                        {
                            Msg.CC.Add(Convert.ToString(CC[j]));
                        }
                    }
                }
                else
                {
                    //Msg = new MailMessage("care@mobex.in", Convert.ToString(dt.Rows[i]["MAILTO"]));
                }
                Msg.From = new MailAddress(MAILFROM, SUBJECT);
                Msg.Subject = SUBJECT;
                Msg.Body = MESSAGE;

                //FileStream file = new FileStream(ATTACHURL, FileMode.Open, FileAccess.Read);
                //MemoryStream ms = new MemoryStream();
                //byte[] bytes = new byte[file.Length];
                //file.Read(bytes, 0, (int)file.Length);
                //ms.Write(bytes, 0, (int)file.Length);

                //Msg.Attachments.Add(new Attachment(ATTACHURL));
                //Msg.Attachments.Add(new Attachment(ms, new FileInfo(ATTACHURL).Name));

                //using (var stream = File.Open(ATTACHURL, FileMode.Open))
                //{
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(ATTACHURL);
                Msg.Attachments.Add(attachment);

                //}

                Msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.office365.com";
                smtp.EnableSsl = true;
                smtp.Port = PORT;
                smtp.Credentials = new System.Net.NetworkCredential(MAILFROM, PASSWORD);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(Msg);

                //file.Close();
                //ms.Close();
                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                smtp.Dispose();
                Msg.Attachments.Clear();
                Msg.Attachments.Dispose();
                attachment.Dispose();
                Msg.Dispose();


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool ApprovePR(string PRNO, int STATUS, string DOCTYPE, string USERID, string REASON, int STAGESEQ)
        {
            bool iResult = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlCommand PRcmd = new SqlCommand("SP_APRV_PRMST", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", intCmpId);
                    PRcmd.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                    PRcmd.Parameters.AddWithValue("@STATUS", STATUS);
                    PRcmd.Parameters.AddWithValue("@UPDATEBY", USERID);
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();



                    SqlCommand ARcmd = new SqlCommand("SP_INSERT_APRV_LOG", ConnSherpa);
                    ARcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    ARcmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(PRNO));
                    ARcmd.Parameters.AddWithValue("@APRVBY", USERID);
                    ARcmd.Parameters.AddWithValue("@REASON", REASON);
                    ARcmd.Parameters.AddWithValue("@STATUS", STATUS);
                    ARcmd.Parameters.AddWithValue("@STAGESEQ", STAGESEQ);
                    ARcmd.CommandType = CommandType.StoredProcedure;
                    ARcmd.Connection.Open();
                    ARcmd.ExecuteNonQuery();
                    ARcmd.Connection.Close();

                    scope.Complete();
                    scope.Dispose();
                    iResult = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return iResult;
        }


        public bool ApprovrPO(string DOCTYPE, string PONO, string USERID, string REASON, int STATUS, int STAGESEQ)
        {
            bool iResult = false;

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlCommand ARcmd = new SqlCommand("SP_INSERT_APRV_LOG", ConnSherpa);
                    ARcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    ARcmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(PONO));
                    ARcmd.Parameters.AddWithValue("@APRVBY", USERID);
                    ARcmd.Parameters.AddWithValue("@REASON", REASON);
                    ARcmd.Parameters.AddWithValue("@STATUS", STATUS);
                    ARcmd.Parameters.AddWithValue("@STAGESEQ", STAGESEQ);
                    ARcmd.CommandType = CommandType.StoredProcedure;
                    ARcmd.Connection.Open();
                    ARcmd.ExecuteNonQuery();
                    ARcmd.Connection.Close();


                    scope.Complete();
                    scope.Dispose();
                    iResult = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iResult;
        }

        public bool ApproveMR(string MRNO, int STATUS, string DOCTYPE, string USERID, string REASON, int STAGESEQ)
        {
            bool iResult = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {



                    SqlCommand PRcmd = new SqlCommand("SP_APRV_MRMST", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", intCmpId);
                    PRcmd.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                    PRcmd.Parameters.AddWithValue("@STATUS", STATUS);
                    PRcmd.Parameters.AddWithValue("@UPDATEBY", USERID);
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();



                    SqlCommand ARcmd = new SqlCommand("SP_INSERT_APRV_LOG", ConnSherpa);
                    ARcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    ARcmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(MRNO));
                    ARcmd.Parameters.AddWithValue("@APRVBY", USERID);
                    ARcmd.Parameters.AddWithValue("@REASON", REASON);
                    ARcmd.Parameters.AddWithValue("@STATUS", STATUS);
                    ARcmd.Parameters.AddWithValue("@STAGESEQ", STAGESEQ);
                    ARcmd.CommandType = CommandType.StoredProcedure;
                    ARcmd.Connection.Open();
                    ARcmd.ExecuteNonQuery();
                    ARcmd.Connection.Close();

                    scope.Complete();
                    scope.Dispose();
                    iResult = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return iResult;
        }

        public bool ApproveMRNEW(string MRNO, int STATUS, string DOCTYPE, string USERID, string REASON, int STAGESEQ)
        {
            bool iResult = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    SqlCommand RVcmd = new SqlCommand("SP_UPDATE_APRVLOG", ConnSherpa);
                    RVcmd.Parameters.AddWithValue("@MRNO", MRNO);
                    RVcmd.CommandType = CommandType.StoredProcedure;
                    RVcmd.Connection.Open();
                    RVcmd.ExecuteNonQuery();
                    RVcmd.Connection.Close();

                    SqlCommand PRcmd = new SqlCommand("SP_APRV_MRMST", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", intCmpId);
                    PRcmd.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                    PRcmd.Parameters.AddWithValue("@STATUS", STATUS);
                    PRcmd.Parameters.AddWithValue("@UPDATEBY", USERID);
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();



                    SqlCommand ARcmd = new SqlCommand("SP_INSERT_APRV_LOG", ConnSherpa);
                    ARcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    ARcmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(MRNO));
                    ARcmd.Parameters.AddWithValue("@APRVBY", USERID);
                    ARcmd.Parameters.AddWithValue("@REASON", REASON);
                    ARcmd.Parameters.AddWithValue("@STATUS", STATUS);
                    ARcmd.Parameters.AddWithValue("@STAGESEQ", STAGESEQ);
                    ARcmd.CommandType = CommandType.StoredProcedure;
                    ARcmd.Connection.Open();
                    ARcmd.ExecuteNonQuery();
                    ARcmd.Connection.Close();

                    scope.Complete();
                    scope.Dispose();
                    iResult = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return iResult;
        }

        public bool UpdateNewItemReq(int ID, int STATUS, string UPDATEBY)
        {
            bool iResult = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //SP_UPDATE_NEW_ITEMREQ
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_NEW_ITEMREQ", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@ID", ID);
                    PRcmd.Parameters.AddWithValue("@STATUS", STATUS);
                    PRcmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                    iResult = true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return iResult;
        }


        public DataTable SelectJobDetails(string JOBID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_JOBDETAILNEW", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@CMPID", intCmpId);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable SelectLocationBySegment(string SEGMENT)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_MST_LOCATION_BYSEGMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@CMPID", intCmpId);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable SelectUserSegment(string USERID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_SEGMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", USERID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public string Validate_CardNo(string strCouponNo, int iCheck)
        {
            string strReturn = "";

            SqlCommand cmd = new SqlCommand("SP_VALIDATE_CARDNO", ConnSherpa);
            cmd.Parameters.AddWithValue("@COUPONNO", strCouponNo);
            cmd.Parameters.AddWithValue("@CHECK", iCheck);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();

            if (obj != null)
            {
                strReturn = obj.ToString();
            }
            cmd.Connection.Close();
            return strReturn;
        }


        public string Validate_BasepackExpireRWR(string strCouponNo)
        {
            string strReturn = "";

            SqlCommand cmd = new SqlCommand("SP_CHECKBASEPACKEXPIRERWR", ConnSherpa);
            cmd.Parameters.AddWithValue("@CARDNO", strCouponNo);
            cmd.Parameters.AddWithValue("@CMPID", intCmpId);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            if (obj != null)
            {
                strReturn = obj.ToString();
            }
            cmd.Connection.Close();
            return strReturn;
        }

        public string Validate_CloseCouponNo(string strCouponNo)
        {
            string strReturn = "";

            SqlCommand cmd = new SqlCommand("SP_VALIDATE_CLOSECOUPONNO", ConnSherpa);
            cmd.Parameters.AddWithValue("@COUPONNO", strCouponNo);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            if (obj != null)
            {
                strReturn = obj.ToString();
            }
            cmd.Connection.Close();
            return strReturn;
        }


        public void UpdateCallAttempt(string CallID, string Callstart, string CallAttempBy, string strFlag)
        {
            SqlCommand cmd = new SqlCommand("SP_UPDATE_TRAN_CALLDATA", ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", intCmpId);
            cmd.Parameters.AddWithValue("@CALLID", CallID);
            cmd.Parameters.AddWithValue("@callattmby", int.Parse(CallAttempBy));
            if (strFlag == "S")
            {
                cmd.Parameters.AddWithValue("@callattmst", indianTime);
                cmd.Parameters.AddWithValue("@callattmend", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@callattmst", DBNull.Value);
                cmd.Parameters.AddWithValue("@callattmend", indianTime);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }


        public int SendSMSToServerOld(string strSMSTxt, string strJobNo, string MobileNo, string strDocType = "", Boolean ShowConfMsg = true, int StageId = 32, string strOriginator = "MOBEXX", string ErrorMessage = "")
        {
            int intResult = 0;
            try
            {
                if (strSMSTxt != string.Empty)
                {
                    if (MobileNo != "")
                    {
                        MobileNo = MobileNo.Trim().Replace(" ", "").Replace("-", "");

                        HttpWebRequest request;
                        string url;
                        string username;
                        string password;
                        string host;
                        string originator;

                        host = "http://alerts.sinfini.com";
                        originator = strOriginator; // "QRMTEK"
                        username = "qarmatek";
                        password = "dhaval12345";

                        url = host + "/api/web2sms.php?"
                                    + "username=" + username
                                    + "&password=" + password
                                    + "&sender=" + originator
                                    + "&to=" + MobileNo
                                    + "&message=" + strSMSTxt;

                        request = (HttpWebRequest)HttpWebRequest.Create(url);
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                        if (response.StatusDescription == "OK")
                        {
                            if (ShowConfMsg == true)
                            {
                                //MessageBox.Show("Response: " & response.StatusDescription & " SMS has been sent to " & MobileNo & ".", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            }
                            intResult = 1;
                            //updating SMS Log
                            DALLogs objDALLogs = new DALLogs();
                            objDALLogs.SaveSMSLogs(strSMSTxt, strConvertZeroPadding(strJobNo), MobileNo, originator, strDocType, StageId);
                        }
                        else
                        {
                            //'MessageBox.Show("Response: " & response.StatusDescription & " Error in sending SMS!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            ErrorMessage = "Response: " + response.StatusDescription + " Error in sending SMS!";
                            intResult = 0;
                        }
                    }
                }
                return intResult;

            }
            catch (Exception)
            {
                return intResult;
                // throw;
            }
        }


        public int SendSMSToServer(string strSMSTxt, string strJobNo, string MobileNo, string strDocType = "", Boolean ShowConfMsg = true, int StageId = 32, string strOriginator = "MOBEXX", string ErrorMessage = "")
        {
            int intResult = 0;
            try
            {
                if (strSMSTxt != string.Empty)
                {
                    if (MobileNo != "")
                    {
                        MobileNo = MobileNo.Trim().Replace(" ", "").Replace("-", "");

                        HttpWebRequest request;
                        string url;
                        string username;
                        string password;
                        string host;
                        string originator;

                        string APIKEY, CHANNEL, DCS, FLASH, ROUTE, MPREFIX;

                        DataTable dtAPI = new DataTable();
                        dtAPI = GetWAData("NEWSMSNEW", 1, "GETWADATA");

                        if (dtAPI.Rows.Count > 0)
                        {

                            host = Convert.ToString(dtAPI.Rows[0]["OTHER"]);

                            APIKEY = Convert.ToString(dtAPI.Rows[0]["KEYVALUE"]);
                            CHANNEL = Convert.ToString(dtAPI.Rows[0]["TOKEN"]);
                            DCS = Convert.ToString(dtAPI.Rows[0]["VERSION"]);
                            FLASH = Convert.ToString(dtAPI.Rows[0]["AREA"]);
                            ROUTE = Convert.ToString(dtAPI.Rows[0]["APITYPE"]);
                            MPREFIX = Convert.ToString(dtAPI.Rows[0]["UNIQUECODE"]);
                            originator = strOriginator;


                            host = host.Replace("@APIKEY", APIKEY).Replace("@SENDER", originator).Replace("@CHANL", CHANNEL).Replace("@DCS", DCS).Replace("@FLASH", FLASH).Replace("@ROUTE", ROUTE).Replace("@SMSTRN", strSMSTxt).Replace("@CONTACT", MPREFIX + "" + MobileNo);
                            //host = "http://alerts.sinfini.com";
                            //originator = strOriginator; // "QRMTEK"
                            //username = "qarmatek";
                            //password = "dhaval12345";

                            //url = host + "/api/web2sms.php?"
                            //            + "username=" + username
                            //            + "&password=" + password
                            //            + "&sender=" + originator
                            //            + "&to=" + MobileNo
                            //            + "&message=" + strSMSTxt;

                            url = host;

                            request = (HttpWebRequest)HttpWebRequest.Create(url);
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


                            if (response.StatusDescription == "OK")
                            {
                                if (ShowConfMsg == true)
                                {
                                    //MessageBox.Show("Response: " & response.StatusDescription & " SMS has been sent to " & MobileNo & ".", "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                }
                                intResult = 1;
                                //updating SMS Log
                                DALLogs objDALLogs = new DALLogs();
                                objDALLogs.SaveSMSLogs(strSMSTxt, strConvertZeroPadding(strJobNo), MobileNo, originator, strDocType, StageId);
                            }
                            else
                            {
                                //'MessageBox.Show("Response: " & response.StatusDescription & " Error in sending SMS!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                ErrorMessage = "Response: " + response.StatusDescription + " Error in sending SMS!";
                                intResult = 0;
                            }
                        }
                    }
                }
                return intResult;

            }
            catch (Exception)
            {
                return intResult;
                // throw;
            }
        }


        public class MessageData
        {
            public string Number { get; set; }
            public string MessageId { get; set; }
            public string Message { get; set; }
        }

        public class SMSResponse
        {
            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
            public string JobId { get; set; }
            public List<MessageData> MessageData { get; set; }
        }
        public int SendEmail(string toAddress, string subject, string body)
        {
            int iResult = 0;
            try
            {
                MailMessage Msg = new MailMessage();
                Msg.Bcc.Add("sentitem@qarma-tek.com");
                Msg.To.Add(toAddress);
                Msg.From = new MailAddress("customercare@qarmatek.com", "Blynk");
                Msg.Subject = subject;
                Msg.Body = body;
                Msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("customercare@qarmatek.com", "qarmatek1234");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(Msg);
                iResult = 1;
            }
            catch (Exception ex)
            {
                iResult = 0;
            }
            return iResult;
        }

        public DataTable SELECT_CITY_BYPINCODE(string strPincode)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand("SP_MST_CITY_SELECTBYPINCODE", ConnSherpa);
                cmd.Parameters.AddWithValue("@PINCODE", strPincode);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPincodeDetail(string strPincode)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CHECKFEDEXPINCODE", ConnSherpa);
            cmd.Parameters.AddWithValue("@POSTALCODE", strPincode);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        //SELECT_POSTO_DTL
        public DataTable GetPOSTODtl(int CMPID, string PONO, string POSRNO)
        {
            PONO = strConvertZeroPadding(PONO);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_POSTO_DTL", ConnSherpa);

            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PONO", PONO);
                cmd.Parameters.AddWithValue("@POSRNO", POSRNO);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetVendorDetails(int CMPID, string VENDORCODE, string VENDTYPE)
        {
            //SP_SELECT_VENDOR_BY_CODE
            DataTable dt = new DataTable();
            try
            {

                SqlCommand cmd = new SqlCommand("SP_SELECT_VENDOR_BY_CODE", ConnSherpa);
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@VENDORCODE", strConvertZeroPadding(VENDORCODE));
                cmd.Parameters.AddWithValue("@VENDORTYPE", VENDTYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable GetVendor(int CMPID, string VENDCODE, string VENDNAME, int DEALERID, int APPROVED, string CITYID, string VENDTYPE, int VENDCAT, string ACTION, string FROMDATE, string TODATE)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@VENDCODE", VENDCODE == "" ? VENDCODE : strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@VENDNAME", VENDNAME);
                cmd.Parameters.AddWithValue("@DEALERID", DEALERID);
                cmd.Parameters.AddWithValue("@ISACTIVE", APPROVED);
                cmd.Parameters.AddWithValue("@CITY", CITYID);
                cmd.Parameters.AddWithValue("@VENDTYPE", VENDTYPE);
                cmd.Parameters.AddWithValue("@VENDCAT", VENDCAT);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public int InsertOnAccountBankEntry(int CMPID, string DOCTYPE, string DOCNO, string DOCDATE, string BANKAC, int ADVFLAG, int OACFLAG, string POSONO, string PARTYAC, string DISCAC, string REMARKS, string DISCAMT,
            string DOCAMT, string ADJAMT, int CREATEBY, string TXNID, int PAYMENTMODE, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    DOCNO = MAXFIRANGENO(intCmpId, getFinYear(setDateFormat(DateTime.Now.ToShortDateString(), true)), "BP");

                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                    cmd.Parameters.AddWithValue("@DOCDT", setDateFormat(DOCDATE, true));
                    cmd.Parameters.AddWithValue("@BANKAC", BANKAC);
                    cmd.Parameters.AddWithValue("@ADVFLAG", ADVFLAG);
                    cmd.Parameters.AddWithValue("@OACFLAG", OACFLAG);
                    cmd.Parameters.AddWithValue("@POSONO", POSONO);
                    cmd.Parameters.AddWithValue("@PARTYAC", strConvertZeroPadding(PARTYAC));
                    cmd.Parameters.AddWithValue("@DISCAC", DISCAC);
                    cmd.Parameters.AddWithValue("@REMARK", REMARKS);
                    cmd.Parameters.AddWithValue("@DISCAMT", DISCAMT);
                    cmd.Parameters.AddWithValue("@DOCAMT", DOCAMT);
                    cmd.Parameters.AddWithValue("@ADJAMT", ADJAMT);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@TXNID", TXNID);
                    cmd.Parameters.AddWithValue("@PAYMENTMODE", PAYMENTMODE);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    iResult = 1;

                    string RANGENO = UPDATEMAXFIRANGENO(CMPID, "BP", getFinYear(setDateFormat(DateTime.Now.ToShortDateString(), true)), DOCNO);
                    if (RANGENO != "" && RANGENO != null && RANGENO != string.Empty)
                    {
                        iResult = 1;
                    }

                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
            }

            return iResult;
        }

        public int InsertAdvanceBankEntry(int CMPID, string DOCTYPE, string DOCNO, string DOCDATE, string BANKAC, int ADVFLAG, int OACFLAG, string PARTYAC, string DISCAC, string REMARKS,
            string DISCAMT, int CREATEBY, string TXNID, int PAYMENTMODE, GridView gvList, string ACTION)
        {
            int iResult = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
                        GridViewRow row = gvList.Rows[i];
                        CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                        if (chkSelect.Checked == true)
                        {



                            DOCNO = MAXFIRANGENO(intCmpId, getFinYear(setDateFormat(DateTime.Now.ToShortDateString(), true)), "BP");
                            cmd.Parameters.AddWithValue("@CMPID", CMPID);
                            cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                            cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                            cmd.Parameters.AddWithValue("@DOCDT", setDateFormat(DOCDATE, true));
                            cmd.Parameters.AddWithValue("@BANKAC", BANKAC);
                            cmd.Parameters.AddWithValue("@ADVFLAG", ADVFLAG);
                            cmd.Parameters.AddWithValue("@OACFLAG", OACFLAG);
                            cmd.Parameters.AddWithValue("@POSONO", ((Label)row.FindControl("lblPONO")).Text);
                            cmd.Parameters.AddWithValue("@PARTYAC", strConvertZeroPadding(PARTYAC));
                            cmd.Parameters.AddWithValue("@DISCAC", DISCAC);
                            cmd.Parameters.AddWithValue("@REMARK", REMARKS);
                            cmd.Parameters.AddWithValue("@DISCAMT", DISCAMT);
                            cmd.Parameters.AddWithValue("@DOCAMT", ((TextBox)row.FindControl("txtPayableAmt")).Text);
                            cmd.Parameters.AddWithValue("@ADJAMT", 0);
                            cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                            cmd.Parameters.AddWithValue("@TXNID", TXNID);
                            cmd.Parameters.AddWithValue("@PAYMENTMODE", PAYMENTMODE);
                            cmd.Parameters.AddWithValue("@ACTION", ACTION);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();

                            //SqlCommand POcmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
                            //POcmd.Parameters.AddWithValue("@ADJAMT", ((TextBox)row.FindControl("txtPayableAmt")).Text);
                            //POcmd.Parameters.AddWithValue("@ADVAMT", ((TextBox)row.FindControl("txtPayableAmt")).Text);
                            //POcmd.Parameters.AddWithValue("@PENDINGAMT", Convert.ToDecimal(((Label)row.FindControl("lblPayableAmt")).Text) - Convert.ToDecimal(((TextBox)row.FindControl("txtPayableAmt")).Text));
                            //POcmd.Parameters.AddWithValue("@UPDATEBY", CREATEBY);
                            //POcmd.Parameters.AddWithValue("@CMPID", CMPID);
                            //POcmd.Parameters.AddWithValue("@PONO", ((Label)row.FindControl("lblPONO")).Text);
                            //POcmd.Parameters.AddWithValue("@ACTION", "UPDATEPO");
                            //POcmd.CommandType = CommandType.StoredProcedure;
                            //POcmd.Connection.Open();
                            //POcmd.ExecuteNonQuery();
                            //POcmd.Connection.Close();


                            string RANGENO = UPDATEMAXFIRANGENO(CMPID, "BP", getFinYear(setDateFormat(DateTime.Now.ToShortDateString(), true)), DOCNO);
                            if (RANGENO != "" && RANGENO != null && RANGENO != string.Empty)
                            {
                                iResult = 1;
                            }
                        }
                    }

                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return iResult;
        }


        public int InsertPBBankEntry(int CMPID, string DOCTYPE, string DOCNO, string DOCDATE, string BANKAC, int ADVFLAG, int OACFLAG, string PARTYAC, string DISCAC, string REMARKS,
         string DISCAMT, string DOCAMT, int CREATEBY, string TXNID, int PAYMENTMODE, GridView gvList, string ACTION)
        {
            int iResult = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
                    DOCNO = MAXFIRANGENO(intCmpId, getFinYear(setDateFormat(DateTime.Now.ToShortDateString(), true)), "BP");
                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                    cmd.Parameters.AddWithValue("@DOCDT", setDateFormat(DOCDATE, true));
                    cmd.Parameters.AddWithValue("@BANKAC", BANKAC);
                    cmd.Parameters.AddWithValue("@ADVFLAG", ADVFLAG);
                    cmd.Parameters.AddWithValue("@OACFLAG", OACFLAG);
                    //cmd.Parameters.AddWithValue("@POSONO", ((Label)row.FindControl("lblPBNO")).Text);
                    cmd.Parameters.AddWithValue("@POSONO", "");
                    cmd.Parameters.AddWithValue("@PARTYAC", strConvertZeroPadding(PARTYAC));
                    cmd.Parameters.AddWithValue("@DISCAC", DISCAC);
                    cmd.Parameters.AddWithValue("@REMARK", REMARKS);
                    cmd.Parameters.AddWithValue("@DISCAMT", DISCAMT);
                    cmd.Parameters.AddWithValue("@DOCAMT", DOCAMT);
                    cmd.Parameters.AddWithValue("@ADJAMT", 0);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@TXNID", TXNID);
                    cmd.Parameters.AddWithValue("@PAYMENTMODE", PAYMENTMODE);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    for (int i = 0; i < gvList.Rows.Count; i++)
                    {
                        GridViewRow row = gvList.Rows[i];
                        CheckBox chkSelect = row.FindControl("chkSelectPBAdj") as CheckBox;
                        if (chkSelect.Checked == true)
                        {
                            SqlCommand ADJcmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
                            ADJcmd.Parameters.AddWithValue("@CMPID", CMPID);
                            ADJcmd.Parameters.AddWithValue("@PDOCTYPE", ((Label)row.FindControl("lblPBTYPE")).Text);
                            if (((Label)row.FindControl("lblPBTYPE")).Text == "OB")
                            {
                                ADJcmd.Parameters.AddWithValue("@PDOCNO", getFinYear(setDateFormat(DateTime.Now.ToShortDateString(), true)) + "0" + (((Label)row.FindControl("lblVENDCODE")).Text).TrimStart(new Char[] { '0' }));
                            }
                            else
                            {
                                ADJcmd.Parameters.AddWithValue("@PDOCNO", ((Label)row.FindControl("lblPBNO")).Text);
                            }
                            ADJcmd.Parameters.AddWithValue("@PDOCDT", setDateFormat(((Label)row.FindControl("lblPBDT")).Text, true));
                            ADJcmd.Parameters.AddWithValue("@REFTYPE", "BP");
                            if (((TextBox)row.FindControl("txtPayableAmt")).Text.Contains("-") == true)
                            {
                                ADJcmd.Parameters.AddWithValue("@CRDR", "DR");
                            }
                            else
                            {
                                ADJcmd.Parameters.AddWithValue("@CRDR", "CR");
                            }
                            ADJcmd.Parameters.AddWithValue("@REFNO", DOCNO);
                            ADJcmd.Parameters.AddWithValue("@REFDT", setDateFormat(DOCDATE, true));
                            ADJcmd.Parameters.AddWithValue("@ADJAMT", ((TextBox)row.FindControl("txtPayableAmt")).Text);
                            ADJcmd.Parameters.AddWithValue("@REMARK", REMARKS);
                            ADJcmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                            ADJcmd.Parameters.AddWithValue("@ACTION", "BANKADJENTRY");
                            ADJcmd.CommandType = CommandType.StoredProcedure;
                            ADJcmd.Connection.Open();
                            ADJcmd.ExecuteNonQuery();
                            ADJcmd.Connection.Close();

                            if (((TextBox)row.FindControl("txtPayableAmt")).Text.Contains("-") == true)
                            {
                                if (((Label)row.FindControl("lblPBTYPE")).Text == "OB")
                                {
                                    SqlCommand OBcmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
                                    OBcmd.Parameters.AddWithValue("@CMPID", CMPID);
                                    OBcmd.Parameters.AddWithValue("@ADJAMT", ((TextBox)row.FindControl("txtPayableAmt")).Text);
                                    OBcmd.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(PARTYAC));
                                    OBcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(setDateFormat(DateTime.Now.ToShortDateString(), true)));
                                    OBcmd.Parameters.AddWithValue("@ACTION", "UPDATEOB");
                                    OBcmd.CommandType = CommandType.StoredProcedure;
                                    OBcmd.Connection.Open();
                                    OBcmd.ExecuteNonQuery();
                                    OBcmd.Connection.Close();
                                }
                                else
                                {
                                    SqlCommand BPcmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
                                    BPcmd.Parameters.AddWithValue("@CMPID", CMPID);
                                    BPcmd.Parameters.AddWithValue("@ADJAMT", ((TextBox)row.FindControl("txtPayableAmt")).Text);
                                    BPcmd.Parameters.AddWithValue("@DOCNO", ((Label)row.FindControl("lblPBNO")).Text);
                                    BPcmd.Parameters.AddWithValue("@ACTION", "BPADJUST");
                                    BPcmd.CommandType = CommandType.StoredProcedure;
                                    BPcmd.Connection.Open();
                                    BPcmd.ExecuteNonQuery();
                                    BPcmd.Connection.Close();
                                }
                            }

                            //if (!((TextBox)row.FindControl("txtPayableAmt")).Text.Contains("-") == true)
                            if (((Label)row.FindControl("lblPBTYPE")).Text == "MPB")
                            {
                                SqlCommand PBcmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
                                PBcmd.Parameters.AddWithValue("@ADJAMT", ((TextBox)row.FindControl("txtPayableAmt")).Text);
                                PBcmd.Parameters.AddWithValue("@ADVAMT", 0);
                                PBcmd.Parameters.AddWithValue("@PENDINGAMT", Convert.ToDecimal(((Label)row.FindControl("lblPayableAmt")).Text) - Convert.ToDecimal(((TextBox)row.FindControl("txtPayableAmt")).Text));
                                PBcmd.Parameters.AddWithValue("@UPDATEBY", CREATEBY);
                                PBcmd.Parameters.AddWithValue("@CMPID", CMPID);
                                PBcmd.Parameters.AddWithValue("@PONO", ((Label)row.FindControl("lblPBNO")).Text);
                                PBcmd.Parameters.AddWithValue("@ACTION", "UPDATEPB");
                                PBcmd.CommandType = CommandType.StoredProcedure;
                                PBcmd.Connection.Open();
                                PBcmd.ExecuteNonQuery();
                                PBcmd.Connection.Close();
                            }

                            if (((Label)row.FindControl("lblPBTYPE")).Text == "SCM" || ((Label)row.FindControl("lblPBTYPE")).Text == "SIT" || ((Label)row.FindControl("lblPBTYPE")).Text == "SCR")
                            {
                                SqlCommand PBcmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
                                PBcmd.Parameters.AddWithValue("@ADJAMT", ((TextBox)row.FindControl("txtPayableAmt")).Text);
                                //PBcmd.Parameters.AddWithValue("@ADVAMT", 0);
                                PBcmd.Parameters.AddWithValue("@PENDINGAMT", Convert.ToDecimal(((Label)row.FindControl("lblPayableAmt")).Text) - Convert.ToDecimal(((TextBox)row.FindControl("txtPayableAmt")).Text));
                                PBcmd.Parameters.AddWithValue("@UPDATEBY", CREATEBY);
                                PBcmd.Parameters.AddWithValue("@CMPID", CMPID);
                                PBcmd.Parameters.AddWithValue("@PONO", strConvertZeroPadding(((Label)row.FindControl("lblPBNO")).Text));
                                PBcmd.Parameters.AddWithValue("@ACTION", "UPDATESI");
                                PBcmd.CommandType = CommandType.StoredProcedure;
                                PBcmd.Connection.Open();
                                PBcmd.ExecuteNonQuery();
                                PBcmd.Connection.Close();
                            }

                            if (((Label)row.FindControl("lblPBTYPE")).Text == "DN")
                            {
                                SqlCommand PBcmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
                                PBcmd.Parameters.AddWithValue("@ADJAMT", ((TextBox)row.FindControl("txtPayableAmt")).Text);
                                //PBcmd.Parameters.AddWithValue("@ADVAMT", 0);
                                PBcmd.Parameters.AddWithValue("@PENDINGAMT", Convert.ToDecimal(((Label)row.FindControl("lblPayableAmt")).Text) - Convert.ToDecimal(((TextBox)row.FindControl("txtPayableAmt")).Text));
                                PBcmd.Parameters.AddWithValue("@UPDATEBY", CREATEBY);
                                PBcmd.Parameters.AddWithValue("@CMPID", CMPID);
                                PBcmd.Parameters.AddWithValue("@PONO", strConvertZeroPadding(((Label)row.FindControl("lblPBNO")).Text));
                                PBcmd.Parameters.AddWithValue("@ACTION", "UPDATESI");
                                PBcmd.CommandType = CommandType.StoredProcedure;
                                PBcmd.Connection.Open();
                                PBcmd.ExecuteNonQuery();
                                PBcmd.Connection.Close();
                            }



                            string RANGENO = UPDATEMAXFIRANGENO(CMPID, "BP", getFinYear(setDateFormat(DateTime.Now.ToShortDateString(), true)), DOCNO);
                            if (RANGENO != "" && RANGENO != null && RANGENO != string.Empty)
                            {
                                iResult = 1;
                            }
                        }
                    }

                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            return iResult;
        }


        public DataTable GetPendingPayData(int CMPID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetPendingAdjPayData(int CMPID, string REFNO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@REFNO", REFNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public int UpdateActualPay(int CMPID, string DOCNO, string BANKAC, int USERID, DateTime PAYDATE, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                cmd.Parameters.AddWithValue("@BANKAC", BANKAC);
                cmd.Parameters.AddWithValue("@ACTUALPAYBY", USERID);
                cmd.Parameters.AddWithValue("@ACTUALPAYDATE", PAYDATE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;

        }

        public DataTable ExportPaymentEntry(int CMPID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public int InsertAdjustmentEntry(int CMPID, string PDOCTYPE, string PDOCNO, string PDOCDT, string REFTYPE, string REFNO, string REFDT, string ADJAMT, string REMARK,
            int CREATEBY, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PDOCTYPE", PDOCTYPE);
                cmd.Parameters.AddWithValue("@PDOCNO", PDOCNO);
                cmd.Parameters.AddWithValue("@PDOCDT", PDOCDT);
                cmd.Parameters.AddWithValue("@REFTYPE", REFTYPE);
                cmd.Parameters.AddWithValue("@REFNO", REFNO);
                cmd.Parameters.AddWithValue("@REFDT", REFDT);
                cmd.Parameters.AddWithValue("@ADJAMT", ADJAMT);
                cmd.Parameters.AddWithValue("@REMARK", REMARK);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public string Select_BizlogPostalcode(string PostalCode)
        {
            try
            {
                string strReturn = "";
                SqlCommand cmd = new SqlCommand("SELECT_BIZLOGPOSTALCODE", ConnSherpa);
                cmd.Parameters.AddWithValue("@POSTALCODE", PostalCode);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                object obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    strReturn = obj.ToString();
                }
                cmd.Connection.Close();
                return strReturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string SaveMaterialInwardFromPo(Int64 FINYEAR, string DOCNO, string DOCTYPE, string DOCDATE, string CHLNNO, string CHLNDT, string TRANCODE, string REFDOCNO, string REFNO, string REMARK,
           string MODE, GridView grvListItem, string UserId, byte[] bytes, string EXTENSION, byte[] PObytes, string POExtension)
        {
            MainClass objMainClass = new MainClass();
            bool IsAddUpdate = false;
            int result = 0;
            try
            {
                DOCNO = MAXPRNO(DOCTYPE, "MIR");
                using (TransactionScope scope = new TransactionScope())
                {
                    #region Insert TRAN_MMMST...
                    SqlCommand cmdM = new SqlCommand("SP_PO_MATERIAL_INWARDTRAN_MMMSTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@FINYEAR", FINYEAR);
                    cmdM.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                    cmdM.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    cmdM.Parameters.AddWithValue("@DOCDATE", setDateFormat(DOCDATE, true));
                    cmdM.Parameters.AddWithValue("@CHLNNO", CHLNNO);
                    cmdM.Parameters.AddWithValue("@CHLNDT", setDateFormat(CHLNDT, true));
                    cmdM.Parameters.AddWithValue("@POSTDATE", setDateFormat(DOCDATE, true));
                    cmdM.Parameters.AddWithValue("@TRANCODE", strConvertZeroPadding(TRANCODE));
                    cmdM.Parameters.AddWithValue("@REFDOCNO", strConvertZeroPadding(REFDOCNO));
                    cmdM.Parameters.AddWithValue("@REFNO", REFNO);
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    if (result > 0)
                    {
                        IsAddUpdate = true;
                    }
                    #endregion

                    #region Update MMNORANGE...
                    if (MODE == "I")
                    {
                        SqlCommand MMcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                        MMcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                        MMcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(DOCNO));
                        MMcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                        MMcmd.Parameters.AddWithValue("@DOCTYPE", "MIR");
                        MMcmd.CommandType = CommandType.StoredProcedure;
                        MMcmd.Connection.Open();
                        result = MMcmd.ExecuteNonQuery();
                        MMcmd.Connection.Close();
                        if (result > 0)
                        {
                            IsAddUpdate = true;
                        }
                    }
                    #endregion


                    for (int i = 0; i < grvListItem.Rows.Count; i++)
                    {
                        GridViewRow row = grvListItem.Rows[i];
                        SqlCommand cmdD = new SqlCommand("SP_PO_MATERIAL_INWARDTRAN_MMCRUDOPERATION", ConnSherpa);
                        cmdD.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                        cmdD.Parameters.AddWithValue("@FINYEAR", FINYEAR);
                        cmdD.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                        cmdD.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                        cmdD.Parameters.AddWithValue("@SRNO", ((HiddenField)row.FindControl("hdSrNo")).Value);
                        cmdD.Parameters.AddWithValue("@PONO", ((HiddenField)row.FindControl("hdPoNo")).Value);
                        cmdD.Parameters.AddWithValue("@POSRNO", ((HiddenField)row.FindControl("hdPoSrNo")).Value);
                        cmdD.Parameters.AddWithValue("@UOM", ((HiddenField)row.FindControl("hdUOMID")).Value);
                        cmdD.Parameters.AddWithValue("@ITEMID", ((HiddenField)row.FindControl("hdITEMID")).Value);
                        cmdD.Parameters.AddWithValue("@QTY", ((HiddenField)row.FindControl("hdQty")).Value);
                        cmdD.Parameters.AddWithValue("@CHLNQTY", ((HiddenField)row.FindControl("hdChalanQty")).Value);
                        cmdD.Parameters.AddWithValue("@RATE", ((HiddenField)row.FindControl("hdRATE")).Value);
                        cmdD.Parameters.AddWithValue("@CAMOUNT", ((HiddenField)row.FindControl("hdCAMOUNT")).Value);
                        cmdD.Parameters.AddWithValue("@ITEMTEXT", ((HiddenField)row.FindControl("hdItemText")).Value);
                        cmdD.Parameters.AddWithValue("@CSTCENTCD", ((HiddenField)row.FindControl("hdCostCenter")).Value);
                        cmdD.Parameters.AddWithValue("@PRFCNT", ((HiddenField)row.FindControl("hdprfct")).Value);
                        cmdD.Parameters.AddWithValue("@PLANTCD", ((HiddenField)row.FindControl("hdPlantCode")).Value);
                        cmdD.Parameters.AddWithValue("@LOCCD", ((HiddenField)row.FindControl("hdLoccd")).Value);
                        cmdD.Parameters.AddWithValue("@GLCD", ((HiddenField)row.FindControl("hdGlCd")).Value);
                        cmdD.Parameters.AddWithValue("@ASSETCD", ((HiddenField)row.FindControl("hdAssetcd")).Value);
                        cmdD.Parameters.AddWithValue("@ITEMDESC", ((HiddenField)row.FindControl("hdItemDesc")).Value);
                        cmdD.Parameters.AddWithValue("@ITEMGRPID", ((HiddenField)row.FindControl("hdITEMGRPID")).Value);
                        cmdD.Parameters.AddWithValue("@MODE", MODE);
                        cmdD.CommandType = CommandType.StoredProcedure;
                        cmdD.Connection.Open();
                        result = cmdD.ExecuteNonQuery();
                        cmdD.Connection.Close();
                        if (result > 0)
                        {
                            IsAddUpdate = true;
                        }
                    }


                    SqlCommand imgCMD = new SqlCommand("SP_INSERT_GRNINVIMAGE", ConnSherpa);
                    imgCMD.Parameters.AddWithValue("@PONO", strConvertZeroPadding(REFDOCNO));
                    imgCMD.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                    imgCMD.Parameters.AddWithValue("@INVIMAGE", bytes);
                    imgCMD.Parameters.AddWithValue("@EXTENSION", EXTENSION);
                    imgCMD.Parameters.AddWithValue("@POIMAGE", PObytes);
                    imgCMD.Parameters.AddWithValue("@POEXTENSION", POExtension);
                    imgCMD.CommandType = CommandType.StoredProcedure;
                    imgCMD.Connection.Open();
                    imgCMD.ExecuteNonQuery();
                    imgCMD.Connection.Close();

                    scope.Complete();
                    scope.Dispose();
                }
                if (IsAddUpdate)
                {
                    return DOCNO;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                IsAddUpdate = false;
                Console.WriteLine(ex);
                return "";
            }
        }

        public DataTable CheckPOGRN(int CMPID, string PONO, string GRNNO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_POGRNCHECK", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PONO", PONO == "" ? "" : strConvertZeroPadding(PONO));
                cmd.Parameters.AddWithValue("@GRNNO", GRNNO == "" ? "" : strConvertZeroPadding(GRNNO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UploadPODoc(string PONO, string DOCNO, byte[] bytes, string EXTENSION, byte[] PObytes, string POExtension)
        {
            int i = 0;
            SqlCommand imgCMD = new SqlCommand("SP_INSERT_GRNINVIMAGE", ConnSherpa);
            try
            {
                imgCMD.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                imgCMD.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                imgCMD.Parameters.AddWithValue("@INVIMAGE", bytes);
                imgCMD.Parameters.AddWithValue("@EXTENSION", EXTENSION);
                imgCMD.Parameters.AddWithValue("@POIMAGE", PObytes);
                imgCMD.Parameters.AddWithValue("@POEXTENSION", POExtension);
                imgCMD.CommandType = CommandType.StoredProcedure;
                imgCMD.Connection.Open();
                imgCMD.ExecuteNonQuery();
                imgCMD.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                imgCMD.Connection.Close();
                throw ex;
            }
            return i;
        }

        public DataTable GetPOMaterialInwardItemDetail(int CMPID, string PONO, string DOCNO, string DOCTYPE, string MODE)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            //SP_SELECT_PODATA
            SqlCommand cmd = new SqlCommand("SP_PO_MATERIAL_INWARDITEMDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@MODE", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetMaterialInwardFromPO(int CMPID, string DOCNO, string DOCTYPE, string FROMDATE, string TODATE, string REFDOCNO, string REFNO, string MAINQUERY, string PLANTCODE)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            //SP_SELECT_PODATA
            SqlCommand cmd = new SqlCommand("SP_SELECT_MATERIALINWARDFROMPODATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", (DOCNO.Length == 0 ? null : strConvertZeroPadding(DOCNO)));
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@REFDOCNO", REFDOCNO);
                cmd.Parameters.AddWithValue("@REFNO", REFNO);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable SelectGRNIMAGE(string PONO, string DOCNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_GRNINVIMAGE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable SelectMRInvoice(int CMPID, string MRNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MR_INVOICE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MRNO", strConvertZeroPadding(MRNO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        public DataTable GetEachMaterialInwardPoData(int CMPID, string DOCNO, string DOCTYPE, string SELEFROM)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GET_PO_MATERIAL_INWARDDATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@SELEFROM", SELEFROM);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public string InsertDepartmentMaterialIssue(string DOCTYPE, string DOCNO, string DOCDT, int DEPTCD, string EMPNAME, string REFNO, string REMARKS, GridView GRVLIST, string USERID, int CMPID, int UPDATEACTION)
        {
            MainClass objMainClass = new MainClass();
            string DOCKNO = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DOCKNO = MAXPRNO(DOCTYPE, "MI");
                    DOCKNO = strConvertZeroPadding(DOCKNO);
                    #region Update MMNORANGE...
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(DOCKNO));
                    PRcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                    PRcmd.Parameters.AddWithValue("@DOCTYPE", "MI");
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    #endregion

                    #region Insert MMMST

                    SqlCommand MMcmd = new SqlCommand("SP_PO_MATERIAL_INWARDTRAN_MMMSTCRUDOPERATION", ConnSherpa);
                    MMcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    MMcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                    MMcmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCKNO));
                    MMcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    MMcmd.Parameters.AddWithValue("@DOCDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@POSTDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@DEPTCD", DEPTCD);
                    MMcmd.Parameters.AddWithValue("@EMPNAME", EMPNAME);
                    MMcmd.Parameters.AddWithValue("@REFNO", REFNO);
                    MMcmd.Parameters.AddWithValue("@REMARK", REMARKS);
                    MMcmd.Parameters.AddWithValue("@CREATEBY", USERID);
                    MMcmd.Parameters.AddWithValue("@MODE", "I");
                    MMcmd.CommandType = CommandType.StoredProcedure;
                    MMcmd.Connection.Open();
                    MMcmd.ExecuteNonQuery();
                    MMcmd.Connection.Close();

                    #endregion

                    #region Insert MM Details

                    for (int i = 0; i < GRVLIST.Rows.Count; i++)
                    {
                        GridViewRow row = GRVLIST.Rows[i];
                        SqlCommand MDcmd = new SqlCommand("SP_INSERT_TRAN_MM", ConnSherpa);
                        MDcmd.Parameters.AddWithValue("@CMPID", CMPID);
                        MDcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                        MDcmd.Parameters.AddWithValue("@DOCNO", DOCKNO);
                        MDcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                        MDcmd.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblID")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblItemId")).Text);
                        MDcmd.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblPlantID")).Text);
                        MDcmd.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblLocationCDID")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGroupId")).Text);
                        MDcmd.Parameters.AddWithValue("@itemdesc", ((Label)row.FindControl("lblItemDesc")).Text);
                        MDcmd.Parameters.AddWithValue("@QTY", ((Label)row.FindControl("lblQty")).Text);
                        MDcmd.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblUOMID")).Text);
                        MDcmd.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGLCode")).Text);
                        MDcmd.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblCostCenter")).Text);
                        MDcmd.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblProfitCenter")).Text);
                        MDcmd.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblAssetCode")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblRemarks")).Text);
                        MDcmd.Parameters.AddWithValue("@TOITEMID", 0);
                        MDcmd.Parameters.AddWithValue("@TRACKNO", ((Label)row.FindControl("lblTrackNo")).Text == "" ? "0" : ((Label)row.FindControl("lblTrackNo")).Text);
                        MDcmd.Parameters.AddWithValue("@MODE", "I");
                        MDcmd.CommandType = CommandType.StoredProcedure;
                        MDcmd.Connection.Open();
                        MDcmd.ExecuteNonQuery();
                        MDcmd.Connection.Close();

                    }
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                    return DOCKNO;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }


        public string InsertSTODCData(int CMPID, string DOCTYPE, string DOCKTYPE, string DOCNO, string DOCDT, string REFNO, string TRANCODE, string REFDOCNO, string REVDOCNO, string REMARK, string USERID, string CHALLANNO, string CHALLANDT, string DEPTCODE, string EMPNAME, string DOCKNO, string NOOFBOX, GridView GRVLIST)
        {
            DOCNO = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    DOCNO = MAXPRNO(DOCTYPE, DOCKTYPE);
                    DOCNO = strConvertZeroPadding(DOCNO);

                    #region Update MMNORANGE...
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(DOCNO));
                    PRcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                    PRcmd.Parameters.AddWithValue("@DOCTYPE", DOCKTYPE);
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    #endregion

                    #region Insert MMMST

                    SqlCommand MMcmd = new SqlCommand("SP_ALL_TRAN_MMMST", ConnSherpa);
                    MMcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    MMcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                    MMcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    MMcmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                    MMcmd.Parameters.AddWithValue("@DOCDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@POSTDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@REFNO", REFNO);
                    MMcmd.Parameters.AddWithValue("@TRANCODE", TRANCODE);
                    MMcmd.Parameters.AddWithValue("@REFDOCNO", REFDOCNO);
                    MMcmd.Parameters.AddWithValue("@REFDOCYEAR", getFinYear(DOCDT));
                    MMcmd.Parameters.AddWithValue("@REVYEAR", Convert.ToInt32(getFinYear(DOCDT)));
                    MMcmd.Parameters.AddWithValue("@REMARK", REMARK);
                    MMcmd.Parameters.AddWithValue("@CREATEBY", Convert.ToInt32(USERID));
                    MMcmd.Parameters.AddWithValue("@CHLNNO", CHALLANNO);
                    MMcmd.Parameters.AddWithValue("@CHLNDT", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@DOCKNO", DOCKNO);
                    MMcmd.Parameters.AddWithValue("@NOOFBOX", NOOFBOX);
                    MMcmd.CommandType = CommandType.StoredProcedure;
                    MMcmd.Connection.Open();
                    MMcmd.ExecuteNonQuery();
                    MMcmd.Connection.Close();

                    #endregion


                    #region Insert MM Details

                    for (int i = 0; i < GRVLIST.Rows.Count; i++)
                    {
                        GridViewRow row = GRVLIST.Rows[i];
                        SqlCommand MDcmd = new SqlCommand("SP_ALL_INSERT_TRAN_MM", ConnSherpa);

                        MDcmd.Parameters.AddWithValue("@CMPID", CMPID);
                        MDcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                        MDcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                        MDcmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                        MDcmd.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblGVSrNo")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblGVItemID")).Text);
                        MDcmd.Parameters.AddWithValue("@QTY", ((Label)row.FindControl("lblGVPOQty")).Text);
                        MDcmd.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblGVUOMID")).Text);
                        MDcmd.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblGVItemRate")).Text);
                        MDcmd.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblGVItemAmount")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblGVItemText")).Text);
                        MDcmd.Parameters.AddWithValue("@PONO", ((Label)row.FindControl("lblGVPONO")).Text);
                        MDcmd.Parameters.AddWithValue("@POSRNO", ((Label)row.FindControl("lblGVPoSrNo")).Text);
                        MDcmd.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblGVCostCenter")).Text);
                        MDcmd.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblGVProfitCenter")).Text);
                        MDcmd.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblGVFromPlantID")).Text);
                        MDcmd.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblGVFromLocationID")).Text);
                        MDcmd.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGVGLCODE")).Text);
                        MDcmd.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblGVAssetcode")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblItemGroupID")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblGVItemDesc")).Text);
                        MDcmd.Parameters.AddWithValue("@TOPLANTCD", ((Label)row.FindControl("lblGVToPlantID")).Text);
                        MDcmd.Parameters.AddWithValue("@TOLOCCD", ((Label)row.FindControl("lblGVToLocationID")).Text);
                        MDcmd.Parameters.AddWithValue("@CHLNQTY", ((Label)row.FindControl("lblGVChlnQty")).Text);
                        MDcmd.Parameters.AddWithValue("@ACPTQTY", 0);
                        MDcmd.Parameters.AddWithValue("@RTNQTY", 0);
                        MDcmd.CommandType = CommandType.StoredProcedure;
                        MDcmd.Connection.Open();
                        MDcmd.ExecuteNonQuery();
                        MDcmd.Connection.Close();

                    }
                    #endregion


                    scope.Complete();
                    scope.Dispose();
                    return DOCNO;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        public string InsertSTODCINData(int CMPID, string DOCTYPE, string DOCKTYPE, string DOCNO, string DOCDT, string PONO, string REFNO, string TRANCODE, string REFDOCNO, string REMARK, string USERID, string CHALLANNO, string CHALLANDT, GridView GRVLIST)
        {
            DOCNO = string.Empty;
            PONO = strConvertZeroPadding(PONO);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    DOCNO = MAXPRNO(DOCTYPE, DOCKTYPE);
                    DOCNO = strConvertZeroPadding(DOCNO);

                    #region Update MMNORANGE...
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(DOCNO));
                    PRcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                    PRcmd.Parameters.AddWithValue("@DOCTYPE", DOCKTYPE);
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    #endregion

                    #region Insert MMMST

                    SqlCommand MMcmd = new SqlCommand("SP_ALL_TRAN_MMMST", ConnSherpa);
                    MMcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    MMcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                    MMcmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                    MMcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    MMcmd.Parameters.AddWithValue("@DOCDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@CHLNNO", CHALLANNO);
                    MMcmd.Parameters.AddWithValue("@CHLNDT", setDateFormat(CHALLANDT, true));
                    MMcmd.Parameters.AddWithValue("@POSTDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@TRANCODE", TRANCODE);
                    MMcmd.Parameters.AddWithValue("@REFDOCNO", strConvertZeroPadding(REFDOCNO));
                    MMcmd.Parameters.AddWithValue("@REFDOCYEAR", getFinYear(DOCDT));
                    MMcmd.Parameters.AddWithValue("@CREATEBY", Convert.ToInt32(USERID));
                    MMcmd.Parameters.AddWithValue("@REFNO", REFNO);
                    MMcmd.Parameters.AddWithValue("@REMARK", REMARK);


                    MMcmd.CommandType = CommandType.StoredProcedure;
                    MMcmd.Connection.Open();
                    MMcmd.ExecuteNonQuery();
                    MMcmd.Connection.Close();

                    #endregion


                    #region Insert MM Details

                    for (int i = 0; i < GRVLIST.Rows.Count; i++)
                    {
                        GridViewRow row = GRVLIST.Rows[i];
                        SqlCommand MDcmd = new SqlCommand("SP_ALL_INSERT_TRAN_MM", ConnSherpa);

                        MDcmd.Parameters.AddWithValue("@CMPID", CMPID);
                        MDcmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                        MDcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                        MDcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                        MDcmd.Parameters.AddWithValue("@SRNO", ((HiddenField)row.FindControl("hdSrNo")).Value);
                        MDcmd.Parameters.AddWithValue("@ITEMID", ((HiddenField)row.FindControl("hdITEMID")).Value);
                        MDcmd.Parameters.AddWithValue("@ITEMDESC", ((HiddenField)row.FindControl("hdItemDesc")).Value);
                        MDcmd.Parameters.AddWithValue("@ITEMGRPID", ((HiddenField)row.FindControl("hdITEMGRPID")).Value);
                        MDcmd.Parameters.AddWithValue("@UOM", ((HiddenField)row.FindControl("hdUOMID")).Value);
                        MDcmd.Parameters.AddWithValue("@QTY", ((HiddenField)row.FindControl("hdQty")).Value);
                        MDcmd.Parameters.AddWithValue("@CHLNQTY", ((HiddenField)row.FindControl("hdChalanQty")).Value);
                        MDcmd.Parameters.AddWithValue("@RATE", ((HiddenField)row.FindControl("hdRATE")).Value);
                        MDcmd.Parameters.AddWithValue("@CAMOUNT", ((HiddenField)row.FindControl("hdCAMOUNT")).Value);
                        MDcmd.Parameters.AddWithValue("@ITEMTEXT", ((HiddenField)row.FindControl("hdItemText")).Value);
                        MDcmd.Parameters.AddWithValue("@PONO", ((HiddenField)row.FindControl("hdPoNo")).Value);
                        MDcmd.Parameters.AddWithValue("@POSRNO", ((HiddenField)row.FindControl("hdPoSrNo")).Value);
                        MDcmd.Parameters.AddWithValue("@CSTCENTCD", ((HiddenField)row.FindControl("hdCostCenter")).Value);
                        MDcmd.Parameters.AddWithValue("@PRFCNT", ((HiddenField)row.FindControl("hdprfct")).Value);
                        MDcmd.Parameters.AddWithValue("@PLANTCD", ((HiddenField)row.FindControl("hdPlantCode")).Value);
                        MDcmd.Parameters.AddWithValue("@LOCCD", ((HiddenField)row.FindControl("hdLoccd")).Value);
                        MDcmd.Parameters.AddWithValue("@GLCD", ((HiddenField)row.FindControl("hdGlCd")).Value);
                        MDcmd.Parameters.AddWithValue("@ASSETCD", ((HiddenField)row.FindControl("hdAssetcd")).Value);
                        //MDcmd.Parameters.AddWithValue("@TOPLANTCD", ((Label)row.FindControl("lblGVToPlantID")).Text);
                        //MDcmd.Parameters.AddWithValue("@TOLOCCD", ((Label)row.FindControl("lblGVToLocationID")).Text);
                        //MDcmd.Parameters.AddWithValue("@ACPTQTY", 0);
                        //MDcmd.Parameters.AddWithValue("@RTNQTY", 0);


                        MDcmd.CommandType = CommandType.StoredProcedure;
                        MDcmd.Connection.Open();
                        MDcmd.ExecuteNonQuery();
                        MDcmd.Connection.Close();

                    }
                    #endregion


                    scope.Complete();
                    scope.Dispose();
                    return DOCNO;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        public string InsertMaterialAdjust(int CMPID, string DOCTYPE, string DOCKTYPE, string DOCNO, string DOCDT, string REFNO, string REMARK, string USERID, GridView GRVLIST)
        {
            DOCNO = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    DOCNO = MAXPRNO(DOCTYPE, DOCKTYPE);
                    DOCNO = strConvertZeroPadding(DOCNO);

                    #region Update MMNORANGE...
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(DOCNO));
                    PRcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                    PRcmd.Parameters.AddWithValue("@DOCTYPE", DOCKTYPE);
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    #endregion

                    #region Insert MMMST

                    SqlCommand MMcmd = new SqlCommand("SP_ALL_TRAN_MMMST", ConnSherpa);
                    MMcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    MMcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                    MMcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    MMcmd.Parameters.AddWithValue("@DOCDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                    MMcmd.Parameters.AddWithValue("@POSTDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@REFNO", REFNO);
                    MMcmd.Parameters.AddWithValue("@CREATEBY", Convert.ToInt32(USERID));
                    MMcmd.Parameters.AddWithValue("@REMARK", REMARK);

                    //MMcmd.Parameters.AddWithValue("@CHLNNO", CHALLANNO);
                    //MMcmd.Parameters.AddWithValue("@CHLNDT", setDateFormat(CHALLANDT, true));
                    //MMcmd.Parameters.AddWithValue("@POSTDATE", setDateFormat(DOCDT, true));
                    //MMcmd.Parameters.AddWithValue("@TRANCODE", TRANCODE);
                    //MMcmd.Parameters.AddWithValue("@REFDOCNO", strConvertZeroPadding(REFDOCNO));
                    //MMcmd.Parameters.AddWithValue("@REFDOCYEAR", getFinYear(DOCDT));





                    MMcmd.CommandType = CommandType.StoredProcedure;
                    MMcmd.Connection.Open();
                    MMcmd.ExecuteNonQuery();
                    MMcmd.Connection.Close();

                    #endregion


                    #region Insert MM Details

                    for (int i = 0; i < GRVLIST.Rows.Count; i++)
                    {
                        GridViewRow row = GRVLIST.Rows[i];
                        SqlCommand MDcmd = new SqlCommand("SP_ALL_INSERT_TRAN_MM", ConnSherpa);

                        MDcmd.Parameters.AddWithValue("@CMPID", CMPID);
                        MDcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                        MDcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                        MDcmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                        MDcmd.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblID")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblItemId")).Text);
                        MDcmd.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblPlantID")).Text);
                        MDcmd.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblLocationCDID")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGroupId")).Text);
                        MDcmd.Parameters.AddWithValue("@QTY", ((Label)row.FindControl("lblQty")).Text);
                        MDcmd.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblUOMID")).Text);
                        MDcmd.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGLCode")).Text);
                        MDcmd.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblCostCenter")).Text);
                        MDcmd.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblProfitCenter")).Text);
                        MDcmd.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblAssetCode")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblRemarks")).Text);
                        MDcmd.Parameters.AddWithValue("@CRDR", ((Label)row.FindControl("lblCRDR")).Text);



                        //MDcmd.Parameters.AddWithValue("@CHLNQTY", ((HiddenField)row.FindControl("hdChalanQty")).Value);
                        //MDcmd.Parameters.AddWithValue("@RATE", ((HiddenField)row.FindControl("hdRATE")).Value);
                        //MDcmd.Parameters.AddWithValue("@CAMOUNT", ((HiddenField)row.FindControl("hdCAMOUNT")).Value);
                        //MDcmd.Parameters.AddWithValue("@PONO", ((HiddenField)row.FindControl("hdPoNo")).Value);
                        //MDcmd.Parameters.AddWithValue("@POSRNO", ((HiddenField)row.FindControl("hdPoSrNo")).Value);
                        //MDcmd.Parameters.AddWithValue("@TOPLANTCD", ((Label)row.FindControl("lblGVToPlantID")).Text);
                        //MDcmd.Parameters.AddWithValue("@TOLOCCD", ((Label)row.FindControl("lblGVToLocationID")).Text);
                        //MDcmd.Parameters.AddWithValue("@ACPTQTY", 0);
                        //MDcmd.Parameters.AddWithValue("@RTNQTY", 0);



                        MDcmd.CommandType = CommandType.StoredProcedure;
                        MDcmd.Connection.Open();
                        MDcmd.ExecuteNonQuery();
                        MDcmd.Connection.Close();

                    }
                    #endregion


                    scope.Complete();
                    scope.Dispose();
                    return DOCNO;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        public string InsertIS(int CMPID, string DOCTYPE, string DOCNO, string DOCDT, string REFNO, string REMARK, GridView GRVLIST, string USERID)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DOCNO = MAXPRNO(DOCTYPE, "IS");
                    DOCNO = strConvertZeroPadding(DOCNO);


                    #region Update MMNORANGE...
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(DOCNO));
                    PRcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                    PRcmd.Parameters.AddWithValue("@DOCTYPE", "IS");
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    #endregion


                    #region Insert MMMST

                    SqlCommand MMcmd = new SqlCommand("SP_ALL_TRAN_MMMST", ConnSherpa);
                    MMcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    MMcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                    MMcmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                    MMcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    MMcmd.Parameters.AddWithValue("@DOCDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@POSTDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@REFNO", REFNO);
                    MMcmd.Parameters.AddWithValue("@CREATEBY", Convert.ToInt32(USERID));
                    MMcmd.Parameters.AddWithValue("@REMARK", REMARK);

                    MMcmd.CommandType = CommandType.StoredProcedure;
                    MMcmd.Connection.Open();
                    MMcmd.ExecuteNonQuery();
                    MMcmd.Connection.Close();

                    #endregion


                    #region Insert MM Details

                    for (int i = 0; i < GRVLIST.Rows.Count; i++)
                    {
                        GridViewRow row = GRVLIST.Rows[i];
                        SqlCommand MDcmd = new SqlCommand("SP_ALL_INSERT_TRAN_MM", ConnSherpa);

                        int itemid = Convert.ToInt32(((Label)row.FindControl("lblItemId")).Text);
                        decimal rate = Convert.ToDecimal(((Label)row.FindControl("lblRate")).Text);
                        int qty = Convert.ToInt32(((Label)row.FindControl("lblQty")).Text);

                        if (itemid != 0 && rate > 0 && qty > 0)
                        {
                            MDcmd.Parameters.AddWithValue("@CMPID", CMPID);
                            MDcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                            MDcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                            MDcmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                            MDcmd.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblID")).Text);
                            MDcmd.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblItemId")).Text);
                            MDcmd.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblPlantID")).Text);
                            MDcmd.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblLocationCDID")).Text);
                            MDcmd.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGroupId")).Text);
                            MDcmd.Parameters.AddWithValue("@QTY", ((Label)row.FindControl("lblQty")).Text);
                            MDcmd.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblUOMID")).Text);
                            MDcmd.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGLCode")).Text);
                            MDcmd.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblAssetCode")).Text);
                            MDcmd.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblCostCenter")).Text);
                            MDcmd.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblProfitCenter")).Text);
                            MDcmd.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblRemarks")).Text);

                            MDcmd.Parameters.AddWithValue("@TOITEMID", ((Label)row.FindControl("lblToItemId")).Text);
                            MDcmd.Parameters.AddWithValue("@TOPLANTCD", ((Label)row.FindControl("lblPlantID")).Text);
                            MDcmd.Parameters.AddWithValue("@TOLOCCD", ((Label)row.FindControl("lblLocationCDID")).Text);
                            MDcmd.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblRate")).Text);
                            MDcmd.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblAmount")).Text);


                            MDcmd.Parameters.AddWithValue("@CRDR", ((Label)row.FindControl("lblCRDR")).Text);



                            //MDcmd.Parameters.AddWithValue("@CHLNQTY", ((HiddenField)row.FindControl("hdChalanQty")).Value);
                            //MDcmd.Parameters.AddWithValue("@PONO", ((HiddenField)row.FindControl("hdPoNo")).Value);
                            //MDcmd.Parameters.AddWithValue("@POSRNO", ((HiddenField)row.FindControl("hdPoSrNo")).Value);
                            //MDcmd.Parameters.AddWithValue("@ACPTQTY", 0);
                            //MDcmd.Parameters.AddWithValue("@RTNQTY", 0);



                            MDcmd.CommandType = CommandType.StoredProcedure;
                            MDcmd.Connection.Open();
                            MDcmd.ExecuteNonQuery();
                            MDcmd.Connection.Close();
                        }

                    }
                    #endregion


                    scope.Complete();
                    scope.Dispose();
                    return DOCNO;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }


        public string InsertDepartmentMaterialConsume(string DOCTYPE, string DOCNO, string DOCDT, string REFNO, string REMARKS, GridView GRVLIST, string USERID, int CMPID, int UPDATEACTION)
        {
            MainClass objMainClass = new MainClass();
            string DOCKNO = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DOCKNO = MAXPRNO(DOCTYPE, "CM");
                    DOCKNO = strConvertZeroPadding(DOCKNO);
                    #region Update MMNORANGE...
                    SqlCommand PRcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    PRcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(DOCKNO));
                    PRcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                    PRcmd.Parameters.AddWithValue("@DOCTYPE", "CM");
                    PRcmd.CommandType = CommandType.StoredProcedure;
                    PRcmd.Connection.Open();
                    PRcmd.ExecuteNonQuery();
                    PRcmd.Connection.Close();
                    #endregion

                    #region Insert MMMST

                    SqlCommand MMcmd = new SqlCommand("SP_PO_MATERIAL_INWARDTRAN_MMMSTCRUDOPERATION", ConnSherpa);
                    MMcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    MMcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                    MMcmd.Parameters.AddWithValue("@REFDOCYEAR", getFinYear(DOCDT));
                    MMcmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCKNO));
                    MMcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    MMcmd.Parameters.AddWithValue("@DOCDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@POSTDATE", setDateFormat(DOCDT, true));
                    MMcmd.Parameters.AddWithValue("@REFNO", REFNO);
                    MMcmd.Parameters.AddWithValue("@REMARK", REMARKS);
                    MMcmd.Parameters.AddWithValue("@CREATEBY", USERID);
                    MMcmd.Parameters.AddWithValue("@MODE", "I");
                    MMcmd.CommandType = CommandType.StoredProcedure;
                    MMcmd.Connection.Open();
                    MMcmd.ExecuteNonQuery();
                    MMcmd.Connection.Close();

                    #endregion

                    #region Insert MM Details

                    for (int i = 0; i < GRVLIST.Rows.Count; i++)
                    {
                        GridViewRow row = GRVLIST.Rows[i];
                        SqlCommand MDcmd = new SqlCommand("SP_INSERT_TRAN_MM", ConnSherpa);
                        MDcmd.Parameters.AddWithValue("@CMPID", CMPID);
                        MDcmd.Parameters.AddWithValue("@FINYEAR", getFinYear(DOCDT));
                        MDcmd.Parameters.AddWithValue("@DOCNO", DOCKNO);
                        MDcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                        MDcmd.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblID")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblItemId")).Text);
                        MDcmd.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblPlantID")).Text);
                        MDcmd.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblLocationCDID")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGroupId")).Text);
                        MDcmd.Parameters.AddWithValue("@itemdesc", ((Label)row.FindControl("lblItemDesc")).Text);
                        MDcmd.Parameters.AddWithValue("@QTY", ((Label)row.FindControl("lblQty")).Text);
                        MDcmd.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblUOMID")).Text);
                        MDcmd.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGLCode")).Text);
                        MDcmd.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblCostCenter")).Text);
                        MDcmd.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblProfitCenter")).Text);
                        MDcmd.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblAssetCode")).Text);
                        MDcmd.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblRemarks")).Text == "" || ((Label)row.FindControl("lblRemarks")).Text == string.Empty ? "CONSUMPTION" : ((Label)row.FindControl("lblRemarks")).Text);
                        MDcmd.Parameters.AddWithValue("@TOITEMID", 0);
                        MDcmd.Parameters.AddWithValue("@TRACKNO", ((Label)row.FindControl("lblTrackNo")).Text == "" ? "0000000000" : strConvertZeroPadding(((Label)row.FindControl("lblTrackNo")).Text));
                        MDcmd.Parameters.AddWithValue("@MODE", "I");
                        MDcmd.CommandType = CommandType.StoredProcedure;
                        MDcmd.Connection.Open();
                        MDcmd.ExecuteNonQuery();
                        MDcmd.Connection.Close();

                    }
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                    return DOCKNO;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        public DataTable GetVendorDetailFromPoNo(int CMPID, string PONO)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_POFROMMATERIAL_VENDORDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetMaterialIssueDetails(string DOCNO, int CMPID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MMMST", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@ACTION", "SELECTONE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetEachMaterialInwardDetail(int CMPID, string DOCNO, string DOCTYPE)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            //SP_SELECT_PODATA
            SqlCommand cmd = new SqlCommand("SP_SELECT_MATERIALINWARDFROMPODATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@Action", "SELECTONE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetIssueRegisterData(int CMPID, string DOCTYPE, string DOCNO, string FROMDATE, string TODATE, string PLANTCD)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MMMST", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable CheckDocNoExistForMaterialInspection(string DOCNO, string DOCTYPE, int CMPID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MATERIALINSPECTIONCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetMaterialDetailForInspection(string DOCNO, string DOCTYPE, int CMPID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MATERIALINSPECTIONCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetPORefData(string REFNO, string VENDCODE)
        {
            //SP_SELECT_PO_REFNO_DATA
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PO_REFNO_DATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@REFNO", REFNO);
                cmd.Parameters.AddWithValue("@VENDCODE", VENDCODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw;
            }
            return dt;
        }


        public DataTable GetSTOINData(int CMPID, string DOCNO)
        {
            DOCNO = strConvertZeroPadding(DOCNO);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_STO_INWARD_DATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCNO", strConvertZeroPadding(DOCNO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetImageUrl(string JOBID)
        {
            JOBID = strConvertZeroPadding(JOBID);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("GET_MOBEX_IMAGEURL", ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", JOBID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }


        public int InsertImageUrl(string JOBID, string IMAGEURL)
        {
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand("INSERT_MOBEX_IMAGEURL", ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", JOBID);
                cmd.Parameters.AddWithValue("@IMAGEURL", IMAGEURL);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iReturn = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        //SELECT_PO_BY_POTYPE
        public DataTable GetPoByPOType(int CMPID, string PONO, string POTYPE)
        {
            PONO = strConvertZeroPadding(PONO);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_PO_BY_POTYPE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PONO", PONO);
                cmd.Parameters.AddWithValue("@POTYPE", POTYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }


            return dt;
        }

        public DataTable GetProblemCharges(int MAKE, int MODELID)
        {
            //SP_SELECT_PROBLEMS_BY_MODELID
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PROBLEM_CHARGES", ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODELID", MODELID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdatePartPrice(int ID, string AMOUNT, int STATUS)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_UPDATE_PART_PRICE", ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@CHARGE", AMOUNT);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;
        }

        public int UpdatePaymentEntry(int CMPID, string TXNNO, string TXNDT, string TXNBY, string VCHRNO, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_CROMADATA", ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@TXNNO", TXNNO);
                cmd.Parameters.AddWithValue("@TXNDT", setDateFormat(TXNDT, true));
                cmd.Parameters.AddWithValue("@TXNBY", TXNBY);
                cmd.Parameters.AddWithValue("@VCHRNO", VCHRNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public int UpdateItemPrice(int CMPID, int ITEMID, string MRP, int UPDATEBY, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_ITEMMAPPING_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMID", ITEMID);
                cmd.Parameters.AddWithValue("@MRP", MRP);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;
        }


        // TATA SKY SYSTEM METHOD START

        // CATEGORY METHOD    
        //public bool SaveCategoryMaster(Int64 CATID, string CATNAME, string CATVALUE, string CATCODE, Int32 CATISACTIVE, string MODE, string UserId)
        //{
        //    MainClass objMainClass = new MainClass();
        //    bool IsAddUpdate = false;
        //    int result = 0;
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            #region ADD UPDATE CATEGORY MASTER...
        //            SqlCommand cmdM = new SqlCommand("TATASKYCATEGORYMASTERCRUDOPERATION", ConnSherpa);
        //            cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
        //            cmdM.Parameters.AddWithValue("@CATID", CATID);
        //            cmdM.Parameters.AddWithValue("@CATNAME", CATNAME);
        //            cmdM.Parameters.AddWithValue("@CATVALUE", CATVALUE);
        //            cmdM.Parameters.AddWithValue("@CATCODE", CATCODE);
        //            cmdM.Parameters.AddWithValue("@CATISACTIVE", CATISACTIVE);
        //            cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
        //            cmdM.Parameters.AddWithValue("@Action", MODE);
        //            cmdM.CommandType = CommandType.StoredProcedure;
        //            cmdM.Connection.Open();
        //            result = cmdM.ExecuteNonQuery();
        //            cmdM.Connection.Close();
        //            if (result > 0)
        //            {
        //                IsAddUpdate = true;
        //            }
        //            #endregion
        //            scope.Complete();
        //            scope.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IsAddUpdate = false;
        //        Console.WriteLine(ex);
        //    }
        //    return IsAddUpdate;
        //}

        //public DataTable GetCategoryMaster(Int64 CMPID, string CATNAME, string CATVALUE, string CATCODE, string MODE, Int64 CATID = 0)
        //{
        //    MainClass objMainClass = new MainClass();
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = new SqlCommand("TATASKYCATEGORYMASTERCRUDOPERATION", ConnSherpa);
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@CMPID", CMPID);
        //        cmd.Parameters.AddWithValue("@CATNAME", CATNAME);
        //        cmd.Parameters.AddWithValue("@CATVALUE", CATVALUE);
        //        cmd.Parameters.AddWithValue("@CATCODE", CATCODE);
        //        cmd.Parameters.AddWithValue("@CATID", CATID);
        //        cmd.Parameters.AddWithValue("@Action", MODE);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        cmd.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        cmd.Connection.Close();
        //        throw ex;
        //    }
        //    return dt;
        //}
        //// CATEGORY METHOD    

        ////USER DETAIL 
        //public bool SaveUserDetail(Int64 USERID, string USERFIRSTNAME, string USERLASTNAME, string USERCODE, string USERPLANT, Int64 USERDEPARTMENT,
        //                            string USERROLE, Int32 USERSTATUS, string MODE, string UserId)
        //{
        //    MainClass objMainClass = new MainClass();
        //    bool IsAddUpdate = false;
        //    int result = 0;
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            #region ADD UPDATE USER MASTER...
        //            SqlCommand cmdM = new SqlCommand("TATASKYUSERDETAILCRUDOPERATION", ConnSherpa);
        //            cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
        //            cmdM.Parameters.AddWithValue("@USERID", USERID);
        //            cmdM.Parameters.AddWithValue("@USERFIRSTNAME", USERFIRSTNAME);
        //            cmdM.Parameters.AddWithValue("@USERLASTNAME", USERLASTNAME);
        //            cmdM.Parameters.AddWithValue("@USERCODE", USERCODE);
        //            cmdM.Parameters.AddWithValue("@USERPLANT", USERPLANT);
        //            cmdM.Parameters.AddWithValue("@USERDEPARTMENT", USERDEPARTMENT);
        //            cmdM.Parameters.AddWithValue("@USERROLE", USERROLE);
        //            cmdM.Parameters.AddWithValue("@USERSTATUS", USERSTATUS);
        //            cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
        //            cmdM.Parameters.AddWithValue("@Action", MODE);

        //            cmdM.CommandType = CommandType.StoredProcedure;
        //            cmdM.Connection.Open();
        //            result = cmdM.ExecuteNonQuery();
        //            cmdM.Connection.Close();
        //            if (result > 0)
        //            {
        //                IsAddUpdate = true;
        //            }
        //            #endregion
        //            scope.Complete();
        //            scope.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        IsAddUpdate = false;
        //        Console.WriteLine(ex);
        //    }
        //    return IsAddUpdate;
        //}

        //public DataTable GetUserDetail(Int64 CMPID, string USERFIRSTNAME, string USERLASTNAME, Int64 USERID, string MODE)
        //{
        //    MainClass objMainClass = new MainClass();
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = new SqlCommand("TATASKYUSERDETAILCRUDOPERATION", ConnSherpa);
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@CMPID", CMPID);
        //        cmd.Parameters.AddWithValue("@USERFIRSTNAME", USERFIRSTNAME);
        //        cmd.Parameters.AddWithValue("@USERLASTNAME", USERLASTNAME);
        //        cmd.Parameters.AddWithValue("@USERID", USERID);
        //        cmd.Parameters.AddWithValue("@Action", MODE);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        cmd.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        cmd.Connection.Close();
        //        throw ex;
        //    }
        //    return dt;
        //}

        //public DataTable SearchUserDetail(string USERFIRSTNAME, string USERLASTNAME, string USERCODE, string USERPLANT, Int64 USERDEPARTMENT,
        //                                  string USERROLE, Int32 USERSTATUS, string MODE)
        //{
        //    MainClass objMainClass = new MainClass();
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = new SqlCommand("TATASKYUSERDETAILCRUDOPERATION", ConnSherpa);
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
        //        cmd.Parameters.AddWithValue("@USERFIRSTNAME", USERFIRSTNAME);
        //        cmd.Parameters.AddWithValue("@USERLASTNAME", USERLASTNAME);
        //        cmd.Parameters.AddWithValue("@USERCODE", USERCODE);
        //        cmd.Parameters.AddWithValue("@USERPLANT", USERPLANT);
        //        cmd.Parameters.AddWithValue("@USERDEPARTMENT", USERDEPARTMENT);
        //        cmd.Parameters.AddWithValue("@USERROLE", USERROLE);
        //        cmd.Parameters.AddWithValue("@USERSTATUS", USERSTATUS);
        //        cmd.Parameters.AddWithValue("@Action", MODE);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        cmd.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        cmd.Connection.Close();
        //        throw ex;
        //    }
        //    return dt;
        //}
        ////USER DETAIL 

        //// CALL ASSIGNMENT 
        //public string MAXASSIGNMENTNO()
        //{
        //    MainClass objMainClass = new MainClass();
        //    string MAXASSIGNMENTNO = string.Empty;
        //    SqlCommand cmd = new SqlCommand("SP_GET_MAX_ASSIGNMENTNO", ConnSherpa);
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        object obj = cmd.ExecuteScalar();
        //        if ((obj) != null)
        //        {
        //            MAXASSIGNMENTNO = obj.ToString();
        //            MAXASSIGNMENTNO = Convert.ToString(Convert.ToInt32(MAXASSIGNMENTNO) + 1);
        //        }
        //        cmd.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        cmd.Connection.Close();
        //        throw ex;
        //    }
        //    return MAXASSIGNMENTNO;
        //}

        //public string SAVETATASKYJOBASSIGNMENT(string ASSIGNMENTNO, string NDSNO, string ESNNO, string JOBNO, string ASSIGNDATE, string ASSIGNTIME, Int64 ENGINEERKEY, Int64 MODELSKEY,
        //                                        Int64 CONDITIONKEY, Int64 PROBLEMKEY, Int64 LEVELKEY, string RECEIVEDATE, string PRESCANNINGDATE, Int64 PRESCANNINGPROBLEMKEY, string ISPFAULTCODE, string ISPFAULTVALUE, string CIDREASON, string MODE, string UserId)
        //{
        //    MainClass objMainClass = new MainClass();
        //    try
        //    {
        //        if (MODE == "ADD")
        //        {
        //            ASSIGNMENTNO = MAXASSIGNMENTNO();
        //        }
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            #region Update TATASKYMMNORANGE...
        //            if (MODE == "ADD")
        //            {
        //                SqlCommand PRcmd = new SqlCommand("SP_UPDATE_TATASKYMMNORANGE", ConnSherpa);
        //                PRcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
        //                PRcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(ASSIGNMENTNO));
        //                PRcmd.CommandType = CommandType.StoredProcedure;
        //                PRcmd.Connection.Open();
        //                PRcmd.ExecuteNonQuery();
        //                PRcmd.Connection.Close();
        //            }

        //            #endregion

        //            #region Insert TATASKYJOBASSIGNMENT...
        //            SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
        //            cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
        //            cmdM.Parameters.AddWithValue("@ASSIGNMENTNO", strConvertZeroPadding(ASSIGNMENTNO));
        //            cmdM.Parameters.AddWithValue("@NDSNO", NDSNO);
        //            cmdM.Parameters.AddWithValue("@ESNNO", ESNNO);
        //            cmdM.Parameters.AddWithValue("@JOBNO", strConvertZeroPadding(JOBNO));
        //            cmdM.Parameters.AddWithValue("@ASSIGNDATE", setDateFormat(ASSIGNDATE, true));
        //            cmdM.Parameters.AddWithValue("@ASSIGNTIME", ASSIGNTIME);
        //            cmdM.Parameters.AddWithValue("@ENGINEERKEY", ENGINEERKEY);
        //            cmdM.Parameters.AddWithValue("@MODELSKEY", MODELSKEY);
        //            cmdM.Parameters.AddWithValue("@CONDITIONKEY", CONDITIONKEY);
        //            cmdM.Parameters.AddWithValue("@LEVELKEY", LEVELKEY);
        //            cmdM.Parameters.AddWithValue("@PROBLEMKEY", PROBLEMKEY);
        //            cmdM.Parameters.AddWithValue("@RECEIVEDATE", setDateFormat(RECEIVEDATE, true));
        //            cmdM.Parameters.AddWithValue("@PRESCANNINGDATE", setDateFormat(PRESCANNINGDATE, true));
        //            cmdM.Parameters.AddWithValue("@PRESCANNINGPROBLEMKEY", PRESCANNINGPROBLEMKEY);
        //            cmdM.Parameters.AddWithValue("@ISPFAULTCODE", ISPFAULTCODE);
        //            cmdM.Parameters.AddWithValue("@ISPFAULTVALUE", ISPFAULTVALUE);
        //            cmdM.Parameters.AddWithValue("@CIDREASON", CIDREASON);

        //            cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
        //            cmdM.Parameters.AddWithValue("@ACTION", MODE);
        //            cmdM.CommandType = CommandType.StoredProcedure;
        //            cmdM.Connection.Open();
        //            cmdM.ExecuteNonQuery();
        //            cmdM.Connection.Close();
        //            #endregion
        //            scope.Complete();
        //            scope.Dispose();
        //            return ASSIGNMENTNO;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //        return "";
        //    }
        //}

        //public DataTable GETTATASKYJOBASSIGNMENT(string ASSIGNMENTNO, string NDSNO, string JOBNO, string ESNNO, Int64 ENGINEERKEY, Int64 MODELSKEY,
        //                                        Int64 CONDITIONKEY, Int64 PROBLEMKEY, Int64 LEVELKEY, string MODE, Int64 UserId)
        //{
        //    MainClass objMainClass = new MainClass();
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
        //        cmd.Parameters.AddWithValue("@ASSIGNMENTNO", (ASSIGNMENTNO.Length > 0 ? strConvertZeroPadding(ASSIGNMENTNO) : ""));
        //        cmd.Parameters.AddWithValue("@NDSNO", NDSNO);
        //        cmd.Parameters.AddWithValue("@JOBNO", (JOBNO.Length > 0 ? strConvertZeroPadding(JOBNO) : ""));
        //        cmd.Parameters.AddWithValue("@ENGINEERKEY", ENGINEERKEY);
        //        cmd.Parameters.AddWithValue("@MODELSKEY", MODELSKEY);
        //        cmd.Parameters.AddWithValue("@CONDITIONKEY", CONDITIONKEY);
        //        cmd.Parameters.AddWithValue("@LEVELKEY", LEVELKEY);
        //        cmd.Parameters.AddWithValue("@PROBLEMKEY", PROBLEMKEY);
        //        cmd.Parameters.AddWithValue("@ESNNO", ESNNO);
        //        cmd.Parameters.AddWithValue("@CREATEBY", UserId);
        //        cmd.Parameters.AddWithValue("@ACTION", MODE);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        cmd.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        cmd.Connection.Close();
        //        throw ex;
        //    }
        //    return dt;
        //}

        //// CALL ASSIGNMENT 

        //// QC SCREEN 
        //public DataTable GETCALLASSIGMENTFORQCUPDATE(string FROMDATE, string TODATE, string ASSIGNMENTNO, string NDSNO, string ESNNO,
        //                                             Int64 ENGINEERKEY, Int64 MODELSKEY, Int64 CONDITIONKEY, Int64 LEVELKEY, string MODE = "QCSEARCH")
        //{
        //    MainClass objMainClass = new MainClass();
        //    DataTable dt = new DataTable();

        //    //SP_SELECT_PODATA
        //    SqlCommand cmd = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
        //        cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(FROMDATE, true));
        //        cmd.Parameters.AddWithValue("@Todate", setDateFormat(TODATE, true));
        //        cmd.Parameters.AddWithValue("@ASSIGNMENTNO", (ASSIGNMENTNO.Length > 0 ? strConvertZeroPadding(ASSIGNMENTNO) : ""));
        //        cmd.Parameters.AddWithValue("@NDSNO", NDSNO);
        //        cmd.Parameters.AddWithValue("@ESNNO", ESNNO);
        //        cmd.Parameters.AddWithValue("@ENGINEERKEY", ENGINEERKEY);
        //        cmd.Parameters.AddWithValue("@MODELSKEY", MODELSKEY);
        //        cmd.Parameters.AddWithValue("@CONDITIONKEY", CONDITIONKEY);
        //        cmd.Parameters.AddWithValue("@LEVELKEY", LEVELKEY);
        //        cmd.Parameters.AddWithValue("@ACTION", MODE);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        cmd.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        cmd.Connection.Close();
        //        throw ex;
        //    }
        //    return dt;
        //}


        //public int SAVEQCBULKUPDATE(string QCBULKUPDATEJSON, string UserId, string MODE = "QCBULKUPDATE")
        //{
        //    MainClass objMainClass = new MainClass();
        //    int result = 0;
        //    try
        //    {
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            #region BULKUPDATE QC STATUS...
        //            SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
        //            cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
        //            cmdM.Parameters.AddWithValue("@QCBULKUPDATEJSON", QCBULKUPDATEJSON);
        //            cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
        //            cmdM.Parameters.AddWithValue("@ACTION", MODE);
        //            cmdM.CommandType = CommandType.StoredProcedure;
        //            cmdM.Connection.Open();
        //            result = cmdM.ExecuteNonQuery();
        //            cmdM.Connection.Close();
        //            #endregion
        //            scope.Complete();
        //            scope.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }
        //    return result;
        //}

        //// QC SCREEN 

        //// DAILY REPORT
        //public DataTable GETDAILYREPORT(string FROMDATE, string TODATE, string ASSIGNMENTNO, string NDSNO, string ESNNO,
        //                                            Int64 ENGINEERKEY, Int64 MODELSKEY, Int64 CONDITIONKEY, Int64 LEVELKEY, string FILTERREQ, string MODE = "DAILYPRODUCTION")
        //{
        //    MainClass objMainClass = new MainClass();
        //    DataTable dt = new DataTable();

        //    //SP_SELECT_PODATA
        //    SqlCommand cmd = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
        //        cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(FROMDATE, true));
        //        cmd.Parameters.AddWithValue("@Todate", setDateFormat(TODATE, true));
        //        cmd.Parameters.AddWithValue("@ASSIGNMENTNO", (ASSIGNMENTNO.Length > 0 ? strConvertZeroPadding(ASSIGNMENTNO) : ""));
        //        cmd.Parameters.AddWithValue("@NDSNO", NDSNO);
        //        cmd.Parameters.AddWithValue("@ESNNO", ESNNO);
        //        cmd.Parameters.AddWithValue("@ENGINEERKEY", ENGINEERKEY);
        //        cmd.Parameters.AddWithValue("@MODELSKEY", MODELSKEY);
        //        cmd.Parameters.AddWithValue("@CONDITIONKEY", CONDITIONKEY);
        //        cmd.Parameters.AddWithValue("@LEVELKEY", LEVELKEY);
        //        cmd.Parameters.AddWithValue("@FILTERREQ", FILTERREQ);
        //        cmd.Parameters.AddWithValue("@ACTION", MODE);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection.Open();
        //        cmd.ExecuteNonQuery();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        cmd.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        cmd.Connection.Close();
        //        throw ex;
        //    }
        //    return dt;
        //}

        // CATEGORY METHOD    
        public bool SaveCategoryMaster(Int64 CATID, string CATNAME, string CATVALUE, string CATCODE, Int32 CATISACTIVE, string MODE, string UserId, string CATCHILDVALUE
            , Int64 CATCHILDKEY, string CATMATERIALCODE)
        {
            MainClass objMainClass = new MainClass();
            bool IsAddUpdate = false;
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region ADD UPDATE CATEGORY MASTER...
                    SqlCommand cmdM = new SqlCommand("TATASKYCATEGORYMASTERCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@CATID", CATID);
                    cmdM.Parameters.AddWithValue("@CATNAME", CATNAME);
                    cmdM.Parameters.AddWithValue("@CATVALUE", CATVALUE);
                    cmdM.Parameters.AddWithValue("@CATCODE", CATCODE);
                    cmdM.Parameters.AddWithValue("@CATISACTIVE", CATISACTIVE);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@CATCHILDVALUE", CATCHILDVALUE);
                    cmdM.Parameters.AddWithValue("@CATCHILDKEY", CATCHILDKEY);
                    cmdM.Parameters.AddWithValue("@CATMATERIALCODE", CATMATERIALCODE);
                    cmdM.Parameters.AddWithValue("@Action", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    if (result > 0)
                    {
                        IsAddUpdate = true;
                    }
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                IsAddUpdate = false;
                Console.WriteLine(ex);
            }
            return IsAddUpdate;
        }

        public DataTable GetCategoryMaster(Int64 CMPID, string CATNAME, string CATVALUE, string CATCODE, string MODE, Int64 CATID = 0)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("TATASKYCATEGORYMASTERCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CATNAME", CATNAME);
                cmd.Parameters.AddWithValue("@CATVALUE", CATVALUE);
                cmd.Parameters.AddWithValue("@CATCODE", CATCODE);
                cmd.Parameters.AddWithValue("@CATID", CATID);
                cmd.Parameters.AddWithValue("@Action", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        // CATEGORY METHOD    

        //USER DETAIL 
        public bool SaveUserDetail(Int64 USERID, string USERFIRSTNAME, string USERLASTNAME, string USERCODE, string USERPLANT, Int64 USERDEPARTMENT,
                                    string USERROLE, Int32 USERSTATUS, string MODE, string UserId)
        {
            MainClass objMainClass = new MainClass();
            bool IsAddUpdate = false;
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region ADD UPDATE USER MASTER...
                    SqlCommand cmdM = new SqlCommand("TATASKYUSERDETAILCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@USERID", USERID);
                    cmdM.Parameters.AddWithValue("@USERFIRSTNAME", USERFIRSTNAME);
                    cmdM.Parameters.AddWithValue("@USERLASTNAME", USERLASTNAME);
                    cmdM.Parameters.AddWithValue("@USERCODE", USERCODE);
                    cmdM.Parameters.AddWithValue("@USERPLANT", USERPLANT);
                    cmdM.Parameters.AddWithValue("@USERDEPARTMENT", USERDEPARTMENT);
                    cmdM.Parameters.AddWithValue("@USERROLE", USERROLE);
                    cmdM.Parameters.AddWithValue("@USERSTATUS", USERSTATUS);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@Action", MODE);

                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    if (result > 0)
                    {
                        IsAddUpdate = true;
                    }
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                IsAddUpdate = false;
                Console.WriteLine(ex);
            }
            return IsAddUpdate;
        }

        public DataTable GetUserDetail(Int64 CMPID, string USERFIRSTNAME, string USERLASTNAME, Int64 USERID, string MODE)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("TATASKYUSERDETAILCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@USERFIRSTNAME", USERFIRSTNAME);
                cmd.Parameters.AddWithValue("@USERLASTNAME", USERLASTNAME);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@Action", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable SearchUserDetail(string USERFIRSTNAME, string USERLASTNAME, string USERCODE, string USERPLANT, Int64 USERDEPARTMENT,
                                          string USERROLE, Int32 USERSTATUS, string MODE)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("TATASKYUSERDETAILCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@USERFIRSTNAME", USERFIRSTNAME);
                cmd.Parameters.AddWithValue("@USERLASTNAME", USERLASTNAME);
                cmd.Parameters.AddWithValue("@USERCODE", USERCODE);
                cmd.Parameters.AddWithValue("@USERPLANT", USERPLANT);
                cmd.Parameters.AddWithValue("@USERDEPARTMENT", USERDEPARTMENT);
                cmd.Parameters.AddWithValue("@USERROLE", USERROLE);
                cmd.Parameters.AddWithValue("@USERSTATUS", USERSTATUS);
                cmd.Parameters.AddWithValue("@Action", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        //USER DETAIL 

        // CALL ASSIGNMENT 
        public string MAXASSIGNMENTNO(string plantcode)
        {
            MainClass objMainClass = new MainClass();
            string MAXASSIGNMENTNO = string.Empty;
            string MAXASSIGNMENTNOPREFIX = string.Empty;
            SqlCommand cmd = new SqlCommand("SP_GET_MAX_ASSIGNMENTNO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                object obj = cmd.ExecuteScalar();
                if ((obj) != null)
                {
                    MAXASSIGNMENTNO = obj.ToString();
                    MAXASSIGNMENTNO = Convert.ToString(Convert.ToInt32(MAXASSIGNMENTNO) + 1);
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            //
            MAXASSIGNMENTNOPREFIX = objMainClass.MAXASSIGNMENTNOPREFIX(plantcode);
            MAXASSIGNMENTNO = objMainClass.strConvertZeroFiveDigitPadding(MAXASSIGNMENTNO);
            MAXASSIGNMENTNO = MAXASSIGNMENTNOPREFIX + MAXASSIGNMENTNO;
            return MAXASSIGNMENTNO;
        }

        public int EXCELMAXASSIGNMENTNO()
        {
            MainClass objMainClass = new MainClass();
            string MAXASSIGNMENTNO = string.Empty;
            int INTMAXASSIGNMENTNO;
            SqlCommand cmd = new SqlCommand("SP_GET_MAX_ASSIGNMENTNO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                object obj = cmd.ExecuteScalar();
                if ((obj) != null)
                {
                    MAXASSIGNMENTNO = obj.ToString();
                    MAXASSIGNMENTNO = Convert.ToString(Convert.ToInt32(MAXASSIGNMENTNO) + 1);
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            //
            INTMAXASSIGNMENTNO = Convert.ToInt32(MAXASSIGNMENTNO);
            return INTMAXASSIGNMENTNO;
        }

        public string strConvertZeroFiveDigitPadding(string strText, string padChar = "0", int intTimes = 5)
        {
            strText = strReplicate(padChar, intTimes - strText.Length) + strText;
            return strText;
        }

        public string MAXASSIGNMENTNOPREFIX(string plantcode)
        {
            string assigmentprefix = "";
            try
            {
                string curmnth = "";
                string curyr = "";
                string curdate = "";
                string plprefix = "";
                curdate = DateTime.Now.ToString("dd");
                curmnth = DateTime.Now.ToString("MM");
                curyr = DateTime.Now.ToString("yy");
                if (plantcode == "1001")
                {
                    plprefix = "A";
                }
                else if (plantcode == "1002")
                {
                    plprefix = "B";
                }
                else if (plantcode == "1004")
                {
                    plprefix = "P";
                }
                else if (plantcode == "1005")
                {
                    plprefix = "K";
                }
                else
                {
                    plprefix = "A";
                }
                assigmentprefix = plprefix + curdate + curmnth + curyr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
            return assigmentprefix;
        }

        public string SAVETATASKYJOBASSIGNMENT(string ASSIGNMENTNO, string NDSNO, string ESNNO, string JOBNO, string ASSIGNDATE, string ASSIGNTIME, Int64 ENGINEERKEY, Int64 MODELSKEY,
                                                Int64 CONDITIONKEY, Int64 PROBLEMKEY, Int64 LEVELKEY, string RECEIVEDATE,
                                                string PRESCANNINGDATE, Int64 PRESCANNINGPROBLEMKEY, string ISPFAULTCODE,
                                                string ISPFAULTVALUE, string CIDREASON, string MODE, int TAGKEY, int FAULTREPORTEDKEY,
                                                string UserId, string plantcode)

        {
            string updateassigmentno = "";
            MainClass objMainClass = new MainClass();
            try
            {
                if (MODE == "ADD")
                {
                    ASSIGNMENTNO = MAXASSIGNMENTNO(plantcode);
                    updateassigmentno = ASSIGNMENTNO.Substring(5, 5);
                }
                using (TransactionScope scope = new TransactionScope())
                {
                    #region Update TATASKYMMNORANGE...
                    if (MODE == "ADD")
                    {
                        SqlCommand PRcmd = new SqlCommand("SP_UPDATE_TATASKYMMNORANGE", ConnSherpa);
                        PRcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                        PRcmd.Parameters.AddWithValue("@CURRNO", Convert.ToInt32(updateassigmentno));
                        PRcmd.CommandType = CommandType.StoredProcedure;
                        PRcmd.Connection.Open();
                        PRcmd.ExecuteNonQuery();
                        PRcmd.Connection.Close();
                    }

                    #endregion

                    #region Insert TATASKYJOBASSIGNMENT...
                    SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNO", strConvertZeroPadding(ASSIGNMENTNO));
                    cmdM.Parameters.AddWithValue("@NDSNO", NDSNO);
                    cmdM.Parameters.AddWithValue("@ESNNO", ESNNO);
                    cmdM.Parameters.AddWithValue("@JOBNO", strConvertZeroPadding(JOBNO));
                    cmdM.Parameters.AddWithValue("@ASSIGNDATE", setDateFormat(ASSIGNDATE, true));
                    cmdM.Parameters.AddWithValue("@ASSIGNTIME", ASSIGNTIME);
                    cmdM.Parameters.AddWithValue("@ENGINEERKEY", ENGINEERKEY);
                    cmdM.Parameters.AddWithValue("@MODELSKEY", MODELSKEY);
                    cmdM.Parameters.AddWithValue("@CONDITIONKEY", CONDITIONKEY);
                    cmdM.Parameters.AddWithValue("@LEVELKEY", LEVELKEY);
                    cmdM.Parameters.AddWithValue("@PROBLEMKEY", PROBLEMKEY);
                    cmdM.Parameters.AddWithValue("@RECEIVEDATE", setDateFormat(RECEIVEDATE, true));
                    cmdM.Parameters.AddWithValue("@PRESCANNINGDATE", setDateFormat(PRESCANNINGDATE, true));
                    cmdM.Parameters.AddWithValue("@PRESCANNINGPROBLEMKEY", PRESCANNINGPROBLEMKEY);
                    cmdM.Parameters.AddWithValue("@ISPFAULTCODE", ISPFAULTCODE);
                    cmdM.Parameters.AddWithValue("@ISPFAULTVALUE", ISPFAULTVALUE);
                    cmdM.Parameters.AddWithValue("@CIDREASON", CIDREASON);
                    cmdM.Parameters.AddWithValue("@TAGKEY", TAGKEY);
                    cmdM.Parameters.AddWithValue("@FAULTREPORTEDKEY", FAULTREPORTEDKEY);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@ACTION", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                    return ASSIGNMENTNO;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        public int UPDATENOTIFICATIONENTRY(string ASSIGNMENTNO, int MODELSKEY, string ISPFAULTCODE, int TAGKEY, int FAULTREPORTEDKEY, string RECEIVEDATE, string UserId, string MODE = "UPDATE")
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE NOTIFICATION ENTRY...
                    SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@MODELSKEY", MODELSKEY);
                    cmdM.Parameters.AddWithValue("@ISPFAULTCODE", ISPFAULTCODE);
                    cmdM.Parameters.AddWithValue("@TAGKEY", TAGKEY);
                    cmdM.Parameters.AddWithValue("@FAULTREPORTEDKEY", FAULTREPORTEDKEY);
                    cmdM.Parameters.AddWithValue("@RECEIVEDATE", setDateFormat(RECEIVEDATE, true));
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNO", ASSIGNMENTNO);
                    cmdM.Parameters.AddWithValue("@ACTION", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public DataTable GETTATASKYJOBASSIGNMENT(string ASSIGNMENTNO, string NDSNO, string JOBNO, string ESNNO, Int64 ENGINEERKEY, Int64 MODELSKEY,
                                                Int64 CONDITIONKEY, Int64 PROBLEMKEY, Int64 LEVELKEY, string MODE, Int64 UserId)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@ASSIGNMENTNO", (ASSIGNMENTNO.Length > 0 ? strConvertZeroPadding(ASSIGNMENTNO) : ""));
                cmd.Parameters.AddWithValue("@NDSNO", NDSNO);
                cmd.Parameters.AddWithValue("@JOBNO", (JOBNO.Length > 0 ? strConvertZeroPadding(JOBNO) : ""));
                cmd.Parameters.AddWithValue("@ENGINEERKEY", ENGINEERKEY);
                cmd.Parameters.AddWithValue("@MODELSKEY", MODELSKEY);
                cmd.Parameters.AddWithValue("@CONDITIONKEY", CONDITIONKEY);
                cmd.Parameters.AddWithValue("@LEVELKEY", LEVELKEY);
                cmd.Parameters.AddWithValue("@PROBLEMKEY", PROBLEMKEY);
                cmd.Parameters.AddWithValue("@ESNNO", ESNNO);
                cmd.Parameters.AddWithValue("@CREATEBY", UserId);
                cmd.Parameters.AddWithValue("@ACTION", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        // CALL ASSIGNMENT 

        // QC SCREEN 
        public DataTable GETCALLASSIGMENTFORQCUPDATE(string FROMDATE, string TODATE, string ASSIGNMENTNO, string NDSNO, string ESNNO,
                                                     Int64 ENGINEERKEY, Int64 MODELSKEY, Int64 CONDITIONKEY, Int64 LEVELKEY, string MODE = "QCSEARCH")
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            //SP_SELECT_PODATA
            SqlCommand cmd = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@Todate", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ASSIGNMENTNO", (ASSIGNMENTNO.Length > 0 ? strConvertZeroPadding(ASSIGNMENTNO) : ""));
                cmd.Parameters.AddWithValue("@NDSNO", NDSNO);
                cmd.Parameters.AddWithValue("@ESNNO", ESNNO);
                cmd.Parameters.AddWithValue("@ENGINEERKEY", ENGINEERKEY);
                cmd.Parameters.AddWithValue("@MODELSKEY", MODELSKEY);
                cmd.Parameters.AddWithValue("@CONDITIONKEY", CONDITIONKEY);
                cmd.Parameters.AddWithValue("@LEVELKEY", LEVELKEY);
                cmd.Parameters.AddWithValue("@ACTION", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int SAVEQCBULKUPDATE(string QCBULKUPDATEJSON, string UserId, string MODE = "QCBULKUPDATE")
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region BULKUPDATE QC STATUS...
                    SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@QCBULKUPDATEJSON", QCBULKUPDATEJSON);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@ACTION", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        // QC SCREEN 

        // DAILY REPORT
        public DataTable GETDAILYREPORT(string FROMDATE, string TODATE, string ASSIGNMENTNO, string NDSNO, string ESNNO,
                                                    Int64 ENGINEERKEY, Int64 MODELSKEY, Int64 CONDITIONKEY, Int64 LEVELKEY, int REPARITASKDESCRIPTIONKEY, string MODE = "DAILYPRODUCTION")
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            //SP_SELECT_PODATA
            SqlCommand cmd = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@Todate", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ASSIGNMENTNO", (ASSIGNMENTNO.Length > 0 ? strConvertZeroPadding(ASSIGNMENTNO) : ""));
                cmd.Parameters.AddWithValue("@NDSNO", NDSNO);
                cmd.Parameters.AddWithValue("@ESNNO", ESNNO);
                cmd.Parameters.AddWithValue("@ENGINEERKEY", ENGINEERKEY);
                cmd.Parameters.AddWithValue("@MODELSKEY", MODELSKEY);
                cmd.Parameters.AddWithValue("@CONDITIONKEY", CONDITIONKEY);
                cmd.Parameters.AddWithValue("@LEVELKEY", LEVELKEY);
                cmd.Parameters.AddWithValue("@REPARITASKDESCRIPTIONKEY", REPARITASKDESCRIPTIONKEY);
                cmd.Parameters.AddWithValue("@ACTION", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        //GET ESN DETAIL
        public DataTable GETESNTOMODELDETAIL(string ESNSTARTNO, string MODE = "SELECTONE")
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            //ESNTOMODELDETAILCRUDOPERATIONS
            SqlCommand cmd = new SqlCommand("ESNTOMODELDETAILCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ESNSTARTNO", ESNSTARTNO);
                cmd.Parameters.AddWithValue("@ACTION", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GETTATASKYJOBASSIGNMENTFORPRESCANREPAIRSTAGE(string ESNNO, string MODE)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@ESNNO", ESNNO);
                cmd.Parameters.AddWithValue("@ACTION", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int SAVEPRESCANDETAIL(string ASSIGNMENTNO, string PRESCANDATE, int PRESCANPROBLEMKEY, int PRESCANNINGENGINEERKEY, int TECHENGINEERKEY, string UserId, string MODE = "PRESCANNINGUPDATE")
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region SAVEPRESCANDETAIL...
                    SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@PRESCANNINGDATE", setDateFormat(PRESCANDATE, true));
                    cmdM.Parameters.AddWithValue("@PRESCANNINGPROBLEMKEY", PRESCANPROBLEMKEY);
                    cmdM.Parameters.AddWithValue("@PRESCANNINGENGINEERKEY", PRESCANNINGENGINEERKEY);
                    cmdM.Parameters.AddWithValue("@ENGINEERKEY", TECHENGINEERKEY);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNO", ASSIGNMENTNO);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@ACTION", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int SAVEREPAIRDETAIL(string ASSIGNMENTNO, string REPAIRDATE, int REPARIENGINEERKEY, int OBJECTPARTKEY,
            int FAULTOBSERVEDKEY, int FAULTREASONKEY, int ACTIONKEY, int REPARITASKDESCRIPTIONKEY,
            string UserId, string PARTASSIGMENTJSON, string MODE = "REPAIRUPDATE")
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region SAVEPRESCANDETAIL...
                    SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@REPAIRDATE", setDateFormat(REPAIRDATE, true));
                    cmdM.Parameters.AddWithValue("@REPARIENGINEERKEY", REPARIENGINEERKEY);
                    cmdM.Parameters.AddWithValue("@OBJECTPARTKEY", OBJECTPARTKEY);
                    cmdM.Parameters.AddWithValue("@FAULTOBSERVEDKEY", FAULTOBSERVEDKEY);
                    cmdM.Parameters.AddWithValue("@FAULTREASONKEY", FAULTREASONKEY);
                    cmdM.Parameters.AddWithValue("@ACTIONKEY", ACTIONKEY);
                    cmdM.Parameters.AddWithValue("@REPARITASKDESCRIPTIONKEY", REPARITASKDESCRIPTIONKEY);
                    cmdM.Parameters.AddWithValue("@PARTNAME", "");
                    cmdM.Parameters.AddWithValue("@PARTLOCATION", "");
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNO", ASSIGNMENTNO);
                    cmdM.Parameters.AddWithValue("@PARTASSIGMENTJSON", PARTASSIGMENTJSON);
                    cmdM.Parameters.AddWithValue("@ACTION", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int DELETEREPAIRPARTDETAIL(string ASSIGNMENTNO, int PARTASSIGMENTKEY, string MODE = "DELETE")
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region SAVEPRESCANDETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_TATASKYASSIGMENTINSERT_DELETE", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@PARTASSIGMENTKEY", PARTASSIGMENTKEY);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNO", ASSIGNMENTNO);
                    cmdM.Parameters.AddWithValue("@ACTION", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public DataTable GetPartsDetail(string assignmentNo, string PARTENTRYFOR)
        {
            //SP_SELECT_MST_CONDITION_CALTAX
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_TATASKYASSIGMENTINSERT_DELETE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ASSIGNMENTNO", assignmentNo);
                cmd.Parameters.AddWithValue("@PARTENTRYFOR", PARTENTRYFOR);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTALL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int SAVEIRDETAIL(string ASSIGNMENTNO, string IRDATE, int REASONFORIRKEY,
            int REPARIENGINEERKEY, int OBJECTPARTKEY, int FAULTOBSERVEDKEY, int FAULTREASONKEY,
            int ACTIONKEY, int REPARITASKDESCRIPTIONKEY, string UserId, string partjson, string BERDATE, int REASONFORBERKEY, string MODE = "IRUPDATE"
            )
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region SAVEPRESCANDETAIL...
                    SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@IRDATE", (IRDATE.Length > 0 ? setDateFormat(IRDATE, true) : null));
                    cmdM.Parameters.AddWithValue("@REASONFORIRKEY", REASONFORIRKEY);
                    cmdM.Parameters.AddWithValue("@BERDATE", (BERDATE.Length > 0 ? setDateFormat(BERDATE, true) : null));
                    cmdM.Parameters.AddWithValue("@REASONFORBERKEY", REASONFORBERKEY);
                    cmdM.Parameters.AddWithValue("@REPARIENGINEERKEY", REPARIENGINEERKEY);
                    cmdM.Parameters.AddWithValue("@OBJECTPARTKEY", OBJECTPARTKEY);
                    cmdM.Parameters.AddWithValue("@FAULTOBSERVEDKEY", FAULTOBSERVEDKEY);
                    cmdM.Parameters.AddWithValue("@FAULTREASONKEY", FAULTREASONKEY);
                    cmdM.Parameters.AddWithValue("@ACTIONKEY", ACTIONKEY);
                    cmdM.Parameters.AddWithValue("@REPARITASKDESCRIPTIONKEY", REPARITASKDESCRIPTIONKEY);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNO", ASSIGNMENTNO);
                    cmdM.Parameters.AddWithValue("@PARTASSIGMENTJSON", partjson);
                    cmdM.Parameters.AddWithValue("@ACTION", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int SAVEBERDETAIL(string ASSIGNMENTNO, string BERDATE, int REASONFORBERKEY,
           int REPARIENGINEERKEY, int OBJECTPARTKEY, int FAULTOBSERVEDKEY, int FAULTREASONKEY,
           int ACTIONKEY, int REPARITASKDESCRIPTIONKEY, string UserId, string partjson, string MODE = "BERUPDATE"
           )
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region SAVEBERDETAIL...
                    SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@BERDATE", setDateFormat(BERDATE, true));
                    cmdM.Parameters.AddWithValue("@REASONFORBERKEY", REASONFORBERKEY);
                    cmdM.Parameters.AddWithValue("@REPARIENGINEERKEY", REPARIENGINEERKEY);
                    cmdM.Parameters.AddWithValue("@OBJECTPARTKEY", OBJECTPARTKEY);
                    cmdM.Parameters.AddWithValue("@FAULTOBSERVEDKEY", FAULTOBSERVEDKEY);
                    cmdM.Parameters.AddWithValue("@FAULTREASONKEY", FAULTREASONKEY);
                    cmdM.Parameters.AddWithValue("@ACTIONKEY", ACTIONKEY);
                    cmdM.Parameters.AddWithValue("@REPARITASKDESCRIPTIONKEY", REPARITASKDESCRIPTIONKEY);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNO", ASSIGNMENTNO);
                    cmdM.Parameters.AddWithValue("@PARTASSIGMENTJSON", partjson);
                    cmdM.Parameters.AddWithValue("@ACTION", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int SAVEFAILUREDETAIL(string ASSIGNMENTNO, string FAILUREENTRYDATE, int FAILURESTAGEKEY, int FAILUREFAULTEKEY,
            int INSPECTORYKEY, string UserId, string MODE = "FAILUREUPDATE")
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region SAVEPRESCANDETAIL...
                    SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@FAILUREENTRYDATE", setDateFormat(FAILUREENTRYDATE, true));
                    cmdM.Parameters.AddWithValue("@FAILURESTAGEKEY", FAILURESTAGEKEY);
                    cmdM.Parameters.AddWithValue("@FAILUREFAULTEKEY", FAILUREFAULTEKEY);
                    cmdM.Parameters.AddWithValue("@INSPECTORYKEY", INSPECTORYKEY);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNO", ASSIGNMENTNO);
                    cmdM.Parameters.AddWithValue("@ACTION", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }


        public int SAVEDISPATCHDETAIL(string ASSIGNMENTNO, string DISPATCHDATE, string UserId, string MODE = "DISPATCH")
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region SAVEPRESCANDETAIL...
                    SqlCommand cmdM = new SqlCommand("TATASKYJOBASSIGNMENTCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmdM.Parameters.AddWithValue("@DISPATCHDATE", setDateFormat(DISPATCHDATE, true));
                    cmdM.Parameters.AddWithValue("@FAILURESTAGEKEY", 301);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNO", ASSIGNMENTNO);
                    cmdM.Parameters.AddWithValue("@ACTION", MODE);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }


        public int BULKADDUPDATENOTIFICATIONENTRY(string NOTIFICATIONJSONDETAIL, string ASSIGNMENTNOPREFIX, string ASSIGNDATE, string ASSIGNTIME, int UserId)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE NOTIFICATION ENTRY...
                    SqlCommand cmdM = new SqlCommand("SP_TATASKYNOTIFICATIONIMPORT", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@NOTIFICATIONJSONDETAIL", NOTIFICATIONJSONDETAIL);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNOPREFIX", ASSIGNMENTNOPREFIX);
                    cmdM.Parameters.AddWithValue("@ASSIGNDATE", ASSIGNDATE);
                    cmdM.Parameters.AddWithValue("@ASSIGNTIME", ASSIGNTIME);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int BULKADDUPDATEBECKENDNOTIFICATIONENTRY(string NOTIFICATIONJSONDETAIL, string ASSIGNMENTNOPREFIX, string ASSIGNTIME, int UserId)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE NOTIFICATION ENTRY...
                    SqlCommand cmdM = new SqlCommand("SP_TATASKYBECKENDNOTIFICATIONIMPORT", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@NOTIFICATIONJSONDETAIL", NOTIFICATIONJSONDETAIL);
                    cmdM.Parameters.AddWithValue("@ASSIGNMENTNOPREFIX", ASSIGNMENTNOPREFIX);
                    cmdM.Parameters.AddWithValue("@ASSIGNTIME", ASSIGNTIME);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int BULKADDUPDATEBECKENDPARTSENTRY(string PARTSJSONDETAIL, int UserId)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE NOTIFICATION ENTRY...
                    SqlCommand cmdM = new SqlCommand("SP_TATASKYBECKENDNOTIFICATIONPARTSIMPORT", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@NOTIFICATIONPARTSJSONDETAIL", PARTSJSONDETAIL);
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }


        public int BULKADDUPDATEDISPATCHENTRY(string DISPATCHJSONDETAIL, string DISPATCHDATE, int UserId)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE DISPATCH ENTRY...
                    SqlCommand cmdM = new SqlCommand("SP_TATASKYDISPATCHIMPORT", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@DISPATCHJSONDETAIL", DISPATCHJSONDETAIL);
                    cmdM.Parameters.AddWithValue("@DISPATCHDATE", objMainClass.setDateFormat(DISPATCHDATE, true));
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int BULKUPDATEBERTOIRENTRY(string BERTOIRJSONDETAIL, string IRDATE, int UserId)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE DISPATCH ENTRY...
                    SqlCommand cmdM = new SqlCommand("SP_TATASKYBERTOIRIMPORT", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@BERTOIRJSONDETAIL", BERTOIRJSONDETAIL);
                    cmdM.Parameters.AddWithValue("@IRDATE", objMainClass.setDateFormat(IRDATE, true));
                    cmdM.Parameters.AddWithValue("@CREATEBY", UserId);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }




        // TATA SKY SYSTEM METHOD END



        //SP_GETCONDITION_BY_RATE
        public DataTable GetTaxByRate(decimal RATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GETCONDITION_BY_RATE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@RATE", RATE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }



        public DataTable GetTaxCalData(string CONDTYPE)
        {
            //SP_SELECT_MST_CONDITION_CALTAX
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_CONDITION_CALTAX", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CONDTYPE", CONDTYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        // TATASKY DROPDOWN IN DATATABLE

        public DataTable GetTaTaSkyReqDropDown(string comboname, string req = "REQUESTDROPDOWN", string reqtype = "Entry")
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("COMBOGET", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@COMBONAME", comboname);
            cmd.Parameters.AddWithValue("@COMBOREQUEST", req);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            dt.Rows.Add("0", "-- SELECT --");
            return dt;
        }

        // TATASKY DROPDOWN IN DATATABLE

        // TATASKY DASHBOARD IN DATATABLE

        public DataTable GetTaTaSkyDashBoard()
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_TATASKYDASHBOARD", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable GetTaTaSkyDispatchReportforSAPUpload(string frmdate, string todate)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_TATASKYDISPATCHSAPREPORT", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(frmdate, true));
            cmd.Parameters.AddWithValue("@Todate", setDateFormat(todate, true));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable GetTaTaSkyRepairReportforSAPUpload(string frmdate, string todate)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_TATASKYREPAIRREPORT", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(frmdate, true));
            cmd.Parameters.AddWithValue("@Todate", setDateFormat(todate, true));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable GetTaTaSkyBERReportforSAPUpload(string frmdate, string todate)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_TATASKYBERREPORT", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(frmdate, true));
            cmd.Parameters.AddWithValue("@Todate", setDateFormat(todate, true));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }


        public DataTable GetTaTaSkyIRReportforSAPUpload(string frmdate, string todate)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_TATASKYIRREPORT", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(frmdate, true));
            cmd.Parameters.AddWithValue("@Todate", setDateFormat(todate, true));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable GetTaTaSkyConsumptionReportforSAPUpload(string frmdate, string todate)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_TATASKYCONSUMPTIONREPORT", objMainClass.ConnSherpa);
            cmd.CommandTimeout = 400;
            cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(frmdate, true));
            cmd.Parameters.AddWithValue("@Todate", setDateFormat(todate, true));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        // TATASKY DROPDOWN IN DATATABLE


        // Purchase bill method start


        public string SavePurchaseBill(int CMPID, string DOCTYPE, string PBTYPE, string PBDT, string VENDCODE, string BILLNO, string BILLDT,
                                       string PMTTERMS, string PMTTERMSDESC, Decimal NETMATVALUE, Decimal NETTAXAMT, Decimal DISCOUNT, Decimal NETPBAMT,
                                       string REMARK, Int64 STATUS, GridView ITEMDETAIL, GridView TAXDETAIL, GridView CHARGESDETAIL, string CREATEBY
                                      , string Action)
        {
            MainClass objMainClass = new MainClass();
            string PBNO = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    PBNO = MAXPRNO(DOCTYPE, "PB");
                    PBNO = strConvertZeroPadding(PBNO);

                    #region Update MMNORANGE...
                    SqlCommand POcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    POcmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    POcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(PBNO));
                    POcmd.Parameters.AddWithValue("@TRANTYPE", DOCTYPE);
                    POcmd.Parameters.AddWithValue("@DOCTYPE", "PB");
                    POcmd.CommandType = CommandType.StoredProcedure;
                    POcmd.Connection.Open();
                    POcmd.ExecuteNonQuery();
                    POcmd.Connection.Close();
                    #endregion

                    //SP_TRAN_PBMST
                    #region Insert MST Purchase Bill...
                    SqlCommand cmdM = new SqlCommand("SP_INSERT_TRAN_PBMST", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdM.Parameters.AddWithValue("@PBTYPE", PBTYPE);
                    cmdM.Parameters.AddWithValue("@PBNO", PBNO);
                    cmdM.Parameters.AddWithValue("@PBDT", setDateFormat(PBDT, true));
                    cmdM.Parameters.AddWithValue("@VENDCODE", VENDCODE);
                    cmdM.Parameters.AddWithValue("@BILLNO", BILLNO);
                    cmdM.Parameters.AddWithValue("@BILLDT", setDateFormat(BILLDT, true));
                    cmdM.Parameters.AddWithValue("@PMTTERMS", PMTTERMS);
                    cmdM.Parameters.AddWithValue("@PMTTERMSDESC", PMTTERMSDESC);
                    cmdM.Parameters.AddWithValue("@NETMATVALUE", NETMATVALUE);
                    cmdM.Parameters.AddWithValue("@NETTAXAMT", NETTAXAMT);
                    cmdM.Parameters.AddWithValue("@DISCOUNT", DISCOUNT);
                    cmdM.Parameters.AddWithValue("@NETPBAMT", NETPBAMT);
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@STATUS", STATUS);
                    cmdM.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmdM.Parameters.AddWithValue("@Action", Action);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    int resultmaster = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion

                    for (int i = 0; i < ITEMDETAIL.Rows.Count; i++)
                    {
                        GridViewRow row = ITEMDETAIL.Rows[i];

                        SqlCommand cmdD = new SqlCommand("SP_INSERT_TRAN_PBDTL", ConnSherpa);
                        cmdD.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdD.Parameters.AddWithValue("@PBNO", strConvertZeroPadding(PBNO));
                        cmdD.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblGVSrNo")).Text);
                        cmdD.Parameters.AddWithValue("@PONO", strConvertZeroPadding(((Label)row.FindControl("lblGVPoNo")).Text));
                        cmdD.Parameters.AddWithValue("@POSRNO", ((Label)row.FindControl("lblGVPoSrNo")).Text);
                        cmdD.Parameters.AddWithValue("@MIRNO", strConvertZeroPadding(((Label)row.FindControl("lblGVGRNNo")).Text));
                        cmdD.Parameters.AddWithValue("@MIRSRNO", ((Label)row.FindControl("lblGVGRNSrNo")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblGVItemId")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblGVItemDesc")).Text);
                        cmdD.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblGVPlantCD")).Text);
                        cmdD.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblGVLocationCD")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGVGroupId")).Text);
                        cmdD.Parameters.AddWithValue("@PBQTY", ((Label)row.FindControl("lblGVQty")).Text);
                        cmdD.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblGVUOMID")).Text);
                        cmdD.Parameters.AddWithValue("@BRATE", ((Label)row.FindControl("lblGVBaseRate")).Text);
                        cmdD.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblGVAmount")).Text);
                        cmdD.Parameters.AddWithValue("@DISCAMT", ((Label)row.FindControl("lblGVDiscount")).Text);
                        cmdD.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGVGLCode")).Text);
                        cmdD.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblGVCostCenter")).Text);
                        cmdD.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblGVProfitCenter")).Text);
                        cmdD.Parameters.AddWithValue("@ASSETCD", ((Label)row.FindControl("lblGVAssetCode")).Text);
                        cmdD.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblGVItemText")).Text);
                        cmdD.Parameters.AddWithValue("@TRNUM", strConvertZeroPadding(((Label)row.FindControl("lblGVTrackNo")).Text));
                        cmdD.Parameters.AddWithValue("@REFNO", ((Label)row.FindControl("lblGVRefNo")).Text);
                        cmdD.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblGVRate")).Text);
                        cmdD.Parameters.AddWithValue("@Action", Action);
                        cmdD.CommandType = CommandType.StoredProcedure;
                        cmdD.Connection.Open();
                        int resultchild = cmdD.ExecuteNonQuery();
                        cmdD.Connection.Close();
                    }

                    for (int j = 0; j < TAXDETAIL.Rows.Count; j++)
                    {
                        GridViewRow row = TAXDETAIL.Rows[j];

                        SqlCommand cmdT = new SqlCommand("SP_INSERT_TRAN_PBCOND", ConnSherpa);

                        cmdT.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdT.Parameters.AddWithValue("@CONDORDER", ((Label)row.FindControl("lblTaxSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@PBNO", PBNO);
                        cmdT.Parameters.AddWithValue("@PBSRNO", ((Label)row.FindControl("lblPBSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@CONDID", ((Label)row.FindControl("lblCONDID")).Text);
                        cmdT.Parameters.AddWithValue("@CONDTYPE", ((Label)row.FindControl("lblTaxCondType")).Text);
                        cmdT.Parameters.AddWithValue("@GLCODE", ((Label)row.FindControl("lblGLCode")).Text);
                        cmdT.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblTaxRate")).Text);
                        cmdT.Parameters.AddWithValue("@BASEAMT", ((Label)row.FindControl("lblTaxBaseAmount")).Text);
                        cmdT.Parameters.AddWithValue("@PID", ((Label)row.FindControl("lblPID")).Text);
                        cmdT.Parameters.AddWithValue("@TAXAMT", ((Label)row.FindControl("lblTaxAmount")).Text);
                        cmdT.Parameters.AddWithValue("@OPERATOR", ((Label)row.FindControl("lblTaxOperator")).Text);
                        cmdT.Parameters.AddWithValue("@Action", Action);
                        cmdT.CommandType = CommandType.StoredProcedure;
                        cmdT.Connection.Open();
                        int resulttax = cmdT.ExecuteNonQuery();
                        cmdT.Connection.Close();
                    }

                    for (int k = 0; k < CHARGESDETAIL.Rows.Count; k++)
                    {
                        //SP_INSERT_POCHARGES
                        GridViewRow row = CHARGESDETAIL.Rows[k];
                        SqlCommand cmdC = new SqlCommand("SP_INSERT_TRAN_PBCHG", ConnSherpa);
                        cmdC.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdC.Parameters.AddWithValue("@PBNO", strConvertZeroPadding(PBNO));
                        cmdC.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblChrgSrNo")).Text);
                        cmdC.Parameters.AddWithValue("@CHGTYPE", (((Label)row.FindControl("lblChrgCondType")).Text).Split('-')[0].Trim());
                        cmdC.Parameters.AddWithValue("@CHGAMT", ((Label)row.FindControl("lblChrgAmount")).Text);
                        cmdC.Parameters.AddWithValue("@Action", Action);
                        cmdC.CommandType = CommandType.StoredProcedure;
                        cmdC.Connection.Open();
                        int resultcharges = cmdC.ExecuteNonQuery();
                        cmdC.Connection.Close();
                    }
                    scope.Complete();
                    scope.Dispose();
                    return PBNO;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        public DataTable GetPurchaseBill(string FROMDATE, string TODATE, string PBNO, string PONO)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            //SP_SELECT_PURCHASEBILL
            SqlCommand cmd = new SqlCommand("SP_SELECT_PURCHASEBILL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@PBNO", (PBNO.Length > 0 ? strConvertZeroPadding(PBNO) : ""));
                cmd.Parameters.AddWithValue("@PONO", (PONO.Length > 0 ? strConvertZeroPadding(PONO) : ""));
                cmd.Parameters.AddWithValue("@Action", "SELECTALL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetEachPurchaseBill(string PBNO, string RequestTable)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            //SP_SELECT_PURCHASEBILL
            SqlCommand cmd = new SqlCommand("SP_SELECT_PURCHASEBILL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@PBNO", (PBNO.Length > 0 ? strConvertZeroPadding(PBNO) : ""));
                cmd.Parameters.AddWithValue("@Action", "SELECTONE");
                cmd.Parameters.AddWithValue("@RequestTable", RequestTable);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable CheckPurchaseBillPoGrNExist(string PONO, string VENDCODE, string MMDOCNO, string MMSRNO, string BILLNO = "", string MODE = "CHECKPONOEXIST")
        {
            MainClass objMainClass = new MainClass();
            DataTable dtMaerialDetail = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PURCHASE_BILL_CHECKEXIST", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@PONO", (PONO.Length > 0 ? strConvertZeroPadding(PONO) : ""));
                cmd.Parameters.AddWithValue("@VENDCODE", (VENDCODE.Length > 0 ? strConvertZeroPadding(VENDCODE) : ""));
                cmd.Parameters.AddWithValue("@MMDOCNO", (MMDOCNO.Length > 0 ? strConvertZeroPadding(MMDOCNO) : ""));
                cmd.Parameters.AddWithValue("@MMSRNO", Convert.ToInt64((MMSRNO.Length > 0 ? MMSRNO : "0")));
                cmd.Parameters.AddWithValue("@BILLNO", BILLNO);
                cmd.Parameters.AddWithValue("@MODE", MODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtMaerialDetail);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dtMaerialDetail;
        }

        public DataTable PurchaseBillReport(string PBNO, string Mode = "MASTER")
        {
            MainClass objMainClass = new MainClass();
            DataTable dtPBDetail = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_REPORT_PURCHASEBILL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@PBNO", (PBNO.Length > 0 ? strConvertZeroPadding(PBNO) : ""));
                cmd.Parameters.AddWithValue("@Action", Mode);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dtPBDetail);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dtPBDetail;
        }

        //SP_REPORT_PO
        public DataTable PurchaseOrderReport(int CMPID, string PONO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_REPORT_PO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@PONO", strConvertZeroPadding(PONO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;

        }


        public DataTable GetSafetyQuestion(int STATUS, string TYPE)
        {
            //SP_SELECT_SAFETYQUESTION
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_SAFETYQUESTION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public int SPSafetyReport(string REPORTDATE, string LOCATION, string INSPECTBY, string CREATEBY, GridView GRVSAFETYREPORT, string TYPE)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string ID = string.Empty;
                    SqlCommand cmd = new SqlCommand("SP_INSERT_SAFETYREPORT", ConnSherpa);
                    cmd.Parameters.AddWithValue("@REPORTDATE", setDateFormat(REPORTDATE, true));
                    cmd.Parameters.AddWithValue("@LOCATION", LOCATION);
                    cmd.Parameters.AddWithValue("@INSPECTBY", INSPECTBY);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@TYPE", TYPE);
                    cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    ID = Convert.ToString((cmd.Parameters["@ID"].Value));
                    cmd.Connection.Close();

                    for (int i = 0; i < GRVSAFETYREPORT.Rows.Count; i++)
                    {
                        GridViewRow row = GRVSAFETYREPORT.Rows[i];
                        string questionid = ((Label)row.FindControl("lblID")).Text;
                        RadioButtonList rblAnswer = (RadioButtonList)row.FindControl("rblAnswer");
                        int result = Convert.ToInt32(rblAnswer.SelectedValue);
                        FileUpload fuImage = (FileUpload)row.FindControl("fuImage");
                        string Remarks = ((TextBox)row.FindControl("txtRemarks")).Text;

                        byte[] bytes = null;
                        byte[] results = null;
                        if (fuImage != null)
                        {
                            if (fuImage.HasFiles)
                            {
                                System.Drawing.Image uploaded = System.Drawing.Image.FromStream(fuImage.PostedFile.InputStream);

                                int originalWidth = uploaded.Width;
                                int originalHeight = uploaded.Height;
                                float percentWidth = (float)256 / (float)originalWidth;
                                float percentHeight = (float)256 / (float)originalHeight;
                                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                                int newWidth = (int)(originalWidth * percent);
                                int newHeight = (int)(originalHeight * percent);

                                System.Drawing.Image newImage = new System.Drawing.Bitmap(newWidth, newHeight);
                                using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newImage))
                                {
                                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    g.DrawImage(uploaded, 0, 0, newWidth, newHeight);
                                }


                                using (MemoryStream ms = new MemoryStream())
                                {
                                    System.Drawing.Imaging.ImageCodecInfo codec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                                    System.Drawing.Imaging.EncoderParameters jpegParms = new System.Drawing.Imaging.EncoderParameters(1);
                                    jpegParms.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 95L);
                                    newImage.Save(ms, codec, jpegParms);
                                    results = ms.ToArray();
                                }



                                //using (BinaryReader br = new BinaryReader(fuImage.PostedFile.InputStream))
                                //{
                                //    bytes = br.ReadBytes(fuImage.PostedFile.ContentLength);
                                //}
                            }
                        }
                        int iReturn = InsertSafetyReport(ID, questionid, result, results, Remarks);

                    }
                    iResult = 1;
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;
        }

        public int SPComplianceReport(string REPORTDATE, string LOCATION, string INSPECTBY, string CREATEBY, GridView GRVSAFETYREPORT, string TYPE)//, byte[] IMAGE1, byte[] IMAGE2, byte[] IMAGE3)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string ID = string.Empty;
                    SqlCommand cmd = new SqlCommand("SP_INSERT_SAFETYREPORT", ConnSherpa);
                    cmd.Parameters.AddWithValue("@REPORTDATE", setDateFormat(REPORTDATE, true));
                    cmd.Parameters.AddWithValue("@LOCATION", LOCATION);
                    cmd.Parameters.AddWithValue("@INSPECTBY", INSPECTBY);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@TYPE", TYPE);
                    cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    ID = Convert.ToString((cmd.Parameters["@ID"].Value));
                    cmd.Connection.Close();

                    for (int i = 0; i < GRVSAFETYREPORT.Rows.Count; i++)
                    {
                        GridViewRow row = GRVSAFETYREPORT.Rows[i];
                        string questionid = ((Label)row.FindControl("lblID")).Text;
                        RadioButtonList rblAnswer = (RadioButtonList)row.FindControl("rblAnswer");
                        int result = Convert.ToInt32(rblAnswer.SelectedValue);
                        FileUpload fuImage = (FileUpload)row.FindControl("fuImage");
                        string Remarks = ((TextBox)row.FindControl("txtRemarks")).Text;

                        byte[] bytes = null;
                        if (fuImage != null)
                        {
                            if (fuImage.HasFiles)
                            {
                                using (BinaryReader br = new BinaryReader(fuImage.PostedFile.InputStream))
                                {
                                    bytes = br.ReadBytes(fuImage.PostedFile.ContentLength);
                                }
                            }
                        }
                        if (questionid == "15")
                        {
                            FileUpload fuImage1 = (FileUpload)row.FindControl("fuImage1");
                            byte[] bytes1 = null;
                            if (fuImage1 != null)
                            {
                                if (fuImage1.HasFiles)
                                {
                                    using (BinaryReader br = new BinaryReader(fuImage1.PostedFile.InputStream))
                                    {
                                        bytes1 = br.ReadBytes(fuImage1.PostedFile.ContentLength);
                                    }
                                }
                            }

                            FileUpload fuImage2 = (FileUpload)row.FindControl("fuImage2");
                            byte[] bytes2 = null;
                            if (fuImage2 != null)
                            {
                                if (fuImage2.HasFiles)
                                {
                                    using (BinaryReader br = new BinaryReader(fuImage2.PostedFile.InputStream))
                                    {
                                        bytes2 = br.ReadBytes(fuImage2.PostedFile.ContentLength);
                                    }
                                }
                            }

                            int iReturn0 = InsertSafetyReport(ID, questionid, result, bytes, Remarks);
                            int iReturn1 = InsertSafetyReport(ID, questionid, result, bytes1, Remarks);
                            int iReturn2 = InsertSafetyReport(ID, questionid, result, bytes2, Remarks);
                        }
                        else
                        {
                            int iReturn = InsertSafetyReport(ID, questionid, result, bytes, Remarks);
                        }

                    }
                    iResult = 1;
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;
        }

        public int InsertSafetyReport(string RPTID, string QUESTIONID, int RESULT, byte[] BYTES, string REMARKS)
        {
            //SP_INSERT_SAFETYREPORT
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_INSERT_SAFETYREPORT_DETAILS", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@RPTID", RPTID);
                cmd.Parameters.AddWithValue("@QUESTIONID", QUESTIONID);
                cmd.Parameters.AddWithValue("@RESULT", RESULT);
                cmd.Parameters.AddWithValue("@IMAGE", BYTES);
                cmd.Parameters.AddWithValue("@REMARKS", REMARKS);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;
        }

        public DataTable GetSafetyReport(int ID, string ACTION, string MAINQUERY, string FROMDATE, string TODATE, string AREA, string LOCATION, string QUESTION, int RESULT, string TYPE)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GET_SAFETYREPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@AREA", AREA == "0" ? "" : AREA);
                cmd.Parameters.AddWithValue("@LOCATION", LOCATION == "0" ? "" : LOCATION);
                cmd.Parameters.AddWithValue("@QUESTION", QUESTION == "0" ? "" : QUESTION);
                cmd.Parameters.AddWithValue("@RESULT", RESULT);
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }



        // Purchase bill method end



        public DataTable GETMRTOPURCHASEBILLREPORT(string FROMDATE, string TODATE, string MRNO, string PRNO, string PONO, string GRNNO, string PBNO, string PLANTCD, int ITEMGRPID)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("MRTOPURCHASEBILLREPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@Todate", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@MRNO", (MRNO.Length > 0 ? strConvertZeroPadding(MRNO) : ""));
                cmd.Parameters.AddWithValue("@PRNO", (PRNO.Length > 0 ? strConvertZeroPadding(PRNO) : ""));
                cmd.Parameters.AddWithValue("@PONO", (PONO.Length > 0 ? strConvertZeroPadding(PONO) : ""));
                cmd.Parameters.AddWithValue("@GRNNO", (GRNNO.Length > 0 ? strConvertZeroPadding(GRNNO) : ""));
                cmd.Parameters.AddWithValue("@PBNO", (PBNO.Length > 0 ? strConvertZeroPadding(PBNO) : ""));
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@LOCCD", "0");
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 500;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public DataTable GetGRMInvoiceRPT(string FROMDATE, string TODATE, string PLANTCODE, string DOCNO, string MAINQUERY, string PONO)
        {
            //SP_GRN_INVOICE_REPORT
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GRN_INVOICE_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCODE == "0" ? "" : PLANTCODE);
                cmd.Parameters.AddWithValue("@DOCNO", DOCNO == "" ? "" : strConvertZeroPadding(DOCNO));
                cmd.Parameters.AddWithValue("@PONO", PONO == "" ? "" : strConvertZeroPadding(PONO));
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetDCReport(string DCNO, string STATUS, decimal AMOUNT, string SHIPPED, string HSN, string ACTION, string TOTALBOX)
        {
            //SP_DCSUMMARY_REPORT
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DCSUMMARY_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DCNO", strConvertZeroPadding(DCNO));
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@AMOUNT", AMOUNT);
                cmd.Parameters.AddWithValue("@SHIPPED", SHIPPED);
                cmd.Parameters.AddWithValue("@HSN", HSN);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@TOTALBOX", TOTALBOX);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;

        }

        //VENDOR PURCHASE DETAIL SYSTEM 
        public int SaveProductEntry(string MAKE, string MODEL, string COLOR, string RAM, string ROM, string VENDORGRADE,
          Decimal VENDORPRICE, string VENDORQTY, string VENDORNAME, string UserId, int INVOICE, int BOX, int CHARGER, string imeino
            , int VENDORID, string REJECTREASON, string NGEAPRV, double MOBILENEWRATE, double MOBILEPURCHASEPERCENTAGE,
            int ISAPPROVEDFK, int ISAPPROVEDAMZ, int ISAPPROVEDWEB, double FKAMT, double AMZAMT, double WEBAMT, double FKPER,
            double AMZPER, double WEBPER, string ITEMCODE, int STATUS, byte[] IMAGE, string MNLAPRREASON, Decimal BASICPURRATE,
            double PURFKAMT, double PURAMZAMT, double PURWEBAMT, double PURFKPER, double PURAMZPER, double PURWEBPER, double PURCHASEPERONVENDORPRICE,
            int ISPURAPPROVEDFK, int ISPURAPPROVEDAMZ, int ISPURAPPROVEDWEB, int ITEMGRPID, int ITEMSUBGRPID, string MODELDIPLAYNAME
            , string PARTSNAME, string PARTSRATE, double FinalApproveListingAmount, int LISTINGTYPE)
        //,

        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region Insert PRODUCT ENTRY...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Connection.Close();
                    cmdM.Parameters.AddWithValue("@MAKE", MAKE);
                    cmdM.Parameters.AddWithValue("@MODEL", MODEL);
                    cmdM.Parameters.AddWithValue("@COLOR", COLOR);
                    cmdM.Parameters.AddWithValue("@RAM", RAM);
                    cmdM.Parameters.AddWithValue("@ROM", ROM);
                    cmdM.Parameters.AddWithValue("@VENDORGRADE", VENDORGRADE);
                    cmdM.Parameters.AddWithValue("@VENDORPRICE", VENDORPRICE);
                    cmdM.Parameters.AddWithValue("@VENDORQTY", VENDORQTY);
                    cmdM.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                    cmdM.Parameters.AddWithValue("@INVOICE", INVOICE);
                    cmdM.Parameters.AddWithValue("@BOX", BOX);
                    cmdM.Parameters.AddWithValue("@CHARGER", CHARGER);
                    cmdM.Parameters.AddWithValue("@CREATEBY", Convert.ToInt32(UserId));
                    cmdM.Parameters.AddWithValue("@IMEINO", imeino);
                    cmdM.Parameters.AddWithValue("@STATUS", STATUS);
                    cmdM.Parameters.AddWithValue("@VENDORID", VENDORID);
                    cmdM.Parameters.AddWithValue("@REJECTREASON", REJECTREASON);
                    cmdM.Parameters.AddWithValue("@NGEAPRV", NGEAPRV);
                    cmdM.Parameters.AddWithValue("@NEGAPRVBY", 14);
                    cmdM.Parameters.AddWithValue("@MOBILENEWRATE", MOBILENEWRATE);
                    cmdM.Parameters.AddWithValue("@MOBILEPURCHASEPERCENTAGE", MOBILEPURCHASEPERCENTAGE);
                    cmdM.Parameters.AddWithValue("@ISAPPROVEDFK", ISAPPROVEDFK);
                    cmdM.Parameters.AddWithValue("@ISAPPROVEDAMZ", ISAPPROVEDAMZ);
                    cmdM.Parameters.AddWithValue("@ISAPPROVEDWEB", ISAPPROVEDWEB);
                    cmdM.Parameters.AddWithValue("@FKAMT", FKAMT);
                    cmdM.Parameters.AddWithValue("@AMZAMT", AMZAMT);
                    cmdM.Parameters.AddWithValue("@WEBAMT", WEBAMT);
                    cmdM.Parameters.AddWithValue("@FKPER", FKPER);
                    cmdM.Parameters.AddWithValue("@AMZPER", AMZPER);
                    cmdM.Parameters.AddWithValue("@WEBPER", WEBPER);
                    cmdM.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                    cmdM.Parameters.AddWithValue("@IMAGE", IMAGE);
                    cmdM.Parameters.AddWithValue("@MNLAPRREASON", MNLAPRREASON);
                    cmdM.Parameters.AddWithValue("@BASICPURRATE", BASICPURRATE);
                    cmdM.Parameters.AddWithValue("@PURFKAMT", PURFKAMT);
                    cmdM.Parameters.AddWithValue("@PURAMZAMT", PURAMZAMT);
                    cmdM.Parameters.AddWithValue("@PURWEBAMT", PURWEBAMT);
                    cmdM.Parameters.AddWithValue("@PURFKPER", PURFKPER);
                    cmdM.Parameters.AddWithValue("@PURAMZPER", PURAMZPER);
                    cmdM.Parameters.AddWithValue("@PURWEBPER", PURWEBPER);
                    cmdM.Parameters.AddWithValue("@PURCHASEPERONVENDORPRICE", PURCHASEPERONVENDORPRICE);
                    cmdM.Parameters.AddWithValue("@ISPURAPPROVEDFK", ISPURAPPROVEDFK);
                    cmdM.Parameters.AddWithValue("@ISPURAPPROVEDAMZ", ISPURAPPROVEDAMZ);
                    cmdM.Parameters.AddWithValue("@ISPURAPPROVEDWEB", ISPURAPPROVEDWEB);
                    cmdM.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                    cmdM.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                    cmdM.Parameters.AddWithValue("@MODELDIPLAYNAME", MODELDIPLAYNAME);
                    cmdM.Parameters.AddWithValue("@PARTSNAME", PARTSNAME);
                    cmdM.Parameters.AddWithValue("@PARTSRATE", PARTSRATE);
                    cmdM.Parameters.AddWithValue("@FinalApproveListingAmount", FinalApproveListingAmount);
                    cmdM.Parameters.AddWithValue("@LISTINGTYPE", LISTINGTYPE);
                    cmdM.Parameters.AddWithValue("@ACTION", "INSERT");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
            return result;
        }


        public DataTable GetProductEntryDetail(string FROMDATE, string TODATE, string VENDORNAME, int status, int userid = 0, int ID = 0, string MNLAPRREASON = "", string IMEINO = "")
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 500;
                cmd.Parameters.AddWithValue("@FROMDATE", (FROMDATE.Length > 0 ? setDateFormat(FROMDATE, true) : ""));
                cmd.Parameters.AddWithValue("@TODATE", (TODATE.Length > 0 ? setDateFormat(TODATE, true) : ""));
                cmd.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MNLAPRREASON", MNLAPRREASON);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTALL");
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetPrPoDataDetail(string FROMDATE, string TODATE, string VENDORNAME, int status, int userid = 0, int ID = 0, string MNLAPRREASON = "", string IMEINO = "")
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_PRPOCREATION", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@FROMDATE", (FROMDATE.Length > 0 ? setDateFormat(FROMDATE, true) : ""));
                cmd.Parameters.AddWithValue("@TODATE", (TODATE.Length > 0 ? setDateFormat(TODATE, true) : ""));
                cmd.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MNLAPRREASON", MNLAPRREASON);
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTALL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetTestProductEntryDetail(string FROMDATE, string TODATE, string VENDORNAME, int status, int userid = 0)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTALL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetListedProductDetail(string VENDORNAME, int status, int id)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTALL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetAllStatusProductDetail(string VENDORNAME, int status, int id, int userid
            , int AGING = 0)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@AGING", AGING);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTALLSTATUS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateQcDetail(string QCRESULT, string MOBEXGRADE, string QCFAILREASON, string UserId, int ID)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE QC DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@QCRESULT", QCRESULT);
                    cmdM.Parameters.AddWithValue("@MOBEXGRADE", MOBEXGRADE);
                    cmdM.Parameters.AddWithValue("@QCFAILREASON", QCFAILREASON);
                    cmdM.Parameters.AddWithValue("@QCBY", Convert.ToInt32(UserId));
                    cmdM.Parameters.AddWithValue("@QCDATE", setDateFormat(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), true));
                    cmdM.Parameters.AddWithValue("@STATUS", 11228);
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@ACTION", "UPDATEQC");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int UpdatePurchaseDetail(string PURQTY, Decimal MOBEXPRICE, string NGEAPRV, int NEGAPRVBY, int STATUS, int ID,
            Double MOBILENEWRATE, Double MOBILEPURCHASEPERCENTAGE, int ISAPPROVEDFK, int ISAPPROVEDAMZ, int ISAPPROVEDWEB,
            Double FKAMT, Double AMZAMT, Double WEBAMT, Double FKPER, Double AMZPER, Double WEBPER, string REJECTREASON
            , string ITEMCODE, Double BASICPURRATE, double PURFKAMT, double PURAMZAMT, double PURWEBAMT, double PURFKPER, double PURAMZPER, double PURWEBPER, double PURCHASEPERONVENDORPRICE,
            int ISPURAPPROVEDFK, int ISPURAPPROVEDAMZ, int ISPURAPPROVEDWEB, double FinalApproveListingAmount)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE PURCHASE DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@PURQTY", PURQTY);
                    cmdM.Parameters.AddWithValue("@MOBEXPRICE", MOBEXPRICE);
                    cmdM.Parameters.AddWithValue("@NGEAPRV", NGEAPRV);
                    cmdM.Parameters.AddWithValue("@NEGAPRVBY", NEGAPRVBY);
                    cmdM.Parameters.AddWithValue("@NEGAPRVDATE", setDateFormat(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), true));
                    cmdM.Parameters.AddWithValue("@STATUS", STATUS);
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@MOBILENEWRATE", MOBILENEWRATE);
                    cmdM.Parameters.AddWithValue("@MOBILEPURCHASEPERCENTAGE", MOBILEPURCHASEPERCENTAGE);
                    cmdM.Parameters.AddWithValue("@ISAPPROVEDFK", ISAPPROVEDFK);
                    cmdM.Parameters.AddWithValue("@ISAPPROVEDAMZ", ISAPPROVEDAMZ);
                    cmdM.Parameters.AddWithValue("@ISAPPROVEDWEB", ISAPPROVEDWEB);
                    cmdM.Parameters.AddWithValue("@FKAMT", FKAMT);
                    cmdM.Parameters.AddWithValue("@AMZAMT", AMZAMT);
                    cmdM.Parameters.AddWithValue("@WEBAMT", WEBAMT);
                    cmdM.Parameters.AddWithValue("@FKPER", FKPER);
                    cmdM.Parameters.AddWithValue("@AMZPER", AMZPER);
                    cmdM.Parameters.AddWithValue("@WEBPER", WEBPER);
                    cmdM.Parameters.AddWithValue("@REJECTREASON", REJECTREASON);
                    cmdM.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                    cmdM.Parameters.AddWithValue("@BASICPURRATE", BASICPURRATE);
                    cmdM.Parameters.AddWithValue("@PURFKAMT", PURFKAMT);
                    cmdM.Parameters.AddWithValue("@PURAMZAMT", PURAMZAMT);
                    cmdM.Parameters.AddWithValue("@PURWEBAMT", PURWEBAMT);
                    cmdM.Parameters.AddWithValue("@PURFKPER", PURFKPER);
                    cmdM.Parameters.AddWithValue("@PURAMZPER", PURAMZPER);
                    cmdM.Parameters.AddWithValue("@PURWEBPER", PURWEBPER);
                    cmdM.Parameters.AddWithValue("@PURCHASEPERONVENDORPRICE", PURCHASEPERONVENDORPRICE);
                    cmdM.Parameters.AddWithValue("@ISPURAPPROVEDFK", ISPURAPPROVEDFK);
                    cmdM.Parameters.AddWithValue("@ISPURAPPROVEDAMZ", ISPURAPPROVEDAMZ);
                    cmdM.Parameters.AddWithValue("@ISPURAPPROVEDWEB", ISPURAPPROVEDWEB);
                    cmdM.Parameters.AddWithValue("@FinalApproveListingAmount", FinalApproveListingAmount);
                    cmdM.Parameters.AddWithValue("@ACTION", "UPDATEPURCHASE");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int UpdateListedDetail(int ID)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE QC DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@STATUS", 11229);
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@ACTION", "UPDATELIST");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int ProductBulkListed(string productjson, int userid)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE QC DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEXLISTEDUPDATE", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@PRODUCTJSON", productjson);
                    cmdM.Parameters.AddWithValue("@CREATEBY", userid);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int UpdateUnListedDetail(int ID, string userid)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE UNLIST DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@STATUS", 11238);
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@UNLISTBY", Convert.ToInt32(userid));
                    cmdM.Parameters.AddWithValue("@UNLISTEDDATE", setDateFormat(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), true));
                    cmdM.Parameters.AddWithValue("@ACTION", "UPDATEUNLIST");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int UpdateConfirmDetail(int ID, string userid, int ACTUALSTATUS, string NGEAPRV, decimal FinalApproveListingAmount, string FIRSTCREATEDDATE = null)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE UNLIST DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@LISTEDCONFIRMBY", Convert.ToInt32(userid));
                    cmdM.Parameters.AddWithValue("@FIRSTCREATEDDATE1", FIRSTCREATEDDATE);
                    cmdM.Parameters.AddWithValue("@STATUS", ACTUALSTATUS);
                    cmdM.Parameters.AddWithValue("@NGEAPRV", NGEAPRV);
                    cmdM.Parameters.AddWithValue("@FinalApproveListingAmount", FinalApproveListingAmount);
                    cmdM.Parameters.AddWithValue("@ACTION", "UPDATECONFIRM");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int UpdateEachListingReservedDetail(int ID, string userid)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE UNLIST DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@RESERVEDBY", Convert.ToInt32(userid));
                    cmdM.Parameters.AddWithValue("@ACTION", "UPDATERESERVED");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int UpdateBlanccoQcDetail(int ID, string BLANCCORESULT,
            string BLANCCOREMARK, string FINALQCRESULT, string FINALQCREMARK, string IMEINO, string userid,
            string BLANCCOGRADE, decimal PURCHASERATE, int STATUS, string REJECTREASON, string VENDORASSOCVENDCODE
            , byte[] IMAGE, byte[] INVOICE)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE BLANCCO DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@STATUS", STATUS);
                    cmdM.Parameters.AddWithValue("@BLANCCORESULT", BLANCCORESULT);
                    cmdM.Parameters.AddWithValue("@BLANCCOREMARK", BLANCCOREMARK);
                    cmdM.Parameters.AddWithValue("@FINALQCRESULT", FINALQCRESULT);
                    cmdM.Parameters.AddWithValue("@FINALQCREMARK", FINALQCREMARK);
                    cmdM.Parameters.AddWithValue("@IMEINO", IMEINO);
                    cmdM.Parameters.AddWithValue("@BLANCCOQCDATE", setDateFormat(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), true));
                    cmdM.Parameters.AddWithValue("@BLANCCOQCBY", Convert.ToInt32(userid));
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@BLANCCOGRADE", BLANCCOGRADE);
                    cmdM.Parameters.AddWithValue("@PURCHASERATE", PURCHASERATE);
                    cmdM.Parameters.AddWithValue("@REJECTREASON", REJECTREASON);
                    cmdM.Parameters.AddWithValue("@VENDORASSOCVENDCODE", VENDORASSOCVENDCODE);
                    cmdM.Parameters.AddWithValue("@IMEIIMAGE", IMAGE);
                    cmdM.Parameters.AddWithValue("@INVOICEIMAGE", INVOICE);
                    cmdM.Parameters.AddWithValue("@ACTION", "UPDATEBLANCCOQCRESULT");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int UpdateInwardDetail(int ID, string INWARDRESULT,
               string INWARDFAILREASON, string INWARDGRADE, decimal INWARDPURCHASERATE, string userid,
               string INWARDDATE, int STATUS, string REJECTREASON, string ORDERNO, string REFNO, string sono
            , int INWARDEDINVOICE, int INWARDEDBOX, int INWARDEDCHARGER, int INWARDEDCHARGERORIGNAL
            , string INWARDEDCHARGERWALTAGE, string itemcode, string color)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE BLANCCO DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@INWARDRESULT", INWARDRESULT);
                    cmdM.Parameters.AddWithValue("@INWARDFAILREASON", INWARDFAILREASON);
                    cmdM.Parameters.AddWithValue("@INWARDGRADE", INWARDGRADE);
                    cmdM.Parameters.AddWithValue("@INWARDPURCHASERATE", INWARDPURCHASERATE);
                    cmdM.Parameters.AddWithValue("@INWARDDATE", objMainClass.setDateFormat(INWARDDATE, true));
                    cmdM.Parameters.AddWithValue("@INWARDBY", Convert.ToInt32(userid));
                    cmdM.Parameters.AddWithValue("@STATUS", STATUS);
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@REJECTREASON", REJECTREASON);
                    cmdM.Parameters.AddWithValue("@ORDERNO", ORDERNO);
                    cmdM.Parameters.AddWithValue("@REFNO", REFNO);
                    cmdM.Parameters.AddWithValue("@SONO", sono);
                    cmdM.Parameters.AddWithValue("@INWARDEDINVOICE", INWARDEDINVOICE);
                    cmdM.Parameters.AddWithValue("@INWARDEDBOX", INWARDEDBOX);
                    cmdM.Parameters.AddWithValue("@INWARDEDCHARGER", INWARDEDCHARGER);
                    cmdM.Parameters.AddWithValue("@INWARDEDCHARGERORIGNAL", INWARDEDCHARGERORIGNAL);
                    cmdM.Parameters.AddWithValue("@INWARDEDCHARGERWALTAGE", INWARDEDCHARGERWALTAGE);
                    cmdM.Parameters.AddWithValue("@ITEMCODE", itemcode);
                    cmdM.Parameters.AddWithValue("@COLOR", color);
                    cmdM.Parameters.AddWithValue("@ACTION", "UPDATEINWARDRESULT");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public DataTable GetProdSpec(int ID, int BRANDID, int MODELID, string ACTION, int ITEMTYPEID, int ITEMGRPID, int ITEMSUBGRPID
            , string SEARCHTOP10MODEL, String ASIN)
        {
            //SP_MOBEX_SPECDATA
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SPECDATA", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 800;
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@BRAND_ID", BRANDID);
                cmd.Parameters.AddWithValue("@MODEL_ID", MODELID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ITEMTYPEID", ITEMTYPEID);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.Parameters.AddWithValue("@SEARCHTOP10MODEL", SEARCHTOP10MODEL);
                cmd.Parameters.AddWithValue("@ASIN", ASIN);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetMakeModelSuggestPriceHistory(int ID, int BRANDID, int MODELID, string FROMDATE, string TODATE)
        {
            //SP_MOBEX_SPECDATA
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SPECDATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@BRAND_ID", BRANDID);
                cmd.Parameters.AddWithValue("@MODEL_ID", MODELID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ACTION", "SEARCHHISTORY");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateProdSpec(int ID, int BRANDID, string BRANDDESC, int MODELID, string MODELDESC,
            string RAM, string ROM, string COLOR, int ACTIVE, int UPDATEBY, string ACTION, Decimal newRate, Decimal BASICPURRATE
            , string LAUNCHYEAR, int ITEMTYPEID, int ITEMGRPID, int ITEMSUBGRPID, string MODELDIPLAYNAME, Decimal FinalApproveListingAmount
            , int MNTTOPSALESSRNO, int ISINSTANTSELLING, Decimal INSTANTSELLINGAMOUNT, Decimal FinalStockApproveAmount)
        {
            //MST_MAKEMODEL_SPEC
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SPECDATA", ConnSherpa);

            try
            {
                cmd.Parameters.AddWithValue("@BRAND_ID", BRANDID);
                cmd.Parameters.AddWithValue("@BRAND_DESC", BRANDDESC);
                cmd.Parameters.AddWithValue("@MODEL_ID", MODELID);
                cmd.Parameters.AddWithValue("@MODEL_NAME", MODELDESC);
                cmd.Parameters.AddWithValue("@RAMSIZE", RAM);
                cmd.Parameters.AddWithValue("@ROMSIZE", ROM);
                cmd.Parameters.AddWithValue("@COLOR", COLOR);
                cmd.Parameters.AddWithValue("@ACTIVE", ACTIVE);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@NEWRATE", newRate);
                cmd.Parameters.AddWithValue("@BASICPURRATE", BASICPURRATE);
                cmd.Parameters.AddWithValue("@LAUNCHYEAR", LAUNCHYEAR);
                cmd.Parameters.AddWithValue("@ITEMTYPEID", ITEMTYPEID);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.Parameters.AddWithValue("@MODELDIPLAYNAME", MODELDIPLAYNAME);
                cmd.Parameters.AddWithValue("@FinalApproveListingAmount", FinalApproveListingAmount);
                cmd.Parameters.AddWithValue("@MNTTOPSALESSRNO", MNTTOPSALESSRNO);
                cmd.Parameters.AddWithValue("@ISINSTANTSELLING", ISINSTANTSELLING);
                cmd.Parameters.AddWithValue("@INSTANTSELLINGAMOUNT", INSTANTSELLINGAMOUNT);
                cmd.Parameters.AddWithValue("@FinalStockApproveAmount", FinalStockApproveAmount);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iReturn = 1;

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iReturn;
        }

        public int InsertProdSpec(int BRANDID, string BRANDDESC, int MODELID, string MODELDESC, string RAM, string ROM,
            string COLOR, int ACTIVE, int CREATEBY, string ACTION, Decimal newrate, Decimal BASICPURRATE, string LAUNCHYEAR
            , int ITEMTYPEID, int ITEMGRPID, int ITEMSUBGRPID, string MODELDIPLAYNAME, Decimal FinalApproveListingAmount
            , int MNTTOPSALESSRNO, int ISINSTANTSELLING, Decimal INSTANTSELLINGAMOUNT, Decimal FinalStockApproveAmount)
        {
            //MST_MAKEMODEL_SPEC
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SPECDATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BRAND_ID", BRANDID);
                cmd.Parameters.AddWithValue("@BRAND_DESC", BRANDDESC);
                cmd.Parameters.AddWithValue("@MODEL_ID", MODELID);
                cmd.Parameters.AddWithValue("@MODEL_NAME", MODELDESC);
                cmd.Parameters.AddWithValue("@RAMSIZE", RAM);
                cmd.Parameters.AddWithValue("@ROMSIZE", ROM);
                cmd.Parameters.AddWithValue("@COLOR", COLOR);
                cmd.Parameters.AddWithValue("@ACTIVE", ACTIVE);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@NEWRATE", newrate);
                cmd.Parameters.AddWithValue("@BASICPURRATE", BASICPURRATE);
                cmd.Parameters.AddWithValue("@LAUNCHYEAR", LAUNCHYEAR);
                cmd.Parameters.AddWithValue("@ITEMTYPEID", ITEMTYPEID);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.Parameters.AddWithValue("@MODELDIPLAYNAME", MODELDIPLAYNAME);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@FinalApproveListingAmount", FinalApproveListingAmount);
                cmd.Parameters.AddWithValue("@MNTTOPSALESSRNO", MNTTOPSALESSRNO);
                cmd.Parameters.AddWithValue("@ISINSTANTSELLING", ISINSTANTSELLING);
                cmd.Parameters.AddWithValue("@INSTANTSELLINGAMOUNT", INSTANTSELLINGAMOUNT);
                cmd.Parameters.AddWithValue("@FinalStockApproveAmount", FinalStockApproveAmount);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iReturn = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iReturn;
        }


        public DataTable GetProductStatusWiseDetail(string FROMDATE, string TODATE, int status,
            string OLDDAYSFILTER, string MOBEXRATEFILTER, string MOBEXGRADE, int CREATEBY, string CITY)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLERSTATUSREPORT", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@OLDDAYSFILTER", OLDDAYSFILTER);
                cmd.Parameters.AddWithValue("@MOBEXRATEFILTER", MOBEXRATEFILTER);
                cmd.Parameters.AddWithValue("@MOBEXGRADE", MOBEXGRADE);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetMobexSellerListingTrackingDetail(string FROMDATE, string TODATE, int status,
            int CREATEBY)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLERSTATUSTRACKINGREPORT", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetProductReportDetail(string FROMDATE, string TODATE, int status,
           string OLDDAYSFILTER, string MOBEXRATEFILTER, string MOBEXGRADE, int CREATEBY, string MAKE, string MODEL)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLERPRODUCTDETAILREPORT", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@OLDDAYSFILTER", OLDDAYSFILTER);
                cmd.Parameters.AddWithValue("@MOBEXRATEFILTER", MOBEXRATEFILTER);
                cmd.Parameters.AddWithValue("@MOBEXGRADE", MOBEXGRADE);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetProductBikerStatusWiseDetail(string FROMDATE, string TODATE, string vendorname, int status
            , string MODEL, string OLDDAYSFILTER, string MOBEXRATEFILTER, string MOBEXGRADE, int CREATEBY
            , int SMTYPE, int LISTINGTYPE)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLERBIKERSTATUSREPORT", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@VENDORNAME", vendorname);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@OLDDAYSFILTER", OLDDAYSFILTER);
                cmd.Parameters.AddWithValue("@MOBEXRATEFILTER", MOBEXRATEFILTER);
                cmd.Parameters.AddWithValue("@MOBEXGRADE", MOBEXGRADE);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@SMTYPE", SMTYPE);
                cmd.Parameters.AddWithValue("@LISTINGTYPE", LISTINGTYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetProductASMStatusWiseDetail(string FROMDATE, string TODATE, string vendorname, int status
            , string MODEL, int CREATEBY)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLERBIKERASMSTATUSREPORT", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@VENDORNAME", vendorname);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetSOAge(int CMPID, string FROMDATE, string TODATE, string SONO, string MAINQUERY, string AGE)
        {
            //SP_SOAGING
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SOAGING", ConnSherpa);

            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.Parameters.AddWithValue("@AGE", AGE);
                //cmd.Parameters.AddWithValue("@GROUP", GROUPBY);
                //cmd.Parameters.AddWithValue("@SEQUENCE", SEQUENCE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateVendor(int CMPID, string VENDCODE, string ACTION, int STATUS, int UPDATEBY, string REASON)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@VENDCODE", VENDCODE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@REASON", REASON);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;
        }

        public void DeleteVendImage(string VENDCODE, int IMAGETYPE, string ACTION)
        {
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@IMAGETYPE", IMAGETYPE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public string UpdateVendorData(int CMPID, string VENDCODE, string PANNO, string AADHARNO, string GSTNO, int MSME, string IFSCCODE, string BANKNAME, string ACCOUNTNO, int ACCTYPE, int UPIWALLET,
           string UPIPAYNO, string UPIOWNERNAME, int ISACTIVE, byte[] IDPROOF, byte[] PAN, byte[] CHEQUE, byte[] SHOP, byte[] GSTCERTI, byte[] MSMECERTI,
           string IDPROOFTYPE, string PANTYPE, string CHEQUETYPE, string GSTCERTITYPE, string MSMECERTITYPE, int UPDATEBY, string ACTION)
        {
            string iResult = "";
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            VENDCODE = strConvertZeroPadding(VENDCODE);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@VENDCODE", VENDCODE);
                    cmd.Parameters.AddWithValue("@PANNO", PANNO);
                    cmd.Parameters.AddWithValue("@AADHARNO", AADHARNO);
                    cmd.Parameters.AddWithValue("@GSTNO", GSTNO);
                    cmd.Parameters.AddWithValue("@MSME", MSME);
                    cmd.Parameters.AddWithValue("@IFSCCODE", IFSCCODE);
                    cmd.Parameters.AddWithValue("@BANKNAME", BANKNAME);
                    cmd.Parameters.AddWithValue("@ACCOUNTNO", ACCOUNTNO);
                    cmd.Parameters.AddWithValue("@ACCNTTYPE", ACCTYPE);
                    cmd.Parameters.AddWithValue("@UPIWALLET", UPIWALLET);
                    cmd.Parameters.AddWithValue("@WALLTEPAYNO", UPIPAYNO);
                    cmd.Parameters.AddWithValue("@WALLETOWNERNAME", UPIOWNERNAME);
                    cmd.Parameters.AddWithValue("@ISACTIVE", ISACTIVE);
                    cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();




                    if (IDPROOF != null)
                    {
                        DeleteVendImage(VENDCODE, 11315, "DELETEIMAGE");
                        int result1 = InsertVendImage(VENDCODE, 11315, IDPROOF, 2, "INSERTIMAGE", IDPROOFTYPE);
                    }

                    if (PAN != null)
                    {
                        DeleteVendImage(VENDCODE, 11316, "DELETEIMAGE");
                        int result2 = InsertVendImage(VENDCODE, 11316, PAN, 2, "INSERTIMAGE", PANTYPE);
                    }

                    if (CHEQUE != null)
                    {
                        DeleteVendImage(VENDCODE, 11317, "DELETEIMAGE");
                        int result3 = InsertVendImage(VENDCODE, 11317, CHEQUE, 2, "INSERTIMAGE", CHEQUETYPE);
                    }

                    if (SHOP != null)
                    {
                        DeleteVendImage(VENDCODE, 11318, "DELETEIMAGE");
                        int result4 = InsertVendImage(VENDCODE, 11318, SHOP, 2, "INSERTIMAGE", ".jpeg");
                    }

                    if (GSTCERTI != null)
                    {
                        DeleteVendImage(VENDCODE, 11824, "DELETEIMAGE");
                        int result4 = InsertVendImage(VENDCODE, 11824, GSTCERTI, 2, "INSERTIMAGE", GSTCERTITYPE);
                    }

                    if (MSMECERTI != null)
                    {
                        DeleteVendImage(VENDCODE, 11825, "DELETEIMAGE");
                        int result4 = InsertVendImage(VENDCODE, 11825, MSMECERTI, 2, "INSERTIMAGE", MSMECERTITYPE);
                    }


                    scope.Complete();
                    scope.Dispose();
                    iResult = strConvertZeroPadding(VENDCODE);
                }

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }



        public DataTable GetUnregiVendor(int CMPID, string VENDCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@VENDCODE", VENDCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public string InsertVendor(int CMPID, string VENDNAME, string PASSWORD, string VENDTYPE, string TITLE, string NAME1, string NAME2, string SHORTNM, int ADDID, string PDOB,
            string PPOB, string PPROF, string GENDER, string INDKEY, string BUSITYPE, string INDTYPE, int CAPAMT, string CURRCODE, string CUSTCODE, string DELFLAG, string BLKFLAG,
            string AUTHGRP, string RECAC, string PAYTERMS, string PAYMETHOD, string PAYBLKKEY, string SORTFLD, string PAYEEAC, string ACATVEND, string CHECKINV, string OLDACCODE,
            string TDSCOUNTRY, string CSTNO, string CSTDT, string LSTNO, string LSTDT, string STREGNO, string ECCNO, string EXREGNO, string EXRANGE, string EXDIVISION,
            string EXCOMM, string EXVENDTYPE, string PANNO, int VENDCAT, int VENDGRP, int ZONE, string REGION, int CREATEBY, string GSTNO, int ISACTIVE, string BANKNAME,
            string ACCOUNTNO, string IFSCCODE, int UNDERMARGINSCHEME, string ADD1, string ADD2, string ADD3, string CITY, int STCD, string CNCD, string POSTALCODE, string CONTACTPERSON,
            string CONTACTNO, string MOBILENO, string REFTYPE, string ADDOF, byte[] IDPROOF, byte[] PAN, byte[] CHEQUE, byte[] SHOP, int DEALERID,
            string AADHARNO, int ACCTYPE, int UPIWALLET, string WALLTEPAYNO, string WALLETOWNERNAME, int AGREEMENTRCVD, int MOBILESELLINGSCALE, int MSME, byte[] GSTCERTI, byte[] MSMECERTI,
            int TALLY, int TALLYGROUP, string IDPROOFTYPE, string PANTYPE, string CHEQUETYPE, string GSTCERTITYPE, string MSMECERTITYPE, GridView GridViewComm = null)
        {
            string iResult = "";
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                string DOCNO = GETMAXVENDCODE(VENDTYPE, CMPID, "SELECT", "0");

                if (DOCNO != null && DOCNO != string.Empty && DOCNO != "")
                {

                    using (TransactionScope scope = new TransactionScope())
                    {
                        string ADDRESSID = InsertAddress(CMPID, strConvertZeroPadding(DOCNO), REFTYPE, ADDOF, ADD1, ADD2, ADD3, CITY, STCD, CNCD, POSTALCODE, CONTACTPERSON,
                            CONTACTNO, "", "", "", "", MOBILENO, "", "", "", "", "");
                        if (ADDRESSID != null && ADDRESSID != "" && ADDRESSID != string.Empty)
                        {
                            string CURRNO = GETMAXVENDCODE(VENDTYPE, CMPID, "UPDATE", DOCNO);


                            cmd.Parameters.AddWithValue("@CMPID", CMPID);
                            cmd.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(DOCNO));
                            cmd.Parameters.AddWithValue("@VENDNAME", VENDNAME);
                            cmd.Parameters.AddWithValue("@PASSWORD", PASSWORD);
                            cmd.Parameters.AddWithValue("@VENDTYPE", VENDTYPE);
                            cmd.Parameters.AddWithValue("@TITLE", TITLE);
                            cmd.Parameters.AddWithValue("@NAME1", NAME1);
                            cmd.Parameters.AddWithValue("@NAME2", NAME2);
                            cmd.Parameters.AddWithValue("@SHORTNM", SHORTNM);
                            cmd.Parameters.AddWithValue("@ADDID", ADDRESSID);
                            cmd.Parameters.AddWithValue("@PDOB", PDOB == "" ? null : PDOB);
                            cmd.Parameters.AddWithValue("@PPOB", PPOB);
                            cmd.Parameters.AddWithValue("@PPROF", PPROF);
                            cmd.Parameters.AddWithValue("@GENDER", GENDER);
                            cmd.Parameters.AddWithValue("@INDKEY", INDKEY);
                            cmd.Parameters.AddWithValue("@BUSITYPE", BUSITYPE);
                            cmd.Parameters.AddWithValue("@INDTYPE", INDTYPE);
                            cmd.Parameters.AddWithValue("@CAPAMT", CAPAMT);
                            cmd.Parameters.AddWithValue("@CURRCODE", CURRCODE);
                            cmd.Parameters.AddWithValue("@CUSTCODE", CUSTCODE);
                            cmd.Parameters.AddWithValue("@DELFLAG", DELFLAG);
                            cmd.Parameters.AddWithValue("@BLKFLAG", BLKFLAG);
                            cmd.Parameters.AddWithValue("@AUTHGRP", AUTHGRP);
                            cmd.Parameters.AddWithValue("@RECAC", RECAC);
                            cmd.Parameters.AddWithValue("@PAYTERMS", PAYTERMS);
                            cmd.Parameters.AddWithValue("@PAYMETHOD", PAYMETHOD);
                            cmd.Parameters.AddWithValue("@PAYBLKKEY", PAYBLKKEY);
                            cmd.Parameters.AddWithValue("@SORTFLD", SORTFLD);
                            cmd.Parameters.AddWithValue("@PAYEEAC", PAYEEAC);
                            cmd.Parameters.AddWithValue("@ACATVEND", ACATVEND);
                            cmd.Parameters.AddWithValue("@CHECKINV", CHECKINV);
                            cmd.Parameters.AddWithValue("@OLDACCODE", OLDACCODE);
                            cmd.Parameters.AddWithValue("@TDSCOUNTRY", TDSCOUNTRY);
                            cmd.Parameters.AddWithValue("@CSTNO", CSTNO);
                            cmd.Parameters.AddWithValue("@CSTDT", CSTDT == "" ? null : CSTDT);
                            cmd.Parameters.AddWithValue("@LSTNO", LSTNO);
                            cmd.Parameters.AddWithValue("@LSTDT", LSTDT == "" ? null : LSTDT);
                            cmd.Parameters.AddWithValue("@STREGNO", STREGNO);
                            cmd.Parameters.AddWithValue("@ECCNO", ECCNO);
                            cmd.Parameters.AddWithValue("@EXREGNO", EXREGNO);
                            cmd.Parameters.AddWithValue("@EXRANGE", EXRANGE);
                            cmd.Parameters.AddWithValue("@EXDIVISION", EXDIVISION);
                            cmd.Parameters.AddWithValue("@EXCOMM", EXCOMM);
                            cmd.Parameters.AddWithValue("@EXVENDTYPE", EXVENDTYPE);
                            cmd.Parameters.AddWithValue("@PANNO", PANNO);
                            cmd.Parameters.AddWithValue("@VENDCAT", VENDCAT);
                            cmd.Parameters.AddWithValue("@VENDGRP", VENDGRP);
                            cmd.Parameters.AddWithValue("@ZONE", ZONE);
                            cmd.Parameters.AddWithValue("@REGION", REGION);
                            cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                            cmd.Parameters.AddWithValue("@GSTNO", GSTNO);
                            cmd.Parameters.AddWithValue("@ISACTIVE", ISACTIVE);
                            cmd.Parameters.AddWithValue("@BANKNAME", BANKNAME);
                            cmd.Parameters.AddWithValue("@ACCOUNTNO", ACCOUNTNO);
                            cmd.Parameters.AddWithValue("@IFSCCODE", IFSCCODE);
                            cmd.Parameters.AddWithValue("@UNDERMARGINSCHEME", UNDERMARGINSCHEME);
                            cmd.Parameters.AddWithValue("@ACTION", "INSERT");
                            cmd.Parameters.AddWithValue("@DEALERID", DEALERID);

                            cmd.Parameters.AddWithValue("@AADHARNO", AADHARNO);
                            cmd.Parameters.AddWithValue("@ACCNTTYPE", ACCTYPE);
                            cmd.Parameters.AddWithValue("@UPIWALLET", UPIWALLET);
                            cmd.Parameters.AddWithValue("@WALLTEPAYNO", WALLTEPAYNO);
                            cmd.Parameters.AddWithValue("@WALLETOWNERNAME", WALLETOWNERNAME);
                            cmd.Parameters.AddWithValue("@AGREEMENTRCVD", AGREEMENTRCVD);
                            cmd.Parameters.AddWithValue("@MOBILESELLINGSCALE", MOBILESELLINGSCALE);

                            cmd.Parameters.AddWithValue("@MSME", MSME);
                            cmd.Parameters.AddWithValue("@TALLYVENDOR", TALLY);
                            cmd.Parameters.AddWithValue("@TALLYGROUP", TALLYGROUP);

                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();


                            if (GridViewComm.Rows.Count > 0)
                            {
                                for (int i = 0; i < GridViewComm.Rows.Count; i++)
                                {
                                    GridViewRow row = GridViewComm.Rows[i];

                                    InsertCommunication(intCmpId, i + 1, DOCNO, Convert.ToInt32(((Label)row.FindControl("lblDesignation")).Text),
                                        Convert.ToString(((Label)row.FindControl("lblName")).Text), Convert.ToString(((Label)row.FindControl("lblContact")).Text),
                                        Convert.ToString(((Label)row.FindControl("lblEmail")).Text), CREATEBY, "INSERT", "VEND");
                                }
                            }


                            if (IDPROOF != null)
                            {
                                int result1 = InsertVendImage(DOCNO, 11315, IDPROOF, 2, "INSERTIMAGE", IDPROOFTYPE);
                            }

                            if (PAN != null)
                            {
                                int result2 = InsertVendImage(DOCNO, 11316, PAN, 2, "INSERTIMAGE", PANTYPE);
                            }

                            if (CHEQUE != null)
                            {
                                int result3 = InsertVendImage(DOCNO, 11317, CHEQUE, 2, "INSERTIMAGE", CHEQUETYPE);
                            }

                            if (SHOP != null)
                            {
                                int result4 = InsertVendImage(DOCNO, 11318, SHOP, 2, "INSERTIMAGE", ".jpeg");
                            }

                            if (GSTCERTI != null)
                            {
                                int result4 = InsertVendImage(DOCNO, 11824, GSTCERTI, 2, "INSERTIMAGE", GSTCERTITYPE);
                            }

                            if (MSMECERTI != null)
                            {
                                int result4 = InsertVendImage(DOCNO, 11825, MSMECERTI, 2, "INSERTIMAGE", MSMECERTITYPE);
                            }

                            scope.Complete();
                            scope.Dispose();
                            iResult = DOCNO;


                        }
                        else
                        {
                            iResult = "";
                        }
                    }
                }
                else
                {
                    iResult = "";
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;
        }


        public DataTable GetImageByID(int ID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public void InsertCommunication(int CMPID, int SRNO, string VENDCODE, int DESIGNATION, string CONTNAME, string CONTNO, string CONTEMAIL, int USERID, string ACTION, string CUSTTYPE)
        {
            SqlCommand cmd = new SqlCommand("SP_PARTY_COMMUNICATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SRNO", SRNO);
                cmd.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@DESIGNATION", DESIGNATION);
                cmd.Parameters.AddWithValue("@CONTNAME", CONTNAME);
                cmd.Parameters.AddWithValue("@CONTNO", CONTNO);
                cmd.Parameters.AddWithValue("@EMAILID", CONTEMAIL);
                cmd.Parameters.AddWithValue("@CREATEBY", USERID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@CUSTTYPE", CUSTTYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public DataTable GetDealer(int CMPID, string ACTION, string DEALERNAME, string CATEGORY, int STATUS, string FROMDATE, string TODATE)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DEALER_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@DEALERNAME", DEALERNAME);
                cmd.Parameters.AddWithValue("@CATEGORY", CATEGORY);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable FetchDealerData(int ID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DEALER_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }


            return dt;
        }

        public string GETMAXVENDCODE(string TYPE, int CMPID, string ACTION, string CURRNO)
        {
            //SP_GET_MAX_VENDCODE
            string MAXVENDCODE = string.Empty;
            SqlCommand cmd = new SqlCommand("SP_GET_MAX_VENDCODE", ConnSherpa);
            try
            {
                if (ACTION == "SELECT")
                {
                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@VENDTYPE", TYPE);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.Parameters.AddWithValue("@CURRNO", CURRNO);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();


                    object obj = cmd.ExecuteScalar();
                    if ((obj) != null)
                    {
                        MAXVENDCODE = obj.ToString();
                        MAXVENDCODE = Convert.ToString(Convert.ToInt32(MAXVENDCODE) + 1);
                    }
                    cmd.Connection.Close();
                }
                if (ACTION == "UPDATE")
                {
                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@VENDTYPE", TYPE);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.Parameters.AddWithValue("@CURRNO", CURRNO);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    MAXVENDCODE = CURRNO;

                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return MAXVENDCODE;
        }

        public int InsertVendImage(string VENDCODE, int IMAGETYPE, byte[] IMAGE, int STATUS, string ACTION, string EXTENSION)
        {
            //INSERTIMAGE
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@IMAGETYPE", IMAGETYPE);
                cmd.Parameters.AddWithValue("@IMAGE", IMAGE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@EXTENSION", EXTENSION);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public string InsertAddress(int CMPID, string REFID, string REFTYPE, string ADDOF, string ADDR1, string ADDR2, string ADDR3, string CITY, int STCD, string CNCD, string POSTALCODE,
           string CONTACTPERSON, string CONTACTNO, string CONTACTPERSON2, string CONTACTNO2, string CONTACTPERSON3, string CONTACTNO3, string MOBILENO, string MOBILENO2,
           string MOBILENO3, string FAXNO, string EMAILID, string WEBSITE)
        {
            //SP_MST_ADDRESS
            string ADDID;
            SqlCommand cmd = new SqlCommand("SP_MST_ADDRESS", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@REFID", REFID);
                cmd.Parameters.AddWithValue("@REFTYPE", REFTYPE);
                cmd.Parameters.AddWithValue("@ADDOF", ADDOF);
                cmd.Parameters.AddWithValue("@ADDR1", ADDR1);
                cmd.Parameters.AddWithValue("@ADDR2", ADDR2);
                cmd.Parameters.AddWithValue("@ADDR3", ADDR3);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                cmd.Parameters.AddWithValue("@STCD", STCD);
                cmd.Parameters.AddWithValue("@CNCD", CNCD);
                cmd.Parameters.AddWithValue("@POSTALCODE", POSTALCODE);
                cmd.Parameters.AddWithValue("@CONTACTPERSON", CONTACTPERSON);
                cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                cmd.Parameters.AddWithValue("@CONTACTPERSON2", CONTACTPERSON2);
                cmd.Parameters.AddWithValue("@CONTACTNO2", CONTACTNO2);
                cmd.Parameters.AddWithValue("@CONTACTPERSON3", CONTACTPERSON3);
                cmd.Parameters.AddWithValue("@CONTACTNO3", CONTACTNO3);
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                cmd.Parameters.AddWithValue("@MOBILENO2", MOBILENO2);
                cmd.Parameters.AddWithValue("@MOBILENO3", MOBILENO3);
                cmd.Parameters.AddWithValue("@FAXNO", FAXNO);
                cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
                cmd.Parameters.AddWithValue("@WEBSITE", WEBSITE);
                cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                ADDID = Convert.ToString((cmd.Parameters["@ID"].Value));
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return ADDID;
        }

        public int ProductBulkApproveRejectUpdate(string productjson)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE APPROVED REJECT DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEXSELLERLISTEDDETAILUPDATE", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@MOBEXSELLERPRODUCTDETAILJSON", productjson);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.CommandTimeout = 5000;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }


        public DataTable GetBikerVisitReport(string ACTION, string FROMDATE, string TODATE, int USERID, int SEARCHBYID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DAILY_VISIT_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@SEARCHBYID", SEARCHBYID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 800;
                cmd.Connection.Open();
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //da.Fill(dt);

                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);


                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetBikerRejectionReport(string ACTION, string FROMDATE, string TODATE, int SEARCHBY, string CITY)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_REJECTION_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SEARCHBY", SEARCHBY);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetBikerVisitFeedBackReport(string ACTION, string FROMDATE, string TODATE, int SEARCHBY, int REQBYSYSTEM, string CITY)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BDO_DAILY_ACTIVITY_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SEARCHBY", SEARCHBY);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                //cmd.Parameters.AddWithValue("@REQBYSYSTEM", REQBYSYSTEM);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public int SaveVendorVisitEntry(string VENDORNAME, string VISIFEEDBACK, int VISIUSERID, int VENDORID)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region Insert PRODUCT ENTRY...
                    SqlCommand cmdM = new SqlCommand("MOBEXSELLERVENDORVISITDETAILCRUDOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                    cmdM.Parameters.AddWithValue("@VISIFEEDBACK", VISIFEEDBACK);
                    cmdM.Parameters.AddWithValue("@VISIUSERID", VISIUSERID);
                    cmdM.Parameters.AddWithValue("@VENDORID", VENDORID);
                    cmdM.Parameters.AddWithValue("@ACTION", "ADD");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }



        public int InsertDealer(int CMPID, string DEALERNAME, int CATEGORY, string ADDR1, string ADDR2, string ADDR3,
            string CITY, int STCD, string CNCD, string POSTALCODE, byte[] SHOPIMAGE, int CREATEBY, string ACTION,
            int STATUS, string AREA, int BIKERID, string contactno, string contactno2, string contactno3, string image, int ISKRO, int JANGADMAXDAYLIMIT)
        {
            //SP_DEALER_MASTER
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_DEALER_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DEALERNAME", DEALERNAME);
                cmd.Parameters.AddWithValue("@CATEGORY", CATEGORY);
                cmd.Parameters.AddWithValue("@ADDR1", ADDR1);
                cmd.Parameters.AddWithValue("@ADDR2", ADDR2);
                cmd.Parameters.AddWithValue("@ADDR3", ADDR3);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                cmd.Parameters.AddWithValue("@STCD", STCD);
                cmd.Parameters.AddWithValue("@CNCD", CNCD);
                cmd.Parameters.AddWithValue("@POSTALCODE", POSTALCODE);
                cmd.Parameters.AddWithValue("@SHOPIMAGE", SHOPIMAGE);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@AREA", AREA);
                cmd.Parameters.AddWithValue("@CONTACTNO", contactno);
                cmd.Parameters.AddWithValue("@CONTACTNO2", contactno2);
                cmd.Parameters.AddWithValue("@CONTACTNO3", contactno3);
                cmd.Parameters.AddWithValue("@BIKERID", BIKERID);
                cmd.Parameters.AddWithValue("@ISKRO", ISKRO);
                cmd.Parameters.AddWithValue("@JANGADMAXDAYLIMIT", JANGADMAXDAYLIMIT);
                //cmd.Parameters.AddWithValue("@SHOPIMAGE", image);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public DataTable GetVendorReport(int CMPID, string FROMDATE, string TODATE, string VENDCODE, int VENDCAT, string VENDTYPE, int ISACTIVE, int MARGINSCEME, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@VENDCODE", VENDCODE == "" ? VENDCODE : strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@VENDCAT", VENDCAT);
                cmd.Parameters.AddWithValue("@VENDTYPE", VENDTYPE);
                cmd.Parameters.AddWithValue("@ISACTIVE", ISACTIVE);
                cmd.Parameters.AddWithValue("@UNDERMARGINSCHEME", MARGINSCEME);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetProductRecomendedRate(string MAKE, string MODEL, string RAM, string ROM, string GRADE, string COLOR)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_PRODUCTRECOMENDEDRATE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@RAM", RAM);
                cmd.Parameters.AddWithValue("@ROM", ROM);
                cmd.Parameters.AddWithValue("@GRADE", GRADE);
                cmd.Parameters.AddWithValue("@COLOR", COLOR);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetMobileNewRate(string MAKE, string MODEL, string RAM, string ROM)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_GETNEWRATE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@RAM", RAM);
                cmd.Parameters.AddWithValue("@ROM", ROM);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetLabelData(int CMPID, string SEGMENT, int JOBSTATUS, string SERIALNO, string JOBID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LABEL_PRINT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@JOBSTATUS", JOBSTATUS);
                cmd.Parameters.AddWithValue("@SERIALNO", SERIALNO);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }


        public DataTable GetItemDetails(int ITEMGRP, int ITEMSUBGRP, string Mode)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLERITEMCODEOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ITEMGRP", ITEMGRP);
                cmd.Parameters.AddWithValue("@ITEMSUBGRP", ITEMSUBGRP);
                cmd.Parameters.AddWithValue("@Action", Mode);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetItemMaxCode(int ITEMGRP, int ITEMSUBGRP, string Mode)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLERITEMCODEOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ITEMGRP", ITEMGRP);
                cmd.Parameters.AddWithValue("@ITEMSUBGRP", ITEMSUBGRP);
                cmd.Parameters.AddWithValue("@Action", Mode);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public int INSERTITEMDETAIL(int CMPID,
            string ITEMCODE, int CREATEBY, int ITEMCAT, int ITEMGRP, int ITEMSUBGRP, int SKU,
            int ISSUEUNIT, Decimal CONVFACT, int ITEMTYPE, Decimal MRATE, int VALTYPE, int VALCLASSID, string ITEMDESC,
            string DISPNAME, int PHOTOID, string MAKE, string MODEL, int HSNGRP, int STATUS)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE QC DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEXSELLERITEMCODEOPERATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdM.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                    cmdM.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmdM.Parameters.AddWithValue("@ITEMCAT", ITEMCAT);
                    cmdM.Parameters.AddWithValue("@ITEMGRP", ITEMGRP);
                    cmdM.Parameters.AddWithValue("@ITEMSUBGRP", ITEMSUBGRP);
                    cmdM.Parameters.AddWithValue("@SKU", SKU);
                    cmdM.Parameters.AddWithValue("@ISSUEUNIT", ISSUEUNIT);
                    cmdM.Parameters.AddWithValue("@CONVFACT", CONVFACT);
                    cmdM.Parameters.AddWithValue("@ITEMTYPE", ITEMTYPE);
                    cmdM.Parameters.AddWithValue("@MRATE", MRATE);
                    cmdM.Parameters.AddWithValue("@VALTYPE", VALTYPE);
                    cmdM.Parameters.AddWithValue("@VALCLASSID", VALCLASSID);
                    cmdM.Parameters.AddWithValue("@ITEMDESC", ITEMDESC);
                    cmdM.Parameters.AddWithValue("@DISPNAME", DISPNAME);
                    cmdM.Parameters.AddWithValue("@PHOTOID", PHOTOID);
                    cmdM.Parameters.AddWithValue("@MAKE", MAKE);
                    cmdM.Parameters.AddWithValue("@MODEL", MODEL);
                    cmdM.Parameters.AddWithValue("@HSNGRP", HSNGRP);
                    cmdM.Parameters.AddWithValue("@STATUS", STATUS);
                    cmdM.Parameters.AddWithValue("@ACTION", "ADD");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        //VENDOR PURCHASE DETAIL SYSTEM 


        //CRM FOR CALL CENTER 
        public string SaveSO(int CMPID, string SOTYPE, string SONO, string SODATE, string SEGMENT, string DISTCHNL, string BILLTOCODE, string SHIPTOCODE, string PMTTERMS, string REMARK, int STATUS,
                string NETMATVALUE, string NETTAXAMT, string DISCOUNT, string NETSOAMT, int CREATEBY, string REFNO, string REFDT, int SALESFROM, string CUSTNAME, string CUSTADD1,
                string CUSTADD2, string CUSTADD3, string CITY, int STATEID, string PINCODE, string CUSTMOBILENO, string CUSTEMAILID, string JOBID, string COMMAGENT, int SCHEMEID,
                GridView ITEMDETAILS, GridView TAXDETAILS, GridView CHARGEDETAILS, int PAYMODE, string PREPAIDAMT, string REMAINAMT, string GSTNO, string TXNNO, string TXNDT, int PAYGATEWAY, string OLDSONO)
        {
            string SO = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    SO = MAXPRNO(SOTYPE, "SO");
                    SO = strConvertZeroPadding(SO);


                    #region Update MMNORANGE...
                    SqlCommand POcmd = new SqlCommand("SP_UPDATE_MMNORANGE_BY_TRANTYPE", ConnSherpa);
                    POcmd.Parameters.AddWithValue("@CMPID", CMPID);
                    POcmd.Parameters.AddWithValue("@CURRNO", strConvertZeroPadding(SO));
                    POcmd.Parameters.AddWithValue("@TRANTYPE", SOTYPE);
                    POcmd.Parameters.AddWithValue("@DOCTYPE", "SO");
                    POcmd.CommandType = CommandType.StoredProcedure;
                    POcmd.Connection.Open();
                    POcmd.ExecuteNonQuery();
                    POcmd.Connection.Close();
                    #endregion

                    #region SO Master Table Insert...
                    SqlCommand cmdM = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdM.Parameters.AddWithValue("@SOTYPE", SOTYPE);
                    cmdM.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SO));
                    cmdM.Parameters.AddWithValue("@SODT", setDateFormat(SODATE, true));
                    cmdM.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                    cmdM.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
                    cmdM.Parameters.AddWithValue("@BILLTOCODE", strConvertZeroPadding(BILLTOCODE));
                    cmdM.Parameters.AddWithValue("@SHIPTOCODE", strConvertZeroPadding(SHIPTOCODE));
                    cmdM.Parameters.AddWithValue("@PMTTERMS", PMTTERMS);
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@STATUS", STATUS);
                    cmdM.Parameters.AddWithValue("@NETMATVALUE", NETMATVALUE);
                    cmdM.Parameters.AddWithValue("@NETTAXAMT", NETTAXAMT);
                    cmdM.Parameters.AddWithValue("@DISCOUNT", DISCOUNT);
                    cmdM.Parameters.AddWithValue("@NETSOAMT", NETSOAMT);
                    cmdM.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmdM.Parameters.AddWithValue("@REFNO", REFNO);
                    cmdM.Parameters.AddWithValue("@REFDT", setDateFormat(REFDT, true));
                    cmdM.Parameters.AddWithValue("@SALESFROM", SALESFROM);
                    cmdM.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                    cmdM.Parameters.AddWithValue("@CUSTADD1", CUSTADD1);
                    cmdM.Parameters.AddWithValue("@CUSTADD2", CUSTADD2);
                    cmdM.Parameters.AddWithValue("@CUSTADD3", CUSTADD3);
                    cmdM.Parameters.AddWithValue("@CITY", CITY);
                    cmdM.Parameters.AddWithValue("@STATEID", STATEID);
                    cmdM.Parameters.AddWithValue("@PINCODE", PINCODE);
                    cmdM.Parameters.AddWithValue("@CUSTMOBILENO", CUSTMOBILENO);
                    cmdM.Parameters.AddWithValue("@CUSTEMAILID", CUSTEMAILID);
                    cmdM.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                    cmdM.Parameters.AddWithValue("@COMMAGENT", COMMAGENT);
                    cmdM.Parameters.AddWithValue("@SCHEMEID", SCHEMEID);
                    cmdM.Parameters.AddWithValue("@PAYMODE", PAYMODE);

                    cmdM.Parameters.AddWithValue("@PREPAIDAMT", PREPAIDAMT);
                    cmdM.Parameters.AddWithValue("@REMAINAMT", REMAINAMT);

                    cmdM.Parameters.AddWithValue("@GSTNO", GSTNO);
                    cmdM.Parameters.AddWithValue("@TXNNO", TXNNO);
                    cmdM.Parameters.AddWithValue("@TXNDT", TXNDT == "" ? "" : setDateFormat(TXNDT, true));
                    cmdM.Parameters.AddWithValue("@PAYGATEWAY", PAYGATEWAY);

                    cmdM.Parameters.AddWithValue("@OLDSONO", OLDSONO);

                    cmdM.Parameters.AddWithValue("@ACTION", "SOMSTINSERT");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion

                    #region SO Detail Table Insert...
                    for (int i = 0; i < ITEMDETAILS.Rows.Count; i++)
                    {
                        GridViewRow row = ITEMDETAILS.Rows[i];

                        SqlCommand cmdI = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                        cmdI.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdI.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SO));
                        cmdI.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblGVID")).Text);
                        cmdI.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblGVItemId")).Text);
                        cmdI.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblGVItemDesc")).Text);
                        cmdI.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblGVPlantID")).Text);
                        cmdI.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblGVLocationCDID")).Text);
                        cmdI.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGVGroupId")).Text);
                        cmdI.Parameters.AddWithValue("@SOQTY", ((Label)row.FindControl("lblGVQty")).Text);
                        cmdI.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblGVUOMID")).Text);
                        cmdI.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblGVRate")).Text);
                        cmdI.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblGVAmount")).Text);
                        cmdI.Parameters.AddWithValue("@DISCAMT", ((Label)row.FindControl("lblGVDiscount")).Text);
                        cmdI.Parameters.AddWithValue("@DELIDT", setDateFormat(Convert.ToString(((Label)row.FindControl("lblGVDeliDate")).Text), true));
                        cmdI.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGVGLCode")).Text);
                        cmdI.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblGVCostCenter")).Text);
                        cmdI.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblGVProfitCenter")).Text);
                        cmdI.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblGVRemarks")).Text);
                        cmdI.Parameters.AddWithValue("@TAXAMT", "0.00");
                        cmdI.Parameters.AddWithValue("@CUSTPARTNO", ((Label)row.FindControl("lblGVCUSTPARTNO")).Text);
                        cmdI.Parameters.AddWithValue("@CUSTPARTDESC", ((Label)row.FindControl("lblGVCUSTPARTDESC")).Text);
                        cmdI.Parameters.AddWithValue("@CUSTPARTDESC2", ((Label)row.FindControl("lblGVIMEI")).Text);
                        //cmdI.Parameters.AddWithValue("@OLDITEMID", "");
                        cmdI.Parameters.AddWithValue("@CHANGEREASON", "");
                        cmdI.Parameters.AddWithValue("@PRODGRADE", ((Label)row.FindControl("lblGVGrade")).Text);
                        cmdI.Parameters.AddWithValue("@JOBID", ((Label)row.FindControl("lblGVTrackNo")).Text);
                        cmdI.Parameters.AddWithValue("@SCHEMEID", SCHEMEID);
                        cmdI.Parameters.AddWithValue("@LOCKAMT", ((Label)row.FindControl("lblLockAmt")).Text);
                        cmdI.Parameters.AddWithValue("@ACTION", "SODTLINSERT");
                        cmdI.CommandType = CommandType.StoredProcedure;
                        cmdI.Connection.Open();
                        cmdI.ExecuteNonQuery();
                        cmdI.Connection.Close();

                        #region JOB Data Change...
                        string NEWJOBID = ((Label)row.FindControl("lblGVTrackNo")).Text;
                        if (NEWJOBID != null && NEWJOBID != "" && NEWJOBID != string.Empty && Convert.ToInt32(NEWJOBID) > 0)
                        {
                            SqlCommand cmdA = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                            cmdA.Parameters.AddWithValue("@CUSTADD1", CUSTADD1);
                            cmdA.Parameters.AddWithValue("@CUSTADD2", CUSTADD2);
                            cmdA.Parameters.AddWithValue("@CUSTADD3", CUSTADD3);
                            cmdA.Parameters.AddWithValue("@CITY", CITY);
                            cmdA.Parameters.AddWithValue("@STATEID", STATEID);
                            cmdA.Parameters.AddWithValue("@PINCODE", PINCODE);
                            cmdA.Parameters.AddWithValue("@CUSTMOBILENO", CUSTMOBILENO);
                            cmdA.Parameters.AddWithValue("@CUSTEMAILID", CUSTEMAILID);
                            cmdA.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(NEWJOBID));
                            cmdA.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                            cmdA.Parameters.AddWithValue("@UPDATEBY", CREATEBY);
                            cmdA.Parameters.AddWithValue("@ACTION", "UPDATEJOB");
                            cmdA.CommandType = CommandType.StoredProcedure;
                            cmdA.Connection.Open();
                            cmdA.ExecuteNonQuery();
                            cmdA.Connection.Close();
                        }
                        #endregion

                    }
                    #endregion

                    #region  SO TAX Table Insert...
                    for (int j = 0; j < TAXDETAILS.Rows.Count; j++)
                    {
                        GridViewRow row = TAXDETAILS.Rows[j];

                        SqlCommand cmdT = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                        cmdT.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdT.Parameters.AddWithValue("@CONDORDER", ((Label)row.FindControl("lblTaxSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SO));
                        cmdT.Parameters.AddWithValue("@SOSRNO", ((Label)row.FindControl("lblTaxPOSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@CONDID", ((Label)row.FindControl("lblCONDID")).Text);
                        cmdT.Parameters.AddWithValue("@CONDTYPE", ((Label)row.FindControl("lblTaxCondType")).Text);
                        cmdT.Parameters.AddWithValue("@GLCODE", "");
                        cmdT.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblTaxRate")).Text);
                        cmdT.Parameters.AddWithValue("@BASEAMT", ((Label)row.FindControl("lblTaxBaseAmount")).Text);
                        cmdT.Parameters.AddWithValue("@PID", ((Label)row.FindControl("lblPID")).Text);
                        cmdT.Parameters.AddWithValue("@TAXAMT", ((Label)row.FindControl("lblTaxAmount")).Text);
                        cmdT.Parameters.AddWithValue("@OPERATOR", ((Label)row.FindControl("lblTaxOperator")).Text);
                        cmdT.Parameters.AddWithValue("@ACTION", "SOCONDINSERT");
                        cmdT.CommandType = CommandType.StoredProcedure;
                        cmdT.Connection.Open();
                        cmdT.ExecuteNonQuery();
                        cmdT.Connection.Close();
                    }
                    #endregion

                    #region SO Charges Table Insert...
                    for (int k = 0; k < CHARGEDETAILS.Rows.Count; k++)
                    {
                        GridViewRow row = CHARGEDETAILS.Rows[k];

                        SqlCommand cmdT = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                        cmdT.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdT.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblChrgSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SO));
                        cmdT.Parameters.AddWithValue("@CHGTYPE", ((Label)row.FindControl("lblChrgCondType")).Text);
                        cmdT.Parameters.AddWithValue("@CHGAMT", ((Label)row.FindControl("lblChrgAmount")).Text);
                        cmdT.Parameters.AddWithValue("@ACTION", "SOCHGINSERT");
                        cmdT.CommandType = CommandType.StoredProcedure;
                        cmdT.Connection.Open();
                        cmdT.ExecuteNonQuery();
                        cmdT.Connection.Close();
                    }
                    #endregion




                    scope.Complete();
                    scope.Dispose();
                    return SO;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public int UpdateLeadInqNo(int LEADID, string INQNO, int STATUS, int UPDATEBY, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@INQNO", INQNO);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@LEADID", LEADID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;
        }

        public int UpdateSalesReturn(int CMPID, string SONO, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_SO_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public int UpdateSODeviation(int CMPID, int STATUS, string REASON, int APRVBY, int ID, string ACTION, int NEWITEMID, string NEWITEMDESC, string NEWGRADE, int OLDITEMID, string REMARKS, string SONO, int SRNO)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_SO_DEVIATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@REASONTOAPRVCANCEL", REASON);
                cmd.Parameters.AddWithValue("@APRVBY", APRVBY);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                if (STATUS == 35)
                {
                    SqlCommand cmdSO = new SqlCommand("SP_SO_DEVIATION", ConnSherpa);
                    cmdSO.Parameters.AddWithValue("@NEWITEMID", NEWITEMID);
                    cmdSO.Parameters.AddWithValue("@NEWITEMDESC", NEWITEMDESC);
                    cmdSO.Parameters.AddWithValue("@NEWGRADE", NEWGRADE);
                    cmdSO.Parameters.AddWithValue("@OLDITEMID", OLDITEMID);
                    cmdSO.Parameters.AddWithValue("@REMARKS", REMARKS);
                    cmdSO.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SONO));
                    cmdSO.Parameters.AddWithValue("@SRNO", SRNO);
                    cmdSO.Parameters.AddWithValue("@ACTION", "UPDATESOITEM");
                    cmdSO.CommandType = CommandType.StoredProcedure;
                    cmdSO.Connection.Open();
                    cmdSO.ExecuteNonQuery();
                    cmdSO.Connection.Close();
                }
                iResult = 1;

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }


        public string UpdateSO(int CMPID, string SOTYPE, string SONO, string SODATE, string SEGMENT, string DISTCHNL, string BILLTOCODE, string SHIPTOCODE, string PMTTERMS, string REMARK, int STATUS,
             string NETMATVALUE, string NETTAXAMT, string DISCOUNT, string NETSOAMT, int CREATEBY, string REFNO, string REFDT, int SALESFROM, string CUSTNAME, string CUSTADD1,
             string CUSTADD2, string CUSTADD3, string CITY, int STATEID, string PINCODE, string CUSTMOBILENO, string CUSTEMAILID, string JOBID, string COMMAGENT, int SCHEMEID,
             GridView ITEMDETAILS, GridView TAXDETAILS, GridView CHARGEDETAILS, int PAYMODE, string PREPAIDAMT, string REMAINAMT, string GSTNO, string TXNNO, string TXNDT, int PAYGATEWAY, string OLDSONO)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    #region SO Master Table Update...
                    SqlCommand cmdM = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdM.Parameters.AddWithValue("@SOTYPE", SOTYPE);
                    cmdM.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SONO));
                    cmdM.Parameters.AddWithValue("@SODT", setDateFormat(SODATE, true));
                    cmdM.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                    cmdM.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
                    cmdM.Parameters.AddWithValue("@BILLTOCODE", strConvertZeroPadding(BILLTOCODE));
                    cmdM.Parameters.AddWithValue("@SHIPTOCODE", strConvertZeroPadding(SHIPTOCODE));
                    cmdM.Parameters.AddWithValue("@PMTTERMS", PMTTERMS);
                    cmdM.Parameters.AddWithValue("@REMARK", REMARK);
                    cmdM.Parameters.AddWithValue("@STATUS", STATUS);
                    cmdM.Parameters.AddWithValue("@NETMATVALUE", NETMATVALUE);
                    cmdM.Parameters.AddWithValue("@NETTAXAMT", NETTAXAMT);
                    cmdM.Parameters.AddWithValue("@DISCOUNT", DISCOUNT);
                    cmdM.Parameters.AddWithValue("@NETSOAMT", NETSOAMT);
                    cmdM.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmdM.Parameters.AddWithValue("@REFNO", REFNO);
                    cmdM.Parameters.AddWithValue("@REFDT", setDateFormat(REFDT, true));
                    cmdM.Parameters.AddWithValue("@SALESFROM", SALESFROM);
                    cmdM.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                    cmdM.Parameters.AddWithValue("@CUSTADD1", CUSTADD1);
                    cmdM.Parameters.AddWithValue("@CUSTADD2", CUSTADD2);
                    cmdM.Parameters.AddWithValue("@CUSTADD3", CUSTADD3);
                    cmdM.Parameters.AddWithValue("@CITY", CITY);
                    cmdM.Parameters.AddWithValue("@STATEID", STATEID);
                    cmdM.Parameters.AddWithValue("@PINCODE", PINCODE);
                    cmdM.Parameters.AddWithValue("@CUSTMOBILENO", CUSTMOBILENO);
                    cmdM.Parameters.AddWithValue("@CUSTEMAILID", CUSTEMAILID);
                    cmdM.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                    cmdM.Parameters.AddWithValue("@COMMAGENT", COMMAGENT);
                    cmdM.Parameters.AddWithValue("@SCHEMEID", SCHEMEID);
                    cmdM.Parameters.AddWithValue("@PAYMODE", PAYMODE);

                    cmdM.Parameters.AddWithValue("@PREPAIDAMT", PREPAIDAMT);
                    cmdM.Parameters.AddWithValue("@REMAINAMT", REMAINAMT);

                    cmdM.Parameters.AddWithValue("@GSTNO", GSTNO == "" ? null : GSTNO);
                    cmdM.Parameters.AddWithValue("@TXNNO", TXNNO == "" ? null : TXNNO);
                    cmdM.Parameters.AddWithValue("@TXNDT", TXNDT == "" ? null : TXNDT);
                    cmdM.Parameters.AddWithValue("@PAYGATEWAY", PAYGATEWAY);

                    cmdM.Parameters.AddWithValue("@OLDSONO", OLDSONO);

                    cmdM.Parameters.AddWithValue("@ACTION", "SOMSTUPDATE");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion

                    #region SO Detail Table Update...
                    SqlCommand cmdDD = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                    cmdDD.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SONO));
                    cmdDD.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdDD.Parameters.AddWithValue("@ACTION", "SODETAILSDELETE");
                    cmdDD.CommandType = CommandType.StoredProcedure;
                    cmdDD.Connection.Open();
                    cmdDD.ExecuteNonQuery();
                    cmdDD.Connection.Close();


                    for (int i = 0; i < ITEMDETAILS.Rows.Count; i++)
                    {
                        GridViewRow row = ITEMDETAILS.Rows[i];

                        SqlCommand cmdI = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                        cmdI.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdI.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SONO));
                        cmdI.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblGVID")).Text);
                        cmdI.Parameters.AddWithValue("@ITEMID", ((Label)row.FindControl("lblGVItemId")).Text);
                        cmdI.Parameters.AddWithValue("@ITEMDESC", ((Label)row.FindControl("lblGVItemDesc")).Text);
                        cmdI.Parameters.AddWithValue("@PLANTCD", ((Label)row.FindControl("lblGVPlantID")).Text);
                        cmdI.Parameters.AddWithValue("@LOCCD", ((Label)row.FindControl("lblGVLocationCDID")).Text);
                        cmdI.Parameters.AddWithValue("@ITEMGRPID", ((Label)row.FindControl("lblGVGroupId")).Text);
                        cmdI.Parameters.AddWithValue("@SOQTY", ((Label)row.FindControl("lblGVQty")).Text);
                        cmdI.Parameters.AddWithValue("@UOM", ((Label)row.FindControl("lblGVUOMID")).Text);
                        cmdI.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblGVRate")).Text);
                        cmdI.Parameters.AddWithValue("@CAMOUNT", ((Label)row.FindControl("lblGVAmount")).Text);
                        cmdI.Parameters.AddWithValue("@DISCAMT", ((Label)row.FindControl("lblGVDiscount")).Text);
                        cmdI.Parameters.AddWithValue("@DELIDT", setDateFormat(Convert.ToString(((Label)row.FindControl("lblGVDeliDate")).Text), true));
                        cmdI.Parameters.AddWithValue("@GLCD", ((Label)row.FindControl("lblGVGLCode")).Text);
                        cmdI.Parameters.AddWithValue("@CSTCENTCD", ((Label)row.FindControl("lblGVCostCenter")).Text);
                        cmdI.Parameters.AddWithValue("@PRFCNT", ((Label)row.FindControl("lblGVProfitCenter")).Text);
                        cmdI.Parameters.AddWithValue("@ITEMTEXT", ((Label)row.FindControl("lblGVRemarks")).Text);
                        cmdI.Parameters.AddWithValue("@TAXAMT", "0.00");
                        cmdI.Parameters.AddWithValue("@CUSTPARTNO", ((Label)row.FindControl("lblGVCUSTPARTNO")).Text);
                        cmdI.Parameters.AddWithValue("@CUSTPARTDESC", ((Label)row.FindControl("lblGVCUSTPARTDESC")).Text);
                        cmdI.Parameters.AddWithValue("@CUSTPARTDESC2", ((Label)row.FindControl("lblGVIMEI")).Text);
                        //cmdI.Parameters.AddWithValue("@OLDITEMID", "");
                        cmdI.Parameters.AddWithValue("@CHANGEREASON", "");
                        cmdI.Parameters.AddWithValue("@PRODGRADE", ((Label)row.FindControl("lblGVGrade")).Text);
                        cmdI.Parameters.AddWithValue("@JOBID", ((Label)row.FindControl("lblGVTrackNo")).Text);
                        cmdI.Parameters.AddWithValue("@SCHEMEID", SCHEMEID);
                        cmdI.Parameters.AddWithValue("@LOCKAMT", ((Label)row.FindControl("lblLockAmt")).Text);
                        cmdI.Parameters.AddWithValue("@ACTION", "SODTLINSERT");
                        cmdI.CommandType = CommandType.StoredProcedure;
                        cmdI.Connection.Open();
                        cmdI.ExecuteNonQuery();
                        cmdI.Connection.Close();


                        #region JOB Data Change...
                        string NEWJOBID = ((Label)row.FindControl("lblGVTrackNo")).Text;
                        if (NEWJOBID != null && NEWJOBID != "" && NEWJOBID != string.Empty && Convert.ToInt32(NEWJOBID) > 0)
                        {
                            SqlCommand cmdA = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                            cmdA.Parameters.AddWithValue("@CUSTADD1", CUSTADD1);
                            cmdA.Parameters.AddWithValue("@CUSTADD2", CUSTADD2);
                            cmdA.Parameters.AddWithValue("@CUSTADD3", CUSTADD3);
                            cmdA.Parameters.AddWithValue("@CITY", CITY);
                            cmdA.Parameters.AddWithValue("@STATEID", STATEID);
                            cmdA.Parameters.AddWithValue("@PINCODE", PINCODE);
                            cmdA.Parameters.AddWithValue("@CUSTMOBILENO", CUSTMOBILENO);
                            cmdA.Parameters.AddWithValue("@CUSTEMAILID", CUSTEMAILID);
                            cmdA.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(NEWJOBID));
                            cmdA.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                            cmdA.Parameters.AddWithValue("@UPDATEBY", CREATEBY);
                            cmdA.Parameters.AddWithValue("@ACTION", "UPDATEJOB");
                            cmdA.CommandType = CommandType.StoredProcedure;
                            cmdA.Connection.Open();
                            cmdA.ExecuteNonQuery();
                            cmdA.Connection.Close();
                        }
                        #endregion


                    }
                    #endregion

                    #region  SO TAX Table Update...

                    SqlCommand cmdCD = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                    cmdCD.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SONO));
                    cmdCD.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdCD.Parameters.AddWithValue("@ACTION", "SOCONDELETE");
                    cmdCD.CommandType = CommandType.StoredProcedure;
                    cmdCD.Connection.Open();
                    cmdCD.ExecuteNonQuery();
                    cmdCD.Connection.Close();

                    for (int j = 0; j < TAXDETAILS.Rows.Count; j++)
                    {
                        GridViewRow row = TAXDETAILS.Rows[j];

                        SqlCommand cmdT = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                        cmdT.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdT.Parameters.AddWithValue("@CONDORDER", ((Label)row.FindControl("lblTaxSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SONO));
                        cmdT.Parameters.AddWithValue("@SOSRNO", ((Label)row.FindControl("lblTaxPOSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@CONDID", ((Label)row.FindControl("lblCONDID")).Text);
                        cmdT.Parameters.AddWithValue("@CONDTYPE", ((Label)row.FindControl("lblTaxCondType")).Text);
                        cmdT.Parameters.AddWithValue("@GLCODE", "");
                        cmdT.Parameters.AddWithValue("@RATE", ((Label)row.FindControl("lblTaxRate")).Text);
                        cmdT.Parameters.AddWithValue("@BASEAMT", ((Label)row.FindControl("lblTaxBaseAmount")).Text);
                        cmdT.Parameters.AddWithValue("@PID", ((Label)row.FindControl("lblPID")).Text);
                        cmdT.Parameters.AddWithValue("@TAXAMT", ((Label)row.FindControl("lblTaxAmount")).Text);
                        cmdT.Parameters.AddWithValue("@OPERATOR", ((Label)row.FindControl("lblTaxOperator")).Text);
                        cmdT.Parameters.AddWithValue("@ACTION", "SOCONDINSERT");
                        cmdT.CommandType = CommandType.StoredProcedure;
                        cmdT.Connection.Open();
                        cmdT.ExecuteNonQuery();
                        cmdT.Connection.Close();
                    }
                    #endregion

                    #region SO Charges Table Update...

                    SqlCommand cmdCH = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                    cmdCH.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SONO));
                    cmdCH.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdCH.Parameters.AddWithValue("@ACTION", "SOCHGDELETE");
                    cmdCH.CommandType = CommandType.StoredProcedure;
                    cmdCH.Connection.Open();
                    cmdCH.ExecuteNonQuery();
                    cmdCH.Connection.Close();

                    for (int k = 0; k < CHARGEDETAILS.Rows.Count; k++)
                    {
                        GridViewRow row = CHARGEDETAILS.Rows[k];

                        SqlCommand cmdT = new SqlCommand("SP_SO_MASTER", ConnSherpa);
                        cmdT.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdT.Parameters.AddWithValue("@SRNO", ((Label)row.FindControl("lblChrgSrNo")).Text);
                        cmdT.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SONO));
                        cmdT.Parameters.AddWithValue("@CHGTYPE", ((Label)row.FindControl("lblChrgCondType")).Text);
                        cmdT.Parameters.AddWithValue("@CHGAMT", ((Label)row.FindControl("lblChrgAmount")).Text);
                        cmdT.Parameters.AddWithValue("@ACTION", "SOCHGINSERT");
                        cmdT.CommandType = CommandType.StoredProcedure;
                        cmdT.Connection.Open();
                        cmdT.ExecuteNonQuery();
                        cmdT.Connection.Close();
                    }
                    #endregion




                    scope.Complete();
                    scope.Dispose();
                    return SONO;
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }

        }

        public DataTable GetCostCenter(string PLANTCD, string LOCCD)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_COSTCENTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetClaimACData(int CMPID, string FROMDATE, string TODATE, string JOBID, string REFNO, int QCDONE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CLAIMDATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@REFNO", REFNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable SearchSODeviation(int CMPID, string FROMDATE, string TODATE, string SONO, int STATUS, string ACTION, int ID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SO_DEVIATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? FROMDATE : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? TODATE : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@SONO", SONO == "" ? "" : strConvertZeroPadding(SONO));
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public int SaveSODeviation(int CMPID, string SONO, string SRNO, int OLDITEMID, string OLDITEMDESC, string OLDGRADE, int NEWITEMID, string NEWITEMDESC, string NEWGRADE,
            string REMARKS, int STATUS, int USERID, string ACTION, string OLDITEMCODE, string NEWITEMCODE)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_SO_DEVIATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@SRNO", SRNO);
                cmd.Parameters.AddWithValue("@OLDITEMID", OLDITEMID);
                cmd.Parameters.AddWithValue("@OLDITEMDESC", OLDITEMDESC);
                cmd.Parameters.AddWithValue("@OLDGRADE", OLDGRADE);
                cmd.Parameters.AddWithValue("@NEWITEMID", NEWITEMID);
                cmd.Parameters.AddWithValue("@NEWITEMDESC", NEWITEMDESC);
                cmd.Parameters.AddWithValue("@NEWGRADE", NEWGRADE);
                cmd.Parameters.AddWithValue("@REMARKS", REMARKS);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@CREATEBY", USERID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@OLDITEMCODE", OLDITEMCODE);
                cmd.Parameters.AddWithValue("@NEWITEMCODE", NEWITEMCODE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }


        public DataTable GetSOSearchData(int CMPID, string FROMDATE, string TODATE, string SONO, string ACTION, int STATUS, string SOTYPE, int PENDING)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SO_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@SOTYPE", SOTYPE);
                cmd.Parameters.AddWithValue("@PENDING", PENDING);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetSODev(int CMPID, string SONO, string SRNO, int STATUS, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SO_DEVIATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@SRNO", SRNO);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable CheckPlantLocationSales(int CMPID, string PLANT, string LOCATION, int ISSALES, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CHECKSALES_PLANT_LOCATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PLANT", PLANT);
                cmd.Parameters.AddWithValue("@LOCATION", LOCATION);
                cmd.Parameters.AddWithValue("@ISSALES", ISSALES);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetSODetails(int CMPID, string SOTYPE, string IMEINO, string ACTION, string REFNO, string SONO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SO_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SOTYPE", SOTYPE);
                cmd.Parameters.AddWithValue("@CUSTPARTDESC2", IMEINO);
                cmd.Parameters.AddWithValue("@REFNO", REFNO);
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GETPOMASTER(int CMPID, string IMEINO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PO_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetLeadStatusData(int CMPID, int ASSIGNTO, int STATUS, string FROMDATE, string TODATE, string ACTION, int LEADID = 0, int REFERENCE = 0, string CONTACTNO = "", string PRODUCT = "")
        {
            //GETSTATUSDATA
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);

            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@LEADID", LEADID);
                cmd.Parameters.AddWithValue("@REFID", REFERENCE);
                cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? FROMDATE : setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? TODATE : setDateFormat(TODATE, true));
                //cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? FROMDATE : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                //cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? TODATE : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@PRODUCT", PRODUCT);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetSalesReport(int CMPID, string SINO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SIREPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SINO", strConvertZeroPadding(SINO));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetCustReq(int CMPID, string FROMDATE, string TODATE, string CONTACTNO, string PRODUCT, string CUSTNAME, string ACTION, int LEADID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);

            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? FROMDATE : setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? TODATE : setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@PRODUCT", PRODUCT);
                cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                cmd.Parameters.AddWithValue("@LEADID", LEADID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }



        public int insertNewLead(DateTime CUSTUPDATEDATE, string CUSTNAME, string CONTACTNO, string EMAIL, string MAKE, string MODEL, string RAM, string ROM, string COLOR, string PRICE,
        string ASSIGNTO, int STATUS, string ADDRESS, string CREATEBY, string CUSTREMARK, string REFERENCE, int CMPID, string INQTYPE, string REF, string LTYPE, string PREFIX, string PHONENO,
        string CATEGORY, string CITY, string AREA, string PINCODE, int DNCMOBILENO, int DNCPHONENO, string COMPANYNAME, string BRANCHAREA, string BRANCHPIN, string PARENTID, int REFID,
        int CITYID, int STATEID, int GRABBED, string CSID, int ISUPDATEDONLAKHU, DateTime LAKHUUPDATEDDATETIME, string ACTION)
        {
            int iResult = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                SqlCommand cmd = new SqlCommand("SP_LEAD_API_MASTER", ConnSherpa);
                try
                {
                    cmd.Parameters.AddWithValue("@CUSTUPDATEDATE", CUSTUPDATEDATE);
                    cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                    cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                    cmd.Parameters.AddWithValue("@EMAIL", EMAIL);
                    cmd.Parameters.AddWithValue("@MAKE", MAKE);
                    cmd.Parameters.AddWithValue("@MODEL", MODEL);
                    cmd.Parameters.AddWithValue("@RAM", RAM);
                    cmd.Parameters.AddWithValue("@ROM", ROM);
                    cmd.Parameters.AddWithValue("@COLOR", COLOR);
                    cmd.Parameters.AddWithValue("@PRICERANGE", PRICE);
                    cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.Parameters.AddWithValue("@ADDRESS", ADDRESS);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@CUSTREMARKS", CUSTREMARK);
                    cmd.Parameters.AddWithValue("@REFERENCE", REFERENCE);
                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@INQTYPE", INQTYPE);
                    cmd.Parameters.AddWithValue("@REFFNO", REF);
                    cmd.Parameters.AddWithValue("@LEADTYPE", LTYPE);
                    cmd.Parameters.AddWithValue("@PREFIX", PREFIX);
                    cmd.Parameters.AddWithValue("@PHONE", PHONENO);
                    cmd.Parameters.AddWithValue("@CATEGORY", CATEGORY);
                    cmd.Parameters.AddWithValue("@CITY", CITY);
                    cmd.Parameters.AddWithValue("@AREA", AREA);
                    cmd.Parameters.AddWithValue("@PINCODE", PINCODE);
                    cmd.Parameters.AddWithValue("@DNCMOBILENO", DNCMOBILENO);
                    cmd.Parameters.AddWithValue("@DNCPHONENO", DNCPHONENO);
                    cmd.Parameters.AddWithValue("@COMPANYNAME", COMPANYNAME);
                    cmd.Parameters.AddWithValue("@BRANCHAREA", BRANCHAREA);
                    cmd.Parameters.AddWithValue("@BRANCHPIN", BRANCHPIN);
                    cmd.Parameters.AddWithValue("@PARENTID", PARENTID);
                    cmd.Parameters.AddWithValue("@REFID", REFID);
                    cmd.Parameters.AddWithValue("@GRABBED", GRABBED);
                    cmd.Parameters.AddWithValue("@CSID", CSID);
                    cmd.Parameters.AddWithValue("@ISUPDATEDONLAKHU", ISUPDATEDONLAKHU);
                    cmd.Parameters.AddWithValue("@LAKHUUPDATEDDATETIME", LAKHUUPDATEDDATETIME);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    iResult = Convert.ToInt32((cmd.Parameters["@ID"].Value));
                    cmd.Connection.Close();

                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
            }

            return iResult;
        }

        public int DeleteTempLead(int CMPID, int LEADID, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@LEADID", LEADID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;
        }

        public DataTable GetJSDetails(string JOBID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VIE_JOBSHEETWITHADD_SELECTBYJOBID", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetJSDetailsItem(int CMPID, string JOBID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_JOBSPECIFICATION_BYJOBID", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetJSDetailsItemNew(int CMPID, string JOBID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_JOBSHEETDTL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetJobIDStageDtl(int CMPID, string JOBID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_TRAN_JSMSTBYJOBID", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetSegmentDtl(int CMPID, string SEGMENT)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_DISTCHNL_BY_SEGMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetCustData(int CMPID, string CUSTCODE, string CUSTTYPE, string NAME, string CITY, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CUSTOMER_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CUSTCODE", CUSTCODE);
                cmd.Parameters.AddWithValue("@NAME1", NAME);
                cmd.Parameters.AddWithValue("@CUSTTYPE", CUSTTYPE);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public int BRANDID(string BRAND)
        {
            int BRANDID = 0;
            SqlCommand cmd = new SqlCommand("SELECT_BRANDID_BYBRANDNAME", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BRANDNAME", BRAND);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter daBrand = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                daBrand.Fill(dt);
                cmd.Connection.Close();
                if (dt.Rows.Count > 0)
                {
                    BRANDID = Convert.ToInt32(dt.Rows[0]["BRAND_ID"]);
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return BRANDID;
        }

        public int MODELID(string MODELNAME)
        {
            int MODELID = 0;
            SqlCommand cmd = new SqlCommand("SELECT_MODELID_BYMODELNAME", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter daBrand = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                daBrand.Fill(dt);
                cmd.Connection.Close();
                if (dt.Rows.Count > 0)
                {
                    MODELID = Convert.ToInt32(dt.Rows[0]["MODEL_ID"]);
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return MODELID;
        }


        public int InsertManualLead(string CUSTUPDATEDATE, string CUSTNAME, string CONTACTNO, string EMAIL, string MAKE, string MODEL, string RAM, string ROM, string COLOR, string PRICE,
        string ASSIGNTO, int STATUS, string CREATEBY, string CUSTREMARK, string REFERENCE, string ACTION, int CMPID, string INQTYPE, int REFID, int LTYPE, int CITYID, int STATEID, string CITY,
        string PRODUCT, string ATTRIBUTE, string ATTRVALUE, string LEADTYPE, string COMPANYNAME)
        {
            int iResult = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
                try
                {
                    cmd.Parameters.AddWithValue("@CUSTUPDATEDATE", setDateFormat(CUSTUPDATEDATE, true));
                    cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                    cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                    cmd.Parameters.AddWithValue("@EMAIL", EMAIL);
                    cmd.Parameters.AddWithValue("@MAKE", MAKE);
                    cmd.Parameters.AddWithValue("@MODEL", MODEL);
                    cmd.Parameters.AddWithValue("@RAM", RAM);
                    cmd.Parameters.AddWithValue("@ROM", ROM);
                    cmd.Parameters.AddWithValue("@COLOR", COLOR);
                    cmd.Parameters.AddWithValue("@PRICERANGE", PRICE == "" ? "" : PRICE);
                    cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@CUSTREMARKS", CUSTREMARK);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.Parameters.AddWithValue("@REFERENCE", REFERENCE);
                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@INQTYPE", INQTYPE);
                    cmd.Parameters.AddWithValue("@REFID", REFID);

                    cmd.Parameters.AddWithValue("@LTYPE", LTYPE);
                    cmd.Parameters.AddWithValue("@CITYID", CITYID);
                    cmd.Parameters.AddWithValue("@STATEID", STATEID);
                    cmd.Parameters.AddWithValue("@CITY", CITY);
                    cmd.Parameters.AddWithValue("@PRODUCT", PRODUCT);

                    cmd.Parameters.AddWithValue("@ATTRIBUTE", ATTRIBUTE);
                    cmd.Parameters.AddWithValue("@ATTRVALUE", ATTRVALUE);

                    cmd.Parameters.AddWithValue("@LEADTYPE", LEADTYPE);
                    cmd.Parameters.AddWithValue("@COMPANYNAME", COMPANYNAME);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    iResult = 1;
                    scope.Complete();
                    scope.Dispose();

                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
            }

            return iResult;
        }


        public DataTable GetCRMNotificationData(int STATUS, int ID, string ACTION)
        {
            //SP_CRM_NOTIFICATION
            SqlCommand cmd = new SqlCommand("SP_CRM_NOTIFICATION", ConnSherpa);
            DataTable dt = new DataTable();
            try
            {
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateCRMNotification(int ID, int STATUS, string ACTION)
        {
            SqlCommand cmd = new SqlCommand("SP_CRM_NOTIFICATION", ConnSherpa);
            int iResult = 0;
            try
            {
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public int InsertLeadGeneration(string LEADJSON, string CREATEBY, string ACTION, int STATUS)
        {
            int iResult = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
                try
                {
                    cmd.Parameters.AddWithValue("@LEADJSON", LEADJSON);
                    cmd.Parameters.AddWithValue("@CREATEBY", Convert.ToInt32(CREATEBY));
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    iResult = 1;
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
            }


            return iResult;
        }

        public int InsertManualLead(string CUSTUPDATEDATE, string CUSTNAME, string CONTACTNO, string EMAIL, string MAKE, string MODEL, string RAM, string ROM, string COLOR, string PRICE,
    string ASSIGNTO, int STATUS, string CREATEBY, string CUSTREMARK, string REFERENCE, string ACTION, int CMPID, string INQTYPE, int REFID)
        {
            int iResult = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
                try
                {
                    cmd.Parameters.AddWithValue("@CUSTUPDATEDATE", setDateFormat(CUSTUPDATEDATE, true));
                    cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                    cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                    cmd.Parameters.AddWithValue("@EMAIL", EMAIL);
                    cmd.Parameters.AddWithValue("@MAKE", MAKE);
                    cmd.Parameters.AddWithValue("@MODEL", MODEL);
                    cmd.Parameters.AddWithValue("@RAM", RAM);
                    cmd.Parameters.AddWithValue("@ROM", ROM);
                    cmd.Parameters.AddWithValue("@COLOR", COLOR);
                    cmd.Parameters.AddWithValue("@PRICERANGE", PRICE == "" ? "0" : PRICE);
                    cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@CUSTREMARKS", CUSTREMARK);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.Parameters.AddWithValue("@REFERENCE", REFERENCE);
                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@INQTYPE", INQTYPE);
                    cmd.Parameters.AddWithValue("@REFID", REFID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    iResult = 1;
                    scope.Complete();
                    scope.Dispose();

                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
            }

            return iResult;
        }

        public DataTable GetLeadDataMethod(string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);

            try
            {
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public void WALOG(int CMPID, string MSGTEXT, string MSGTO, string MSGBY, string IMGURL)
        {
            SqlCommand cmd = new SqlCommand("SP_INSERT_WAMSGLOG", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MSGTEXT", MSGTEXT);
                cmd.Parameters.AddWithValue("@MSGTO", MSGTO);
                cmd.Parameters.AddWithValue("@MSGBY", MSGBY);
                cmd.Parameters.AddWithValue("@IMGURL", IMGURL);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public int UpdateReassingCall(int CMPID, int LEADID, int ASSIGNTO, string ACTION, int USERID, int OLDID)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@LEADID", LEADID);
                cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
                cmd.Parameters.AddWithValue("@REASSIGNBY", USERID);
                cmd.Parameters.AddWithValue("@OLDID", OLDID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public int UpdateLeadData(int LEADID, int STATUS, int UPDATEBY, string CALLREMARKS, string CUSTREMARKS, string RECREFFNO, int HOLDREASON, DateTime POSTPONEDDATE, TimeSpan POSTPONEDTIME, string ACTION, string CALLEND, int DURATION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@LEADID", LEADID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@CALLREMARKS", CALLREMARKS);
                cmd.Parameters.AddWithValue("@CUSTREMARKS", CUSTREMARKS);
                cmd.Parameters.AddWithValue("@RECREFFNO", RECREFFNO);
                cmd.Parameters.AddWithValue("@HOLDREASON", HOLDREASON);
                cmd.Parameters.AddWithValue("@POSTPONEDDATE", POSTPONEDDATE);
                cmd.Parameters.AddWithValue("@POSTPONEDTIME", POSTPONEDTIME);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                cmd.Parameters.AddWithValue("@CALLEND", setDateFormat(CALLEND, true));
                cmd.Parameters.AddWithValue("@DURATION", DURATION);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public int UpdateAllReassignData(int CMPID, string ACTION, int USERID, GridView gvList, int ASSIGNTO)
        {
            int iResult = 0;
            for (int i = 0; i < gvList.Rows.Count; i++)
            {
                GridViewRow row = gvList.Rows[i];
                DropDownList ddlGVAgedtList = row.FindControl("ddlGVAgedtList") as DropDownList;
                CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;
                if (chkSelect.Checked == true)
                {

                    string LEADID = ((Label)row.FindControl("lblGVID")).Text;
                    string OLDID = ((Label)row.FindControl("lblGVASSIGNINT")).Text;
                    //iResult = UpdateReassingCall(CMPID, Convert.ToInt32(LEADID), Convert.ToInt32(ddlGVAgedtList.SelectedValue), ACTION, USERID, Convert.ToInt32(OLDID));
                    iResult = UpdateReassingCall(CMPID, Convert.ToInt32(LEADID), ASSIGNTO, ACTION, USERID, Convert.ToInt32(OLDID));
                }

            }
            return iResult;
        }

        public DataTable GetReAssignData(int CMPID, string FROMDATE, string TODATE, int ASSIGNTO, int STATUS, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? FROMDATE : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? TODATE : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public string ClickToCall(string MOBILENO, string DOCTYPE, string DOCNO, string AGENTID)
        {
            string url = "http://192.168.1.103/agent/api.php?source=click_to_call&user=1001&pass=elision123&agent_user="
            + AGENTID + "&function=external_dial&value=" + MOBILENO + "&phone_code=&search=YES&preview=NO&focus=YES&vendor_id=" + DOCTYPE + DOCNO + "&dial_prefix=&group_alias=";
            WebClient webClient = new WebClient();
            string result = webClient.DownloadString(url);
            return result;
        }

        public int CancelUpdateLeadData(int LEADID, int STATUS, int UPDATEBY, string CALLREMARKS, string CUSTREMARKS, string RECREFFNO, int HOLDREASON, string ACTION, string CALLEND, int DURATION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@LEADID", LEADID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@CALLREMARKS", CALLREMARKS);
                cmd.Parameters.AddWithValue("@CUSTREMARKS", CUSTREMARKS);
                cmd.Parameters.AddWithValue("@RECREFFNO", RECREFFNO);
                cmd.Parameters.AddWithValue("@HOLDREASON", HOLDREASON);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@CALLEND", setDateFormat(CALLEND, true));
                cmd.Parameters.AddWithValue("@DURATION", DURATION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;

        }

        public DataTable SelectWALog(int CMPID, string FROMDATE, string TODATE, string MOBILENO, string MAINQUERY)
        {
            //SP_SELECT_WAMSG
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_WAMSG", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? (object)DBNull.Value : DateTime.Parse(FROMDATE).ToString("yyyy-MM-dd") + " 00:00:00");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? (object)DBNull.Value : DateTime.Parse(TODATE).ToString("yyyy-MM-dd") + " 23:59:59");
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;

        }

        public DataTable GetGrabSummary(int CMPID, int GRABBED, string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@GRABBED", GRABBED);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetLeadData(int CMPID, string FROMDATE, string TODATE, int STATUS, int NEWID, int UPDATEBY, int CREATEDBY, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@NEWID", NEWID);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEDBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetPhyDocReturnHistory(string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PHYDOC_RETURNHISTORY", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetPORegister(int CMPID, string FROMDATE, string TODATE, string PLANTCODE, string LOCCODE, string PONO, string PRNO, string TRNUM, string REFNO, string VENDCODE, string ITEMCODE, string ACTION, string IMEINO, string ITEMGROUP)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PO_REGISTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE == "0" ? "" : PLANTCODE);
                cmd.Parameters.AddWithValue("@LOCATIONCODE", LOCCODE == "0" ? "" : LOCCODE);
                cmd.Parameters.AddWithValue("@PONO", PONO == "" ? "" : strConvertZeroPadding(PONO));
                cmd.Parameters.AddWithValue("@PRNO", PRNO == "" ? "" : strConvertZeroPadding(PRNO));
                cmd.Parameters.AddWithValue("@TRNUM", TRNUM == "" ? "" : strConvertZeroPadding(TRNUM));
                cmd.Parameters.AddWithValue("@REFNO", REFNO);
                cmd.Parameters.AddWithValue("@VENDCODE", VENDCODE == "" ? "" : strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE == "" ? "" : strConvertZeroPadding(ITEMCODE));
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@ITEMGROUP", ITEMGROUP);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        public DataTable GetDealerData(int BIKERID, int STATUS, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BDO_MAPPING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BIKERID", BIKERID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateDealerBiker(int CMPID, int DEALERID, int STATUS, int BIKERID, int UPDATEBY, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_DEALER_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ID", DEALERID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@BIKERID", BIKERID);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }


        public DataTable GetPlatFormNotListed(string PLATFORM, string MAKE, string MODEL, string GRADE, int CREATEBY, string Action = "SELECTONE")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_NOTLISTEDMODELDETAILCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@PLATFORM", PLATFORM);
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@GRADE", GRADE);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@ACTION", Action);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public int UpdateASM(int BDO, int STATUS, int ASMID, int UPDATEBAY, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_BDO_MAPPING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@USERID", BDO);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ASMID", ASMID);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBAY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public DataTable GetBDOASMData(int BIKERID, int STATUS, int SMTYPE, int ASMID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BDO_MAPPING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BIKERID", BIKERID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@SMTYPE", SMTYPE);
                cmd.Parameters.AddWithValue("@ASMID", ASMID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }





        //CRM FOR CALL CENTER 

        //AUTO INQUIRY FROM MOBEX SELLER INWARD
        public int INSERTINQUIRYFROMMOBEXSELLERINWARD(string REF, string MODELNAME, string IMEINO, string FULLNAME, int STATE_ID, int CITY_ID
            , string CITY, string STATE, int CREATEBY)
        {
            MainClass objMainClass = new MainClass();
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //long orderid    =  objDALInquiry.SELECT_MAX_ORDERID("1015");
                    SqlCommand cmd = new SqlCommand("INSERT_TRAN_PARTREQ", ConnSherpa);
                    cmd.Parameters.AddWithValue("@REF", REF);
                    cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME);
                    cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                    cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME);
                    cmd.Parameters.AddWithValue("@STATE_ID", STATE_ID);
                    cmd.Parameters.AddWithValue("@CITY_ID", CITY_ID);
                    cmd.Parameters.AddWithValue("@CITY", CITY);
                    cmd.Parameters.AddWithValue("@STATE", STATE);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    iResult = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iResult;
        }

        public long SELECT_MAX_DOCNO(string strDocType)
        {
            long strReturn = 0;
            SqlCommand cmd = new SqlCommand("SP_SELECT_MAX_DOCNO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", intCmpId);
                cmd.Parameters.AddWithValue("@DOCTYPE", strDocType);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                object obj = cmd.ExecuteScalar();
                if (obj != null)
                {
                    strReturn = long.Parse(obj.ToString()) + 1;
                }
                cmd.Connection.Close();
                return strReturn;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void UPDATE_MST_MMNORANGE_CURRNO(string strDocNo, string strDocType)
        {
            SqlCommand cmd = new SqlCommand("SP_UPDATE_MST_MMNORANGE_CURRNO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", intCmpId);
                cmd.Parameters.AddWithValue("@DOCNO", strDocNo);
                cmd.Parameters.AddWithValue("@DOCTYPE", strDocType);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void INSERT_ADDRESSG(string refid, string reftype, string addof, string addr1, string addr2, string addr3, string city, int stcd, string cncd,
        string postalcode, string contactno, string contactperson, string mobileno, string emailid, string website)
        {
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("INSERT_MST_ADDRESS", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@refid", objMainClass.strConvertZeroPadding(refid));
                cmd.Parameters.AddWithValue("@reftype", reftype);
                cmd.Parameters.AddWithValue("@addof", addof);
                cmd.Parameters.AddWithValue("@addr1", addr1);
                cmd.Parameters.AddWithValue("@addr2", addr2);
                cmd.Parameters.AddWithValue("@addr3", addr3);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@stcd", stcd);
                cmd.Parameters.AddWithValue("@cncd", cncd);
                cmd.Parameters.AddWithValue("@postalcode", postalcode);
                cmd.Parameters.AddWithValue("@contactno", contactno);
                cmd.Parameters.AddWithValue("@contactperson", contactperson);
                cmd.Parameters.AddWithValue("@mobileno", mobileno);
                cmd.Parameters.AddWithValue("@emailid", emailid);
                cmd.Parameters.AddWithValue("@website", website);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public void INSERT_TRAN_JSMST(string jobid, string shiptoparty, string billtoparty, string endcust, int addid, string remark, int jobstatus, int statupdby,
                                      string statres, string segment, string distchnl, int jstype, string isreturn, string refjobid, string jdaref, string jwrefno, string pickupfrom,
                                      string shipto, string aprvflag, int pickupaddid, int dropaddid, string inqno, int OOW, int CREATEBY)
        {
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("SP_INSERT_TRAN_JSMST", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@jobid", objMainClass.strConvertZeroPadding(jobid));
                cmd.Parameters.AddWithValue("@jobdt", objMainClass.indianTime);
                cmd.Parameters.AddWithValue("@shiptoparty", shiptoparty);
                cmd.Parameters.AddWithValue("@billtoparty", billtoparty);
                cmd.Parameters.AddWithValue("@endcust", endcust);
                cmd.Parameters.AddWithValue("@addid", addid);
                cmd.Parameters.AddWithValue("@remark", remark);
                cmd.Parameters.AddWithValue("@jobstatus", jobstatus);
                cmd.Parameters.AddWithValue("@statupdby", statupdby);
                cmd.Parameters.AddWithValue("@statres", statres);
                cmd.Parameters.AddWithValue("@segment", segment);
                cmd.Parameters.AddWithValue("@distchnl", distchnl);
                cmd.Parameters.AddWithValue("@jstype", jstype);
                cmd.Parameters.AddWithValue("@isreturn", isreturn);
                cmd.Parameters.AddWithValue("@refjobid", refjobid);
                cmd.Parameters.AddWithValue("@jdaref", objMainClass.strConvertZeroPadding(jdaref));
                cmd.Parameters.AddWithValue("@jdarefdt", objMainClass.indianTime);
                cmd.Parameters.AddWithValue("@jwrefno", jwrefno);
                cmd.Parameters.AddWithValue("@pickupfrom", pickupfrom);
                cmd.Parameters.AddWithValue("@shipto", shipto);
                cmd.Parameters.AddWithValue("@aprvflag", aprvflag);
                cmd.Parameters.AddWithValue("@pickupaddid", pickupaddid);
                cmd.Parameters.AddWithValue("@dropaddid", dropaddid);
                cmd.Parameters.AddWithValue("@inqno", inqno);
                cmd.Parameters.AddWithValue("@OOW", OOW);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@CREATEDATE", objMainClass.indianTime);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public void INSERT_TRAN_JSDTL(string jobid, int srno, int itemid, string itemdesc, decimal qty,
                                      int uom, decimal rate, string plantcd, string loccd, decimal itemvalue, string extwarno, string prodmake, string prodmodel, string imeino,
                                      string jobtype, string jobdesc, string refinvno, DateTime refinvdt, decimal refinvamt, string insuco, string note, string batteryno, string backcoverflag
                                      , string prodcond, string prodcolor, string lockcode, string phyimeino, string imeino2)
        {
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("SP_INSERT_TRAN_JSDTL", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@jobid", objMainClass.strConvertZeroPadding(jobid));
                cmd.Parameters.AddWithValue("@srno", srno);
                cmd.Parameters.AddWithValue("@itemid", itemid);
                cmd.Parameters.AddWithValue("@itemdesc", itemdesc);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@uom", uom);
                cmd.Parameters.AddWithValue("@rate", rate);
                cmd.Parameters.AddWithValue("@plantcd", plantcd);
                cmd.Parameters.AddWithValue("@loccd", loccd);
                cmd.Parameters.AddWithValue("@itemvalue", itemvalue);
                cmd.Parameters.AddWithValue("@extwarno", extwarno);
                cmd.Parameters.AddWithValue("@prodmake", prodmake);
                cmd.Parameters.AddWithValue("@prodmodel", prodmodel);
                cmd.Parameters.AddWithValue("@imeino", imeino);
                cmd.Parameters.AddWithValue("@jobtype", jobtype);
                cmd.Parameters.AddWithValue("@jobdesc", jobdesc);
                cmd.Parameters.AddWithValue("@refinvno", refinvno);
                cmd.Parameters.AddWithValue("@refinvdt", refinvdt);
                cmd.Parameters.AddWithValue("@refinvamt", refinvamt);
                cmd.Parameters.AddWithValue("@insuco", insuco);
                cmd.Parameters.AddWithValue("@note", note);
                cmd.Parameters.AddWithValue("@batteryno", batteryno);
                cmd.Parameters.AddWithValue("@backcoverflag", backcoverflag);
                cmd.Parameters.AddWithValue("@prodcond", prodcond);
                cmd.Parameters.AddWithValue("@prodcolor", prodcolor);
                cmd.Parameters.AddWithValue("@lockcode", lockcode);
                cmd.Parameters.AddWithValue("@phyimeino", phyimeino);
                cmd.Parameters.AddWithValue("@imeino2", imeino2);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public void UPDATE_ADDID_JSMST(string strJobNo)
        {
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("UPDATE_ADDID_JSMST", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@JOBID", objMainClass.strConvertZeroPadding(strJobNo));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }


        public void UPDATE_PICKUPADDID_JSMST(string strJobNo)
        {
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_PICKUPADDID_JSMST", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", objMainClass.strConvertZeroPadding(strJobNo));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public string MAXFIRANGENO(int CMPID, int FINYEAR, string DOCTYPE)
        {
            string FIRANGENO = string.Empty;
            SqlCommand cmd = new SqlCommand("SP_SELECT_FIMAX_DOCNO", ConnSherpa);
            try
            {


                cmd.Parameters.AddWithValue("@FINYEAR", FINYEAR);
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                object obj = cmd.ExecuteScalar();
                if ((obj) != null)
                {
                    FIRANGENO = obj.ToString();
                    FIRANGENO = Convert.ToString(Convert.ToInt64(FIRANGENO) + 1);
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return FIRANGENO;
        }

        public string UPDATEMAXFIRANGENO(int CMPID, string DOCTYPE, int FINYEAR, string CURRNO)
        {
            string RANGENO;
            SqlCommand cmd = new SqlCommand("SP_UPDATE_FINORANGE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@FINYER", FINYEAR);
                cmd.Parameters.AddWithValue("@CURRNO", CURRNO);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                RANGENO = CURRNO;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return RANGENO;
        }
        public void UPDATE_DROPADDID_JSMST(string strJobNo)
        {
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("SP_UPDATE_DROPADDID_JSMST", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", objMainClass.strConvertZeroPadding(strJobNo));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public DataTable GetAdvPOData(int CMPID, string VENDCODE, int ADVFLAG, int OACFLAG, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ADVFLAG", ADVFLAG);
                cmd.Parameters.AddWithValue("@OACFLAG", OACFLAG);
                cmd.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable CheckSameAmt(int CMPID, string VENDCODE, decimal DOCAMT, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PARTYAC", strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@DOCAMT", DOCAMT);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public void UpdateInquiryStatus(string INQNO, int STATUS)
        {
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("UPDATE_INQUIRY_STATUS", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@INQNO", INQNO);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                //cmd.Parameters.AddWithValue("@UPDATEBY", MainClass.BackGroundUser);
                //cmd.Parameters.AddWithValue("@UPDATEDATE", objMainClass.indianTime);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }
        public string InsertJobSheet(string addof, string add1, string add2, string add3, string city, int stcd, string cncd
            , string postalcode, string contactno, string contactperson, string mobileno, string emailid, string website
            , string jobfrom, string billtoparty, string customer, int addid, string remark, int status, int userid, string statres
            , string segment, string distchnl, int jstype, string isreturn, string refjobid, string jdaref, string jwrefno
            , string pickupfrom, string shipto, string aprvflag, int pickupaddid, int dropaddid, string inqno, int OOW, int CREATEBY
            , int srno, int itemid, string itemdesc, decimal qty, int uom, decimal rate, string plantcd, string loccd
            , decimal itemvalue, string extwarno, string prodmake, string prodmodel, string imeino, string jobtype, string jobdesc
            , string refinvno, DateTime refinvdt, decimal refinvamt, string insuco, string note, string batteryno, string backcoverflag
            , string prodcond, string prodcolor, string lockcode, string phyimeino, string imeino2, string txtJDARefNo
                    )
        {
            MainClass objMainClass = new MainClass();
            string strMaxJobId = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    //GET JOBNO 
                    strMaxJobId = objMainClass.SELECT_MAX_DOCNO("JS").ToString();
                    //GET JOBNO 

                    // UPDATE JOBNO 
                    objMainClass.UPDATE_MST_MMNORANGE_CURRNO(strMaxJobId, "JS");
                    // UPDATE JOBNO 

                    //INSERT ADDRESS 
                    objMainClass.INSERT_ADDRESSG(strMaxJobId, "JS", addof, add1, add2, "", city, stcd, cncd, postalcode,
                                 contactno, customer, mobileno, emailid, website);
                    //INSERT ADDRESS

                    //INSERT JOBMASTER
                    objMainClass.INSERT_TRAN_JSMST(strMaxJobId, jobfrom, billtoparty, customer,
                                 0, remark, (int)enumJobStatus.Saved, userid, statres, segment, distchnl, jstype, isreturn,
                                 refjobid, jdaref, jwrefno, pickupfrom, shipto, aprvflag, pickupaddid, dropaddid, inqno, OOW, CREATEBY);
                    //INSERT JOBMASTER

                    //INSERT JOBDETAIL
                    objMainClass.INSERT_TRAN_JSDTL(strMaxJobId, 1, itemid, itemdesc, qty, uom, rate, plantcd,
                                   loccd, itemvalue, extwarno, prodmake, prodmodel, prodmodel, jobtype, jobdesc, refinvno,
                                   refinvdt, refinvamt, insuco, note, batteryno, backcoverflag, prodcond, prodcolor,
                                   lockcode, phyimeino, phyimeino);
                    //INSERT JOBDETAIL

                    //UPDATE ADD ID AT JOBMASTER
                    objMainClass.UPDATE_ADDID_JSMST(strMaxJobId);
                    //UPDATE ADD ID AT JOBMASTER

                    //UPDATE PICKUP ADD ID AT JOBMASTER
                    objMainClass.UPDATE_PICKUPADDID_JSMST(strMaxJobId);
                    //UPDATE PICKUP ADD ID AT JOBMASTER

                    //UPDATE DROPUP ADD ID AT JOBMASTER
                    objMainClass.UPDATE_DROPADDID_JSMST(strMaxJobId);
                    //UPDATE DROPUP ADD ID AT JOBMASTER

                    //UPDATE INQUIRY STATUS JOBMASTER
                    //objMainClass.UpdateInquiryStatus(txtJDARefNo, (int)enumStatus.Converted);
                    //UPDATE INQUIRY STATUS JOBMASTER

                    //UPDATE JOBID AT SALES RETURN REQUEST
                    //objMainClass.UPDATE_JOBNO_ATSRETURNREQUEST(refinvno);
                    //UPDATE JOBID AT SALES RETURN REQUEST

                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return strMaxJobId;
        }

        public DataTable GetJobStatusByIMEI(int CMPID, string JOBID, string IMEINO, int JOBSTATUS)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CHECKJOBSTATUS_IMEINO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@JOBSTATUS", JOBSTATUS);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }


        public DataTable GetSegmentStageData(int STAGESEQ, string SEGMENT, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SEGMENT_STAGESEQ", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@STAGESEQ", STAGESEQ);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }


        public int GetStatusByStageID(int STAGEID)
        {
            int statusid = 0;
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_STATUSBYSTAGE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@STAGEID", STAGEID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                if (dt.Rows.Count > 0)
                {
                    statusid = Convert.ToInt32(dt.Rows[0]["ID"]);
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return statusid;
        }


        public DataTable GetMobexDateWiseListedReport(string FROMDATE, string TODATE, int CREATEBY)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEXDATEWISELISTEDREPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetMOBEXVENODRNOTACTIVELISTINGREPORT(string FROMDATE, string TODATE, string action, int userid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEXVENODRNOTACTIVELISTINGREPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@ACTION", action);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetInwardedProductEntryDetail(string FROMDATE, string TODATE, string VENDORNAME, int status, int userid = 0, string IMEINO = ""
            , int ID = 0)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_RETURNCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@ACTION", "SEARCH");
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetKeepaData(int CMPID, string AMAZONMAPPED, string FLIPKARTMAPPED, string WEBSITEMAPPED, string SCWEBSITEMAPPED, string ITEMCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_KEEPA_DATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@AMAZONMAPPED", AMAZONMAPPED);
                cmd.Parameters.AddWithValue("@FLIPKARTMAPPED", FLIPKARTMAPPED);
                cmd.Parameters.AddWithValue("@WEBSITEMAPPED", WEBSITEMAPPED);
                cmd.Parameters.AddWithValue("@SCWEBSITEMAPPED", SCWEBSITEMAPPED);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE == "" ? "" : strConvertZeroPadding(ITEMCODE));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetVendList(int CMPID, int STATUS, string ITEMCODE, string ACTION, string SONO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SO_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetInstallationData(int CMPID, string FROMDATE, string TODATE, string SINO, string SONO, int SOSRNO, string ACTION, int ONLYRETAIL, int FINALENTRY, int ACTUALDISPATCH)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PRODUCT_DEMONSTRATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@SINO", SINO == "" ? "" : strConvertZeroPadding(SINO));
                cmd.Parameters.AddWithValue("@SONO", SONO == "" ? "" : strConvertZeroPadding(SONO));
                cmd.Parameters.AddWithValue("@SOSRNO", SOSRNO);
                cmd.Parameters.AddWithValue("@ONLYRETAIL", ONLYRETAIL);
                cmd.Parameters.AddWithValue("@FINALENTRY", FINALENTRY);
                cmd.Parameters.AddWithValue("@ACTUALDISPATCH", ACTUALDISPATCH);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable SalesDashBoard(int CMPID, string TODAYDATE, string THISMONTH, string LASTFIRST, string LASTLAST, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_NEW_DASHBOARD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@TODAY", setDateFormat(TODAYDATE, true));
                cmd.Parameters.AddWithValue("@THISMONTH", setDateFormat(THISMONTH, true));
                cmd.Parameters.AddWithValue("@LASTMONTHFIRST", setDateFormat(LASTFIRST, true));
                cmd.Parameters.AddWithValue("@LASTMONTHLAST", setDateFormat(LASTLAST, true));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public String GetMobexSellerTokenFromDealerID(int DEALERID)
        {
            DataTable dt = new DataTable();
            string token = "";
            SqlCommand cmd = new SqlCommand("SP_APPTOKENDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DEALERID", DEALERID);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTONE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                if (dt.Rows.Count > 0)
                {
                    token = dt.Rows[0]["APPTOKEN"].ToString();
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return token;
        }


        public int AddNotificationLog(string SUBJECT, string SENTMSG, int SENTUSERID, int ISSENT)
        {
            int result = 0;
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("SP_NOTIFICATIONSENTCRUDOPERATION", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@SUBJECT", SUBJECT);
                cmd.Parameters.AddWithValue("@SENTMSG", SENTMSG);
                cmd.Parameters.AddWithValue("@SENTUSERID", SENTUSERID);
                cmd.Parameters.AddWithValue("@ISSENT", ISSENT);
                cmd.Parameters.AddWithValue("@ACTION", "ADD");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return result;
        }


        public int UpdateReturnStatus(string returnreason, int STATUS, int userid, int ID)
        {
            int result = 0;
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_RETURNCRUDOPERATION", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@RETURNREASON", returnreason);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", "UPDATERETURNDETAIL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return result;
        }

        public int UPDATERETURNGENERATEDTOBDOHANDOVERDETAIL(int STATUS, int userid, int ID)
        {
            int result = 0;
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_RETURNCRUDOPERATION", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", "UPDATERETURNGENERATEDTOBDOHANDOVERDETAIL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return result;
        }

        public int UPDATERETURNVENDORETAIL(int STATUS, int userid, int ID)
        {
            int result = 0;
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_RETURNCRUDOPERATION", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", "UPDATERETURNVENDORETAIL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return result;
        }

        public DataTable CheckIMEINOJobExist(string IMEINO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_RETURNCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@ACTION", "CHECKJOBSHEETEXIST");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetEachMobexSellerReturnReport(string ID, int userid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_RETURNCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@ACTION", "GETEACHRETURNREPORT");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public int SAVEMOBEXSELLERBULKRESERVEDDETAIL(string MOBEXSELLERLISTEDJSON, string UserId)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region SAVEMOBEXSELLERETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEXSELLER_BULKRESERVED", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@MOBEXSELLERLISTEDJSON", MOBEXSELLERLISTEDJSON);
                    cmdM.Parameters.AddWithValue("@RESERVEDBY", UserId);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public int RejectedMobileApproved(int ID, string NGEAPRV, int NEGAPRVBY, int STATUS, string ITEMCODE)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE PURCHASE DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@NGEAPRV", NGEAPRV);
                    cmdM.Parameters.AddWithValue("@NEGAPRVBY", NEGAPRVBY);
                    cmdM.Parameters.AddWithValue("@NEGAPRVDATE", setDateFormat(objMainClass.indianTime.Date.ToString("dd-MM-yyyy"), true));
                    cmdM.Parameters.AddWithValue("@STATUS", STATUS);
                    cmdM.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@ACTION", "REJECTEDTOAPPROVED");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
        public DataTable CheckContactNo(int CMPID, string CONTACTNO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GETMRTOPURCHASEBILLREPORTUSER(int CMPID, string FROMDATE, string TODATE, string MRNO, string PRNO, string PONO, string GRNNO, string PBNO, string PLANTCD, int ITEMGRPID, int USERID, string ACTION)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MRTOPURCHASEBILLREPORT_USER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@Todate", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@MRNO", (MRNO.Length > 0 ? strConvertZeroPadding(MRNO) : ""));
                cmd.Parameters.AddWithValue("@PRNO", (PRNO.Length > 0 ? strConvertZeroPadding(PRNO) : ""));
                cmd.Parameters.AddWithValue("@PONO", (PONO.Length > 0 ? strConvertZeroPadding(PONO) : ""));
                cmd.Parameters.AddWithValue("@GRNNO", (GRNNO.Length > 0 ? strConvertZeroPadding(GRNNO) : ""));
                cmd.Parameters.AddWithValue("@PBNO", (PBNO.Length > 0 ? strConvertZeroPadding(PBNO) : ""));
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@LOCCD", "0");
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 500;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public int UpdateCustName(int CMPID, int LEADID, string CUSTNAME, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@LEADID", LEADID);
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);


                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public int UpdateStartTime(int CMPID, int LEADID, string STARTDATE, int UPDATEBY, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CALLSTART", setDateFormat(STARTDATE, true));
                cmd.Parameters.AddWithValue("@LEADID", LEADID);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public DataTable GetSISOData(int CMPID, string SONO, string SINO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SISO_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SONO", SONO != "" ? strConvertZeroPadding(SONO) : "");
                cmd.Parameters.AddWithValue("@SINO", SINO != "" ? strConvertZeroPadding(SINO) : "");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public string UpdateSISOAddress(int CMPID, string SONO, string CUSTNAME, string CUSTADD1, string CUSTADD2, string CUSTADD3, string CITY, int STATEID, string PINCODE, string CUSTMOBILENO,
           string CUSTEMAIL, int UPDATEBY, string ACTION, string GSTNO, GridView GVLIST)
        {
            string SOSINO = "";
            SqlCommand cmd = new SqlCommand("SP_SISO_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SONO", strConvertZeroPadding(SONO));
                cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                cmd.Parameters.AddWithValue("@CUSTADD1", CUSTADD1);
                cmd.Parameters.AddWithValue("@CUSTADD2", CUSTADD2);
                cmd.Parameters.AddWithValue("@CUSTADD3", CUSTADD3);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                cmd.Parameters.AddWithValue("@STATEID", STATEID);
                cmd.Parameters.AddWithValue("@PINCODE", PINCODE);
                cmd.Parameters.AddWithValue("@CUSTMOBILENO", SqlDbType.Int).Value = CUSTMOBILENO == "0" ? (object)DBNull.Value : CUSTMOBILENO;
                //cmd.Parameters.AddWithValue("@CUSTMOBILENO", CUSTMOBILENO == "" ? (object)DBNull.Value : CUSTMOBILENO);
                cmd.Parameters.AddWithValue("@CUSTEMAILID", CUSTEMAIL);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@GSTNO", GSTNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                for (int i = 0; i < GVLIST.Rows.Count; i++)
                {
                    GridViewRow row = GVLIST.Rows[i];
                    string NEWJOBID = ((Label)row.FindControl("lblGVTrackNo")).Text;
                    if (NEWJOBID != null && NEWJOBID != "" && NEWJOBID != string.Empty && Convert.ToInt32(NEWJOBID) > 0)
                    {
                        SqlCommand cmdA = new SqlCommand("SP_SISO_MASTER", ConnSherpa);
                        cmdA.Parameters.AddWithValue("@CUSTADD1", CUSTADD1);
                        cmdA.Parameters.AddWithValue("@CUSTADD2", CUSTADD2);
                        cmdA.Parameters.AddWithValue("@CUSTADD3", CUSTADD3);
                        cmdA.Parameters.AddWithValue("@CITY", CITY);
                        cmdA.Parameters.AddWithValue("@STATEID", STATEID);
                        cmdA.Parameters.AddWithValue("@PINCODE", PINCODE);
                        cmdA.Parameters.AddWithValue("@CUSTMOBILENO", CUSTMOBILENO);
                        cmdA.Parameters.AddWithValue("@CUSTEMAILID", CUSTEMAIL);
                        cmdA.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(NEWJOBID));
                        cmdA.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                        cmdA.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                        cmdA.Parameters.AddWithValue("@ACTION", "UPDATEJOBADDRESS");
                        cmdA.CommandType = CommandType.StoredProcedure;
                        cmdA.Connection.Open();
                        cmdA.ExecuteNonQuery();
                        cmdA.Connection.Close();
                    }

                }
                SOSINO = strConvertZeroPadding(SONO);
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return SOSINO;
        }

        public DataTable GetVendorLedger(int CMPID, string VENDCODE, string FROMDATE, string TODATE, string ACTION, int TALLYGROUP = 0)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PARTY_LEDGER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@DOCFROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@DOCTODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@TALLYGROUP", TALLYGROUP);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }

        public DataTable GetListableData(int CMPID, string PLANTCD)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LISTABLEREPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public string IPADDRESS()
        {
            string IPAdd = "";
            string HostName = System.Net.Dns.GetHostName();
            System.Net.IPHostEntry iphe = System.Net.Dns.GetHostEntry(HostName);
            foreach (System.Net.IPAddress ipheal in iphe.AddressList)
            {
                if (ipheal.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAdd = ipheal.ToString();
                }
            }
            return IPAdd;
        }

        public void LogoutTrace(long LoginID)
        {
            SqlCommand cmd = new SqlCommand("SA_LOGINTRACEUPDATEFORLOGOUT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", LoginID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public long LoginTrace(int USERID, string IPADDRESS)
        {
            long LoginID = 0;
            SqlCommand cmd = new SqlCommand("SA_LOGINTRACEINSERT_NEW", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@IPADDRESS", IPADDRESS);
                cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                LoginID = Convert.ToInt64((cmd.Parameters["@ID"].Value));
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return LoginID;
        }

        public DataTable GetPurchaseBillReg(string FROMDATE, string TODATE, string PBNO, string PONO, string ACTION)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            //SP_SELECT_PURCHASEBILL
            SqlCommand cmd = new SqlCommand("SP_SELECT_PURCHASEBILL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                cmd.Parameters.AddWithValue("@FROM", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TO", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@PBNO", (PBNO.Length > 0 ? strConvertZeroPadding(PBNO) : ""));
                cmd.Parameters.AddWithValue("@PONO", (PONO.Length > 0 ? strConvertZeroPadding(PONO) : ""));
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 800;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateBillNo(int CMPID, string PBNO, string BILLNO, int UPDATEBY, string ACTION)
        {
            int i = 0;
            //SP_INSERT_TRAN_PBMST_NEW
            SqlCommand cmd = new SqlCommand("SP_INSERT_TRAN_PBMST_NEW", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PBNO", strConvertZeroPadding(PBNO));
                cmd.Parameters.AddWithValue("@BILLNO", BILLNO);
                cmd.Parameters.AddWithValue("@CREATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }
        public DataTable GetDashBoardCount(string FROMDATE, string TODATE, int SEARCHBY)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            //SP_SELECT_PURCHASEBILL
            SqlCommand cmd = new SqlCommand("SP_LISTINGSYSTEM_DASHBOARD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SEARCHBY", SEARCHBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetUnregisterCust(int CMPID, int UNREGISTER, string CUSTCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CUST_APPROVAL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@UNREGISTER", UNREGISTER);
                cmd.Parameters.AddWithValue("@CUSTCODE", CUSTCODE == "" ? "" : strConvertZeroPadding(CUSTCODE));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateCustUnregi(int CMPID, int UNREGISTER, string CUSTCODE, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_CUST_APPROVAL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@UNREGISTER", UNREGISTER);
                cmd.Parameters.AddWithValue("@CUSTCODE", CUSTCODE == "" ? "" : strConvertZeroPadding(CUSTCODE));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }


        public DataTable GetProductSpec(int CMPID, string JOBID, string ITEMCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GET_ITEMSPEC", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetIntervalListedUnlistedReport(string FROMDATE, string TODATE, int USERID)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            //SP_SELECT_PURCHASEBILL
            SqlCommand cmd = new SqlCommand("SP_INTERVALLISTEDUNLISTEDREPORTCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE);
                cmd.Parameters.AddWithValue("@TODATE", TODATE);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        //AUTO INQUIRY FROM MOBEX SELLER INWARD

        public DataTable GetStockwithPurchaePrice(int CMPID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_STOCKWITHPURCHASEPRICE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", intCmpId);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        public DataTable GetItemDetails(int CMPID, string ITEMDESC, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_ITEMMAPPING_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMDESC", ITEMDESC);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetItemDetailsByItemCode(int CMPID, string ITEMCODE, int STATUS, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_ITEMMAPPING_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetEWData(int CMPID, int ITEMSUBGROUP, decimal NETAMT, decimal MINAMT, decimal MAXAMT, int ID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_EW_PROCEDURE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGROUP", ITEMSUBGROUP);
                cmd.Parameters.AddWithValue("@NETAMT", NETAMT);
                cmd.Parameters.AddWithValue("@MINAMT", MINAMT);
                cmd.Parameters.AddWithValue("@MAXAMT", MAXAMT);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public List<string> GetItemDetailsList(int CMPID, string ITEMDESC, string ACTION)
        {
            List<String> itemcode = new List<string>();
            SqlCommand cmd = new SqlCommand("SP_ITEMMAPPING_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMDESC", ITEMDESC);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                using (SqlDataReader sda = cmd.ExecuteReader())
                {
                    while (sda.Read())
                    {
                        itemcode.Add(sda["ITEMCODE"].ToString() + " - " + sda["ITEMDESC"].ToString());
                    }
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return itemcode;
        }

        public DataTable GetItemMappingData(int CMPID, string ITEMCODE, string SKU, string FLIPKART, string AMAZON, string WEBSITE, int STATUS, string ACTION, int ONLYAMAZON, int ONLYWEBSITE)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_ITEMMAPPING_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@SKU", SKU);
                cmd.Parameters.AddWithValue("@FLIPKART", FLIPKART);
                cmd.Parameters.AddWithValue("@AMAZON", AMAZON);
                cmd.Parameters.AddWithValue("@WEBSITE", WEBSITE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@ONLYAMAZON", ONLYAMAZON);
                cmd.Parameters.AddWithValue("@ONLYWEBSITE", ONLYWEBSITE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public int InsertItemMapping(int CMPID, string ITEMCODE, string ITEMDESC, string SKU, string FLIPKART, string AMAZON, string WEBSITE, int STATUS, int USERID, string EXTRA1, string EXTRA2,
            string EXTRA3, string ACTION, string SCWEBSITE, string NEWAMAZON, string CFURL)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_ITEMMAPPING_MASTER", ConnSherpa);

            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@ITEMDESC", ITEMDESC);
                cmd.Parameters.AddWithValue("@SKU", SKU);
                cmd.Parameters.AddWithValue("@FLIPKART", FLIPKART);
                cmd.Parameters.AddWithValue("@AMAZON", AMAZON);
                cmd.Parameters.AddWithValue("@WEBSITE", WEBSITE);
                cmd.Parameters.AddWithValue("@SCWEBSITE", SCWEBSITE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@CREATEBY", USERID);
                cmd.Parameters.AddWithValue("@EXTFIELD1", EXTRA1);
                cmd.Parameters.AddWithValue("@EXTFIELD2", EXTRA2);
                cmd.Parameters.AddWithValue("@EXTFIELD3", EXTRA3);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                cmd.Parameters.AddWithValue("@NEWAMAZON", NEWAMAZON);
                cmd.Parameters.AddWithValue("@CFURL", CFURL);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;
        }


        public DataTable GetPhyDOcData(string FROMDATE, string TODATE, int JOBSTATUS, string QCRESULT, string PLANTCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MODELPURCHASECOSTDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@Todate", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@JobStatus", JOBSTATUS);
                cmd.Parameters.AddWithValue("@QcResult", QCRESULT);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCODE);
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //public DataTable GetPhyDocReturnHistory(string ACTION)
        //{
        //    DataTable dt = new DataTable();
        //    SqlCommand cmd = new SqlCommand("SP_PHYDOC_RETURNHISTORY", ConnSherpa);
        //    try
        //    {
        //        cmd.Parameters.AddWithValue("@ACTION", ACTION);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //        cmd.Connection.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        cmd.Connection.Close();
        //        throw ex;
        //    }
        //    return dt;
        //}

        public DataTable GetSOAge(int CMPID, string FROMDATE, string TODATE, string SONO, string MAINQUERY, string AGE, string PLANTCODE = null, int SALESFROM = 0)
        {
            //SP_SOAGING
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SOAGING", ConnSherpa);

            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MAINQUERY", MAINQUERY);
                cmd.Parameters.AddWithValue("@SALESFROM", SALESFROM);
                cmd.Parameters.AddWithValue("@AGE", AGE);
                cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);
                //cmd.Parameters.AddWithValue("@GROUP", GROUPBY);
                //cmd.Parameters.AddWithValue("@SEQUENCE", SEQUENCE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }


            return dt;
        }


        public DataTable SalesRegisterData(int CMPID, string FROMDATE, string TODATE, string PLANTCODE, string LOCCD, string SINO, string DOCTYPE, string REFNO, int EXCLUDERETURN, string SEGMENT,
             string DISTCHNL, string JOBID, string SONO, string IMEINO, string ACTION, string SALEFROM, string DEVICETYPE, string CUSTNAME)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SALES_REGISTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);
                cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
                cmd.Parameters.AddWithValue("@SINO", SINO == "" ? "" : strConvertZeroPadding(SINO));
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@REFNO", REFNO);
                cmd.Parameters.AddWithValue("@EXCLUDERETURNS", EXCLUDERETURN);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@SONO", SONO == "" ? "" : strConvertZeroPadding(SONO));
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@SALECHNL", SALEFROM);
                cmd.Parameters.AddWithValue("@DEVICETYPE", DEVICETYPE);
                cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 800;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetCalculatedWebsiteAvgAmt(int SEARCHBYID, string MAKE, string SEARCHOPERATOR)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_WEBRATEUPDATEDETAIL", ConnSherpa);
            cmd.CommandTimeout = 800;
            try
            {
                cmd.Parameters.AddWithValue("@SEARCHBYID", SEARCHBYID);
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@SEARCHOPERATOR", SEARCHOPERATOR);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetCalculatedAllPlatFormAvgAmt(int SEARCHBYID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_ALLPLATEFORMAVERAGECALCULATEETAIL_TEST", ConnSherpa);
            cmd.CommandTimeout = 800;
            try
            {
                cmd.Parameters.AddWithValue("@SEARCHBYID", SEARCHBYID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetProductEveryStatusDatetimeDetail(string FROMDATE, string TODATE, int status,
           string OLDDAYSFILTER, string MOBEXRATEFILTER, string MOBEXGRADE, int CREATEBY)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLEREVERYSTATUSDATETIMEREPORT", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@OLDDAYSFILTER", OLDDAYSFILTER);
                cmd.Parameters.AddWithValue("@MOBEXRATEFILTER", MOBEXRATEFILTER);
                cmd.Parameters.AddWithValue("@MOBEXGRADE", MOBEXGRADE);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int GetPopupStatus(int USERID, int STATUS, string ACTION)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@USERID", USERID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Connection.Close();

                if (dt.Rows.Count > 0)
                {
                    iResult = Convert.ToInt32(dt.Rows[0]["POPUP"]);
                }

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }
        public DataTable GetAllLeadReport(int CMPID, int INQTYPE, string FROMDATE, string TODATE, string ACTION)
        {
            //GETSTATUSDATA
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", ConnSherpa);

            try
            {

                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@INQTYPE", INQTYPE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? FROMDATE : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? TODATE : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetUnMappedItemDetail(int SEARCHBYID, string ACTION)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            //SP_SELECT_PODATA
            SqlCommand cmd = new SqlCommand("SP_WEBUNMAPPEDITEMETAIL", ConnSherpa);
            cmd.CommandTimeout = 800;
            try
            {
                cmd.Parameters.AddWithValue("@SEARCHBYID", SEARCHBYID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetSDDashboard(int CMPID, string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_ORDERMANAGEMENT_DASHBOARD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetPopupCCE(int STATUS, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_POPUP", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public int UpdatePopup(int POPUP, int ID, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_LEAD_POPUP", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@POPUP", POPUP);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public DataTable OtherData(int FROMNO, int TONO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_OTHER_DATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMNO", FROMNO);
                cmd.Parameters.AddWithValue("@TONO", TONO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GETAGINGBUCKETS(string Segmentcode, string AgingAction)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SegmentAgingBuckets", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@Segmentcode", Segmentcode);
                cmd.Parameters.AddWithValue("@AgingAction", AgingAction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetMobileIMEIImageDetail(int ID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_MOBILEIMEIIMAGE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable Msl_Report(string ACTION, string PLANTCODE, string LOCATION)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MSL_REPORT", ConnSherpa);
            {
                try
                {
                    cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.Parameters.AddWithValue("@PLANTCD", PLANTCODE);
                    cmd.Parameters.AddWithValue("@LOCCD", LOCATION);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    cmd.Connection.Close();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
                return dt;
            }
        }

        public DataTable Matirial_Movment(string MAINQUERY, String FORMDATE, String TODATE, String PLANTCD, String LOCCD, String ITEMCODE, string SEGMENT, String JOBID, String DOCTYPE, string IMEINO, int VALUE)
        {
            DataTable dt = new DataTable();
            SqlCommand Cmd = new SqlCommand("SP_MATERIAL_MOVEMENT", ConnSherpa);
            try
            {
                Cmd.Parameters.AddWithValue("@MAINQUERY", @MAINQUERY);
                Cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FORMDATE, true));
                Cmd.Parameters.AddWithValue("@ToDate", setDateFormat(TODATE, true));
                Cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                Cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
                Cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                Cmd.Parameters.AddWithValue("@JOBID", JOBID);
                Cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                Cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                Cmd.Parameters.AddWithValue("@VALUE", VALUE);
                Cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                Cmd.CommandType = CommandType.StoredProcedure;

                Cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(Cmd);
                da.Fill(dt);
                Cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetOpenJobId(int CMPID, string ITEMCODE, int JOBSTATUS, int STAGEID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_ITEMMAPPING_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@JOBSTATUS", JOBSTATUS);
                cmd.Parameters.AddWithValue("@STAGEID", STAGEID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable JobsheetItemcode(int CMPID, string JOBID, string IMEINO, string FROMDATE, string TODATE, int STATUS, string PLANTCODE)
        {
            DataTable DT = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_JOBSHEET_WITH_ITEMCODE", ConnSherpa);

            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCODE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(DT);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return DT;
        }

        public int BULKWEBSITERATEQTYUPDATEHISTORYLOG(string WEBSITERATEQTYUPDATEJSON, int UPDATEBY)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("BULKWEBSITERATEQTYUPDATEHISTORYLOG", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@WEBSITERATEQTYUPDATEJSON", WEBSITERATEQTYUPDATEJSON);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                i = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public DataTable MOBEXSELLERVENDORLEDGER(string FROMDATE, string TODATE, int VENDORID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLER_VENDORLEDGER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GETITEMSUBGRP_SPECDETAIL(int ITEMSUBGRPID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_ITEMSUBGRP_SPEC_CRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTONE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetOtherProductRecomendedRate(string MAKE, string MODEL, string RAM, string ROM, string GRADE, string COLOR, int ITEMGRPID
            , int ITEMSUBGRPID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_OTHERPRODUCTRECOMENDEDRATE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@RAM", RAM);
                cmd.Parameters.AddWithValue("@ROM", ROM);
                cmd.Parameters.AddWithValue("@GRADE", GRADE);
                cmd.Parameters.AddWithValue("@COLOR", COLOR);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetOtherProductNewRate(string MAKE, string MODEL, string RAM, string ROM, string COLOR, int ITEMGRPID,
            int ITEMSUBGRPID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_OTHERPRODUCTGETNEWRATE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@RAM", RAM);
                cmd.Parameters.AddWithValue("@ROM", ROM);
                cmd.Parameters.AddWithValue("@COLOR", COLOR);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetMainGroupSubGroupShortName(int ITEMGRPID, int ITEMSUBGRPID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GETITEMGROUPSUBGROUPSHORTNAME", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTONE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetMakeModelPartsDetail(string MAKE, string MODEL, string COLOR)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("   ", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@COLOR", COLOR);
                cmd.Parameters.AddWithValue("@ACTION", "MAKEMODELPARTS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }



        public int UpdateDOCList(int CMPID, int ID, int RCVDBY, int STATUSID, int STATUSUPDATEBY, string ACTION, string REJECTREASON)
        {
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand("SP_PB_RECEIVEAPPROVE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@RCVDBY", RCVDBY);
                cmd.Parameters.AddWithValue("@RCVDDT", DateTime.Now);
                cmd.Parameters.AddWithValue("@STATUSID", STATUSID);
                cmd.Parameters.AddWithValue("@STATUSUPDATEBY", STATUSUPDATEBY);
                cmd.Parameters.AddWithValue("@STATUSUPDATEDATE", DateTime.Now);
                cmd.Parameters.AddWithValue("@REJREASON", REJECTREASON);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteReader();
                cmd.Connection.Close();
                iReturn = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iReturn;
        }

        public bool ApprovePO(int CMPID, int ID, string REJREASON, int APRVSTATUS, int APRVBY, string ACTION)
        {
            bool iResult = false;
            SqlCommand cmd = new SqlCommand("SP_APPROVEPO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@REJREASON", REJREASON);
                cmd.Parameters.AddWithValue("@APRVBBY", APRVBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@APRVSTATUS", APRVSTATUS);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = true;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public int UpdateItemBeforeCreatePRPO(int CMPID, int LISTINGID, string ITEMCODE, string ACTION = "UPDATEITEMCODE")
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_PRPO_FORM_LISTING", ConnSherpa);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@LISTINGID", LISTINGID);
                    cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    iResult = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
            }
            return iResult;
        }

        public DataTable GetSODetail(string REFNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BULKSOCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@REFNO", REFNO);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTONE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int BulkSoCreation(string SOCREATIONJSON, string ACTION = "BULKINSERT")
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_BULKSOCRUDOPERATION", ConnSherpa);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    cmd.Parameters.AddWithValue("@SOCREATIONJSON", SOCREATIONJSON);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    iResult = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
            }
            return iResult;
        }

        public int BulkSoAddUpdate(string SOADDRESSJSON, int UPDATEBY, string ACTION = "UPDATE")
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_BULKSONAMEADDRESSUPDATE", ConnSherpa);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    cmd.Parameters.AddWithValue("@SONAMEADDRESSDETAIL", SOADDRESSJSON);
                    cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    iResult = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
            }
            return iResult;
        }

        public DataTable GetASINITEMDETAIL(string ASIN)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BULKSOCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ASIN", ASIN);
                cmd.Parameters.AddWithValue("@ACTION", "GETITEMDETAI");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        public DataTable GetMakeModelPOAvrageAmount(int CMPID, string MAKE, string MODEL)
        {

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_PO_FOR_APPROVE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetProductFinalListingRate(string MAKE, string MODEL, string RAM, string ROM, string GRADE, string COLOR)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_PRODUCTFINALLISTRATE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@RAM", RAM);
                cmd.Parameters.AddWithValue("@ROM", ROM);
                cmd.Parameters.AddWithValue("@GRADE", GRADE);
                cmd.Parameters.AddWithValue("@COLOR", COLOR);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetLockPrice(string MAKE, string MODEL, string RAM, string ROM, string COLOR, string GRADE, string ITEMDESC)
        {
            //SP_MOBEX_SELLER_PRODUCTFINALLISTRATE
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_PRODUCTFINALLISTRATE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@RAM", RAM);
                cmd.Parameters.AddWithValue("@ROM", ROM);
                cmd.Parameters.AddWithValue("@COLOR", COLOR);
                cmd.Parameters.AddWithValue("@GRADE", GRADE);
                cmd.Parameters.AddWithValue("@ITEMDESC", ITEMDESC);
                cmd.Parameters.AddWithValue("@CHECK", "YES");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetRCVAPRVREJPBData(int CMPID, string DOCTYPE, string DOCNO, int STATUSID, int SENDBY, int RCVDBY, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PB_RECEIVEAPPROVE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                cmd.Parameters.AddWithValue("@STATUSID", STATUSID);
                cmd.Parameters.AddWithValue("@SENDBY", SENDBY);
                cmd.Parameters.AddWithValue("@RCVDBY", RCVDBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetTRNData(int CMPID, string FROMDATE, string TODATE, string PLANTCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_INSTRANSIT_FUNC", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 800;
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetJSDetailsByRefjobid(int CMPID, string JOBID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_JSDATA_BYREFJOBID", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetAdvPendingPaymentData(int CMPID, string VENDCODE, int ADVFLAG, int OACFLAG, string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ADVFLAG", ADVFLAG);
                cmd.Parameters.AddWithValue("@OACFLAG", OACFLAG);
                cmd.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetEachProdSpecPrimaryDetail(string BRAND_DESC, string MODEL_NAME, string RAMSIZE, string ROMSIZE, int ITEMGRPID, int ITEMSUBGRPID)
        {
            //SP_MOBEX_SPECDATA
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SPECDATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BRAND_DESC", BRAND_DESC);
                cmd.Parameters.AddWithValue("@MODEL_NAME", MODEL_NAME);
                cmd.Parameters.AddWithValue("@RAMSIZE", RAMSIZE);
                cmd.Parameters.AddWithValue("@ROMSIZE", ROMSIZE);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTONE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;

        }

        public DataTable GetEachProdLaunchDetail(string BRAND_DESC, string MODEL_NAME, int ITEMGRPID, int ITEMSUBGRPID)
        {
            //SP_MOBEX_SPECDATA
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SPECDATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BRAND_DESC", BRAND_DESC);
                cmd.Parameters.AddWithValue("@MODEL_NAME", MODEL_NAME);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTONEMAKEMODELDETAIL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;

        }

        public DataTable GetEachProdSpecalreadyExist(string BRAND_DESC, string MODEL_NAME, string RAMSIZE, string ROMSIZE, string COLOR, int ITEMGRPID, int ITEMSUBGRPID)
        {
            //SP_MOBEX_SPECDATA
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SPECDATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BRAND_DESC", BRAND_DESC);
                cmd.Parameters.AddWithValue("@MODEL_NAME", MODEL_NAME);
                cmd.Parameters.AddWithValue("@RAMSIZE", RAMSIZE);
                cmd.Parameters.AddWithValue("@ROMSIZE", ROMSIZE);
                cmd.Parameters.AddWithValue("@COLOR", COLOR);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.Parameters.AddWithValue("@ACTION", "CHECK");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable CMSFORMAT(int CMPID, string BANK, string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BANKPAYMENT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@BANK", BANK);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetFollowupCount(string FROMDATE, string TODATE, int SEARCHBY)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            //SP_SELECT_PURCHASEBILL
            SqlCommand cmd = new SqlCommand("SP_LISTINGFOLLOWPCOUNT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SEARCHBY", SEARCHBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetMobexSellerTrackingCount(string FROMDATE, string TODATE, int SEARCHBY)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            //SP_SELECT_PURCHASEBILL
            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLERLISTINGTRACKING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SEARCHBY", SEARCHBY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetDailyFTD(int CMPID, string DATE, string SEGMENT, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DAILY_FTD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DATE", DATE);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetClaimData(int CMPID, int STATUSCODE, string FROMDATE, string TODATE, string JOBID, string PLANTCD, string LOCCD, string LISTTYPE, string ACTION, string ORDERID, string CLAINREFNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CLAIMDATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@STATUSCODE", STATUSCODE);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
                cmd.Parameters.AddWithValue("@LISTTYPE", LISTTYPE);
                cmd.Parameters.AddWithValue("@ORDERID", ORDERID);
                cmd.Parameters.AddWithValue("@CLAIMREFNO", CLAINREFNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetMaxMinStk(int CMPID, int ITEMID, string PLANTCODE, string LOCCD, int STATUS, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GET_MAXMINSTK", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMID", ITEMID);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCODE);
                cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int InsertLogisticDetail(string PICKUPFROM_PARTYNAME, string PICKUPFROM_PARTYADDRESS1, string PICKUPFROM_PARTYCITY, string PICKUPFROM_PARTYSTATE,
           string PICKUPFROM_PARTYPINCODE, string PICKUPFROM_PARTYCONTACTNO, string PICKUPFROM_PARTYEMAIL, string PICKUPFROM_DATE, string PICKUPFROM_TIME, string DELIVERTO_PARTYNAME,
           string DELIVERTO_PARTYADDRESS1, string DELIVERTO_PARTYCITY, string DELIVERTO_PARTYSTATE, string DELIVERTO_PARTYPINCODE, string DELIVERTO_PARTYCONTACTNO,
           string DELIVERTO_PARTYEMAIL, string TYPE, string SEGMENTNAME, string SEGMENTREFNO, int CREATEDBY, string JOBID, string BRAND, string PRODUCTNAME, string PRODUCTDESCRIPTION,
           string PICKUPFROM_PARTYADDRESS2, string DELIVERTO_PARTYADDRESS2, string PRODUCTIDENTIFICATION, string SEGMENTINQREFNO, string REFTYPE, string REFNO,
           string SERVICEPROVIDERTRACKNO, string SERVICEPROVIDERTYPE, string SERVICEPROVIDERNAME, decimal SERVICEPROVIDERRATE, string STATUS, int LOGISTICREQID, string TRACKINGURL,
           string CREATEDATE, string SERVICEPROVIDERPICKUPNO, string SERVICEPROVIDERTRACKSTATUSREMARKS, int LOGIALLOTMENTID, int LISTSTATUS)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                    int ID = 0;
                    SqlCommand cmd = new SqlCommand("SP_LOGIREQ_TRAN", ConnSherpa);
                    cmd.Parameters.AddWithValue("@PICKUPFROM_PARTYNAME", PICKUPFROM_PARTYNAME);
                    cmd.Parameters.AddWithValue("@PICKUPFROM_PARTYADDRESS1", PICKUPFROM_PARTYADDRESS1);
                    cmd.Parameters.AddWithValue("@PICKUPFROM_PARTYCITY", PICKUPFROM_PARTYCITY);
                    cmd.Parameters.AddWithValue("@PICKUPFROM_PARTYSTATE", PICKUPFROM_PARTYSTATE);
                    cmd.Parameters.AddWithValue("@PICKUPFROM_PARTYPINCODE", PICKUPFROM_PARTYPINCODE);
                    cmd.Parameters.AddWithValue("@PICKUPFROM_PARTYCONTACTNO", PICKUPFROM_PARTYCONTACTNO);
                    cmd.Parameters.AddWithValue("@PICKUPFROM_PARTYEMAIL", PICKUPFROM_PARTYEMAIL);
                    cmd.Parameters.AddWithValue("@PICKUPFROM_DATE", setDateFormat(PICKUPFROM_DATE, true));
                    cmd.Parameters.AddWithValue("@PICKUPFROM_TIME", PICKUPFROM_TIME);
                    cmd.Parameters.AddWithValue("@DELIVERTO_PARTYNAME", DELIVERTO_PARTYNAME);
                    cmd.Parameters.AddWithValue("@DELIVERTO_PARTYADDRESS1", DELIVERTO_PARTYADDRESS1);
                    cmd.Parameters.AddWithValue("@DELIVERTO_PARTYCITY", DELIVERTO_PARTYCITY);
                    cmd.Parameters.AddWithValue("@DELIVERTO_PARTYSTATE", DELIVERTO_PARTYSTATE);
                    cmd.Parameters.AddWithValue("@DELIVERTO_PARTYPINCODE", DELIVERTO_PARTYPINCODE);
                    cmd.Parameters.AddWithValue("@DELIVERTO_PARTYCONTACTNO", DELIVERTO_PARTYCONTACTNO);
                    cmd.Parameters.AddWithValue("@DELIVERTO_PARTYEMAIL", DELIVERTO_PARTYEMAIL);
                    cmd.Parameters.AddWithValue("@TYPE", TYPE);
                    cmd.Parameters.AddWithValue("@SEGMENTNAME", SEGMENTNAME);
                    cmd.Parameters.AddWithValue("@SEGMENTREFNO", SEGMENTREFNO);
                    cmd.Parameters.AddWithValue("@CREATEDBY", CREATEDBY);
                    cmd.Parameters.AddWithValue("@JOBID", JOBID);
                    cmd.Parameters.AddWithValue("@BRAND", BRAND);
                    cmd.Parameters.AddWithValue("@PRODUCTNAME", PRODUCTNAME);
                    cmd.Parameters.AddWithValue("@PRODUCTDESCRIPTION", PRODUCTDESCRIPTION);
                    cmd.Parameters.AddWithValue("@PICKUPFROM_PARTYADDRESS2", PICKUPFROM_PARTYADDRESS2);
                    cmd.Parameters.AddWithValue("@DELIVERTO_PARTYADDRESS2", DELIVERTO_PARTYADDRESS2);
                    cmd.Parameters.AddWithValue("@PRODUCTIDENTIFICATION", PRODUCTIDENTIFICATION);
                    cmd.Parameters.AddWithValue("@SEGMENTINQREFNO", SEGMENTINQREFNO);
                    cmd.Parameters.AddWithValue("@REFTYPE", REFTYPE);
                    cmd.Parameters.AddWithValue("@ACTION", "INSERTREQ");
                    cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    ID = Convert.ToInt32((cmd.Parameters["@ID"].Value));
                    cmd.Connection.Close();

                    if (ID > 0)
                    {
                        int allotmentid = 0;
                        SqlCommand cmdAllotment = new SqlCommand("SP_LOGIREQ_TRAN", ConnSherpa);
                        cmdAllotment.Parameters.AddWithValue("@REFNO", ID);
                        cmdAllotment.Parameters.AddWithValue("@SERVICEPROVIDERTRACKNO", SERVICEPROVIDERTRACKNO);
                        cmdAllotment.Parameters.AddWithValue("@SERVICEPROVIDERTYPE", SERVICEPROVIDERTYPE);
                        cmdAllotment.Parameters.AddWithValue("@SERVICEPROVIDERNAME", SERVICEPROVIDERNAME);
                        cmdAllotment.Parameters.AddWithValue("@SERVICEPROVIDERRATE", SERVICEPROVIDERRATE);
                        cmdAllotment.Parameters.AddWithValue("@STATUS", STATUS);
                        cmdAllotment.Parameters.AddWithValue("@LOGISTICREQID", LOGISTICREQID);
                        cmdAllotment.Parameters.AddWithValue("@TRACKINGURL", TRACKINGURL);
                        cmdAllotment.Parameters.AddWithValue("@CREATEDBY", CREATEDBY);
                        cmdAllotment.Parameters.AddWithValue("@SERVICEPROVIDERPICKUPNO", SERVICEPROVIDERPICKUPNO);
                        cmdAllotment.Parameters.AddWithValue("@SERVICEPROVIDERTRACKSTATUSREMARKS", SERVICEPROVIDERTRACKSTATUSREMARKS);
                        cmdAllotment.Parameters.AddWithValue("@ACTION", "INSERTALLOTMENT");
                        cmdAllotment.Parameters.AddWithValue("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                        cmdAllotment.CommandType = CommandType.StoredProcedure;
                        cmdAllotment.Connection.Open();
                        cmdAllotment.ExecuteNonQuery();
                        allotmentid = Convert.ToInt32((cmdAllotment.Parameters["@ID"].Value));
                        cmdAllotment.Connection.Close();

                        if (allotmentid > 0)
                        {
                            SqlCommand cmdTracking = new SqlCommand("SP_LOGIREQ_TRAN", ConnSherpa);
                            cmdTracking.Parameters.AddWithValue("@STATUS", STATUS);
                            cmdTracking.Parameters.AddWithValue("@LOGIALLOTMENTID", allotmentid);
                            cmdTracking.Parameters.AddWithValue("@CREATEDBY", CREATEDBY);
                            cmdTracking.Parameters.AddWithValue("@ACTION", "INSERTLOGINTRACKING");
                            //cmdTracking.Parameters.AddWithValue("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                            cmdTracking.CommandType = CommandType.StoredProcedure;
                            cmdTracking.Connection.Open();
                            cmdTracking.ExecuteNonQuery();
                            cmdTracking.Connection.Close();


                            SqlCommand cmdList = new SqlCommand("SP_LOGIREQ_TRAN", ConnSherpa);
                            cmdList.Parameters.AddWithValue("@JOBID", JOBID);
                            cmdList.Parameters.AddWithValue("@STATUS", LISTSTATUS);
                            cmdList.Parameters.AddWithValue("@TRACKINGURL", TRACKINGURL);
                            cmdList.Parameters.AddWithValue("@ACTION", "LISTUPDATE");
                            //cmdList.Parameters.AddWithValue("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                            cmdList.CommandType = CommandType.StoredProcedure;
                            cmdList.Connection.Open();
                            cmdList.ExecuteNonQuery();
                            cmdList.Connection.Close();

                        }


                    }


                    iResult = 1;
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;
        }

        public DataTable GetContactDetails(int CMPID, int STCD, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GETCONTACT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@STCD", STCD);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetListingAssign(int CMPID, string LISTINGID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LISTINGASSIGN", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@LISTINGID", LISTINGID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateVendorBank(int CMPID, string VENDCODE, string BANKNAME, string IFSC, string ACCOUNTNO, int ACCTYPE, int UPDATEBY, string ACTION, string CHEQUETYPE, byte[] CHEQUE)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(VENDCODE));
                    cmd.Parameters.AddWithValue("@BANKNAME", BANKNAME);
                    cmd.Parameters.AddWithValue("@IFSCCODE", IFSC);
                    cmd.Parameters.AddWithValue("@ACCOUNTNO", ACCOUNTNO);
                    cmd.Parameters.AddWithValue("@ACCNTTYPE", ACCTYPE);
                    cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    if (CHEQUE != null)
                    {
                        DeleteVendImage(strConvertZeroPadding(VENDCODE), 11317, "DELETEIMAGE");
                        int result3 = InsertVendImage(VENDCODE, 11317, CHEQUE, 1, "INSERTIMAGE", CHEQUETYPE);
                    }


                    scope.Complete();
                    scope.Dispose();
                    iResult = 1;
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;
        }


        public DataTable GetProductInstantDetail(string FROMDATE, string TODATE, int status,
   int CREATEBY, string CITY)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLER_INSTANTSELLINGDETAIL", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetOpenSOData(int CMPID, int STATUS, string SOTYPE, string PLANTCD, string ACTION, int SALESFROM, string AGENT)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_OPENSO_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@SOTYPE", SOTYPE);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@SALESFROM", SALESFROM);
                cmd.Parameters.AddWithValue("@AGENT", AGENT);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.CommandTimeout = 600;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //01-02-23
        public DataTable GetRejecttoApprovedReport(string FROMDATE, string ToDate)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_REJECTEDTOAPPROVEDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(ToDate, true));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public void SaveWAMSGNotification(int CMPID, string MSGTO, string MSGTEXT, string DOCTYPE, string DOCNO, int SENTBY, string ACTION)
        {
            SqlCommand cmd = new SqlCommand("SP_WAMSG", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MSGTO", MSGTO);
                cmd.Parameters.AddWithValue("@MSGTEXT", MSGTEXT);
                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                cmd.Parameters.AddWithValue("@SENTBY", SENTBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public int UpdateJWREFNo(int CMPID, string JOBID, string JWREFNO, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_UPDATE_JWREFNO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JWREFNO", JWREFNO);
                cmd.Parameters.AddWithValue("@JOBID", JOBID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public void UpdateSOORDER(string ORDERNO, string SONO, int ID, string ACTION)
        {
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ORDERNO", ORDERNO);
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public DataTable GetMobileINVOICEImageDetail(int ID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_MOBILEINVOICEIMAGE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        public DataTable GetPurchaseOrderVsSaleDepartment(string FROMDATE, string ToDate, int Plantcd)
        {
            MainClass obj = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cd = new SqlCommand("SP_PURCHAS_VS_SALE", ConnSherpa);
            try
            {
                cd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cd.Parameters.AddWithValue("@TODATE", setDateFormat(ToDate, true));
                cd.Parameters.AddWithValue("@Plantcd", Plantcd);
                cd.Parameters.AddWithValue("@CMPID", this.intCmpId);
                cd.CommandType = CommandType.StoredProcedure;
                cd.Connection.Open();
                cd.ExecuteNonQuery();
                SqlDataReader dr = cd.ExecuteReader();
                dt.Load(dr);
                cd.Connection.Close();
            }
            catch (Exception ex)
            {
                cd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataSet GetLogistic(string FROMDATE, string TODATE, string CRNNO, string JOBID, string ACTION)
        {
            DataSet dt = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_LOGIREQ_TRAN", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@Fromdate", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@Todate", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SERVICEPROVIDERTRACKNO", CRNNO);
                cmd.Parameters.AddWithValue("@JOBID", JOBID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetBrandwidelisting(string FROMDATE, string ToDate, int plantid)
        {
            MainClass obj = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cd = new SqlCommand("SP_Brand_Wise_Listing", ConnSherpa);
            try
            {
                cd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cd.Parameters.AddWithValue("@TODATE", setDateFormat(ToDate, true));
                cd.Parameters.AddWithValue("@plantid", plantid);
                cd.Parameters.AddWithValue("@CMPID", this.intCmpId);
                cd.CommandType = CommandType.StoredProcedure;
                cd.Connection.Open();
                cd.CommandTimeout = 500;
                cd.ExecuteNonQuery();
                SqlDataReader dr = cd.ExecuteReader();
                dt.Load(dr);
                cd.Connection.Close();
            }
            catch (Exception ex)
            {
                cd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetGradewidelisting(string FROMDATE, string ToDate, int plantid)
        {
            MainClass obj = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cd = new SqlCommand("SP_Grade_Wise_Listing", ConnSherpa);
            try
            {
                cd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cd.Parameters.AddWithValue("@TODATE", setDateFormat(ToDate, true));
                cd.Parameters.AddWithValue("@plantid", plantid);
                cd.Parameters.AddWithValue("@CMPID", this.intCmpId);
                cd.CommandType = CommandType.StoredProcedure;
                cd.CommandTimeout = 500;
                cd.Connection.Open();
                cd.ExecuteNonQuery();
                SqlDataReader dr = cd.ExecuteReader();
                dt.Load(dr);
                cd.Connection.Close();
            }
            catch (Exception ex)
            {
                cd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetItemID(string ITEMCODE, string Mode)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEXSELLERITEMCODEOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@Action", Mode);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        public string SavePRdetails(GridView grvListItem, string PRNO, string PRTYPE, string SRNO)
        {
            try
            {
                //PRNO = MAXPRNO(PRTYPE, "PR");
                //PRNO = strConvertZeroPadding(PRNO);

                MainClass objMainClass = new MainClass();
                for (int i = 0; i < grvListItem.Rows.Count; i++)
                {
                    GridViewRow row = grvListItem.Rows[i];

                    SqlCommand cmdD = new SqlCommand("SP_INSERT_PR_DETAILS", ConnSherpa);
                    cmdD.Parameters.AddWithValue("@cmpid", objMainClass.intCmpId);
                    cmdD.Parameters.AddWithValue("@PRNO", strConvertZeroPadding(PRNO));
                    cmdD.Parameters.AddWithValue("@SRNO", SRNO);
                    cmdD.Parameters.AddWithValue("@ITEMID", Convert.ToString(grvListItem.Rows[0].Cells[5].Text));
                    cmdD.Parameters.AddWithValue("@ITEMDESC", Convert.ToString(grvListItem.Rows[0].Cells[6].Text));
                    cmdD.Parameters.AddWithValue("@PLANTCD", Convert.ToString(grvListItem.Rows[0].Cells[9].Text));
                    cmdD.Parameters.AddWithValue("@LOCCD", Convert.ToString(grvListItem.Rows[0].Cells[10].Text));
                    cmdD.Parameters.AddWithValue("@TRNUM", Convert.ToString(grvListItem.Rows[0].Cells[23].Text));
                    cmdD.Parameters.AddWithValue("@ITEMGRPID", Convert.ToInt32(grvListItem.Rows[0].Cells[7].Text));
                    cmdD.Parameters.AddWithValue("@PRQTY", Convert.ToInt32(grvListItem.Rows[0].Cells[17].Text));
                    cmdD.Parameters.AddWithValue("@UOM", grvListItem.Rows[0].Cells[8].Text == "&nbsp;" ? 1 : Convert.ToInt32(grvListItem.Rows[0].Cells[8].Text));
                    cmdD.Parameters.AddWithValue("@RATE", Convert.ToString(grvListItem.Rows[0].Cells[18].Text));
                    cmdD.Parameters.AddWithValue("@DELIDT", setDateFormat(DateTime.Now.ToString(), true));
                    cmdD.Parameters.AddWithValue("@CAMOUNT", Convert.ToString(grvListItem.Rows[0].Cells[25].Text));
                    cmdD.Parameters.AddWithValue("@GLCD", Convert.ToString(grvListItem.Rows[0].Cells[11].Text));
                    cmdD.Parameters.AddWithValue("@CSTCENTCD", Convert.ToString(grvListItem.Rows[0].Cells[12].Text));
                    cmdD.Parameters.AddWithValue("@PRFCNT", Convert.ToString(grvListItem.Rows[0].Cells[13].Text));
                    cmdD.Parameters.AddWithValue("@ASSETCD", Convert.ToString(grvListItem.Rows[0].Cells[19].Text) == "&nbsp;" ? "" : Convert.ToString(grvListItem.Rows[0].Cells[19].Text));
                    cmdD.Parameters.AddWithValue("@PRBY", Convert.ToString(grvListItem.Rows[0].Cells[20].Text));
                    cmdD.Parameters.AddWithValue("@ITEMTEXT", Convert.ToString(grvListItem.Rows[0].Cells[14].Text) + " " + Convert.ToString(grvListItem.Rows[0].Cells[24].Text));
                    cmdD.Parameters.AddWithValue("@PARTREQNO", 0);
                    cmdD.CommandType = CommandType.StoredProcedure;
                    cmdD.Connection.Open();
                    cmdD.ExecuteNonQuery();
                    cmdD.Connection.Close();

                }
                return "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "";
            }
        }

        public DataTable GetNewList(string LISTTYPE, string LISTDESC)
        {
            MainClass obj = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cd = new SqlCommand("SP_MST_LISTS_CRUD", ConnSherpa);
            try
            {
                cd.Parameters.AddWithValue("@LISTTYPE", LISTTYPE);
                cd.Parameters.AddWithValue("@LISTDESC", LISTDESC);
                cd.CommandType = CommandType.StoredProcedure;
                cd.Connection.Open();
                cd.ExecuteNonQuery();
                SqlDataReader dr = cd.ExecuteReader();
                dt.Load(dr);
                cd.Connection.Close();

            }
            catch (Exception ex)
            {
                cd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //04-02-2023 swetal start
        public int SaveNewList(string LISTTYPE, string LISTTYPEFOR, string DESCRIPTION, string STATUS)
        {
            MainClass objMainClass = new MainClass();
            int lresult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlCommand cd = new SqlCommand("SP_MST_LISTS_CRUD ", ConnSherpa);
                    cd.Parameters.AddWithValue("@LISTTYPE", LISTTYPE);
                    cd.Parameters.AddWithValue("@LISTTYPEFOR", LISTTYPEFOR);
                    cd.Parameters.AddWithValue("@LISTDESC", DESCRIPTION);
                    cd.Parameters.AddWithValue("@STATUS", STATUS);
                    cd.Parameters.AddWithValue("@ACTION", "INSERT");
                    cd.CommandType = CommandType.StoredProcedure;
                    cd.Connection.Open();
                    lresult = cd.ExecuteNonQuery();
                    cd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return lresult;
        }

        //06-02-2023 swetal
        public DataTable GetNewData(string ACTION, int BRAND_ID, string MODEL_NAME)
        {
            MainClass obj = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cd = new SqlCommand("SP_MST_Model_List", ConnSherpa);
            try
            {
                cd.Parameters.AddWithValue("@ACTION", ACTION);
                cd.Parameters.AddWithValue("@BRAND_ID", BRAND_ID);
                cd.Parameters.AddWithValue("@MODEL_NAME", MODEL_NAME);
                cd.CommandType = CommandType.StoredProcedure;
                cd.Connection.Open();
                cd.ExecuteNonQuery();
                SqlDataReader dr = cd.ExecuteReader();
                dt.Load(dr);
                cd.Connection.Close();

            }
            catch (Exception ex)
            {
                cd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        //07-02-2023 swetal start
        public int SaveNewModelList(int BRAND_ID, string MODEL_NAME, string DISP_NAME, string DISP_NAME2, string DISP_NAME3, int ITEMID, string ISACTIVE, int userid)
        {
            MainClass objMainClass = new MainClass();
            int mresult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlCommand cd = new SqlCommand("SP_MST_Model_List", ConnSherpa);
                    cd.Parameters.AddWithValue("@BRAND_ID", BRAND_ID);
                    cd.Parameters.AddWithValue("@MODEL_NAME", MODEL_NAME);
                    cd.Parameters.AddWithValue("@DISP_NAME", DISP_NAME);
                    cd.Parameters.AddWithValue("@DISP_NAME2", DISP_NAME2);
                    cd.Parameters.AddWithValue("@DISP_NAME3", DISP_NAME3);
                    cd.Parameters.AddWithValue("@ITEMID", ITEMID);
                    cd.Parameters.AddWithValue("@ISACTIVE", ISACTIVE);
                    cd.Parameters.AddWithValue("@CREATEBY", userid);
                    cd.Parameters.AddWithValue("@ACTION", "INSERT");
                    cd.CommandType = CommandType.StoredProcedure;
                    cd.Connection.Open();
                    mresult = cd.ExecuteNonQuery();
                    cd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return mresult;
        }
        //end

        public int UPDATEORDERNO(int ID, string userid, string ORDERNO)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE UNLIST DETAIL...
                    SqlCommand cmdM = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@RESERVEDBY", Convert.ToInt32(userid));
                    cmdM.Parameters.AddWithValue("@ORDERNO", ORDERNO);
                    cmdM.Parameters.AddWithValue("@ACTION", "UPDATERESERVED");
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public bool CheckSOnumber(string SONO)
        {
            SqlCommand cmd = new SqlCommand("SP_CHECK_SO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@SONumber", strConvertZeroPadding(SONO));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                // cmd.ExecuteNonQuery();

                object obj = cmd.ExecuteScalar();
                if ((obj) != null)
                {
                    return true;
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return false;
        }

        public int BulkWebsiteSoCreation(string SOCREATIONJSON, string ACTION = "BULKINSERT")
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_BULKSOCRUDOPERATION", ConnSherpa);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    cmd.Parameters.AddWithValue("@SOCREATIONJSON", SOCREATIONJSON);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    iResult = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
            }
            return iResult;
        }

        //09-02-2023  swetal start
        public DataTable GetExistsPannos(string PANNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@PANNO", PANNO);
                cmd.Parameters.AddWithValue("@ACTION", "EXISTDATA");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        // end 

        //10-02-2023 SWETAL START
        public DataTable GetexistsAdharNo(string AADHARNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@AADHARNO", AADHARNO);
                cmd.Parameters.AddWithValue("@ACTION", "EXISTSADHARNO");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        //END

        //10-02-2023 swetal start
        public DataTable GetExistsGSTNO(string GSTNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@GSTNO", GSTNO);
                cmd.Parameters.AddWithValue("@ACTION", "EXISTGST");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        //end

        //14-02-2023 SWETAL START
        public DataTable GetVendorNAME(string NAME1)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@NAME1", NAME1);
                cmd.Parameters.AddWithValue("@ACTION", "EXISTVENDORNAME");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        //END

        //14-02-2023 swetal
        public DataTable GetPaymentTo(string SHORTNM)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@SHORTNM", SHORTNM);
                cmd.Parameters.AddWithValue("@ACTION", "EXIXTSPAYMENTNO");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        //end

        //16-02-2023 SWETAL START 
        public int UpdateContactnNo(int ID, string CONTACTNO, string CONTACTNO2, string CONTACTNO3, string ACTION)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            SqlCommand cmd = new SqlCommand("SP_DEALER_MASTER", ConnSherpa);
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                    cmd.Parameters.AddWithValue("@CONTACTNO2", CONTACTNO2);
                    cmd.Parameters.AddWithValue("@CONTACTNO3", CONTACTNO3);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    result = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
        //END 
        public DataTable GetSalesInvoice(string ACTION, string FROMDATE, string TODATE, string PLANTCD,
                string LOCCD, string SINO, string TRANTYPE, string SALESCHANNEL, string REFNO, int SEGMENT,
                string DISTCHNL, string JOBID, string SONO, string CUSTPARTDESC2)
        {
            MainClass obj = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cd = new SqlCommand("SP_MST_SALESINVOICE", ConnSherpa);
            try
            {
                cd.Parameters.AddWithValue("@ACTION", ACTION);
                cd.Parameters.AddWithValue("@CMPID", this.intCmpId);
                cd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cd.Parameters.AddWithValue("@LOCCD", LOCCD);
                cd.Parameters.AddWithValue("@SINO", SINO);
                cd.Parameters.AddWithValue("@TRANTYPE", TRANTYPE);
                cd.Parameters.AddWithValue("@SALESCHANNEL", SALESCHANNEL);
                cd.Parameters.AddWithValue("@REFNO", REFNO);
                cd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
                cd.Parameters.AddWithValue("@JOBID", JOBID);
                cd.Parameters.AddWithValue("@SONO", SONO);
                cd.Parameters.AddWithValue("@CUSTPARTDESC2", CUSTPARTDESC2);
                cd.CommandType = CommandType.StoredProcedure;
                cd.Connection.Open();
                cd.CommandTimeout = 500;
                cd.ExecuteNonQuery();
                SqlDataReader dr = cd.ExecuteReader();
                dt.Load(dr);
                cd.Connection.Close();
            }
            catch (Exception ex)
            {
                cd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int SaveMobileBrandList(string BRANDNAME, int ISACTIVE, int userid)
        {
            MainClass objMainClass = new MainClass();
            int lresult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlCommand cd = new SqlCommand("SP_MST_MOBILEBRAND", ConnSherpa);
                    cd.Parameters.AddWithValue("@BRANDNAME", BRANDNAME);
                    cd.Parameters.AddWithValue("@ISACTIVE", ISACTIVE);
                    cd.Parameters.AddWithValue("@CREATEBY", userid);
                    cd.Parameters.AddWithValue("@ACTION", "INSERT");
                    cd.CommandType = CommandType.StoredProcedure;
                    cd.Connection.Open();
                    lresult = cd.ExecuteNonQuery();
                    cd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return lresult;
        }

        //21-02-2023 SWETAL
        public DataTable GetMobileBrand(string ACTION, string BRANDNAME)
        {
            MainClass obj = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cd = new SqlCommand("SP_MST_MOBILEBRAND", ConnSherpa);
            try
            {
                cd.Parameters.AddWithValue("@ACTION", ACTION);
                cd.Parameters.AddWithValue("@BRANDNAME", BRANDNAME);
                cd.CommandType = CommandType.StoredProcedure;
                cd.Connection.Open();
                cd.ExecuteNonQuery();
                SqlDataReader dr = cd.ExecuteReader();
                dt.Load(dr);
                cd.Connection.Close();
            }
            catch (Exception ex)
            {
                cd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        //END

        public int BulkAsinCreation(string HARDWAREREPORTJSON, string ACTION)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SP_MST_SKU_ASINWISE", ConnSherpa);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    cmd.Parameters.AddWithValue("@HARDWAREREPORTJSON", HARDWAREREPORTJSON);
                    cmd.Parameters.AddWithValue("@ACTION", ACTION);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    result = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
                return result;
            }
        }

        public bool CheckCustomerName(string Name = "", string Name2 = "", string PAN = "", string Adhar = "", string GST = "")
        {
            string MAXPR = string.Empty;
            SqlCommand cmd = new SqlCommand("SP_Check_Customer_Name", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Name2", Name2);
                cmd.Parameters.AddWithValue("@PAN", PAN);
                cmd.Parameters.AddWithValue("@Adhar", Adhar);
                cmd.Parameters.AddWithValue("@GST", GST);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                object obj = cmd.ExecuteScalar();
                if ((obj) != null)
                {
                    return true;
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return false;
        }

        public string MAXCUSCODENO(string CustType)
        {
            string MAXPR = string.Empty;
            SqlCommand cmd = new SqlCommand("SP_GET_MAX_CUCO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CustType", CustType);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();

                object obj = cmd.ExecuteScalar();
                if ((obj) != null)
                {
                    MAXPR = obj.ToString();
                    MAXPR = Convert.ToString(Convert.ToInt64(MAXPR) + 1);
                }
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return MAXPR;
        }

        public DataSet CustomerListByCode(string CustomerCode, int ImageID = 0, string ACTION = "SELECTBYCODE")
        {
            MainClass objMainClass = new MainClass();
            DataSet dt = new DataSet();
            try
            {
                SqlCommand cd = new SqlCommand("SP_MST_CUSTOMER_CRUD", ConnSherpa);
                cd.Parameters.AddWithValue("@CMPID", this.intCmpId);
                cd.Parameters.AddWithValue("@CUSTCODE", strConvertZeroPadding(CustomerCode));
                cd.Parameters.AddWithValue("@ImageID", ImageID);
                cd.Parameters.AddWithValue("@ACTION", ACTION);
                cd.CommandType = CommandType.StoredProcedure;
                cd.Connection.Open();
                cd.ExecuteNonQuery();
                //SqlDataReader dr = cd.ExecuteReader();
                SqlDataAdapter da = new SqlDataAdapter(cd);
                da.Fill(dt);
                cd.Connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dt;
        }

        public void DeleteCustomerImage(string VENDCODE, int IMAGETYPE, string ACTION)
        {
            SqlCommand cmd = new SqlCommand("SP_MST_CUSTOMER_CRUD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@VENDCODE", strConvertZeroPadding(VENDCODE));
                cmd.Parameters.AddWithValue("@IMAGETYPE", IMAGETYPE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public int InsertCustomerImage(string CUSTCODE, int IMAGETYPE, byte[] IMAGE, int STATUS, string ACTION, string EXTENSION)
        {
            //INSERTIMAGE
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_MST_CUSTOMER_CRUD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CUSTCODE", strConvertZeroPadding(CUSTCODE));
                cmd.Parameters.AddWithValue("@IMAGETYPE", IMAGETYPE);
                cmd.Parameters.AddWithValue("@IMAGE", IMAGE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@EXTENSION", EXTENSION);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public string saveCustomerdetails(string CustType, string CustCode, string Title, string Name1, string Name2, string Shortnm, string CURRCODE, string EXVENDTYPE
           , string CUSTCAT, string CUSTGRP, string ZONE, string BANKNAME, string AADHARNO, string PANNO, string ACCOUNTNO, string IFSCCODE, int cretatedby
            , string ADDR1, string ADDR2, string ADDR3, string CITY, string STCD, string CNCD, string POSTALCODE, string CONTACTPERSON
           , string CONTACTNO, string MOBILE, string EMAIL, DataTable gv
           , string CSTNO, string CSTDT, string LSTNO, string LSTDT, string STREGNO, string ECCNO, string EXREGNO, string EXRANGE, string EXDIVISION, string EXCOMM, string OLDACCODE, string WEBSITE, string REGION
           , byte[] IDPROOF, string IDPROOFTYPE
           , byte[] PAN, string PANTYPE
           , byte[] CHEQUE, string CHEQUETYPE
           , byte[] GSTCERTI, string GSTCERTITYPE, string ACCNTTYPE, string GSTNO)
        {
            CustCode = strConvertZeroPadding(CustCode);
            string NewCustCode = string.Empty;
            SqlCommand cmd = new SqlCommand("SP_MST_CUSTOMER_CRUD", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@ACTION", "INSERT");
                cmd.Parameters.AddWithValue("@CMPID", this.intCmpId);
                cmd.Parameters.AddWithValue("@CUSTCODE", CustCode);
                cmd.Parameters.AddWithValue("@CUSTTYPE", CustType);
                cmd.Parameters.AddWithValue("@TITLE", Title);
                cmd.Parameters.AddWithValue("@NAME1", Name1);
                cmd.Parameters.AddWithValue("@NAME2", Name2);
                cmd.Parameters.AddWithValue("@SHORTNM", Shortnm);
                cmd.Parameters.AddWithValue("@INDKEY", "S");
                cmd.Parameters.AddWithValue("@RECAC", "10030001");
                cmd.Parameters.AddWithValue("@CURRCODE", CURRCODE);
                cmd.Parameters.AddWithValue("@PAYMETHOD", "5");
                cmd.Parameters.AddWithValue("@PAYBLKKEY", "F");
                cmd.Parameters.AddWithValue("@EXVENDTYPE", EXVENDTYPE);
                cmd.Parameters.AddWithValue("@CUSTCAT", CUSTCAT);
                cmd.Parameters.AddWithValue("@CUSTGRP", CUSTGRP);
                cmd.Parameters.AddWithValue("@ZONE", ZONE);
                cmd.Parameters.AddWithValue("@CREATEBY", cretatedby);
                cmd.Parameters.AddWithValue("@CREATEDATE", DateTime.Now);
                cmd.Parameters.AddWithValue("@STATUS", 2);
                cmd.Parameters.AddWithValue("@NODELI", 0);
                cmd.Parameters.AddWithValue("@BANKNAME", BANKNAME);
                cmd.Parameters.AddWithValue("@ACCNTTYPE", ACCNTTYPE);
                cmd.Parameters.AddWithValue("@AADHARNO", AADHARNO);
                cmd.Parameters.AddWithValue("@ACCOUNTNO", ACCOUNTNO);
                cmd.Parameters.AddWithValue("@IFSCCODE", IFSCCODE);
                cmd.Parameters.AddWithValue("@PANNO", PANNO);
                cmd.Parameters.AddWithValue("@OLDACCODE", OLDACCODE);
                cmd.Parameters.AddWithValue("@WEBSITE", WEBSITE);
                cmd.Parameters.AddWithValue("@REGION", REGION);
                cmd.Parameters.AddWithValue("@GSTNO", GSTNO);


                if (CSTNO != "")
                {
                    cmd.Parameters.AddWithValue("@CSTNO", CSTNO);
                    cmd.Parameters.AddWithValue("@CSTDT", Convert.ToDateTime(CSTDT));
                }

                if (LSTNO != "")
                {
                    cmd.Parameters.AddWithValue("@LSTNO", LSTNO);
                    cmd.Parameters.AddWithValue("@LSTDT", Convert.ToDateTime(LSTDT));
                }
                cmd.Parameters.AddWithValue("@STREGNO", STREGNO);
                cmd.Parameters.AddWithValue("@ECCNO", ECCNO);
                cmd.Parameters.AddWithValue("@EXREGNO", EXREGNO);
                cmd.Parameters.AddWithValue("@EXRANGE", EXRANGE);
                cmd.Parameters.AddWithValue("@EXDIVISION", EXDIVISION);
                cmd.Parameters.AddWithValue("@EXCOMM", EXCOMM);




                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                //cmd.ExecuteNonQuery();

                object obj = cmd.ExecuteScalar();
                if ((obj) != null)
                {
                    NewCustCode = obj.ToString();
                    //NewCustCode = Convert.ToString(Convert.ToInt64(NewCustCode) + 1);
                    NewCustCode = strConvertZeroPadding(NewCustCode);
                }
                cmd.Connection.Close();


                //ADDRESS 
                cmd = new SqlCommand("SP_MST_CUSTOMER_CRUD", ConnSherpa);
                cmd.Parameters.AddWithValue("@ACTION", "INSERTADDRESS");
                cmd.Parameters.AddWithValue("@CMPID", this.intCmpId);
                cmd.Parameters.AddWithValue("@REFID", NewCustCode);
                cmd.Parameters.AddWithValue("@CUSTCODE", NewCustCode);
                cmd.Parameters.AddWithValue("@REFTYPE", "CM");
                cmd.Parameters.AddWithValue("@ADDOF", "");
                cmd.Parameters.AddWithValue("@CustType", CustType);
                cmd.Parameters.AddWithValue("@ADDR1", ADDR1);
                cmd.Parameters.AddWithValue("@ADDR2", ADDR2);
                cmd.Parameters.AddWithValue("@ADDR3", ADDR3);
                cmd.Parameters.AddWithValue("@CITY", CITY);
                cmd.Parameters.AddWithValue("@STCD", STCD);
                cmd.Parameters.AddWithValue("@CNCD", CNCD);
                cmd.Parameters.AddWithValue("@POSTALCODE", POSTALCODE);
                cmd.Parameters.AddWithValue("@CONTACTPERSON", CONTACTPERSON);
                cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILE);
                cmd.Parameters.AddWithValue("@EMAILID", EMAIL);
                cmd.Parameters.AddWithValue("@CREATEBY", cretatedby);
                cmd.Parameters.AddWithValue("@CREATEDATE", DateTime.Now);
                cmd.Parameters.AddWithValue("@WEBSITE", WEBSITE);





                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                for (int i = 0; i < gv.Rows.Count; i++)
                {

                    SqlCommand cmdD = new SqlCommand("SP_MST_CUSTOMER_CRUD", ConnSherpa);
                    cmdD.Parameters.AddWithValue("ACTION", "INSERTComm");
                    cmdD.Parameters.AddWithValue("@comCMPID", this.intCmpId);
                    cmdD.Parameters.AddWithValue("@comSRNO", "1");
                    cmdD.Parameters.AddWithValue("@comCUSTCODE", NewCustCode);
                    cmdD.Parameters.AddWithValue("@comDESIGNATION", Convert.ToString(gv.Rows[i]["DesignationID"]));
                    cmdD.Parameters.AddWithValue("@comCONTNAME", Convert.ToString(gv.Rows[i]["Name"]));
                    cmdD.Parameters.AddWithValue("@comCONTNO", Convert.ToString(gv.Rows[i]["ContactNo"]));
                    cmdD.Parameters.AddWithValue("@comEMAILID", Convert.ToString(gv.Rows[i]["Email"]));
                    cmdD.Parameters.AddWithValue("@comCREATEBY", cretatedby);
                    cmdD.Parameters.AddWithValue("@comCREATEDATE", DateTime.Now);
                    cmdD.CommandType = CommandType.StoredProcedure;
                    cmdD.Connection.Open();
                    cmdD.ExecuteNonQuery();
                    cmdD.Connection.Close();

                }

                if (IDPROOF != null)
                {
                    DeleteCustomerImage(NewCustCode, 11315, "DELETEIMAGE");
                    int result1 = InsertCustomerImage(NewCustCode, 11315, IDPROOF, 2, "INSERTIMAGE", IDPROOFTYPE);
                }

                if (PAN != null)
                {
                    DeleteCustomerImage(NewCustCode, 11316, "DELETEIMAGE");
                    int result2 = InsertCustomerImage(NewCustCode, 11316, PAN, 2, "INSERTIMAGE", PANTYPE);
                }

                if (CHEQUE != null)
                {
                    DeleteCustomerImage(NewCustCode, 11317, "DELETEIMAGE");
                    int result3 = InsertCustomerImage(NewCustCode, 11317, CHEQUE, 2, "INSERTIMAGE", CHEQUETYPE);
                }

                //if (SHOP != null)
                //{
                //    DeleteCustomerImage(NewCustCode, 11318, "DELETEIMAGE");
                //    int result4 = InsertVendImage(NewCustCode, 11318, SHOP, 2, "INSERTIMAGE", ".jpeg");
                //}

                if (GSTCERTI != null)
                {
                    DeleteCustomerImage(NewCustCode, 11824, "DELETEIMAGE");
                    int result4 = InsertCustomerImage(NewCustCode, 11824, GSTCERTI, 2, "INSERTIMAGE", GSTCERTITYPE);
                }

                //if (MSMECERTI != null)
                //{
                //    DeleteCustomerImage(NewCustCode, 11825, "DELETEIMAGE");
                //    int result4 = InsertVendImage(NewCustCode, 11825, MSMECERTI, 2, "INSERTIMAGE", MSMECERTITYPE);
                //}

            }

            catch (Exception ex)
            {

                throw ex;
            }
            return NewCustCode;
        }

        public DataTable CustomerList(string CustomerCode, string custtypeid, string Name, string Zone, string City, string PostalCode)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            try
            {

                SqlCommand cd = new SqlCommand("SP_MST_CUSTOMER_CRUD", ConnSherpa);
                cd.Parameters.AddWithValue("@CMPID", this.intCmpId);
                cd.Parameters.AddWithValue("@CUSTCODE", (CustomerCode != "" ? strConvertZeroPadding(CustomerCode) : ""));
                cd.Parameters.AddWithValue("@custtypeid", custtypeid);
                cd.Parameters.AddWithValue("@NAME1", Name);
                cd.Parameters.AddWithValue("@ZONE", Zone);
                cd.Parameters.AddWithValue("@CITY", City);
                cd.Parameters.AddWithValue("@POSTALCODE", PostalCode);
                cd.Parameters.AddWithValue("@ACTION", "SELECTALL");
                cd.CommandType = CommandType.StoredProcedure;
                cd.Connection.Open();
                cd.ExecuteNonQuery();
                SqlDataReader dr = cd.ExecuteReader();
                dt.Load(dr);
                cd.Connection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return dt;
        }

        public DataTable GetShopName(string DEALERNAME)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DEALER_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DEALERNAME", DEALERNAME);
                cmd.Parameters.AddWithValue("@ACTION", "SHOPNAME");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetIMEIDetailFromJobID(string JOBID)
        {
            DataTable dt = new DataTable();
            MainClass objMain = new MainClass();
            SqlCommand cmd = new SqlCommand("SP_IMEIDETAILFROMJOBNO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", JOBID);
                cmd.Parameters.AddWithValue("@CMPID", objMain.intCmpId);
                cmd.Parameters.AddWithValue("@ACTION", "IMEIDETAIL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetJangadListing(string FROMDATE, string TODATE, string VENDORNAME, string LISTINGFILTER, int status,
            int userid = 0, int ID = 0, string MNLAPRREASON = "", string IMEINO = "")
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_JANGADKROLISTING", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@FROMDATE", (FROMDATE.Length > 0 ? setDateFormat(FROMDATE, true) : ""));
                cmd.Parameters.AddWithValue("@TODATE", (TODATE.Length > 0 ? setDateFormat(TODATE, true) : ""));
                cmd.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@MNLAPRREASON", MNLAPRREASON);
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@LISTINGFILTER", LISTINGFILTER);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTALL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetJangadKROReturnListing(string VENDORNAME, string LISTINGFILTER, int status,
            int userid = 0, int ID = 0, string IMEINO = "")
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_JANGADKRORETURNLISTING", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 600;
                cmd.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@LISTINGFILTER", LISTINGFILTER);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTALL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int JangadRelisting(int ID, int MAXYDAYS, int STATUS, string NGEAPRV, double MOBILENEWRATE, int ISAPPROVEDFK, int ISAPPROVEDAMZ, int ISAPPROVEDWEB, double FKAMT, double AMZAMT, double WEBAMT, double FKPER,
            double AMZPER, double WEBPER, double PURFKAMT, double PURAMZAMT, double PURWEBAMT, double PURFKPER, double PURAMZPER, double PURWEBPER, double PURCHASEPERONVENDORPRICE,
            int ISPURAPPROVEDFK, int ISPURAPPROVEDAMZ, int ISPURAPPROVEDWEB, Decimal FinalApproveListingAmount, double VENDORPRICE, int CREATEBY)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_JANGADKROLISTING", ConnSherpa);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@MAXYDAYS", MAXYDAYS);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.Parameters.AddWithValue("@NGEAPRV", NGEAPRV);
                    cmd.Parameters.AddWithValue("@MOBILENEWRATE", MOBILENEWRATE);
                    cmd.Parameters.AddWithValue("@ISAPPROVEDFK", ISAPPROVEDFK);
                    cmd.Parameters.AddWithValue("@ISAPPROVEDAMZ", ISAPPROVEDAMZ);
                    cmd.Parameters.AddWithValue("@ISAPPROVEDWEB", ISAPPROVEDWEB);
                    cmd.Parameters.AddWithValue("@FKAMT", FKAMT);
                    cmd.Parameters.AddWithValue("@AMZAMT", AMZAMT);
                    cmd.Parameters.AddWithValue("@WEBAMT", WEBAMT);
                    cmd.Parameters.AddWithValue("@FKPER", FKPER);
                    cmd.Parameters.AddWithValue("@AMZPER", AMZPER);
                    cmd.Parameters.AddWithValue("@WEBPER", WEBPER);
                    cmd.Parameters.AddWithValue("@PURFKAMT", PURFKAMT);
                    cmd.Parameters.AddWithValue("@PURAMZAMT", PURAMZAMT);
                    cmd.Parameters.AddWithValue("@PURWEBAMT", PURWEBAMT);
                    cmd.Parameters.AddWithValue("@PURFKPER", PURFKPER);
                    cmd.Parameters.AddWithValue("@PURAMZPER", PURAMZPER);
                    cmd.Parameters.AddWithValue("@PURWEBPER", PURWEBPER);
                    cmd.Parameters.AddWithValue("@PURCHASEPERONVENDORPRICE", PURCHASEPERONVENDORPRICE);
                    cmd.Parameters.AddWithValue("@ISPURAPPROVEDFK", ISPURAPPROVEDFK);
                    cmd.Parameters.AddWithValue("@ISPURAPPROVEDAMZ", ISPURAPPROVEDAMZ);
                    cmd.Parameters.AddWithValue("@ISPURAPPROVEDWEB", ISPURAPPROVEDWEB);
                    cmd.Parameters.AddWithValue("@FinalApproveListingAmount", FinalApproveListingAmount);
                    cmd.Parameters.AddWithValue("@VENDORPRICE", VENDORPRICE);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@ACTION", "UPDATERELIST");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    result = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
                return result;
            }
        }



        public int VendortoKro(string ACTION, int ID, int JANGADMAXDAYLIMIT, int UPDATEBY)
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    SqlCommand cmdM = new SqlCommand("SP_MST_DELEARTOKRO", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@ACTION", ACTION);
                    cmdM.Parameters.AddWithValue("@ID", ID);
                    cmdM.Parameters.AddWithValue("@JANGADMAXDAYLIMIT", JANGADMAXDAYLIMIT);
                    cmdM.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }
        //end

        //fetch vendor code
        public DataTable FetchDealerId(int ID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_DELEARTOKRO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }
        public DataTable GetExistsImeiNo(string imeino, string LISTINGTYPE, string ACTION, int ID = 0)
        {
            DataTable dt = new DataTable();
            SqlCommand cd = new SqlCommand("SP_MOBEX_SELLER", ConnSherpa);
            try
            {
                cd.Parameters.AddWithValue("@IMEINO", imeino);
                cd.Parameters.AddWithValue("@LISTINGTYPE", LISTINGTYPE);
                cd.Parameters.AddWithValue("@ACTION", ACTION);
                cd.Parameters.AddWithValue("@ID", ID);
                cd.CommandType = CommandType.StoredProcedure;
                cd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cd);
                da.Fill(dt);
                cd.Connection.Close();
            }
            catch (Exception ex)
            {
                cd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetLogisticData(int CMPID, string TRANTYPE, string FROMDATE, string TODATE, string SINO, int SISRNO, string APPNAME, string PLANT, int STATUS, string ACTION)
        {
            //SP_LOGISTIC_ASSIGN
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LOGISTIC_ASSIGN", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@TRANTYPE", TRANTYPE);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SINO", SINO == "" ? "" : strConvertZeroPadding(SINO));
                cmd.Parameters.AddWithValue("@SISRNO", SISRNO);

                cmd.Parameters.AddWithValue("@APPNAME", APPNAME);
                cmd.Parameters.AddWithValue("@PLANT", PLANT);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);

                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateAppkey(string JWTTOKEN, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_LOGISTIC_ASSIGN", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JWTTOKEN", JWTTOKEN);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }
        public int UpdateLogisticJob(int CMPID, string JOBID, string FWAYBILLNO, string FWAYBILLSTATUS, string FTRANNAME, int CREATEBY, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_LOGISTIC_ASSIGN", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", JOBID);
                cmd.Parameters.AddWithValue("@FWAYBILLNO", FWAYBILLNO);
                cmd.Parameters.AddWithValue("@FWAYBILLSTATUS", FWAYBILLSTATUS);
                cmd.Parameters.AddWithValue("@FTRANNAME", FTRANNAME);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return i;
        }

        public int UpdateReturnVendorBDOStatus(string returnreason, int STATUS, int USERID, int ID)
        {
            int result = 0;
            MainClass objMainClass = new MainClass();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_RETURNCRUDOPERATION", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CREATEBY", USERID);
                cmd.Parameters.AddWithValue("@RETURNREASON", returnreason);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", "UPDATERETURNBDOVENDORDETAIL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                result = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return result;
        }

        public DataTable GetRelistDetail(string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_RELISTDETAL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetAutoUnlistedDetail(string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_AUTOUNLISTEDDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateReturnFlaginJobid(int CMPID, string JOBID, int USERID, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_SO_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SONO", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@CREATEBY", USERID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }




        public int ProductBulkFBAUpdate(string AllocateinFBADetailJSON, int UPDATEBY, string mode = "UPDATEFBAALLOTMENT")
        {
            MainClass objMainClass = new MainClass();
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    #region UPDATE FBA DETAIL...
                    SqlCommand cmdM = new SqlCommand("BULKFBAALLOCATION", ConnSherpa);
                    cmdM.Parameters.AddWithValue("@AllocateinFBADetailJSON", AllocateinFBADetailJSON);
                    cmdM.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                    cmdM.Parameters.AddWithValue("@ACTION", mode);
                    cmdM.CommandType = CommandType.StoredProcedure;
                    cmdM.CommandTimeout = 5000;
                    cmdM.Connection.Open();
                    result = cmdM.ExecuteNonQuery();
                    cmdM.Connection.Close();
                    #endregion
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result;
        }

        public DataTable GetAccountNumber(string ACCOUNTNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACCOUNTNO", ACCOUNTNO);
                cmd.Parameters.AddWithValue("@ACTION", "EXISTACCOUNTNO");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetMessageText(int CMPID, int TRIGGERID, int STAGEID, int MSGID, string SEGMENT, string DISTCHNL)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_MESSAGETEXT", ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", CMPID);
            cmd.Parameters.AddWithValue("@STAGEID", STAGEID);
            cmd.Parameters.AddWithValue("@MSGID", MSGID);
            cmd.Parameters.AddWithValue("@TRIGGERID", TRIGGERID);
            cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
            cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public void SaveNotification(int CMPID, string SENDFROM, string PASSWORD, string SENDTO, string SENDCC, string SUBJECT, string MESSAGE, string PORT, string DOCTYPE, string DOCNO, string NOTTYPE, string URL, int SENTBY, string ACTION)
        {
            SqlCommand cmd = new SqlCommand("SP_NOTIFICATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SENDFROM", SENDFROM);
                cmd.Parameters.AddWithValue("@PASSWORD", PASSWORD);
                cmd.Parameters.AddWithValue("@SENDTO", SENDTO);
                cmd.Parameters.AddWithValue("@SENDCC", SENDCC);
                cmd.Parameters.AddWithValue("@SUBJECT", SUBJECT);
                cmd.Parameters.AddWithValue("@MESSAGE", MESSAGE);
                cmd.Parameters.AddWithValue("@PORT", PORT);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                cmd.Parameters.AddWithValue("@NOTTYPE", NOTTYPE);
                cmd.Parameters.AddWithValue("@URL", URL);
                cmd.Parameters.AddWithValue("@CREATEBY", SENTBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public DataTable GetDealerOutStanding(int CMPID, string FROMDATE, string TODATE, int DEALERID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_JANGADOUTSTANDING_LEDGER", ConnSherpa);
            cmd.CommandTimeout = 500;
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@DOCFROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@DOCTODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@DEALERID", DEALERID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }


        public DataTable GetDealerData(string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_CUSTOMER_CRUD", ConnSherpa);
            cmd.CommandTimeout = 800;
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return dt;
        }

        public DataTable GetFBAFBMReportData(int CMPID, string FROMDATE, string TODATE, int LISTINGTYPE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_FBAFBM", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@MTDTODATE", setDateFormat(TODATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@LISTINGTYPE", LISTINGTYPE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.CommandTimeout = 500;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int RemovefromFBA(int ID, int CREATEBY)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_JANGADKROLISTING", ConnSherpa);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    cmd.Parameters.AddWithValue("@ID", ID);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@ACTION", "REMOVEFROMFBA");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    result = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
                return result;
            }
        }


        public DataTable GetMakeModelLaunchYear(string MAKE, string MODEL, string mode)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_MAKEMODEL_LAUNCHYEAR", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@MAKE", MAKE);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@ACTION", mode);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetSalesInvoiceforGenerateInsurance(int CMPID, string SINO, int INSURANCEALLOSTATUS = 12312)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SALESINVOICEDETAILFORINSURANCE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SINO", SINO);
                cmd.Parameters.AddWithValue("@INSURANCEALLOSTATUS", INSURANCEALLOSTATUS);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }


        public DataTable GetSalesInvoiceEachDetail(int CMPID, string SONO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SALESINVOICEDETAILFORINSURANCE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@ACTION", "SELECTONE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public int UpdateSalesInvoiceInsuranceStatus(int SRNO, string SONO, string INSURANCEINVOICEURL, string INSURANCEIMEIIMAGEURL,
            int INSURANCEALLOSTATUS, int CREATEBY)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("SP_SALESINVOICEDETAILFORINSURANCE", ConnSherpa);
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    cmd.Parameters.AddWithValue("@SRNO", SRNO);
                    cmd.Parameters.AddWithValue("@SONO", SONO);
                    cmd.Parameters.AddWithValue("@INSURANCEINVOICEURL", INSURANCEINVOICEURL);
                    cmd.Parameters.AddWithValue("@INSURANCEIMEIIMAGEURL", INSURANCEIMEIIMAGEURL);
                    cmd.Parameters.AddWithValue("@INSURANCEALLOSTATUS", INSURANCEALLOSTATUS);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@ACTION", "UPDATE");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    result = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    scope.Complete();
                    scope.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    throw ex;
                }
                return result;
            }
        }

        public int ChangeOrderStatus(int CMPID, string ENTITYID, string SONO, string JOBID, string STATUS, string APPNAME, string STATUSAPPNAME, string COMMENT, string COURIERSERVICE, string WAYBILLMSG, string WAYBILLNO, int USERID)
        {
            int iReturn = 0;

            try
            {
                DataTable dtAPI = new DataTable();
                dtAPI = GetWAData(APPNAME, 1, "GETWADATA");
                if (dtAPI.Rows.Count > 0)
                {
                    string URL = Convert.ToString(dtAPI.Rows[0]["OTHER"]);
                    WebsiteStatusData objWebsiteStatusData = new WebsiteStatusData();
                    objWebsiteStatusData.APPNAME = STATUSAPPNAME;
                    objWebsiteStatusData.COMMENT = COMMENT;
                    objWebsiteStatusData.COURIERSERVICE = COURIERSERVICE;
                    objWebsiteStatusData.ENTITYID = ENTITYID;
                    objWebsiteStatusData.STATUS = STATUS;
                    objWebsiteStatusData.WAYBILLMSG = WAYBILLMSG;
                    objWebsiteStatusData.WAYBILLNO = WAYBILLNO;


                    var client = new RestClient(URL);
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("KEY_NAME", "" + Convert.ToString(dtAPI.Rows[0]["KEYNAME"]) + "");
                    request.AddHeader("" + Convert.ToString(dtAPI.Rows[0]["KEYNAME"]) + "", "" + Convert.ToString(dtAPI.Rows[0]["TOKEN"]) + "");
                    request.AddHeader("APP_NAME", APPNAME);
                    request.AddHeader("Content-Type", "application/json");
                    var jsonInput = JsonConvert.SerializeObject(objWebsiteStatusData);
                    request.AddParameter("application/json", jsonInput, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string jsonconn = response.Content;
                        WebsiteStatusResponse objWebsiteStatusResponse = JsonConvert.DeserializeObject<WebsiteStatusResponse>(jsonconn);
                        if (objWebsiteStatusResponse.STATUSCODE == 200)
                        {
                            InsertOrderStatusLog(CMPID, JOBID, SONO, STATUS, WAYBILLNO, USERID, "INSERTDATA");
                            iReturn = 1;
                        }
                        else
                        {
                            iReturn = 0;
                        }
                    }
                    else
                    {
                        iReturn = 0;
                    }
                }
                else
                {
                    iReturn = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iReturn;
        }

        public void InsertOrderStatusLog(int CMPID, string JOBID, string SONO, string STATUS, string WAYBILL, int UPDATEBY, string ACTION)
        {
            SqlCommand cmd = new SqlCommand("SP_ORDERSTATUS_LOG", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", JOBID);
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@WAYBILL", WAYBILL);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteReader();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        //TRAN_MOBEXUPLOADED_LISTING

        public int insertListingLog(int CMPID, string SKU, decimal PRICE, decimal QTY, decimal AVAILQTY, decimal MRP, int STATUS, string PRICEJSON, string QTYJSON, int UPDATEBY, string MESSAGE, string ERROR, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_INSERTLISTINGLOG", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@SKU", SKU);
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PRICE", PRICE);
                cmd.Parameters.AddWithValue("@QTY", QTY);
                cmd.Parameters.AddWithValue("@AVAILQTY", AVAILQTY);
                cmd.Parameters.AddWithValue("@MRP", MRP);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@PRICEJSON", PRICEJSON);
                cmd.Parameters.AddWithValue("@QTYJSON", QTYJSON);
                cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
                cmd.Parameters.AddWithValue("@ERROR", ERROR);
                cmd.Parameters.AddWithValue("@MESSAGE", MESSAGE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }
        public DataTable OCRScanReport(int CMPID, string FROMDATE, string TODATE, string ACTION = "SELECTALL")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SCANSYSTEMSCANIMAGETRACKINGCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int CreateJObSpecification(int CMPID, string JOBID, string RAMSIZE, string ROMSIZE, string COLOR, int VOLTE_4G, string PRODGRADE, string MODELDESC, string SERIALNO, string SKU, string MRP, int CREATEBY, string LCDCOLOR, string PURGRADE, string ITEMCODE, int ADJUSTREQ)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_INSERTSPECIFICATION", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@RAMSIZE", RAMSIZE);
                cmd.Parameters.AddWithValue("@ROMSIZE", ROMSIZE);
                cmd.Parameters.AddWithValue("@COLOR", COLOR);
                cmd.Parameters.AddWithValue("@VOLTE_4G", VOLTE_4G);
                cmd.Parameters.AddWithValue("@PRODGRADE", PRODGRADE);
                cmd.Parameters.AddWithValue("@MODELDESC", MODELDESC);
                cmd.Parameters.AddWithValue("@SERIALNO", SERIALNO);
                cmd.Parameters.AddWithValue("@SKU", SKU);
                cmd.Parameters.AddWithValue("@MRP", MRP);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@LCDCOLOR", LCDCOLOR);
                cmd.Parameters.AddWithValue("@PURGRADE", PURGRADE);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@ADJUSTREQ", ADJUSTREQ);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iResult;
        }

        public DataTable SalesOrderRegister(int CMPID, int SEGMENT, string DISTCHNL,
            string PLANTCD, string LOCCD, string FROMDATE, string TODATE, string SONO,
            string JOBID, string CUSTPARTDESC2, string ITEMCODE, string REFNO,
            string ACTION = "SELECTALL"
            )
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SALESORDER_REGISTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@JOBID", (JOBID.Length > 0 ? strConvertZeroPadding(JOBID) : ""));
                cmd.Parameters.AddWithValue("@CUSTPARTDESC2", CUSTPARTDESC2);
                cmd.Parameters.AddWithValue("@REFNO", REFNO);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetExtendedWarrantyDetail(string STATUS,
           string ACTION = "SELECTALL"
           )
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SIMSTMOBILEVERIFIEDCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetVendorListingTransaction(int VENDORID)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_VENDOR_LISTING_TRANSACTION", ConnSherpa);
            try
            {
                cmd.CommandTimeout = 300;
                cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetMakeModelSuggestPriceHistorywithItemCode(int ID, int BRANDID, int MODELID, string FROMDATE, string TODATE)
        {
            //SP_MOBEX_SPECDATA
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SPECDATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@BRAND_ID", BRANDID);
                cmd.Parameters.AddWithValue("@MODEL_ID", MODELID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@ACTION", "SEARCHHISTORYWITHTIMECODE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetTRNAgeing(int CMPID, string DOCTYPE, string FROMDATE, string TODATE, string PLANTCODE, string LOCCD, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_TRNAGEING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCODE);
                cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetDeviationReport(int CMPID, string FROMDATE, string TODATE, string PLANTCD, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SALESDEVIATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataSet GetMosecUserActivityDetail(int CMPID, string FROMDATE, string TODATE)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand("SP_MOSECUSERACTIVITYDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return ds;
        }

        public int UpdateTEMPDETAIL(byte[] IMAGEDETAIL, string DOCNO)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_TEMPDETAIL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
                cmd.Parameters.AddWithValue("@IMAGEDETAIL", IMAGEDETAIL);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public DataTable CheckReturnableSoAvaibility(string SONO, string ACTION = "SOAVAIBILITY")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_RETURNABLEDCCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GenerateReturnableDc(string JOBID, string SONO, string itemcod, int LISTINGID, int userid, string ACTION = "ADD")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_RETURNABLEDCCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", JOBID);
                cmd.Parameters.AddWithValue("@LISTINGID", LISTINGID);
                cmd.Parameters.AddWithValue("@ITEMCODE", itemcod);
                cmd.Parameters.AddWithValue("@SONO", SONO);
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable ViewReturnableDcPDF(int DCNO, string ACTION = "SELECTONE")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_RETURNABLEDCCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DCNO", DCNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable CheckAssignmentAvaibility(string JOBID, string ACTION = "ASSIGNMENTAVAIBILITY")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_RETURNABLEDCCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@JOBID", JOBID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateReturnableDc(int DCNO, int CREATEBY)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_RETURNABLEDCCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DCNO", DCNO);
                cmd.Parameters.AddWithValue("@RETURNABLEUPDATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@ACTION", "RETURNABLEUPDATE");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                i = cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public DataTable GetReturnGenerateReturnableDc(int status, string ACTION = "SELECTALL")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_RETURNABLEDCCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@STATUS", status);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int InsertCheckList(int CMPID, string DOCDATE, string JOBID, string SERIALNO, string PROJECT, string STATUS, string GRADE, string MAKE, string MODEL, string COLOR, int CHECKBY, int VERIFIEDBY, int CREATEBY, GridView GRVCHECKLIST)
        {
            int iResult = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    string ID = string.Empty;
                    SqlCommand cmd = new SqlCommand("SP_LAPTOPCHECKLIST", ConnSherpa);
                    cmd.Parameters.AddWithValue("@CMPID", CMPID);
                    cmd.Parameters.AddWithValue("@DOCDATE", setDateFormat(DOCDATE, true));
                    cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                    cmd.Parameters.AddWithValue("@SERIALNO", SERIALNO);
                    cmd.Parameters.AddWithValue("@PROJECT", PROJECT);
                    cmd.Parameters.AddWithValue("@STATUS", STATUS);
                    cmd.Parameters.AddWithValue("@GRADE", GRADE);
                    cmd.Parameters.AddWithValue("@MAKE", MAKE);
                    cmd.Parameters.AddWithValue("@MODEL", MODEL);
                    cmd.Parameters.AddWithValue("@COLOR", COLOR);
                    cmd.Parameters.AddWithValue("@CHECKBY", CHECKBY);
                    cmd.Parameters.AddWithValue("@VERIFIEDBY", VERIFIEDBY);
                    cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                    cmd.Parameters.AddWithValue("@ID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    ID = Convert.ToString((cmd.Parameters["@ID"].Value));
                    cmd.Connection.Close();


                    if (ID != "" && ID != null && ID != string.Empty)
                    {
                        int iReturn = 0;
                        for (int i = 0; i < GRVCHECKLIST.Rows.Count; i++)
                        {
                            GridViewRow row = GRVCHECKLIST.Rows[i];
                            string questionid = ((Label)row.FindControl("lblID")).Text;
                            RadioButtonList rblAnswer = (RadioButtonList)row.FindControl("rblAnswer");
                            int result = Convert.ToInt32(rblAnswer.SelectedValue);
                            FileUpload fuImage = (FileUpload)row.FindControl("fuImage");
                            string Remarks = "";
                            string Remarkstext = "";
                            TextBox txtRemarks = ((TextBox)row.FindControl("txtRemarks"));

                            if (txtRemarks.Visible == true)
                            {
                                Remarks = ((TextBox)row.FindControl("txtRemarks")).Text;
                            }

                            DropDownList ddlRemarks = ((DropDownList)row.FindControl("ddlRemarks"));
                            if (ddlRemarks.Visible == true)
                            {
                                //Remarkstext = ((DropDownList)row.FindControl("ddlRemarks")).Text;
                                Remarkstext = ((DropDownList)row.FindControl("ddlRemarks")).SelectedItem.Text;
                            }




                            byte[] bytes = null;
                            byte[] results = null;
                            if (fuImage != null)
                            {
                                if (fuImage.HasFiles)
                                {
                                    System.Drawing.Image uploaded = System.Drawing.Image.FromStream(fuImage.PostedFile.InputStream);

                                    int originalWidth = uploaded.Width;
                                    int originalHeight = uploaded.Height;
                                    float percentWidth = (float)256 / (float)originalWidth;
                                    float percentHeight = (float)256 / (float)originalHeight;
                                    float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                                    int newWidth = (int)(originalWidth * percent);
                                    int newHeight = (int)(originalHeight * percent);

                                    System.Drawing.Image newImage = new System.Drawing.Bitmap(newWidth, newHeight);
                                    using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(newImage))
                                    {
                                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                        g.DrawImage(uploaded, 0, 0, newWidth, newHeight);
                                    }


                                    using (MemoryStream ms = new MemoryStream())
                                    {
                                        System.Drawing.Imaging.ImageCodecInfo codec = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().FirstOrDefault(c => c.FormatID == System.Drawing.Imaging.ImageFormat.Jpeg.Guid);
                                        System.Drawing.Imaging.EncoderParameters jpegParms = new System.Drawing.Imaging.EncoderParameters(1);
                                        jpegParms.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 95L);
                                        newImage.Save(ms, codec, jpegParms);
                                        results = ms.ToArray();
                                    }
                                }
                            }

                            if (txtRemarks.Visible == true)
                            {
                                iReturn = InsertCheckListDtl(CMPID, JOBID, ID, questionid, result, results, Remarks);
                            }
                            else if (ddlRemarks.Visible == true)
                            {
                                iReturn = InsertCheckListDtl(CMPID, JOBID, ID, questionid, result, results, Remarkstext);
                            }
                            else
                            {
                                iReturn = InsertCheckListDtl(CMPID, JOBID, ID, questionid, result, results, "");
                            }

                        }

                        if (iReturn == 1)
                        {
                            iResult = 1;
                            scope.Complete();
                            scope.Dispose();
                        }
                        else
                        {
                            iResult = 0;
                            //scope.Complete();
                            //scope.Dispose();
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iResult;
        }

        public int InsertCheckListDtl(int CMPID, string JOBID, string CHKID, string QUESTIONID, int RESULT, byte[] BYTES, string REMARKS)
        {
            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_LAPTOPCHECKLISTDTL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CHKID", CHKID);
                cmd.Parameters.AddWithValue("@JOBID", strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@QUESTIONID", QUESTIONID);
                cmd.Parameters.AddWithValue("@RESULT", RESULT);
                cmd.Parameters.AddWithValue("@REMARKS", REMARKS);
                cmd.Parameters.AddWithValue("@IMAGE", BYTES);
                cmd.Parameters.AddWithValue("@ACTION", "INSERTDETAILS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iResult = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return iResult;
        }

        public DataTable GetCheckListData(int CMPID, int ID, string JOBID, string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LAPTOPCHECKLISTDTL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable SelectOldPOData(int CMPID, string PONO, string ITEMCODE, int ACTION)
        {
            //SP_SELECT_PO_ALLDATA
            DataTable dt = new DataTable();
            PONO = strConvertZeroPadding(PONO);
            SqlCommand cmd = new SqlCommand("SP_SELECT_PO_ALLDATA", ConnSherpa);
            try
            {

                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PONO", PONO);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                //SqlDataReader dr = cmd.ExecuteReader();
                //dt.Load(dr);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetJobForLogistic(int CMPID, string JOBID, string JOBSTATUS, string PLANTCD, string SEGMENT, string DISTCHNL, string IMEINO, string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PRODUCT_LOGISTIC", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@JOBSTATUS", JOBSTATUS);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateDemoIntsllation(int CMPID, string ACTUALDISPDATE, string INSTALLATIONREQON, string DEMOREQON, string DEMOINSTBY, string INSTALLATIONDONEON, string DEMODONEON, string CHARGESTOBETAKEN, string CHARGESRCVDSTORE, string CHARGESRCVDONSTORE, string CHARGESRCVDACCOUNT,
            string CHARGESRCVDONACCOUNT, int FINALENTRY, int DEMOINSTUPDATEBY, string SONO, int SRNO, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_SO_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);

                if (ACTUALDISPDATE != "")
                {
                    cmd.Parameters.AddWithValue("@ACTUALDISPDATE", ACTUALDISPDATE == "" ? "" : setDateFormat(ACTUALDISPDATE, true));
                }

                if (INSTALLATIONREQON != "")
                {
                    cmd.Parameters.AddWithValue("@INSTALLATIONREQON", INSTALLATIONREQON == "" ? "" : setDateFormat(INSTALLATIONREQON, true));
                }
                if (DEMOREQON != "")
                {
                    cmd.Parameters.AddWithValue("@DEMOREQON", DEMOREQON == "" ? "" : setDateFormat(DEMOREQON, true));
                }

                if (DEMOINSTBY != "")
                {
                    cmd.Parameters.AddWithValue("@DEMOINSTBY", DEMOINSTBY);
                }

                if (INSTALLATIONDONEON != "")
                {
                    cmd.Parameters.AddWithValue("@INSTALLATIONDONEON", INSTALLATIONDONEON == "" ? "" : setDateFormat(INSTALLATIONDONEON, true));
                }

                if (DEMODONEON != "")
                {
                    cmd.Parameters.AddWithValue("@DEMODONEON", DEMODONEON == "" ? "" : setDateFormat(DEMODONEON, true));
                }

                if (CHARGESTOBETAKEN != "")
                {
                    cmd.Parameters.AddWithValue("@CHARGESTOBETAKEN", Convert.ToDecimal(CHARGESTOBETAKEN));
                }

                if (CHARGESRCVDSTORE != "")
                {
                    cmd.Parameters.AddWithValue("@CHARGESRCVDSTORE", Convert.ToDecimal(CHARGESRCVDSTORE));
                }

                if (CHARGESRCVDONSTORE != "")
                {
                    cmd.Parameters.AddWithValue("@CHARGESRCVDONSTORE", CHARGESRCVDONSTORE == "" ? "" : setDateFormat(CHARGESRCVDONSTORE, true));
                }

                if (CHARGESRCVDACCOUNT != "")
                {
                    cmd.Parameters.AddWithValue("@CHARGESRCVDACCOUNT", Convert.ToDecimal(CHARGESRCVDACCOUNT));
                }

                if (CHARGESRCVDONACCOUNT != "")
                {
                    cmd.Parameters.AddWithValue("@CHARGESRCVDONACCOUNT", CHARGESRCVDONACCOUNT == "" ? "" : setDateFormat(CHARGESRCVDONACCOUNT, true));
                }

                cmd.Parameters.AddWithValue("@FINALENTRY", FINALENTRY);
                cmd.Parameters.AddWithValue("@DEMOINSTUPDATEBY", DEMOINSTUPDATEBY);
                cmd.Parameters.AddWithValue("@SONO", SONO == "" ? "" : strConvertZeroPadding(SONO));
                cmd.Parameters.AddWithValue("@SRNO", SRNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public int InsertUpdateJobDetails(int CMPID, string JOBID, int JOBSTATUS, string PLANTCD, string SEGMENT, string DISTCHNL, string IMEINO, string FROMDATE, string TODATE, string REVTRANNAME, string WAYBILLNO, string WAYBILLSTATUS,
                                          string DOCTYPE, int STAGEID, string STATRES, int CREATEBY, string ACTION)
        {
            int iReturn = 0;
            SqlCommand cmd = new SqlCommand("SP_PRODUCT_LOGISTIC", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@JOBSTATUS", JOBSTATUS);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(FROMDATE, true));
                cmd.Parameters.AddWithValue("@TODATE", setDateFormat(TODATE, true));
                cmd.Parameters.AddWithValue("@REVTRANNAME", REVTRANNAME);
                cmd.Parameters.AddWithValue("@WAYBILLNO", WAYBILLNO);
                cmd.Parameters.AddWithValue("@WAYBILLSTATUS", WAYBILLSTATUS);
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@STAGEID", STAGEID);
                cmd.Parameters.AddWithValue("@STATRES", STATRES);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                iReturn = 1;

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return iReturn;

        }

        public DataTable GetReturnData(int CMPID, string FROMDATE, string TODATE, string SEGMENT, string PLANTCD, string LOCCD, string JOBID, string REFJOBID, string ORDERNO, int SALESFROM, string RETURNTYPE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_JSRETURN_DATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@REFJOBID", REFJOBID == "" ? "" : strConvertZeroPadding(REFJOBID));
                cmd.Parameters.AddWithValue("@ORDERNO", ORDERNO);
                cmd.Parameters.AddWithValue("@SALESFROM", SALESFROM);
                cmd.Parameters.AddWithValue("@RETURNTYPE", RETURNTYPE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetCommonLists(string LISTTYPE, int ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_LISTS_SELECTALL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@LISTTYPE", LISTTYPE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetListingUploadData(int CMPID, string PLANTCD, string STOCKTYPE, string GRADE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_UPLOADLISTING_DATA", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
                cmd.Parameters.AddWithValue("@STOCKTYPE", STOCKTYPE);
                cmd.Parameters.AddWithValue("@GRADE", GRADE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetPartnerAssociateAllVendorListing(int VENDORID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("MOBEXSELLER_PARTNERAPPROVEDQTYCRUDOPERATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "REPORT");
                cmd.Parameters.AddWithValue("@VENDORID", VENDORID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetDealerEligibleForNotification(int ID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DEALERINPARTERMINIFRANCHISEE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetPendingSCM(int CMPID, string PURTYPE, string ACTION)
        {
            //SP_COMMISSIONSI
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_COMMISSIONSI", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PURTYPE", PURTYPE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 1000;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetLockPriceHistory(string AsOnDate)
        {
            MainClass objMainClass = new MainClass();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MAKEMODEL_LOCKPRICE_LOG", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", setDateFormat(AsOnDate, true));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                return dt;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public string InsertSamsungTCR(string RCPTNO, string COMPLAINTNO, string CUSTNAME, string ADDRESS, string MOBILENO, string CONTACTNO, decimal LABOURCOST, decimal OTHERCOST, string PARTNAME, decimal PARTCOST, decimal TOTAL, int PAYMENTMODE,
            string TRANSACTIONID, int CREATEBY, string OTHER1, string OTHER2, GridView gvPartData, string EMAILID, string MODELNO, string SERIALNO, byte[] IMAGE, string EXTENSION, string ENGINNERNAME, string SERVICETYPE, string PRODUCT, string LOCATION, string GPCODE,
            int MOBEXAMC, int ISGST, string GSTNO, string GSTFIRMNAME, string ACTION)
        {
            string i = "";
            SqlCommand cmd = new SqlCommand("SP_SAMSUNG_TCR", ConnSherpa);
            try
            {
                DataTable dtRcptno = new DataTable();
                dtRcptno = GetSamsnugTCR("", "", "", "", "", "", 0, "", "MAXRCPTNO");
                RCPTNO = Convert.ToString(Convert.ToInt32(dtRcptno.Rows[0]["RCPTNO"]) + 1);

                cmd.Parameters.AddWithValue("@RCPTNO", RCPTNO);
                cmd.Parameters.AddWithValue("@COMPLAINTNO", COMPLAINTNO);
                cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME.ToUpper());
                cmd.Parameters.AddWithValue("@ADDRESS", ADDRESS.ToUpper());
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
                cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
                cmd.Parameters.AddWithValue("@MODELNO", MODELNO);
                cmd.Parameters.AddWithValue("@SERIALNO", SERIALNO);
                cmd.Parameters.AddWithValue("@LABOURCOST", LABOURCOST);
                cmd.Parameters.AddWithValue("@OTHERCOST", OTHERCOST);
                cmd.Parameters.AddWithValue("@PARTNAME", PARTNAME.ToUpper());
                cmd.Parameters.AddWithValue("@PARTCOST", PARTCOST);
                cmd.Parameters.AddWithValue("@TOTAL", TOTAL);
                cmd.Parameters.AddWithValue("@PAYMENTMODE", PAYMENTMODE);
                cmd.Parameters.AddWithValue("@TRANSACTIONID", TRANSACTIONID.ToUpper());
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@OTHER1", OTHER1);
                cmd.Parameters.AddWithValue("@OTHER2", OTHER2);
                cmd.Parameters.AddWithValue("@IMAGE", IMAGE);
                cmd.Parameters.AddWithValue("@EXTENSION", EXTENSION);

                cmd.Parameters.AddWithValue("@ENGGNAME", ENGINNERNAME);
                cmd.Parameters.AddWithValue("@SERVICETYPE", SERVICETYPE);
                cmd.Parameters.AddWithValue("@PRODUCT", PRODUCT);
                cmd.Parameters.AddWithValue("@LOCATION", LOCATION);
                cmd.Parameters.AddWithValue("@GPCODE", GPCODE);

                cmd.Parameters.AddWithValue("@MOBEXAMC", MOBEXAMC);

                cmd.Parameters.AddWithValue("@ISGST", ISGST);
                cmd.Parameters.AddWithValue("@CUSTGSTNO", GSTNO);
                cmd.Parameters.AddWithValue("@GSTFIRMNAME", GSTFIRMNAME);

                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = RCPTNO;

                for (int j = 0; j < gvPartData.Rows.Count; j++)
                {
                    string partnm = Convert.ToString(gvPartData.Rows[j].Cells[0].Text);
                    decimal partcst = Convert.ToDecimal(gvPartData.Rows[j].Cells[1].Text);

                    int ii = InsertTCRPart(RCPTNO, COMPLAINTNO, partnm, partcst, "INSERTPART");
                }

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return i;
        }

        public int InsertTCRPart(string RCPTNO, string COMPLAINTNO, string PARTNAME, decimal PARTCOST, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_SAMSUNG_TCR", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@RCPTNO", RCPTNO);
                cmd.Parameters.AddWithValue("@COMPLAINTNO", COMPLAINTNO);
                cmd.Parameters.AddWithValue("@PARTNAME", PARTNAME.ToUpper());
                cmd.Parameters.AddWithValue("@PARTCOST", PARTCOST);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }


        public DataTable GetSamsnugTCR(string FROMDATE, string TODATE, string COMPLAINTNO, string MOBILENO, string RCPTNO, string TRANSACTIONID, int PAYMENTMODE, string GPCODE, string ACTION)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SAMSUNG_TCR", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@COMPLAINTNO", COMPLAINTNO);
                cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                cmd.Parameters.AddWithValue("@RCPTNO", RCPTNO);
                cmd.Parameters.AddWithValue("@TRANSACTIONID", TRANSACTIONID);
                cmd.Parameters.AddWithValue("@PAYMENTMODE", PAYMENTMODE);
                cmd.Parameters.AddWithValue("@GPCODE", GPCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public int ReceiveTCR(string TCRNO, string COMPLAINTNO, int RCVDBY, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_SAMSUNG_TCR", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@RCVDBYCM", RCVDBY);
                cmd.Parameters.AddWithValue("@RCVDBYAC", RCVDBY);
                cmd.Parameters.AddWithValue("@RCPTNO", TCRNO);
                cmd.Parameters.AddWithValue("@COMPLAINTNO", COMPLAINTNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        public DataTable GetSegmentDistValid(int CMPID, string SEGMENT, string DISTCHNL)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_SEGMENTPARA_PARTYDTL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetProditembyModel(int CMPID, string MODELNAME)
        {
            //SP_PRODITEMBYMODEL
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PRODITEMBYMODEL", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetPOApprovalReport(int CMPID, string FROMDATE, string TODATE, string PLANTCODE, string PONO, string VENDORCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_POAPPROVAL_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@PONO", PONO != "" && PONO != string.Empty && PONO != null ? strConvertZeroPadding(PONO) : "");
                cmd.Parameters.AddWithValue("@VENDCODE", VENDORCODE != "" && VENDORCODE != string.Empty && VENDORCODE != null ? strConvertZeroPadding(VENDORCODE) : "");
                cmd.Parameters.AddWithValue("@PLANCD", PLANTCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetJobsheetForPO(int CMPID, string FROMDATE, string TODATE, string SEGMENT, string PLANT, int STAGEID, int STATUSID, string JOBID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_JOBSHEETFORAUTOPO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@PLANT", PLANT);
                cmd.Parameters.AddWithValue("@STAGEID", STAGEID);
                cmd.Parameters.AddWithValue("@STATUS", STATUSID);
                cmd.Parameters.AddWithValue("@JOBID", JOBID != "" && JOBID != string.Empty && JOBID != null ? strConvertZeroPadding(JOBID) : "");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetOpenGrn(int CMPID, string ACTION)
        {
            //SP_OPENGRN_REPORT
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_OPENGRN_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetJobsheetCount(int CMPID, int STATUSTYPE, int STATUSID, string FROMDATE, string TODATE, string SEGMENT, string PLANTCODE, string ACTION, int FROMDAY, int TODAY)
        {
            //SP_JOBSHEETDATACOUNT_REPORT
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_JOBSHEETDATACOUNT_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@STATUSTYPE", STATUSTYPE);
                cmd.Parameters.AddWithValue("@STATUSID", STATUSID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);

                cmd.Parameters.AddWithValue("@FROMDAY", FROMDAY);
                cmd.Parameters.AddWithValue("@TODAY", TODAY);

                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetJobsheetCountNew(int CMPID, int STATUSTYPE, string STATUSID, string FROMDATE, string TODATE, string SEGMENT, string PLANTCODE, string ACTION, int FROMDAY, int TODAY)
        {
            //SP_JOBSHEETDATACOUNT_REPORT
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_JOBSHEETDATACOUNT_REPORT", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@STATUSTYPE", STATUSTYPE);
                cmd.Parameters.AddWithValue("@STATUSIDNEW", STATUSID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
                cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);

                cmd.Parameters.AddWithValue("@FROMDAY", FROMDAY);
                cmd.Parameters.AddWithValue("@TODAY", TODAY);

                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int InsertRateCard(int CMPID, string ITEMCODE, string ITEMDESC, string BRAND, string CATEGORY, string JOBID, string SERIALNO, decimal DEALERPRICE, decimal CUSTOMERPRICE, int SOLD, string EXTFIELD1, string EXTFIELD2, int CREATEBY, string ACTION, decimal MRP, string SIZE, string GRADE, decimal ONLINEPRICE, string URL)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_CROMA_RATECARD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@IETMDESC", ITEMDESC);
                cmd.Parameters.AddWithValue("@BRAND", BRAND);
                cmd.Parameters.AddWithValue("@CATEGORY", CATEGORY);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@SERIALNO", SERIALNO);
                cmd.Parameters.AddWithValue("@DEALERPRICE", DEALERPRICE);
                cmd.Parameters.AddWithValue("@CUSTOMERPRCE", CUSTOMERPRICE);
                cmd.Parameters.AddWithValue("@SOLD", SOLD);
                cmd.Parameters.AddWithValue("@EXTFIELD1", EXTFIELD1);
                cmd.Parameters.AddWithValue("@EXTFIELD2", EXTFIELD2);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@ONLINEPRICE", ONLINEPRICE);
                cmd.Parameters.AddWithValue("@MRP", MRP);
                cmd.Parameters.AddWithValue("@SIZE", SIZE);
                cmd.Parameters.AddWithValue("@GRADE", GRADE);
                cmd.Parameters.AddWithValue("@URL", URL);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }


        public int InsertCromaLot(int CMPID, string RPASITECODE, string CROMALOTNO, string QTEKLOTNO, string INWARDSCANID, int SRNO, string ARTICLENO, string ITEMCODE, string ITEMDESC, string BRAND, string PRODUCT, string SERIALNO, int QTY, string GRADE,
            decimal MRP, decimal MAP, decimal ASP, decimal ASPGST, decimal POAMT, decimal INVVALUE, decimal AVGRECOVERYPER, decimal RECOVERYWOMARKUP, decimal MARKUPBRANDPER, decimal FINALRECOVERYWOGST, decimal GSTPER, decimal GSTAMT, decimal QTEKPRICE,
            decimal OLDPRICE, decimal SALESPRICE, string EXTFIELD1, string EXTFIELD2, int STATUS, int CREATEBY, string LOCATION, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_CROMA_LOTSALE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@RPASITECODE", RPASITECODE);
                cmd.Parameters.AddWithValue("@CROMALOTNO", CROMALOTNO);
                cmd.Parameters.AddWithValue("@QTEKLOTNO", QTEKLOTNO);
                cmd.Parameters.AddWithValue("@INWARDSCANID", INWARDSCANID);
                cmd.Parameters.AddWithValue("@SRNO", SRNO);
                cmd.Parameters.AddWithValue("@ARTICLENO", ARTICLENO);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@ITEMDESC", ITEMDESC);
                cmd.Parameters.AddWithValue("@BRAND", BRAND);
                cmd.Parameters.AddWithValue("@PRODUCT", PRODUCT);
                cmd.Parameters.AddWithValue("@SERIALNO", SERIALNO);
                cmd.Parameters.AddWithValue("@QTY", QTY);
                cmd.Parameters.AddWithValue("@GRADE", GRADE);
                cmd.Parameters.AddWithValue("@MRP", MRP);
                cmd.Parameters.AddWithValue("@MAP", MAP);
                cmd.Parameters.AddWithValue("@ASP", ASP);
                cmd.Parameters.AddWithValue("@ASPGST", ASPGST);
                cmd.Parameters.AddWithValue("@POAMT", POAMT);
                cmd.Parameters.AddWithValue("@INVVALUE", INVVALUE);
                cmd.Parameters.AddWithValue("@AVGRECOVERYPER", AVGRECOVERYPER);
                cmd.Parameters.AddWithValue("@RECOVERYWOMARKUP", RECOVERYWOMARKUP);
                cmd.Parameters.AddWithValue("@MARKUPBRANDPER", MARKUPBRANDPER);
                cmd.Parameters.AddWithValue("@FINALRECOVERYWOGST", FINALRECOVERYWOGST);
                cmd.Parameters.AddWithValue("@GSTPER", GSTPER);
                cmd.Parameters.AddWithValue("@GSTAMT", GSTAMT);
                cmd.Parameters.AddWithValue("@QTEKPRICE", QTEKPRICE);
                cmd.Parameters.AddWithValue("@OLDPRICE", OLDPRICE);
                cmd.Parameters.AddWithValue("@SALESPRICE", SALESPRICE);
                cmd.Parameters.AddWithValue("@EXTFIELD1", EXTFIELD1);
                cmd.Parameters.AddWithValue("@EXTFIELD2", EXTFIELD2);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@LOCATION", LOCATION);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }


        public int InsertCromaLotNew(int CMPID, string RPASITECODE, string CROMALOTNO, string QTEKLOTNO, decimal QTY, decimal MRP, decimal MAP, decimal OnlinePrice, decimal QTEKPRICE, decimal SALESPRICE, string LOCATION,
            GridView grvLead, int STATUS, int SHOWLOT, int CREATEBY, int BID, int BIDID, string BIDSTARTDT, string BIDENDDT, decimal BIDSTARTAMT, decimal MINIMUMBIDAMT, string ACTION)
        {
            int j = 0;

            try
            {

                using (TransactionScope scope = new TransactionScope())
                {

                    SqlCommand cmdMaster = new SqlCommand("SP_CROMA_LOTSALE", ConnSherpa);
                    cmdMaster.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdMaster.Parameters.AddWithValue("@RPASITECODE", RPASITECODE);
                    cmdMaster.Parameters.AddWithValue("@CROMALOTNO", CROMALOTNO);
                    cmdMaster.Parameters.AddWithValue("@QTEKLOTNO", QTEKLOTNO);
                    cmdMaster.Parameters.AddWithValue("@TOTALQTY", QTY);
                    cmdMaster.Parameters.AddWithValue("@TOTALMRP", MRP);
                    cmdMaster.Parameters.AddWithValue("@TOTALMAP", MAP);
                    cmdMaster.Parameters.AddWithValue("@TOTALASP", OnlinePrice);
                    cmdMaster.Parameters.AddWithValue("@TOTALQTEKPRICE", QTEKPRICE);
                    cmdMaster.Parameters.AddWithValue("@TOTALSALESPRICE", SALESPRICE);
                    cmdMaster.Parameters.AddWithValue("@LOCATION", LOCATION);
                    cmdMaster.Parameters.AddWithValue("@STATUS", STATUS);
                    cmdMaster.Parameters.AddWithValue("@SHOWLOT", SHOWLOT);
                    cmdMaster.Parameters.AddWithValue("@CREATEBY", CREATEBY);

                    if (BID == 1)
                    {
                        cmdMaster.Parameters.AddWithValue("@BIDID", BIDID);
                        cmdMaster.Parameters.AddWithValue("@BIDSTARTDATE", setDateFormat(BIDSTARTDT, true) + " 00:00:00");
                        cmdMaster.Parameters.AddWithValue("@BIDENDDATE", setDateFormat(BIDENDDT, true) + " 23:59:59");
                        cmdMaster.Parameters.AddWithValue("@BIDSTARTAMT", BIDSTARTAMT);
                        cmdMaster.Parameters.AddWithValue("@MINIMUMBIDAMT", MINIMUMBIDAMT);
                        cmdMaster.Parameters.AddWithValue("@BIDSTARTBY", CREATEBY);
                        cmdMaster.Parameters.AddWithValue("@BIDSTARTBYDATE", DateTime.Now);
                    }

                    cmdMaster.Parameters.AddWithValue("@ACTION", ACTION);
                    cmdMaster.CommandType = CommandType.StoredProcedure;
                    cmdMaster.Connection.Open();
                    cmdMaster.ExecuteNonQuery();
                    cmdMaster.Connection.Close();


                    if (BID == 1)
                    {
                        SqlCommand bidMaster = new SqlCommand("SP_CROMA_LOTSALE", ConnSherpa);
                        bidMaster.Parameters.AddWithValue("@CMPID", CMPID);
                        bidMaster.Parameters.AddWithValue("@BIDID", BIDID);
                        bidMaster.Parameters.AddWithValue("@BIDSTARTDATE", setDateFormat(BIDSTARTDT, true) + " 00:00:00");
                        bidMaster.Parameters.AddWithValue("@BIDENDDATE", setDateFormat(BIDENDDT, true) + " 23:59:59");
                        bidMaster.Parameters.AddWithValue("@BIDSTARTAMT", BIDSTARTAMT);
                        bidMaster.Parameters.AddWithValue("@MINIMUMBIDAMT", MINIMUMBIDAMT);
                        bidMaster.Parameters.AddWithValue("@STATUS", STATUS);
                        bidMaster.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                        bidMaster.Parameters.AddWithValue("@BIDSTARTBY", CREATEBY);
                        bidMaster.Parameters.AddWithValue("@BIDSTARTBYDATE", DateTime.Now);
                        bidMaster.Parameters.AddWithValue("@ACTION", "INSERTBIDMASTER");
                        bidMaster.CommandType = CommandType.StoredProcedure;
                        bidMaster.Connection.Open();
                        bidMaster.ExecuteNonQuery();
                        bidMaster.Connection.Close();
                    }


                    for (int i = 0; i < grvLead.Rows.Count; i++)
                    {
                        SqlCommand cmd = new SqlCommand("SP_CROMA_LOTSALE", ConnSherpa);
                        GridViewRow row = grvLead.Rows[i];
                        string RPASITECODEGV = Convert.ToString(row.Cells[0].Text);
                        string CROMALOTNOGV = Convert.ToString(row.Cells[1].Text);
                        string QTEKLOTNOGV = Convert.ToString(row.Cells[2].Text);
                        string INWARDSCANID = Convert.ToString(row.Cells[3].Text);
                        int SRNO = Convert.ToInt32(row.Cells[4].Text);
                        string ARTICLENO = Convert.ToString(row.Cells[5].Text);
                        string ITEMCODE = Convert.ToString(row.Cells[6].Text);
                        string ITEMDESC = Convert.ToString(row.Cells[7].Text);
                        string BRAND = Convert.ToString(row.Cells[8].Text);
                        string PRODUCT = Convert.ToString(row.Cells[9].Text);
                        string SERIALNO = Convert.ToString(row.Cells[10].Text);
                        int QTYGV = Convert.ToInt32(row.Cells[11].Text);
                        string GRADE = Convert.ToString(row.Cells[12].Text);
                        decimal MRPGV = Convert.ToDecimal(row.Cells[13].Text);
                        decimal MAPGV = Convert.ToDecimal(row.Cells[14].Text);
                        decimal ASPGV = Convert.ToDecimal(row.Cells[15].Text);
                        decimal ASPGST = Convert.ToDecimal(row.Cells[16].Text);
                        decimal POAMT = Convert.ToDecimal(row.Cells[17].Text);
                        decimal INVVALUE = Convert.ToDecimal(row.Cells[18].Text);
                        decimal AVGRECOVERYPER = Convert.ToDecimal(row.Cells[19].Text);
                        decimal RECOVERYWOMARKUP = Convert.ToDecimal(row.Cells[20].Text);
                        decimal MARKUPBRANDPER = Convert.ToDecimal(row.Cells[21].Text);
                        decimal FINALRECOVERYWOGST = Convert.ToDecimal(row.Cells[22].Text);
                        decimal GSTPER = Convert.ToDecimal(row.Cells[23].Text);
                        decimal GSTAMT = Convert.ToDecimal(row.Cells[24].Text);
                        decimal QTEKPRICEGV = Convert.ToDecimal(row.Cells[25].Text);
                        decimal OLDPRICE = Convert.ToDecimal(row.Cells[25].Text);
                        decimal SALESPRICEGV = Convert.ToDecimal(row.Cells[27].Text);
                        string LocationGV = Convert.ToString(row.Cells[28].Text);

                        cmd.Parameters.AddWithValue("@CMPID", CMPID);
                        cmd.Parameters.AddWithValue("@RPASITECODE", RPASITECODEGV);
                        cmd.Parameters.AddWithValue("@CROMALOTNO", CROMALOTNOGV);
                        cmd.Parameters.AddWithValue("@QTEKLOTNO", QTEKLOTNOGV);
                        cmd.Parameters.AddWithValue("@INWARDSCANID", INWARDSCANID);
                        cmd.Parameters.AddWithValue("@SRNO", SRNO);
                        cmd.Parameters.AddWithValue("@ARTICLENO", ARTICLENO);
                        cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                        cmd.Parameters.AddWithValue("@ITEMDESC", ITEMDESC);
                        cmd.Parameters.AddWithValue("@BRAND", BRAND);
                        cmd.Parameters.AddWithValue("@PRODUCT", PRODUCT);
                        cmd.Parameters.AddWithValue("@SERIALNO", SERIALNO);
                        cmd.Parameters.AddWithValue("@QTY", QTYGV);
                        cmd.Parameters.AddWithValue("@GRADE", GRADE);
                        cmd.Parameters.AddWithValue("@MRP", MRPGV);
                        cmd.Parameters.AddWithValue("@MAP", MAPGV);
                        cmd.Parameters.AddWithValue("@ASP", ASPGV);
                        cmd.Parameters.AddWithValue("@ASPGST", ASPGST);
                        cmd.Parameters.AddWithValue("@POAMT", POAMT);
                        cmd.Parameters.AddWithValue("@INVVALUE", INVVALUE);
                        cmd.Parameters.AddWithValue("@AVGRECOVERYPER", AVGRECOVERYPER);
                        cmd.Parameters.AddWithValue("@RECOVERYWOMARKUP", RECOVERYWOMARKUP);
                        cmd.Parameters.AddWithValue("@MARKUPBRANDPER", MARKUPBRANDPER);
                        cmd.Parameters.AddWithValue("@FINALRECOVERYWOGST", FINALRECOVERYWOGST);
                        cmd.Parameters.AddWithValue("@GSTPER", GSTPER);
                        cmd.Parameters.AddWithValue("@GSTAMT", GSTAMT);
                        cmd.Parameters.AddWithValue("@QTEKPRICE", QTEKPRICEGV);
                        cmd.Parameters.AddWithValue("@OLDPRICE", OLDPRICE);
                        cmd.Parameters.AddWithValue("@SALESPRICE", SALESPRICEGV);
                        //cmd.Parameters.AddWithValue("@EXTFIELD1", EXTFIELD1);
                        //cmd.Parameters.AddWithValue("@EXTFIELD2", EXTFIELD2);
                        cmd.Parameters.AddWithValue("@STATUS", 1);
                        cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                        cmd.Parameters.AddWithValue("@LOCATION", LocationGV);
                        cmd.Parameters.AddWithValue("@ACTION", "INSERT");
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Connection.Open();
                        cmd.ExecuteNonQuery();
                        cmd.Connection.Close();
                        j = 1;

                    }


                    scope.Complete();
                    scope.Dispose();

                    j = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return j;
        }
        public DataTable GetCromaBookingData(int CMPID, int BOOKINGBY, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMA_BOOKING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@BOOKINGBY", BOOKINGBY);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateLotAccept(int CMPID, decimal AMTVERIFIED, int AMTVRIFIEDSTATUS, int AMTVERIFIEDBY, string AMTVERIFIEDREMARKS, int BOOKINGSTATUS, int ID, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_CROMA_BOOKING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@AMTVERIFIED", AMTVERIFIED);
                cmd.Parameters.AddWithValue("@AMTVRIFIEDSTATUS", AMTVRIFIEDSTATUS);
                cmd.Parameters.AddWithValue("@AMTVERIFIEDBY", AMTVERIFIEDBY);
                cmd.Parameters.AddWithValue("@AMTVERIFIEDREMARKS", AMTVERIFIEDREMARKS);
                cmd.Parameters.AddWithValue("@BOOKINGSTATUS", BOOKINGSTATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public int UpdateLotCancel(int CMPID, int BOOKINGCANCELSTATUS, int BOOKINGCANCELBY, string BOOKINGCANCELREMARKS, int BOOKINGSTATUS, int ID, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_CROMA_BOOKING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@BOOKINGCANCELSTATUS", BOOKINGCANCELSTATUS);
                cmd.Parameters.AddWithValue("@BOOKINGCANCELBY", BOOKINGCANCELBY);
                cmd.Parameters.AddWithValue("@BOOKINGCANCELREMARKS", BOOKINGCANCELREMARKS);
                cmd.Parameters.AddWithValue("@BOOKINGSTATUS", BOOKINGSTATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public int UpdateLotReturn(int CMPID, int AMTRETURNSTATUS, int AMTRETURBY, string AMTRETURNREMARKS, string AMTRETURNUTR, int BOOKINGSTATUS, int ID, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_CROMA_BOOKING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@AMTRETURNSTATUS", AMTRETURNSTATUS);
                cmd.Parameters.AddWithValue("@AMTRETURBY", AMTRETURBY);
                cmd.Parameters.AddWithValue("@AMTRETURNREMARKS", AMTRETURNREMARKS);
                cmd.Parameters.AddWithValue("@AMTRETURNUTR", AMTRETURNUTR);
                cmd.Parameters.AddWithValue("@BOOKINGSTATUS", BOOKINGSTATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public int UpdateLotPurchase(int CMPID, int PURCHASEDBY, int BOOKINGSTATUS, string PURCHASEUTR, int ID, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_CROMA_BOOKING", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@PURCHASEDBY", PURCHASEDBY);
                cmd.Parameters.AddWithValue("@PURCHASEUTR", PURCHASEUTR);
                cmd.Parameters.AddWithValue("@BOOKINGSTATUS", BOOKINGSTATUS);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public DataTable GetCromaLotData(int CMPID, int STATUS, string CROMALOTNO, string QTEKLOTNO, string PRODUCT, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMA_LOTSALE", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@CROMALOTNO", CROMALOTNO);
                cmd.Parameters.AddWithValue("@QTEKLOTNO", QTEKLOTNO);
                cmd.Parameters.AddWithValue("@PRODUCT", PRODUCT);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader da = cmd.ExecuteReader();
                dt.Load(da);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public int UpdateBuybackPickup(string PICKUPBY, int BUYBACKID, string VCHRNO, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_CROMADATA", ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@PICKUPBY", PICKUPBY);
                cmd.Parameters.AddWithValue("@BUYBACKID", BUYBACKID);
                cmd.Parameters.AddWithValue("@VCHRNO", VCHRNO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public int UpdatePriceApproval(int CMPID, int ID, int STATUS, string APRVREJBY, string APRVREJREASON, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_CROMADATA", ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@APRVREJBY", APRVREJBY);
                cmd.Parameters.AddWithValue("@APRVREJREASON", APRVREJREASON);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public DataTable CheckRateJobDuplicate(int CMPID, string JOBID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMA_RATECARD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataReader da = cmd.ExecuteReader();
                dt.Load(da);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }

        public DataTable GetRateCard(int CMPID, string JOBID, string SERIALNO, string ITEMCODE, string BRAND, string CATEGORY, int SOLD, string SIZE, string GRADE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMA_RATECARD", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? "" : strConvertZeroPadding(JOBID));
                cmd.Parameters.AddWithValue("@SERIALNO", SERIALNO);
                cmd.Parameters.AddWithValue("@ITEMCODE", ITEMCODE);
                cmd.Parameters.AddWithValue("@BRAND", BRAND);
                cmd.Parameters.AddWithValue("@CATEGORY", CATEGORY);
                cmd.Parameters.AddWithValue("@SOLD", SOLD);
                cmd.Parameters.AddWithValue("@SIZE", SIZE);
                cmd.Parameters.AddWithValue("@GRADE", GRADE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 800;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable GetCromaPickupData(int CMPID, string CREATEBY, string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMADATA", ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }


        public DataTable GetCromaVchrData(int CMPID, int STOREID, string FROMDATE, string TODATE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMADATA", ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@STOREID", STOREID);
                cmd.Parameters.AddWithValue("@FROMDATE", FROMDATE == "" ? "" : setDateFormat(FROMDATE, true) + " 00:00:00.000");
                cmd.Parameters.AddWithValue("@TODATE", TODATE == "" ? "" : setDateFormat(TODATE, true) + " 23:59:59.000");
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }



        public string MaxQuotNo()
        {
            string QuotNo = "0";
            SqlCommand cmd = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "MAXQUOTNO");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                cmd.Connection.Close();
                QuotNo = strConvertZeroPadding(Convert.ToString((Convert.ToInt32(dt.Rows[0]["MAXQUOTNO"]) + 1)));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return QuotNo;
        }

        public string InsertCromaQuotation(int CMPID, string VENDORCODE, string VENDORNAME, string VENDORMAIL, decimal TOTALAMT, decimal TOTALDISCPER, decimal TOTALDISCAMT, decimal TOTALTAXAMT, decimal QUOTAMT, GridView GRVDATA, string PLANTCODE, string LOCATIONCODE,
            int USERID, string MOBILENO, string PAYTERMS, string PAYTERMSDESC, int pricecalc)
        {
            string i = "";
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DataTable dtcst = new DataTable();
                    string costcente = "1081";
                    dtcst = GetCostCenter(PLANTCODE, LOCATIONCODE);
                    if (dtcst.Rows.Count > 0)
                    {
                        costcente = Convert.ToString(dtcst.Rows[0]["CSTCENTCD"]);
                    }

                    string quotno = MaxQuotNo();
                    SqlCommand cmdMaster = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
                    cmdMaster.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdMaster.Parameters.AddWithValue("@QUOTNO", quotno);
                    cmdMaster.Parameters.AddWithValue("@VENDORCODE", VENDORCODE == "" ? "" : strConvertZeroPadding(VENDORCODE));
                    cmdMaster.Parameters.AddWithValue("@VENDORNAME", VENDORNAME);
                    cmdMaster.Parameters.AddWithValue("@VENDOREMAIL", VENDORMAIL);
                    cmdMaster.Parameters.AddWithValue("@MOBILENO", MOBILENO);
                    cmdMaster.Parameters.AddWithValue("@TOTALAMT", TOTALAMT);
                    cmdMaster.Parameters.AddWithValue("@TOTALDISCPER", TOTALDISCPER);
                    cmdMaster.Parameters.AddWithValue("@TOTALDISCAMT", TOTALDISCAMT);
                    cmdMaster.Parameters.AddWithValue("@TOTALTAXAMT", TOTALTAXAMT);
                    cmdMaster.Parameters.AddWithValue("@QUOTAMT", QUOTAMT);
                    cmdMaster.Parameters.AddWithValue("@PAYTERMS", PAYTERMS);
                    cmdMaster.Parameters.AddWithValue("@PAYTERMSDESC", PAYTERMSDESC);
                    cmdMaster.Parameters.AddWithValue("@CREATEBY", USERID);
                    cmdMaster.Parameters.AddWithValue("@ACTION", "INSERTMASTER");
                    cmdMaster.CommandType = CommandType.StoredProcedure;
                    cmdMaster.Connection.Open();
                    cmdMaster.ExecuteNonQuery();
                    cmdMaster.Connection.Close();

                    int qtsrno = 0;
                    decimal totalbaseamt = 0;
                    decimal totaltaxamt = 0;
                    decimal totaldiscount = 0;
                    for (int j = 0; j < GRVDATA.Rows.Count; j++)
                    {


                        GridViewRow row = GRVDATA.Rows[j];

                        CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
                        Label lblITEMCODE = (Label)row.FindControl("lblITEMCODE");
                        Label lblIETMDESC = (Label)row.FindControl("lblIETMDESC");
                        Label lblJOBID = (Label)row.FindControl("lblJOBID");
                        Label lblSERIALNO = (Label)row.FindControl("lblSERIALNO");
                        Label lblMRP = (Label)row.FindControl("lblMRP");
                        Label lblDEALERPRICE = (Label)row.FindControl("lblDEALERPRICE");
                        Label lblCUSTOMERPRCE = (Label)row.FindControl("lblCUSTOMERPRCE");
                        Label lblRATE = (Label)row.FindControl("lblRATE");
                        Label lblITEMID = (Label)row.FindControl("lblITEMID");
                        Label lblITEMGRP = (Label)row.FindControl("lblITEMGRP");
                        Label lblUOM = (Label)row.FindControl("lblUOM");
                        Label lblBASEAMOUNT = (Label)row.FindControl("lblBASEAMOUNT");
                        Label lblCONDID = (Label)row.FindControl("lblCONDID");
                        Label lblCONDTYPE = (Label)row.FindControl("lblCONDTYPE");

                        //if (chkSelect.Checked == true)
                        //{
                        qtsrno++;
                        decimal discountamt = ((Convert.ToDecimal(lblBASEAMOUNT.Text) * TOTALDISCPER) / 100);
                        decimal discountedamount = Convert.ToDecimal(lblBASEAMOUNT.Text) - ((Convert.ToDecimal(lblBASEAMOUNT.Text) * TOTALDISCPER) / 100);
                        decimal baseamtt = Convert.ToDecimal(lblBASEAMOUNT.Text);
                        decimal taxamount = Convert.ToDecimal((discountedamount * Convert.ToDecimal(lblRATE.Text)) / 100);
                        decimal rate = discountedamount + taxamount;

                        totalbaseamt = totalbaseamt + discountedamount;
                        totaltaxamt = totaltaxamt + taxamount;
                        totaldiscount = totaldiscount + discountamt;

                        SqlCommand cmdDetails = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
                        cmdDetails.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdDetails.Parameters.AddWithValue("@QUOTNO", quotno);
                        cmdDetails.Parameters.AddWithValue("@SRNO", qtsrno);
                        cmdDetails.Parameters.AddWithValue("@ITEMID", lblITEMID.Text);
                        cmdDetails.Parameters.AddWithValue("@ITEMDESC", lblIETMDESC.Text);
                        cmdDetails.Parameters.AddWithValue("@PLANTCD", PLANTCODE);
                        cmdDetails.Parameters.AddWithValue("@LOCCD", LOCATIONCODE);
                        cmdDetails.Parameters.AddWithValue("@ITEMGRPID", lblITEMGRP.Text);
                        cmdDetails.Parameters.AddWithValue("@UOM", lblUOM.Text);
                        cmdDetails.Parameters.AddWithValue("@QTY", 1);
                        if (pricecalc == 1)
                        {
                            cmdDetails.Parameters.AddWithValue("@BRATE", lblDEALERPRICE.Text);
                        }
                        else
                        {
                            cmdDetails.Parameters.AddWithValue("@BRATE", lblCUSTOMERPRCE.Text);
                        }

                        cmdDetails.Parameters.AddWithValue("@RATE", rate);
                        cmdDetails.Parameters.AddWithValue("@CAMOUNT", baseamtt);
                        cmdDetails.Parameters.AddWithValue("@DISCAMT", discountamt);
                        cmdDetails.Parameters.AddWithValue("@GLCODE", "10010000");
                        cmdDetails.Parameters.AddWithValue("@CSTCENTCD", costcente);
                        cmdDetails.Parameters.AddWithValue("@PRFCNT", "1000");
                        cmdDetails.Parameters.AddWithValue("@ITEMTEXT", "CROMA QUOTATION");
                        cmdDetails.Parameters.AddWithValue("@TAXAMT", taxamount);
                        cmdDetails.Parameters.AddWithValue("@SERIALNO", lblSERIALNO.Text);
                        cmdDetails.Parameters.AddWithValue("@JOBID", lblJOBID.Text);
                        cmdDetails.Parameters.AddWithValue("@MRP", Convert.ToDecimal(lblMRP.Text));
                        cmdDetails.Parameters.AddWithValue("@ACTION", "INSERTDETAILS");
                        cmdDetails.CommandType = CommandType.StoredProcedure;
                        cmdDetails.Connection.Open();
                        cmdDetails.ExecuteNonQuery();
                        cmdDetails.Connection.Close();

                        SqlCommand cmdTax = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
                        cmdTax.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdTax.Parameters.AddWithValue("@SRNO", qtsrno);
                        cmdTax.Parameters.AddWithValue("@QUOTNO", quotno);
                        cmdTax.Parameters.AddWithValue("@QUOTSRNO", qtsrno);
                        cmdTax.Parameters.AddWithValue("@CONDID", lblCONDID.Text);
                        cmdTax.Parameters.AddWithValue("@CONDTYPE", lblCONDTYPE.Text);
                        cmdTax.Parameters.AddWithValue("@GLCODE", "10010000");
                        cmdTax.Parameters.AddWithValue("@RATE", lblRATE.Text);
                        cmdTax.Parameters.AddWithValue("@BASEAMT", discountedamount);
                        cmdTax.Parameters.AddWithValue("@PID", 0);
                        cmdTax.Parameters.AddWithValue("@TAXAMT", taxamount);
                        cmdTax.Parameters.AddWithValue("@OPERATOR", "+");
                        cmdTax.Parameters.AddWithValue("@ACTION", "INSERTTAX");
                        cmdTax.CommandType = CommandType.StoredProcedure;
                        cmdTax.Connection.Open();
                        cmdTax.ExecuteNonQuery();
                        cmdTax.Connection.Close();

                        InsertRateCard(intCmpId, "", "", "", "", lblJOBID.Text, "", 0, 0, 2, "", "", USERID, "UPDATERESERVED", 0, "", "", 0, "");

                        //}

                    }

                    SqlCommand cmdUpdateMater = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
                    cmdUpdateMater.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdUpdateMater.Parameters.AddWithValue("@QUOTNO", quotno);
                    cmdUpdateMater.Parameters.AddWithValue("@TOTALDISCAMT", totaldiscount);
                    cmdUpdateMater.Parameters.AddWithValue("@TOTALBASEAMT", totalbaseamt);
                    cmdUpdateMater.Parameters.AddWithValue("@TOTALTAXAMT", totaltaxamt);
                    cmdUpdateMater.Parameters.AddWithValue("@ACTION", "UPDATEMASTER");
                    cmdUpdateMater.CommandType = CommandType.StoredProcedure;
                    cmdUpdateMater.Connection.Open();
                    cmdUpdateMater.ExecuteNonQuery();
                    cmdUpdateMater.Connection.Close();


                    scope.Complete();
                    scope.Dispose();

                    i = quotno;



                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }


        public string UpdateCromaQuotation(int CMPID, string QUOTNO, decimal TOTALAMT, decimal TOTALDISCPER, decimal TOTALDISCAMT, decimal TOTALTAXAMT, decimal QUOTAMT, GridView GRVDATA, GridView GRVTAX, int USERID, int pricecalc)
        {
            string i = "";
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    decimal totalbaseamt = 0;
                    decimal totaltaxamt = 0;
                    decimal totaldiscount = 0;

                    SqlCommand cmdDTLDelete = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
                    cmdDTLDelete.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdDTLDelete.Parameters.AddWithValue("@QUOTNO", strConvertZeroPadding(QUOTNO));
                    cmdDTLDelete.Parameters.AddWithValue("@ACTION", "DELETEDTL");
                    cmdDTLDelete.CommandType = CommandType.StoredProcedure;
                    cmdDTLDelete.Connection.Open();
                    cmdDTLDelete.ExecuteNonQuery();
                    cmdDTLDelete.Connection.Close();

                    SqlCommand cmdCondDelete = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
                    cmdCondDelete.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdCondDelete.Parameters.AddWithValue("@QUOTNO", strConvertZeroPadding(QUOTNO));
                    cmdCondDelete.Parameters.AddWithValue("@ACTION", "DELETECOND");
                    cmdCondDelete.CommandType = CommandType.StoredProcedure;
                    cmdCondDelete.Connection.Open();
                    cmdCondDelete.ExecuteNonQuery();
                    cmdCondDelete.Connection.Close();


                    for (int j = 0; j < GRVDATA.Rows.Count; j++)
                    {
                        GridViewRow row = GRVDATA.Rows[j];

                        Label lblQUOTID = (Label)row.FindControl("lblQUOTID");
                        Label lblITEMCODE = (Label)row.FindControl("lblITEMCODE");
                        Label lblITEMDESC = (Label)row.FindControl("lblITEMDESC");
                        Label lblTRACKNO = (Label)row.FindControl("lblTRACKNO");
                        Label lblIMEINO = (Label)row.FindControl("lblIMEINO");

                        //Label lblDEALERPRICE = (Label)row.FindControl("lblDEALERPRICE");
                        //Label lblCUSTOMERPRCE = (Label)row.FindControl("lblCUSTOMERPRCE");

                        Label lblGSTRATE = (Label)row.FindControl("lblGSTRATE");
                        Label lblITEMID = (Label)row.FindControl("lblITEMID");
                        Label lblITEMGROUPID = (Label)row.FindControl("lblITEMGROUPID");
                        Label lblUOMID = (Label)row.FindControl("lblUOMID");
                        Label lblITEMBRATE = (Label)row.FindControl("lblITEMRATE");
                        Label lblITEMPLANTID = (Label)row.FindControl("lblITEMPLANTID");
                        Label lblLOCCDID = (Label)row.FindControl("lblLOCCDID");
                        Label lblGLCODE = (Label)row.FindControl("lblGLCODE");
                        Label lblCSTCENTCD = (Label)row.FindControl("lblCSTCENTCD");
                        Label lblPROFITCENTER = (Label)row.FindControl("lblPROFITCENTER");

                        DataTable dtax = new DataTable();
                        dtax = GetTaxByRate(Convert.ToDecimal(lblGSTRATE.Text), "GETDATABYRATE");
                        string lblCONDID = Convert.ToString(dtax.Rows[0]["ID"]);
                        string lblCONDTYPE = Convert.ToString(dtax.Rows[0]["CONDTYPE"]);

                        decimal netamt = (Convert.ToDecimal(lblITEMBRATE.Text)) / ((100 + Convert.ToDecimal(lblGSTRATE.Text)) / 100);
                        decimal discountamt = ((netamt * TOTALDISCPER) / 100);
                        decimal discountedamount = (netamt) - (discountamt);
                        decimal baseamtt = Convert.ToDecimal(lblITEMBRATE.Text);
                        decimal taxamount = Convert.ToDecimal((discountedamount * Convert.ToDecimal(lblGSTRATE.Text)) / 100);
                        decimal rate = discountedamount + taxamount;

                        totalbaseamt = totalbaseamt + discountedamount;
                        totaltaxamt = totaltaxamt + taxamount;
                        totaldiscount = totaldiscount + discountamt;

                        SqlCommand cmdDetails = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
                        cmdDetails.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdDetails.Parameters.AddWithValue("@QUOTNO", QUOTNO);
                        cmdDetails.Parameters.AddWithValue("@SRNO", lblQUOTID.Text);
                        cmdDetails.Parameters.AddWithValue("@ITEMID", lblITEMID.Text);
                        cmdDetails.Parameters.AddWithValue("@ITEMDESC", lblITEMDESC.Text);
                        cmdDetails.Parameters.AddWithValue("@PLANTCD", lblITEMPLANTID.Text);
                        cmdDetails.Parameters.AddWithValue("@LOCCD", lblLOCCDID.Text);
                        cmdDetails.Parameters.AddWithValue("@ITEMGRPID", lblITEMGROUPID.Text);
                        cmdDetails.Parameters.AddWithValue("@UOM", lblUOMID.Text);
                        cmdDetails.Parameters.AddWithValue("@QTY", 1);
                        cmdDetails.Parameters.AddWithValue("@BRATE", lblITEMBRATE.Text);
                        cmdDetails.Parameters.AddWithValue("@RATE", rate);
                        cmdDetails.Parameters.AddWithValue("@CAMOUNT", netamt);
                        cmdDetails.Parameters.AddWithValue("@DISCAMT", discountamt);
                        cmdDetails.Parameters.AddWithValue("@GLCODE", lblGLCODE.Text);
                        cmdDetails.Parameters.AddWithValue("@CSTCENTCD", lblCSTCENTCD.Text);
                        cmdDetails.Parameters.AddWithValue("@PRFCNT", lblPROFITCENTER.Text);
                        cmdDetails.Parameters.AddWithValue("@ITEMTEXT", "CROMA QUOTATION");
                        cmdDetails.Parameters.AddWithValue("@TAXAMT", taxamount);
                        cmdDetails.Parameters.AddWithValue("@SERIALNO", lblIMEINO.Text);
                        cmdDetails.Parameters.AddWithValue("@JOBID", lblTRACKNO.Text);
                        cmdDetails.Parameters.AddWithValue("@ACTION", "INSERTDETAILS");
                        cmdDetails.CommandType = CommandType.StoredProcedure;
                        cmdDetails.Connection.Open();
                        cmdDetails.ExecuteNonQuery();
                        cmdDetails.Connection.Close();

                        SqlCommand cmdTax = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
                        cmdTax.Parameters.AddWithValue("@CMPID", CMPID);
                        cmdTax.Parameters.AddWithValue("@SRNO", lblQUOTID.Text);
                        cmdTax.Parameters.AddWithValue("@QUOTNO", QUOTNO);
                        cmdTax.Parameters.AddWithValue("@QUOTSRNO", lblQUOTID.Text);
                        cmdTax.Parameters.AddWithValue("@CONDID", lblCONDID);
                        cmdTax.Parameters.AddWithValue("@CONDTYPE", lblCONDTYPE);
                        cmdTax.Parameters.AddWithValue("@GLCODE", lblGLCODE.Text);
                        cmdTax.Parameters.AddWithValue("@RATE", lblGSTRATE.Text);
                        cmdTax.Parameters.AddWithValue("@BASEAMT", discountedamount);
                        cmdTax.Parameters.AddWithValue("@PID", 0);
                        cmdTax.Parameters.AddWithValue("@TAXAMT", taxamount);
                        cmdTax.Parameters.AddWithValue("@OPERATOR", "+");
                        cmdTax.Parameters.AddWithValue("@ACTION", "INSERTTAX");
                        cmdTax.CommandType = CommandType.StoredProcedure;
                        cmdTax.Connection.Open();
                        cmdTax.ExecuteNonQuery();
                        cmdTax.Connection.Close();

                        InsertRateCard(intCmpId, "", "", "", "", lblTRACKNO.Text, "", 0, 0, 2, "", "", USERID, "UPDATERESERVED", 0, "", "", 0, "");

                    }

                    SqlCommand cmdUpdateMater = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
                    cmdUpdateMater.Parameters.AddWithValue("@CMPID", CMPID);
                    cmdUpdateMater.Parameters.AddWithValue("@QUOTNO", QUOTNO);
                    cmdUpdateMater.Parameters.AddWithValue("@TOTALDISCPER", TOTALDISCPER);
                    cmdUpdateMater.Parameters.AddWithValue("@TOTALDISCAMT", totaldiscount);
                    cmdUpdateMater.Parameters.AddWithValue("@TOTALBASEAMT", totalbaseamt);
                    cmdUpdateMater.Parameters.AddWithValue("@TOTALTAXAMT", totaltaxamt);
                    cmdUpdateMater.Parameters.AddWithValue("@TOTALAMT", TOTALAMT);
                    cmdUpdateMater.Parameters.AddWithValue("@QUOTAMT", QUOTAMT);
                    cmdUpdateMater.Parameters.AddWithValue("@UPDATEBY", USERID);
                    cmdUpdateMater.Parameters.AddWithValue("@ACTION", "UPDATEEDITMASTER");
                    cmdUpdateMater.CommandType = CommandType.StoredProcedure;
                    cmdUpdateMater.Connection.Open();
                    cmdUpdateMater.ExecuteNonQuery();
                    cmdUpdateMater.Connection.Close();

                    scope.Complete();
                    scope.Dispose();

                    i = QUOTNO;



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }

        private DataTable GridViewToDataTable(GridView gridView)
        {
            DataTable dataTable = new DataTable();
            foreach (DataControlField column in gridView.Columns)
            {
                if (column is BoundField boundField)
                {
                    dataTable.Columns.Add(boundField.DataField);
                }
            }
            foreach (GridViewRow row in gridView.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int i = 0; i < dataTable.Columns.Count; i++)
                    {
                        dataRow[i] = row.Cells[i].Text;
                    }
                    dataTable.Rows.Add(dataRow);
                }
            }

            return dataTable;
        }

        public int AprvRejQO(int CMPID, string QUOTNO, int STATUS, int APRVBY, string REASON, string ACTION)
        {
            int i = 0;
            SqlCommand cmd = new SqlCommand("SP_CROMA_QUOTATION", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@QUOTNO", QUOTNO == "" ? "" : strConvertZeroPadding(QUOTNO));
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@APRVBY", APRVBY);
                cmd.Parameters.AddWithValue("@APRVREJREASON", REASON);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                i = 1;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return i;
        }

        public DataTable GetApprovalData(string DOCTYPE, string PLANTCODE, string DEPTCODE, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_APPROVAL_MATRIX", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);
                cmd.Parameters.AddWithValue("@DEPTCODE", DEPTCODE);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetJobIdByIMEI(int CMPID, string IMEINO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_JOBID_BYIMEINO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GETITEMFROMPO(string JOBID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_GETITEMFROMPO", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@TRNUM", strConvertZeroPadding(JOBID));
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


        public DataTable GetCustAddress(int CMPID, string CUSTMOBILENO, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SO_MASTER", ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@CMPID", CMPID);
                cmd.Parameters.AddWithValue("@CUSTMOBILENO", CUSTMOBILENO);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
            return dt;
        }


    }
    //CRM FOR CALL CENTER ENUMS

    enum APRVTYPE : int
    {
        APPROVED = 260,
        REJECT = 261
    }

    enum JOBSTATUS : int
    {
        ForwDocGen = 22,
        PhyDocVar = 53,
        Saved = 4,
    }

    enum STAGE : int
    {
        VerifyDocs = 46,

        CreateDocs = 1,
        SendDocs = 2,
        DocsEsc1 = 33,
        DocsEsc2 = 34,
        DocsReceived = 3,
        DocsVerified = 4,
        EmailForPacking = 5,
        PackEsc1 = 35,
        PackEsc2 = 36,
        RcvdPackConf = 6,
        RevWayBillNo = 7,
        GenRevDocs = 8,
        SendRevDocs = 28,
        RcvdProd = 9,
        VerifyProd = 10,
        SendToAsc = 51,
        RcvdFromAsc = 53,
        CreateJobCard = 11,
        PrintJobCard = 29,
        SendToWS = 12,
        EstimateCreate = 19,
        SendForApproval = 38,
        EstimatApproved = 39,
        ForwardWayBillNo = 24,
        CreateForwardDocs = 25,
        Dispatch = 32,
        SendDispatchEmail = 48,
        ForwDeliConfim = 26,
        RevPickUp = 47,
        JCInProd = 44,
        JCReset1 = 13,
        JCReset2 = 41,
        JCL1 = 14,
        JCL2 = 42,
        JCPartOrder = 15,
        JCPartRcvd = 16,
        JCL3 = 17,
        JCL4 = 18,
        JCQC1 = 20,
        JCSoaking = 21,
        JCQC2 = 22,
        JCSendForPacking = 23,
        JCOnHold = 43,
        JCInwardInsp = 50,
        Packed = 59,
        SendForSales = 65,
        RcvdFromSales = 66,
        STOSent = 67,
        STORcvd = 68,
    }

    enum STATUS : int
    {
        Approved = 35,
        Cancelled = 58,
        ApprovalPending = 66,
        PRCreated = 73,
        Saved = 57,
        Rejected = 3,
        PBSent = 85,
        PBReceived = 86,
        PBApproved = 87,
        PBRejected = 88,


        Canceled = 3,
        DocGenererated = 5,
        DocEmailed = 6,
        DocRcvd = 7,
        DocVerified = 8,
        WaitForPackingConf = 9,
        ReadyForPickup = 10,
        RevWayBillGen = 11,
        RevDocGen = 12,
        WaitForPickup = 13,
        ProdReceived = 14,
        ProdVerified = 15,
        AtAsc = 60,
        RcvdFromAsc = 61,
        JobCardCreated = 16,
        JobCardPrinted = 19,
        UnderProd = 20,
        ForwWayBillGen = 21,
        ForwDocGen = 22,
        Closed = 23,
        Dispatched = 26,
        DispatchEmailSent = 55,
        EscForDoc = 28,
        Esc2ForDoc = 29,
        EscForPack = 30,
        Esc2ForPack = 31,
        Estimated = 33,
        WaitForApproval = 34,
        PartApproved = 37,
        PhyDocsVerify = 53,
        ProdPickedUp = 54,
        PendingRcpt = 62,
        RevDocsSent = 63,
        JCSaved = 17,
        JCInProd = 52,
        JCFectoryReset = 39,
        JCSoftWareReset = 42,
        JCL1 = 43,
        JCL2 = 44,
        JCWaitingforParts = 45,
        JCL2_AfterPart = 46,
        JCL3 = 47,
        JCL4 = 48,
        JCQC1 = 49,
        JCSoaking = 50,
        JCReadyForDispatch = 40,
        JCClosed = 41,
        JCOnHold = 38,
        JCInwardInsp = 56,
    }

    enum DEPT : int
    {
        SmartPhone = 18
    }

    enum LeadStatus
    {
        Saved = 57,
        Cancelled = 58,
        OnHold = 59,
        Converted = 64,
        PaymentPending = 65,
        ApprovalPending = 66,
    }
    //CRM FOR CALL CENTER ENUMS

    public class WebsiteStatusData
    {
        public string ENTITYID { get; set; }

        public string APPNAME { get; set; }

        public string STATUS { get; set; }

        public string COMMENT { get; set; }

        public string WAYBILLNO { get; set; }

        public string COURIERSERVICE { get; set; }

        public string WAYBILLMSG { get; set; }
    }

    public class WebsiteStatusResponse
    {
        public string MESSAGE { get; set; }
        public int STATUSCODE { get; set; }
    }

    enum RTVSTATUSDEV
    {
        RETURNREQUESTGENERATED = 31883,
        RETURNED = 31884,
    }

    enum RTVSTATUS
    {
        RETURNREQUESTGENERATED = 11999,
        RETURNED = 11920,
    }
}


