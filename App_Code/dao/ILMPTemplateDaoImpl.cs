using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ILMPTemplateDaoImpl
/// </summary>
public class ILMPTemplateDaoImpl:IILMPTemplateDao
{
    public string AddILMPTemplate(ILMPTemplateVO inIlmpTemplateVO)
    {
        string status = "";
        int ilmpTemplateId = 0;
        try
        {
            DBConnection.conn.Open();            
            SqlCommand cmd = new SqlCommand("dbo.spAddILMPForTemplate", DBConnection.conn);
            cmd.Parameters.AddWithValue("@ProgrammeId", inIlmpTemplateVO.ProgrammeId);
            cmd.Parameters.AddWithValue("@MajorId", inIlmpTemplateVO.MajorId);
            cmd.Parameters.AddWithValue("@TemplateName", inIlmpTemplateVO.TemplateName);
            cmd.Parameters.AddWithValue("@StudentId", inIlmpTemplateVO.StudentId);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ilmpTemplateId = int.Parse(reader[0].ToString());
            }
            reader.Close();            
            if (ilmpTemplateId > 0)
            {
                if (null != inIlmpTemplateVO.TemplateCourses && inIlmpTemplateVO.TemplateCourses.Count > 0)
                {
                    foreach (TemplateCourseVO templateCourse in inIlmpTemplateVO.TemplateCourses)
                    {
                        String query = "INSERT INTO dbo.TemplateCourse (TemplateId,CourseCode,CourseType,Semester,Year,WorkshopId) VALUES (@TemplateId,@CourseCode,@CourseType,@Semester,@Year,@WorkshopId)";
                        SqlCommand cmd1 = new SqlCommand(query, DBConnection.conn);
                        cmd1.Parameters.AddWithValue("@TemplateId", ilmpTemplateId);
                        cmd1.Parameters.AddWithValue("@Semester", templateCourse.Semester);
                        cmd1.Parameters.AddWithValue("@Year", templateCourse.Year);
                        cmd1.Parameters.AddWithValue("@CourseType", templateCourse.CourseType);
                        cmd1.Parameters.AddWithValue("@CourseCode", templateCourse.CourseCode);
                        cmd1.Parameters.AddWithValue("@WorkshopId", templateCourse.WorkshopId);
                        // Add other parameters here
                        int result = cmd1.ExecuteNonQuery();
                        if (result <= 0)
                        {
                            status = "Template creation failed";
                            break;
                        }
                        else
                        {
                            status = "Template added successfully";
                        }
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
            if (ex.Number == 2627)
            {
                throw new CustomException("Template name already exists.Please provide different template name");
            }
            else
            {
                throw new CustomException("Error in template addition.Please contact system administrator");
            }                        
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
    public DataSet GetAllProgrammeInTemplate()
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT DISTINCT t.ProgrammeID,p.ProgrammeName FROM dbo.IlmpTemplate t"+
                            " INNER JOIN dbo.Programme p ON t.ProgrammeID = p.ProgrammeID";
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "Programme");
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
    public DataSet GetAllMajorForProgramme(string programmeId)
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT DISTINCT MajorId FROM dbo.IlmpTemplate WHERE ProgrammeID ='"+programmeId+"'";
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "Major");
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
    public DataSet GetAllTemplateName(string programmeId,string majorId, int studentId)
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT TemplateName FROM dbo.IlmpTemplate WHERE ProgrammeID ='" 
                            + programmeId + "'"+" AND MajorId='"+majorId+"' AND StudentId='"+studentId+"'";
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "TemplateName");
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
    public ILMPTemplateVO GetTemplate(ILMPTemplateVO inilmptemplateVO)
    {
        ILMPTemplateVO ilmpTemplateVO = new ILMPTemplateVO();
        try
        {
            DBConnection.conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.spGetILMPTemplate", DBConnection.conn);
            cmd.Parameters.AddWithValue("@ProgrammeID", inilmptemplateVO.ProgrammeId);
            cmd.Parameters.AddWithValue("@MajorID", inilmptemplateVO.MajorId);
            cmd.Parameters.AddWithValue("@TemplateName", inilmptemplateVO.TemplateName);
            cmd.Parameters.AddWithValue("@StudentID", inilmptemplateVO.StudentId);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                List<TemplateCourseVO> templateCourses = new List<TemplateCourseVO>();
                TemplateCourseVO ilmpTemplateCourseVO;
                while (reader.Read())
                {
                    ilmpTemplateCourseVO = new TemplateCourseVO();
                    ilmpTemplateVO.TemplateId = int.Parse(reader["TemplateId"].ToString());
                    ilmpTemplateCourseVO.Semester = int.Parse(reader["Semester"].ToString());
                    ilmpTemplateCourseVO.Year = int.Parse(reader["Year"].ToString());
                    ilmpTemplateCourseVO.TemplateCourseId = int.Parse(reader["TemplateCourseID"].ToString());
                    ilmpTemplateCourseVO.CourseCode = reader["CourseCode"].ToString();
                    ilmpTemplateCourseVO.WorkshopId = int.Parse(reader["WorkshopID"].ToString());
                    templateCourses.Add(ilmpTemplateCourseVO);
                }      
                ilmpTemplateVO.TemplateName = inilmptemplateVO.TemplateName;
                ilmpTemplateVO.ProgrammeId = inilmptemplateVO.ProgrammeId;
                ilmpTemplateVO.MajorId = inilmptemplateVO.MajorId;
                ilmpTemplateVO.StudentId = inilmptemplateVO.StudentId;
                ilmpTemplateVO.TemplateCourses = templateCourses;
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
        return ilmpTemplateVO;
    }
    public ILMPTemplateVO GetCutomTemplate(ILMPTemplateVO inilmptemplateVO)
    {
        ILMPTemplateVO ilmpTemplateVO = new ILMPTemplateVO();
        try
        {
            DBConnection.conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.[spGetILMPCustomTemplate]", DBConnection.conn);
            cmd.Parameters.AddWithValue("@TemplateName", inilmptemplateVO.TemplateName);
            cmd.Parameters.AddWithValue("@StudentID", inilmptemplateVO.StudentId);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                List<TemplateCourseVO> templateCourses = new List<TemplateCourseVO>();
                TemplateCourseVO ilmpTemplateCourseVO;
                while (reader.Read())
                {
                    ilmpTemplateCourseVO = new TemplateCourseVO();
                    ilmpTemplateVO.TemplateId = int.Parse(reader["TemplateId"].ToString());
                    ilmpTemplateCourseVO.Semester = int.Parse(reader["Semester"].ToString());
                    ilmpTemplateCourseVO.Year = int.Parse(reader["Year"].ToString());
                    ilmpTemplateCourseVO.TemplateCourseId = int.Parse(reader["TemplateCourseID"].ToString());
                    ilmpTemplateCourseVO.CourseCode = reader["CourseCode"].ToString();
                    ilmpTemplateCourseVO.WorkshopId = int.Parse(reader["WorkshopID"].ToString());
                    templateCourses.Add(ilmpTemplateCourseVO);
                }
                ilmpTemplateVO.TemplateName = inilmptemplateVO.TemplateName;
                ilmpTemplateVO.ProgrammeId = inilmptemplateVO.ProgrammeId;
                ilmpTemplateVO.MajorId = inilmptemplateVO.MajorId;
                ilmpTemplateVO.StudentId = inilmptemplateVO.StudentId;
                ilmpTemplateVO.TemplateCourses = templateCourses;
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
        return ilmpTemplateVO;
    }

    public ILMPTemplateVO GetTemplateForId(int templateId)
    {
        ILMPTemplateVO ilmpTemplateVO = new ILMPTemplateVO();
        try
        {
            DBConnection.conn.Open();
            SqlCommand cmd = new SqlCommand("dbo.spGetILMPTemplateForId", DBConnection.conn);
            cmd.Parameters.AddWithValue("@TemplateId", templateId);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                List<TemplateCourseVO> templateCourses = new List<TemplateCourseVO>();
                TemplateCourseVO ilmpTemplateCourseVO;
                while (reader.Read())
                {
                    ilmpTemplateCourseVO = new TemplateCourseVO();
                    ilmpTemplateVO.TemplateId = int.Parse(reader["TemplateId"].ToString());
                    ilmpTemplateCourseVO.Semester = int.Parse(reader["Semester"].ToString());
                    ilmpTemplateCourseVO.Year = int.Parse(reader["Year"].ToString());
                    ilmpTemplateCourseVO.TemplateCourseId = int.Parse(reader["TemplateCourseID"].ToString());
                    ilmpTemplateCourseVO.CourseCode = reader["CourseCode"].ToString();
                    ilmpTemplateCourseVO.WorkshopId = int.Parse(reader["WorkshopID"].ToString());
                    templateCourses.Add(ilmpTemplateCourseVO);
                }
                ilmpTemplateVO.TemplateCourses = templateCourses;
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
        return ilmpTemplateVO;
    }
    public DataSet GetAllTemplateNameForStudent(int studentId)
    {
        DataSet ds = new DataSet();
        try
        {
            DBConnection.conn.Open();
            String query = "SELECT TemplateName FROM dbo.IlmpTemplate WHERE StudentId='" + studentId + "'";
            SqlDataAdapter cmd = new SqlDataAdapter(query, DBConnection.conn);
            cmd.Fill(ds, "TemplateName");
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
}