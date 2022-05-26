using System;
using System.Data;
using System.Configuration;
using System.Web;

using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;

/// <summary>
/// Summary description for xslFile
/// </summary>
public class xslFile
{
	public xslFile()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable GetData(string FilePath, string query)
    {
        DataTable dtImportData = new DataTable();
        try
        {
            string conn = string.Empty;


            conn = @"Provider=Microsoft.Jet.OLEDB.4.0; 
                         Data Source=" + FilePath + "; " +
                // "Extended Properties=Excel 12.0; IMEX=1";
                    "Extended Properties=" + (char)34 + "Excel 8.0;HDR=Yes;IMEX=1" + (char)34;

            OleDbConnection oledbconn = new OleDbConnection(conn);

            OleDbDataAdapter oledbda = new OleDbDataAdapter(query, oledbconn);
            oledbda.Fill(dtImportData);
        }
        catch (Exception ex)
        {
        }
        return dtImportData;
    }

    public static DataTable GetDataEx(string FilePath, string query)
    {
        DataTable dtImportData = new DataTable();
        try
        {
            string conn = string.Empty;


            conn = @"Provider=Microsoft.Jet.OLEDB.4.0; 
                         Data Source=" + FilePath + "; " +
                // "Extended Properties=Excel 12.0; IMEX=1";
                    "Extended Properties=" + (char)34 + "Excel 8.0;HDR=No;IMEX=1" + (char)34;


            //conn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+ FilePath+";Extended Properties=Excel 12.0 Xml;HDR=YES;IMEX=1";
                         

            OleDbConnection oledbconn = new OleDbConnection(conn);

            OleDbDataAdapter oledbda = new OleDbDataAdapter(query, oledbconn);
            oledbda.Fill(dtImportData);
            
        }
        catch (Exception ex)
        {
        }
        return dtImportData;
    }
}
