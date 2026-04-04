using System;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using System.Web.UI.WebControls;
namespace ShERPa360net.Class
{
    public class DALCustReg
    {
        MainClass objMainClass = new MainClass();

        public void FETCH_TRAN_CUSTREG(string REGID, string REFREGID, string DISCAMT, string REGDATE, string REF, string COMMENT, string FULLNAME, string COMPANY, string MOBILENO,
           string EMAILID, string LANDLINE, string ADDR1, string ADDR2, string STATEID, string CITY, string CITYID, string LANDMARK, string POSTCODE, string HANDSETPURDATE,
           string PURBILLNO, string DEALERNAME, string PURDATE, int PURSTATE, string PURCITY, string PURPOSTCODE, string PURAREA, string BARCODE, int PRODID, string CARDNO,
           int BRANDID, string MODELID, string MODELNAME, string IMEINO, string IMEINO2, string CALLID, decimal HSVALUE, string COUPONCODE, string WITHBASEPRICE, string TERRITORY, string BLRID,
           string GSTVALUE, string strTOTVALUE, string strTOTADDONVALUE, string strUPSALEBYAGENT, string strREFDLRID, string strREFDLRNAME, string PAYMENTTYPE, string BLRNAME, string DLRID, string PROMOTERID, string PROMOTERNAME, string STATUS, int CREATEBY)
        {

            SqlCommand cmd = new SqlCommand("SP_INSERT_TRAN_CUSTREG", objMainClass.ConnSherpa);
            //cmd.Transaction = tranObj;

            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@REF", REF.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@COMMENT", COMMENT.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@REF_REGID", REFREGID == "" ? (object)DBNull.Value : REFREGID);
            cmd.Parameters.AddWithValue("@REGDATE", DateTime.Parse(REGDATE).Date);
            cmd.Parameters.AddWithValue("@REGID", REGID);
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@COMPANY", COMPANY.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
            cmd.Parameters.AddWithValue("@LANDLINE", LANDLINE);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID == "" ? "ABC@ABC.COM" : EMAILID);
            cmd.Parameters.AddWithValue("@ADDR1", ADDR1.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ADDR2", ADDR2.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@CITY", CITY);
            cmd.Parameters.AddWithValue("@CITYID", CITYID == "" ? "3026" : CITYID);
            cmd.Parameters.AddWithValue("@STATEID", STATEID == "" ? "1" : STATEID);
            cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE == "" ? "382445" : POSTCODE);
            cmd.Parameters.AddWithValue("@CNCD", "IN");
            cmd.Parameters.AddWithValue("@CALLID", CALLID == "" ? (object)DBNull.Value : CALLID);
            cmd.Parameters.AddWithValue("@DEALERNAME", DEALERNAME);
            cmd.Parameters.AddWithValue("@PURDATE", PURDATE == "" ? (object)DBNull.Value : DateTime.Parse(PURDATE).Date);
            cmd.Parameters.AddWithValue("@PURSTATE", PURSTATE);
            cmd.Parameters.AddWithValue("@PURCITY", PURCITY == "" ? "3026" : PURCITY);
            cmd.Parameters.AddWithValue("@PURAREA", PURAREA);
            cmd.Parameters.AddWithValue("@PURPOSTCODE", PURPOSTCODE);
            cmd.Parameters.AddWithValue("@PURBILLNO", PURBILLNO == "" ? "123" : PURBILLNO);
            cmd.Parameters.AddWithValue("@HANDSETPURDATE", HANDSETPURDATE == "" ? DateTime.Parse(REGDATE).Date : DateTime.Parse(HANDSETPURDATE).Date);
            cmd.Parameters.AddWithValue("@PRODID", PRODID);
            cmd.Parameters.AddWithValue("@HSVALUE", HSVALUE);
            cmd.Parameters.AddWithValue("@CARDNO", CARDNO);
            cmd.Parameters.AddWithValue("@BARCODE", BARCODE);
            cmd.Parameters.AddWithValue("@BRANDID", BRANDID);
            cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@MODELID", MODELID == "" ? (object)DBNull.Value : MODELID);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@IMEINO2", IMEINO2);
            cmd.Parameters.AddWithValue("@COUPONCODE", COUPONCODE);
            cmd.Parameters.AddWithValue("@TERRITORY", string.IsNullOrEmpty(TERRITORY) ? "1" : TERRITORY);
            cmd.Parameters.AddWithValue("@BLRID", BLRID == "" ? (object)DBNull.Value : int.Parse(BLRID));
            cmd.Parameters.AddWithValue("@BLRNAME", BLRNAME == "" ? (object)DBNull.Value : BLRNAME);
            cmd.Parameters.AddWithValue("@DLRID", int.Parse(DLRID));
            cmd.Parameters.AddWithValue("@PROMOTERID", PROMOTERID == "" ? (object)DBNull.Value : long.Parse(PROMOTERID));
            cmd.Parameters.AddWithValue("@PROMOTERNAME", PROMOTERNAME == "" ? (object)DBNull.Value : PROMOTERNAME);
            cmd.Parameters.AddWithValue("@WITHBASEPRICE", WITHBASEPRICE == "" ? (object)DBNull.Value : WITHBASEPRICE);
            cmd.Parameters.AddWithValue("@GSTVALUE", GSTVALUE == "" ? (object)DBNull.Value : GSTVALUE);
            cmd.Parameters.AddWithValue("@DISCAMT", DISCAMT == "" ? "0" : DISCAMT);
            cmd.Parameters.AddWithValue("@TOTVALUE", strTOTVALUE);
            cmd.Parameters.AddWithValue("@TOTADDONVAL", string.IsNullOrEmpty(strTOTADDONVALUE) ? "0" : strTOTADDONVALUE);
            cmd.Parameters.AddWithValue("@PAYMENTTYPE", PAYMENTTYPE == "" ? (object)DBNull.Value : PAYMENTTYPE);
            cmd.Parameters.AddWithValue("@UPSALEBYAGENT", strUPSALEBYAGENT == "" ? (object)DBNull.Value : strUPSALEBYAGENT);
            cmd.Parameters.AddWithValue("@REFDLRID", strREFDLRID == "" ? (object)DBNull.Value : strREFDLRID);
            cmd.Parameters.AddWithValue("@REFDLRNAME", strREFDLRNAME == "" ? (object)DBNull.Value : strUPSALEBYAGENT);
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            cmd.Parameters.AddWithValue("@CREATEBY", MainClass.BackGroundUser);
            cmd.Parameters.AddWithValue("@CREATEDATE", objMainClass.indianTime);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public int INSERT_DEALERCUSTREG_BASEPACK_CC(GridView gvList, string REFREGID, string REFCARDNO, string REF, string COMMENT, string CUSTNAME, string CUSTMOBILENO, string MOBILENO2, string EMAILID,
            string ADDR1, string ADDR2, int STATEID, int CITYID, string LANDMARK, string POSTCODE, string CALLID, string PACKVALUE, string PACKID, string BARCODE, string HANDSETPRICE,
            string BRAND, string BRANDID, string MODEL, string MODELID, string IMEINO, string IMEINO2, string INVOICEDATE, string PURBILLNO, string PURSTATE, string PURCITY,
            string PURPOSTCODE, string PURAREA, string TOTVALUE, string strPromoterid, string strPromoterName, string DLRID, string DEALERNAME, string REFDLRID,
            string REFDLRNAME, string COUPONNO, int DISCAMT, string STATUS, string WITHBASEPRICE, string GSTVALUE, string PAYMENTTYPE, string TERRITORY, byte[] Invimgarray, byte[] Phoneimgarray)
        {
            int iResult = 0;
            //SqlTransaction tranObj;
            //if (objMainClass.ConnQuike.State == ConnectionState.Closed)
            //{
            //    objMainClass.ConnQuike.Open();
            //}
            //tranObj = objMainClass.ConnQuike.BeginTransaction();
            //try
            //{
            using (TransactionScope scope = new TransactionScope())
            {
                SqlCommand cmd = new SqlCommand("SP_MST_DEALERCUSTREG_INSERT_BASEPACK_CC", objMainClass.ConnQuike);//, tranObj);
                cmd.Parameters.AddWithValue("@REF", REF.ToString().ToUpper());
                cmd.Parameters.AddWithValue("@COMMENT", COMMENT.ToString().ToUpper());
                cmd.Parameters.AddWithValue("@REFREGID", REFREGID == "" ? (object)DBNull.Value : REFREGID);
                cmd.Parameters.AddWithValue("@REFBARCODE", REFCARDNO == "" ? (object)DBNull.Value : REFCARDNO);
                cmd.Parameters.AddWithValue("@CUSTNAME", CUSTNAME);
                cmd.Parameters.AddWithValue("@CUSTMOBILENO", CUSTMOBILENO);
                cmd.Parameters.AddWithValue("@MOBILENO2", MOBILENO2);
                cmd.Parameters.AddWithValue("@EMAILID", EMAILID == "" ? (object)DBNull.Value : EMAILID);
                cmd.Parameters.AddWithValue("@ADDR1", ADDR1.ToString().ToUpper());
                cmd.Parameters.AddWithValue("@ADDR2", ADDR2.ToString().ToUpper());
                cmd.Parameters.AddWithValue("@STATEID", STATEID);
                cmd.Parameters.AddWithValue("@CITYID", CITYID);
                cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK.ToString().ToUpper());
                cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE);
                cmd.Parameters.AddWithValue("@CNCD", "IN");
                cmd.Parameters.AddWithValue("@HANDSETPRICE", decimal.Parse(HANDSETPRICE));
                cmd.Parameters.AddWithValue("@PURBILLNO", PURBILLNO);
                cmd.Parameters.AddWithValue("@INVOICEDATE", DateTime.Parse(INVOICEDATE));
                //cmd.Parameters.AddWithValue("@PURDATE", PURDATE == "" ? (object)DBNull.Value : DateTime.Parse(PURDATE));
                cmd.Parameters.AddWithValue("@BRAND", BRAND.ToUpper());
                cmd.Parameters.AddWithValue("@BRANDID", BRANDID);
                cmd.Parameters.AddWithValue("@MODEL", MODEL);
                cmd.Parameters.AddWithValue("@MODELID", int.Parse(MODELID) == 0 ? (object)DBNull.Value : MODELID);
                cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
                cmd.Parameters.AddWithValue("@IMEINO2", IMEINO2);
                cmd.Parameters.AddWithValue("@PURSTATE", int.Parse(PURSTATE));
                cmd.Parameters.AddWithValue("@PURCITY", int.Parse(PURCITY));
                cmd.Parameters.AddWithValue("@PURPOSTCODE", PURPOSTCODE);
                cmd.Parameters.AddWithValue("@PURAREA", PURAREA);
                cmd.Parameters.AddWithValue("@PACKVALUE", PACKVALUE);
                cmd.Parameters.AddWithValue("@PACKID", PACKID);
                cmd.Parameters.AddWithValue("@BARCODE", BARCODE);
                cmd.Parameters.AddWithValue("@BASEPRICE", decimal.Parse(MainClass.strBasePackMRP));
                cmd.Parameters.AddWithValue("@BASECOST", decimal.Parse(MainClass.strBasePackMRate));
                cmd.Parameters.AddWithValue("@FLAGAUTO", "1");
                cmd.Parameters.AddWithValue("@CALLID", CALLID);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@COUPONNO", COUPONNO);
                cmd.Parameters.AddWithValue("@DISCAMT", DISCAMT);
                cmd.Parameters.AddWithValue("@WITHBASEPRICE", WITHBASEPRICE);
                cmd.Parameters.AddWithValue("@GSTVALUE", decimal.Parse(GSTVALUE));
                cmd.Parameters.AddWithValue("@PAYMENTTYPE", PAYMENTTYPE);
                cmd.Parameters.AddWithValue("@TOTADDONVALUE", decimal.Parse(TOTVALUE) - (WITHBASEPRICE == "1" ? decimal.Parse(MainClass.strBasePackMRP) : 0) - DISCAMT - decimal.Parse(GSTVALUE));
                cmd.Parameters.AddWithValue("@TOTVALUE", decimal.Parse(TOTVALUE));
                cmd.Parameters.AddWithValue("@TERRITORY", int.Parse(TERRITORY));
                cmd.Parameters.AddWithValue("@DLRID", DLRID);
                cmd.Parameters.AddWithValue("@DEALERNAME", DEALERNAME);
                cmd.Parameters.AddWithValue("@REFDLRID", REFDLRID);
                cmd.Parameters.AddWithValue("@REFDLRNAME", REFDLRNAME);
                cmd.Parameters.AddWithValue("@PROMOTERID", int.Parse(strPromoterid));
                cmd.Parameters.AddWithValue("@PROMOTERNAME", strPromoterName);
                cmd.Parameters.AddWithValue("@CREATEDATE", objMainClass.indianTime);
                cmd.Parameters.AddWithValue("@UPSALEBYAGENT", "1");
                cmd.Parameters.Add("@ID", SqlDbType.Decimal).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                long iRegID = long.Parse(cmd.Parameters["@ID"].Value.ToString());

                if (Invimgarray != null)
                {
                    cmd = new SqlCommand("SP_INSERT_MST_REGIMG", objMainClass.ConnQuike); //, cnnQuike, tranObj);
                    cmd.Parameters.AddWithValue("@DLRREGID", iRegID);
                    cmd.Parameters.AddWithValue("@IMG", Invimgarray);
                    cmd.Parameters.AddWithValue("@IMGTYPE", "I");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }

                if (Phoneimgarray != null)
                {
                    cmd = new SqlCommand("SP_INSERT_MST_REGIMG", objMainClass.ConnQuike); //, cnnQuike, tranObj);
                    cmd.Parameters.AddWithValue("@DLRREGID", iRegID);
                    cmd.Parameters.AddWithValue("@IMG", Phoneimgarray);
                    cmd.Parameters.AddWithValue("@IMGTYPE", "P");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                if (PACKID != "9864")
                {
                    foreach (GridViewRow row in gvList.Rows)
                    {
                        CheckBox chkBox = (CheckBox)row.FindControl("chkgvAddon");
                        if (chkBox.Checked)
                        {
                            string strFeatureID = row.Cells[0].Text;
                            string strFeature = row.Cells[2].Text;
                            string strFeatureDesc = row.Cells[3].Text;
                            string strValidity = row.Cells[4].Text;
                            Label lblAddonVal = (Label)row.FindControl("lblAddonValue");

                            DateTime dtValidFrom = objMainClass.indianTime;
                            DateTime dtValidTo = objMainClass.indianTime;

                            if (strFeatureID == "1" || strFeatureID == "3")
                            {
                                dtValidFrom = objMainClass.indianTime.Date.AddYears(1);
                                dtValidTo = dtValidFrom.Date.AddYears(1);
                            }

                            if (strFeatureID == "2" || strFeatureID == "5" || strFeatureID == "11")
                            {
                                dtValidFrom = objMainClass.indianTime.Date;
                                dtValidTo = dtValidFrom.Date.AddYears(1);
                            }

                            if (strFeatureID == "4")
                            {
                                dtValidFrom = objMainClass.indianTime.Date;
                                dtValidTo = dtValidFrom.Date.AddYears(2);
                            }

                            //string strAddonVal = lblAddonVal.Text.Substring(3);
                            cmd = new SqlCommand("SP_INSERT_TRAN_PROTHISTORY", objMainClass.ConnQuike); //, cnnQuike, tranObj);
                            cmd.Parameters.AddWithValue("@DLRREGID", iRegID);
                            cmd.Parameters.AddWithValue("@FEATUREID", int.Parse(strFeatureID));
                            cmd.Parameters.AddWithValue("@FEATURE", strFeature);
                            cmd.Parameters.AddWithValue("@FEATUREDESC", strFeatureDesc);
                            cmd.Parameters.AddWithValue("@VALIDITY", strValidity);
                            cmd.Parameters.AddWithValue("@ADDONVALUE", decimal.Parse(lblAddonVal.Text));
                            cmd.Parameters.AddWithValue("@HANDSETVAL", decimal.Parse(HANDSETPRICE));
                            cmd.Parameters.AddWithValue("@TOTVALUE", decimal.Parse(TOTVALUE));
                            cmd.Parameters.AddWithValue("@ADDONCOST", (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@PASSONCOST", (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@VALIDFROM", dtValidFrom);
                            cmd.Parameters.AddWithValue("@VALIDTO", dtValidTo);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();
                        }
                    }
                }

                scope.Complete();
                scope.Dispose();

            }
            //tranObj.Commit();
            //cnnQuike.Close();
            iResult = 1;
            //}
            //catch (Exception ex) { tranObj.Rollback(); }
            //finally { cnnQuike.Close(); }
            return iResult;
        }

        public string CheckAddon(string RegId)
        {
            string strReturn = "";
            SqlCommand cmd = new SqlCommand("CHECKADDONUPSALE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@REGID", RegId);
            cmd.Parameters.AddWithValue("@STATUS", 57);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            if (obj != null)
            {
                strReturn = obj.ToString();
                return strReturn;
            }
            else
            {
                SqlCommand cmd1 = new SqlCommand("SP_CHECKADDONPMTRCVD", objMainClass.ConnSherpa);
                cmd1.Parameters.AddWithValue("@REGID", RegId);
                cmd1.Parameters.AddWithValue("@PMTRCVD", 1);
                cmd1.CommandType = CommandType.StoredProcedure;
                if (objMainClass.ConnSherpa.State == ConnectionState.Closed)
                {
                    cmd1.Connection.Open();
                }
                object obj1 = cmd1.ExecuteScalar();
                cmd1.Connection.Close();
                if (obj1 != null)
                {
                    strReturn = obj1.ToString();
                    return strReturn;
                }

                SqlCommand cmd2 = new SqlCommand("CHECKADDON", objMainClass.ConnSherpa);
                cmd2.Parameters.AddWithValue("@REGID", RegId);
                cmd2.CommandType = CommandType.StoredProcedure;
                if (objMainClass.ConnSherpa.State == ConnectionState.Closed)
                {
                    cmd2.Connection.Open();
                }
                object obj2 = cmd2.ExecuteScalar();
                cmd2.Connection.Close();
                if (obj2 != null)
                {
                    strReturn = obj2.ToString();
                    return strReturn;
                }
            }

            return strReturn;
        }

        public DataTable SELECT_CUSTREGDTL_BYCARDNO(string strCardNo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECTCUSTREGDTLBYCARDNO", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@CARDNO", strCardNo);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SELECT_CUSTREG_BY_IMEINO(string IMEINO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_ACTIVATION_DETAILS", objMainClass.ConnMI);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SELECTCUSTREG_BYCARDNO(string strCardNo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECTCUSTREG_BYCARDNO", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@CARDNO", strCardNo);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SELECT_TRAN_CUSTREG_BYREGNO(string strRegNo)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_TRAN_CUSTREG_SELECT_BYREGNO", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@REGID", strRegNo);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public string CheckIMEIOnline(string strPackId, string strIMEI)
        {
            string strReturn = "";
            SqlConnection cnnQuike = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnStringQuike"]);
            SqlCommand cmd = new SqlCommand("SP_MST_DEALERCUSTREG_CHECKDUPIMEI", cnnQuike);
            cmd.Parameters.AddWithValue("@PACKID", strPackId);
            cmd.Parameters.AddWithValue("@IMEINO", strIMEI);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            if ((obj) != null)
            {
                strReturn = obj.ToString();
            }
            cmd.Connection.Close();
            return strReturn;
        }

        public DataTable SearchCustRegReport(int TERRITORYID, string FROMDT, string TODT, int PRODID, int FEATUREID, int DLRID, int PROMOTERID, string CARDNO, bool WITHADDON)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SEARCH_CUSTREGREPORT", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@TERRITORYID", TERRITORYID);
            cmd.Parameters.AddWithValue("@FROMDT", DateTime.Parse(FROMDT));
            cmd.Parameters.AddWithValue("@TODT", DateTime.Parse(TODT));
            cmd.Parameters.AddWithValue("@PRODID", PRODID);
            cmd.Parameters.AddWithValue("@FEATUREID", FEATUREID);
            cmd.Parameters.AddWithValue("@DLRID", DLRID);
            cmd.Parameters.AddWithValue("@PROMOTERID", PROMOTERID);
            cmd.Parameters.AddWithValue("@CARDNO", CARDNO == "" ? (object)DBNull.Value : CARDNO);
            cmd.Parameters.AddWithValue("@STATUS", 57);
            cmd.Parameters.AddWithValue("@WITHADDON", WITHADDON == true ? 1 : 0);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SearchCustRegList(string FROMDT, string TODT, string REGNO, string CUSTNAME, int PRODID, string CARDNO, string MOBILENO, string IMEINO, int STATUS, bool PENDING, bool FORUPSALE, bool WITHADDON)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SEARCH_TRAN_CUSTREG", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@PRODID", PRODID == 0 ? (object)DBNull.Value : PRODID);
            cmd.Parameters.AddWithValue("@REGNO", REGNO);
            cmd.Parameters.AddWithValue("@FULLNAME", CUSTNAME);
            cmd.Parameters.AddWithValue("@CARDNO", CARDNO);
            cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.Parameters.AddWithValue("@FROMDT", FROMDT == "" ? (object)DBNull.Value : DateTime.Parse(FROMDT));
            cmd.Parameters.AddWithValue("@TODT", TODT == "" ? (object)DBNull.Value : DateTime.Parse(TODT));
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            cmd.Parameters.AddWithValue("@PENDING", PENDING == true ? 1 : 0);
            cmd.Parameters.AddWithValue("@FORUPSALE", FORUPSALE == true ? 1 : 0);
            cmd.Parameters.AddWithValue("@WITHADDON", WITHADDON == true ? 1 : 0);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SearchCustRegList_CC(string CARDNO, string MOBILENO, string IMEINO)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SEARCH_TRAN_CUSTREG_CC", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@CARDNO", CARDNO);
            cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
            cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SelectPendingQuikeReg()
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_PENDING_MST_DEALERCUSTREG", objMainClass.ConnQuike);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SelectProtHistoryByRegId(string DlrRegId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_TRAN_PROTHISTORY_BYREGID", objMainClass.ConnQuike);
            cmd.Parameters.AddWithValue("@DLRREGID", DlrRegId);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable SelectProtHistoryByRegId_Sherpa(string RegId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_TRAN_PROTHISTORY_BYREGID", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@REGID", RegId);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public void Update_Quike_Flag(string strId, SqlConnection ConnQuiike)
        {
            SqlCommand cmd = new SqlCommand("SP_UPDATE_FLAG_MST_DEALERCUSTREG", ConnQuiike);
            cmd.Parameters.AddWithValue("@ID", long.Parse(strId));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void INSERT_REGIMG(string strDlrRegId, byte[] bImgArray, string strImgType)
        {
            SqlCommand cmd = new SqlCommand("SP_INSERT_MST_REGIMG", objMainClass.ConnSherpa);
            //cmd.Transaction = tranObj;
            cmd.Parameters.AddWithValue("@REGID", strDlrRegId);
            cmd.Parameters.AddWithValue("@IMG", bImgArray);
            cmd.Parameters.AddWithValue("@IMGTYPE", strImgType);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void INSERT_PROTHISTORY(string REGID, string FEATUREID, string FEATURE, string FEATUREDESC, string VALIDITY, string ADDONVALUE, string HANDSETVAL, string TOTVALUE, string ADDONCOST, string PASSONCOST, string VALIDFROM, string VALIDTO)
        {
            SqlCommand cmd = new SqlCommand("SP_INSERT_TRAN_PROTHISTORY", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@REGID", REGID);
            cmd.Parameters.AddWithValue("@FEATUREID", int.Parse(FEATUREID));
            cmd.Parameters.AddWithValue("@FEATURE", FEATURE);
            cmd.Parameters.AddWithValue("@FEATUREDESC", FEATUREDESC);
            cmd.Parameters.AddWithValue("@VALIDITY", VALIDITY);
            cmd.Parameters.AddWithValue("@ADDONVALUE", decimal.Parse(ADDONVALUE));
            cmd.Parameters.AddWithValue("@HANDSETVAL", decimal.Parse(HANDSETVAL));
            cmd.Parameters.AddWithValue("@TOTVALUE", decimal.Parse(TOTVALUE));
            cmd.Parameters.AddWithValue("@ADDONCOST", ADDONCOST == "" ? (object)DBNull.Value : decimal.Parse(ADDONCOST));
            cmd.Parameters.AddWithValue("@PASSONCOST", PASSONCOST == "" ? (object)DBNull.Value : decimal.Parse(PASSONCOST));
            cmd.Parameters.AddWithValue("@VALIDFROM", VALIDFROM == "" ? (object)DBNull.Value : DateTime.Parse(VALIDFROM));
            cmd.Parameters.AddWithValue("@VALIDTO", VALIDTO == "" ? (object)DBNull.Value : DateTime.Parse(VALIDTO));
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public void UPDATE_TRAN_CUSTREG(string REF, string COMMENT, string FULLNAME, string COMPANY, string MOBILENO,
         string EMAILID, string LANDLINE, string ADDR1, string ADDR2, int STATEID, int CITYID, string LANDMARK, string POSTCODE, string HANDSETPURDATE,
         string PURBILLNO, int PURSTATE, int PURCITY, string PURPOSTCODE, string PAYMENTTYPE, int UPDATEBY, string REGID)
        {
            SqlCommand cmd = new SqlCommand("SP_UPDATE_TRAN_CUSTREG", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@REF", REF);
            cmd.Parameters.AddWithValue("@COMMENT", COMMENT.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@FULLNAME", FULLNAME.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@COMPANY", COMPANY.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@MOBILENO", MOBILENO);
            cmd.Parameters.AddWithValue("@EMAILID", EMAILID);
            cmd.Parameters.AddWithValue("@LANDLINE", LANDLINE);
            cmd.Parameters.AddWithValue("@ADDR1", ADDR1.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@ADDR2", ADDR2.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@STATEID", STATEID);
            cmd.Parameters.AddWithValue("@CITYID", CITYID);
            cmd.Parameters.AddWithValue("@LANDMARK", LANDMARK.ToString().ToUpper());
            cmd.Parameters.AddWithValue("@POSTCODE", POSTCODE);
            cmd.Parameters.AddWithValue("@HANDSETPURDATE", DateTime.Parse(HANDSETPURDATE).Date);
            cmd.Parameters.AddWithValue("@PURBILLNO", PURBILLNO);
            //cmd.Parameters.AddWithValue("@DEALERNAME", DEALERNAME);
            //cmd.Parameters.AddWithValue("@PURDATE", DateTime.Parse(PURDATE).Date);
            cmd.Parameters.AddWithValue("@PURSTATE", PURSTATE);
            cmd.Parameters.AddWithValue("@PURCITY", PURCITY);
            cmd.Parameters.AddWithValue("@PURPOSTCODE", PURPOSTCODE);
            cmd.Parameters.AddWithValue("@PAYMENTTYPE", PAYMENTTYPE);
            //cmd.Parameters.AddWithValue("@BARCODE", BARCODE);
            //cmd.Parameters.AddWithValue("@PRODID", PRODID);
            //cmd.Parameters.AddWithValue("@BRANDID", BRANDID);
            //cmd.Parameters.AddWithValue("@MODELID", MODELID);
            //cmd.Parameters.AddWithValue("@MODELNAME", MODELNAME.ToString().ToUpper());
            //cmd.Parameters.AddWithValue("@IMEINO", IMEINO);
            //cmd.Parameters.AddWithValue("@IMEINO2", IMEINO2);
            //cmd.Parameters.AddWithValue("@HSVALUE", HSVALUE);
            cmd.Parameters.AddWithValue("@UPDATEBY", UPDATEBY);
            cmd.Parameters.AddWithValue("@UPDATEDATE", objMainClass.indianTime);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@REGID", REGID);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        public DataTable Select_ProtHistory(string REGID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_TRAN_PROTHISTORY_SELECTBYREGID", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@REGID", REGID);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public DataTable Select_ProtFeatures()
        {
            SqlConnection cnnQuike = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["ConnStringQuike"]);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_PROTECTFEATURES_SELECTALL", cnnQuike);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public string CheckDupCardNo(string strCardNo)
        {
            string strReturn = "";
            SqlCommand cmd = new SqlCommand("SP_MST_DEALERCUSTREG_CHECKDUPLICATE", objMainClass.ConnQuike);
            cmd.Parameters.AddWithValue("@BARCODE", strCardNo);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            if ((obj) != null)
            {
                strReturn = obj.ToString();
            }
            return strReturn;
        }

        public string CheckDupCardNoSherpa(string strCardNo)
        {
            string strReturn = "";
            SqlCommand cmd = new SqlCommand("SP_TRAN_CUSTREG_CHECKDUPLICATE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CARDNO", strCardNo);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            if ((obj) != null)
            {
                strReturn = obj.ToString();
            }
            return strReturn;
        }

        public string ValidateCardNo(string strCardNo)
        {
            string strReturn = "";
            SqlCommand cmd = new SqlCommand("SP_CHECKBARCODENO", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@RANDOMNUMBER", strCardNo);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            object obj = cmd.ExecuteScalar();
            cmd.Connection.Close();
            if ((obj) != null)
            {
                strReturn = obj.ToString();
            }
            return strReturn;
        }
    }
}