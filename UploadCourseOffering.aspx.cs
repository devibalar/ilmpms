using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadCourseOffering : System.Web.UI.Page
{
    string mainconn = ConfigurationManager.ConnectionStrings["dbILMPV1ConnectionString"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public void DeleteCourseOffering()
    {
        using (SqlConnection sqlconn = new SqlConnection(mainconn))
        {
            sqlconn.Open();
            string query = "TRUNCATE TABLE dbo.tmpCourseOffering ";
            SqlCommand cmd = new SqlCommand(query, sqlconn);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
        }
    }

    public void DeleteData()
    {
        List<ILMPCourseVO> ilmpCourses = new List<ILMPCourseVO>();
        using (SqlConnection sqlconn = new SqlConnection(mainconn))
        {
            sqlconn.Open();
            string query = "select distinct ic.coursecode,ic.Semester,ic.Year from ilmpcourse ic inner join ilmp i on i.IlmpID = ic.IlmpID and i.Active='Yes'";
            SqlCommand cmd = new SqlCommand(query, sqlconn);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                ILMPCourseVO ilmpCourseVO;
                while (reader.Read())
                {
                    ilmpCourseVO = new ILMPCourseVO();
                    ilmpCourseVO.CourseCode = reader["coursecode"].ToString();
                    ilmpCourseVO.Semester = int.Parse(reader["Semester"].ToString());
                    ilmpCourseVO.Year = int.Parse(reader["Year"].ToString());
                    ilmpCourses.Add(ilmpCourseVO);
                }
            }
            sqlconn.Close();
        }

        using (SqlConnection sqlconn = new SqlConnection(mainconn))
        {
            sqlconn.Open();
            string query = "DELETE FROM CourseOffering";
            SqlCommand cmd = new SqlCommand(query, sqlconn);
            cmd.ExecuteNonQuery();
            sqlconn.Close();
        }
    }
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        try
        {
            List<ILMPCourseVO> ilmpCourses = new List<ILMPCourseVO>();
            List<ILMPCourseVO> existingOffering;
            List<ILMPCourseVO> UserMessageCourseOffering = new List<ILMPCourseVO>();
            CourseOfferingDaoImpl courseOfferingDaoImpl = new CourseOfferingDaoImpl();
            List<CourseOfferingVO> allCourseOffering = new List<CourseOfferingVO>();
            List<CourseOfferingVO> existCourseOffering;

            /*  using (SqlConnection sqlconn = new SqlConnection(mainconn))
              {
                  sqlconn.Open();
                  string query = "select distinct ic.coursecode,ic.Semester,ic.Year from ilmpcourse ic inner join ilmp i on i.IlmpID = ic.IlmpID and i.Active='Yes'";
                  SqlCommand cmd = new SqlCommand(query, sqlconn);
                  SqlDataReader reader = cmd.ExecuteReader();
                  if (reader.HasRows)
                  {
                      ILMPCourseVO ilmpCourseVO;
                      while (reader.Read())
                      {
                          ilmpCourseVO = new ILMPCourseVO();
                          ilmpCourseVO.CourseCode = reader["coursecode"].ToString();
                          ilmpCourseVO.Semester = int.Parse(reader["Semester"].ToString());
                          ilmpCourseVO.Year = int.Parse(reader["Year"].ToString());
                          ilmpCourses.Add(ilmpCourseVO);
                      }
                  }
                  sqlconn.Close();
              }*/


            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                using (SqlBulkCopy sqlbkcpy = new SqlBulkCopy(sqlconn))
                {
                    string filepath = Server.MapPath("~/Files/") + Path.GetFileName(fsBulkCourseOfferingUpload.PostedFile.FileName);

                    if (fsBulkCourseOfferingUpload.HasFile)
                    {
                        fsBulkCourseOfferingUpload.SaveAs(filepath);

                        DataTable dtable = new DataTable();
                        dtable.Columns.AddRange(new DataColumn[4]
                         {
                            new DataColumn("CourseCode", typeof(string)),
                            new DataColumn("Semester", typeof(int)),
                            new DataColumn("Year", typeof(int)),
                            new DataColumn("CreatedDTM", typeof(string))
                        });

                        string data = File.ReadAllText(filepath);
                        Boolean headerRowHasBeenSkipped = false;
                        ILMPCourseVO uploadedCourse = new ILMPCourseVO();
                        CourseOfferingVO uploadedCourseOffering = new CourseOfferingVO();
                        DeleteCourseOffering();
                        foreach (string row in data.Split('\n'))
                        {
                            uploadedCourse = new ILMPCourseVO();
                            uploadedCourseOffering = new CourseOfferingVO();

                            if (headerRowHasBeenSkipped)
                            {
                                if (!string.IsNullOrEmpty(row))
                                {
                                    dtable.Rows.Add();
                                    int i = 0;
                                    foreach (string cell in row.Split(','))
                                    {
                                        string celltemp = "";
                                        if (cell.Contains("\r"))
                                        {
                                            celltemp = cell.Replace("\r", "");
                                        }
                                        else
                                        {
                                            celltemp = cell;
                                        }
                                        if (i == 0)
                                        {
                                            uploadedCourse.CourseCode = celltemp;
                                            uploadedCourseOffering.CourseCode = celltemp;
                                        }
                                        if (i == 1)
                                        {
                                            uploadedCourse.Semester = int.Parse(celltemp);
                                            uploadedCourseOffering.Semester = int.Parse(celltemp);
                                        }
                                        dtable.Rows[dtable.Rows.Count - 1][i] = celltemp;
                                        if (i == 2)
                                        {
                                            uploadedCourse.Year = int.Parse(celltemp);
                                            uploadedCourseOffering.Year = int.Parse(cell);
                                            dtable.Rows[dtable.Rows.Count - 1][3] = DateTime.Now;
                                        }
                                        i++;
                                    }
                                }
                                /* existingOffering = ilmpCourses.Where(c => c.CourseCode == uploadedCourse.CourseCode & c.Semester == uploadedCourse.Semester & c.Year == uploadedCourse.Year).ToList();
                                 if (existingOffering.Count > 0)
                                 {
                                     UserMessageCourseOffering.Concat(existingOffering);
                                     dtable.Rows[dtable.Rows.Count - 1].Delete();
                                 }
                                 */
                                /* DBConnection.conn.Open();
                                 SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM dbo.CourseOffering WHERE CourseCode='" + uploadedCourse.CourseCode + "' AND Semester=" + uploadedCourse.Semester + "AND Year=" + uploadedCourse.Year, DBConnection.conn);
                                 Int32 count = (Int32)cmd.ExecuteScalar();
                                 if (count > 0)
                                 {
                                     dtable.Rows[dtable.Rows.Count - 1].Delete();
                                 }
                                 DBConnection.conn.Close();*/
                            }
                            headerRowHasBeenSkipped = true;
                        }

                        sqlbkcpy.DestinationTableName = "dbo.tmpCourseOffering";
                        sqlconn.Open();
                        sqlbkcpy.WriteToServer(dtable);
                        sqlconn.Close();

                        // call stored procedure to perform course offering validations
                        DBConnection.conn.Open();
                        SqlCommand cmd1 = new SqlCommand("spLoadCourseOffering", DBConnection.conn);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        // execute the command
                        int result = cmd1.ExecuteNonQuery();
                        DBConnection.conn.Close();
                        ClientScript.RegisterStartupScript(this.GetType(), "successmsg", "File has successfully uploaded ");
                        //Response.Write("<script>alert('File has successfully uploaded. Please check CourseOffernigException.csv for errors');</script>");
                        WriteErrorsIntoCSV();
                        /*if (UserMessageCourseOffering.Count > 0)
                        {
                            string existingcl = "";
                            foreach (ILMPCourseVO cv in UserMessageCourseOffering)
                            {
                                existingcl = cv.CourseCode + ", " + cv.Semester + "," + cv.Year + ":";
                            }
                            Response.Write("<script>alert('ILMP exists for" + existingcl + "');</script>");
                        }
                        else
                        if (allCourseOffering.Count > 0)
                        {
                            string existco = "";
                            foreach (CourseOfferingVO co in allCourseOffering)
                            {
                                existco = co.CourseCode + "," + co.Semester + "," + co.Year + ":";
                            }
                            Response.Write("<script>alert('Course Code exists for " + existco + "');</script>");
                        }
                        else
                        {*/
                        
                        // }
                        File.Delete(filepath);
                    }
                    else
                    {
                        Response.Write("<script>alert('Please select file to upload');</script>");
                    }
                }
            }
        }
        catch (ThreadAbortException )
        {
            //Response.Write("<script>alert('File has successfully uploaded ');</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "successmsg", "File has successfully uploaded ");
            //ScriptManager.RegisterClientScriptBlock(this.Page, this.Page.GetType(), "alert", "alert(' File has successfully uploaded ');", true);
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            if (ex.Message.Contains("used by another process"))
            {
                Response.Write("<script>alert('CSV file is in use.Please close it and try again');</script>");
            }
            else
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/CourseOfferingList.aspx");
    }

    private void WriteErrorsIntoCSV()
    {       
        StringBuilder sb = new StringBuilder();
        DataTable dt = GetDataForCSV();
        IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                          Select(column => column.ColumnName);
        sb.AppendLine(string.Join(",", columnNames));
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
                sb.AppendLine(string.Join(",", fields));
            }
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment;filename=CourseOfferingErrors.csv");
            Response.ContentType = "text/csv";
            Response.Write(sb.ToString());
            Response.End();
        }
        else
        {
            Response.Write("<script>alert('File has successfully uploaded.');</script>");
        }
    }
    private DataTable GetDataForCSV()
    {
        DataTable dt = new DataTable();
        try
        {
            DBConnection.conn.Open();
            String query = "Select * from dbo.CourseOfferingException ";
            SqlCommand cmd = new SqlCommand(query, DBConnection.conn);   
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('Error in report');</script>");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return dt;
    }

}