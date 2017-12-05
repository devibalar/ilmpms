using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseBO
/// </summary>
public class CourseBO
{
    ICourseDao courseobj = new CourseDaoImpl();
    public DataSet GetAllCourseCode()
    {
        DataSet ds;
        try
        {
             ds = courseobj.GetAllCourseCode();
        }
         catch (CustomException e)
         {
             throw e;
         }
        return ds;
    }
    public CourseVO GetCourseDetailsForCourseCode(string incourseCode)
    {
        CourseVO courseVO = new CourseVO();
        try{
         courseVO = courseobj.GetCourseDetailsForCourseCode(incourseCode);
         }
         catch (CustomException e)
         {
             throw e;
         }
        return courseVO;
    }
    public string AddCourse(CourseVO inCourse)
    {  
         string status = "";
         try
         {
             status = courseobj.AddCourse(inCourse);
         }
         catch (CustomException e)
         {
             throw e;
         }
        return status;
    }

    public string UpdateCourse(CourseVO inCourse, string originalCourseCode)
    {
        string status = "";
        try
        {
            status = courseobj.UpdateCourse(inCourse,originalCourseCode);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }

    public Boolean DeleteCourse(CourseVO inCourse)
    { 
        // Add business validations if any
        Boolean courseCode = courseobj.DeleteCourse(inCourse);
        return courseCode;
    }
    public List<CourseProgrammeVO> GetCourseForPgmMajor(string inprogrammeId, string inmajorid)
    {  
        List<CourseProgrammeVO> courses = courseobj.GetCourseForPgmMajor(inprogrammeId, inmajorid);
        return courses;
    }
    public ILMPCourseGridVO GetCourseDetailsForTemplate(CourseProgrammeVO courseProgarmmeVO)
    {
        ILMPCourseGridVO ilmpcourse = courseobj.GetCourseDetailsForTemplate(courseProgarmmeVO);
        return ilmpcourse;
    }
    public List<string> GetAllCourseCodeAsList()
    {
        List<string> courseCodes = new List<string>();
        try
        {
            courseCodes = courseobj.GetAllCourseCodeAsList();
        }
        catch(CustomException e)
        {
            throw e;
        }
        return courseCodes;
    }
}