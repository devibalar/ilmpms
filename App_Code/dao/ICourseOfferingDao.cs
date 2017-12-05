using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ICourseOfferingDao
/// </summary>
public interface ICourseOfferingDao
{
    DataSet GetAllAvailableCourseCode();
    List<int> GetSemesterForCourseCode(string incourseCode);
    List<int> GetYearForCourseCode(string incourseCode, int semester);
    Boolean DeleteCourseOffering(CourseOfferingVO inCourseOffering);
    Boolean AddCourseOffering(CourseOfferingVO inCourseOffering);
    List<CourseOfferingVO> GetOfferedCoursesForSemester(int semester, int year);
    DataTable FillCourseCodeOffering();
    DataTable FillYearCourseOffering();
    DataTable FillCourseCode();
    Boolean UpdateCourseOffering(CourseOfferingVO inCourseOffering, int oldSemester, int oldYear);
    List<CourseOfferingVO> GetOfferedCoursesForPgmMajor(int semester, int year, string programmeId, string majorId);
    List<CourseOfferingVO> GetOfferedCourses();
    DataSet FillYearForSemester(int semester);
}