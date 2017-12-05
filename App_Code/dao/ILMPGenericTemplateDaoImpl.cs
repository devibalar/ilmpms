using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPGenericTemplateDaoImpl
/// </summary>
public class ILMPGenericTemplateDaoImpl:IILMPGenericTemplateDao
{   
    public string AddILMPTemplate(ILMPGenericTemplateVO inIlmpGenericTemplateVO)
    {
        string status = "";
        int templateId = 0;
        try
        {
            DBConnection.conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.spAddILMPForGenericTemplate", DBConnection.conn);
            cmd.Parameters.AddWithValue("@ProgrammeID", inIlmpGenericTemplateVO.ProgrammeID);
            cmd.Parameters.AddWithValue("@MajorID", inIlmpGenericTemplateVO.MajorID);
            cmd.Parameters.AddWithValue("@Title", inIlmpGenericTemplateVO.Title);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                templateId = int.Parse(reader[0].ToString());
            }
            reader.Close();
            
            if (templateId > 0)
            {
                if (null != inIlmpGenericTemplateVO.Courses && inIlmpGenericTemplateVO.Courses.Count > 0)
                {
                    using (SqlBulkCopy sqlbkcpy = new SqlBulkCopy(DBConnection.conn))
                    {
                        DataTable dtable = new DataTable();
                        dtable.Columns.AddRange(new DataColumn[3]
                        {        
                            new DataColumn("Semester", typeof(int)),
                            new DataColumn("CourseCount", typeof(int)),                              
                            new DataColumn("TemplateID", typeof(int))
                        });
                        int rowCount = 0;
                        foreach (ILMPGenericTemplateCourseVO course in inIlmpGenericTemplateVO.Courses)
                        {
                            dtable.Rows.Add();
                            dtable.Rows[dtable.Rows.Count - 1][0] = inIlmpGenericTemplateVO.Courses[rowCount].Semester;
                            dtable.Rows[dtable.Rows.Count - 1][1] = inIlmpGenericTemplateVO.Courses[rowCount].CourseCount;
                            dtable.Rows[dtable.Rows.Count - 1][2] = templateId;
                            rowCount++;
                        }
                        sqlbkcpy.DestinationTableName = "dbo.ILMPGenericTemplateCourse";
                        sqlbkcpy.WriteToServer(dtable);
                        status = "Template added successfully";
                    }
                }
                else
                {
                    status = "No semesters found in the ILMP template";
                }
            }
            else
            {
                status = "Error in addition";
            }
        }
        catch (SqlException ex)
        {
            ExceptionUtility.LogException(ex, "Error Page");
            throw ex;
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return status;
    }

    public DataSet GetTitleForProgrammeMajor(string programmeCode, string major)
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT Title FROM dbo.IlmpGenericTemplate WHERE ProgrammeID='"+programmeCode+"' AND MajorID='"+major+"'";           
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);            
            cmd.Fill(ds, "Title");            
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
        }
        catch (Exception e)
        {
            ExceptionUtility.LogException(e, "Error Page");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return ds;
    }

    public ILMPGenericTemplateVO GetGenericTemplate(string programme, string major, string title)
    {
        ILMPGenericTemplateVO ilmpGenericTemplateVO = new ILMPGenericTemplateVO();
        try
        {           
            DBConnection.conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.spGetGenericILMPTemplate", DBConnection.conn);
            cmd.Parameters.AddWithValue("@ProgrammeID", programme);
            cmd.Parameters.AddWithValue("@MajorID", major);
            cmd.Parameters.AddWithValue("@Title", title);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if(reader.HasRows)
            {
                List<ILMPGenericTemplateCourseVO> genericTemplateCourses = new List<ILMPGenericTemplateCourseVO>();
                ILMPGenericTemplateCourseVO ilmpGenericTemplateCourseVO ;
                while (reader.Read())
                {
                    ilmpGenericTemplateCourseVO = new ILMPGenericTemplateCourseVO();
                    ilmpGenericTemplateCourseVO.Semester = int.Parse(reader[0].ToString());
                    ilmpGenericTemplateCourseVO.CourseCount = int.Parse(reader[1].ToString());
                    ilmpGenericTemplateVO.TemplateID = int.Parse(reader[2].ToString());
                    genericTemplateCourses.Add(ilmpGenericTemplateCourseVO);
                }

                ilmpGenericTemplateVO.Title = title;
                ilmpGenericTemplateVO.ProgrammeID = programme;
                ilmpGenericTemplateVO.MajorID = major;
                ilmpGenericTemplateVO.Courses = genericTemplateCourses;
            }
            else
            {
                // return no templates message
            }
            reader.Close();
        }
        catch (SqlException e)
        {
            ExceptionUtility.LogException(e, "Error Page");
        }
        finally
        {
            if (DBConnection.conn != null)
            {
                DBConnection.conn.Close();
            }
        }
        return ilmpGenericTemplateVO;
    }
}