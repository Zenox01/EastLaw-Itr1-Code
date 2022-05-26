using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik;
using Telerik.Charting;
using Telerik.Web;
using System.Data;


namespace EastlawUI_v2.companyadminpanel
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         
            //Telerik.Charting.ChartSeries aa = new ChartSeries();
            ////aa.AddItem(23,"Umar",);
            ////aa.AddItem(23);
            ////aa.AddItem(23);
            ////aa.AddItem(23);
            ////aa.AddItem(23);
            //aa.DataXColumn.Insert(0,"55");
            //aa.DataYColumn.Insert(0,"55");
            //RadChart1.AddChartSeries(aa);
         




            //RadChart1.AutoLayout = true;

            //ChartSeries series = new ChartSeries();
            //series.Type = ChartSeriesType.Bar;
            ////series.DataYColumn = "Value";
            ////series.DataXColumn = "OADate";
            //RadChart1.Series[0].DataYColumn = "Value";
            //RadChart1.PlotArea.XAxis.DataLabelsColumn = "DD";
            //RadChart1.Series.Add(series);
            //////RadChart1.PlotArea.XAxis.Appearance.ValueFormat = ChartValueFormat.ShortDate;
            ////RadChart1.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 320;
            ////RadChart1.PlotArea.XAxis.IsZeroBased = false;

            //RadChart1.DataSource = GetData();
            //RadChart1.DataBind();


            //RadHtmlChart1.DataSource = GetData1();
            //RadHtmlChart1.DataBind();
            //RadChart1.DataSource = GetData();
            //// Set the column for data and data labels:
            //// Each bar will show "TotalSales", each label along
            //// X-axis will show "CategoryName.
            //RadChart1.Series[0].DataYColumn = "Value";
            //RadChart1.PlotArea.XAxis.DataLabelsColumn = "DD";
            //// assign appearance related properties
            //RadChart1.PlotArea.XAxis.Appearance.LabelAppearance.RotationAngle = 300;
            //RadChart1.PlotArea.XAxis.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.BlueViolet;
            //RadChart1.PlotArea.Appearance.Dimensions.Margins.Bottom =
            //Telerik.Charting.Styles.Unit.Percentage(30);
        }
        protected DataTable GetData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Value", typeof(int));
            //dt.Columns.Add("Date", typeof(double));
            dt.Columns.Add("DateTime", typeof(DateTime));
            dt.Columns.Add("DD", typeof(string));

            dt.Rows.Add(2, new DateTime(2011, 06, 12),"12/10/2016");
            dt.Rows.Add(5, new DateTime(2011, 12, 12), "12/10/2016");
            dt.Rows.Add(6, new DateTime(2012, 06, 17), "12/10/2016");
            dt.Rows.Add(4, new DateTime(2012, 09, 18), "12/10/2016");
            dt.Rows.Add(7, new DateTime(2013, 03, 18), "12/10/2016");
            
            dt.Columns.Add("OADate", typeof(double));
            foreach (DataRow dr in dt.Rows)
            {
                DateTime date = DateTime.Parse(dr["DateTime"].ToString());
                double newData = date.ToOADate();
                //dr["OADate"] = newData;
                dr["OADate"] = ConvertToJavaScriptDateTime(date);
            }
            return dt;
        }
        protected decimal ConvertToJavaScriptDateTime(DateTime fromDate)
        {
            return (decimal)fromDate.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        private DataSet GetData1()
        {
            DataSet ds = new DataSet("Bookstore");
            DataTable dt = new DataTable("Products");
            dt.Columns.Add("Id", Type.GetType("System.Int32"));
            dt.Columns.Add("Name", Type.GetType("System.String"));
            dt.Columns.Add("Price", Type.GetType("System.Double"));

            dt.Rows.Add(1, "12/Dec/2016", 5.45);
            dt.Rows.Add(2, "12/Dec/2016", 9.95);
            dt.Rows.Add(3, "5/Dec/2016", 1.99);
            dt.Rows.Add(4, "12/Dec/2016", 15.95);
            dt.Rows.Add(5, "12/Dec/2016", 0.95);
            dt.Rows.Add(6, "12/Dec/2016", 3.95);
            ds.Tables.Add(dt);
            return ds;
        }
    }
}