using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Data.SqlClient;
namespace EastlawUI_v2.adminpanel
{
    /// <summary>
    /// Summary description for ImageHandler
    /// </summary>
    public class ImageHandler : IHttpHandler
    {
        EastLawBL.Cases objCases = new EastLawBL.Cases();

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "image/jpeg";
            //context.Response.Write("Hello World");

            string val = "{4F42D19F-8ED2-4022-98D4-1D85DE257044}";

            //DataTable dtdataimages = objCases.GetCasesImagesByGUID(val);

            //if (dtdataimages.Rows.Count > 0)
            //{
            //    for (int a = 0; a < dtdataimages.Rows.Count; a++)
            //    {
            //        //objCases.GUID = dtdataimages.Rows[a]["guid"].ToString();
            //        //objCases.ImageData = GetBytes(dtdataimages.Rows[a]["ImgData"].ToString());
            //        //int chk = objCases.InsertCaseImagesMigrate();

                      
            //    }
            //}
            //context.Response.AddHeader("content-disposition", "attachment; filename=test");
            //context.Response.ContentType = "Name";
            //context.Response.BinaryWrite((byte[])GetBytes(dtdataimages.Rows[0]["ImgData"].ToString()));


          //  string sql = string.Format("EXECUTE sp_get_cases_images_byGUID @GUID = '{0}'", val);

            //SqlConnection sqlcon = new SqlConnection(DBHelper.GetConnectionString());
            //SqlCommand cm = sqlcon.CreateCommand();
            //sqlcon.Open();
            //cm.CommandText = "select data from tbl_CasesImages Where guid='" + val + "'";
            //byte[] img = (byte[])cm.ExecuteScalar();
            //sqlcon.Close();

            //context.Response.BinaryWrite(img);




            SqlConnection myConnection = new SqlConnection(DBHelper.GetConnectionString());
  myConnection.Open();
  string sql = "select data from tbl_CasesImages Where guid='" + val + "'";
  SqlCommand cmd = new SqlCommand(sql, myConnection);
  //cmd.Parameters.Add("@ImageId", SqlDbType.Int).Value = context.Request.QueryString["id"];
  cmd.Prepare();
  SqlDataReader dr = cmd.ExecuteReader();
  dr.Read();
  context.Response.ContentType = "Name";
  context.Response.BinaryWrite((byte[])dr["data"]);
  dr.Close();
  myConnection.Close(); 


         
        }
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}