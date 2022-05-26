using System;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using iTextSharp.text.html;
namespace EastlawUI_v2
{
    internal class PDFSettings
    {
        public static int MarginLeft { get { return 60; } }
        public static int MarginRight { get { return 60; } }
        public static int MarginTop { get { return 100; } }
        public static int MarginBottom { get { return 50; } }
        public static int LogoScale { get { return 27; } }

    }
    public class PDFGenerator
    {
        public static void GeneratePDF(string Html, string FileName)
        {
            try
            {
                var template = new PDFTemplate();

                Document doc = new Document(PageSize.A4, PDFSettings.MarginLeft, PDFSettings.MarginRight, PDFSettings.MarginTop, PDFSettings.MarginBottom);

                // Change the content type to application/pdf !Important
                HttpContext.Current.Response.ContentType = "application/pdf";
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + FileName + ".pdf");

                // Get Instance of pdfWriter to be able to create the document in the OutputStream
                PdfWriter writer = PdfWriter.GetInstance(doc, HttpContext.Current.Response.OutputStream);

                writer.PageEvent = template;
                doc.Open();
                parseHTML(doc, Html);
                doc.Close();
                HttpContext.Current.Response.Flush();
            }
            catch(Exception ex)
            { }
        }
        private static void parseHTML(Document doc, string html)
        {
            HTMLWorker htmlWorker = new HTMLWorker(doc);
            //htmlWorker.SetStyleSheet(GenerateStyleSheet());
            htmlWorker.Parse(new StringReader(html));
            doc.AddSubject("www.eastlaw.com");
            doc.AddKeywords("Pakistan Law");
            doc.AddCreator("Online law");

        }
        private static StyleSheet GenerateStyleSheet()
        {
            //FontFactory.Register(@"c:\windows\fonts\gara.ttf", "Garamond");
            //FontFactory.Register(@"c:\windows\fonts\garabd.ttf");
            //FontFactory.Register(@"c:\windows\fonts\garait.ttf");


            FontFactory.Register(@"c:\windows\fonts\arial.ttf", "Arial");
            //FontFactory.Register(@"c:\windows\fonts\garabd.ttf");
            //FontFactory.Register(@"c:\windows\fonts\garait.ttf");

            StyleSheet css = new StyleSheet();

            //css.LoadTagStyle("body", "face", "Garamond");
            css.LoadTagStyle("body", "face", "Arial");
            css.LoadTagStyle("body", "encoding", "Identity-H");
            css.LoadTagStyle("body", "size", "10pt");
            css.LoadTagStyle("h1", "size", "30pt");
            css.LoadTagStyle("h1", "style", "line-height:30pt;font-weight:bold;");
            css.LoadTagStyle("h2", "size", "22pt");
            css.LoadTagStyle("h2", "style", "line-height:30pt;font-weight:bold;margin-top:5pt;margin-bottom:12pt;");
            css.LoadTagStyle("h3", "size", "15pt");
            css.LoadTagStyle("h3", "style", "line-height:25pt;font-weight:bold;margin-top:1pt;margin-bottom:15pt;");
            css.LoadTagStyle("h4", "size", "13pt");
            css.LoadTagStyle("h4", "style", "line-height:23pt;margin-top:1pt;margin-bottom:15pt;");
            css.LoadTagStyle("hr", "width", "100%");
            css.LoadTagStyle("a", "style", "text-decoration:underline;");
            //css.LoadTagStyle(HtmlTags.HEADERCELL, HtmlTags.wi.BORDERWIDTH, "0.5");
            //css.LoadTagStyle(HtmlTags.HEADERCELL, HtmlTags.BORDERCOLOR, "#333");
            //css.LoadTagStyle(HtmlTags.HEADERCELL, HtmlTags.BACKGROUNDCOLOR, "#cccccc");
            //css.LoadTagStyle(HtmlTags.CELL, HtmlTags.BACKGROUNDCOLOR, "#EFEFEF");
            //css.LoadTagStyle(HtmlTags.CELL, HtmlTags.BORDERWIDTH, "0.5");
            //css.LoadTagStyle(HtmlTags.CELL, HtmlTags.BORDERCOLOR, "#333");
            return css;
        }
    }
    internal class PDFTemplate : iTextSharp.text.pdf.PdfPageEventHelper
    {
        float TEXTSIZE = 13;

        public PdfTemplate total;
        //I create a font object to use within my footer
        protected Font FooterFont
        {
            get
            {
                return FontFactory.GetFont("Arial", 7);
            }
        }
        protected Font HeaderFont
        {
            get
            {
                return FontFactory.GetFont("Arial", TEXTSIZE);
            }
        }

        //I create a font object to use within my footer
        protected BaseFont BaseFnt
        {
            get
            {
                return BaseFont.CreateFont(@"c:\windows\fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            }
        }



        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            total = writer.DirectContent.CreateTemplate(100, 100);
            total.BoundingBox = new Rectangle(-20, -20, 100, 100);
        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            total.BeginText();
            total.SetFontAndSize(BaseFnt, TEXTSIZE);
            total.SetTextMatrix(0, 0);
            int pageNumber = writer.PageNumber - 1;
            total.ShowText("(" + pageNumber + ")");
            total.EndText();
        }

        //override the OnStartPage event handler to add our header
        public override void OnStartPage(PdfWriter writer, Document doc)
        {
            PdfContentByte cb = writer.DirectContent;
            cb.SaveState();
            string text = writer.PageNumber.ToString();
            float textBase = doc.Top;

            //Sidnummrering
            cb.BeginText();
            cb.SetFontAndSize(BaseFnt, TEXTSIZE);
            float adjust = BaseFnt.GetWidthPoint("0", TEXTSIZE);
            cb.SetTextMatrix(doc.Right - TEXTSIZE - adjust, textBase + 40);
            cb.ShowText(text);
            cb.EndText();

            //Datum
            cb.BeginText();
            cb.SetFontAndSize(BaseFnt, TEXTSIZE);
            cb.SetTextMatrix(doc.Right - 90, textBase + 20);
            // cb.NewlineText();
            cb.ShowText("Date: " + DateTime.Now.ToShortDateString());
            cb.EndText();

            cb.AddTemplate(total, doc.Right - adjust, textBase + 40);
            cb.RestoreState();

            PdfPTable headerTbl = new PdfPTable(1);
            headerTbl.TotalWidth = doc.PageSize.Width - (PDFSettings.MarginRight + PDFSettings.MarginLeft);

            Image logo = Image.GetInstance(HttpContext.Current.Server.MapPath("/style/img/logo2.jpg"));
            logo.ScalePercent(PDFSettings.LogoScale);

            //create instance of a table cell to contain the logo
            PdfPCell cell = new PdfPCell(logo);
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.PaddingLeft = PDFSettings.MarginLeft;
            cell.PaddingTop = PDFSettings.MarginTop;
            cell.Border = 0;
            headerTbl.AddCell(cell);

            headerTbl.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height + PDFSettings.MarginBottom), writer.DirectContent);


            string watermarkText = "www.eastlaw.pk";
            float fontSize = 50;
            float xPosition = 300;
            float yPosition = 400;
            float angle = 45;
            try
            {
                PdfContentByte under = writer.DirectContentUnder;
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED);
                under.BeginText();
                under.SetColorFill(iTextSharp.text.pdf.CMYKColor.LIGHT_GRAY);
                under.SetFontAndSize(baseFont, fontSize);
                under.ShowTextAligned(PdfContentByte.ALIGN_CENTER, watermarkText, xPosition, yPosition, angle);
                under.EndText();
            }
            catch (Exception ex)
            {
                //Console.Error.WriteLine(ex.Message);
            }

            //Watermark watermark = new Watermark(Image.getInstance("watermark.jpg"), 200, 420);
            //document.Add(watermark);

            //Image watermark = Image.GetInstance(logo);

            //watermark.SetAbsolutePosition(80, 200);


            //doc.Add(watermark);


        }


        public override void OnEndPage(PdfWriter writer, Document doc)
        {

            PdfPTable footerTbl = new PdfPTable(6);
            //set the width of the table to be the same as the document
            footerTbl.TotalWidth = doc.PageSize.Width - (PDFSettings.MarginRight + PDFSettings.MarginLeft);

            //Paragraph para = new Paragraph("Web Link", FooterFont);
            ////para.Add(Environment.NewLine);
            ////para.Add("EastLaw (Pvt.) Ltd.");
            ////para.Add(Environment.NewLine);
            ////para.Add("www.eastlaw.pk");
            //PdfPCell cell = new PdfPCell(para);
            //cell.Border = Rectangle.TOP_BORDER;
            //cell.BorderWidthTop = .5f;
            //// cell.PaddingLeft = 20;
            //footerTbl.AddCell(cell);

            //Paragraph para = new Paragraph("Company name", FooterFont);
            //para.Add(Environment.NewLine);
            //para.Add("EastLaw (Pvt.) Ltd.");
            //para.Add(Environment.NewLine);
            //para.Add("www.eastlaw.pk");
            //PdfPCell cell = new PdfPCell(para);
            //cell.Border = Rectangle.TOP_BORDER;
            //cell.BorderWidthTop = .5f;
            //// cell.PaddingLeft = 20;
            //footerTbl.AddCell(cell);

            //// 2nd cell 
            //para = new Paragraph("Address", FooterFont);
            //para.Add(Environment.NewLine);
            //para.Add("39-Link Farid Kot Road");
            //para.Add(Environment.NewLine);
            //para.Add("Lahore, Pakistan");
            //cell = new PdfPCell(para);
            //cell.Border = Rectangle.TOP_BORDER;
            //cell.BorderWidthTop = .5f;
            //footerTbl.AddCell(cell);

            //// 3d cell 
            //para = new Paragraph("WhatsApp", FooterFont);
            //para.Add(Environment.NewLine);
            //para.Add("0310-8131610");
            //cell = new PdfPCell(para);
            //cell.Border = Rectangle.TOP_BORDER;
            //cell.BorderWidthTop = .5f;
            //footerTbl.AddCell(cell);

            //// 4th cell 
            //para = new Paragraph("TELEPHONE", FooterFont);
            //para.Add(Environment.NewLine);
            //para.Add("042 37311670");
            ////para.Add(Environment.NewLine);
            ////para.Add("042 37311671");
            //cell = new PdfPCell(para);
            //cell.Border = Rectangle.TOP_BORDER;
            //cell.BorderWidthTop = .5f;
            //footerTbl.AddCell(cell);

            //// 5th cell 
            //para = new Paragraph("TELEPHONE", FooterFont);
            //para.Add(Environment.NewLine);
            //para.Add("042 37311671");
            ////para.Add(Environment.NewLine);
            ////para.Add("INT +46 8 12 31 45");
            //cell = new PdfPCell(para);
            //cell.Border = Rectangle.TOP_BORDER;
            //cell.BorderWidthTop = .5f;
            //footerTbl.AddCell(cell);

            //// 6th cell 
            //para = new Paragraph("Email", FooterFont);
            //para.Add(Environment.NewLine);
            //para.Add("info@eastlaw.pk");
            //para.Add(Environment.NewLine);
            //para.Add("support@eastlaw.pk");
            //cell = new PdfPCell(para);
            ////cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.Border = Rectangle.TOP_BORDER;
            //cell.BorderWidthTop = .5f;
            //footerTbl.AddCell(cell);

            //write the rows out to the PDF output stream.
            footerTbl.WriteSelectedRows(0, -1, PDFSettings.MarginLeft, (PDFSettings.MarginBottom), writer.DirectContent);


        }
    }
}