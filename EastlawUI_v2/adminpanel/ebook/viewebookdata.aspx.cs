using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

namespace EastlawUI_v2.adminpanel.ebook
{
    public partial class viewebookdata : System.Web.UI.Page
    {
        EastLawBL.EBook objeb = new EastLawBL.EBook();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindData(9);
        }
        private void BindData(int BookID)
        {
            try
            {
                string cntnt = "";

                DataTable dtlevel1 = objeb.GetEBookParentIndex(BookID, 0);
                if (dtlevel1 != null)
                {
                    for (int l1 = 0; l1 < dtlevel1.Rows.Count; l1++)
                    {

                        cntnt = cntnt + dtlevel1.Rows[l1]["IndexContent"].ToString();
                        DataTable dtlevel2 = objeb.GetEBookParentIndex(BookID, int.Parse(dtlevel1.Rows[l1]["ID"].ToString()));
                        if (dtlevel2 != null)
                        {
                            for (int l2 = 0; l2 < dtlevel2.Rows.Count; l2++)
                            {

                                cntnt = cntnt + dtlevel2.Rows[l2]["IndexContent"].ToString();
                                DataTable dtlevel3 = objeb.GetEBookParentIndex(BookID, int.Parse(dtlevel2.Rows[l2]["ID"].ToString()));
                                if (dtlevel3 != null)
                                {
                                    for (int l3 = 0; l3 < dtlevel3.Rows.Count; l3++)
                                    {
                                        cntnt = cntnt + dtlevel3.Rows[l3]["IndexContent"].ToString();

                                      
                                    }
                                }
                            }
                        }

                    }
                }
                //Response.Write(cntnt);
                GeneratePDFBook(cntnt);
               
            }
            catch (Exception Ex) { throw Ex; }
        }
        void GeneratePDFBook(string html)
        {
            try
            {

                PDFGenerator.GeneratePDF(html, "book1");
            }
            catch { }

        }
    }
}