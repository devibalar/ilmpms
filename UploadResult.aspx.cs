using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UploadResult : System.Web.UI.Page
{
    SemesterBO semesterBO = new SemesterBO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillSemesterDropDown();
            FillYearDropDown();
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        String failedUpdates = "";
        try
        {
            string semester = ddSemester.SelectedItem.Text;
            string year = ddYear.SelectedItem.Text;
            ResultVO resultVO = new ResultVO();
            //SqlTransaction transaction;         
        
            string filepath = Server.MapPath("~/Files/") + Path.GetFileName(fuResultUpload.PostedFile.FileName);
            if (fuResultUpload.HasFile)
            {
                fuResultUpload.SaveAs(filepath);
            
                string data = File.ReadAllText(filepath);
                List<ResultVO> results = new List<ResultVO>();
                Boolean headerRowHasBeenSkipped = false;
                foreach (string row in data.Split('\n'))
                {
                    if (headerRowHasBeenSkipped)
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            //dtable.Rows.Add();
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
                                //dtable.Rows[dtable.Rows.Count - 1][i] = celltemp;  
                                if (i == 0)
                                {
                                    resultVO.StudentId = int.Parse(celltemp);
                                }
                                else if (i == 1)
                                {
                                    resultVO.CourseCode = celltemp;
                                }
                                else if (i == 2)
                                {
                                    resultVO.Result = celltemp;
                                }
                                i++;
                            }
                            results.Add(resultVO);
                        }
                    }
                    headerRowHasBeenSkipped = true;
                }
                DBConnection.conn.Open();
                foreach(ResultVO result in results)
                {
                    SqlCommand cmd = new SqlCommand("spUpdateStudentFailedResult", DBConnection.conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@StudentId", result.StudentId));
                    cmd.Parameters.Add(new SqlParameter("@CourseCode", result.CourseCode));
                    cmd.Parameters.Add(new SqlParameter("@Result", result.Result));
                    cmd.Parameters.Add(new SqlParameter("@Semester", semester));
                    cmd.Parameters.Add(new SqlParameter("@Year", year));
                    int status = cmd.ExecuteNonQuery();
                    if (status < 0)
                    {
                        failedUpdates += result.StudentId + " - " + result.CourseCode +" , ";
                    }
                }
                UpdatePassResult();
                SqlCommand cmd1 = new SqlCommand("ilmpStatusUpdate", DBConnection.conn);
                cmd1.CommandType = CommandType.StoredProcedure;                
                int status1 = cmd1.ExecuteNonQuery();
                
                if (failedUpdates != "")
                {
                    Response.Write("<script>alert('Result updation failed for " + failedUpdates + "');</script>");
                }
                else
                {
                    Response.Write("<script>alert('Result uploaded successfully');</script>");
                }
           /*     DBConnection.conn.Open();
                using (transaction = DBConnection.conn.BeginTransaction())
                {
                    using (SqlBulkCopy bulkCopy_tblResult = new SqlBulkCopy(DBConnection.conn, SqlBulkCopyOptions.KeepIdentity, transaction))
                    {
                        using (SqlCommand cmd = new SqlCommand("dbo.spUpdateStudentFailedResult"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Connection = DBConnection.conn;
                            cmd.Parameters.AddWithValue("@StudentId", dtable);
                            cmd.Parameters.AddWithValue("@CourseCode", dtable);
                            cmd.Parameters.AddWithValue("@Semester",int.Parse(semester));
                            cmd.Parameters.AddWithValue("@Year",int.Parse(year));
                            cmd.Transaction = transaction;
                            int result = cmd.ExecuteNonQuery();                            
                        }
                    }                    
                    transaction.Commit();
                    DBConnection.conn.Close();
                    File.Delete(filepath);
                }*/
            }
        }
        catch (Exception ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
    }
    private void UpdatePassResult()
    {
        SqlCommand cmd = new SqlCommand("spUpdateStudentPassResult", DBConnection.conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Semester", int.Parse(ddSemester.SelectedItem.Text)));
        cmd.Parameters.Add(new SqlParameter("@Year", int.Parse(ddYear.SelectedItem.Text)));
        int status = cmd.ExecuteNonQuery();
    }
    private void FillSemesterDropDown()
    {
        DataSet ds = semesterBO.getAllSemesters();
        ddSemester.DataSource = ds;
        ddSemester.DataTextField = "SemesterID";
        ddSemester.DataValueField = "SemesterID";
        ddSemester.DataBind();
    }
    private void FillYearDropDown()
    {
        ArrayList arr = GetYear();
        foreach (ListItem item in arr)
        {
            ddYear.Items.Add(item);
        }
    }
    private ArrayList GetYear()
    {
        ArrayList arr = new ArrayList();
        string currentYearStr = DateTime.Now.Year.ToString();
        int currentYear = Int32.Parse(currentYearStr);
        arr.Add(new ListItem(currentYearStr, currentYearStr));
        arr.Add(new ListItem((currentYear -1).ToString(), (currentYear -1).ToString()));
        arr.Add(new ListItem((currentYear + 1).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear + 2).ToString(), (currentYear + 2).ToString()));
      
        return arr;
    }
    class ResultVO
    {
        private int studentId;

        public int StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }
        private string courseCode;

        public string CourseCode
        {
            get { return courseCode; }
            set { courseCode = value; }
        }
        private string result;

        public string Result
        {
            get { return result; }
            set { result = value; }
        }
    }
}