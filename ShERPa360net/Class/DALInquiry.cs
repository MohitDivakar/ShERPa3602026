using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
namespace ShERPa360net.Class
{
    public class DALInquiry
    {
        MainClass objMainClass = new MainClass();
        public DataTable SearchInquiryList(string SEGMENT, string INQNO, string INQTYPE, int STATUS, string MULTISTATUS, string FULLNAME, string CONTACTNO, string COUPONNO, string FROMDT, string TODT, bool PENDING,
            bool USERWISE, int ASSIGNTO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SEARCH_TRAN_INQUIRY", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
            cmd.Parameters.AddWithValue("@INQNO", INQNO == "" ? (object)DBNull.Value : INQNO);
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            cmd.Parameters.AddWithValue("@INQTYPE", INQTYPE);
            cmd.Parameters.AddWithValue("@MULTISTATUS", STATUS == 0 ? MULTISTATUS : "");
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME);
            cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
            cmd.Parameters.AddWithValue("@COUPONNO", COUPONNO);
            cmd.Parameters.AddWithValue("@FROMDT", FROMDT == "" ? (object)DBNull.Value : DateTime.Parse(FROMDT).ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@TODT", TODT == "" ? (object)DBNull.Value : DateTime.Parse(TODT).ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@PENDING", PENDING == true ? 1 : 0);
            cmd.Parameters.AddWithValue("@ASSIGNTO", USERWISE == true ? ASSIGNTO : (object)DBNull.Value);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            cmd.CommandTimeout = 100;
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SearchJobcardList(string SEGMENT, string JOBID, string FROMDT, string TODT, int JOBSTATUS)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PENDING_JOBCARD_LIST", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
            cmd.Parameters.AddWithValue("@JOBSTATUS", JOBSTATUS);
            cmd.Parameters.AddWithValue("@JOBID", JOBID == "" ? (object)DBNull.Value : objMainClass.strConvertZeroPadding(JOBID));
            cmd.Parameters.AddWithValue("@FROMDT", FROMDT == "" ? (object)DBNull.Value : DateTime.Parse(FROMDT).ToString("yyyy-MM-dd") + " 00:00:00");
            cmd.Parameters.AddWithValue("@TODT", TODT == "" ? (object)DBNull.Value : DateTime.Parse(TODT).ToString("yyyy-MM-dd") + " 23:59:59");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SearchPartReq(int STAGEID, string JOBID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PART_REQ_BY_ENG", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@JCSTAGEID", STAGEID);
            cmd.Parameters.AddWithValue("@JOBID", JOBID);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SelectInquiryByInqNo_View(string InquiryNo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_INQUIRY_BYINQID", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@INQNO", InquiryNo);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SelectInquiryByArea_ForPickupAssign(int AREAID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_TRAN_INQMST_BYAREA_FORPICKUPASSIGN", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@AREAID", AREAID);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SelectInquiryDtl_BizLog(Int32 InquiryNo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_CUSTINQDTL_BIZLOG", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@INQNO", InquiryNo);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SELECT_TRAN_INQMST_BYINQNO(string strInqNo)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_TRAN_INQMST_SELECT_BYORDERID", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ORDER_ID", long.Parse(strInqNo));
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public string CheckOpenInquiryByContactNo(string CONTACTNO)
        {
            string strReturn = "";
            SqlCommand cmd = new SqlCommand("CHECK_OPENINQ_BYCONTACTNO", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CONTACTNO", CONTACTNO);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            if (obj != null)
            {
                strReturn = obj.ToString();
            }
            return strReturn;
        }

        public string CheckInquiryNo(string ORDER_ID)
        {
            string strReturn = "";
            SqlCommand cmd = new SqlCommand("SP_CHECKINQUIRYNO", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            if (obj != null)
            {
                strReturn = obj.ToString();
            }
            return strReturn;
        }

        public string CheckOpenInquiryNo(string ORDER_ID)
        {
            string strReturn = "";
            SqlCommand cmd = new SqlCommand("   ", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            if (obj != null)
            {
                strReturn = obj.ToString();
            }
            return strReturn;
        }

        public long SELECT_MAX_ORDERID(string Segment)
        {
            long strInquiryNo = 0;
            SqlCommand cmd = default(SqlCommand);
            if (Segment == "1012")
            {
                cmd = new SqlCommand("SP_TRAN_INQMST_MAX_ORDER_ID", objMainClass.ConnMI);
            }   
            else if (Segment == "1009")
            {
                cmd = new SqlCommand("SP_TRAN_JEEVESINQ_MAX_ORDER_ID", objMainClass.ConnMI);
            }
            else if (Segment == "1005")
            {
                cmd = new SqlCommand("SP_TRAN_INQMST_MAX_ORDER_ID", objMainClass.ConnSherpa);
                Segment = "1003";
            }
            else if (Segment == "1019" || Segment == "1099")
            {
                cmd = new SqlCommand("SP_TRAN_INQMST_MAX_ORDER_ID_FOR_CORPORATE", objMainClass.ConnSherpa);
            }
            else
            {
                cmd = new SqlCommand("SP_TRAN_INQMST_MAX_ORDER_ID", objMainClass.ConnSherpa);
            }
            cmd.Parameters.AddWithValue("@SEGMENT", Segment);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            strInquiryNo = long.Parse(obj.ToString()) + 1;
            return strInquiryNo;
        }

        public DataTable SelectPendingQuikeInquiry()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_PENDINGINQUIRY", objMainClass.ConnQuike);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SelectPendingMIInquiry()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECTPENDINGINQUIRY", objMainClass.ConnMI);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SelectPendingJeevesInquiry()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_PENDING_JEEVES_INQUIRY", objMainClass.ConnMI);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SELECT_PAYMENT(string strInqno)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("CHECK_PAYMENT_INQNO", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@INQNO", long.Parse(strInqno));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }


