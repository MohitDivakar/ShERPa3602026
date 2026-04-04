using System;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

public partial class Dashboard : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSalesData();
        }
    }

    private void LoadSalesData()
    {
        DataTable dtSales = new DataTable();
        dtSales.Columns.Add("Month");
        dtSales.Columns.Add("Category");
        dtSales.Columns.Add("Amount", typeof(decimal));

        dtSales.Rows.Add("Jan", "Electronics", 10000);
        dtSales.Rows.Add("Jan", "Grocery", 5000);
        dtSales.Rows.Add("Feb", "Electronics", 15000);
        dtSales.Rows.Add("Feb", "Grocery", 6000);
        dtSales.Rows.Add("Mar", "Electronics", 20000);
        dtSales.Rows.Add("Mar", "Grocery", 7000);

        gvSales.DataSource = dtSales;
        gvSales.DataBind();

        decimal totalSales = 0;
        foreach (DataRow row in dtSales.Rows)
            totalSales += Convert.ToDecimal(row["Amount"]);

        lblTotalSales.Text = "₹" + totalSales.ToString("N0");

        DataTable dtCategory = dtSales.Clone();
        foreach (DataRow dr in dtSales.Rows)
        {
            bool exists = false;
            foreach (DataRow catRow in dtCategory.Rows)
            {
                if (catRow["Category"].ToString() == dr["Category"].ToString())
                {
                    catRow["Amount"] = Convert.ToDecimal(catRow["Amount"]) + Convert.ToDecimal(dr["Amount"]);
                    exists = true;
                    break;
                }
            }
            if (!exists)
                dtCategory.Rows.Add("", dr["Category"], dr["Amount"]);
        }

        ChartCategorySales.Series["CategorySales"].Points.Clear();
        foreach (DataRow row in dtCategory.Rows)
        {
            ChartCategorySales.Series["CategorySales"].Points.AddXY(row["Category"].ToString(), row["Amount"]);
        }

        DataTable dtMonthly = dtSales.Clone();
        foreach (DataRow dr in dtSales.Rows)
        {
            bool exists = false;
            foreach (DataRow mRow in dtMonthly.Rows)
            {
                if (mRow["Month"].ToString() == dr["Month"].ToString())
                {
                    mRow["Amount"] = Convert.ToDecimal(mRow["Amount"]) + Convert.ToDecimal(dr["Amount"]);
                    exists = true;
                    break;
                }
            }
            if (!exists)
                dtMonthly.Rows.Add(dr["Month"], "", dr["Amount"]);
        }

        ChartMonthlySales.Series["MonthlySales"].Points.Clear();
        foreach (DataRow row in dtMonthly.Rows)
        {
            ChartMonthlySales.Series["MonthlySales"].Points.AddXY(row["Month"].ToString(), row["Amount"]);
        }
    }
}
