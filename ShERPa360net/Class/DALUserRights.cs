using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{

    public static class FormRights
    {
        public static bool bView { get; set; }
        public static bool bAdd { get; set; }
        public static bool bEdit { get; set; }
        public static bool bCancel { get; set; }
        public static bool bExport { get; set; }
        public static bool bPrint { get; set; }
    }

    public class DALUserRights
    {
        MainClass objMainClass = new MainClass();

        public DataTable SELECT_MENURIGHTS_BYUSERID(string USERID, string MENUNAME)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECT_FORMRIGHTS", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@USERID", USERID);
            cmd.Parameters.AddWithValue("@MENUNAME", MENUNAME);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            return dt;
        }

        public void CHECK_FORMRIGHTS(string USERID, string MENUNAME, string ACTION)
        {
            FormRights.bView = false;
            FormRights.bAdd = false;
            FormRights.bEdit = false;
            FormRights.bCancel = false;
            FormRights.bExport = false;
            FormRights.bPrint = false;

            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("CHECK_FORMRIGHTS", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@USERID", USERID);
            cmd.Parameters.AddWithValue("@MENUNAME", MENUNAME);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            //return dt;


            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    if (FormRights.bView == false)
                    {
                        FormRights.bView = Convert.ToBoolean(int.Parse(dt.Rows[i]["MVIEW"].ToString()));
                    }
                    if (FormRights.bAdd == false)
                    {
                        FormRights.bAdd = Convert.ToBoolean(int.Parse(dt.Rows[i]["MADD"].ToString()));
                    }
                    if (FormRights.bEdit == false)
                    {
                        FormRights.bEdit = Convert.ToBoolean(int.Parse(dt.Rows[i]["MEDIT"].ToString()));
                    }
                    if (FormRights.bCancel == false)
                    {
                        FormRights.bCancel = Convert.ToBoolean(int.Parse(dt.Rows[i]["MDEL"].ToString()));
                    }
                    if (FormRights.bExport == false)
                    {
                        FormRights.bExport = Convert.ToBoolean(int.Parse(dt.Rows[i]["MEXP"].ToString()));
                    }
                    if (FormRights.bPrint == false)
                    {
                        FormRights.bPrint = Convert.ToBoolean(int.Parse(dt.Rows[i]["MPRINT"].ToString()));
                    }
                }
                //bView = Convert.ToBoolean(int.Parse(dt.Rows[0]["MVIEW"].ToString()));
                //bAdd = Convert.ToBoolean(int.Parse(dt.Rows[0]["MADD"].ToString()));
                //bEdit = Convert.ToBoolean(int.Parse(dt.Rows[0]["MEDIT"].ToString()));
                //bCancel = Convert.ToBoolean(int.Parse(dt.Rows[0]["MDEL"].ToString()));
            }
            else
            {
                FormRights.bView = false;
                FormRights.bAdd = false;
                FormRights.bEdit = false;
                FormRights.bCancel = false;
                FormRights.bExport = false;
                FormRights.bPrint = false;
            }
        }

        public DataTable FIRST_APPROVE_RIGHTS(string DOCTYPE, string USERID, string PLANTCODE, string DEPTCD = null)
        {
            DataTable dt = new DataTable();
            SqlCommand Rcmd = new SqlCommand("SP_APPROVAL_RIGHTS", objMainClass.ConnSherpa);
            try
            {
                Rcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                Rcmd.Parameters.AddWithValue("@APRVBY", USERID);
                Rcmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);
                Rcmd.Parameters.AddWithValue("@DEPTCD", DEPTCD);
                Rcmd.Parameters.AddWithValue("@ACTION", 1);
                Rcmd.CommandType = CommandType.StoredProcedure;
                Rcmd.Connection.Open();
                Rcmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(Rcmd);
                da.Fill(dt);
                Rcmd.Connection.Close();
            }
            catch (Exception ex)
            {
                Rcmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public DataTable PO_APPROVE_RIGHTS(string DOCTYPE, string USERID, string PLANTCODE, string DEPTCD, int ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_APPROVAL_RIGHTS", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                cmd.Parameters.AddWithValue("@APRVBY", USERID);
                cmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);
                cmd.Parameters.AddWithValue("@DEPTCD", DEPTCD);
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

        public bool APPROVE_RIGHTS(string DOCTYPE, string USERID, string DEPTCD, string PRNO, int ACTION, int STGAESEQ, string PLANTCODE)
        {
            bool rights = false;
            DataTable dt = new DataTable();
            SqlCommand Rcmd = new SqlCommand("SP_APPROVAL_RIGHTS", objMainClass.ConnSherpa);
            try
            {
                Rcmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                Rcmd.Parameters.AddWithValue("@APRVBY", USERID);
                Rcmd.Parameters.AddWithValue("@DEPTCD", DEPTCD);
                Rcmd.Parameters.AddWithValue("@PRNO", objMainClass.strConvertZeroPadding(PRNO));
                Rcmd.Parameters.AddWithValue("@ACTION", ACTION);
                Rcmd.Parameters.AddWithValue("@PLANTCODE", PLANTCODE);
                Rcmd.Parameters.AddWithValue("@APRVSEQ", STGAESEQ);
                Rcmd.CommandType = CommandType.StoredProcedure;
                Rcmd.Connection.Open();
                Rcmd.ExecuteNonQuery();
                SqlDataAdapter da = new SqlDataAdapter(Rcmd);
                da.Fill(dt);
                Rcmd.Connection.Close();

                if (dt.Rows.Count > 0)
                {
                    DataTable Cdt = new DataTable();
                    SqlCommand Ccmd = new SqlCommand("SP_APPROVAL_RIGHTS", objMainClass.ConnSherpa);
                    Ccmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
                    Ccmd.Parameters.AddWithValue("@ACTION", 6);
                    Ccmd.CommandType = CommandType.StoredProcedure;
                    Ccmd.Connection.Open();
                    Ccmd.ExecuteNonQuery();
                    SqlDataAdapter Cda = new SqlDataAdapter(Ccmd);
                    Cda.Fill(Cdt);
                    Ccmd.Connection.Close();

                    if (ACTION == 2 && Convert.ToInt32(dt.Rows[0]["STAGESEQ"]) == Convert.ToInt32(Cdt.Rows[0]["CNT"]))
                    {
                        rights = false;
                    }
                    else
                    {
                        rights = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Rcmd.Connection.Close();
                throw ex;
            }

            return rights;
        }


        public string CheckMenuVisible(string USERID, string MENUNAME)
        {
            string strReturn = "";
            SqlCommand cmd = new SqlCommand("CHECK_USERMENU_VISIBLE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@USERID", USERID);
            cmd.Parameters.AddWithValue("@MENUNAME", MENUNAME);
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
    }
}