using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DBConnection
/// </summary>
public class DBConnection
{
    public static SqlConnection conn = null;
    static DBConnection()
    {
        string config = ConfigurationManager.ConnectionStrings["dbILMPV1ConnectionString"].ConnectionString;
        conn = new SqlConnection(config);
    }
   
}