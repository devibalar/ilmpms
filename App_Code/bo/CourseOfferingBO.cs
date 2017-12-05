using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CourseOfferingBO
/// </summary>
public class CourseOfferingBO
{
    ICourseOfferingDao courseOfferingObj = new CourseOfferingDaoImpl();
    public DataSet GetAllAvailableCourseCode()
    {
        DataSet ds;
        try
        {
            ds = courseOfferingObj.GetAllAvailableCourseCode();
        }
        catch (CustomException e)
        {
            throw e;
        }
        return ds;
    }
    public List<int> GetSemesterForCourseCode(string incourseCode)
    {
        List<int> offeredPeriod;
        try
        {
            offeredPeriod = courseOfferingObj.GetSemesterForCourseCode(incourseCode);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return offeredPeriod;
    }
    public List<int> GetYearForCourseCode(string incourseCode, int semester)
    {
        List<int> offeredYear;
        try
        {
            offeredYear = courseOfferingObj.GetYearForCourseCode(incourseCode, semester);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return offeredYear;
    }
    public Boolean AddCourseOffering(CourseOfferingVO inCourseOffering)
    {
        Boolean status = false;
        try
        {
            status = courseOfferingObj.AddCourseOffering(inCourseOffering);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }
    public Boolean DeleteCourseOffering(CourseOfferingVO inCourseOffering)
    {
        Boolean courseCode = false;
        try
        {
         courseCode = courseOfferingObj.DeleteCourseOffering(inCourseOffering);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return true;
    }

    public List<CourseOfferingVO> GetOfferedCoursesForSemester(int semester, int year)
    {
        List<CourseOfferingVO> courseOffered;
        try
        {
          courseOffered = courseOfferingObj.GetOfferedCoursesForSemester(semester,year);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return courseOffered;
    }
    public DataTable FillCourseCodeOffering()
    {
        DataTable dt = null;
        try
        {
            dt = courseOfferingObj.FillCourseCodeOffering();
        }
        catch (CustomException e)
        {
            throw e;
        }
        return dt;
    }
    public DataTable FillYearCourseOffering()
    {
        DataTable dt = null;
        try
        {
            dt = courseOfferingObj.FillYearCourseOffering();
        }
        catch (CustomException e)
        {
            throw e;
        }
        return dt;
    }
    public DataTable FillCourseCode()
    {
        DataTable dt = null;
        try
        {
            dt = courseOfferingObj.FillCourseCode();
        }
        catch (CustomException e)
        {
            throw e;
        }
        return dt;
    }

    public Boolean UpdateCourseOffering(CourseOfferingVO inCourseOffering, int oldSemester, int oldYear)
    {
        Boolean status=false;
        try
        {
            status= courseOfferingObj.UpdateCourseOffering(inCourseOffering,oldSemester,oldYear);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return status;
    }
     public List<CourseOfferingVO> GetOfferedCoursesForPgmMajor(int semester, int year, string programmeId, string majorId)
    {
        List<CourseOfferingVO> coursesOffered = new List<CourseOfferingVO>();
        try
        {
            coursesOffered = courseOfferingObj.GetOfferedCoursesForPgmMajor(semester,year,programmeId,majorId);
        }
        catch (CustomException e)
        {
            throw e;
        }
        return coursesOffered;
     }
     public DataSet FillYearForSemester(int semester)
     {
         DataSet ds = null;
         try
         {
             ds = courseOfferingObj.FillYearForSemester(semester);
         }
         catch(CustomException e)
         {
             throw e;
         }
         return ds;
     }
}