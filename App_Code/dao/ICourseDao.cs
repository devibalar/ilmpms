using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ICourse
/// </summary>
public interface ICourseDao
{
    DataSet GetAllCourseCode();
    CourseVO GetCourseDetailsForCourseCode(string courseCode);
    List<CourseProgrammeVO> GetCourseForPgmMajor(string inprogrammeId, string inmajorid);
    string AddCourse(CourseVO inCourse);
    string UpdateCourse(CourseVO inCourse, string originalCourseCode);
    Boolean DeleteCourse(CourseVO inCourse);
    ILMPCourseGridVO GetCourseDetailsForTemplate(CourseProgrammeVO courseProgarmmeVO);
    List<string> GetAllCourseCodeAsList();
}