        public string CheckAssignedInqNo(string strInqno)
        {
            string strRInqNo = "";
            SqlCommand cmd = new SqlCommand("SP_CHECKDUPLICATE_TRAN_CALLASSIGN", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@INQUIRYNO", long.Parse(strInqno));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            if (obj != null)
            {
                strRInqNo = obj.ToString();
            }
            cmd.Connection.Close();
            return strRInqNo;
        }

        public DataTable SelectInqDtl_CallAssign(string strInqNo)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_INQDTL_TRAN_CALLASSIGN", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@INQUIRYNO", long.Parse(strInqNo));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SearchInqDetail_CC(string MOBILENO, string INQNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_INQUIRYDETAIL_CC", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@SEGMENT", MainClass.strQuikeSegment);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            //cmd.Parameters.AddWithValue("@CARDNO", CARDNO);
            cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
            cmd.Parameters.AddWithValue("@INQNO", INQNO);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public void Update_PendingQuikeInquiryFlag(SqlConnection ConnQuike, int ORDER_ID)
        {
            SqlCommand cmd = new SqlCommand("UPDATE_INQUIRYFLAG", ConnQuike);
            cmd.Parameters.AddWithValue("@BOOKING_ID", ORDER_ID);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void Update_PendingInquiryFlag(SqlConnection ConnMI, string ORDER_ID)
        {
            SqlCommand cmd = new SqlCommand("SP_UPDATE_PENDINGINQUIRYFLAG", ConnMI);
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void Update_Jeeves_Pending_Inquiry_Flag(SqlConnection ConnMI, string INQNO)
        {
            SqlCommand cmd = new SqlCommand("UPDATE_JEEVES_PENDINGINQUIRY_FLAG", ConnMI);
            cmd.Parameters.AddWithValue("@INQNO", INQNO);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void UPDATE_INQUIRYSTATUSWITHREASON(string ORDER_ID, int STATUS, string REASON, string REMARK, string UPDATEBY)
        {
            SqlCommand cmd = new SqlCommand("SP_UPDATE_TRAN_INQMST_STATUS", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@ID", ORDER_ID);
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            cmd.Parameters.AddWithValue("@REASON", REASON);
            cmd.Parameters.AddWithValue("@REMARK", REMARK);
            cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
            cmd.Parameters.AddWithValue("@UPDATEDATE", objMainClass.indianTime);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void INSERT_PENDINGINQUIRY(SqlConnection cnnSherpa, int CMPID, int MAX_COST, string SEGMENT, string DISTCHNL, string ORDER_ID, int BRAND_ID,
            int MODEL_ID, string MODELNAME, string FULLNAME, string COMPANYNAME, string MOB_NO, string LANDLINE, string EMAILID, string ADDRESS1,
            string ADDRESS2, string STATE_ID, string CITYNAME, string POSTCODE, string PAYMODE, string LOANER, string LANDMARK, DateTime PICKUPDATE, string PICKUPTIME, string PRBLMSUMMARY, string IMEINO,
            string IMEINO2, string PURINVNO, string PURINVDT, int ASSIGNTO, string CUSTCODE, string ESTAMT, string strMOBEXINQREFNO, string WEBINQTXNID, string STATUS,
            int CREATEBY, DateTime dtCREATEDATE)
        {

            //int Status = 57;
            SqlCommand cmd = new SqlCommand("SP_INSERT_PENDINGINQUIRY", cnnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@MAX_COST", MAX_COST);
            cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
            cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.Parameters.AddWithValue("@REF", SEGMENT == "1003" ? "WEBMAIL" : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@BRAND_ID", BRAND_ID);
            cmd.Parameters.AddWithValue("@MODEL_ID", MODEL_ID);
            cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@CARDNO", SEGMENT == "1003" ? MainClass.CommonQTEKCardNo : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@COMPANYNAME", COMPANYNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@MOB_NO", MOB_NO);
            cmd.Parameters.AddWithValue("@LANDLINE", LANDLINE);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@STATE_ID", STATE_ID == "" ? (object)DBNull.Value : STATE_ID);
            cmd.Parameters.AddWithValue("@CITYNAME", CITYNAME);
            cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE);
            cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@PICKUPDATE", PICKUPDATE);
            cmd.Parameters.AddWithValue("@PICKUPTIME", PICKUPTIME == "" ? (object)DBNull.Value : TimeSpan.Parse(PICKUPTIME));
            cmd.Parameters.AddWithValue("@PRBLMSUMMARY", PRBLMSUMMARY);
            cmd.Parameters.AddWithValue("@CALLID", 0);
            cmd.Parameters.AddWithValue("@CNCD", "IN");
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            if (SEGMENT == "1003")
            {
                cmd.Parameters.AddWithValue("@PAYMODE", string.IsNullOrEmpty(PAYMODE) ? "8" : PAYMODE);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PAYMODE", SEGMENT == "1013" ? "8" : "1");
            }
            cmd.Parameters.AddWithValue("@LOANER", SEGMENT == "1003" ? LOANER : (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@IMEINO2", IMEINO2);
            cmd.Parameters.AddWithValue("@PURINVNO", PURINVNO);
            cmd.Parameters.AddWithValue("@PURINVDT", PURINVDT == "" ? (object)DBNull.Value : DateTime.Parse(PURINVDT));
            cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
            cmd.Parameters.AddWithValue("@CUSTCODE", CUSTCODE);
            cmd.Parameters.AddWithValue("@ESTAMT", ESTAMT == "" ? (object)DBNull.Value : ESTAMT);
            cmd.Parameters.AddWithValue("@MOBEXINQREFNO", strMOBEXINQREFNO == "" ? (object)DBNull.Value : strMOBEXINQREFNO);
            cmd.Parameters.AddWithValue("@WEBINQTXNID", WEBINQTXNID == "" ? (object)DBNull.Value : WEBINQTXNID);
            cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
            cmd.Parameters.AddWithValue("@CREATEDATE", dtCREATEDATE);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void INSERT_JEEVES_PENDING_INQUIRY(SqlConnection cnnSherpa, string SEGMENT, string DISTCHNL, string ORDER_ID, DateTime REGDATE, string CLAIMID, string PLANID,
            string CUSTNAME, string MOBILENO, string EMAILID, string ADDRESS1, string ADDRESS2, string STATE, string CITY,
            string POSTALCODE, string BRAND, string MODEL, string IMEINO, string IMEINO2, string PURINVNO, string PURINVDT,
             string INVOICEVALUE, DateTime STARTDATE, DateTime ENDDATE, string HANDSETPROBLEM, string RSA, string CLAIMNO, string CLAIMFORM, string IMAGEURLS, int ASSIGNTO, int CREATEBY, DateTime dtCREATEDATE)
        {

            int Status = 57;
            SqlCommand cmd = new SqlCommand("INSERT_PENDING_JEEVES_INQUIRY", cnnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@MAX_COST", 0);
            cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
            cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.Parameters.AddWithValue("@REGDATE", REGDATE);
            cmd.Parameters.AddWithValue("@CLAIMID", CLAIMID);
            cmd.Parameters.AddWithValue("@PLANID", PLANID);
            cmd.Parameters.AddWithValue("@FULLNAME", CUSTNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@MOB_NO", MOBILENO);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@STATE", STATE);
            cmd.Parameters.AddWithValue("@CITYNAME", CITY);
            cmd.Parameters.AddWithValue("@POSTCODE", POSTALCODE);
            cmd.Parameters.AddWithValue("@BRAND_ID", 27); // OTHER BRAND
            cmd.Parameters.AddWithValue("@BRAND", BRAND);
            cmd.Parameters.AddWithValue("@MODELNAME", MODEL);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@IMEINO2", IMEINO2);
            cmd.Parameters.AddWithValue("@PURINVNO", PURINVNO);
            cmd.Parameters.AddWithValue("@PURINVDT", PURINVDT == "" ? (object)DBNull.Value : DateTime.Parse(PURINVDT));
            cmd.Parameters.AddWithValue("@INVOICEVALUE", INVOICEVALUE);
            cmd.Parameters.AddWithValue("@STARTDATE", STARTDATE);
            cmd.Parameters.AddWithValue("@ENDDATE", ENDDATE);
            cmd.Parameters.AddWithValue("@PRBLMSUMMARY", HANDSETPROBLEM);
            cmd.Parameters.AddWithValue("@RSA", RSA);
            cmd.Parameters.AddWithValue("@CLAIMNO", CLAIMNO);
            cmd.Parameters.AddWithValue("@CLAIMFORM", CLAIMFORM);
            cmd.Parameters.AddWithValue("@IMAGEURLS", IMAGEURLS);
            cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
            cmd.Parameters.AddWithValue("@STATUS", Status);
            cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
            cmd.Parameters.AddWithValue("@CREATEDATE", dtCREATEDATE);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public int INSERT_TRAN_INQMST(int MAX_COST, string REF, string COMMENT, string ORDER_ID, int BRAND_ID, int MODEL_ID, int CARRIER_ID,
            string MODELNAME, string IMEINO, string PASSCODE, string FULLNAME, string COMPANY_NAME, string MOB_NO, string LANDLINE, string EMAILID, string COUPANNO, string ADDRESS1,
            string ADDRESS2, int STATE_ID, int CITY_ID, string LANDMARK, string POSTCODE, Int32 AREAID, DateTime PICKUPDATE, TimeSpan PICKUPTIME, int CALLID, string OTHERPRBLMS, string PRBLMSUMMARY
            , string strPartEstDescr, string iEstAmt, int iEstBy, string strAprvFlag, string strNtAprvReason, string strEstComment, string SEGMENT, string DISTCHNL, int PayMode, int FedexLoc,
            string FedexAdd, string OTHASC, object ESTINQID, int LOANER, int CREATEBY, int ASSIGNTO, string LEADS)
        {
            int Status = 57;
            int iResult = 0;

            //SqlConnection cnnSherpa = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnStringSherpa"]);
            SqlCommand cmd = new SqlCommand("SP_TRAN_INQMST_INSERT_N_DEMO", objMainClass.ConnSherpa);
            //cmd.Transaction = objSqlTran;
            cmd.Parameters.AddWithValue("@MAX_COST", MAX_COST);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@REF", REF.Equals("0") ? "" : REF);
            cmd.Parameters.AddWithValue("@COMMENT", COMMENT.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.Parameters.AddWithValue("@BRAND_ID", BRAND_ID);
            cmd.Parameters.AddWithValue("@MODEL_ID", MODEL_ID);
            cmd.Parameters.AddWithValue("@CARRIER_ID", CARRIER_ID);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@PASSCODE", PASSCODE);
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@COMPANYNAME", COMPANY_NAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@MOB_NO", MOB_NO);
            cmd.Parameters.AddWithValue("@LANDLINE", LANDLINE);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@COUPANNO", COUPANNO);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@STATE_ID", STATE_ID);
            cmd.Parameters.AddWithValue("@CITY_ID", CITY_ID);
            cmd.Parameters.AddWithValue("@CNCD", "IN");
            cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE);
            cmd.Parameters.AddWithValue("@AREAID", AREAID);
            cmd.Parameters.AddWithValue("@PICKUPDATE", PICKUPDATE);
            cmd.Parameters.AddWithValue("@PICKUPTIME", PICKUPTIME);
            cmd.Parameters.AddWithValue("@CALLID", CALLID);
            cmd.Parameters.AddWithValue("@STATUS", Status);
            cmd.Parameters.AddWithValue("@OTHERPRBLMS", OTHERPRBLMS);
            cmd.Parameters.AddWithValue("@PRBLMSUMMARY", PRBLMSUMMARY);
            cmd.Parameters.AddWithValue("@ESTDESCR", strPartEstDescr);
            cmd.Parameters.AddWithValue("@ESTAMT", decimal.Parse(iEstAmt));
            cmd.Parameters.AddWithValue("@ESTBY", iEstBy);
            cmd.Parameters.AddWithValue("@APRVFLAG", strAprvFlag);
            cmd.Parameters.AddWithValue("@NTAPRVRSN", strNtAprvReason);
            cmd.Parameters.AddWithValue("@ESTCOMMENT", strEstComment);
            cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
            cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
            cmd.Parameters.AddWithValue("@PAYMODE", (MODEL_ID == 2214 || MODELNAME.Contains("MIX 2")) ? 8 : PayMode);
            cmd.Parameters.AddWithValue("@FEDEXLOC", FedexLoc);
            cmd.Parameters.AddWithValue("@FEDEXADD", FedexAdd);
            cmd.Parameters.AddWithValue("@OTHASC", OTHASC);
            cmd.Parameters.AddWithValue("@ESTINQID", ESTINQID == null ? (object)DBNull.Value : ESTINQID);
            cmd.Parameters.AddWithValue("@LOANER", LOANER);
            cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
            cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
            cmd.Parameters.AddWithValue("@LEAD", LEADS);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            iResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return iResult;
        }

        public int INSERT_TRAN_INQMST_WITH_ITEMID(int MAX_COST, string REF, string COMMENT, string ORDER_ID, int BRAND_ID, int MODEL_ID, int CARRIER_ID,
           string MODELNAME, string IMEINO, string PASSCODE, string FULLNAME, string COMPANY_NAME, string MOB_NO, string LANDLINE, string EMAILID, string COUPANNO, string ADDRESS1,
           string ADDRESS2, int STATE_ID, int CITY_ID, string LANDMARK, string POSTCODE, Int32 AREAID, DateTime PICKUPDATE, TimeSpan PICKUPTIME, int CALLID, string OTHERPRBLMS, string PRBLMSUMMARY
           , string strPartEstDescr, string iEstAmt, int iEstBy, string strAprvFlag, string strNtAprvReason, string strEstComment, string SEGMENT, string DISTCHNL, int PayMode, int FedexLoc,
           string FedexAdd, string OTHASC, object ESTINQID, int LOANER, int CREATEBY, int ASSIGNTO, string LEADS, string ITEMID)
        {
            int Status = 57;
            int iResult = 0;

            //SqlConnection cnnSherpa = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnStringSherpa"]);
            SqlCommand cmd = new SqlCommand("SP_TRAN_INQMST_INSERT_WITH_ITEMID", objMainClass.ConnSherpa);
            //cmd.Transaction = objSqlTran;
            cmd.Parameters.AddWithValue("@MAX_COST", MAX_COST);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@REF", REF.Equals("0") ? "" : REF);
            cmd.Parameters.AddWithValue("@COMMENT", COMMENT.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.Parameters.AddWithValue("@BRAND_ID", BRAND_ID);
            cmd.Parameters.AddWithValue("@MODEL_ID", MODEL_ID);
            cmd.Parameters.AddWithValue("@CARRIER_ID", CARRIER_ID);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@PASSCODE", PASSCODE);
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@COMPANYNAME", COMPANY_NAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@MOB_NO", MOB_NO);
            cmd.Parameters.AddWithValue("@LANDLINE", LANDLINE);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@COUPANNO", COUPANNO);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@STATE_ID", STATE_ID);
            cmd.Parameters.AddWithValue("@CITY_ID", CITY_ID);
            cmd.Parameters.AddWithValue("@CNCD", "IN");
            cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE);
            cmd.Parameters.AddWithValue("@AREAID", AREAID);
            cmd.Parameters.AddWithValue("@PICKUPDATE", PICKUPDATE);
            cmd.Parameters.AddWithValue("@PICKUPTIME", PICKUPTIME);
            cmd.Parameters.AddWithValue("@CALLID", CALLID);
            cmd.Parameters.AddWithValue("@STATUS", Status);
            cmd.Parameters.AddWithValue("@OTHERPRBLMS", OTHERPRBLMS);
            cmd.Parameters.AddWithValue("@PRBLMSUMMARY", PRBLMSUMMARY);
            cmd.Parameters.AddWithValue("@ESTDESCR", strPartEstDescr);
            cmd.Parameters.AddWithValue("@ESTAMT", decimal.Parse(iEstAmt));
            cmd.Parameters.AddWithValue("@ESTBY", iEstBy);
            cmd.Parameters.AddWithValue("@APRVFLAG", strAprvFlag);
            cmd.Parameters.AddWithValue("@NTAPRVRSN", strNtAprvReason);
            cmd.Parameters.AddWithValue("@ESTCOMMENT", strEstComment);
            cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
            cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
            cmd.Parameters.AddWithValue("@PAYMODE", (MODEL_ID == 2214 || MODELNAME.Contains("MIX 2")) ? 8 : PayMode);
            cmd.Parameters.AddWithValue("@FEDEXLOC", FedexLoc);
            cmd.Parameters.AddWithValue("@FEDEXADD", FedexAdd);
            cmd.Parameters.AddWithValue("@OTHASC", OTHASC);
            cmd.Parameters.AddWithValue("@ESTINQID", ESTINQID == null ? (object)DBNull.Value : ESTINQID);
            cmd.Parameters.AddWithValue("@LOANER", LOANER);
            cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
            cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
            cmd.Parameters.AddWithValue("@LEAD", LEADS);
            cmd.Parameters.AddWithValue("@ITEMID", ITEMID);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            iResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return iResult;
        }


        public int INSERT_TRAN_INQMST_NEW(int MAX_COST, string REF, string COMMENT, string ORDER_ID, int BRAND_ID, int MODEL_ID, int CARRIER_ID,
          string MODELNAME, string IMEINO, string PASSCODE, string FULLNAME, string COMPANY_NAME, string MOB_NO, string LANDLINE, string EMAILID, string COUPANNO, string ADDRESS1,
          string ADDRESS2, int STATE_ID, int CITY_ID, string LANDMARK, string POSTCODE, Int32 AREAID, DateTime PICKUPDATE, TimeSpan PICKUPTIME, int CALLID, string OTHERPRBLMS, string PRBLMSUMMARY
          , string strPartEstDescr, string iEstAmt, int iEstBy, string strAprvFlag, string strNtAprvReason, string strEstComment, string SEGMENT, string DISTCHNL, int PayMode, int FedexLoc,
          string FedexAdd, string OTHASC, object ESTINQID, int LOANER, int CREATEBY, int ASSIGNTO)
        {
            int Status = 57;
            int iResult = 0;

            //SqlConnection cnnSherpa = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnStringSherpa"]);
            SqlCommand cmd = new SqlCommand("SP_TRAN_INQMST_INSERT_N", objMainClass.ConnSherpa);
            //cmd.Transaction = objSqlTran;
            cmd.Parameters.AddWithValue("@MAX_COST", MAX_COST);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@REF", REF.Equals("0") ? "" : REF);
            cmd.Parameters.AddWithValue("@COMMENT", COMMENT.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.Parameters.AddWithValue("@BRAND_ID", BRAND_ID);
            cmd.Parameters.AddWithValue("@MODEL_ID", MODEL_ID);
            cmd.Parameters.AddWithValue("@CARRIER_ID", CARRIER_ID);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@PASSCODE", PASSCODE);
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@COMPANYNAME", COMPANY_NAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@MOB_NO", MOB_NO);
            cmd.Parameters.AddWithValue("@LANDLINE", LANDLINE);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@COUPANNO", COUPANNO);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@STATE_ID", STATE_ID);
            cmd.Parameters.AddWithValue("@CITY_ID", CITY_ID);
            cmd.Parameters.AddWithValue("@CNCD", "IN");
            cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE);
            cmd.Parameters.AddWithValue("@AREAID", AREAID);
            cmd.Parameters.AddWithValue("@PICKUPDATE", PICKUPDATE);
            cmd.Parameters.AddWithValue("@PICKUPTIME", PICKUPTIME);
            cmd.Parameters.AddWithValue("@CALLID", CALLID);
            cmd.Parameters.AddWithValue("@STATUS", Status);
            cmd.Parameters.AddWithValue("@OTHERPRBLMS", OTHERPRBLMS);
            cmd.Parameters.AddWithValue("@PRBLMSUMMARY", PRBLMSUMMARY);
            cmd.Parameters.AddWithValue("@ESTDESCR", strPartEstDescr);
            cmd.Parameters.AddWithValue("@ESTAMT", decimal.Parse(iEstAmt));
            cmd.Parameters.AddWithValue("@ESTBY", iEstBy);
            cmd.Parameters.AddWithValue("@APRVFLAG", strAprvFlag);
            cmd.Parameters.AddWithValue("@NTAPRVRSN", strNtAprvReason);
            cmd.Parameters.AddWithValue("@ESTCOMMENT", strEstComment);
            cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
            cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
            cmd.Parameters.AddWithValue("@PAYMODE", (MODEL_ID == 2214 || MODELNAME.Contains("MIX 2")) ? 8 : PayMode);
            cmd.Parameters.AddWithValue("@FEDEXLOC", FedexLoc);
            cmd.Parameters.AddWithValue("@FEDEXADD", FedexAdd);
            cmd.Parameters.AddWithValue("@OTHASC", OTHASC);
            cmd.Parameters.AddWithValue("@ESTINQID", ESTINQID == null ? (object)DBNull.Value : ESTINQID);
            cmd.Parameters.AddWithValue("@LOANER", LOANER);
            cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
            cmd.Parameters.AddWithValue("@ASSIGNTO", ASSIGNTO);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            iResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return iResult;
        }

        public int INSERT_TRAN_INQMST_ONLINEPORTAL(int MAX_COST, string REF, string COMMENT, long ORDER_ID, int BRAND_ID, int CARRIER_ID, string ModelName, string IMEINO,
            string IMEINO2, string ESN, string PASSCODE, string FULLNAME, string COMPANY_NAME, string strPurInvNo, string MOB_NO, string LANDLINE,
            string EMAILID, string COUPANNO, string ADDRESS1, string ADDRESS2, string CNCD, int STATE_ID, string strCityName, string LANDMARK, string POSTCODE,
            DateTime PICKUPDATE, TimeSpan PICKUPTIME, int CALLID, int iStatus, string txtHandsetPrblms, string PRBLMSUMMARY, string SEGMENT, string DISTCHNL,
            int iCreateby)
        {

            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_TRAN_INQMST_INSERT", objMainClass.ConnMI);
            cmd.Parameters.AddWithValue("@MAX_COST", MAX_COST);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@REF", REF.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@COMMENT", COMMENT.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.Parameters.AddWithValue("@BRAND_ID", BRAND_ID);
            cmd.Parameters.AddWithValue("@MODEL_ID", (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CARRIER_ID", CARRIER_ID);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@IMEINO2", IMEINO2);
            cmd.Parameters.AddWithValue("@ESN", ESN);
            cmd.Parameters.AddWithValue("@MODELNAME", ModelName);
            cmd.Parameters.AddWithValue("@PASSCODE", PASSCODE);
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME);
            cmd.Parameters.AddWithValue("@COMPANYNAME", COMPANY_NAME);
            cmd.Parameters.AddWithValue("@PURINVNO", strPurInvNo);
            cmd.Parameters.AddWithValue("@PURINVDT", (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@MOB_NO", MOB_NO);
            cmd.Parameters.AddWithValue("@LANDLINE", LANDLINE);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@COUPANNO", COUPANNO);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1);
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2);
            cmd.Parameters.AddWithValue("@STATE_ID", STATE_ID);
            cmd.Parameters.AddWithValue("@CITYNAME", strCityName);
            cmd.Parameters.AddWithValue("@CNCD", CNCD);
            cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK);
            cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE);
            cmd.Parameters.AddWithValue("@PICKUPDATE", PICKUPDATE);
            cmd.Parameters.AddWithValue("@PICKUPTIME", PICKUPTIME);
            cmd.Parameters.AddWithValue("@CALLID", CALLID);
            cmd.Parameters.AddWithValue("@STATUS", iStatus);
            cmd.Parameters.AddWithValue("@OTHERPRBLMS", txtHandsetPrblms);
            cmd.Parameters.AddWithValue("@PRBLMSUMMARY", PRBLMSUMMARY);
            cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
            cmd.Parameters.AddWithValue("@DISTCHNL", DISTCHNL);
            cmd.Parameters.AddWithValue("@CREATEBY", iCreateby);
            cmd.Parameters.AddWithValue("@CREATEDATE", objMainClass.indianTime);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            iResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return iResult;
        }

        public int INSERT_TRAN_INQMST_JEEVES(int MAX_COST, string REF, string COMMENT, string CLAIMID, long ORDER_ID, string BRAND, int CARRIER_ID, string ModelName, string IMEINO,
            string IMEINO2, string ESN, string PASSCODE, string FULLNAME, string COMPANY_NAME, string strPurInvNo, string MOB_NO, string LANDLINE,
            string EMAILID, string COUPANNO, string ADDRESS1, string ADDRESS2, string CNCD, int STATE_ID, string strCityName, string LANDMARK, string POSTCODE,
            DateTime PICKUPDATE, TimeSpan PICKUPTIME, int CALLID, int iStatus, string txtHandsetPrblms, string PRBLMSUMMARY, string SEGMENT, string DISTCHNL,
            int iCreateby)
        {

            int iResult = 0;
            SqlCommand cmd = new SqlCommand("SP_TRAN_JEEVESINQ_INSERT", objMainClass.ConnMI);
            cmd.Parameters.AddWithValue("@SEGMENT", SEGMENT);
            cmd.Parameters.AddWithValue("@INQNO", ORDER_ID);
            cmd.Parameters.AddWithValue("@REGDATE", (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CLAIMID", CLAIMID);
            cmd.Parameters.AddWithValue("@PLANID", (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CUSTNAME", FULLNAME);
            cmd.Parameters.AddWithValue("@MOBILENO", MOB_NO);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1);
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2);
            cmd.Parameters.AddWithValue("@CITY", strCityName);
            cmd.Parameters.AddWithValue("@POSTALCODE", POSTCODE);
            cmd.Parameters.AddWithValue("@BRAND", BRAND);
            cmd.Parameters.AddWithValue("@MODEL", ModelName);
            cmd.Parameters.AddWithValue("@IMEINO1", IMEINO);
            cmd.Parameters.AddWithValue("@IMEINO2", IMEINO2);
            cmd.Parameters.AddWithValue("@INVOICENO", (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@INVOICEDATE", (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@INVOICEVALUE", 0);
            cmd.Parameters.AddWithValue("@STARTDATE", (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@ENDDATE", (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@HANDSETPROBLEM", PRBLMSUMMARY);
            cmd.Parameters.AddWithValue("@RSA", 0);
            cmd.Parameters.AddWithValue("@CLAIMNO", 0);
            cmd.Parameters.AddWithValue("@CLAIMFORM", (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@IMAGEURLS", (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@CREATEDATE", objMainClass.indianTime);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            iResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return iResult;
        }

        public int UPDATE_TRAN_INQMST(int MAX_COST, string REF, string COMMENT, string ORDER_ID, int BRAND_ID, int MODEL_ID, int CARRIER_ID, string MODELNAME,
            string IMEINO, string PASSCODE, string FULLNAME, string COMPANY_NAME, string MOB_NO, string LANDLINE, string EMAILID, string COUPANNO, string ADDRESS1,
            string ADDRESS2, int STATE_ID, int CITY_ID, string LANDMARK, string POSTCODE, Int32 AREAID, DateTime PICKUPDATE, TimeSpan PICKUPTIME, string OTHERPRBLMS,
            string PRBLMSUMMARY, string strPartEstDescr, string iEstAmt, int iEstBy, string strAprvFlag, string strNtAprvReason, string strEstComment,
            int PayMode, int FedexLoc, string FedexAdd, string OTHASC, int LOANER, int UPDATEBY)
        {

            int iResult = 0;
            //SqlConnection cnnSherpa = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnStringSherpa"]);
            SqlCommand cmd = new SqlCommand("SP_TRAN_INQMST_UPDATE_N", objMainClass.ConnSherpa);
            //cmd.Transaction = objSqlTran;

            cmd.Parameters.AddWithValue("@MAX_COST", MAX_COST);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@REF", REF);
            cmd.Parameters.AddWithValue("@COMMENT", COMMENT);
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.Parameters.AddWithValue("@BRAND_ID", BRAND_ID);
            cmd.Parameters.AddWithValue("@MODEL_ID", MODEL_ID);
            cmd.Parameters.AddWithValue("@CARRIER_ID", CARRIER_ID);
            cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@PASSCODE", PASSCODE);
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME);
            cmd.Parameters.AddWithValue("@COMPANYNAME", COMPANY_NAME);
            cmd.Parameters.AddWithValue("@MOB_NO", MOB_NO);
            cmd.Parameters.AddWithValue("@LANDLINE", LANDLINE);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@COUPANNO", COUPANNO);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1);
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2);
            cmd.Parameters.AddWithValue("@STATE_ID", STATE_ID);
            cmd.Parameters.AddWithValue("@CITY_ID", CITY_ID);
            cmd.Parameters.AddWithValue("@CNCD", "IN");
            cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK);
            cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE);
            cmd.Parameters.AddWithValue("@AREAID", AREAID);
            cmd.Parameters.AddWithValue("@PICKUPDATE", PICKUPDATE);
            cmd.Parameters.AddWithValue("@PICKUPTIME", PICKUPTIME);
            cmd.Parameters.AddWithValue("@OTHERPRBLMS", OTHERPRBLMS);
            cmd.Parameters.AddWithValue("@PRBLMSUMMARY", PRBLMSUMMARY);
            cmd.Parameters.AddWithValue("@ESTDESCR", strPartEstDescr);
            cmd.Parameters.AddWithValue("@ESTAMT", decimal.Parse(iEstAmt));
            cmd.Parameters.AddWithValue("@ESTBY", iEstBy);
            cmd.Parameters.AddWithValue("@APRVFLAG", strAprvFlag);
            cmd.Parameters.AddWithValue("@NTAPRVRSN", strNtAprvReason);
            cmd.Parameters.AddWithValue("@ESTCOMMENT", strEstComment);
            cmd.Parameters.AddWithValue("@PAYMODE", (MODEL_ID == 2214 || MODELNAME.Contains("MIX 2")) ? 8 : PayMode);
            cmd.Parameters.AddWithValue("@FEDEXLOC", FedexLoc);
            cmd.Parameters.AddWithValue("@FEDEXADD", FedexAdd);
            cmd.Parameters.AddWithValue("@OTHASC", OTHASC);
            cmd.Parameters.AddWithValue("@LOANER", LOANER);
            cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
            cmd.Parameters.AddWithValue("@UPDATEDATE", objMainClass.indianTime);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            iResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return iResult;
        }

        public int UPDATE_TRAN_INQMST_WITH_ITEMID(int MAX_COST, string REF, string COMMENT, string ORDER_ID, int BRAND_ID, int MODEL_ID, int CARRIER_ID, string MODELNAME,
           string IMEINO, string PASSCODE, string FULLNAME, string COMPANY_NAME, string MOB_NO, string LANDLINE, string EMAILID, string COUPANNO, string ADDRESS1,
           string ADDRESS2, int STATE_ID, int CITY_ID, string LANDMARK, string POSTCODE, Int32 AREAID, DateTime PICKUPDATE, TimeSpan PICKUPTIME, string OTHERPRBLMS,
           string PRBLMSUMMARY, string strPartEstDescr, string iEstAmt, int iEstBy, string strAprvFlag, string strNtAprvReason, string strEstComment,
           int PayMode, int FedexLoc, string FedexAdd, string OTHASC, int LOANER, int UPDATEBY, string ITEMID)
        {

            int iResult = 0;
            //SqlConnection cnnSherpa = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnStringSherpa"]);
            SqlCommand cmd = new SqlCommand("SP_TRAN_INQMST_UPDATE_N_WITH_ITEMID", objMainClass.ConnSherpa);
            //cmd.Transaction = objSqlTran;

            cmd.Parameters.AddWithValue("@MAX_COST", MAX_COST);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@REF", REF);
            cmd.Parameters.AddWithValue("@COMMENT", COMMENT);
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.Parameters.AddWithValue("@BRAND_ID", BRAND_ID);
            cmd.Parameters.AddWithValue("@MODEL_ID", MODEL_ID);
            cmd.Parameters.AddWithValue("@CARRIER_ID", CARRIER_ID);
            cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@PASSCODE", PASSCODE);
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME);
            cmd.Parameters.AddWithValue("@COMPANYNAME", COMPANY_NAME);
            cmd.Parameters.AddWithValue("@MOB_NO", MOB_NO);
            cmd.Parameters.AddWithValue("@LANDLINE", LANDLINE);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@COUPANNO", COUPANNO);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1);
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2);
            cmd.Parameters.AddWithValue("@STATE_ID", STATE_ID);
            cmd.Parameters.AddWithValue("@CITY_ID", CITY_ID);
            cmd.Parameters.AddWithValue("@CNCD", "IN");
            cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK);
            cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE);
            cmd.Parameters.AddWithValue("@AREAID", AREAID);
            cmd.Parameters.AddWithValue("@PICKUPDATE", PICKUPDATE);
            cmd.Parameters.AddWithValue("@PICKUPTIME", PICKUPTIME);
            cmd.Parameters.AddWithValue("@OTHERPRBLMS", OTHERPRBLMS);
            cmd.Parameters.AddWithValue("@PRBLMSUMMARY", PRBLMSUMMARY);
            cmd.Parameters.AddWithValue("@ESTDESCR", strPartEstDescr);
            cmd.Parameters.AddWithValue("@ESTAMT", decimal.Parse(iEstAmt));
            cmd.Parameters.AddWithValue("@ESTBY", iEstBy);
            cmd.Parameters.AddWithValue("@APRVFLAG", strAprvFlag);
            cmd.Parameters.AddWithValue("@NTAPRVRSN", strNtAprvReason);
            cmd.Parameters.AddWithValue("@ESTCOMMENT", strEstComment);
            cmd.Parameters.AddWithValue("@PAYMODE", (MODEL_ID == 2214 || MODELNAME.Contains("MIX 2")) ? 8 : PayMode);
            cmd.Parameters.AddWithValue("@FEDEXLOC", FedexLoc);
            cmd.Parameters.AddWithValue("@FEDEXADD", FedexAdd);
            cmd.Parameters.AddWithValue("@OTHASC", OTHASC);
            cmd.Parameters.AddWithValue("@LOANER", LOANER);
            cmd.Parameters.AddWithValue("@ITEMID", ITEMID);
            cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
            cmd.Parameters.AddWithValue("@UPDATEDATE", objMainClass.indianTime);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            iResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return iResult;
        }


        public int UPDATE_TRAN_INQMST_WEBINQ(int MAX_COST, string REF, string COMMENT, string ORDER_ID, int BRAND_ID, int MODEL_ID, int CARRIER_ID, string MODELNAME,
            string IMEINO, string PASSCODE, string FULLNAME, string COMPANY_NAME, string MOB_NO, string LANDLINE, string EMAILID, string COUPANNO, string ADDRESS1,
            string ADDRESS2, int STATE_ID, int CITY_ID, string LANDMARK, string POSTCODE, Int32 AREAID, DateTime PICKUPDATE, TimeSpan PICKUPTIME, string OTHERPRBLMS,
            string PRBLMSUMMARY, string strPartEstDescr, string iEstAmt, int iEstBy, string strAprvFlag, string strNtAprvReason, string strEstComment,
            int PayMode, int FedexLoc, string FedexAdd, string OTHASC, int LOANER, int STATUS, int CREATEBY)
        {

            int iResult = 0;
            //SqlConnection cnnSherpa = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnStringSherpa"]);
            SqlCommand cmd = new SqlCommand("UPDATE_TRAN_INQMST_WEBINQ", objMainClass.ConnSherpa);
            //cmd.Transaction = objSqlTran;

            cmd.Parameters.AddWithValue("@MAX_COST", MAX_COST);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@REF", REF);
            cmd.Parameters.AddWithValue("@COMMENT", COMMENT);
            cmd.Parameters.AddWithValue("@ORDER_ID", ORDER_ID);
            cmd.Parameters.AddWithValue("@BRAND_ID", BRAND_ID);
            cmd.Parameters.AddWithValue("@MODEL_ID", MODEL_ID);
            cmd.Parameters.AddWithValue("@CARRIER_ID", CARRIER_ID);
            cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@PASSCODE", PASSCODE);
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME);
            cmd.Parameters.AddWithValue("@COMPANYNAME", COMPANY_NAME);
            cmd.Parameters.AddWithValue("@MOB_NO", MOB_NO);
            cmd.Parameters.AddWithValue("@LANDLINE", LANDLINE);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@COUPANNO", COUPANNO);
            cmd.Parameters.AddWithValue("@ADDRESS1", ADDRESS1);
            cmd.Parameters.AddWithValue("@ADDRESS2", ADDRESS2);
            cmd.Parameters.AddWithValue("@STATE_ID", STATE_ID);
            cmd.Parameters.AddWithValue("@CITY_ID", CITY_ID);
            cmd.Parameters.AddWithValue("@CNCD", "IN");
            cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK);
            cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE);
            cmd.Parameters.AddWithValue("@AREAID", AREAID);
            cmd.Parameters.AddWithValue("@PICKUPDATE", PICKUPDATE);
            cmd.Parameters.AddWithValue("@PICKUPTIME", PICKUPTIME);
            cmd.Parameters.AddWithValue("@OTHERPRBLMS", OTHERPRBLMS);
            cmd.Parameters.AddWithValue("@PRBLMSUMMARY", PRBLMSUMMARY);
            cmd.Parameters.AddWithValue("@ESTDESCR", strPartEstDescr);
            cmd.Parameters.AddWithValue("@ESTAMT", decimal.Parse(iEstAmt));
            cmd.Parameters.AddWithValue("@ESTBY", iEstBy);
            cmd.Parameters.AddWithValue("@APRVFLAG", strAprvFlag);
            cmd.Parameters.AddWithValue("@NTAPRVRSN", strNtAprvReason);
            cmd.Parameters.AddWithValue("@ESTCOMMENT", strEstComment);
            cmd.Parameters.AddWithValue("@PAYMODE", PayMode);
            cmd.Parameters.AddWithValue("@FEDEXLOC", FedexLoc);
            cmd.Parameters.AddWithValue("@FEDEXADD", FedexAdd);
            cmd.Parameters.AddWithValue("@OTHASC", OTHASC);
            cmd.Parameters.AddWithValue("@LOANER", LOANER);
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
            cmd.Parameters.AddWithValue("@CREATEDATE", objMainClass.indianTime);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            iResult = cmd.ExecuteNonQuery();
            cmd.Connection.Close();
            return iResult;
        }

        public void INSERT_CHECKED_PRB_DEVICE_DETAILS(long InquiryNo)
        {
            SqlCommand cmd = new SqlCommand("SP_TEMP_PROBLEMS_INSERT", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@SUBPROB_ID", 1);
            cmd.Parameters.AddWithValue("@ORDER_ID", InquiryNo);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public string GetInqNoByMobile(string strMobileNo)
        {
            string strReturn = "";

            SqlCommand cmd = new SqlCommand("SP_SELECTINQBYMOBILENO", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@MOBILENO", strMobileNo);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            if (obj != null)
            {
                strReturn = obj.ToString();
            }
            return strReturn;
        }



        public void UPDATE_TRAN_INQMST_STRSN(string strInqNo)
        {
            SqlCommand cmd = new SqlCommand("SP_UPDATE_TRAN_INQMST_STRSN", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ID", strInqNo);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void UpdateInquiryStatus(string INQNO, int STATUS)
        {
            SqlCommand cmd = new SqlCommand("UPDATE_INQUIRY_STATUS", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@INQNO", INQNO);
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            //cmd.Parameters.AddWithValue("@UPDATEBY", MainClass.BackGroundUser);
            //cmd.Parameters.AddWithValue("@UPDATEDATE", objMainClass.indianTime);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void INSERT_CALLLOGS(string DOCTYPE, string DOCNO, string NAME, string TONO, string CALLATTENDBY, string CONVERSATION,
            string REMARK, int CREATEBY)
        {
            SqlCommand cmd = new SqlCommand("INSERT_TRAN_CALLLOG", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
            cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
            cmd.Parameters.AddWithValue("@CALLDATE", objMainClass.indianTime);
            cmd.Parameters.AddWithValue("@NAME", NAME);
            cmd.Parameters.AddWithValue("@TONO", TONO);
            cmd.Parameters.AddWithValue("@CALLATTENDBY", CALLATTENDBY);
            cmd.Parameters.AddWithValue("@CONVERSATION", CONVERSATION);
            cmd.Parameters.AddWithValue("@REMARK", REMARK);
            cmd.Parameters.AddWithValue("@CREATEBY", CREATEBY);
            cmd.Parameters.AddWithValue("@CREATEDATE", objMainClass.indianTime);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public DataTable SELECT_TRAN_CALLLOGS_BYDOCNO(string DOCNO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_TRAN_CALLLOGS_BYDOCNO", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@DOCNO", DOCNO);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }



    }
}