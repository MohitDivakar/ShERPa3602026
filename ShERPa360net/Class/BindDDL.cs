using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ShERPa360net.Class
{
    public class BindDDL
    {
        MainClass objMainClass = new MainClass();

        public void FillVendor(DropDownList ddlVendor)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand("SP_BANKPAYMENT", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@ACTION", "VENDLIST");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlVendor.DataSource = dt;
            ddlVendor.DataTextField = "VENDNAME";
            ddlVendor.DataValueField = "VENDCODE";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillCustomer(DropDownList ddlVendor)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand("SP_FILLCUSTOMER", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@CUSTTYPE", "DIS");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlVendor.DataSource = dt;
            ddlVendor.DataTextField = "CustName";
            ddlVendor.DataValueField = "CustCode";
            ddlVendor.DataBind();
            ddlVendor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillStoreData(DropDownList ddlStore, string ID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand("SP_CROMADATA", objMainClass.ConnQuike);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@ID", ID);
            //cmd.Parameters.AddWithValue("@ACTION", "GETSTOREDATA");
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlStore.DataSource = dt;
            ddlStore.DataTextField = "NAME";
            ddlStore.DataValueField = "ID";
            ddlStore.DataBind();
            ddlStore.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillSMState(DropDownList ddlState)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand("SP_SM_MASTER", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", "GETSTATE");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlState.DataSource = dt;
            ddlState.DataTextField = "STATE";
            ddlState.DataValueField = "ID";
            ddlState.DataBind();
            //ddlState.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void Checkboxlist(CheckBoxList Loaction, string PlantCode)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_LOCATION_BY_PLANTCODE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@PLANTCD", PlantCode);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            Loaction.DataSource = dt;
            Loaction.DataTextField = "Descr";
            Loaction.DataTextField = "loccd";
            Loaction.DataBind();

        }

        public void FillDocType(DropDownList ddlDoctype, string DOCTYPE)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MM_TYPE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDoctype.DataSource = dt;
            ddlDoctype.DataTextField = "TRANTYPE";
            ddlDoctype.DataValueField = "DESCR";
            ddlDoctype.DataBind();
            ddlDoctype.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillDocTypewithReverse(DropDownList ddlDoctype, string DOCTYPE)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MM_TYPE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDoctype.DataSource = dt;
            ddlDoctype.DataTextField = "DESCR";
            ddlDoctype.DataValueField = "TRANTYPE";
            ddlDoctype.DataBind();
            ddlDoctype.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillAreaQuestion(DropDownList ddlLists, string AREA, string QUESTION, string ACTION, string TYPE)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_QUESTION_AREA", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@AREA", AREA);
                cmd.Parameters.AddWithValue("@QUESTION", QUESTION);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.Parameters.AddWithValue("@TYPE", TYPE);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                ddlLists.DataSource = dt;
                ddlLists.DataTextField = "ID";
                ddlLists.DataValueField = "ID";
                ddlLists.DataBind();
                ddlLists.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public void FillDocTypeNew(DropDownList ddlDoctype, string DOCTYPE)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DOC_TYPE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@DOCTYPE", DOCTYPE);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDoctype.DataSource = dt;
            ddlDoctype.DataTextField = "DOCTYPE";
            ddlDoctype.DataValueField = "DESCR";
            ddlDoctype.DataBind();
            ddlDoctype.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillBrandCroma(DropDownList ddlBrand, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMA_RATECARD", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlBrand.DataSource = dt;
            ddlBrand.DataTextField = "BRAND";
            ddlBrand.DataValueField = "BRAND";
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillCategoryCroma(DropDownList ddlCategory, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMA_RATECARD", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "CATEGORY";
            ddlCategory.DataValueField = "CATEGORY";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillBrandwiseCategoryCroma(DropDownList ddlCategory, int CMPID, string BRAND, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMA_RATECARD", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", CMPID);
            cmd.Parameters.AddWithValue("@BRAND", BRAND);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "CATEGORY";
            ddlCategory.DataValueField = "CATEGORY";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillSizeCroma(DropDownList ddlCategory, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMA_RATECARD", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "SIZE";
            ddlCategory.DataValueField = "SIZE";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillGradeCroma(DropDownList ddlCategory, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CROMA_RATECARD", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "GRADE";
            ddlCategory.DataValueField = "GRADE";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillSegmentNew(DropDownList ddlSegment)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_SEGMENT", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlSegment.DataSource = dt;
            ddlSegment.DataTextField = "SEGDESC";
            ddlSegment.DataValueField = "SEGCODE";
            ddlSegment.DataBind();
            //ddlSegment.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillPlant(DropDownList ddlPlant, string requestfrom = "ENTRY")
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_PLANT", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlPlant.DataSource = dt;
            ddlPlant.DataTextField = "Descr";
            ddlPlant.DataValueField = "PLANTCD";
            ddlPlant.DataBind();
            ddlPlant.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillPayTerm(DropDownList ddlPayterms)
        {
            //SP_SELECT_PAYTERMS

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_PAYTERMS", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlPayterms.DataSource = dt;
            ddlPayterms.DataTextField = "DESCR";
            ddlPayterms.DataValueField = "PMTTERMS";
            ddlPayterms.DataBind();

        }

        public void FillCondition(DropDownList ddlCondition)
        {
            //SP_SELECT_MST_CONDITION

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_CONDITION", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCondition.DataSource = dt;
            ddlCondition.DataTextField = "DISNAME";
            ddlCondition.DataValueField = "CONDTYPE";
            ddlCondition.DataBind();
            ddlCondition.Items.Insert(0, new ListItem("-- SELECT --", "0"));

        }

        public void FillPurChrgType(DropDownList ddlCharges, int CMPID)
        {
            //SP_SELECT_PURCHASE_CHARGE_TYPE

            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_PURCHASE_CHARGE_TYPE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", CMPID);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCharges.DataSource = dt;
            ddlCharges.DataTextField = "DISNAME";
            ddlCharges.DataValueField = "CHGTYPE";
            ddlCharges.DataBind();

        }

        public void FillLocationByPlantCd(DropDownList ddlLocation, string PlantCode)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_LOCATION_BY_PLANTCODE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@PLANTCD", PlantCode);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlLocation.DataSource = dt;
            ddlLocation.DataTextField = "Descr";
            ddlLocation.DataValueField = "loccd";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillCostCenter(DropDownList ddlCostCenter, string PLANTCD, string LOCCD)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_COSTCENTER", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@PLANTCD", PLANTCD);
            cmd.Parameters.AddWithValue("@LOCCD", LOCCD);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCostCenter.DataSource = dt;
            ddlCostCenter.DataTextField = "DESCR";
            ddlCostCenter.DataValueField = "CSTCENTCD";
            ddlCostCenter.DataBind();
            ddlCostCenter.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillUOM(DropDownList ddlUOM)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_UOM", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@status", 1);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlUOM.DataSource = dt;
            ddlUOM.DataTextField = "Descr";
            ddlUOM.DataValueField = "id";
            ddlUOM.DataBind();
            //  ddlUOM.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillItemCat(DropDownList ddlpopCategory)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_ITEMCAT", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlpopCategory.DataSource = dt;
            ddlpopCategory.DataTextField = "ITEMCAT";
            ddlpopCategory.DataValueField = "ID";
            ddlpopCategory.DataBind();
            ddlpopCategory.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillItemGrp(DropDownList ddlpopGroup, string requestfrom = "ENTRY")
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_ITEMGRP", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlpopGroup.DataSource = dt;
            ddlpopGroup.DataTextField = "ItemGrp";
            ddlpopGroup.DataValueField = "ID";
            ddlpopGroup.DataBind();
            ddlpopGroup.Items.Insert(0, (requestfrom.Equals("ENTRY") ? new ListItem("-- SELECT --", "0") : new ListItem("ALL", "0")));
        }

        public void FillItemSubGrp(DropDownList ddlpopSubGroup)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_ITEMSUBGRP", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlpopSubGroup.DataSource = dt;
            ddlpopSubGroup.DataTextField = "ItemSubGrp";
            ddlpopSubGroup.DataValueField = "ID";
            ddlpopSubGroup.DataBind();
            ddlpopSubGroup.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillBrand(DropDownList ddlBrand, int TODISPLAY)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            if (TODISPLAY == 0)
            {
                cmd = new SqlCommand("SP_MST_BRAND_SELECTALL", objMainClass.ConnSherpa);
            }
            else if (TODISPLAY == 1)
            {
                cmd = new SqlCommand("SP_MST_BRAND_SELECTALL_TODISPLAY", objMainClass.ConnSherpa);
            }
            cmd.Parameters.AddWithValue("@ISACTIVE", true);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlBrand.DataSource = dt;
            ddlBrand.DataTextField = "BRAND_NAME";
            ddlBrand.DataValueField = "BRAND_ID";
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillAddProductBrand(DropDownList ddlBrand)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("SP_MST_BRAND_SELECTALL_TODISPLAY", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ISACTIVE", true);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlBrand.DataSource = dt;
            ddlBrand.DataTextField = "BRAND_NAME";
            ddlBrand.DataValueField = "BRAND_NAME";
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillModel(DropDownList ddlModel, string strBrandId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_MODELBYBRAND", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@BRANDID", int.Parse(strBrandId));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlModel.DataSource = dt;
            ddlModel.DataTextField = "MODEL_NAME";
            ddlModel.DataValueField = "MODEL_ID";
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillVendType(DropDownList ddlVendType)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_VENDTYPE", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlVendType.DataSource = dt;
            ddlVendType.DataTextField = "VENDORNAME";
            ddlVendType.DataValueField = "VENDTYPE";
            ddlVendType.DataBind();
            ddlVendType.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillCity(DropDownList ddlCity, int reqfor = 0)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_CITY", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCity.DataSource = dt;
            ddlCity.DataTextField = "CITY";
            ddlCity.DataValueField = "CITY";
            ddlCity.DataBind();
            if (reqfor == 0)
            {
                ddlCity.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            else
            {
                ddlCity.Items.Insert(0, new ListItem("ALL", "ALL"));
            }
        }

        public void FillDetparment(DropDownList ddlDepartment)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECTALL_MST_DEPT", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "DEPTNAME";
            ddlDepartment.DataValueField = "DEPTCD";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillSegment(DropDownList ddlSegment)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_SEGMENT", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlSegment.DataSource = dt;
            ddlSegment.DataTextField = "SEGDESC";
            ddlSegment.DataValueField = "SEGCODE";
            ddlSegment.DataBind();
            ddlSegment.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void Fill_EMP_Enginner(DropDownList DDLENGINEER, int DESIGNATION, int STATUS)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_ENGINEER_EMP_DESIGNATION", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@DESIGNATION", DESIGNATION);
            cmd.Parameters.AddWithValue("@CMPID", "1");
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            DDLENGINEER.DataSource = dt;
            DDLENGINEER.DataTextField = "EMPNAME";
            DDLENGINEER.DataValueField = "ID";
            DDLENGINEER.DataBind();
            //DDLENGINEER.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }


        //New

        public void FillStatus(DropDownList ddlStatus, int StatusType)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_STATUS", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@STATUSTYPE", StatusType);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "STATUSDESC";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
            ddlStatus.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillDistChnl(DropDownList ddlDistChnl)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_DISTCHNL", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDistChnl.DataSource = dt;
            ddlDistChnl.DataTextField = "DISTCHNLDESC";
            ddlDistChnl.DataValueField = "DISTCHNL";
            ddlDistChnl.DataBind();
        }

        public void FillDistChnlNew(DropDownList ddlDistChnl)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_DISTCHNL", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDistChnl.DataSource = dt;
            ddlDistChnl.DataTextField = "DISTCHNLDESC";
            ddlDistChnl.DataValueField = "DISTCHNL";
            ddlDistChnl.DataBind();
            ddlDistChnl.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillLists(DropDownList ddlList, string ListType)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_LISTS", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@TYPE", ListType);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlList.DataSource = dt;
            ddlList.DataTextField = "LISTDESC";
            ddlList.DataValueField = "ID";
            ddlList.DataBind();
            ddlList.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillListsByValue(DropDownList ddlList, string ListType, int STATUS, string EXTFIELD, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECTLIST_WITHVALUE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@TYPE", ListType);
            cmd.Parameters.AddWithValue("@EXTFIELD", EXTFIELD);
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlList.DataSource = dt;
            ddlList.DataTextField = "LISTDESC";
            ddlList.DataValueField = "ID";
            ddlList.DataBind();
            ddlList.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillAddProductLists(DropDownList ddlList, string ListType)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_LISTS", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@TYPE", ListType);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlList.DataSource = dt;
            ddlList.DataTextField = "LISTDESC";
            ddlList.DataValueField = "LISTDESC";
            ddlList.DataBind();
            ddlList.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillSimCarrier(DropDownList ddlSimCarrier)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_CARRIER_SELECTALL", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ISACTIVE", true);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlSimCarrier.DataSource = dt;
            ddlSimCarrier.DataTextField = "CARRIER_NAME";
            ddlSimCarrier.DataValueField = "CARRIER_ID";
            ddlSimCarrier.DataBind();
            ddlSimCarrier.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillProductItemSubGroup(DropDownList ddlProduct, int STATUS)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_PRODUCT_FOR_LEAD", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "ITEMSUBGRP";
            ddlProduct.DataValueField = "ID";
            ddlProduct.DataBind();
            ddlProduct.Items.Insert(0, new ListItem("ALL", "0"));
        }

        public void FillSpecandValue(DropDownList ddlProduct, int STATUS, int ID, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SPECIFICATION_MASTER", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ISACTIVE", STATUS);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "VALUE";
            ddlProduct.DataValueField = "ID";
            ddlProduct.DataBind();
            //ddlProduct.Items.Insert(0, new ListItem("ALL", "0"));
        }


        public void FillState(DropDownList ddlState, DropDownList ddlCountry = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            if (ddlCountry != null)
            {
                cmd = new SqlCommand("SP_MST_STATE_SELECTBYCOUNTRY", objMainClass.ConnSherpa);
                cmd.Parameters.AddWithValue("@CNCD", ddlCountry.SelectedValue);
            }
            else
            {
                cmd = new SqlCommand("SP_MST_STATE_SELECTALL", objMainClass.ConnSherpa);
            }
            cmd.Parameters.AddWithValue("@STATUS", 1);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlState.DataSource = dt;
            ddlState.DataTextField = "STATE";
            ddlState.DataValueField = "ID";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillPaymentMode(DropDownList ddlPayMode)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_PAYMENTMODE", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlPayMode.DataSource = dt;
            ddlPayMode.DataTextField = "PayMode";
            ddlPayMode.DataValueField = "id";
            ddlPayMode.DataBind();
            ddlPayMode.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillParts(DropDownList ddlParts)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECTALL_ITEMSUBGRP", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@STATUS", 1);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlParts.DataSource = dt;
            ddlParts.DataTextField = "ItemSUBGrp";
            ddlParts.DataValueField = "ID";
            ddlParts.DataBind();
            ddlParts.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillArea(DropDownList ddlArea)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECTALL_MST_AREA", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlArea.DataSource = dt;
            ddlArea.DataTextField = "AREA";
            ddlArea.DataValueField = "ID";
            ddlArea.DataBind();
            ddlArea.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillCity(DropDownList ddlCity, string strStateId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_CITY_SELECT_BYSTATEID", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@STATE_ID", strStateId);
            cmd.Parameters.AddWithValue("@ISACTIVE", true);
            cmd.Parameters.AddWithValue("@STATUS", 1);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCity.DataSource = dt;
            ddlCity.DataTextField = "CITY_NAME";
            ddlCity.DataValueField = "CITY_ID";
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillItemByCat(DropDownList ddlItems, string strCat)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_ITEMBYCAT", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@ITEMCAT", strCat);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlItems.DataSource = dt;
            ddlItems.DataTextField = "ItemDesc";
            ddlItems.DataValueField = "ItemId";
            ddlItems.DataBind();
            ddlItems.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillBrandByItem(DropDownList ddlBrand, string ITEMID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("SP_SELECT_BRAND_BY_ITEM", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ITEMID", ITEMID);
            cmd.Parameters.AddWithValue("@ISACTIVE", true);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlBrand.DataSource = dt;
            ddlBrand.DataTextField = "BRAND_NAME";
            ddlBrand.DataValueField = "BRAND_ID";
            ddlBrand.DataBind();
            ddlBrand.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillModelByItem(DropDownList ddlModel, string strBrandId, string ItemID)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_MODELBYBRAND_AND_ITEMID", objMainClass.ConnSherpa);
            //cmd.Parameters.AddWithValue("@BRANDNAME", strBrand);
            cmd.Parameters.AddWithValue("@BRANDID", int.Parse(strBrandId));
            cmd.Parameters.AddWithValue("@ITEMID", ItemID);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlModel.DataSource = dt;
            ddlModel.DataTextField = "MODEL_NAME";
            ddlModel.DataValueField = "MODEL_ID";
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }



        public void FillDepartment(DropDownList ddlDepartment)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SELECTALL_MST_DEPT", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataTextField = "DEPTNAME";
            ddlDepartment.DataValueField = "DEPTCD";
            ddlDepartment.DataBind();
            ddlDepartment.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }



        // TATA SKY METHOD START

        public void FillCategory(DropDownList ddlCategory)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("TATASKYCATEGORYMASTERCRUDOPERATION", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@Action", "DROPDOWN");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = "CATNAME";
            ddlCategory.DataValueField = "CATNAME";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillStatus(DropDownList ddlStatus)
        {
            DataTable dt = new DataTable();
            DataColumn dtColumn;
            dtColumn = new DataColumn();
            dtColumn.ColumnName = "COMBOID";
            dt.Columns.Add(dtColumn);

            dtColumn = new DataColumn();
            dtColumn.ColumnName = "COMBONAME";
            dt.Columns.Add(dtColumn);

            dt.Rows.Add("-1", "SELECT");
            dt.Rows.Add("1", "ACTIVE");
            dt.Rows.Add("0", "INACTIVE");

            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "COMBONAME";
            ddlStatus.DataValueField = "COMBOID";
            ddlStatus.DataBind();
        }

        public void FillTaTaSkyReqDropDown(DropDownList ddlDropDown, string comboname, string req = "REQUESTDROPDOWN", string reqtype = "Entry")
        {
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
            ddlDropDown.DataSource = dt;
            ddlDropDown.DataTextField = "COMBONAME";
            ddlDropDown.DataValueField = "COMBOID";
            ddlDropDown.DataBind();
            if (reqtype == "Entry")
            {
                ddlDropDown.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            else
            {
                ddlDropDown.Items.Insert(0, new ListItem("ALL", "0"));
            }
        }

        public void FillTaTaSkyPartLocation(DropDownList ddlDropDown, string comboname, string req = "PARTLOCATION", string reqtype = "Entry", int comboid = 0)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("COMBOGET", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@COMBONAME", comboname);
            cmd.Parameters.AddWithValue("@COMBOREQUEST", req);
            cmd.Parameters.AddWithValue("@COMBOID", comboid);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDropDown.DataSource = dt;
            ddlDropDown.DataTextField = "COMBONAME";
            ddlDropDown.DataValueField = "COMBOID";
            ddlDropDown.DataBind();
            if (reqtype == "Entry")
            {
                ddlDropDown.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            else
            {
                ddlDropDown.Items.Insert(0, new ListItem("ALL", "0"));
            }
        }
        // TATA SKY METHOD END

        public void QuikeBRAND(DropDownList ddlBrands, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_SELECT_BRANDS", objMainClass.ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@Action", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlBrands.DataSource = dt;
                ddlBrands.DataTextField = "MAKE";
                ddlBrands.DataValueField = "BRAND_ID";
                ddlBrands.DataBind();
                ddlBrands.Items.Insert(0, new ListItem("-- SELECT --", "0"));


            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void QuikeModel(DropDownList ddlModel, int BRAND)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_SELECT_MODEL_BY_BRAND", objMainClass.ConnQuike);
            try
            {
                cmd.Parameters.AddWithValue("@BRAND", BRAND);
                cmd.Parameters.AddWithValue("@Action", "SHERPA360");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlModel.DataSource = dt;
                ddlModel.DataTextField = "MODEL";
                ddlModel.DataValueField = "MODEL_ID";
                ddlModel.DataBind();
                ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        //Vendor System 
        public void FillBrandVendor(DropDownList ddlBrand)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "BRAND");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlBrand.DataSource = dt;
                ddlBrand.DataTextField = "BRAND_NAME";
                ddlBrand.DataValueField = "BRAND_ID";
                ddlBrand.DataBind();
                ddlBrand.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillModelVendor(DropDownList ddlModel, int brandid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BRANDID", brandid);
                cmd.Parameters.AddWithValue("@ACTION", "MODEL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlModel.DataSource = dt;
                ddlModel.DataTextField = "MODEL_NAME";
                ddlModel.DataValueField = "MODEL_ID";
                ddlModel.DataBind();
                ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillModelSpecification(DropDownList ddlRom, DropDownList ddlRam, DropDownList ddlColor, string brand, string model)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@MAKE", brand);
                cmd.Parameters.AddWithValue("@MODEL", model);
                cmd.Parameters.AddWithValue("@ACTION", "SPECIFICATION");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                // Bind Rom
                ddlRom.DataSource = dt;
                ddlRom.DataTextField = "ROMSIZE";
                ddlRom.DataValueField = "ROMSIZE";
                ddlRom.DataBind();
                ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));

                // Bind Ram
                ddlRam.DataSource = dt;
                ddlRam.DataTextField = "RAMSIZE";
                ddlRam.DataValueField = "RAMSIZE";
                ddlRam.DataBind();
                ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));

                // Bind Color
                ddlColor.DataSource = dt;
                ddlColor.DataTextField = "COLOR";
                ddlColor.DataValueField = "COLOR";
                ddlColor.DataBind();
                ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        //New DropDown
        public void FillMobexSellerBrand(DropDownList ddlBrand, string req = "entry")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "ALLBRANDS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlBrand.DataSource = dt;
                ddlBrand.DataTextField = "BRAND_DESC";
                ddlBrand.DataValueField = "BRAND_ID";
                ddlBrand.DataBind();
                if (req == "entry")
                {
                    ddlBrand.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                }
                else
                {
                    ddlBrand.Items.Insert(0, new ListItem("ALL", "ALL"));
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerModel(DropDownList ddlModel, int brandid, string req = "entry", int ITEMGRPID = 0, int ITEMSUBGRPID = 0)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BRANDID", brandid);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.Parameters.AddWithValue("@ACTION", "BRANDSSPECIFICMODEL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlModel.DataSource = dt;
                ddlModel.DataTextField = "MODEL_NAME";
                ddlModel.DataValueField = "MODEL_ID";
                ddlModel.DataBind();
                if (req == "entry")
                {
                    ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                }
                else
                {
                    ddlModel.Items.Insert(0, new ListItem("ALL", "ALL"));
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerSearchModel(DropDownList ddlModel, int brandid, string req = "entry")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BRANDID", brandid);
                cmd.Parameters.AddWithValue("@ACTION", "BRANDSSPECIFICSEARCHMODEL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlModel.DataSource = dt;
                ddlModel.DataTextField = "MODEL_NAME";
                ddlModel.DataValueField = "MODEL_ID";
                ddlModel.DataBind();
                if (req == "entry")
                {
                    ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                }
                else
                {
                    ddlModel.Items.Insert(0, new ListItem("ALL", "ALL"));
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerAllModel(DropDownList ddlModel)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "ALLMODELS");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlModel.DataSource = dt;
                ddlModel.DataTextField = "MODEL_NAME";
                ddlModel.DataValueField = "MODEL_NAME";
                ddlModel.DataBind();
                ddlModel.Items.Insert(0, new ListItem("ALL", "ALL"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerRam(DropDownList ddlRam, int brandid, int modelid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "BRANDMODELSPECIFICRAM");
                cmd.Parameters.AddWithValue("@BRANDID", brandid);
                cmd.Parameters.AddWithValue("@MODEL_ID", modelid);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlRam.DataSource = dt;
                ddlRam.DataTextField = "RAMSIZE";
                ddlRam.DataValueField = "RAMSIZE";
                ddlRam.DataBind();

                ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerRom(DropDownList ddlRom, int brandid, int modelid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "BRANDMODELSPECIFICROM");
                cmd.Parameters.AddWithValue("@BRANDID", brandid);
                cmd.Parameters.AddWithValue("@MODEL_ID", modelid);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlRom.DataSource = dt;
                ddlRom.DataTextField = "ROMSIZE";
                ddlRom.DataValueField = "ROMSIZE";
                ddlRom.DataBind();
                ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public void FillMobexSellerColor(DropDownList ddlColor, int brandid, int modelid, string ram, string rom)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "BRANDMODELSPECIFICCOLOR");
                cmd.Parameters.AddWithValue("@BRANDID", brandid);
                cmd.Parameters.AddWithValue("@MODEL_ID", modelid);
                cmd.Parameters.AddWithValue("@RAMSIZE", ram);
                cmd.Parameters.AddWithValue("@ROMSIZE", rom);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlColor.DataSource = dt;
                ddlColor.DataTextField = "COLOR";
                ddlColor.DataValueField = "COLOR";
                ddlColor.DataBind();
                ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerVendor(DropDownList ddlVendor, string type = "regular")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "VENDORSDETAIL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlVendor.DataSource = dt;
                if (type == "regular")
                {
                    ddlVendor.DataTextField = "VENDORNAME";
                    ddlVendor.DataValueField = "VENDORNAME";
                }
                else
                {
                    ddlVendor.DataTextField = "VENDORNAME";
                    ddlVendor.DataValueField = "VENDORID";
                }
                ddlVendor.DataBind();
                ddlVendor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerPartnerVendor(DropDownList ddlVendor)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "PARTNERVENDORSDETAIL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlVendor.DataSource = dt;

                ddlVendor.DataTextField = "VENDORNAME";
                ddlVendor.DataValueField = "VENDORID";
                ddlVendor.DataBind();
                ddlVendor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }



        public void FillMobexSellerVendorByBikerAreaWise(DropDownList ddlVendor, int userid, string requestfor = "regular")
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "BIKERAREAWISEBYVENDORSDETAIL");
                cmd.Parameters.AddWithValue("@CREATEBY", userid);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                ddlVendor.DataSource = dt;
                ddlVendor.DataTextField = "VENDORNAME";
                ddlVendor.DataValueField = "VENDORID";
                ddlVendor.DataBind();
                if (requestfor == "regular")
                {
                    ddlVendor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                }
                else if (requestfor == "not regular")
                {
                    ddlVendor.Items.Insert(0, new ListItem("ALL", "0"));
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerAllVendorForUnliste(DropDownList ddlVendor)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "ALLVENDORDETAILFORUNLIST");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                ddlVendor.DataSource = dt;
                ddlVendor.DataTextField = "VENDORNAME";
                ddlVendor.DataValueField = "VENDORID";
                ddlVendor.DataBind();
                ddlVendor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public void FillMobexDealerAssociateVendor(DropDownList ddlDealerVendor, int delaerid)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@DEALERID", delaerid);
                cmd.Parameters.AddWithValue("@ACTION", "DEALERVENDOR");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();
                ddlDealerVendor.DataSource = dt;
                ddlDealerVendor.DataValueField = "VENDCODE";
                ddlDealerVendor.DataTextField = "VENDORNAME";
                ddlDealerVendor.DataBind();
                ddlDealerVendor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public DataTable FillCountry(int STATUS, DropDownList ddlCountry)
        {
            //SP_SELECT_MST_COUNTRY
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_COUNTRY", objMainClass.ConnSherpa);

            try
            {
                cmd.Parameters.AddWithValue("@status", STATUS);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlCountry.DataSource = dt;
                ddlCountry.DataTextField = "country";
                ddlCountry.DataValueField = "cncd";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                ddlCountry.SelectedValue = "IN";
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }

            return dt;
        }

        public void FillBiker(DropDownList ddlBiker, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DAILY_VISIT_REPORT", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlBiker.DataSource = dt;
                ddlBiker.DataTextField = "USERNAME";
                ddlBiker.DataValueField = "ID";
                ddlBiker.DataBind();
                ddlBiker.Items.Insert(0, new ListItem("--- SELECT ---", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillDealer(DropDownList ddlDealer, int STATUS)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_DEALER_MASTER", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@STATUS", STATUS);
            cmd.Parameters.AddWithValue("@ACTION", "BIND");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDealer.DataSource = dt;
            ddlDealer.DataTextField = "DEALERNAME";
            ddlDealer.DataValueField = "ID";
            ddlDealer.DataBind();
            ddlDealer.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillVendByType(DropDownList ddlVend, string VENDTYPE, int CMPID, int STATUS)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_VENDOR_MASTER", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", "VENDSEARCH");
            cmd.Parameters.AddWithValue("@CMPID", CMPID);
            cmd.Parameters.AddWithValue("@ISACTIVE", STATUS);
            cmd.Parameters.AddWithValue("@VENDTYPE", VENDTYPE);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlVend.DataSource = dt;
            ddlVend.DataTextField = "SHOPNAME";
            ddlVend.DataValueField = "VENDCODE";
            ddlVend.DataBind();
            ddlVend.Items.Insert(0, new ListItem("-- Select --", "0"));
        }

        public void FillStatuses(DropDownList ddlStatus)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_ALL_STATUS", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "STATUSDESC";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
            //ddlStatus.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillStatuseCRM(DropDownList ddlStatus)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_ALL_STATUSCRM", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "STATUSDESC";
            ddlStatus.DataValueField = "ID";
            ddlStatus.DataBind();
            //ddlStatus.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillCustype(DropDownList ddlCustType, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_CUSTOMER_MASTER", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlCustType.DataSource = dt;
            ddlCustType.DataTextField = "DESCR";
            ddlCustType.DataValueField = "CUSTTYPE";
            ddlCustType.DataBind();
            ddlCustType.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillAgentName(DropDownList ddlAgent)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", "AGENTCNT");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlAgent.DataSource = dt;
            ddlAgent.DataTextField = "USERNAME";
            ddlAgent.DataValueField = "ID";
            ddlAgent.DataBind();
            ddlAgent.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void GetLeadProduct(DropDownList ddlLeadProduct, string ACTION)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_LEAD_GENERATION", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlLeadProduct.DataSource = dt;
            ddlLeadProduct.DataTextField = "PRODUCT";
            ddlLeadProduct.DataValueField = "PRODUCT";
            ddlLeadProduct.DataBind();
            ddlLeadProduct.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillMakeModel(DropDownList ddlModel, string strBrandId)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_MODELBYMAKE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@BRANDID", int.Parse(strBrandId));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlModel.DataSource = dt;
            ddlModel.DataTextField = "MODEL_NAME";
            ddlModel.DataValueField = "MODEL_ID";
            ddlModel.DataBind();
            ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillBDO(DropDownList ddlBDO, string ACTION)
        {
            //SP_BDO_MAPPING
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BDO_MAPPING", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlBDO.DataSource = dt;
                ddlBDO.DataTextField = "USERNAME";
                ddlBDO.DataValueField = "ID";
                ddlBDO.DataBind();
                ddlBDO.Items.Insert(0, new ListItem("--- SELECT ---", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }


        public void FillSM(DropDownList ddlBDO, int STATUS, int SMTYPE, string ACTION)
        {
            //SP_BDO_MAPPING
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_BDO_MAPPING", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@SMTYPE", SMTYPE);
                cmd.Parameters.AddWithValue("@STATUS", STATUS);
                cmd.Parameters.AddWithValue("@ACTION", ACTION);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlBDO.DataSource = dt;
                ddlBDO.DataTextField = "USERNAME";
                ddlBDO.DataValueField = "ID";
                ddlBDO.DataBind();
                ddlBDO.Items.Insert(0, new ListItem("--- SELECT ---", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillItemType(DropDownList ddlItemType)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("MAKEMODEL_SPEC_CATEGORYCRUDOPERATION", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", "SELECTALL");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlItemType.DataSource = dt;
            ddlItemType.DataTextField = "ITEMTYPENAME";
            ddlItemType.DataValueField = "ITEMTYPEID";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillModelSpecDropDown(DropDownList ddlDropDown, string req)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("COMBOGET", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@COMBOREQUEST", req);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDropDown.DataSource = dt;
            ddlDropDown.DataTextField = "COMBONAME";
            ddlDropDown.DataValueField = "COMBOID";
            ddlDropDown.DataBind();
            ddlDropDown.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillMobexSellerOtherDeviceModel(DropDownList ddlModel, int brandid, string req = "entry", int ITEMGRPID = 0, int ITEMSUBGRPID = 0)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_FOR_OTHERDEVICE", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@BRANDID", brandid);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.Parameters.AddWithValue("@ACTION", "BRANDSSPECIFICMODEL");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlModel.DataSource = dt;
                ddlModel.DataTextField = "MODEL_NAME";
                ddlModel.DataValueField = "MODEL_ID";
                ddlModel.DataBind();
                if (req == "entry")
                {
                    ddlModel.Items.Insert(0, new ListItem("-- SELECT --", "0"));
                }
                else
                {
                    ddlModel.Items.Insert(0, new ListItem("ALL", "ALL"));
                }
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerOtherDeviceRam(DropDownList ddlRam, int brandid, int modelid, int ITEMGRPID = 0, int ITEMSUBGRPID = 0)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_FOR_OTHERDEVICE", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "BRANDMODELSPECIFICRAM");
                cmd.Parameters.AddWithValue("@BRANDID", brandid);
                cmd.Parameters.AddWithValue("@MODEL_ID", modelid);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlRam.DataSource = dt;
                ddlRam.DataTextField = "RAMSIZE";
                ddlRam.DataValueField = "RAMSIZE";
                ddlRam.DataBind();

                ddlRam.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerOtherDeviceRom(DropDownList ddlRom, int brandid, int modelid, int ITEMGRPID = 0, int ITEMSUBGRPID = 0)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_FOR_OTHERDEVICE", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "BRANDMODELSPECIFICROM");
                cmd.Parameters.AddWithValue("@BRANDID", brandid);
                cmd.Parameters.AddWithValue("@MODEL_ID", modelid);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlRom.DataSource = dt;
                ddlRom.DataTextField = "ROMSIZE";
                ddlRom.DataValueField = "ROMSIZE";
                ddlRom.DataBind();
                ddlRom.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillMobexSellerOtherDeviceColor(DropDownList ddlColor, int brandid, int modelid, string ram, string rom
            , int ITEMGRPID = 0, int ITEMSUBGRPID = 0)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_FOR_OTHERDEVICE", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "BRANDMODELSPECIFICCOLOR");
                cmd.Parameters.AddWithValue("@BRANDID", brandid);
                cmd.Parameters.AddWithValue("@MODEL_ID", modelid);
                cmd.Parameters.AddWithValue("@RAMSIZE", ram);
                cmd.Parameters.AddWithValue("@ROMSIZE", rom);
                cmd.Parameters.AddWithValue("@ITEMGRPID", ITEMGRPID);
                cmd.Parameters.AddWithValue("@ITEMSUBGRPID", ITEMSUBGRPID);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlColor.DataSource = dt;
                ddlColor.DataTextField = "COLOR";
                ddlColor.DataValueField = "COLOR";
                ddlColor.DataBind();
                ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        public void FillPlantIsMobex(DropDownList ddlPlant, int ISMOBEX)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_PLANT_ISMOBEX", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@ISMOBEX", ISMOBEX);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlPlant.DataSource = dt;
            ddlPlant.DataTextField = "Descr";
            ddlPlant.DataValueField = "PLANTCD";
            ddlPlant.DataBind();
            ddlPlant.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillPlantIsMobexRadio(RadioButtonList ddlPlant, int ISMOBEX)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_PLANT_ISMOBEX", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@ISMOBEX", ISMOBEX);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlPlant.DataSource = dt;
            ddlPlant.DataTextField = "Descr";
            ddlPlant.DataValueField = "PLANTCD";
            ddlPlant.DataBind();
            ddlPlant.Items.Insert(0, new ListItem("All", "0"));
        }
        public void FillMobexSellerColorByValue(DropDownList ddlColor, string BRAND_DESC, string MODEL_NAME, string ram, string rom)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER", objMainClass.ConnSherpa);
            try
            {
                cmd.Parameters.AddWithValue("@ACTION", "BRANDMODELSPECIFICCOLORBYVALUE");
                cmd.Parameters.AddWithValue("@BRAND", BRAND_DESC);
                cmd.Parameters.AddWithValue("@MODEL", MODEL_NAME);
                cmd.Parameters.AddWithValue("@RAMSIZE", ram);
                cmd.Parameters.AddWithValue("@ROMSIZE", rom);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                cmd.Connection.Close();

                ddlColor.DataSource = dt;
                ddlColor.DataTextField = "COLOR";
                ddlColor.DataValueField = "COLOR";
                ddlColor.DataBind();
                ddlColor.Items.Insert(0, new ListItem("-- SELECT --", "0"));
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                throw ex;
            }
        }

        //04-02-2023 swetal start
        public void FillListTypeFor(DropDownList ddlisttype, string ACTION = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_LISTS_CRUD", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlisttype.DataSource = dt;
            ddlisttype.DataTextField = "LISTTYPEFOR";
            ddlisttype.DataValueField = "LISTTYPEFOR";
            ddlisttype.DataBind();
            ddlisttype.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        //03-02-2023 swetal
        //start
        public void FillList(DropDownList ddlList, string ACTION = null)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_MST_LISTS_CRUD", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlList.DataSource = dt;
            ddlList.DataTextField = "LISTDESCRIPTION";
            ddlList.DataValueField = "LISTTYPE";
            // ddlList.DataValueField = "ID";
            ddlList.DataBind();
            ddlList.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        //06-02-2023 swetal
        //start
        public void FillModelList(DropDownList ddlbrandlist, string ACTION = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cd = new SqlCommand("SP_MST_Model_List", objMainClass.ConnSherpa);
            cd.Parameters.AddWithValue("@ACTION", ACTION);
            cd.CommandType = CommandType.StoredProcedure;
            cd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cd);
            da.Fill(dt);
            cd.Connection.Close();
            ddlbrandlist.DataSource = dt;
            ddlbrandlist.DataTextField = "BRAND_NAME";
            ddlbrandlist.DataValueField = "BRAND_ID";

            ddlbrandlist.DataBind();
            ddlbrandlist.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        //end

        //06-02-2023 SWETAL START
        public void FillCreateModelList(DropDownList ddlproductcateg, string ACTION = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cd = new SqlCommand("SP_MST_Model_List", objMainClass.ConnSherpa);
            cd.Parameters.AddWithValue("@ACTION", ACTION);
            cd.CommandType = CommandType.StoredProcedure;
            cd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cd);
            da.Fill(dt);
            cd.Connection.Close();
            ddlproductcateg.DataSource = dt;
            ddlproductcateg.DataTextField = "ITEMDESC";
            ddlproductcateg.DataValueField = "ITEMID";
            ddlproductcateg.DataBind();
            ddlproductcateg.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }
        //END

        //15-02-2023 SWETAL START
        public void FillSalesChnl(DropDownList ddlsaleschnl, string LISTTYPE, string ACTION = null)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MST_SALESINVOICE", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@ACTION", ACTION);
            cmd.Parameters.AddWithValue("@LISTTYPE", LISTTYPE);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlsaleschnl.DataSource = dt;
            ddlsaleschnl.DataTextField = "LISTDESC";
            ddlsaleschnl.DataValueField = "LISTDESC";
            ddlsaleschnl.DataBind();
            ddlsaleschnl.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }
        //END

        public void FillPaymentGateway(DropDownList ddlPaymentGateway)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();

            cmd = new SqlCommand("SP_PAYMENTGATEWAY", objMainClass.ConnSherpa);
            //cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            //cmd.Parameters.AddWithValue("@ACTION", "VENDLIST");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            ddlPaymentGateway.DataSource = dt;
            ddlPaymentGateway.DataTextField = "PGNAME";
            ddlPaymentGateway.DataValueField = "ID";
            ddlPaymentGateway.DataBind();
            ddlPaymentGateway.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }
        //Vendor System 

        //Manan - 10-02-2023
        public void FillCustGroup(DropDownList ddlDistChnl)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_GROUP", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDistChnl.DataSource = dt;
            ddlDistChnl.DataTextField = "DESCR";
            ddlDistChnl.DataValueField = "CUSTGRP";
            ddlDistChnl.DataBind();
            ddlDistChnl.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        //Manan - 10-02-2023
        public void FillExciseVendType(DropDownList ddlDistChnl)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_VendType", objMainClass.ConnSherpa);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlDistChnl.DataSource = dt;
            ddlDistChnl.DataTextField = "DESCR";
            ddlDistChnl.DataValueField = "EXVENDTYPE";
            ddlDistChnl.DataBind();
            ddlDistChnl.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }


        public void FillFBALocation(DropDownList ddlFBALocation, int LISTINGTYPE)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_MOBEX_SELLER_JANGADKROLISTING_TEST", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@LISTINGTYPE", LISTINGTYPE);
            cmd.Parameters.AddWithValue("@ACTION", "GETFBALOCATION");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlFBALocation.DataSource = dt;
            ddlFBALocation.DataTextField = "FBALOCATIONVALUE";
            ddlFBALocation.DataValueField = "FBALOCATIONID";
            ddlFBALocation.DataBind();
            ddlFBALocation.Items.Insert(0, new ListItem("-- SELECT --", "0"));
        }

        public void FillPlantIsMobexCHK(CheckBoxList lstPlant, int ISMOBEX)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand("SP_SELECT_PLANT_ISMOBEX", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@ISMOBEX", ISMOBEX);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            lstPlant.DataSource = dt;
            lstPlant.DataTextField = "Descr";
            lstPlant.DataValueField = "PLANTCD";
            lstPlant.DataBind();
            //lstPlant.Items.Insert(0, new ListItem("ALL", "0"));
        }

        public void FillListCHK(CheckBoxList lst, string PLANTCD)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_UPLOADLISTING_DATA", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@CMPID", objMainClass.intCmpId);
            cmd.Parameters.AddWithValue("@ACTION", "GETGRADE");
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cmd.CommandTimeout = 1000;
            cmd.Connection.Open();
            da.Fill(dt);
            cmd.Connection.Close();
            lst.DataSource = dt;
            lst.DataTextField = "LISTDESC";
            lst.DataValueField = "ID";
            lst.DataBind();
            //lstCity.Items.Insert(0, new ListItem("ALL", "0"));
        }

        public void FillListsCHK(CheckBoxList ddlList, string ListType)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("SP_SELECT_MST_LISTS", objMainClass.ConnSherpa);
            cmd.Parameters.AddWithValue("@TYPE", ListType);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cmd.Connection.Close();
            ddlList.DataSource = dt;
            ddlList.DataTextField = "LISTDESC";
            ddlList.DataValueField = "ID";
            ddlList.DataBind();
            //ddlList.Items.Insert(0, new ListItem("ALL", "0"));
        }
    }
}