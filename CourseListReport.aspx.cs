﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Collections;
using System.Text;
public partial class CourseListReport : System.Web.UI.Page
{
    DataTable dt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            FillSemesterDropDown();
            FillYearDropDown();
        }
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        showReport();
    }
    private void showReport()
    {
        try
        {
            rptViewer.Reset();
            DataTable dt = GetData(int.Parse(ddSemester.SelectedItem.Text), int.Parse(ddYear.SelectedItem.Value));
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            rptViewer.LocalReport.DataSources.Add(rds);
            rptViewer.LocalReport.ReportPath = "Report2.rdlc";
            ReportParameter[] rptParams = new ReportParameter[]{
            new ReportParameter( "Semester",ddSemester.SelectedItem.Text),
            new ReportParameter( "Year",ddYear.SelectedItem.Text)
            };
        rptViewer.LocalReport.SetParameters(rptParams);
        rptViewer.LocalReport.Refresh();
        }
        catch (CustomException ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
    }
    private DataTable GetData(int semester, int year)
    {
        dt = new DataTable();
        try
        {
            SqlCommand cmd = new SqlCommand("spStudentPerCourseReport", DBConnection.conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Semester", semester);
            cmd.Parameters.AddWithValue("@Year", year);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
        }
        catch (SqlException ex)
        {
            Response.Write("<script>alert('"+ex.Message+"');</script>");
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
    private void FillSemesterDropDown()
    {
        SemesterBO semesterBO = new SemesterBO();
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
        ddYear.Text = DateTime.Now.Year.ToString();
    }
    private ArrayList GetYear()
    {
        ArrayList arr = new ArrayList();
        string currentYearStr = DateTime.Now.Year.ToString();
        int currentYear = Int32.Parse(currentYearStr);
        arr.Add(new ListItem((currentYear - 5).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear - 4).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear - 3).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear - 2).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear - 1).ToString(), (currentYear - 1).ToString()));
        arr.Add(new ListItem(currentYearStr, currentYearStr));
        arr.Add(new ListItem((currentYear + 1).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear + 2).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear + 3).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear + 4).ToString(), (currentYear + 1).ToString()));
        arr.Add(new ListItem((currentYear + 5).ToString(), (currentYear + 1).ToString()));

        return arr;
    }
    protected void btnExportCSV_Click(object sender, EventArgs e)
    {
        WriteIntoCSV();
    }
    private void WriteIntoCSV()
    {       
        StringBuilder sb = new StringBuilder();
       
        dt = GetData(int.Parse(ddSemester.SelectedItem.Text), int.Parse(ddYear.SelectedItem.Value));
        IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
                                            Select(column => column.ColumnName);
        sb.AppendLine(string.Join(",", columnNames));

        foreach (DataRow row in dt.Rows)
        {
            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
            sb.AppendLine(string.Join(",", fields));
        }
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment;filename=CourseListReport.csv");
        Response.ContentType = "text/csv";
        Response.Write(sb.ToString());
        Response.End();
       
    }
}