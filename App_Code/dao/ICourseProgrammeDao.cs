using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ICourseProgrammeDao
/// </summary>
public interface ICourseProgrammeDao
{
    List<CourseProgrammeVO> GetCourseProgrammeForCourseCode(string courseCode);
}